import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 设备信息
 */
export interface DeviceInfo {
  /** 设备ID */
  deviceId: string;
  /** 设备类型（0=未知 1=PC 2=手机 3=平板） */
  deviceType: number;
  /** 设备名称 */
  deviceName?: string;
  /** 设备型号 */
  deviceModel?: string;
  /** 操作系统类型（0=未知 1=Windows 2=MacOS 3=Linux 4=Android 5=iOS） */
  osType?: number;
  /** 操作系统版本 */
  osVersion?: string;
  /** 浏览器类型（0=未知 1=Chrome 2=Firefox 3=Safari 4=Edge） */
  browserType?: number;
  /** 浏览器版本 */
  browserVersion?: string;
  /** 屏幕分辨率 */
  resolution?: string;
  /** IP地址 */
  ipAddress?: string;
  /** 地理位置 */
  location?: string;
  /** CPU核心数 */
  processorCores?: string;
  /** 平台供应商 */
  platformVendor?: string;
  /** 硬件并发数 */
  hardwareConcurrency?: string;
  /** 系统语言 */
  systemLanguage?: string;
  /** 时区 */
  timeZone?: string;
  /** 屏幕颜色深度 */
  screenColorDepth?: string;
  /** 设备内存 */
  deviceMemory?: string;
  /** WebGL渲染器 */
  webGLRenderer?: string;
  /** 设备指纹 */
  deviceFingerprint?: string;
  /** 设备令牌（由后端生成的唯一标识） */
  deviceToken?: string;
  /** 登录令牌 */
  accessToken?: string;
  /** 首次登录时间 */
  firstLoginTime?: string;
  /** 最后登录时间 */
  lastLoginTime?: string;
}

/**
 * 登录参数
 */
export interface LoginParams {
  /** 租户ID */
  tenantId: number;  // 对应C#的long
  /** 用户名 */
  userName: string;
  /** 密码 */
  password: string;
  /** 验证码Token */
  captchaToken?: string;
  /** 验证码偏移量 */
  captchaOffset: number;  // 对应C#的int
  /** IP地址 */
  ipAddress: string;
  /** 用户代理 */
  userAgent: string;
  /** 登录类型（0=密码 1=短信 2=邮箱 3=微信 4=QQ 5=GitHub） */
  loginType: number;  // 对应C#的HbtLoginType
  /** 登录来源（0=Web 1=App 2=小程序 3=其他） */
  loginSource: number;  // 对应C#的int
  /** 设备信息 */
  deviceInfo?: DeviceInfo;  // 对应C#的HbtSignalRDevice
}

/**
 * 设备信息
 */
export interface HbtSignalRDevice {
  /** 设备ID */
  deviceId: string;
  /** 设备类型 */
  deviceType: number;
  /** 设备名称 */
  deviceName?: string;
  /** 设备型号 */
  deviceModel?: string;
  /** 操作系统类型 */
  osType?: number;
  /** 操作系统版本 */
  osVersion?: string;
  /** 浏览器类型 */
  browserType?: number;
  /** 浏览器版本 */
  browserVersion?: string;
  /** 屏幕分辨率 */
  resolution?: string;
  /** IP地址 */
  ipAddress?: string;
  /** 地理位置 */
  location?: string;
  /** CPU核心数 */
  processorCores?: string;
  /** 平台供应商 */
  platformVendor?: string;
  /** 硬件并发数 */
  hardwareConcurrency?: string;
  /** 系统语言 */
  systemLanguage?: string;
  /** 时区 */
  timeZone?: string;
  /** 屏幕颜色深度 */
  screenColorDepth?: string;
  /** 设备内存 */
  deviceMemory?: string;
  /** WebGL渲染器 */
  webGLRenderer?: string;
  /** 设备指纹 */
  deviceFingerprint?: string;
  /** 设备令牌 */
  deviceToken?: string;
  /** 登录令牌 */
  accessToken?: string;
  /** 首次登录时间 */
  firstLoginTime?: string;
  /** 最后登录时间 */
  lastLoginTime?: string;
}

/**
 * 用户信息
 */
export interface UserInfo {
  userId: number
  userName: string
  nickName: string
  englishName: string
  userType: number
  tenantId: number
  tenantName: string
  roles: string[]
  permissions: string[]
}

/**
 * 登录响应
 */
export interface LoginResult {
  accessToken: string;
  userInfo: UserInfoResponse;
}

/**
 * 盐值响应
 */
export interface SaltResponse {
  /** 盐值 */
  salt: string;
  /** 迭代次数 */
  iterations: number;
}

/**
 * 验证码响应
 */
export interface CaptchaResponse {
  /** 背景图片 */
  backgroundImage: string;
  /** 滑块图片 */
  sliderImage: string;
  /** 令牌 */
  token: string;
  /** UUID */
  uuid?: string;
}

/**
 * 验证码验证结果
 */
export interface CaptchaResult {
  /** 是否成功 */
  success: boolean;
  /** 消息 */
  message?: string;
}

/**
 * 账号锁定状态
 */
export type LockoutStatus = number

/**
 * 登录策略配置
 */
export const LOGIN_POLICY = {
  ADMIN: {
    MAX_ATTEMPTS: 3,              // 管理员最大尝试次数（3次后第4次如果还错误就锁定）
    LOCKOUT_MINUTES: 30          // 管理员锁定时间（分钟）
  },
  USER: {
    MAX_ATTEMPTS: 4,             // 普通用户最大尝试次数（4次后第5次如果还错误就禁用）
    LOCKOUT_DAYS: 999           // 普通用户禁用时间（天）
  },
  CAPTCHA: {
    REQUIRED_ATTEMPTS: 3,        // 错误3次后需要验证码
    REQUIRED_MINUTES: 5          // 5分钟内重复登录需要验证码
  }
} as const

/**
 * 登录相关的本地存储键
 */
export const LOGIN_STORAGE_KEYS = {
  LAST_LOGIN_TIME: 'lastLoginTime',
  FAILED_ATTEMPTS: 'failedAttempts',
  USERNAME: 'lastUsername'
} as const

/**
 * 特殊用户名
 */
export const SPECIAL_USERS = {
  ADMIN: 'admin'
} as const

export interface LoginCheckResult {
  existingSession: boolean
  deviceInfo?: string
  canLogin: boolean
  existingDeviceInfo?: string
}