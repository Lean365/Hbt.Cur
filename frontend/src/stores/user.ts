import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
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
  user: UserInfo
  roles: string[]
  permissions: string[]
  menus: MenuProps['items']
}

export const useUserStore = defineStore('user', () => {
  // 状态
  const user = ref<UserInfo | null>(null)
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
      
      // 如果已经连接，直接返回
      if (isSignalRConnected.value) {
        console.log('[SignalR] 连接已存在，跳过初始化')
        return
      }
      
      // 启动SignalR连接
      await signalRService.start()
      
      // 注册踢出事件监听
      signalRService.on('Kickout', handleKickout)
      signalRService.on('UserKickedOut', handleKickout)
      signalRService.on('ForceOffline', handleKickout)
      
      isSignalRConnected.value = true
      console.log('[SignalR] 连接初始化完成')
    } catch (error) {
      console.error('[SignalR] 连接初始化失败:', error)
      isSignalRConnected.value = false
      throw error
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
        console.log('[Login] Token 已设置，获取用户信息')
        const userInfoResponse = await getInfo()
        const userInfo = userInfoResponse.data
        console.log('[Login] 用户信息:', userInfo)
        
        if (userInfo) {
          console.log('[Login] 更新用户状态')
          user.value = userInfo.user
          roles.value = userInfo.roles || []
          permissions.value = userInfo.permissions || []
          
          // 只有在未连接时才初始化SignalR
          if (!isSignalRConnected.value) {
            await initSignalR()
          }
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
      const response = await getInfo()
      if (response.code === 200 && response.data) {
        const userInfo: UserInfoResponse = response.data
        console.log('[用户信息] 获取成功:', userInfo)
        
        // 更新store状态
        if (userInfo) {
          user.value = userInfo.user
          roles.value = userInfo.roles || []
          permissions.value = userInfo.permissions || []
          
          // 只有在未连接时才初始化SignalR
          if (!isSignalRConnected.value) {
            await initSignalR()
          }
          
          return {
            user: userInfo.user,
            roles: userInfo.roles || [],
            permissions: userInfo.permissions || []
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
    initSignalR
  }
}) 