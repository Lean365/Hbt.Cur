//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: operLog.d.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 操作日志类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 操作日志查询参数
 */
export interface HbtOperLogQueryDto extends HbtPagedQuery {
  /** 用户名 */
  userName?: string
  /** 操作类型 */
  operationType?: string
  /** 表名 */
  tableName?: string
  /** IP地址 */
  ipAddress?: string
  /** 操作状态 */
  status?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 操作日志记录
 */
export interface HbtOperLogDto extends HbtBaseEntity {
  /** 日志级别 */
  logLevel: number
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId?: number
  /** 用户名 */
  userName: string
  /** 操作类型 */
  operationType: string
  /** 表名 */
  tableName: string
  /** 业务主键 */
  businessKey: string
  /** 请求方法 */
  requestMethod: string
  /** 请求参数 */
  requestParam?: string
  /** IP地址 */
  ipAddress: string
  /** 操作地点 */
  location?: string
  /** 操作状态（0正常 1异常） */
  status: number
  /** 错误消息 */
  errorMsg?: string
}

/**
 * 操作日志查询参数
 */
export interface OperLogQuery extends HbtPagedQuery {
  /** 模块标题 */
  title?: string
  /** 操作人员 */
  operName?: string
  /** 业务类型（0其它 1新增 2修改 3删除） */
  businessType?: number
  /** 状态（0正常 1异常） */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 操作日志对象
 */
export interface OperLog extends HbtBaseEntity {
  /** 日志ID */
  operId: number
  /** 模块标题 */
  title: string
  /** 业务类型（0其它 1新增 2修改 3删除） */
  businessType: number
  /** 方法名称 */
  method: string
  /** 请求方式 */
  requestMethod: string
  /** 操作类别（0其它 1后台用户 2手机端用户） */
  operatorType: number
  /** 操作人员 */
  operName: string
  /** 部门名称 */
  deptName: string
  /** 请求URL */
  operUrl: string
  /** 主机地址 */
  operIp: string
  /** 操作地点 */
  operLocation: string
  /** 请求参数 */
  operParam: string
  /** 返回参数 */
  jsonResult: string
  /** 状态（0正常 1异常） */
  status: number
  /** 错误消息 */
  errorMsg: string
  /** 操作时间 */
  operTime: string
}

/**
 * 操作日志导出参数
 */
export interface HbtOperLogExportDto extends HbtOperLogQueryDto {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
}

/**
 * 操作日志分页结果
 */
export type HbtOperLogPageResult = HbtPagedResult<HbtOperLogDto> 