import request from '@/utils/request'
import type { LoginParams, LoginResult } from '@/types/auth'
import type { HbtApiResult } from '@/types/api'

// 盐值响应接口
export interface SaltResponse {
  salt: string
  iterations: number
}

// 登录
export function login(data: LoginParams) {
  return request<HbtApiResult<LoginResult>>({
    url: '/api/auth/login',
    method: 'post',
    data
  })
}

// 获取盐值
export function getSalt(username: string) {
  return request<HbtApiResult<SaltResponse>>({
    url: '/api/auth/salt',
    method: 'get',
    params: { username }
  })
}

// 登出
export function logout() {
  return request<HbtApiResult<void>>({
    url: '/api/auth/logout',
    method: 'post'
  })
}

// 刷新Token
export function refreshToken(token: string) {
  return request<HbtApiResult<LoginResult>>({
    url: '/api/auth/refresh-token',
    method: 'post',
    data: { refreshToken: token }
  })
} 