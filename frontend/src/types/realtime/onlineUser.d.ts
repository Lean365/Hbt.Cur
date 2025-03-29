import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '../common'

/** 在线用户DTO */
export interface HbtOnlineUserDto extends HbtBaseEntity {
  /** 用户ID */
  userId: number
  /** 连接ID */
  connectionId: string
  /** 客户端IP */
  clientIp: string
  /** 用户代理 */
  userAgent: string
  /** 最后活动时间 */
  lastActivity: string
}

/** 在线用户查询参数 */
export interface HbtOnlineUserQueryParams extends HbtPagedQuery {
  /** 租户ID */
  tenantId?: number
  /** 用户ID */
  userId?: number
  /** 客户端IP */
  clientIp?: string
}

/** 在线用户分页结果 */
export type HbtOnlineUserPageResult = HbtPagedResult<HbtOnlineUserDto> 