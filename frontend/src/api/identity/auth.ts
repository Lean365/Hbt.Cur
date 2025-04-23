import request from '@/utils/request'
import type { AxiosResponse } from 'axios'
import type { 
  LoginParams, 
  LoginResultData, 
  UserInfo, 
  SaltResponse, 
  CaptchaResponse, 
  CaptchaResult,
  LockoutStatus,
  LoginCheckResult,
  LoginCheckResultData
} from '@/types/identity/auth'
import type { HbtApiResponse } from '@/types/common'
import type { UserInfoResponse } from '@/stores/user'
import { getToken } from '@/utils/auth'

/**
 * 登录
 * @param data 登录参数
 */
export function login(data: LoginParams): Promise<AxiosResponse<HbtApiResponse<LoginResultData>>> {
  return request({
    url: '/api/HbtAuth/login',
    method: 'post',
    data
  })
}

/**
 * 获取盐值
 * @param username 用户名
 */
export function getSalt(username: string): Promise<AxiosResponse<HbtApiResponse<SaltResponse>>> {
  return request({
    url: '/api/HbtAuth/salt',
    method: 'get',
    params: { username }
  })
}

/**
 * 登出
 * @param params 登出参数
 */
export function logout(params?: { isSystemRestart?: boolean }): Promise<AxiosResponse<HbtApiResponse<void>>> {
  return request({
    url: '/api/HbtAuth/logout',
    method: 'post',
    params
  })
}

/**
 * 刷新Token
 * @param refreshToken 刷新令牌
 */
export function refreshToken(refreshToken: string): Promise<AxiosResponse<HbtApiResponse<LoginResultData>>> {
  return request({
    url: '/api/HbtAuth/refresh-token',
    method: 'post',
    data: { refreshToken }
  })
}

/**
 * 获取用户信息
 */
export function getInfo(): Promise<AxiosResponse<HbtApiResponse<UserInfoResponse>>> {
  return request({
    url: '/api/HbtAuth/info',
    method: 'get'
  })
}

/**
 * 获取验证码
 */
export function getCaptcha(): Promise<AxiosResponse<HbtApiResponse<CaptchaResponse>>> {
  return request({
    url: '/api/HbtAuth/captcha',
    method: 'get'
  })
}

/**
 * 验证验证码
 * @param data 验证参数
 */
export function verifyCaptcha(data: { token: string; offset: number }): Promise<AxiosResponse<HbtApiResponse<CaptchaResult>>> {
  return request({
    url: '/api/HbtAuth/verify-captcha',
    method: 'post',
    data
  })
}

/**
 * 检查账号锁定状态
 * @param username 用户名
 */
export function checkAccountLockout(username: string): Promise<AxiosResponse<HbtApiResponse<LockoutStatus>>> {
  return request({
    url: `/api/HbtAuth/lockout/${username}`,
    method: 'get'
  })
}

/**
 * 获取剩余尝试次数
 * @param username 用户名
 */
export function getRemainingAttempts(username: string): Promise<AxiosResponse<HbtApiResponse<number>>> {
  return request({
    url: `/api/HbtAuth/attempts/${username}`,
    method: 'get'
  })
}

/**
 * 解锁用户
 * @param username 用户名
 */
export function unlockUser(username: string): Promise<AxiosResponse<HbtApiResponse<boolean>>> {
  return request({
    url: `/api/HbtAuth/unlock/${username}`,
    method: 'post'
  })
}

/**
 * 检查登录状态
 * @param data 登录参数
 */
export function checkLogin(data: LoginParams): Promise<AxiosResponse<HbtApiResponse<LoginCheckResultData>>> {
  // 构建符合HbtAuthDto的参数
  const loginDto = {
    tenantId: data.tenantId,
    userName: data.userName,
    password: data.password,
    captchaToken: data.captchaToken,
    captchaOffset: data.captchaOffset,
    ipAddress: data.ipAddress ?? window.location.hostname,
    userAgent: data.userAgent ?? navigator.userAgent,
    loginType: data.loginType ?? 0, // 默认使用密码登录
    loginSource: data.loginSource ?? 0, // 默认使用Web登录
    deviceInfo: data.deviceInfo
  }

  return request({
    url: '/api/HbtAuth/check-login',
    method: 'post',
    data: loginDto
  })
} 