<template>
  <div class="qrcode-login-container">
    <!-- 二维码类型选择器 -->
    <a-tabs 
      v-if="enabledOptions.length > 1" 
      v-model:activeKey="selectedType" 
      @change="handleTypeChange"
      centered
      class="qrcode-type-tabs"
    >
      <a-tab-pane 
        v-for="option in enabledOptions" 
        :key="option.key" 
        :tab="option.name"
      >
        <template #tab>
          <span class="tab-content">
            <component :is="getProviderIcon(option.key)" />
            {{ option.name }}
          </span>
        </template>
      </a-tab-pane>
    </a-tabs>

    <!-- 二维码显示区域 -->
    <div class="qrcode-wrapper" :class="{ 'expired': isExpired }">
      <div v-if="loading" class="qrcode-loading">
        <a-spin size="large" />
      </div>
      
      <div v-else-if="isExpired" class="qrcode-expired">
        <exclamation-circle-outlined class="expired-icon" />
        <a-button type="primary" @click="refreshQrCode">
          {{ t('identity.auth.qrCodeLogin.refresh') }}
        </a-button>
      </div>
      
      <div v-else class="qrcode-display">
        <div class="qrcode-image">
          <img v-if="qrCodeData" :src="qrCodeData.qrCodeImage" :alt="t('identity.auth.qrCodeLogin.qrCode')" />
        </div>
        <div class="qrcode-status">
          <p v-if="status === 'waiting'">{{ t('identity.auth.qrCodeLogin.scanToLogin') }}</p>
          <p v-else-if="status === 'scanned'">{{ t('identity.auth.qrCodeLogin.confirmOnPhone') }}</p>
          <p v-else-if="status === 'confirmed'">{{ t('identity.auth.qrCodeLogin.loginSuccess') }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { ExclamationCircleOutlined, WechatOutlined, AlipayCircleOutlined } from '@ant-design/icons-vue'
import { getQrCode, checkQrCodeStatus } from '@/api/identity/auth/qrCodeAuth'

const { t } = useI18n()
const emit = defineEmits(['loginSuccess'])

// 定义props
interface Props {
  options?: Array<{
    key: string
    name: string
    type: string
  }>
}

const props = withDefaults(defineProps<Props>(), {
  options: () => []
})

// 状态
const loading = ref(false)
const isExpired = ref(false)
const status = ref<'waiting' | 'scanned' | 'confirmed'>('waiting')
const qrCodeData = ref<{ qrCodeImage: string; token: string } | null>(null)
const checkInterval = ref<NodeJS.Timeout | null>(null)
const selectedType = ref<string>('wechat')

// 计算启用的选项
const enabledOptions = computed(() => {
  return props.options || []
})

// 获取提供商图标组件
const getProviderIcon = (providerKey: string) => {
  const iconMap: Record<string, any> = {
    wechat: WechatOutlined,
    alipay: AlipayCircleOutlined
  }
  return iconMap[providerKey] || WechatOutlined
}

// 处理类型切换
const handleTypeChange = () => {
  console.log(`[二维码登录] 切换到: ${selectedType.value}`)
  refreshQrCode()
}

// 获取二维码
const getQrCodeData = async () => {
  try {
    loading.value = true
    isExpired.value = false
    status.value = 'waiting'
    
    // 将前端类型映射到后端枚举值
    const qrCodeTypeMap: Record<string, string> = {
      wechat: 'WeChatLogin',
      alipay: 'AlipayLogin'
    }
    
    const qrCodeType = qrCodeTypeMap[selectedType.value] || 'Login'
    
    // 根据选择的类型获取对应的二维码
    const { data } = await getQrCode(qrCodeType)
    qrCodeData.value = data.data
    
    // 开始轮询检查状态
    startStatusCheck()
    
    console.log(`[二维码登录] 获取${selectedType.value}二维码成功`)
  } catch (error) {
    console.error(`[二维码登录] 获取${selectedType.value}二维码失败:`, error)
    message.error(t('identity.auth.qrCodeLogin.getQrCodeFailed'))
    isExpired.value = true
  } finally {
    loading.value = false
  }
}

// 开始状态检查
const startStatusCheck = () => {
  if (checkInterval.value) {
    clearInterval(checkInterval.value)
  }
  
  checkInterval.value = setInterval(async () => {
    if (!qrCodeData.value?.token) return
    
    try {
      const { data } = await checkQrCodeStatus(qrCodeData.value.token)
      const newStatus = data.data.status
      
      if (newStatus !== status.value) {
        status.value = newStatus
        
        if (newStatus === 'confirmed') {
          // 登录成功
          message.success(t('identity.auth.qrCodeLogin.loginSuccess'))
          emit('loginSuccess', data.data.userInfo)
          stopStatusCheck()
        } else if (newStatus === 'expired') {
          // 二维码过期
          isExpired.value = true
          stopStatusCheck()
        }
      }
    } catch (error) {
      console.error('[二维码登录] 检查状态失败:', error)
    }
  }, 2000) // 每2秒检查一次
}

// 停止状态检查
const stopStatusCheck = () => {
  if (checkInterval.value) {
    clearInterval(checkInterval.value)
    checkInterval.value = null
  }
}

// 刷新二维码
const refreshQrCode = async () => {
  stopStatusCheck()
  await getQrCodeData()
}

// 重置所有状态
const resetAllStates = () => {
  console.log('[二维码登录] 开始重置所有状态')
  
  stopStatusCheck()
  loading.value = false
  isExpired.value = false
  status.value = 'waiting'
  qrCodeData.value = null
  
  // 重新获取二维码
  getQrCodeData()
  
  console.log('[二维码登录] 状态重置完成')
}

// 暴露重置方法给父组件
defineExpose({
  resetAllStates
})

// 组件挂载时初始化
onMounted(() => {
  console.log('[二维码登录] 开始初始化')
  
  // 设置默认选择的类型
  if (enabledOptions.value.length > 0) {
    selectedType.value = enabledOptions.value[0].key
  }
  
  getQrCodeData()
})

// 组件卸载时清理
onUnmounted(() => {
  stopStatusCheck()
})
</script>

<style lang="less" scoped>
.qrcode-login-container {
  width: 100%;
  max-width: 400px;
  margin: 0 auto;
  padding: 24px;
}

.qrcode-type-tabs {
  margin-bottom: 24px;
  
  .tab-content {
    display: flex;
    align-items: center;
    gap: 8px;
    
    .anticon {
      font-size: 16px;
    }
  }
}

.qrcode-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 300px;
  border: 1px solid var(--ant-color-border);
  border-radius: 8px;
  background: #fff;
  
  &.expired {
    border-color: var(--ant-color-error);
    background: var(--ant-color-error-bg);
  }
}

.qrcode-loading,
.qrcode-expired {
  text-align: center;
  padding: 40px 20px;
}

.expired-icon {
  font-size: 48px;
  color: var(--ant-color-error);
  margin-bottom: 16px;
}

.qrcode-display {
  text-align: center;
  padding: 20px;
}

.qrcode-image {
  margin-bottom: 16px;
  
  img {
    width: 200px;
    height: 200px;
    border-radius: 4px;
  }
}

.qrcode-status {
  p {
    color: var(--ant-color-text-secondary);
    font-size: 14px;
    margin: 0;
  }
}
</style> 