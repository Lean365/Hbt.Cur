import request from '@/utils/request'
import type { 
  LoginParams, 
  LoginResult, 
  UserInfo, 
  SaltResponse, 
  CaptchaResponse, 
  CaptchaResult,
  LockoutStatus,
  LoginCheckResult
} from '@/types/identity/auth'
import type { HbtApiResponse } from '@/types/common'
import type { UserInfoResponse } from '@/stores/user'
import { getToken } from '@/utils/auth'

/**
 * 登录
 * @param data 登录参数
 */
export function login(data: LoginParams) {
  return request<HbtApiResponse<LoginResult>>({
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
  return request<HbtApiResponse<SaltResponse>>({
    url: '/api/HbtAuth/salt',
    method: 'get',
    params: { username }
  }).then(response => {
    if (response.code === 200 && response.data) {
      return response.data
    }
    throw new Error('获取盐值失败')
  })
}

/**
 * 登出
 * @param params 登出参数
 */
export function logout(params?: { isSystemRestart?: boolean }) {
  return request<HbtApiResponse<void>>({
    url: '/api/HbtAuth/logout',
    method: 'post',
    params,
    data: null,
    headers: {
      'Content-Type': 'application/json',
      'X-Requested-With': 'XMLHttpRequest'
    }
  }).finally(() => {
    // 清理本地存储
    localStorage.removeItem('token')
    localStorage.removeItem('refreshToken')
    localStorage.removeItem('userInfo')
    localStorage.removeItem('isLoggingOut')
  })
}

/**
 * 刷新Token
 * @param refreshToken 刷新令牌
 */
export function refreshToken(refreshToken: string) {
  return request<HbtApiResponse<LoginResult>>({
    url: '/api/HbtAuth/refresh-token',
    method: 'post',
    data: { refreshToken }
  })
}

/**
 * 获取用户信息
 */
export function getInfo() {
  return request<HbtApiResponse<UserInfoResponse>>({
    url: '/api/HbtAuth/info',
    method: 'get'
  })
}

/**
 * 获取验证码
 */
export function getCaptcha() {
  return request<HbtApiResponse<CaptchaResponse>>({
    url: '/api/HbtAuth/captcha',
    method: 'get'
  })
}

/**
 * 验证验证码
 * @param data 验证参数
 */
export function verifyCaptcha(data: { token: string; offset: number }) {
  return request<HbtApiResponse<CaptchaResult>>({
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
  return request<HbtApiResponse<LockoutStatus>>({
    url: `/api/HbtAuth/lockout/${username}`,
    method: 'get'
  })
}

/**
 * 获取剩余尝试次数
 * @param username 用户名
 */
export function getRemainingAttempts(username: string) {
  return request<HbtApiResponse<number>>({
    url: `/api/HbtAuth/attempts/${username}`,
    method: 'get'
  })
}

/**
 * 解锁用户
 * @param username 用户名
 */
export function unlockUser(username: string) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtAuth/unlock/${username}`,
    method: 'post'
  })
}

/**
 * 检查登录状态
 * @param data 登录参数
 */
export function checkLogin(data: LoginParams) {
  return request<HbtApiResponse<LoginCheckResult>>({
    url: '/api/HbtAuth/check-login',
    method: 'post',
    data
  })
} 