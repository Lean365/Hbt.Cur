<template>
  <div class="login-container">
    <hbt-header-login />

    <div class="login-content">
      <!-- 左侧品牌展示卡片 -->
      <a-card class="brand-card" :bordered="false">
        <div class="brand-content">
          <img :src="defaultConfig.logo.src" :alt="defaultConfig.logo.alt" class="brand-logo" />
          <h1 class="brand-title">{{ defaultConfig.title }}</h1>
          <p class="brand-slogan">{{ t('common.system.slogan') }}</p>
        </div>
      </a-card>

      <!-- 右侧登录卡片 -->
      <a-card class="login-card" :bordered="false">
        <h2 class="login-title">{{ t('identity.auth.login.title') }}</h2>
        

        
        <a-tabs 
          v-model:activeKey="activeTabKey" 
          class="login-tabs"
          style="width: 100%; margin-bottom: 20px;"
          @change="handleTabChange"
        >
          <a-tab-pane 
            v-for="tab in loginTabs" 
            :key="tab.key" 
            :tab="tab.label"
          >
            <!-- Tab内容会在下面的条件渲染中处理 -->
          </a-tab-pane>
        </a-tabs>
        

        
        <!-- 密码登录表单 -->
        <div v-if="activeTabKey === 'password'" class="login-form-container">
          <a-form
            :model="loginForm"
            :rules="loginRules"
            ref="loginFormRef"
            @finish="handleLogin"
          >
            <a-form-item name="userName">
              <a-input
                v-model:value="loginForm.userName"
                :placeholder="t('identity.auth.login.username')"
                class="login-input"
                autocomplete="username"
                @change="handleUserNameChange"
              >
                <template #prefix>
                  <user-outlined v-icon-random="'login-user'" />
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
                  <lock-outlined v-icon-random="'login-password'" />
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
            <a class="forgot-password" @click="showPasswordRecovery">
              {{ t('identity.auth.login.forgotPassword') }}
            </a>
          </div>
        </div>

        <!-- 短信登录组件 -->
        <div v-if="activeTabKey === 'sms' || activeTabKey === 'smsAuth' || activeTabKey === 'SmsAuth'" class="login-form-container">
          <SmsLogin />
        </div>

        <!-- 二维码登录组件 -->
        <div v-if="activeTabKey === 'qrcode' || activeTabKey === 'qrCode' || activeTabKey === 'qrCodeAuth' || activeTabKey === 'QrCodeAuth'" class="login-form-container">
          <QrCodeLogin :options="getQrCodeOptions()" />
        </div>

        <!-- 第三方登录组件 -->
        <div v-if="activeTabKey === 'oauth' || activeTabKey === 'thirdParty' || activeTabKey === 'OAuth' || activeTabKey === 'oauthAuth'" class="login-form-container">
          <OAuthLogin :providers="getOAuthProviders()" />
        </div>
        
        <!-- 注册链接（根据配置显示/隐藏） -->
        <div class="register-link" v-if="configStore.showRegister">
          <span>{{ t('identity.auth.login.noAccount') }}</span>
          <a @click="handleShowRegisterModal">
            {{ t('identity.auth.login.registerNow') }}
          </a>
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
        <component
          :is="captchaType === 'Behavior' ? 'HbtBehaviorCaptcha' : 'HbtSliderCaptcha'"
          ref="captchaRef"
          @success="handleCaptchaSuccess"
          @error="handleCaptchaError"
        />
      </div>
    </a-modal>

    <!-- 找回密码弹窗 -->
    <hbt-modal
      :open="showPasswordRecoveryModal"
      :title="t('identity.auth.passwordRecovery.title')"
      :footer="false"
      :mask-closable="false"
      :width="600"
      :centered="true"
      @update:open="handlePasswordRecoveryCancel"
      @cancel="handlePasswordRecoveryCancel"
    >
      <UserRecovery
        ref="passwordRecoveryRef"
        @switch-to-login="handleSwitchToLogin"
        @recovery-success="handleRecoverySuccess"
      />
    </hbt-modal>

    <!-- 注册弹窗 -->
    <hbt-modal
      :open="showRegisterModal"
      :title="t('identity.auth.register.title')"
      :footer="false"
      :mask-closable="false"
      :width="600"
      :centered="true"
      @update:open="handleRegisterModalCancel"
      @cancel="handleRegisterModalCancel"
    >
      <UserRegistration
        ref="userRegistrationRef"
        @switch-to-login="handleRegisterModalCancel"
        @registration-success="handleRegisterSuccess"
      />
    </hbt-modal>
  </div>
</template>

<script lang="ts" setup>
// 类型导入
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { LoginParams } from '@/types/identity/auth'

// API和组件导入
import { getSalt, checkAccountLockout } from '@/api/identity/auth/auth'
import { getCaptchaConfig } from '@/api/security/captcha'
import { getEnabledLoginMethods } from '@/api/identity/auth/loginMethod'
import { PasswordEncryptor } from '@/utils/crypto'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'
import { useAppStore } from '@/stores/app'
import { useTranslationStore } from '@/stores/translation'
import { useConfigStore } from '@/stores/config'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref,  onMounted, onUnmounted, nextTick, watch, computed, h } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { getDeviceInfo } from '@/utils/device'
import { getEnvironmentInfo } from '@/utils/environment'
import { getClientIpAddress } from '@/utils/ip'
import { LOGIN_POLICY, LOGIN_STORAGE_KEYS, SPECIAL_USERS } from '@/types/identity/auth'
import { getDefaultAppConfig } from '@/setting'
import UserRecovery from '@/views/login/components/UserRecovery.vue'
import UserRegistration from '@/views/login/components/UserRegistration.vue'
import SmsLogin from '@/views/login/components/SmsLogin.vue'
import QrCodeLogin from '@/views/login/components/QrCodeLogin.vue'
import OAuthLogin from '@/views/login/components/OAuthLogin.vue'


const { t } = useI18n()
const router = useRouter()
const route = useRoute()
const userStore = useUserStore()
const menuStore = useMenuStore()
const appStore = useAppStore()
const translationStore = useTranslationStore()
const configStore = useConfigStore()

// 获取 setting.ts 的默认配置
const defaultConfig = computed(() => getDefaultAppConfig(false))

// 表单引用
const loginFormRef = ref<FormInstance>()
const captchaRef = ref()

// 登录表单数据
const loginForm = ref({
  userName: 'admin',
  password: '123456',
  captchaToken: '',
  captchaOffset: 0,
  loginSource: 0,
  deviceInfo: null as any,
  environmentInfo: null as any
})

// 表单验证规则
const loginRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    userName: [
      { required: true, type: 'string', message: t('identity.auth.login.form.usernameRequired'), trigger: 'blur' },
      { type: 'string', min: 3, max: 50, message: t('identity.auth.login.form.usernameLength'), trigger: 'blur' }
    ],
    password: [
      { required: true, type: 'string', message: t('identity.auth.login.form.passwordRequired'), trigger: 'blur' },
      { type: 'string', min: 6, max: 100, message: t('identity.auth.login.form.passwordLength'), trigger: 'blur' }
    ]
  }
  return rules
})

// 是否显示验证码
const showCaptcha = ref(false)
// 记住我
const rememberMe = ref(false)
// 加载状态
const loading = ref(false)
// 是否显示找回密码弹窗
const showPasswordRecoveryModal = ref(false)
// 是否显示注册弹窗
const showRegisterModal = ref(false)

// 验证码状态
const captchaVerified = ref(false)
const captchaParams = ref<{ token: string; xOffset: number } | null>(null)

// 组件引用
const passwordRecoveryRef = ref()
const userRegistrationRef = ref()

// 登录方式相关变量
const activeTabKey = ref('password')
const loginTabs = ref<any[]>([])
const enabledLoginMethods = ref<any[]>([])

// 验证码类型（完全由后端配置）
const captchaType = ref<'Slider' | 'Behavior'>('Slider')

// 获取后端验证码配置
const loadCaptchaConfig = async () => {
  try {
    const { data } = await getCaptchaConfig('login')
    
    // 检查数据结构：data.data 是实际的配置数据
    const configData = data.data || data
    if (configData && configData.type) {
      captchaType.value = configData.type as 'Slider' | 'Behavior'
    } else {
      message.error('获取验证码配置失败')
    }
  } catch (error) {
    console.error('[验证码配置] 获取后端验证码配置失败:', error)
    message.error('获取验证码配置失败')
  }
}

// 加载登录方式配置
const loadLoginMethods = async () => {
  try {
    const response: any = await getEnabledLoginMethods()
    
    let loginMethods: any[] = []

    if (Array.isArray(response?.data)) {
      loginMethods = response.data
    } else if (Array.isArray(response?.data?.data)) {
      loginMethods = response.data.data
    } else if (Array.isArray(response)) {
      loginMethods = response
    } else {
      loginMethods = []
    }

    enabledLoginMethods.value = loginMethods

    // 构建tabs配置
    const tabs = []
    for (const method of enabledLoginMethods.value) {
      if (method && method.key && method.name) {
        tabs.push({
          key: method.key,
          label: method.name,
          children: null
        })
      }
    }
    loginTabs.value = tabs
  } catch (error) {
    enabledLoginMethods.value = []
    loginTabs.value = []
    console.error('[登录方式] 获取后端登录方式配置失败:', error)
    message.error('获取登录方式配置失败')
  }
}

// 处理tab切换
const handleTabChange = (key: string | number) => {
  activeTabKey.value = key as string
}

// 获取OAuth提供商列表
const getOAuthProviders = () => {
  const oauthMethod = enabledLoginMethods.value.find(method => method.key === 'oauth')
  if (oauthMethod && oauthMethod.providers) {
    return oauthMethod.providers
  }
  return []
}

// 获取二维码选项列表
const getQrCodeOptions = () => {
  const qrCodeMethod = enabledLoginMethods.value.find(method => method.key === 'qrcode')
  if (qrCodeMethod && qrCodeMethod.options) {
    return qrCodeMethod.options
  }
  return []
}

// 监听用户名变化
const handleUserNameChange = async (e: Event) => {
  const value = (e.target as HTMLInputElement).value;
}

// 检查是否需要验证码（5分钟内重复登录）
const checkNeedCaptcha = () => {
  const lastLoginTime = localStorage.getItem(LOGIN_STORAGE_KEYS.LAST_LOGIN_TIME)
  if (lastLoginTime) {
    const currentTime = Date.now()
    const timeDiff = currentTime - parseInt(lastLoginTime)
    const minutesDiff = timeDiff / (1000 * 60)
    
    return minutesDiff <= LOGIN_POLICY.CAPTCHA.REQUIRED_MINUTES
  }
  return false
}

// 获取失败次数
const getFailedAttempts = (username: string) => {
  const storedAttempts = localStorage.getItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS)
  if (!storedAttempts) return 0
  
  const attempts = JSON.parse(storedAttempts)
  return attempts[username] || 0
}

// 设置失败次数
const setFailedAttempts = (username: string, count: number) => {
  const attempts = JSON.parse(localStorage.getItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS) || '{}')
  attempts[username] = count
  localStorage.setItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS, JSON.stringify(attempts))
}

// 重置失败次数
const resetFailedAttempts = (username: string) => {
  const attempts = JSON.parse(localStorage.getItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS) || '{}')
  delete attempts[username]
  localStorage.setItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS, JSON.stringify(attempts))
}

// 在文件顶部添加 isAdmin 变量定义
const isAdmin = computed(() => loginForm.value.userName.toLowerCase() === SPECIAL_USERS.ADMIN)

// 处理登录
const handleLogin = async () => {
  try {
    loading.value = true
    
    // 验证表单
    if (!loginForm.value.userName) {
      message.error(t('identity.auth.login.form.usernameRequired'))
      loading.value = false
      return
    }
    
    // 验证凭证
    const isValid = await validateCredentials()
    if (!isValid) {
      loading.value = false
      return
    }
    
    // 1. 获取盐值
    const { data: saltResponse } = await getSalt(loginForm.value.userName)
    if (!saltResponse || !saltResponse.data?.salt) {
      message.error(t('identity.auth.login.error.getSalt'))
      loading.value = false
      return
    }

    // 2. 使用盐值加密密码
    const hashedPassword = PasswordEncryptor.hashPassword(
      loginForm.value.password,
      saltResponse.data.salt,
      saltResponse.data.iterations || 100000
    )

    // 3. 获取设备信息、环境信息和IP地址
    const deviceInfo = await getDeviceInfo()
    const environmentInfo = await getEnvironmentInfo()
    const clientIp = await getClientIpAddress()
    
    const loginParams: LoginParams = {
      userName: loginForm.value.userName,
      password: hashedPassword,
      captchaToken: loginForm.value.captchaToken,
      captchaOffset: loginForm.value.captchaOffset,
      ipAddress: clientIp, // 使用前端获取的IP地址
      userAgent: navigator.userAgent,
      loginType: 0, // 密码登录
      loginSource: 0, // Web登录
      deviceInfo: deviceInfo,
      environmentInfo: environmentInfo
    }

    // 5. 发起登录请求
    await userStore.login(loginParams)
    
    message.success(t('identity.auth.login.success'))
    
    // 登录成功后的处理
    await handleLoginSuccess()
  } catch (error: any) {
    console.error('[登录] 失败:', error)
    
    // 改进错误处理
    let errorMessage = error.message || t('identity.auth.login.error.unknown')
    
    // 处理特定的错误类型
    if (error.response?.data?.msg) {
      errorMessage = error.response.data.msg
    } else if (error.response?.status === 401) {
      errorMessage = '用户名或密码错误'
    } else if (error.response?.status === 403) {
      errorMessage = '用户已被禁用或权限不足'
    } else if (error.response?.status === 404) {
      errorMessage = '用户不存在'
    } else if (error.response?.status >= 500) {
      errorMessage = '服务器错误，请稍后重试'
    }
    
    message.error(errorMessage)
  } finally {
    loading.value = false
  }
}

// 登录成功处理
const handleLoginSuccess = async () => {
  // 记录登录时间
  await userStore.recordLoginTime()
  
  // 重置失败次数
  await userStore.resetLoginFailCount()
  
  // 开始加载菜单
  await menuStore.reloadMenus(router)
  
  // 等待路由更新完成
  await nextTick()
  
  // 初始化翻译
  try {
    await translationStore.initializeTranslations(appStore.language)
  } catch (error) {
    console.error('[登录成功] 翻译初始化失败:', error)
  }
  
  // 准备跳转到首页
  const targetPath = route.query.redirect as string || '/home'
  router.push(targetPath)
}

// 处理登录错误
const handleLoginError = (error: any) => {
  const username = loginForm.value.userName
  const failedAttempts = getFailedAttempts(username) + 1
  setFailedAttempts(username, failedAttempts)
  
  const isAdminUser = username.toLowerCase() === SPECIAL_USERS.ADMIN
  const maxAttempts = isAdminUser ? LOGIN_POLICY.ADMIN.MAX_ATTEMPTS : LOGIN_POLICY.USER.MAX_ATTEMPTS
  
  // 检查是否达到最大尝试次数
  if (failedAttempts > maxAttempts) {
    // 锁定账号
    if (isAdminUser) {
      message.error(t('identity.auth.error.adminLocked', { minutes: LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES }))
    } else {
      message.error(t('identity.auth.error.userDisabled', { days: LOGIN_POLICY.USER.LOCKOUT_DAYS }))
    }
    return
  }
  
  // 检查是否需要验证码
  if (failedAttempts >= LOGIN_POLICY.CAPTCHA.REQUIRED_ATTEMPTS) {
    showCaptcha.value = true
  }
  
  // 显示剩余尝试次数
  const remainingAttempts = maxAttempts - failedAttempts
  message.error(t('identity.auth.error.remainingAttempts', { count: remainingAttempts }))
}

// 验证用户名密码
const validateCredentials = async () => {
  try {
    const username = loginForm.value.userName
    
    // 检查账号是否被锁定
    const response = await checkAccountLockout(username)
    const lockStatus = response.data as unknown as number
    
    if (lockStatus === 2) {
      // 永久锁定
      message.error(t('identity.auth.error.permanentlyLocked'))
      return false
    } else if (lockStatus === 1) {
      // 临时锁定30分钟
      const isAdmin = username.toLowerCase() === SPECIAL_USERS.ADMIN
      const minutes = isAdmin ? LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES : 30
      message.error(t('identity.auth.error.temporarilyLocked', { minutes }))
      return false
    }
    
    // 检查失败次数
    const failedAttempts = getFailedAttempts(username)
    const isAdminUser = username.toLowerCase() === SPECIAL_USERS.ADMIN
    const maxAttempts = isAdminUser ? LOGIN_POLICY.ADMIN.MAX_ATTEMPTS : LOGIN_POLICY.USER.MAX_ATTEMPTS
    
    if (failedAttempts >= maxAttempts) {
      message.error(t('identity.auth.error.tooManyAttempts'))
      return false
    }
    
    return true
  } catch (error) {
    handleLoginError(error)
    return false
  }
}

// 处理验证码成功
const handleCaptchaSuccess = (params: { token: string; xOffset: number }) => {
  try {
    // 验证参数
    if (!params || !params.token) {
      message.error('验证码参数无效')
      return
    }
    
    captchaVerified.value = true
    captchaParams.value = params
    
    handleLogin()
  } catch (error) {
    console.error('[登录验证码成功] 处理验证码成功回调时出错:', error)
    message.error('验证码处理失败，请重试')
    captchaVerified.value = false
    captchaParams.value = null
  }
}

// 处理验证码错误
const handleCaptchaError = (error: string) => {
  try {
    captchaVerified.value = false
    captchaParams.value = null
    message.error(t('identity.auth.captcha.error'))
  } catch (err) {
    console.error('[登录验证码错误] 处理验证码错误回调时出错:', err)
    message.error('验证码错误处理失败')
  }
}

// 显示找回密码弹窗
const showPasswordRecovery = () => {
  showPasswordRecoveryModal.value = true
}

// 处理找回密码弹窗取消
const handlePasswordRecoveryCancel = () => {
  showPasswordRecoveryModal.value = false
  // 重置找回密码组件状态
  if (passwordRecoveryRef.value?.resetAllStates) {
    passwordRecoveryRef.value.resetAllStates()
  }
}

// 处理切换到登录
const handleSwitchToLogin = () => {
  showPasswordRecoveryModal.value = false
  // 重置找回密码组件状态
  if (passwordRecoveryRef.value?.resetAllStates) {
    passwordRecoveryRef.value.resetAllStates()
  }
}

// 处理找回密码成功
const handleRecoverySuccess = (userName: string) => {
  showPasswordRecoveryModal.value = false
  // 重置找回密码组件状态
  if (passwordRecoveryRef.value?.resetAllStates) {
    passwordRecoveryRef.value.resetAllStates()
  }
  message.success(t('identity.auth.passwordRecovery.successMessage', { userName }))
}

// 显示注册弹窗
const handleShowRegisterModal = () => {
  showRegisterModal.value = true
}

// 处理注册弹窗取消
const handleRegisterModalCancel = () => {
  showRegisterModal.value = false
  // 重置注册组件状态
  if (userRegistrationRef.value?.resetAllStates) {
    userRegistrationRef.value.resetAllStates()
  }
}

// 处理注册成功
const handleRegisterSuccess = (userName: string) => {
  showRegisterModal.value = false
  // 重置注册组件状态
  if (userRegistrationRef.value?.resetAllStates) {
    userRegistrationRef.value.resetAllStates()
  }
  message.success(t('identity.auth.register.successMessage', { userName }))
}

// 处理忘记密码（保留原有函数名以兼容）
const handleForgotPassword = () => {
  showPasswordRecovery()
}

// 处理OAuth登录
const handleOAuthLogin = (provider: string) => {
  message.info(t('identity.auth.login.notAvailable', { feature: `${provider}${t('identity.auth.login.form.submit')}` }))
}

// 初始化设备信息
const initDeviceInfo = async () => {
  try {
    loginForm.value.deviceInfo = await getDeviceInfo()
  } catch (error) {
    console.error('初始化设备信息失败:', error)
  }
}

// 初始化环境信息
const initEnvironmentInfo = async () => {
  try {
    const environmentInfo = await getEnvironmentInfo()
    loginForm.value.environmentInfo = environmentInfo
  } catch (error) {
    console.error('初始化环境信息失败:', error)
  }
}

// 在组件挂载时初始化设备信息
onMounted(async () => {
  try {
    // 初始化配置
    await configStore.initialize()
    
    // 加载后端验证码配置
    await loadCaptchaConfig()
    
    // 加载登录方式配置
    await loadLoginMethods()
    
    // 初始化设备信息
    await initDeviceInfo()
    await initEnvironmentInfo()

    // 清理登录状态
    resetFailedAttempts(loginForm.value.userName)
    showCaptcha.value = false
    userStore.setNeedCaptcha(false)

    // 如果之前记住了用户名，自动填充
    const lastUsername = localStorage.getItem('lastUsername')
    if (lastUsername) {
      loginForm.value.userName = lastUsername
      rememberMe.value = true
    }
  } catch (error) {
    console.error('[登录页面初始化] 失败:', error)
    message.error('页面初始化失败，请刷新重试')
  }
})

// 监听验证码弹窗显示状态
watch(showCaptcha, async (newValue) => {
  if (newValue && captchaRef.value) {
    await nextTick()
    try {
      await captchaRef.value.initCaptcha()
    } catch (error) {
      console.error('[验证码] 初始化失败:', error)
      message.error(t('identity.auth.captcha.error.initFailed'))
    }
  }
})

// 组件卸载时清理定时器和状态
onUnmounted(() => {
  // 清理工作
})

const handleLockoutCheck = async (username: string) => {
  try {
    const response = await checkAccountLockout(username)
    const lockStatus = response.data as unknown as number
    if (lockStatus === 2) {
      message.error(t('identity.auth.error.permanentlyLocked'))
      return false
    } else if (lockStatus === 1) {
      const isAdmin = username.toLowerCase() === SPECIAL_USERS.ADMIN
      const minutes = isAdmin ? LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES : 30
      message.error(t('identity.auth.error.accountLocked', { minutes }))
      return false
    }
    return true
  } catch (error) {
    console.error('检查账号锁定状态失败:', error)
    return true // 如果检查失败，默认允许继续
  }
}
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

// 登录页面图标样式优化
:deep(.ant-input-prefix),
:deep(.ant-select-suffix) {
  .anticon {
    transition: all 0.3s ease;
    
    &:hover {
      transform: scale(1.1);
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

    &:hover {
      color: var(--ant-color-primary-hover);
    }
  }
}

.register-link {
  text-align: center;
  margin: 16px 0;
  color: var(--ant-color-text-secondary);
  
  a {
    color: var(--ant-color-primary);
    margin-left: 4px;
    
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

.loading-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 24px;
}

.loading-text {
  margin-top: 16px;
  color: var(--ant-color-text-secondary);
}
</style> 