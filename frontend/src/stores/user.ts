import { defineStore } from 'pinia'
import type { MenuProps } from 'ant-design-vue'
import type { LoginParams, LoginResult, UserInfo } from '@/types/identity/auth'
import { login, logout as userLogout, getInfo, checkLogin } from '@/api/identity/auth'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { useMenuStore } from './menu'
import { useDictStore } from './dict'
import i18n from '@/locales'
import { getDeviceInfo } from '@/utils/device'
import request from '@/utils/request'

const { t } = i18n.global

// 扩展UserInfo类型以包含额外的字段
export interface UserInfoResponse extends UserInfo {
  menus?: MenuProps['items']
}

export const useUserStore = defineStore('user', {
  state: () => ({
    token: getToken(),
    userInfo: null as UserInfoResponse | null,
    permissions: [] as string[],
    roles: [] as string[],
    needCaptcha: false, // 添加验证码状态
    lastLoginTime: null as Date | null, // 添加最后登录时间
    loginFailCount: 0 // 添加登录失败次数
  }),
  
  actions: {
    // 设置是否需要验证码
    setNeedCaptcha(value: boolean) {
      this.needCaptcha = value
    },

    // 记录登录时间
    recordLoginTime() {
      this.lastLoginTime = new Date()
      console.log('[Login] 记录登录时间:', this.lastLoginTime)
    },

    // 重置登录失败次数
    resetLoginFailCount() {
      this.loginFailCount = 0
      console.log('[Login] 重置登录失败次数')
    },

    // 增加登录失败次数
    incrementLoginFailCount() {
      this.loginFailCount++
      console.log('[Login] 登录失败次数:', this.loginFailCount)
    },

    // 登录
    async login(loginParams: LoginParams) {
      try {
        console.log('[UserStore] 开始登录流程')
        
        // 1. 检查登录状态
        const checkResult = await checkLogin(loginParams)
        console.log('[UserStore] 登录状态检查结果:', checkResult)
        
        if (!checkResult.data.canLogin) {
          throw new Error('当前设备不允许登录')
        }
        
        // 2. 执行登录
        const result = await login(loginParams)
        console.log('[UserStore] 登录响应:', result)
        
        // 3. 保存token和用户信息
        if (result.code === 200 && result.data) {
          setToken(result.data.accessToken)
          this.token = result.data.accessToken
          this.userInfo = result.data.userInfo
          this.permissions = result.data.userInfo.permissions
          this.roles = result.data.userInfo.roles
          
          console.log('[UserStore] 登录成功')
          return result
        } else {
          throw new Error(result.msg || '登录失败')
        }
      } catch (error) {
        console.error('[UserStore] 登录失败:', error)
        this.loginFailCount++
        throw error
      }
    },

    async getUserInfo() {
      try {
        const response = await getInfo()
        this.userInfo = response.data
        this.permissions = response.data.permissions || []
        this.roles = response.data.roles || []
      } catch (error) {
        console.error('[UserStore] 获取用户信息失败:', error)
        throw error
      }
    },

    async logout() {
      try {
        console.log('[Logout] 开始退出登录')
        await userLogout()
        removeToken()
        this.token = null
        this.userInfo = null
        this.permissions = []
        this.roles = []
        console.log('[Logout] 退出登录成功')
      } catch (error) {
        console.error('[Logout] 退出登录失败:', error)
        throw error
      }
    }
  }
}) 