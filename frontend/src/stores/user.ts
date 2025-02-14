import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { LoginParams, UserInfo } from '@/types/auth'
import { login as userLogin } from '@/api/auth'

export const useUserStore = defineStore('user', () => {
  const token = ref('')
  const userInfo = ref<UserInfo | null>(null)
  const needCaptcha = ref(false)

  // 登录
  const login = async (loginParams: LoginParams) => {
    try {
      const loginData = await userLogin(loginParams)

      if (!loginData?.accessToken) {
        throw new Error('登录失败：未获取到访问令牌')
      }

      token.value = loginData.accessToken
      userInfo.value = loginData.userInfo
      // 登录成功后重置验证码状态
      needCaptcha.value = false
      // 存储token
      localStorage.setItem('token', loginData.accessToken)
      return true
    } catch (error: any) {
      // 如果服务器返回需要验证码
      if (error.response?.data?.code === 'NEED_CAPTCHA') {
        needCaptcha.value = true
      }
      throw error
    }
  }

  // 登出
  const logout = () => {
    token.value = ''
    userInfo.value = null
    needCaptcha.value = false
    localStorage.removeItem('token')
  }

  // 获取用户信息
  const getUserInfo = () => {
    return userInfo.value
  }

  // 设置是否需要验证码
  const setNeedCaptcha = (value: boolean) => {
    needCaptcha.value = value
  }

  return {
    token,
    userInfo,
    needCaptcha,
    login,
    logout,
    getUserInfo,
    setNeedCaptcha
  }
}) 