import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtPagedQuery as BasePagedQuery } from '../base'

/**
 * 数据库差异日志查询参数
 */
export interface HbtDbDiffLogQueryDto extends HbtPagedQuery {
  /** 表名 */
  tableName?: string
  /** 变更类型 */
  changeType?: string
  /** 列名 */
  columnName?: string
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 数据库差异日志数据
 */
export interface HbtDbDiffLogDto extends HbtBaseEntity {
  /** 日志级别 */
  logLevel: number
  /** 表名 */
  tableName: string
  /** 变更类型（新建表、新增列、修改列、删除列） */
  changeType: string
  /** 列名 */
  columnName: string
  /** 原数据类型 */
  oldDataType: string
  /** 新数据类型 */
  newDataType: string
  /** 原长度 */
  oldLength?: number
  /** 新长度 */
  newLength?: number
  /** 原是否允许为空 */
  oldIsNullable?: boolean
  /** 新是否允许为空 */
  newIsNullable?: boolean
  /** 变更描述 */
  changeDescription: string
  /** 执行的SQL语句 */
  executeSql: string
  /** SQL参数（JSON格式） */
  sqlParameters: string
  /** 租户ID */
  tenantId?: number
  /** 变更前的数据（JSON格式） */
  beforeData: string
  /** 变更后的数据（JSON格式） */
  afterData: string
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
export interface DbDiffLogExport extends HbtDbDiffLogQueryDto {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
}

/**
 * 数据库差异日志分页结果
 */
export type HbtDbDiffLogPageResult = HbtPagedResult<HbtDbDiffLogDto> 