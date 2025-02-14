<template>
  <div class="login-container">
    <a-card class="login-card" :bordered="false">
      <h2 class="login-title">{{ t('login.title') }}</h2>
      <a-form
        :model="loginForm"
        :rules="loginRules"
        ref="loginFormRef"
        @finish="handleLogin"
      >
        <a-form-item name="TenantId">
          <a-input-number
            v-model:value="loginForm.TenantId"
            :placeholder="t('login.tenantId')"
            class="login-input"
          >
            <template #prefix>
              <apartment-outlined />
            </template>
          </a-input-number>
        </a-form-item>
        <a-form-item name="UserName">
          <a-input
            v-model:value="loginForm.UserName"
            :placeholder="t('login.username')"
            class="login-input"
            autocomplete="username"
            @change="handleUserNameChange"
          >
            <template #prefix>
              <user-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="Password">
          <a-input-password
            v-model:value="loginForm.Password"
            :placeholder="t('login.password')"
            class="login-input"
            autocomplete="current-password"
          >
            <template #prefix>
              <lock-outlined />
            </template>
          </a-input-password>
        </a-form-item>
        <a-form-item>
          <a-button
            type="primary"
            html-type="submit"
            :loading="loading"
            :disabled="waitingSeconds > 0"
            class="login-button"
          >
            {{ waitingSeconds > 0 ? `${waitingSeconds}秒后重试` : t('login.submit') }}
          </a-button>
        </a-form-item>
      </a-form>
      <div class="login-options">
        <a-checkbox
          v-model:checked="rememberMe"
          class="remember-me"
        >
          {{ t('login.rememberMe') }}
        </a-checkbox>
        <a class="forgot-password" @click="handleForgotPassword">
          {{ t('login.forgotPassword') }}
        </a>
      </div>
      <div class="other-login">
        <div class="divider">
          <span>{{ t('login.otherLogin') }}</span>
        </div>
        <div class="oauth-buttons">
          <a-button
            type="link"
            @click="handleOAuthLogin('github')"
          >
            <template #icon>
              <GithubOutlined />
            </template>
            GitHub
          </a-button>
        </div>
      </div>
    </a-card>
    
    <!-- 验证码弹窗 -->
    <a-modal
      v-model:open="showCaptcha"
      :title="t('captcha.title')"
      :footer="null"
      :maskClosable="false"
      :closable="false"
      width="360px"
      centered
    >
      <div class="captcha-modal-content">
        <slider-captcha ref="captchaRef" @success="handleCaptchaSuccess" @error="handleCaptchaError" />
      </div>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, nextTick, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { message } from 'ant-design-vue'
import {
  UserOutlined,
  LockOutlined,
  SafetyOutlined,
  ApartmentOutlined,
  GithubOutlined
} from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { LoginParams, SaltResponse } from '@/types/auth'
import type { SliderValidateDto } from '@/api/captcha'
import { useUserStore } from '@/stores/user'
import SliderCaptcha from '@/components/base/SliderCaptcha.vue'
import { PasswordEncryptor } from '@/utils/crypto'
import { getSalt } from '@/api/auth'
import CryptoJS from 'crypto-js'
import { getCaptcha, verifyCaptcha } from '@/api/captcha'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()

// 表单引用
const loginFormRef = ref<FormInstance>()
const captchaRef = ref()

// 登录表单数据
const loginForm = reactive({
  TenantId: 0, // 默认为管理员租户ID
  UserName: '',
  Password: '',
  CaptchaToken: '',
  CaptchaOffset: 0
})

// 表单验证规则
const loginRules: Record<string, RuleObject[]> = {
  TenantId: [
    { required: true, type: 'number', message: t('login.tenantIdRequired'), trigger: 'blur' }
  ],
  UserName: [
    { required: true, type: 'string', message: t('login.usernameRequired'), trigger: 'blur' },
    { type: 'string', min: 3, max: 50, message: t('login.usernameLength'), trigger: 'blur' }
  ],
  Password: [
    { required: true, type: 'string', message: t('login.passwordRequired'), trigger: 'blur' },
    { type: 'string', min: 6, max: 100, message: t('login.passwordLength'), trigger: 'blur' }
  ]
}

// 是否显示验证码
const showCaptcha = ref(false)
// 记住我
const rememberMe = ref(false)
// 加载状态
const loading = ref(false)

// 保存上一次的登录参数，用于验证码验证成功后重试
const lastLoginParams = ref<LoginParams | null>(null)

// 倒计时相关
const waitingSeconds = ref(0)
const waitingTimer = ref<number | null>(null)
const failedAttempts = ref(0)
const remainingAttempts = ref(0)

// 登录策略常量
const LOGIN_POLICY = {
  ADMIN: {
    MAX_ATTEMPTS: 3,
    LOCKOUT_MINUTES: 30
  },
  USER: {
    MAX_ATTEMPTS: 5,
    LOCKOUT_MINUTES: 10
  },
  CAPTCHA_REQUIRED_ATTEMPTS: 1,
  CAPTCHA_REQUIRED_MINUTES: 5
}

// 开始倒计时
const startWaiting = (seconds: number) => {
  waitingSeconds.value = seconds
  if (waitingTimer.value) {
    clearInterval(waitingTimer.value)
  }
  waitingTimer.value = setInterval(() => {
    if (waitingSeconds.value > 0) {
      waitingSeconds.value--
    } else {
      if (waitingTimer.value) {
        clearInterval(waitingTimer.value)
        waitingTimer.value = null
      }
    }
  }, 1000)
}

// 监听用户名变化
const handleUserNameChange = (e: Event) => {
  const value = (e.target as HTMLInputElement).value;
  // 如果是admin用户，自动设置租户ID为0
  if (value.toLowerCase() === 'admin') {
    loginForm.TenantId = 0;
  }
}

// 处理登录
const handleLogin = async (values: any) => {
  // 如果正在等待中，不允许登录
  if (waitingSeconds.value > 0) {
    message.warning(`请等待 ${waitingSeconds.value} 秒后再试`)
    return
  }

  try {
    loading.value = true
    
    // 获取盐值
    const saltData = await getSalt(values.UserName) as unknown as SaltResponse
    if (!saltData || typeof saltData.salt !== 'string' || typeof saltData.iterations !== 'number') {
      throw new Error(t('login.saltError'))
    }

    // 构造登录参数
    const loginParams: LoginParams = {
      TenantId: Number(values.TenantId),
      UserName: values.UserName,
      Password: values.Password,  // 直接使用原始密码，让后端处理加密
      CaptchaToken: loginForm.CaptchaToken || '',
      CaptchaOffset: loginForm.CaptchaOffset || 0  // 提供默认值
    }

    // 保存登录参数，用于验证码验证成功后重试
    lastLoginParams.value = loginParams

    try {
      // 尝试登录
      await userStore.login(loginParams)
      // 登录成功，重置所有状态
      failedAttempts.value = 0
      remainingAttempts.value = LOGIN_POLICY.USER.MAX_ATTEMPTS
      showCaptcha.value = false
      loginForm.CaptchaToken = ''
      loginForm.CaptchaOffset = 0
      
      // 跳转到首页
      router.push({ path: '/' })
    } catch (error: any) {
      // 处理登录失败
      if (error.response) {
        const { status, data } = error.response
        
        if (status === 429) {
          // 需要等待
          const waitTime = data.remainingSeconds || 1800 // 默认30分钟
          startWaiting(waitTime)
          message.error(`登录失败，请等待 ${Math.ceil(waitTime / 60)} 分钟后重试`)
        } else if (status === 400 && data.needCaptcha) {
          // 需要验证码
          showCaptcha.value = true
          userStore.setNeedCaptcha(true)
          // 确保验证码组件已加载
          await nextTick()
          if (captchaRef.value) {
            captchaRef.value.refresh()
          }
          message.warning('请完成验证码验证后重试')
        } else {
          // 其他错误
          failedAttempts.value++
          remainingAttempts.value = values.UserName.toLowerCase() === 'admin' 
            ? LOGIN_POLICY.ADMIN.MAX_ATTEMPTS - failedAttempts.value
            : LOGIN_POLICY.USER.MAX_ATTEMPTS - failedAttempts.value
            
          message.error(`登录失败，剩余尝试次数: ${remainingAttempts.value}次`)
          
          if (remainingAttempts.value <= 0) {
            const lockoutMinutes = values.UserName.toLowerCase() === 'admin'
              ? LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES
              : LOGIN_POLICY.USER.LOCKOUT_MINUTES
            message.error(`连续失败次数过多，账号已被锁定${lockoutMinutes}分钟`)
          }
        }
      } else {
        message.error(error.message || '登录失败，请稍后重试')
      }
    }
  } catch (error: any) {
    message.error(error.message || '登录失败，请稍后重试')
  } finally {
    loading.value = false
  }
}

// 处理验证码成功
const handleCaptchaSuccess = async (result: SliderValidateDto) => {
  loginForm.CaptchaToken = result.token
  loginForm.CaptchaOffset = result.xOffset
  showCaptcha.value = false
  
  // 如果有上一次的登录参数，自动重试登录
  if (lastLoginParams.value) {
    const params = {
      ...lastLoginParams.value,
      CaptchaToken: result.token,
      CaptchaOffset: result.xOffset
    }
    await handleLogin(params)
  }
}

// 处理验证码错误
const handleCaptchaError = () => {
  message.error('验证码验证失败，请重试')
  if (captchaRef.value) {
    captchaRef.value.refresh()
  }
}

// 处理忘记密码
const handleForgotPassword = () => {
  message.info('忘记密码功能暂未开放')
}

// 处理OAuth登录
const handleOAuthLogin = (provider: string) => {
  message.info(`${provider}登录功能暂未开放`)
}

// 添加测试方法
const runEncryptionTest = async () => {
  try {
    loading.value = true
    await new Promise(resolve => setTimeout(resolve, 0)) // 让UI有机会更新
    window.testPasswordEncryption()
  } finally {
    loading.value = false
  }
}

// 组件挂载时
onMounted(() => {
  // 清理所有登录状态
  failedAttempts.value = 0
  remainingAttempts.value = 0
  waitingSeconds.value = 0
  if (waitingTimer.value) {
    clearInterval(waitingTimer.value)
    waitingTimer.value = null
  }
  showCaptcha.value = false
  loginForm.CaptchaToken = ''
  loginForm.CaptchaOffset = 0
  userStore.setNeedCaptcha(false)

  // 如果之前记住了用户名，自动填充
  const lastUsername = localStorage.getItem('lastUsername')
  if (lastUsername) {
    loginForm.UserName = lastUsername
    rememberMe.value = true
    // 如果是admin用户，自动设置租户ID为0
    if (lastUsername.toLowerCase() === 'admin') {
      loginForm.TenantId = 0
    }
  }
})

// 组件卸载时清理定时器和状态
onUnmounted(() => {
  if (waitingTimer.value) {
    clearInterval(waitingTimer.value)
    waitingTimer.value = null
  }
  failedAttempts.value = 0
  remainingAttempts.value = 0
})
</script>

<style lang="less" scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  padding: 24px;
  background-color: var(--ant-color-bg-layout);
}

.login-card {
  width: 100%;
  max-width: 400px;

  :deep(.ant-card) {
    background-color: var(--ant-color-bg-container);
    box-shadow: 0 6px 16px 0 rgba(0, 0, 0, 0.08), 0 3px 6px -4px rgba(0, 0, 0, 0.12), 0 9px 28px 8px rgba(0, 0, 0, 0.05);
    border-radius: 8px;
    border: none;
  }

  :deep(.ant-card-body) {
    padding: 32px;
  }
}

.login-title {
  text-align: center;
  margin-bottom: 32px;
  font-size: 28px;
  font-weight: 600;
  color: var(--ant-color-text);
}

.login-input {
  width: 100%;

  :deep(.ant-input-number),
  :deep(.ant-input),
  :deep(.ant-input-password) {
    background-color: var(--ant-color-bg-container);
    border-color: var(--ant-color-border);
    color: var(--ant-color-text);

    input {
      background-color: transparent;
      color: var(--ant-color-text);
    }

    .ant-input-password-icon {
      color: var(--ant-color-text-quaternary);
    }
  }
}

.login-button {
  width: 100%;
  height: 40px;
  font-size: 16px;
  margin-top: 8px;
}

.login-options {
  margin: 24px 0;
  display: flex;
  justify-content: space-between;
  align-items: center;

  .remember-me {
    color: var(--ant-color-text);
  }

  .forgot-password {
    color: var(--ant-color-primary);
  }
}

.other-login {
  .divider {
    position: relative;
    text-align: center;
    margin: 24px 0;
    
    &::before,
    &::after {
      content: '';
      position: absolute;
      top: 50%;
      width: 45%;
      height: 1px;
      background-color: var(--ant-color-split);
    }
    
    &::before {
      left: 0;
    }
    
    &::after {
      right: 0;
    }
    
    span {
      display: inline-block;
      padding: 0 12px;
      color: var(--ant-color-text-quaternary);
      background-color: var(--ant-color-bg-container);
    }
  }

  .oauth-buttons {
    display: flex;
    justify-content: center;
    gap: 16px;

    :deep(.ant-btn) {
      color: var(--ant-color-text);
    }

    :deep(.anticon) {
      font-size: 18px;
    }
  }
}

.captcha-container {
  display: flex;
  gap: 8px;
  
  .captcha-input {
    flex: 1;
  }
}

.captcha-modal-content {
  padding: 16px 0;
}

:deep(.ant-modal-body) {
  padding: 24px;
}
</style> 