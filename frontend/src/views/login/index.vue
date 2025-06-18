<template>
  <div class="login-container">
    <hbt-header-login />
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
            <a-select
              v-model:value="loginForm.tenantId"
              :placeholder="t('identity.auth.login.tenantId')"
              class="login-input"
              :options="tenantList"
              :suffixIcon="h(ApartmentOutlined)"
              @change="(value) => loginForm.tenantId = Number(value)"
            />
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
        <hbt-slider-captcha ref="captchaRef" @success="handleCaptchaSuccess" @error="handleCaptchaError" />
      </div>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
// 类型导入
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { LoginParams } from '@/types/identity/auth'
import { HbtDeviceType, HbtOsType, HbtBrowserType } from '@/types/audit/loginDevLog'

// API和组件导入
import { getSalt, checkAccountLockout } from '@/api/identity/auth'
import { getTenantOptions } from '@/api/identity/tenant'
import { PasswordEncryptor } from '@/utils/crypto'
import { useUserStore } from '@/stores/user'
import { useMenuStore } from '@/stores/menu'
import { useAppStore } from '@/stores/app'
import { useTranslationStore } from '@/stores/translation'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, reactive, onMounted, onUnmounted, nextTick, watch, computed, h } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { getDeviceInfo } from '@/utils/device'
import { getEnvironmentInfo } from '@/utils/environment'
import { LOGIN_POLICY, LOGIN_STORAGE_KEYS, SPECIAL_USERS } from '@/types/identity/auth'
import { registerDynamicRoutes } from '@/router'
import { ApartmentOutlined, UserOutlined, LockOutlined, GithubOutlined } from '@ant-design/icons-vue'


const { t } = useI18n()
const router = useRouter()
const route = useRoute()
const userStore = useUserStore()
const menuStore = useMenuStore()
const appStore = useAppStore()
const translationStore = useTranslationStore()

// 表单引用
const loginFormRef = ref<FormInstance>()
const captchaRef = ref()

// 登录表单数据
const loginForm = ref({
  tenantId: 1,
  userName: 'admin',
  password: '123456',
  captchaToken: '',
  captchaOffset: 0,
  loginSource: 0,
  deviceInfo: null as any,
  environmentInfo: null as any
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

// 验证码状态
const captchaVerified = ref(false)
const captchaParams = ref<{ token: string; xOffset: number } | null>(null)

// 租户列表
const tenantList = ref<{ value: number; label: string }[]>([])

// 获取租户列表
const loadTenantList = async () => {
  const { data: res } = await getTenantOptions()
  console.log('[租户列表] API响应:', res)
  if (res.code === 200 && Array.isArray(res.data)) {
    tenantList.value = res.data.map(item => ({
      value: item.value,
      label: item.label
    }))
    console.log('[租户列表] 加载成功:', tenantList.value)
  }
}

// 检查是否需要验证码（5分钟内重复登录）
const checkNeedCaptcha = () => {
  const lastLoginTime = localStorage.getItem(LOGIN_STORAGE_KEYS.LAST_LOGIN_TIME)
  if (lastLoginTime) {
    const currentTime = Date.now()
    const timeDiff = currentTime - parseInt(lastLoginTime)
    const minutesDiff = timeDiff / (1000 * 60)
    
    console.log('[验证码策略] 时间检查:', {
      lastLoginTime: new Date(parseInt(lastLoginTime)).toLocaleString(),
      currentTime: new Date(currentTime).toLocaleString(),
      timeDiff: `${timeDiff}ms`,
      minutesDiff: `${minutesDiff.toFixed(2)}分钟`,
      requireCaptcha: minutesDiff <= LOGIN_POLICY.CAPTCHA.REQUIRED_MINUTES
    })
    
    return minutesDiff <= LOGIN_POLICY.CAPTCHA.REQUIRED_MINUTES
  }
  console.log('[验证码策略] 未找到上次登录时间记录')
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

// 监听用户名变化
const handleUserNameChange = (e: Event) => {
  const value = (e.target as HTMLInputElement).value;
  // 不再自动设置租户ID
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
    
    // 确保租户ID已设置
    if (!loginForm.value.tenantId) {
      message.error(t('identity.auth.login.form.tenantIdRequired'))
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
      return
    }

    // 2. 使用盐值加密密码
    const hashedPassword = PasswordEncryptor.hashPassword(
      loginForm.value.password,
      saltResponse.data.salt,
      saltResponse.data.iterations || 100000
    )

    // 3. 获取设备信息和环境信息
    const deviceInfo = await getDeviceInfo()
    const environmentInfo = await getEnvironmentInfo()

    // 4. 构建登录参数
    console.log('[登录] 表单数据:', loginForm.value)
    console.log('[登录] 加密后的密码:', hashedPassword)
    console.log('[登录] 设备信息:', deviceInfo)
    console.log('[登录] 环境信息:', environmentInfo)
    
    const loginParams: LoginParams = {
      tenantId: Number(loginForm.value.tenantId), // 确保租户ID是数字类型
      userName: loginForm.value.userName,
      password: hashedPassword,
      captchaToken: loginForm.value.captchaToken,
      captchaOffset: loginForm.value.captchaOffset,
      ipAddress: window.location.hostname,
      userAgent: navigator.userAgent,
      loginType: 0, // 密码登录
      loginSource: 0, // Web登录
      deviceInfo: deviceInfo,
      environmentInfo: environmentInfo
    }
    
    console.log('[登录] 构建的登录参数:', loginParams)

    // 5. 发起登录请求
    await userStore.login(loginParams)
    message.success(t('identity.auth.login.success'))
    // 登录成功后的处理
    await handleLoginSuccess()
  } catch (error: any) {
    console.error('[登录] 失败:', error)
    message.error(error.message || t('identity.auth.login.error.unknown'))
  } finally {
    loading.value = false
  }
}

// 登录成功处理
const handleLoginSuccess = async () => {
  console.log('[登录成功] 开始处理登录成功流程')
  
  // 记录登录时间
  await userStore.recordLoginTime()
  console.log('[登录成功] 记录登录时间完成')
  
  // 重置失败次数
  await userStore.resetLoginFailCount()
  console.log('[登录成功] 重置失败次数完成')
  
  // 开始加载菜单
  console.log('[登录成功] 开始加载菜单')
  await menuStore.reloadMenus(router)
  console.log('[登录成功] 菜单加载完成')
  
  // 等待路由更新完成
  console.log('[登录成功] 等待路由更新完成')
  await nextTick()
  
  // 初始化翻译
  try {
    console.log('[登录成功] 开始初始化翻译')
    await translationStore.initializeTranslations(appStore.language)
    console.log('[登录成功] 翻译初始化完成')
  } catch (error) {
    console.error('[登录成功] 翻译初始化失败:', error)
  }
  
  // 准备跳转到首页
  console.log('[登录成功] 准备跳转到首页')
  const targetPath = route.query.redirect as string || '/home'
  router.push(targetPath)
  console.log('[登录成功] 跳转到:', targetPath)
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
  captchaVerified.value = true
  captchaParams.value = params
  handleLogin()
}

// 处理验证码错误
const handleCaptchaError = (error: string) => {
  captchaVerified.value = false
  captchaParams.value = null
  message.error(t('identity.auth.captcha.error'))
}

// 处理忘记密码
const handleForgotPassword = () => {
  message.info(t('identity.auth.login.notAvailable', { feature: t('identity.auth.login.form.forgot') }))
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
  await initDeviceInfo()
  await initEnvironmentInfo()
  await loadTenantList()
  
  // 清理登录状态
  resetFailedAttempts(loginForm.value.userName)
  showCaptcha.value = false
  userStore.setNeedCaptcha(false)

  // 如果之前记住了用户名，自动填充
  const lastUsername = localStorage.getItem('lastUsername')
  if (lastUsername) {
    loginForm.value.userName = lastUsername
    rememberMe.value = true
    // 如果是admin用户，自动设置租户ID为0
    if (lastUsername.toLowerCase() === 'admin') {
      loginForm.value.tenantId = 1
    }
  }
})

// 监听验证码弹窗显示状态
watch(showCaptcha, async (newValue) => {
  if (newValue && captchaRef.value) {
    await nextTick()
    console.log('[验证码] 监听到显示状态变化，开始初始化')
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
  // 移除这些变量的引用，因为它们不存在
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