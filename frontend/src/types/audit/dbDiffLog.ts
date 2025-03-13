import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 数据库差异日志查询参数
 */
export interface DbDiffLogQuery extends HbtPageQuery {
  /** 表名 */
  tableName?: string
  /** 操作类型 */
  operationType?: string
  /** 操作人 */
  operationUser?: string
  /** 状态（0成功 1失败） */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 数据库差异日志对象
 */
export interface DbDiffLog extends HbtBaseEntity {
  /** 日志ID */
  id: number
  /** 表名 */
  tableName: string
  /** 操作类型 */
  operationType: string
  /** 操作人 */
  operationUser: string
  /** 修改前数据 */
  beforeData: string
  /** 修改后数据 */
  afterData: string
  /** 差异结果 */
  diffResult: string
  /** 状态（0成功 1失败） */
  status: number
  /** 错误消息 */
  errorMsg: string
  /** 创建者 */
  createBy: string
  /** 创建时间 */
  createTime: string
  /** 更新者 */
  updateBy: string
  /** 更新时间 */
  updateTime: string
  /** 备注 */
  remark: string
}

/**
 * 数据库差异日志导出参数
 */
export interface DbDiffLogExport extends DbDiffLogQuery {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
} 