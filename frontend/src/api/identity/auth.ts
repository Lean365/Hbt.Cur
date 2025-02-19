import request from '@/utils/request'
import type { 
  LoginParams, 
  LoginResult, 
  UserInfo, 
  SaltResponse, 
  CaptchaResponse, 
  CaptchaResult 
} from '@/types/identity/auth'
import type { ApiResult } from '@/types/base'

/**
 * 登录
 * @param data 登录参数
 */
export function login(data: LoginParams) {
  return request.post<ApiResult<LoginResult>>('/api/auth/login', data)
}

/**
 * 获取盐值
 * @param username 用户名
 */
export function getSalt(username: string) {
  return request.get<ApiResult<SaltResponse>>('/api/auth/salt', {
    params: { username }
  })
}

/**
 * 登出
 */
export function logout() {
  return request.post<ApiResult<void>>('/api/auth/logout')
}

/**
 * 刷新Token
 * @param refreshToken 刷新令牌
 */
export function refreshToken(refreshToken: string) {
  return request.post<ApiResult<LoginResult>>('/api/auth/refresh-token', {
    refreshToken
  })
}

/**
 * 获取用户信息
 */
export function getInfo() {
  return request.get<ApiResult<UserInfo>>('/api/auth/info')
}

/**
 * 获取验证码
 */
export function getCaptcha() {
  return request.get<ApiResult<CaptchaResponse>>('/api/auth/captcha')
}

/**
 * 验证验证码
 * @param data 验证参数
 */
export function verifyCaptcha(data: { token: string; offset: number }) {
  return request.post<ApiResult<CaptchaResult>>('/api/auth/verify-captcha', data)
} 