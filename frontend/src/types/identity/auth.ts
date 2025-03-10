import type { BaseEntity } from '@/types/base'

/**
 * 设备信息
 */
export interface DeviceInfo {
  deviceId: string
  deviceType: number
  deviceName?: string
  deviceModel?: string
  osType?: number
  osVersion?: string
  browserType?: number
  browserVersion?: string
  resolution?: string
  ipAddress?: string
  location?: string
  processorCores?: string
  platformVendor?: string
  hardwareConcurrency?: string
  systemLanguage?: string
  timeZone?: string
  screenColorDepth?: string
  deviceMemory?: string
  webGLRenderer?: string
  deviceFingerprint?: string
}

/**
 * 登录参数
 */
export interface LoginParams {
  tenantId: number
  userName: string
  password: string
  captchaToken?: string
  captchaOffset?: number
  code?: string
  uuid?: string
  deviceInfo?: DeviceInfo
  loginSource?: number
}

/**
 * 用户信息
 */
export interface UserInfo extends BaseEntity {
  userId: number
  userName: string
  nickName: string
  tenantId: number
  tenantName: string
  avatar?: string
  roles: string[]
  permissions: string[]
}

/**
 * 登录响应
 */
export interface LoginResult {
  accessToken: string
  refreshToken: string
  expiresIn: number
  userInfo: UserInfo
}

/**
 * 盐值响应
 */
export interface SaltResponse {
  salt: string
  iterations: number
}

/**
 * 验证码响应
 */
export interface CaptchaResponse {
  backgroundImage: string
  sliderImage: string
  token: string
  uuid?: string
}

/**
 * 验证码验证结果
 */
export interface CaptchaResult {
  success: boolean
  message?: string
} 