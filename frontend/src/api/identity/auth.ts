import request from '@/utils/request'
import type { AxiosResponse } from 'axios'
import type { HbtApiResponse } from '@/types/common'
import type { 
  LoginParams, 
  LoginResultData, 
  UserInfo, 
  SaltResponse, 
  CaptchaResponse, 
  CaptchaResult,
  LockoutStatus,
  LoginCheckResult,
  LoginCheckResultData,
  HbtSignalRDevice,
  HbtSignalREnvironment
} from '@/types/identity/auth'
import type { UserInfoResponse } from '@/stores/user'
import { getToken } from '@/utils/auth'

/**
 * 登录
 * @param data 登录参数
 */
export function login(data: LoginParams): Promise<AxiosResponse<HbtApiResponse<LoginResultData>>> {
  console.log('[Auth] 开始登录:', {
    用户名: data.userName,
    登录类型: data.loginType,
    登录来源: data.loginSource,
    设备信息: data.deviceInfo,
    环境信息: data.environmentInfo
  })
  return request<HbtApiResponse<LoginResultData>>({
    url: '/api/HbtAuth/login',
    method: 'post',
    data,
    headers: {
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache'
    }
  }).then(response => {
    console.log('[Auth] 登录响应:', {
      状态码: response.status,
      状态文本: response.statusText,
      响应头: response.headers,
      响应数据: response.data
    })
    return response
  }).catch(error => {
    console.error('[Auth] 登录失败:', {
      错误信息: error.message,
      错误代码: error.code,
      请求配置: error.config,
      响应数据: error.response?.data,
      响应状态: error.response?.status,
      响应头: error.response?.headers
    })
    throw error
  })
}

/**
 * 获取盐值
 * @param username 用户名
 */
export function getSalt(username: string): Promise<AxiosResponse<HbtApiResponse<SaltResponse>>> {
  console.log('[Auth] 开始获取盐值:', username)
  return request<HbtApiResponse<SaltResponse>>({
    url: '/api/HbtAuth/salt',
    method: 'get',
    params: { username },
    headers: {
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache'
    },
    validateStatus: function (status) {
      console.log('[Auth] 盐值请求状态码:', status)
      return status >= 200 && status < 300
    }
  }).then(response => {
    console.log('[Auth] 获取盐值响应:', {
      状态码: response.status,
      状态文本: response.statusText,
      响应头: response.headers,
      响应数据: response.data
    })
    return response
  }).catch(error => {
    console.error('[Auth] 获取盐值失败:', {
      错误信息: error.message,
      错误代码: error.code,
      请求配置: error.config,
      响应数据: error.response?.data,
      响应状态: error.response?.status,
      响应头: error.response?.headers
    })
    throw error
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
    data: refreshToken
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
export function checkAccountLockout(userName: string): Promise<AxiosResponse<HbtApiResponse<LockoutStatus>>> {
  console.log('[Auth] 检查账号锁定状态:', userName)
  return request<HbtApiResponse<LockoutStatus>>({
    url: `/api/HbtAuth/lockout/${userName}`,
    method: 'get',
    headers: {
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache'
    }
  }).then(response => {
    console.log('[Auth] 账号锁定状态响应:', {
      状态码: response.status,
      状态文本: response.statusText,
      响应头: response.headers,
      响应数据: response.data
    })
    return response
  }).catch(error => {
    console.error('[Auth] 检查账号锁定状态失败:', {
      错误信息: error.message,
      错误代码: error.code,
      请求配置: error.config,
      响应数据: error.response?.data,
      响应状态: error.response?.status,
      响应头: error.response?.headers
    })
    throw error
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

/**
 * 刷新用户令牌
 * @param refreshToken 刷新令牌
 */
export function refreshUserToken(refreshToken: string): Promise<AxiosResponse<HbtApiResponse<LoginResultData>>> {
  return request({
    url: '/api/HbtAuth/refresh-token',
    method: 'post',
    data: refreshToken
  })
} 