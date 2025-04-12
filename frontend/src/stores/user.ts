import { defineStore } from 'pinia'
import { ref, computed, nextTick, watch, reactive } from 'vue'
import type { MenuProps } from 'ant-design-vue'
import { message, Modal } from 'ant-design-vue'
import type { HbtApiResponse } from '@/types/common'
import type { LoginParams, UserInfo, LoginResult, LoginCheckResult } from '@/types/identity/auth'
import { login as userLogin, logout as userLogout, getInfo, checkLogin } from '@/api/identity/auth'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { useMenuStore } from './menu'
import { useDictStore } from './dict'
import i18n from '@/locales'
import request from '@/utils/request'
import { signalRService } from '@/utils/SignalR/service'
import router from '@/router'

const { t } = i18n.global

export interface UserInfoResponse {
  userId: number
  userName: string
  nickName: string
  englishName?: string
  tenantId: number
  tenantName: string
  avatar?: string
  userType: number
  roles: string[]
  permissions: string[]
  menus: MenuProps['items']
}

export const useUserStore = defineStore('user', () => {
  // 状态
  const user = ref<UserInfoResponse | null>(null)
  const roles = ref<string[]>([])
  const permissions = ref<string[]>([])
  const needCaptcha = ref(false)
  const token = ref<string | null>(null)
  const refreshTokenValue = ref<string | null>(null)
  const isSignalRConnected = ref(false)

  // 计算属性
  const isLoggedIn = computed(() => !!user.value)

  // 重置状态
  const $reset = () => {
    user.value = null
    roles.value = []
    permissions.value = []
    needCaptcha.value = false
    isSignalRConnected.value = false
    removeToken()
    
    // 清除字典缓存
    const dictStore = useDictStore()
    dictStore.clearCache()
    
    // 重置菜单状态
    const menuStore = useMenuStore()
    menuStore.clearMenus()
  }

  // 设置是否需要验证码
  const setNeedCaptcha = (value: boolean) => {
    needCaptcha.value = value
  }

  // 初始化SignalR连接
  const initSignalR = async () => {
    try {
      console.log('[SignalR] 准备初始化连接')
      
      // 获取当前用户信息和token
      const currentUser = user.value
      const currentToken = getToken()
      
      console.log('[SignalR] 当前用户信息:', currentUser)
      console.log('[SignalR] 当前Token:', currentToken)
      
      // 检查用户信息和token是否存在
      if (!currentUser || !currentToken) {
        console.error('[SignalR] 未找到用户信息或Token，无法建立连接', {
          hasUser: !!currentUser,
          hasToken: !!currentToken
        })
        return
      }
      
      // 如果已经连接，直接返回
      if (isSignalRConnected.value) {
        console.log('[SignalR] 已经连接，无需重复初始化')
        return
      }
      
      // 启动SignalR连接
      await signalRService.start()
      isSignalRConnected.value = true
      console.log('[SignalR] 连接初始化完成')
    } catch (error) {
      console.error('[SignalR] 连接初始化失败:', error)
      isSignalRConnected.value = false
    }
  }

  // 登录方法
  const login = async (params: LoginParams) => {
    try {
      console.log('[Login] 开始登录流程')
      const { userName, password } = params
      
      // 检查是否已有会话
      const checkResponse = await checkLogin(params)
      if (checkResponse.data?.canLogin === false) {
        console.log('[Login] 检测到其他会话在线:', {
          deviceInfo: checkResponse.data.existingDeviceInfo,
          userName
        })
        throw new Error('您的账号已在其他地方登录')
      }
      
      console.log('[Login] 开始登录请求')
      const { data } = await userLogin(params)
      console.log('[Login] 登录响应:', data)
      
      if (data) {
        console.log('[Login] 登录成功，设置 Token')
        setToken(data.accessToken)
        console.log('[Login] Token 已设置，准备获取用户信息')
        try {
          const userInfoResponse = await getInfo()
          console.log('[Login] 用户信息获取响应:', userInfoResponse)
          const userInfo = userInfoResponse.data
          console.log('[Login] 用户信息数据:', userInfo)
          
          if (userInfo) {
            console.log('[Login] 更新用户状态')
            user.value = userInfo
            roles.value = userInfo.roles || []
            permissions.value = userInfo.permissions || []
          } else {
            console.error('[Login] 用户信息为空')
          }
        } catch (error) {
          console.error('[Login] 获取用户信息失败:', error)
          throw error
        }
      }
    } catch (error: any) {
      console.error('[Login] 登录失败:', error)
      if (error.response?.data?.error === 'ConcurrentLogin') {
        console.log('[Login] 检测到并发登录错误')
        throw new Error('您的账号已在其他地方登录')
      }
      throw error
    }
  }

  // 处理被踢出的情况
  const handleKickout = async (msg: string) => {
    try {
      console.log('[UserStore] 开始处理踢出')
      
      // 执行登出操作
      await logout(true)  // true表示是被踢出
      console.log('[UserStore] 登出完成')
      
      // 停止SignalR连接
      if (isSignalRConnected.value) {
        await signalRService.stop()
        isSignalRConnected.value = false
        console.log('[UserStore] SignalR连接已停止')
      }

      // 显示提示消息
      message.warning(msg || t('identity.auth.login.error.concurrentLogin'))
      console.log('[UserStore] 提示消息已显示')
      
      // 跳转到登录页
      console.log('[UserStore] 准备跳转到登录页')
      await router.replace('/login')
      console.log('[UserStore] 路由跳转完成')
      
      // 强制刷新页面
      window.location.reload()
    } catch (error) {
      console.error('[UserStore] 处理踢出失败:', error)
    }
  }

  // 获取用户信息
  const getUserInfo = async () => {
    try {
      console.log('[用户信息] 开始获取用户信息')
      const token = getToken()
      console.log('[用户信息] 当前Token:', token)
      
      if (!token) {
        console.error('[用户信息] 未找到Token')
        return Promise.reject(new Error('未找到Token'))
      }
      
      const response = await getInfo()
      console.log('[用户信息] 获取响应:', response)
      
      if (response.code === 200 && response.data) {
        const userInfo = response.data
        console.log('[用户信息] 获取成功:', userInfo)
        
        // 更新store状态
        if (userInfo) {
          try {
            // 直接设置用户信息
            user.value = userInfo
            roles.value = userInfo.roles || []
            permissions.value = userInfo.permissions || []
            
            // 等待store更新完成
            await nextTick()
            
            // 检查用户信息是否设置成功
            if (!user.value) {
              console.error('[用户信息] 用户信息设置失败')
              return Promise.reject(new Error('用户信息设置失败'))
            }
            
            console.log('[用户信息] 用户信息已设置:', user.value)
            
            // 初始化SignalR连接
            try {
              console.log('[用户信息] 开始初始化SignalR连接')
              await initSignalR()
              console.log('[用户信息] SignalR连接初始化完成')
            } catch (error) {
              console.error('[用户信息] SignalR连接初始化失败:', error)
              // 不抛出错误，让流程继续
            }
            
            // 返回用户信息
            return userInfo
          } catch (error) {
            console.error('[用户信息] 设置用户信息时出错:', error)
            return Promise.reject(error)
          }
        }
      }
      console.error('[用户信息] 获取失败:', response)
      return Promise.reject(new Error(response.msg || '获取用户信息失败'))
    } catch (error) {
      console.error('[用户信息] 获取出错:', error)
      return Promise.reject(error)
    }
  }

  // 登出
  const logout = async (isKickout = false) => {
    try {
      console.log('[用户登出] ' + t('identity.auth.logout.start'))
      
      // 如果不是被踢出，才调用登出接口
      if (!isKickout) {
        await userLogout()
      }
      
      // 停止SignalR连接
      if (isSignalRConnected.value) {
        await signalRService.stop()
        // 重置 SignalR 连接状态
        signalRService.resetConnectionState()
        isSignalRConnected.value = false
      }
      
      // 登出成功后再清除状态
      user.value = null
      roles.value = []
      permissions.value = []
      
      // 清除token
      removeToken()
      
      // 清除字典缓存
      const dictStore = useDictStore()
      dictStore.clearCache()
      
      // 重置菜单状态
      const menuStore = useMenuStore()
      menuStore.clearMenus()
      
      console.log('[用户登出] ' + t('identity.auth.logout.success'))
    } catch (error) {
      console.error('[用户登出] ' + t('identity.auth.logout.error'), error)
      throw error
    }
  }

  return {
    user,
    roles,
    permissions,
    needCaptcha,
    isSignalRConnected,
    setNeedCaptcha,
    login,
    getUserInfo,
    logout,
    handleKickout,
    $reset,
    isLoggedIn,
    initSignalR,
    recordLoginTime: () => {
      localStorage.setItem('lastLoginTime', Date.now().toString())
    },
    resetLoginFailCount: () => {
      localStorage.removeItem('failedAttempts')
    }
  }
}) 