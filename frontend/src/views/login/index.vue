<template>
  <div class="login-container">
    <!-- 添加登录页头部导航栏 -->
    <header-login-bar />
    
    <div class="login-content">
      <!-- 左侧品牌展示卡片 -->
      <a-card class="brand-card" :bordered="false">
        <div class="brand-content">
          <img src="@/assets/images/logo.svg" alt="Logo" class="brand-logo" />
          <h1 class="brand-title">Lean.Hbt</h1>
          <p class="brand-slogan">{{ t('common.system.slogan') }}</p>
        </div>
      </a-card>

      <!-- 右侧登录卡片 -->
      <a-card class="login-card" :bordered="false">
        <h2 class="login-title">{{ t('identity.auth.login.title') }}</h2>
        <a-form
          :model="loginForm"
          :rules="loginRules"
          ref="loginFormRef"
          @finish="handleLogin"
        >
          <a-form-item name="tenantId">
            <a-input-number
              v-model:value="loginForm.tenantId"
              :placeholder="t('identity.auth.login.tenantId')"
              class="login-input"
            >
              <template #prefix>
                <apartment-outlined />
              </template>
            </a-input-number>
          </a-form-item>
          <a-form-item name="userName">
            <a-input
              v-model:value="loginForm.userName"
              :placeholder="t('identity.auth.login.username')"
              class="login-input"
              autocomplete="username"
              @change="handleUserNameChange"
            >
              <template #prefix>
                <user-outlined />
              </template>
            </a-input>
          </a-form-item>
          <a-form-item name="password">
            <a-input-password
              v-model:value="loginForm.password"
              :placeholder="t('identity.auth.login.password')"
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
              class="login-button"
              :loading="loading"
              block
            >
              {{ t('identity.auth.login.submit') }}
            </a-button>
          </a-form-item>
        </a-form>
        <div class="login-options">
          <a-checkbox
            v-model:checked="rememberMe"
            class="remember-me"
          >
            {{ t('identity.auth.login.rememberMe') }}
          </a-checkbox>
          <a class="forgot-password" @click="handleForgotPassword">
            {{ t('identity.auth.login.forgotPassword') }}
          </a>
        </div>
        <div class="other-login">
          <div class="divider">
            <span>{{ t('identity.auth.login.otherLogin') }}</span>
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
    </div>
    
    <!-- 验证码弹窗 -->
    <a-modal
      v-model:open="showCaptcha"
      :title="t('identity.auth.captcha.title')"
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
// 类型导入
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { LoginParams, SaltResponse } from '@/types/identity/auth'
import type { SliderValidateDto } from '@/api/security/captcha'
import type { DeviceInfo } from '@/types/identity/deviceExtend'
import { DEVICE_INFO_LENGTH, HbtDeviceType, HbtOsType, HbtBrowserType } from '@/types/identity/deviceExtend'

// API和组件导入
import { getSalt } from '@/api/identity/auth'
import { getCaptcha, verifyCaptcha } from '@/api/security/captcha'
import SliderCaptcha from '@/components/Base/SliderCaptcha.vue'
import HeaderLoginBar from '@/components/Navigation/HeaderLoginBar.vue'
import { PasswordEncryptor } from '@/utils/crypto'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, reactive, onMounted, onUnmounted, nextTick, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const { t } = useI18n()
const router = useRouter()
const route = useRoute()
const userStore = useUserStore()
const menuStore = useMenuStore()

// 表单引用
const loginFormRef = ref<FormInstance>()
const captchaRef = ref()

// 登录表单数据
const loginForm = reactive<LoginParams>({
  tenantId: 0, // 默认为管理员租户ID
  userName: '',
  password: '',
  captchaToken: '',
  captchaOffset: 0,
  loginSource: 0, // Web端
  deviceInfo: {
    deviceId: crypto.randomUUID(),
    deviceType: 0, // PC
    deviceName: navigator.platform || 'Unknown',
    deviceModel: navigator.userAgent || 'Unknown',
    osType: 0, // Windows
    osVersion: navigator.platform || 'Unknown',
    browserType: 0, // Chrome
    browserVersion: navigator.appVersion || 'Unknown',
    resolution: `${window.screen.width}x${window.screen.height}`,
    ipAddress: '',
    location: ''
  }
})

// 表单验证规则
const loginRules: Record<string, RuleObject[]> = {
  tenantId: [
    { required: true, type: 'number', message: t('identity.auth.login.form.tenantIdRequired'), trigger: 'blur' }
  ],
  userName: [
    { required: true, type: 'string', message: t('identity.auth.login.form.usernameRequired'), trigger: 'blur' },
    { type: 'string', min: 3, max: 50, message: t('identity.auth.login.form.usernameLength'), trigger: 'blur' }
  ],
  password: [
    { required: true, type: 'string', message: t('identity.auth.login.form.passwordRequired'), trigger: 'blur' },
    { type: 'string', min: 6, max: 100, message: t('identity.auth.login.form.passwordLength'), trigger: 'blur' }
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
const waitingTimer = ref<NodeJS.Timeout | null>(null)
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
    loginForm.tenantId = 0;
  }
}

// 处理登录
const handleLogin = async () => {
  // 如果正在等待中，不允许登录
  if (waitingSeconds.value > 0) {
    message.warning(t('identity.auth.login.error.waitingRetry', { seconds: waitingSeconds.value }))
    return
  }

  try {
    loading.value = true
    
    // 获取盐值
    try {
      const response = await getSalt(loginForm.userName)
      if (!response || !response.data) {
        throw new Error(t('identity.auth.login.error.saltError'))
      }
      
      const saltData = response.data
      if (!saltData || typeof saltData.salt !== 'string' || typeof saltData.iterations !== 'number') {
        throw new Error(t('identity.auth.login.error.saltError'))
      }

      console.log('[密码加密] 开始加密密码')
      
      // 使用PBKDF2加密原始密码
      const hashedPassword = PasswordEncryptor.hashPassword(
        loginForm.password,
        saltData.salt,
        saltData.iterations
      )
      
      console.log('[登录参数] 开始构造登录参数:', {
        userName: loginForm.userName,
        tenantId: loginForm.tenantId,
        hasPasswordLength: hashedPassword.length,
        deviceInfo: loginForm.deviceInfo
      })

      // 构造登录参数
      const loginParams: LoginParams = {
        ...loginForm,
        password: hashedPassword,
        deviceInfo: {
          deviceId: crypto.randomUUID().slice(0, DEVICE_INFO_LENGTH.DEVICE_ID),
          deviceType: 0, // PC
          deviceName: (navigator.platform || 'Unknown').slice(0, DEVICE_INFO_LENGTH.DEVICE_NAME),
          deviceModel: (navigator.userAgent || 'Unknown').slice(0, DEVICE_INFO_LENGTH.DEVICE_MODEL),
          osType: 0, // Windows
          osVersion: (navigator.platform || 'Unknown').slice(0, DEVICE_INFO_LENGTH.OS_VERSION),
          browserType: 3, // Edge
          browserVersion: (navigator.appVersion || 'Unknown').slice(0, DEVICE_INFO_LENGTH.BROWSER_VERSION),
          resolution: `${window.screen.width}x${window.screen.height}`.slice(0, DEVICE_INFO_LENGTH.RESOLUTION),
          ipAddress: '',
          location: ''
        },
        loginSource: 0
      }

      // 保存登录参数，用于验证码验证成功后重试
      lastLoginParams.value = loginParams

      // 如果需要验证码
      if (failedAttempts.value >= LOGIN_POLICY.CAPTCHA_REQUIRED_ATTEMPTS) {
        showCaptcha.value = true
        message.warning(t('identity.auth.captcha.required'))
        return
      }

      // 执行登录
      console.log('[登录请求] 开始调用登录接口')
      const result = await userStore.login(loginParams)
      console.log('[登录请求] 登录接口返回:', result)
      if (result) {
        handleLoginSuccess()
      }
    } catch (error: any) {
      handleLoginError(error)
    }
  } finally {
    loading.value = false
  }
}

// 处理登录成功
const handleLoginSuccess = async () => {
  try {
    // 登录成功，重置所有状态
    failedAttempts.value = 0
    remainingAttempts.value = LOGIN_POLICY.USER.MAX_ATTEMPTS
    showCaptcha.value = false
    loginForm.captchaToken = ''
    loginForm.captchaOffset = 0
    
    // 如果记住我选项被勾选，保存用户名
    if (rememberMe.value) {
      localStorage.setItem('lastUsername', loginForm.userName)
    } else {
      localStorage.removeItem('lastUsername')
    }

    // 获取用户信息（这里会自动加载菜单）
    await userStore.getUserInfo()

    message.success(t('identity.auth.login.success'))
    
    // 等待一下确保路由已经准备好
    await nextTick()
    
    // 登录成功后跳转到工作台
    await router.push('/dashboard/workplace')
  } catch (error) {
    console.error('登录后处理失败:', error)
    message.error(t('identity.auth.login.failed'))
  }
}

// 处理登录错误
const handleLoginError = (error: any) => {
  if (error.response) {
    const { status, data } = error.response
    
    if (status === 429) {
      // 需要等待
      const waitTime = data.remainingSeconds || 1800 // 默认30分钟
      startWaiting(waitTime)
      const minutes = Math.ceil(waitTime / 60)
      message.error(t('identity.auth.login.error.accountLocked', { minutes }))
    } else {
      // 其他错误
      failedAttempts.value++
      remainingAttempts.value = loginForm.userName.toLowerCase() === 'admin' 
        ? LOGIN_POLICY.ADMIN.MAX_ATTEMPTS - failedAttempts.value
        : LOGIN_POLICY.USER.MAX_ATTEMPTS - failedAttempts.value
      
      message.error(t('identity.auth.login.error.remainingAttempts', { count: remainingAttempts.value }))
      
      // 如果失败次数达到阈值，显示验证码
      if (failedAttempts.value >= LOGIN_POLICY.CAPTCHA_REQUIRED_ATTEMPTS) {
        showCaptcha.value = true
        // 确保验证码组件已加载
        nextTick(() => {
          if (captchaRef.value) {
            captchaRef.value.initCaptcha()
          }
        })
        message.warning(t('identity.auth.captcha.required'))
      }
      
      if (remainingAttempts.value <= 0) {
        const lockoutMinutes = loginForm.userName.toLowerCase() === 'admin'
          ? LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES
          : LOGIN_POLICY.USER.LOCKOUT_MINUTES
        message.error(t('identity.auth.login.error.accountLocked', { minutes: lockoutMinutes }))
      }
    }
  } else {
    message.error(error.message || t('identity.auth.login.error.serverError'))
  }
}

// 处理验证码成功
const handleCaptchaSuccess = async (result: SliderValidateDto) => {
  if (lastLoginParams.value) {
    // 更新登录参数
    const updatedParams: LoginParams = {
      ...lastLoginParams.value,
      captchaToken: result.token,
      captchaOffset: result.xOffset,
      deviceInfo: {
        deviceId: crypto.randomUUID().slice(0, DEVICE_INFO_LENGTH.DEVICE_ID),
        deviceType: 0, // PC
        deviceName: (navigator.platform || 'Unknown').slice(0, DEVICE_INFO_LENGTH.DEVICE_NAME),
        deviceModel: (navigator.userAgent || 'Unknown').slice(0, DEVICE_INFO_LENGTH.DEVICE_MODEL),
        osType: 0, // Windows
        osVersion: (navigator.platform || 'Unknown').slice(0, DEVICE_INFO_LENGTH.OS_VERSION),
        browserType: 3, // Edge
        browserVersion: (navigator.appVersion || 'Unknown').slice(0, DEVICE_INFO_LENGTH.BROWSER_VERSION),
        resolution: `${window.screen.width}x${window.screen.height}`.slice(0, DEVICE_INFO_LENGTH.RESOLUTION),
        ipAddress: '',
        location: ''
      },
      loginSource: 0
    }
    
    // 更新表单数据
    loginForm.captchaToken = result.token
    loginForm.captchaOffset = result.xOffset
    showCaptcha.value = false

    // 执行登录
    const loginResult = await userStore.login(updatedParams)
    if (loginResult) {
      handleLoginSuccess()
    }
  }
}

// 处理验证码错误
const handleCaptchaError = (errorMsg: string) => {
  if (errorMsg.includes('请等待')) {
    const match = errorMsg.match(/(\d+)秒/)
    if (match) {
      const seconds = parseInt(match[1])
      startWaiting(seconds)
      // 关闭验证码对话框
      showCaptcha.value = false
      message.warning(t('identity.auth.captcha.waitingRetry', { seconds }))
      // 等待时间到后再显示验证码
      setTimeout(() => {
        if (captchaRef.value) {
          captchaRef.value.refresh()
        }
        showCaptcha.value = true
      }, seconds * 1000)
    }
  } else {
    message.error(t('identity.auth.captcha.verifyFailed'))
    // 关闭验证码对话框
    showCaptcha.value = false
  }
}

// 处理忘记密码
const handleForgotPassword = () => {
  message.info(t('identity.auth.login.notAvailable', { feature: t('identity.auth.login.form.forgot') }))
}

// 处理OAuth登录
const handleOAuthLogin = (provider: string) => {
  message.info(t('identity.auth.login.notAvailable', { feature: `${provider}${t('identity.auth.login.form.submit')}` }))
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
  loginForm.captchaToken = ''
  loginForm.captchaOffset = 0
  userStore.setNeedCaptcha(false)

  // 如果之前记住了用户名，自动填充
  const lastUsername = localStorage.getItem('lastUsername')
  if (lastUsername) {
    loginForm.userName = lastUsername
    rememberMe.value = true
    // 如果是admin用户，自动设置租户ID为0
    if (lastUsername.toLowerCase() === 'admin') {
      loginForm.tenantId = 0
    }
  }
})

// 监听验证码弹窗显示状态
watch(showCaptcha, (newValue) => {
  if (newValue && captchaRef.value) {
    nextTick(() => {
      captchaRef.value.initCaptcha()
    })
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
  background: var(--ant-color-bg-layout);
  position: relative;

  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: color-mix(in srgb, var(--ant-color-primary) 4%, transparent);
  }

  :global(.dark) & {
    background: color-mix(in srgb, var(--ant-color-bg-layout) 90%, black);
  }
}

.login-content {
  display: flex;
  align-items: stretch;
  max-width: 900px;
  width: 100%;
  position: relative;
  z-index: 1;
  background: var(--ant-color-bg-container);
  border-radius: var(--ant-border-radius-lg);
  box-shadow: 0 6px 16px -8px rgba(0, 0, 0, 0.08),
              0 9px 28px 0 rgba(0, 0, 0, 0.05),
              0 12px 48px 16px rgba(0, 0, 0, 0.03);

  :global(.dark) & {
    background:  #e6e3e3;
    border: 2px solid rgba(255, 255, 255, 0.8);
    box-shadow: 0 0 30px rgba(255, 255, 255, 0.3),    /* 近层：30px 柔和白光 */
                0 0 80px rgba(255, 255, 255, 0.2),    /* 中层：80px 扩散白光 */
                0 0 150px rgba(255, 255, 255, 0.1);   /* 远层：150px 环境白光 */
  }
}

.brand-card {
  flex: 1;
  margin: 0;
  border-right: 1px solid var(--ant-color-split);
  display: flex;
  
  :deep(.ant-card) {
    background: transparent;
    border: none;
    height: 100%;
    width: 100%;
    margin: 0;
    padding: 0;
  }

  :deep(.ant-card-body) {
    padding: 48px;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
  }
}

.brand-content {
  text-align: center;
}

.brand-logo {
  width: 120px;
  height: auto;
  margin-bottom: 24px;
}

.brand-title {
  font-size: 36px;
  font-weight: 600;
  color: var(--ant-color-text);
  margin-bottom: 16px;
}

.brand-slogan {
  font-size: 16px;
  color: var(--ant-color-text-secondary);
  line-height: 1.6;
}

.divider-line {
  display: none;
}

.login-card {
  flex: 1;
  margin: 0;
  display: flex;

  :deep(.ant-card) {
    background: transparent;
    border: none;
    width: 100%;
    margin: 0;
    padding: 0;
    box-shadow: none;
  }

  :deep(.ant-card-body) {
    padding: 32px 48px;
    width: 100%;
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

    &:hover {
      color: var(--ant-color-primary-hover);
    }
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
      background: var(--ant-color-border-split);
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
      color: var(--ant-color-text-secondary);
      background: var(--ant-color-bg-container);
    }
  }

  .oauth-buttons {
    display: flex;
    justify-content: center;
    gap: 16px;

    :deep(.ant-btn) {
      color: var(--ant-color-text);

      &:hover {
        color: var(--ant-color-primary-hover);
      }
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
  background: var(--ant-color-bg-container);
}
</style> 