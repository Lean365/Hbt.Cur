import { defineStore } from 'pinia'
import type { MenuProps } from 'ant-design-vue'
import type { LoginParams, LoginResultData, UserInfo } from '@/types/identity/auth.d'
import { login, logout as userLogout, getInfo, checkLogin } from '@/api/identity/auth'
import { getToken, setToken, removeToken, setRefreshToken, removeRefreshToken } from '@/utils/auth'
import { useMenuStore } from './menu'
import { useDictStore } from './dict'
import i18n from '@/locales'
import { getDeviceInfo } from '@/utils/device'
import request from '@/utils/request'
import { SignalRService } from '@/utils/SignalR/service'

const { t } = i18n.global

// 扩展UserInfo类型以包含额外的字段
export interface UserInfoResponse extends UserInfo {
  menus?: MenuProps['items']
  roles: string[]
  permissions: string[]
}

export const useUserStore = defineStore('user', {
  state: () => ({
    token: getToken(),
    refreshToken: '',
    tokenExpireTime: 0,
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
        const { data: checkResult } = await checkLogin(loginParams)
        console.log('[UserStore] 登录状态检查结果:', checkResult)
        
        if (!checkResult.data.canLogin) {
          throw new Error('当前设备不允许登录')
        }
        
        // 2. 执行登录
        const { data: result } = await login(loginParams)
        console.log('[UserStore] 登录响应:', result)
        
        // 3. 保存token和用户信息
        if (result.code === 200 && result.data) {
          const loginData = result.data
          
          // 设置token
          if (loginData.accessToken) {
            setToken(loginData.accessToken)
            this.token = loginData.accessToken
            console.log('[UserStore] Token设置成功:', loginData.accessToken)
          } else {
            console.warn('[UserStore] 登录响应中没有找到accessToken字段')
          }

          // 设置刷新token
          if (loginData.refreshToken) {
            setRefreshToken(loginData.refreshToken)
            this.refreshToken = loginData.refreshToken
            console.log('[UserStore] 刷新Token设置成功:', loginData.refreshToken)
          } else {
            console.warn('[UserStore] 登录响应中没有找到refreshToken字段')
          }

          // 设置过期时间
          if (loginData.expiresIn) {
            this.tokenExpireTime = Date.now() + loginData.expiresIn * 1000
            console.log('[UserStore] Token过期时间设置成功:', new Date(this.tokenExpireTime).toLocaleString())
          } else {
            console.warn('[UserStore] 登录响应中没有找到expiresIn字段')
          }
          
          // 设置用户信息
          if (loginData.userInfo) {
            this.userInfo = loginData.userInfo as UserInfoResponse
            this.permissions = Array.isArray(loginData.userInfo.permissions) ? loginData.userInfo.permissions : []
            this.roles = Array.isArray(loginData.userInfo.roles) ? loginData.userInfo.roles : []
          } else {
            console.warn('[UserStore] 登录响应中没有找到userInfo字段')
          }
          
          console.log('[UserStore] 登录成功，用户信息:', {
            token: this.token,
            userInfo: this.userInfo,
            permissions: this.permissions,
            roles: this.roles
          })

          // 4. 获取最新的用户信息（包含菜单等）
          await this.getUserInfo()
          
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

    // 获取用户信息
    async getUserInfo() {
      try {
        console.log('[UserStore] 开始获取用户信息')
        const { data: response } = await getInfo()
        
        if (response.code === 200 && response.data) {
          const { permissions = [], roles = [], ...rest } = response.data
          this.userInfo = { ...this.userInfo, ...rest, roles, permissions } as UserInfoResponse
          this.permissions = permissions
          this.roles = roles
          
          console.log('[UserStore] 获取用户信息成功:', {
            userInfo: this.userInfo,
            permissions: this.permissions,
            roles: this.roles
          })
        } else {
          throw new Error(response.msg || '获取用户信息失败')
        }
      } catch (error) {
        console.error('[UserStore] 获取用户信息失败:', error)
        throw error
      }
    },

    async logout() {
      try {
        // 停止 SignalR 连接
        const signalRService = SignalRService.getInstance()
        await signalRService.stop()
        
        // 调用登出 API
        await userLogout()
        
        // 清除本地存储
        removeToken()
        this.token = ''
        this.userInfo = null
        this.roles = []
        this.permissions = []
      } catch (error) {
        console.error('登出失败:', error)
        throw error
      }
    },

    // 初始化 SignalR 连接
    async initSignalR() {
      try {
        const signalRService = SignalRService.getInstance()
        await signalRService.start()
        console.log('[UserStore] SignalR 连接初始化成功')
      } catch (error) {
        console.error('[UserStore] SignalR 连接初始化失败:', error)
        throw error
      }
    }
  }
}) 