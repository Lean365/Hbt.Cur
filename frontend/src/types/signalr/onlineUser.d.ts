import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '../common'

/**
 * 在线用户信息
 */
export interface HbtOnlineUser extends HbtBaseEntity {
  /** 用户ID */
  userId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 连接ID */
  connectionId: string
  /** 设备ID */
  deviceId: string
  /** 客户端IP */
  ipAddress: string
  /** 用户代理 */
  userAgent: string
  /** 最后活动时间 */
  lastActivity: string
  /** 在线状态（0-在线，1-离线） */
  onlineStatus: number
  /** 设备类型 */
  deviceType: number
  /** 设备名称 */
  deviceName: string
  /** 设备型号 */
  deviceModel: string
  /** 操作系统类型 */
  osType: number
  /** 系统版本 */
  osVersion: string
  /** 浏览器类型 */
  browserType: number
  /** 浏览器版本 */
  browserVersion: string
  /** 分辨率 */
  resolution: string
  /** 设备内存 */
  deviceMemory: string
  /** WebGL渲染器 */
  webGLRenderer: string
  /** 位置信息 */
  location: string
  /** 设备指纹 */
  deviceFingerprint: string
  /** 环境信息 */
  environment: {
    timezone?: string
    language?: string
  }
}

/** 在线用户查询参数 */
export interface HbtOnlineUserQueryParams extends HbtPagedQuery {
  /** 用户ID */
  userId?: number
  /** 客户端IP */
  clientIp?: string
}

/** 在线用户分页结果 */
export type HbtOnlineUserPageResult = HbtPagedResult<HbtOnlineUserDto> 