import axios from 'axios'

const TOKEN_KEY = 'access_token'
const REFRESH_TOKEN_KEY = 'refresh_token'

/**
 * 获取Token
 */
export function getToken(): string | null {
  const token = localStorage.getItem(TOKEN_KEY)
  if (token) {
    console.log('[Auth] 获取到Token:', token.substring(0, 20) + '...')
    // 设置 axios 默认请求头
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return token
  }
  console.log('[Auth] 未找到Token')
  return null
}

/**
 * 设置Token
 */
export function setToken(token: string): void {
  if (!token) {
    console.warn('[Auth] 尝试设置空Token')
    return
  }
  try {
    localStorage.setItem(TOKEN_KEY, token)
    // 设置 axios 默认请求头
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    console.log('[Auth] Token设置成功:', {
      token: token.substring(0, 20) + '...',
      header: `Bearer ${token.substring(0, 20)}...`
    })
  } catch (error) {
    console.error('[Auth] Token设置失败:', error)
  }
}

/**
 * 移除Token
 */
export function removeToken(): void {
  try {
    localStorage.removeItem(TOKEN_KEY)
    // 删除 axios 默认请求头
    delete axios.defaults.headers.common['Authorization']
    console.log('[Auth] Token已移除')
  } catch (error) {
    console.error('[Auth] Token移除失败:', error)
  }
}

/**
 * 获取刷新Token
 */
export function getRefreshToken(): string | null {
  const token = localStorage.getItem(REFRESH_TOKEN_KEY)
  if (token) {
    console.log('[Auth] 获取到刷新Token:', token.substring(0, 20) + '...')
    return token
  }
  console.log('[Auth] 未找到刷新Token')
  return null
}

/**
 * 设置刷新Token
 */
export function setRefreshToken(token: string): void {
  if (!token) {
    console.warn('[Auth] 尝试设置空刷新Token')
    return
  }
  try {
    localStorage.setItem(REFRESH_TOKEN_KEY, token)
    console.log('[Auth] 刷新Token设置成功:', token.substring(0, 20) + '...')
  } catch (error) {
    console.error('[Auth] 刷新Token设置失败:', error)
  }
}

/**
 * 移除刷新Token
 */
export function removeRefreshToken(): void {
  try {
    localStorage.removeItem(REFRESH_TOKEN_KEY)
    console.log('[Auth] 刷新Token已移除')
  } catch (error) {
    console.error('[Auth] 刷新Token移除失败:', error)
  }
}