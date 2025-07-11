import { defineStore } from 'pinia'
import type { MenuProps } from 'ant-design-vue'
import type { UserInfo, LoginResultData } from '@/types/identity/auth.d'
import { login as userLogin, logout as userLogout, getInfo as fetchUserInfo, refreshUserToken } from '@/api/identity/auth'
import { removeToken, removeRefreshToken, setToken, setRefreshToken, getToken, getRefreshToken } from '@/utils/auth'
import { ref, computed } from 'vue'
import axios from 'axios'
import { clearAutoLogout, initAutoLogout } from '@/utils/autoLogout'

// 扩展UserInfo类型以包含额外的字段
export interface UserInfoResponse extends UserInfo {
  menus?: MenuProps['items']
}

export const useUserStore = defineStore('user', () => {
  // 用户信息
  const userInfo = ref<UserInfoResponse>()
  // 是否需要验证码
  const needCaptcha = ref<boolean>(false)
  // 最后登录时间
  const lastLoginTime = ref<string>('')
  // 登录失败次数
  const loginFailCount = ref<number>(0)
  // 用户信息是否已加载
  const isUserInfoLoaded = ref<boolean>(false)
  // 防止并发刷新
  const isRefreshing = ref<boolean>(false)

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
      console.log('[User] 用户信息API响应:', response)
      
      if (response.code === 200) {
        const userData = response.data as UserInfoResponse
        
        userInfo.value = userData
        isUserInfoLoaded.value = true
        
        console.log('[User] 用户信息获取成功:', {
          用户ID: userInfo.value.userId,
          用户名: userInfo.value.userName,
          昵称: userInfo.value.nickName,
          角色数: userInfo.value.roles?.length || 0,
          权限数: userInfo.value.permissions?.length || 0,
          头像: userInfo.value.avatar || '未设置'
        })
        
        return userInfo.value
      }
      
      console.error('[User] 用户信息API返回错误:', response.msg)
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
      console.log('[User] 开始登录流程:', {
        用户名: loginData.userName
      })
      
      // 登录前清除所有缓存
      clearUserInfo()
      
      const { data: response } = await userLogin(loginData)
      console.log('[User] 登录API响应:', response)
      
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
        
        console.log('[User] 登录流程完成:', {
          用户ID: userInfo.userId,
          用户名: userInfo.userName
        })
        
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
  }

  // 登出
  const logout = async (callApi = true) => {
    try {
      if (callApi) {
        await userLogout()
      }
      // 清除用户信息
      clearUserInfo()
      // 清除 token
      removeToken()
      removeRefreshToken()
      // 清除登录时间
      lastLoginTime.value = ''
      // 重置登录失败次数
      resetLoginFailCount()
      // 清理自动登出
      clearAutoLogout()
    } catch (error) {
      console.error('登出失败:', error)
      // 即使 API 调用失败，也要清除本地状态
      clearUserInfo()
      removeToken()
      removeRefreshToken()
      lastLoginTime.value = ''
      resetLoginFailCount()
      clearAutoLogout()
    }
  }

  // 刷新Token
  const refreshToken = async () => {
    // 防止并发刷新
    if (isRefreshing.value) {
      console.log('[User] Token正在刷新中，等待完成')
      // 等待刷新完成
      while (isRefreshing.value) {
        await new Promise(resolve => setTimeout(resolve, 100))
      }
      return true
    }

    isRefreshing.value = true
    
    try {
      const refreshToken = getRefreshToken()
      if (!refreshToken) {
        console.error('[User] 刷新令牌不存在')
        throw new Error('刷新令牌不存在')
      }

      console.log('[User] 开始刷新Token，刷新令牌长度:', refreshToken.length)
      const { data: response } = await refreshUserToken(refreshToken)
      
      if (response.code === 200) {
        const loginResult = response.data as LoginResultData
        // 保存新token
        setToken(loginResult.accessToken)
        setRefreshToken(loginResult.refreshToken)
        console.log('[User] Token刷新成功')
        return true
      }
      
      // 处理具体的错误情况
      if (response.code === 401) {
        console.error('[User] 刷新令牌已过期或无效')
        throw new Error('刷新令牌已过期，请重新登录')
      } else if (response.code === 403) {
        console.error('[User] 用户权限不足')
        throw new Error('用户权限不足')
      } else {
        console.error('[User] 刷新令牌失败:', response.msg)
        throw new Error(response.msg || '刷新令牌失败')
      }
    } catch (error: any) {
      console.error('[User] 刷新令牌失败:', error)
      
      // 根据错误类型决定是否清除数据
      if (error.message?.includes('刷新令牌已过期') || 
          error.message?.includes('无效') ||
          error.message?.includes('不存在') ||
          error.response?.status === 401 ||
          error.response?.status === 403) {
        // 刷新令牌相关错误，清除所有数据
        console.log('[User] 刷新令牌错误，清除所有认证数据')
        clearUserInfo()
        removeToken()
        removeRefreshToken()
      }
      
      throw error
    } finally {
      isRefreshing.value = false
    }
  }

  return {
    userInfo,
    needCaptcha,
    lastLoginTime,
    loginFailCount,
    isUserInfoLoaded,
    getUserInfo,
    setNeedCaptcha,
    recordLoginTime,
    resetLoginFailCount,
    incrementLoginFailCount,
    login,
    logout,
    refreshToken,
    clearUserInfo,
    permissions: computed(() => userInfo.value?.permissions || []),
    roles: computed(() => userInfo.value?.roles || [])
  }
}) 