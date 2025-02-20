import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { MenuProps } from 'ant-design-vue'
import type { ApiResult } from '@/types/base'
import type { LoginParams, UserInfo as AuthUserInfo, LoginResult as AuthLoginResult } from '@/types/identity/auth'
import { login as userLogin, logout as userLogout, getInfo } from '@/api/identity/auth'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { useMenuStore } from './menu'

export interface UserInfo extends AuthUserInfo {
  displayName: string
  email: string
}

export interface LoginResult extends AuthLoginResult {
  userInfo: UserInfo
}

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

  // 设置是否需要验证码
  const setNeedCaptcha = (value: boolean) => {
    needCaptcha.value = value
  }

  // 登录
  const login = async (loginParams: LoginParams): Promise<ApiResult<LoginResult>> => {
    try {
      const response = await userLogin(loginParams)
      const { accessToken } = response.data
      if (accessToken) {
        setToken(accessToken)
      }
      return response as ApiResult<LoginResult>
    } catch (error) {
      console.error('登录失败:', error)
      throw error
    }
  }

  // 获取用户信息
  const getUserInfo = async () => {
    try {
      const response = await getInfo()
      if (!response || !response.data) {
        throw new Error('获取用户信息失败')
      }
      
      const data = response.data as unknown as UserInfoResponse
      
      // 设置用户信息
      user.value = data.user
      roles.value = data.roles
      permissions.value = data.permissions
      
      // 加载菜单和权限
      const menuStore = useMenuStore()
      await menuStore.loadUserMenus()
      
      return data
    } catch (error: any) {
      console.error('获取用户信息失败:', error)
      throw error
    }
  }

  // 登出
  const logout = async () => {
    try {
      await userLogout()
      user.value = null
      roles.value = []
      permissions.value = []
      removeToken()
      
      // 重置菜单状态
      const menuStore = useMenuStore()
      menuStore.clearMenus()
    } catch (error) {
      console.error('登出失败:', error)
      throw error
    }
  }

  return {
    user,
    roles,
    permissions,
    needCaptcha,
    setNeedCaptcha,
    login,
    getUserInfo,
    logout
  }
}) 