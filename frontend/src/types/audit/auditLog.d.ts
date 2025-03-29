import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 审计日志查询参数 */
export interface HbtAuditLogQueryDto extends HbtPagedQuery {
  /** 用户名 */
  userName?: string
  /** 模块 */
  module?: string
  /** 操作 */
  operation?: string
  /** 日志级别 */
  logLevel?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/** 审计日志记录 */
export interface HbtAuditLogDto extends HbtBaseEntity {
  /** 日志级别 */
  logLevel: number
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId?: number
  /** 用户名 */
  userName: string
  /** 模块 */
  module: string
  /** 操作 */
  operation: string
  /** 方法 */
  method: string
  /** 参数 */
  parameters?: string
  /** 结果 */
  result?: string
  /** 耗时(毫秒) */
  elapsed: number
  /** IP地址 */
  ipAddress: string
  /** 用户代理 */
  userAgent: string
  /** 请求URL */
  requestUrl: string
  /** 请求方法 */
  requestMethod: string
}

/**
 * 审计日志对象
 */
export interface AuditLog extends HbtBaseEntity {
  /** 日志ID */
  id: number
  /** 用户ID */
  userId: number
  /** 用户名 */
  userName: string
  /** 操作类型 */
  operationType: string
  /** 操作名称 */
  operationName: string
  /** 操作路径 */
  operationPath: string
  /** 操作参数 */
  operationParam: string
  /** 操作结果 */
  operationResult: string
  /** 操作时间 */
  operationTime: string
  /** 操作IP */
  operationIp: string
  /** 操作地点 */
  operationLocation: string
  /** 状态（0成功 1失败） */
  status: number
  /** 错误消息 */
  errorMsg: string
  /** 创建时间 */
  createTime: string
}

/**
 * 审计日志导出参数
 */
export interface AuditLogExport extends HbtAuditLogQueryDto {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
}

/** 审计日志分页结果 */
export type HbtAuditLogPageResult = HbtPagedResult<HbtAuditLogDto> 