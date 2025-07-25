<template>
  <div class="password-login-container">
    <div class="login-header">
      <h2>{{ t('identity.auth.passwordLogin.title') }}</h2>
      <p>{{ t('identity.auth.passwordLogin.subtitle') }}</p>
    </div>

    <a-form
      :model="loginForm"
      :rules="loginRules"
      ref="loginFormRef"
      @finish="handleLogin"
      layout="vertical"
    >
      <!-- 用户名输入 -->
      <a-form-item name="userName" :label="t('identity.auth.passwordLogin.userName')">
        <a-input
          v-model:value="loginForm.userName"
          :placeholder="t('identity.auth.passwordLogin.userNamePlaceholder')"
          size="large"
        >
          <template #prefix>
            <user-outlined />
          </template>
        </a-input>
      </a-form-item>

      <!-- 密码输入 -->
      <a-form-item name="password" :label="t('identity.auth.passwordLogin.password')">
        <a-input-password
          v-model:value="loginForm.password"
          :placeholder="t('identity.auth.passwordLogin.passwordPlaceholder')"
          size="large"
        >
          <template #prefix>
            <lock-outlined />
          </template>
        </a-input-password>
      </a-form-item>

      <!-- 验证码 -->
      <a-form-item name="captcha" :label="t('identity.auth.passwordLogin.captcha')" v-if="showCaptcha">
        <!-- 滑块验证码 -->
        <HbtSliderCaptcha
          v-if="captchaType === 'Slider'"
          ref="captchaRef"
          v-model:captcha-token="captchaToken"
          v-model:captcha-offset="loginForm.captcha"
          @refresh="refreshCaptcha"
          :loading="captchaLoading"
          @success="handleCaptchaSuccess"
          @error="handleCaptchaError"
        />
        
        <!-- 行为验证码 -->
        <HbtBehaviorCaptcha
          v-else-if="captchaType === 'Behavior'"
          ref="captchaRef"
          v-model:captcha-token="captchaToken"
          v-model:captcha-score="loginForm.captcha"
          @refresh="refreshCaptcha"
          :loading="captchaLoading"
          @success="handleCaptchaSuccess"
          @error="handleCaptchaError"
        />
      </a-form-item>

      <!-- 记住密码和忘记密码 -->
      <div class="login-options">
        <a-checkbox v-model:checked="loginForm.rememberMe">
          {{ t('identity.auth.passwordLogin.rememberMe') }}
        </a-checkbox>
        <a-button type="link" @click="$emit('switchToPasswordRecovery')" class="forgot-password">
          {{ t('identity.auth.passwordLogin.forgotPassword') }}
        </a-button>
      </div>

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
          {{ t('identity.auth.passwordLogin.login') }}
        </a-button>
      </a-form-item>
    </a-form>

    <!-- 其他登录方式 -->
    <div class="other-login-methods">
      <div class="divider">
        <span>{{ t('identity.auth.passwordLogin.or') }}</span>
      </div>
      
      <div class="login-methods">
        <a-button type="link" @click="$emit('switchToSms')">
          {{ t('identity.auth.passwordLogin.smsLogin') }}
        </a-button>
        <a-button type="link" @click="$emit('switchToQrCode')">
          {{ t('identity.auth.passwordLogin.qrCodeLogin') }}
        </a-button>
        <a-button type="link" @click="$emit('switchToThirdParty')">
          {{ t('identity.auth.passwordLogin.thirdPartyLogin') }}
        </a-button>
      </div>
    </div>

    <!-- 底部操作 -->
    <div class="login-footer">
      <a-button type="link" @click="$emit('switchToRegister')">
        {{ t('identity.auth.passwordLogin.register') }}
      </a-button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import { 
  UserOutlined, 
  LockOutlined 
} from '@ant-design/icons-vue'
import { getCaptchaConfig } from '@/api/security/captcha'
import { login } from '@/api/identity/auth/auth'
import { getDeviceInfo } from '@/utils/device'
import { getEnvironmentInfo } from '@/utils/environment'


const { t } = useI18n()
const emit = defineEmits(['switchToSms', 'switchToQrCode', 'switchToThirdParty', 'switchToRegister', 'switchToPasswordRecovery', 'loginSuccess'])

// 表单引用
const loginFormRef = ref<FormInstance>()
const captchaRef = ref()

// 登录表单
const loginForm = reactive({
  userName: '',
  password: '',
  captcha: '',
  rememberMe: false
})

// 状态
const loading = ref(false)
const captchaLoading = ref(false)
const showCaptcha = ref(false)
const captchaToken = ref('')
const captchaValid = ref(false)

// 验证码类型（完全由后端配置）
const captchaType = ref<'Slider' | 'Behavior'>('Slider')

// 表单验证规则
const loginRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    userName: [
      { required: true, message: t('identity.auth.passwordLogin.form.userNameRequired'), trigger: 'blur' },
      { min: 3, max: 20, message: t('identity.auth.passwordLogin.form.userNameLength'), trigger: 'blur' }
    ],
    password: [
      { required: true, message: t('identity.auth.passwordLogin.form.passwordRequired'), trigger: 'blur' },
      { min: 6, max: 20, message: t('identity.auth.passwordLogin.form.passwordLength'), trigger: 'blur' }
    ]
  }
  
  // 如果需要验证码，添加验证码验证规则
  if (showCaptcha.value) {
    rules.captcha = [
      { required: true, message: t('identity.auth.passwordLogin.form.captchaRequired'), trigger: 'blur' }
    ]
  }
  
  return rules
})

// 表单是否有效
const formValid = computed(() => {
  const basicValid = loginForm.userName && loginForm.password
  return showCaptcha.value ? (basicValid && captchaValid.value) : basicValid
})

// 获取后端验证码配置
const loadCaptchaConfig = async () => {
  try {
    const { data } = await getCaptchaConfig('login')
    
    // 检查数据结构：data.data 是实际的配置数据
    const configData = data.data || data
    if (configData && configData.type) {
      captchaType.value = configData.type as 'Slider' | 'Behavior'
      showCaptcha.value = true
    } else {
      showCaptcha.value = false
    }
  } catch (error) {
    console.error('获取验证码配置失败:', error)
    showCaptcha.value = false
  }
}

// 刷新验证码
const refreshCaptcha = async () => {
  captchaLoading.value = true
  try {
    // 重置验证码相关状态
    captchaToken.value = ''
    loginForm.captcha = ''
    captchaValid.value = false
    
    // 验证码组件会自动调用API获取新的验证码
  } catch (error) {
    console.error('[密码登录] 刷新验证码失败:', error)
    message.error('获取验证码失败')
  } finally {
    captchaLoading.value = false
  }
}

// 验证码成功回调
const handleCaptchaSuccess = (params: { token: string; xOffset: number }) => {
  try {
    // 验证参数
    if (!params || !params.token) {
      message.error('验证码参数无效')
      return
    }
    
    // 更新表单数据
    captchaToken.value = params.token
    loginForm.captcha = params.xOffset?.toString() || '0'
    
    // 设置验证码为有效
    captchaValid.value = true
  } catch (error) {
    console.error('[验证码成功] 处理验证码成功回调时出错:', error)
    message.error('验证码处理失败，请重试')
    captchaValid.value = false
  }
}

// 验证码错误回调
const handleCaptchaError = (error: any) => {
  try {
    captchaValid.value = false
    captchaToken.value = ''
    loginForm.captcha = ''
    message.error(t('identity.auth.captcha.error'))
  } catch (err) {
    console.error('[验证码错误] 处理验证码错误回调时出错:', err)
    message.error('验证码错误处理失败')
  }
}

// 处理登录
const handleLogin = async () => {
  try {
    loading.value = true
    
    // 验证表单
    await loginFormRef.value?.validate()
    
    // 获取设备信息和环境信息
    const deviceInfo = await getDeviceInfo()
    const environmentInfo = await getEnvironmentInfo()
    
    // 调用登录API
    const { data } = await login({
      userName: loginForm.userName,
      password: loginForm.password,
      captchaToken: showCaptcha.value ? captchaToken.value : '',
      captchaOffset: showCaptcha.value ? (parseInt(loginForm.captcha) || 0) : 0,
      ipAddress: '',
      userAgent: navigator.userAgent,
      loginType: 1, // 密码登录
      loginSource: 0, // Web登录
      deviceInfo: deviceInfo,
      environmentInfo: environmentInfo
    })
    
    if (data.code === 200) {
      message.success(t('identity.auth.passwordLogin.loginSuccess'))
      emit('loginSuccess', data.data.userInfo)
    } else {
      message.error(data.msg || t('identity.auth.passwordLogin.loginFailed'))
      
      // 如果登录失败且需要验证码，刷新验证码
      if (showCaptcha.value && captchaRef.value?.resetCaptcha) {
        captchaRef.value.resetCaptcha()
      }
    }
  } catch (error: any) {
    console.error('[密码登录] 登录失败:', error)
    message.error(error.message || t('identity.auth.passwordLogin.loginFailed'))
    
    // 如果登录失败且需要验证码，刷新验证码
    if (showCaptcha.value && captchaRef.value?.resetCaptcha) {
      captchaRef.value.resetCaptcha()
    }
  } finally {
    loading.value = false
  }
}

// 重置所有状态
const resetAllStates = () => {
  // 重置表单数据
  Object.assign(loginForm, { 
    userName: '', 
    password: '', 
    captcha: '', 
    rememberMe: false 
  })
  
  // 重置状态
  loading.value = false
  captchaLoading.value = false
  captchaToken.value = ''
  captchaValid.value = false
  
  // 重置表单验证状态
  loginFormRef.value?.resetFields()
  
  // 重新加载验证码配置并显示验证码
  loadCaptchaConfig().then(() => {
    // 重置验证码组件状态
    if (captchaRef.value?.resetCaptcha) {
      captchaRef.value.resetCaptcha()
    }
  })
}

// 暴露重置方法给父组件
defineExpose({
  resetAllStates
})

// 组件挂载时初始化
onMounted(async () => {
  // 重置所有状态
  resetAllStates()
  
  // 加载验证码配置
  await loadCaptchaConfig()
})
</script>

<style lang="less" scoped>
.password-login-container {
  width: 100%;
  max-width: 400px;
  margin: 0 auto;
  padding: 24px;
}

.login-header {
  text-align: center;
  margin-bottom: 32px;
  
  h2 {
    font-size: 24px;
    font-weight: 600;
    color: var(--ant-color-text);
    margin-bottom: 8px;
  }
  
  p {
    color: var(--ant-color-text-secondary);
    font-size: 14px;
  }
}

.login-options {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
  
  .forgot-password {
    padding: 0;
    height: auto;
    border: none;
    box-shadow: none;
    
    &:hover {
      background: transparent;
    }
  }
}

.other-login-methods {
  margin: 24px 0;
}

.divider {
  position: relative;
  text-align: center;
  margin: 16px 0;
  
  &::before {
    content: '';
    position: absolute;
    top: 50%;
    left: 0;
    right: 0;
    height: 1px;
    background: var(--ant-color-border-split);
  }
  
  span {
    background: var(--ant-color-bg-container);
    padding: 0 16px;
    color: var(--ant-color-text-secondary);
    font-size: 14px;
  }
}

.login-methods {
  display: flex;
  justify-content: center;
  gap: 16px;
  flex-wrap: wrap;
  
  .ant-btn-link {
    padding: 0;
    height: auto;
    border: none;
    box-shadow: none;
    
    &:hover {
      background: transparent;
    }
  }
}

.login-footer {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 24px;
  padding-top: 16px;
  border-top: 1px solid var(--ant-color-border-split);
  
  .ant-btn-link {
    padding: 0;
    height: auto;
    border: none;
    box-shadow: none;
    
    &:hover {
      background: transparent;
    }
  }
}

:deep(.ant-form-item-label) {
  font-weight: 500;
}

:deep(.ant-input-prefix) {
  color: var(--ant-color-text-secondary);
}

:deep(.ant-checkbox-wrapper) {
  font-size: 14px;
}
</style> 