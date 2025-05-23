import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult, HbtApiResponse } from '@/types/common'

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
  captchaToken: string;
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
  deviceInfo: HbtSignalRDevice;
  /** 环境信息 */
  environmentInfo: HbtSignalREnvironment;
}

/**
 * 用户信息DTO
 */
export interface UserInfo {
  /** 用户ID */
  userId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 英文名 */
  englishName: string
  /** 用户类型 */
  userType: number
  /** 租户ID */
  tenantId: number
  /** 租户名称 */
  tenantName: string
  /** 角色列表 */
  roles: string[]
  /** 权限列表 */
  permissions: string[]
  /** 头像 */
  avatar?: string
}

/**
 * 登录响应数据
 */
export interface LoginResultData {
  /** 访问令牌 */
  accessToken: string
  /** 刷新令牌 */
  refreshToken: string
  /** 过期时间(秒) */
  expiresIn: number
  /** 用户信息 */
  userInfo: UserInfo
}

/**
 * 登录响应
 */
export type LoginResult = HbtApiResponse<LoginResultData>

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

/**
 * 登录检查结果
 */
export interface LoginCheckResultData {
  /** 是否存在会话 */
  existingSession: boolean
  /** 设备信息 */
  deviceInfo?: string
  /** 是否可以登录 */
  canLogin: boolean
  /** 现有设备信息 */
  existingDeviceInfo?: string
}

/**
 * 登录检查响应
 */
export interface LoginCheckResult {
  /** 响应代码 */
  code: number
  /** 响应消息 */
  msg: string
  /** 响应数据 */
  data: LoginCheckResultData
}

/**
 * 设备信息
 */
export interface HbtSignalRDevice {
  /** 租户ID */
  tenantId: number;
  /** 用户ID */
  userId: number;
  /** 用户组ID */
  groupId: number;
  /** 连接ID */
  connectionId?: string;
  /** 设备ID */
  deviceId?: string;
  /** 客户端IP */
  ipAddress?: string;
  /** 用户代理 */
  userAgent?: string;
  /** 最后活动时间 */
  lastActivity: string;
  /** 最后心跳时间 */
  lastHeartbeat: string;
  /** 在线状态（0-在线，1-离线） */
  onlineStatus: number;
  /** 设备类型 */
  deviceType: number;
  /** 设备令牌 */
  deviceToken?: string;
  /** 设备名称 */
  deviceName?: string;
  /** 设备型号 */
  deviceModel?: string;
  /** 操作系统类型 */
  osType?: number;
  /** 系统版本 */
  osVersion?: string;
  /** 浏览器类型 */
  browserType?: number;
  /** 浏览器版本 */
  browserVersion?: string;
  /** 分辨率 */
  resolution?: string;
  /** 设备内存 */
  deviceMemory?: string;
  /** WebGL渲染器 */
  webGLRenderer?: string;
  /** 位置信息 */
  location?: string;
  /** 设备指纹 */
  deviceFingerprint?: string;
}

/**
 * 环境信息
 */
export interface HbtSignalREnvironment {

    /** 租户ID */
    tenantId: number;
    /** 用户ID */
    userId: number;

    /** 设备ID */
    deviceId: number;
  /** 环境ID */
  environmentId?: string;

  /** 登录类型 */
  loginType: number;

  /** 登录来源 */
  loginSource: number;

  /** 登录状态 */
  loginStatus: number;

  /** 登录提供者 */
  loginProvider: number;

  /** 登录提供者密钥 */
  providerKey: string;  

  /** 登录提供者显示名称 */
  providerDisplayName: string;  
  /** 网络类型 */
  networkType?: number;

  /** 时区 */
  timeZone?: string;

  /** 语言 */
  language?: string;

  /** 是否VPN（0-否，1-是） */
  isVpn?: number;
  /** 是否代理（0-否，1-是） */
  isProxy?: number;

  /** 状态 */
  status?: number;

  /** 首次登录时间 */
  firstLoginTime?: string;

  /** 首次登录IP */
  firstLoginIp?: string;

  /** 首次登录位置 */
  firstLoginLocation  ?: string;

  /** 首次登录设备ID */
  firstLoginDeviceId?: string;

  /** 首次登录设备类型 */
  firstLoginDeviceType?: number;

  /** 首次登录浏览器 */
  firstLoginBrowser?: number;

  /** 首次登录操作系统 */
  firstLoginOs?: number;

  /** 最后登录时间 */
  lastLoginTime?: string;

  /** 最后登录IP */
  lastLoginIp?: string;

  /** 最后登录位置 */
  lastLoginLocation?: string;

  /** 最后登录设备ID */
  lastLoginDeviceId?: string;

  /** 最后登录设备类型 */
  lastLoginDeviceType?: number;

  /** 最后登录浏览器 */
  lastLoginBrowser?: number;

  /** 最后登录操作系统 */
  lastLoginOs?: number;

  /** 最后离线时间 */
  lastOfflineTime?: string;

  /** 今日在线时长 */
  todayOnlinePeriods?: number;

  /** 总登录次数 */
  loginCount?: number;
  
  /** 连续登录天数 */
  continuousLoginDays?: number;  
  
    /** 设备指纹 */
    environmentFingerprint?: string;
}

export { UserInfo, LoginResult, LoginParams }