<template>
  <div class="sms-login-container">
    <a-form
      :model="loginForm"
      :rules="loginRules"
      ref="loginFormRef"
      @finish="handleLogin"
      layout="vertical"
    >
      <!-- 手机号输入 -->
      <a-form-item name="phone" :label="t('identity.auth.smsLogin.phone')">
        <a-input
          v-model:value="loginForm.phone"
          :placeholder="t('identity.auth.smsLogin.phonePlaceholder')"
          size="large"
          :maxlength="11"
        >
          <template #prefix>
            <mobile-outlined />
          </template>
        </a-input>
      </a-form-item>

      <!-- 验证码输入 -->
      <a-form-item name="verificationCode" :label="t('identity.auth.smsLogin.verificationCode')">
        <div class="verification-code-input">
          <a-input
            v-model:value="loginForm.verificationCode"
            :placeholder="t('identity.auth.smsLogin.verificationCodePlaceholder')"
            size="large"
            :maxlength="6"
          >
            <template #prefix>
              <key-outlined />
            </template>
          </a-input>
          <a-button
            @click="sendVerificationCode"
            :loading="sendCodeLoading"
            :disabled="!phoneValid || countdown > 0"
            size="large"
            class="send-code-btn"
          >
            {{ countdown > 0 ? `${countdown}s` : t('identity.auth.smsLogin.sendCode') }}
          </a-button>
        </div>
      </a-form-item>

      <!-- 登录按钮 -->
      <a-form-item>
        <a-button
          type="primary"
          html-type="submit"
          size="large"
          :loading="loading"
          :disabled="!formValid || loading"
          block
        >
          {{ t('identity.auth.smsLogin.login') }}
        </a-button>
      </a-form-item>
    </a-form>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed, onMounted, onUnmounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import { 
  MobileOutlined, 
  KeyOutlined 
} from '@ant-design/icons-vue'
import { sendSmsCode, smsLogin } from '@/api/identity/auth/smsAuth'

const { t } = useI18n()
const emit = defineEmits(['loginSuccess'])

// 表单引用
const loginFormRef = ref<FormInstance>()

// 登录表单
const loginForm = reactive({
  phone: '',
  verificationCode: ''
})

// 状态
const loading = ref(false)
const sendCodeLoading = ref(false)
const countdown = ref(0)
const countdownTimer = ref<NodeJS.Timeout | null>(null)

// 表单验证规则
const loginRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    phone: [
      { required: true, message: t('identity.auth.smsLogin.form.phoneRequired'), trigger: 'blur' },
      { pattern: /^1[3-9]\d{9}$/, message: t('identity.auth.smsLogin.form.phoneFormat'), trigger: 'blur' }
    ],
    verificationCode: [
      { required: true, message: t('identity.auth.smsLogin.form.verificationCodeRequired'), trigger: 'blur' },
      { len: 6, message: t('identity.auth.smsLogin.form.verificationCodeLength'), trigger: 'blur' },
      { pattern: /^\d{6}$/, message: t('identity.auth.smsLogin.form.verificationCodeFormat'), trigger: 'blur' }
    ]
  }
  return rules
})

// 手机号是否有效
const phoneValid = computed(() => {
  return /^1[3-9]\d{9}$/.test(loginForm.phone)
})

// 表单是否有效
const formValid = computed(() => {
  return phoneValid.value && loginForm.verificationCode.length === 6
})

// 发送验证码
const sendVerificationCode = async () => {
  try {
    sendCodeLoading.value = true
    
    const { data } = await sendSmsCode({
      phone: loginForm.phone
    })
    
    if (data.data.success) {
      message.success(t('identity.auth.smsLogin.codeSent'))
      startCountdown()
    } else {
      message.error(data.data.message || t('identity.auth.smsLogin.sendCodeFailed'))
    }
  } catch (error: any) {
    console.error('[短信登录] 发送验证码失败:', error)
    message.error(error.message || t('identity.auth.smsLogin.sendCodeFailed'))
  } finally {
    sendCodeLoading.value = false
  }
}

// 开始倒计时
const startCountdown = () => {
  countdown.value = 60
  
  countdownTimer.value = setInterval(() => {
    countdown.value--
    if (countdown.value <= 0) {
      stopCountdown()
    }
  }, 1000)
}

// 停止倒计时
const stopCountdown = () => {
  if (countdownTimer.value) {
    clearInterval(countdownTimer.value)
    countdownTimer.value = null
  }
  countdown.value = 0
}

// 处理登录
const handleLogin = async () => {
  try {
    loading.value = true
    
    // 验证表单
    await loginFormRef.value?.validate()
    
    // 调用登录API
    const { data } = await smsLogin({
      phone: loginForm.phone,
      verificationCode: loginForm.verificationCode
    })
    
    if (data.data.success) {
      message.success(t('identity.auth.smsLogin.loginSuccess'))
      emit('loginSuccess', data.data.userInfo)
    } else {
      message.error(data.data.message || t('identity.auth.smsLogin.loginFailed'))
    }
  } catch (error: any) {
    console.error('[短信登录] 登录失败:', error)
    message.error(error.message || t('identity.auth.smsLogin.loginFailed'))
  } finally {
    loading.value = false
  }
}

// 重置所有状态
const resetAllStates = () => {
  console.log('[短信登录] 开始重置所有状态')
  
  // 重置表单数据
  Object.assign(loginForm, { phone: '', verificationCode: '' })
  
  // 重置状态
  loading.value = false
  sendCodeLoading.value = false
  
  // 停止倒计时
  stopCountdown()
  
  // 重置表单验证状态
  loginFormRef.value?.resetFields()
  
  console.log('[短信登录] 状态重置完成')
}

// 暴露重置方法给父组件
defineExpose({
  resetAllStates
})

// 组件挂载时初始化
onMounted(() => {
  console.log('[短信登录] 开始初始化')
  resetAllStates()
})

// 组件卸载时清理
onUnmounted(() => {
  stopCountdown()
})
</script>

<style lang="less" scoped>
.sms-login-container {
  width: 100%;
  max-width: 400px;
  margin: 0 auto;
  padding: 24px;
}

.verification-code-input {
  display: flex;
  gap: 8px;
  
  .ant-input {
    flex: 1;
  }
  
  .send-code-btn {
    width: 120px;
    white-space: nowrap;
  }
}

:deep(.ant-form-item-label) {
  font-weight: 500;
}

:deep(.ant-input-prefix) {
  color: var(--ant-color-text-secondary);
}
</style> 