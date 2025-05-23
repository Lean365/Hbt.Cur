import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtSignalRDevice, HbtSignalREnvironment } from '@/types/common/signalR'
import type { HbtLoginDevLog } from './loginDevLog'
import type { HbtLoginEnvLog } from './loginEnvLog'

/** 设备信息 */
export interface HbtDeviceInfo {
  /** 租户ID */
  tenantId?: number
  /** 设备ID */
  deviceId?: string
  /** 设备令牌（后端生成的唯一标识） */
  deviceToken?: string
  /** 设备指纹（前端生成的特征值） */
  deviceFingerprint?: string
  /** 登录令牌 */
  accessToken?: string
  /** 首次登录时间 */
  firstLoginTime?: string
  /** 最后登录时间 */
  lastLoginTime?: string
  /** 设备类型 */
  deviceType: number
  /** 设备名称 */
  deviceName?: string
  /** 设备型号 */
  deviceModel?: string
  /** 操作系统类型 */
  osType: number
  /** 操作系统版本 */
  osVersion?: string
  /** 浏览器类型 */
  browserType: number
  /** 浏览器版本 */
  browserVersion?: string
  /** 分辨率 */
  resolution?: string
  /** IP地址 */
  ipAddress?: string
  /** 地理位置 */
  location?: string
  /** 处理器核心数 */
  processorCores?: string
  /** 平台供应商 */
  platformVendor?: string
  /** 硬件并发数 */
  hardwareConcurrency?: string
  /** 系统语言 */
  systemLanguage?: string
  /** 时区 */
  timeZone?: string
  /** 屏幕色深 */
  screenColorDepth?: string
  /** 设备内存 */
  deviceMemory?: string
  /** WebGL渲染器信息 */
  webGLRenderer?: string
}

/** 登录日志查询参数 */
export interface HbtLoginLogQuery extends HbtPagedQuery {
  /** 用户名 */
  userName?: string
  /** IP地址 */
  ipAddress?: string
  /** 是否成功（0失败 1成功） */
  loginSuccess?: number
  /** 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub） */
  loginType?: number
  /** 登录状态（0成功 1失败 2锁定 3离线） */
  loginStatus?: number
  /** 设备类型（0PC 1Android 2iOS 3MacOS 4Linux 5其他） */
  deviceType?: number
  /** 操作系统类型（0Windows 1Android 2iOS 3MacOS 4Linux 5其他） */
  osType?: number
  /** 浏览器类型（0Chrome 1Firefox 2Edge 3Safari 4IE 5其他） */
  browserType?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/** 登录日志数据 */
export interface HbtLoginLog extends HbtBaseEntity {
  /** 登录日志ID */
  loginLogId: number
  /** 日志级别 */
  logLevel: number
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId?: number
  /** 用户名 */
  userName: string
  /** IP地址 */
  ipAddress: string
  /** IP地理位置 */
  location: string
  /** 用户代理 */
  userAgent: string
  /** 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub） */
  loginType: number
  /** 登录状态（0成功 1失败 2锁定 3离线） */
  loginStatus: number
  /** 登录来源（0Web 1App 2小程序 3其他） */
  loginSource: number
  /** 是否成功（0失败 1成功） */
  loginSuccess: number
  /** 消息 */
  loginMessage?: string
  /** 登录时间 */
  loginTime: string
  /** 设备信息 */
  deviceInfo?: HbtSignalRDevice
  /** 环境信息 */
  environmentInfo?: HbtSignalREnvironment
  /** 设备ID */
  deviceId?: string
  /** 设备日志 */
  deviceLog?: HbtLoginDevLog
  /** 环境ID */
  environmentId?: string
  /** 环境日志 */
  envLog?: HbtLoginEnvLog
}

/** 登录日志创建参数 */
export interface HbtLoginLogCreate {
  /** 日志级别 */
  logLevel: number
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId?: number
  /** 用户名 */
  userName: string
  /** IP地址 */
  ipAddress: string
  /** IP地理位置 */
  location: string
  /** 用户代理 */
  userAgent: string
  /** 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub） */
  loginType: number
  /** 登录状态（0成功 1失败 2锁定 3离线） */
  loginStatus: number
  /** 登录来源（0Web 1App 2小程序 3其他） */
  loginSource: number
  /** 是否成功（0失败 1成功） */
  loginSuccess: number
  /** 消息 */
  loginMessage?: string
  /** 登录时间 */
  loginTime: string
  /** 设备信息 */
  deviceInfo?: HbtSignalRDevice
  /** 环境信息 */
  environmentInfo?: HbtSignalREnvironment
  /** 设备ID */
  deviceId?: string
  /** 环境ID */
  environmentId?: string
}

/** 登录日志更新参数 */
export interface HbtLoginLogUpdate extends HbtLoginLogCreate {
  /** ID */
  logId: number
}

/** 登录日志导出参数 */
export interface HbtLoginLogExport {
  /** 用户名 */
  userName: string
  /** IP地址 */
  ipAddress: string
  /** IP地理位置 */
  location: string
  /** 用户代理 */
  userAgent: string
  /** 登录类型 */
  loginType: number
  /** 登录状态 */
  loginStatus: number
  /** 登录来源 */
  loginSource: number
  /** 是否成功 */
  loginSuccess: number
  /** 消息 */
  loginMessage: string
  /** 登录时间 */
  loginTime: string
  /** 创建时间 */
  createTime: string
}

/** 登录日志分页结果 */
export type HbtLoginLogPageResult = HbtPagedResult<HbtLoginLog>

/** 登录日志DTO */
export interface HbtLoginLogDto {
  /** 登录日志ID */
  loginLogId: number
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId?: number
  /** 用户名 */
  userName: string
  /** IP地址 */
  ipAddress: string
  /** 用户代理 */
  userAgent: string
  /** 登录类型 */
  loginType: number
  /** 登录状态 */
  loginStatus: number
  /** 登录来源 */
  loginSource: number
  /** 是否成功 */
  loginSuccess: number
  /** 消息 */
  loginMessage: string
  /** 创建时间 */
  createTime: string
  /** 设备信息 */
  deviceInfo?: HbtSignalRDevice
  /** 设备扩展ID */
  deviceExtendId?: number
  /** 设备扩展 */
  deviceExtend?: HbtLoginDevLog
  /** 登录扩展ID */
  loginExtendId?: number
  /** 登录扩展 */
  loginExtend?: HbtLoginEnvLog
} 