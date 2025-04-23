import { useUserStore } from '@/stores/user'
import axios from 'axios'
import Cookies from 'js-cookie'

const TokenKey = 'Admin-Token'
const RefreshTokenKey = 'Admin-Refresh-Token'

// 获取Token
export function getToken() {
  return Cookies.get(TokenKey)
}

// 设置Token
export function setToken(token: string) {
  // 同时设置axios默认请求头
  axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
  return Cookies.set(TokenKey, token)
}

// 移除Token
export function removeToken() {
  // 同时移除axios默认请求头
  delete axios.defaults.headers.common['Authorization']
  return Cookies.remove(TokenKey)
}

// 获取刷新令牌
export function getRefreshToken() {
  return Cookies.get(RefreshTokenKey)
}

// 设置刷新令牌
export function setRefreshToken(token: string) {
  return Cookies.set(RefreshTokenKey, token)
}

// 移除刷新令牌
export function removeRefreshToken() {
  return Cookies.remove(RefreshTokenKey)
}

// 检查Token是否有效
export const isTokenValid = (): boolean => {
  const token = getToken()
  if (!token) return false

  try {
    const tokenParts = token.split('.')
    if (tokenParts.length === 3) {
      const payload = JSON.parse(atob(tokenParts[1]))
      return payload.exp * 1000 > Date.now()
    }
  } catch (e) {
    console.error('Token 验证失败:', e)
  }
  return false
} 