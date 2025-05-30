import { defineStore } from 'pinia'
import type { MenuProps } from 'ant-design-vue'
import type { UserInfo, LoginResultData } from '@/types/identity/auth.d'
import { login as userLogin, logout as userLogout, getInfo as fetchUserInfo } from '@/api/identity/auth'
import { removeToken, removeRefreshToken, setToken, setRefreshToken, getToken } from '@/utils/auth'
import { ref, computed } from 'vue'
import axios from 'axios'

// 扩展UserInfo类型以包含额外的字段
export interface UserInfoResponse extends UserInfo {
  menus?: MenuProps['items']
}

export const useUserStore = defineStore('user', () => {
  // 用户信息
  const userInfo = ref<UserInfoResponse>()
  // 当前租户ID
  const currentTenantId = ref<number>(0)
  // 是否需要验证码
  const needCaptcha = ref<boolean>(false)
  // 最后登录时间
  const lastLoginTime = ref<string>('')
  // 登录失败次数
  const loginFailCount = ref<number>(0)
  // 用户信息是否已加载
  const isUserInfoLoaded = ref<boolean>(false)

  // 获取用户信息
  const getUserInfo = async (forceRefresh = false) => {
    try {
      const token = getToken()
      if (!token) {
        console.log('[User] 未找到Token，跳过获取用户信息')
        return null
      }

      // 如果不是强制刷新，且有缓存数据，则使用缓存
      if (!forceRefresh && isUserInfoLoaded.value && userInfo.value) {
        console.log('[User] 使用缓存用户信息')
        return userInfo.value
      }

      console.log('[User] 开始获取用户信息')
      const { data: response } = await fetchUserInfo()
      if (response.code === 200) {
        const userData = response.data as UserInfoResponse
        
        // 验证租户ID
        if (!userData.tenantId || userData.tenantId <= 0) {
          console.error('[User] 用户租户ID无效:', userData.tenantId)
          throw new Error('用户租户ID无效')
        }
        
        userInfo.value = userData
        isUserInfoLoaded.value = true
        
        // 设置租户ID
        setCurrentTenantId(userData.tenantId)
        
        console.log('[User] 用户信息获取成功:', {
          用户ID: userInfo.value.userId,
          用户名: userInfo.value.userName,
          昵称: userInfo.value.nickName,
          租户ID: userInfo.value.tenantId,
          角色数: userInfo.value.roles?.length || 0,
          权限数: userInfo.value.permissions?.length || 0,
          头像: userInfo.value.avatar || '未设置'
        })
        
        return userInfo.value
      }
      throw new Error(response.msg || '获取用户信息失败')
    } catch (error) {
      console.error('[User] 获取用户信息失败:', error)
      // 只有在强制刷新（登录）时才清除token
      if (forceRefresh) {
        console.log('[User] 登录时获取用户信息失败，清除token')
        removeToken()
        removeRefreshToken()
      }
      return null
    }
  }

  // 获取当前租户ID
  const getCurrentTenantId = (): number => {
    return userInfo.value?.tenantId ?? 0
  }

  // 设置当前租户ID
  const setCurrentTenantId = (tenantId: number) => {
    if (!tenantId || tenantId <= 0) {
      console.error('[User] 无效的租户ID:', tenantId)
      throw new Error('无效的租户ID')
    }
    
    if (userInfo.value) {
      userInfo.value.tenantId = tenantId
    }
    
    // 更新请求头
    axios.defaults.headers.common['X-Tenant-Id'] = tenantId.toString()
    
    console.log('[User] 租户ID设置完成:', {
      tenantId,
      headers: axios.defaults.headers.common
    })
  }

  // 设置是否需要验证码
  const setNeedCaptcha = (value: boolean) => {
    needCaptcha.value = value
  }

  // 记录登录时间
  const recordLoginTime = () => {
    const now = new Date().toISOString()
    lastLoginTime.value = now
    localStorage.setItem('lastLoginTime', now)
  }

  // 重置登录失败次数
  const resetLoginFailCount = () => {
    loginFailCount.value = 0
    localStorage.removeItem('loginFailCount')
  }

  // 增加登录失败次数
  const incrementLoginFailCount = () => {
    loginFailCount.value++
    localStorage.setItem('loginFailCount', loginFailCount.value.toString())
    // 如果失败次数达到阈值，需要验证码
    if (loginFailCount.value >= 3) {
      setNeedCaptcha(true)
    }
  }

  // 登录
  const login = async (loginData: any) => {
    try {
      // 登录前清除所有缓存
      clearUserInfo()
      
      const { data: response } = await userLogin(loginData)
      if (response.code === 200) {
        const loginResult = response.data as LoginResultData
        console.log('[User] 登录成功，开始获取最新用户信息')
        
        // 保存token
        setToken(loginResult.accessToken)
        setRefreshToken(loginResult.refreshToken)
        
        // 强制从后端获取最新用户信息
        const userInfo = await getUserInfo(true)
        if (!userInfo) {
          throw new Error('获取用户信息失败')
        }
        
        // 记录登录时间
        recordLoginTime()
        // 重置登录失败次数
        resetLoginFailCount()
        
        return response
      }
      // 登录失败，增加失败次数
      incrementLoginFailCount()
      throw new Error(response.msg || '登录失败')
    } catch (error) {
      console.error('[User] 登录失败:', error)
      // 登录失败时清除所有数据
      clearUserInfo()
      removeToken()
      removeRefreshToken()
      throw error
    }
  }

  // 清除用户信息
  const clearUserInfo = () => {
    userInfo.value = undefined
    isUserInfoLoaded.value = false
    currentTenantId.value = 0
  }

  // 登出
  const logout = async () => {
    try {
      await userLogout()
      // 清除用户信息
      clearUserInfo()
      // 清除 token
      removeToken()
      removeRefreshToken()
      // 清除登录时间
      lastLoginTime.value = ''
      // 重置登录失败次数
      resetLoginFailCount()
    } catch (error) {
      console.error('登出失败:', error)
    }
  }

  return {
    userInfo,
    currentTenantId,
    needCaptcha,
    lastLoginTime,
    loginFailCount,
    isUserInfoLoaded,
    getUserInfo,
    getCurrentTenantId,
    setCurrentTenantId,
    setNeedCaptcha,
    recordLoginTime,
    resetLoginFailCount,
    incrementLoginFailCount,
    login,
    logout,
    clearUserInfo,
    permissions: computed(() => userInfo.value?.permissions || [])
  }
}) 