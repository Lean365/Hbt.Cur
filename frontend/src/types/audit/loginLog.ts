import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 登录日志查询参数
 */
export interface LoginLogQuery extends HbtPageQuery {
  /** IP地址 */
  ipaddr?: string
  /** 用户名 */
  userName?: string
  /** 状态（0成功 1失败） */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 登录日志对象
 */
export interface LoginLog extends HbtBaseEntity {
  /** 日志ID */
  infoId: number
  /** 用户名 */
  userName: string
  /** IP地址 */
  ipaddr: string
  /** 登录地点 */
  loginLocation: string
  /** 浏览器类型 */
  browser: string
  /** 操作系统 */
  os: string
  /** 状态（0成功 1失败） */
  status: number
  /** 提示信息 */
  msg: string
  /** 登录时间 */
  loginTime: string
}

/**
 * 登录日志导出参数
 */
export interface LoginLogExport extends LoginLogQuery {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
} 