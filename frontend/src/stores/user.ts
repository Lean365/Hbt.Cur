import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { MenuProps } from 'ant-design-vue'
import type { HbtApiResult } from '@/types/common'
import type { LoginParams, UserInfo as AuthUserInfo, LoginResult as AuthLoginResult } from '@/types/identity/auth'
import { login as userLogin, logout as userLogout, getInfo } from '@/api/identity/auth'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { useMenuStore } from './menu'
import { useDictStore } from './dict'
import i18n from '@/locales'

const { t } = i18n.global

export interface UserInfo extends AuthUserInfo {
  userName: string
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
  const login = async (loginParams: LoginParams): Promise<HbtApiResult<LoginResult>> => {
    try {
      console.log('[用户登录] ' + t('identity.auth.login.start'), loginParams)
      const response = await userLogin(loginParams)
      const { accessToken } = response.data
      
      if (accessToken) {
        console.log('[用户登录] ' + t('identity.auth.login.success'))
        setToken(accessToken)
      } else {
        console.warn('[用户登录] ' + t('identity.auth.login.noToken'))
      }
      
      return response as HbtApiResult<LoginResult>
    } catch (error) {
      console.error('[用户登录] ' + t('identity.auth.login.error'), error)
      throw error
    }
  }

  // 获取用户信息
  const getUserInfo = async () => {
    try {
      console.log('[用户信息] 正在加载用户信息')
      const response = await getInfo()
      console.log('[用户信息] 获取用户信息成功', response)
      
      if (!response?.data) {
        console.error('[用户信息] 响应数据为空')
        throw new Error('用户信息响应数据为空')
      }

      const info = response.data as unknown as UserInfoResponse
      
      // 打印详细信息
      console.log('[用户信息] 详细数据:', {
        用户信息: info.user,
        角色列表: info.roles,
        权限列表: info.permissions?.length ? `共${info.permissions.length}个权限` : '无权限'
      })

      // 更新状态
      user.value = info.user
      roles.value = info.roles
      permissions.value = info.permissions
      
      // 加载菜单和权限
      const menuStore = useMenuStore()
      await menuStore.loadUserMenus()
      
      return info
    } catch (error: any) {
      console.error('[用户信息] 获取失败:', {
        错误消息: error.message,
        响应状态: error.response?.status,
        响应数据: error.response?.data,
        请求配置: {
          url: error.config?.url,
          方法: error.config?.method,
          请求头: error.config?.headers
        }
      })
      throw error
    }
  }

  // 登出
  const logout = async () => {
    try {
      console.log('[用户登出] ' + t('identity.auth.logout.start'))
      await userLogout()
      
      // 清除用户状态
      user.value = null
      roles.value = []
      permissions.value = []
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
    setNeedCaptcha,
    login,
    getUserInfo,
    logout
  }
}) 