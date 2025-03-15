import request from '@/utils/request'
import type { 
  LoginParams, 
  LoginResult, 
  UserInfo, 
  SaltResponse, 
  CaptchaResponse, 
  CaptchaResult,
  LockoutStatus
} from '@/types/identity/auth'
import type { HbtApiResult } from '@/types/common'

/**
 * 登录
 * @param data 登录参数
 */
export function login(data: LoginParams) {
  return request<HbtApiResult<LoginResult>>({
    url: '/api/HbtAuth/login',
    method: 'post',
    data
  })
}

/**
 * 获取盐值
 * @param username 用户名
 */
export function getSalt(username: string) {
  return request<HbtApiResult<SaltResponse>>({
    url: '/api/HbtAuth/salt',
    method: 'get',
    params: { username }
  })
}

/**
 * 登出
 */
export function logout() {
  return request<HbtApiResult<void>>({
    url: '/api/HbtAuth/logout',
    method: 'post'
  })
}

/**
 * 刷新Token
 * @param refreshToken 刷新令牌
 */
export function refreshToken(refreshToken: string) {
  return request<HbtApiResult<LoginResult>>({
    url: '/api/HbtAuth/refresh-token',
    method: 'post',
    data: { refreshToken }
  })
}

/**
 * 获取用户信息
 */
export function getInfo() {
  return request<HbtApiResult<UserInfo>>({
    url: '/api/HbtAuth/info',
    method: 'get'
  })
}

/**
 * 获取验证码
 */
export function getCaptcha() {
  return request<HbtApiResult<CaptchaResponse>>({
    url: '/api/HbtAuth/captcha',
    method: 'get'
  })
}

/**
 * 验证验证码
 * @param data 验证参数
 */
export function verifyCaptcha(data: { token: string; offset: number }) {
  return request<HbtApiResult<CaptchaResult>>({
    url: '/api/HbtAuth/verify-captcha',
    method: 'post',
    data
  })
}

/**
 * 检查账号锁定状态
 * @param username 用户名
 */
export function checkAccountLockout(username: string) {
  return request<HbtApiResult<LockoutStatus>>({
    url: `/api/HbtAuth/lockout/${username}`,
    method: 'get'
  })
}

/**
 * 获取剩余尝试次数
 * @param username 用户名
 */
export function getRemainingAttempts(username: string) {
  return request<HbtApiResult<number>>({
    url: `/api/HbtAuth/attempts/${username}`,
    method: 'get'
  })
}

/**
 * 解锁用户
 * @param username 用户名
 */
export function unlockUser(username: string) {
  return request<HbtApiResult<boolean>>({
    url: `/api/HbtAuth/unlock/${username}`,
    method: 'post'
  })
} 