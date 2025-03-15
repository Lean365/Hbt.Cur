import type { HbtBaseEntity } from '@/types/common'

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
}

/**
 * 登录参数
 */
export interface LoginParams {
  /** 租户ID */
  tenantId: number;
  /** 用户名 */
  userName: string;
  /** 密码 */
  password: string;
  /** 验证码令牌 */
  captchaToken?: string;
  /** 验证码偏移量 */
  captchaOffset?: number;
  /** 验证码 */
  code?: string;
  /** 验证码UUID */
  uuid?: string;
  /** 设备信息 */
  deviceInfo?: DeviceInfo;
  /** 登录来源（0=Web 1=App 2=小程序 3=其他） */
  loginSource?: number;
}

/**
 * 用户信息
 */
export interface UserInfo extends HbtBaseEntity {
  /** 用户ID */
  userId: number;
  /** 用户名 */
  userName: string;
  /** 昵称 */
  nickName: string;
  /** 英文名 */
  englishName?: string;
  /** 租户ID */
  tenantId: number;
  /** 租户名称 */
  tenantName: string;
  /** 头像 */
  avatar?: string;
  /** 角色列表 */
  roles: string[];
  /** 权限列表 */
  permissions: string[];
}

/**
 * 登录响应
 */
export interface LoginResult {
  /** 访问令牌 */
  accessToken: string;
  /** 刷新令牌 */
  refreshToken: string;
  /** 过期时间（秒） */
  expiresIn: number;
  /** 用户信息 */
  userInfo: UserInfo;
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