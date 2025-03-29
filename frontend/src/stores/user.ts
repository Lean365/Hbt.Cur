import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { MenuProps } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { HbtApiResponse } from '@/types/common'
import type { LoginParams, UserInfo, LoginResult } from '@/types/identity/auth'
import { login as userLogin, logout as userLogout, getInfo } from '@/api/identity/auth'
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
  const isSignalRConnected = ref(false)  // 添加SignalR连接状态

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
      if (!isSignalRConnected.value) {
        await signalRService.start()
        isSignalRConnected.value = true
        console.log('[SignalR] 连接已建立')

        // 注册单点登录踢出处理
        if (signalRService['connection']) {
          signalRService['connection'].on('Kickout', (msg: string) => {
            console.log('[SignalR] 收到踢出通知:', msg)
            handleKickout(msg)
          })
        }
      }
    } catch (error) {
      console.error('[SignalR] 连接失败:', error)
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
      message.warning(msg || '您的账号已在其他设备上登录')
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

  // 登录
  const login = async (loginParams: LoginParams): Promise<HbtApiResponse<LoginResult>> => {
    console.log('[用户登录] identity.auth.login.start', {
      ...loginParams,
      password: '******'
    })

    try {
      console.log('[用户登录] 开始调用 userLogin')
      const response = await userLogin(loginParams)
      console.log('[用户登录] 登录响应:', response)

      if (!response || response.code !== 200 || !response.data) {
        console.error('[用户登录] 登录响应中没有数据')
        throw new Error(t('identity.auth.login.noToken'))
      }

      const loginResult = response.data
      if (!loginResult.accessToken) {
        console.error('[用户登录] 登录响应中没有访问令牌', {
          hasData: !!loginResult,
          dataStructure: Object.keys(loginResult)
        })
        throw new Error(t('identity.auth.login.noToken'))
      }

      // 先保存 token 到本地存储，确保后续请求可以使用
      setToken(loginResult.accessToken)
      console.log('[用户登录] Token已保存')

      // 保存到 store
      token.value = loginResult.accessToken
      refreshTokenValue.value = loginResult.refreshToken

      // 验证token是否被正确设置
      const currentToken = getToken()
      if (!currentToken) {
        console.error('[用户登录] Token未被正确设置')
        throw new Error(t('identity.auth.login.tokenNotSet'))
      }

      // 添加一个小延迟，确保token已经被正确保存
      await new Promise(resolve => setTimeout(resolve, 100))

      // 获取完整的用户信息（包括权限和角色）
      try {
        console.log('[用户登录] 开始获取用户信息')
        const userInfo = await getUserInfo()
        console.log('[用户登录] 用户信息获取成功:', {
          用户: userInfo.user,
          角色: userInfo.roles,
          权限: userInfo.permissions
        })
        
        // 设置用户信息
        user.value = userInfo.user
        roles.value = userInfo.roles
        permissions.value = userInfo.permissions

        // 初始化SignalR连接
        await initSignalR()

        return response
      } catch (error) {
        console.error('[用户登录] 获取用户信息失败:', error)
        // 清理token
        removeToken()
        token.value = null
        refreshTokenValue.value = null
        throw new Error(t('identity.auth.login.getUserInfoFailed'))
      }
    } catch (error) {
      console.error('[用户登录] 登录失败:', error)
      throw error
    }
  }

  // 获取用户信息
  const getUserInfo = async () => {
    try {
      console.log('[用户信息] 开始获取用户信息')
      const res = await getInfo()
      if (res.code === 200) {
        const userInfo = res.data
        console.log('[用户信息] 获取成功:', userInfo)
        
        // 更新store状态
        user.value = userInfo.user
        roles.value = userInfo.roles || []
        permissions.value = userInfo.permissions || []
        
        return {
          user: userInfo.user,
          roles: userInfo.roles || [],
          permissions: userInfo.permissions || []
        }
      } else {
        console.error('[用户信息] 获取失败:', res)
        return Promise.reject(new Error(res.msg))
      }
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
    $reset
  }
}) 