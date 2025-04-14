import { useUserStore } from '@/stores/user'
import axios from 'axios'

// 获取Token
export const getToken = (): string | null => {
  return localStorage.getItem('token')
}

// 设置Token
export const setToken = (token: string) => {
  localStorage.setItem('token', token)
}

// 移除Token
export const removeToken = () => {
  localStorage.removeItem('token')
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