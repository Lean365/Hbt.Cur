import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 异常日志查询参数
 */
export interface ExceptionLogQuery extends HbtPageQuery {
  /** 用户名 */
  userName?: string
  /** 服务名称 */
  serviceName?: string
  /** 状态（0成功 1失败） */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 异常日志对象
 */
export interface ExceptionLog extends HbtBaseEntity {
  /** 日志ID */
  id: number
  /** 用户名 */
  userName: string
  /** 服务名称 */
  serviceName: string
  /** 方法名称 */
  methodName: string
  /** 请求方式 */
  requestMethod: string
  /** 请求URL */
  requestUrl: string
  /** 请求参数 */
  requestParams: string
  /** 堆栈信息 */
  stackTrace: string
  /** 异常类型 */
  exceptionType: string
  /** 异常信息 */
  exceptionMessage: string
  /** 状态（0成功 1失败） */
  status: number
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
 * 异常日志导出参数
 */
export interface ExceptionLogExport extends ExceptionLogQuery {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
} 