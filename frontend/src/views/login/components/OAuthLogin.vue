<template>
  <div class="third-party-login-container">
    <!-- 第三方登录选项 -->
    <div class="third-party-options" v-if="enabledProviders.length > 0">
      <!-- 动态渲染启用的OAuth提供商 -->
      <div 
        v-for="provider in enabledProviders" 
        :key="provider.key"
        class="login-option" 
        @click="handleOAuthLogin(provider.key)"
        :title="provider.name"
      >
        <div class="option-icon" :class="provider.key">
          <component :is="getProviderIcon(provider.key)" />
        </div>
      </div>
    </div>

    <!-- 如果没有启用的OAuth提供商，显示提示信息 -->
    <div v-else class="no-providers">
      <a-empty :description="t('identity.auth.thirdPartyLogin.noProviders')" />
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { 
  QqOutlined,
  WeiboOutlined,
  GithubOutlined,
  GoogleOutlined,
  FacebookOutlined,
  TwitterOutlined,
  WindowsOutlined,
  AppleOutlined,
  DingtalkOutlined,
  LinkedinOutlined,
  AmazonOutlined
} from '@ant-design/icons-vue'
import { 
  qqLogin, 
  weiboLogin, 
  githubLogin, 
  googleLogin
} from '@/api/identity/auth/oatuh'

const { t } = useI18n()

// 定义props
interface Props {
  providers?: Array<{
    key: string
    name: string
    icon: string
  }>
}

const props = withDefaults(defineProps<Props>(), {
  providers: () => []
})

// 状态
const loading = ref(false)

// 计算启用的提供商
const enabledProviders = computed(() => {
  return props.providers || []
})

// 获取提供商图标组件
const getProviderIcon = (providerKey: string) => {
  const iconMap: Record<string, any> = {

    google: GoogleOutlined,
    microsoft: WindowsOutlined,
    apple: AppleOutlined,
    amazon: AmazonOutlined,
    facebook: FacebookOutlined,
    twitter: TwitterOutlined,
    github: GithubOutlined,
    linkedin: LinkedinOutlined,
    qq: QqOutlined,
    dingtalk: DingtalkOutlined,
    weibo: WeiboOutlined
  }
  return iconMap[providerKey] || GithubOutlined
}

// 统一的OAuth登录处理函数
const handleOAuthLogin = async (providerKey: string) => {
  try {
    loading.value = true
    
    // 根据后端返回的provider key来调用对应的API
    let response: any
    
    switch (providerKey.toLowerCase()) {
      case 'github':
        response = await githubLogin()
        break
      case 'google':
        response = await googleLogin()
        break
      case 'facebook':
        // 需要添加Facebook登录API
        message.error('Facebook登录暂未实现')
        return
      case 'twitter':
        // 需要添加Twitter登录API
        message.error('Twitter登录暂未实现')
        return
      case 'qq':
        response = await qqLogin()
        break
      case 'microsoft':
        // 需要添加Microsoft登录API
        message.error('Microsoft登录暂未实现')
        return
      case 'apple':
        // 需要添加Apple登录API
        message.error('Apple登录暂未实现')
        return
      case 'amazon':
        // 需要添加Amazon登录API
        message.error('Amazon登录暂未实现')
        return
      case 'dingtalk':
        // 需要添加DingTalk登录API
        message.error('钉钉登录暂未实现')
        return
      case 'linkedin':
        // 需要添加LinkedIn登录API
        message.error('LinkedIn登录暂未实现')
        return
      case 'weibo':
        response = await weiboLogin()
        break
      default:
        message.error(t('identity.auth.thirdPartyLogin.unsupportedProvider'))
        return
    }
    
    if (response?.data?.code === 200 && response.data.data?.success) {
      // 跳转到授权页面
      window.location.href = response.data.data.authUrl
    } else {
      message.error(response?.data?.msg || t(`identity.auth.thirdPartyLogin.${providerKey}Failed`))
    }
  } catch (error: any) {
    console.error(`[OAuth登录] ${providerKey}登录失败:`, error)
    message.error(error.message || t(`identity.auth.thirdPartyLogin.${providerKey}Failed`))
  } finally {
    loading.value = false
  }
}

// 重置所有状态
const resetAllStates = () => {
  console.log('[第三方登录] 开始重置所有状态')
  loading.value = false
  console.log('[第三方登录] 状态重置完成')
}

// 暴露重置方法给父组件
defineExpose({
  resetAllStates
})
</script>

<style lang="less" scoped>
.third-party-login-container {
  width: 100%;
  max-width: 400px;
  margin: 0 auto;
  padding: 24px;
}

.third-party-options {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 16px;
  margin-bottom: 24px;
}

.no-providers {
  text-align: center;
  margin: 32px 0;
  padding: 24px;
  background: var(--ant-color-bg-layout);
  border-radius: 8px;
}

.login-option {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 64px;
  height: 64px;
  border: 1px solid var(--ant-color-border);
  border-radius: 8px;
  background: var(--ant-color-bg-container);
  cursor: pointer;
  transition: all 0.3s ease;
  
  &:hover {
    border-color: var(--ant-color-primary);
    box-shadow: 0 1px 3px var(--ant-color-primary-border);
    transform: translateY(-1px);
  }
  
  .option-icon {
    width: 64px;
    height: 64px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 32px;
    color: var(--ant-color-text);
    background: var(--ant-color-bg-container);
    border: 1px solid var(--ant-color-border);
    
    // GitHub品牌色 - 暗黑主题适配
    &.github {
      color: var(--ant-color-text);
    }
    
    // Google品牌色
    &.google {
      color: #4285f4;
    }
    
    // Facebook品牌色
    &.facebook {
      color: #1877f2;
    }
    
    // Twitter品牌色
    &.twitter {
      color: #1da1f2;
    }
    
    // QQ品牌色
    &.qq {
      color: #12b7f5;
    }
    
    // Microsoft品牌色
    &.microsoft {
      color: #00a4ef;
    }
    
    // Apple品牌色 - 暗黑主题适配
    &.apple {
      color: var(--ant-color-text);
    }
    
    // Amazon品牌色
    &.amazon {
      color: #ff9900;
    }
    
    // 钉钉品牌色
    &.dingtalk {
      color: #0089ff;
    }
    
    // LinkedIn品牌色
    &.linkedin {
      color: #0077b5;
    }
    
    // 微博品牌色
    &.weibo {
      color: #e6162d;
    }
  }
}

// 响应式设计
@media (max-width: 480px) {
  .third-party-options {
    grid-template-columns: 1fr;
  }
}
</style> 