import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 审计日志查询参数
 */
export interface AuditLogQuery extends HbtPageQuery {
  /** 用户名 */
  userName?: string
  /** IP地址 */
  ipaddr?: string
  /** 状态（0成功 1失败） */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
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
export interface AuditLogExport extends AuditLogQuery {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
} 