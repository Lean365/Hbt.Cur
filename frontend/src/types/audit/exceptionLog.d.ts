//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: exceptionLog.d.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 异常日志类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'


/**
 * 异常日志查询参数
 */
export interface HbtExceptionLogQueryDto extends HbtPagedQuery {
  /** 日志级别 */
  logLevel?: number
  /** 用户名 */
  userName?: string
  /** 方法 */
  method?: string
  /** 异常类型 */
  exceptionType?: string
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 异常日志数据
 */
export interface HbtExceptionLogDto extends HbtBaseEntity {
  /** 日志级别 */
  logLevel: number
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId?: number
  /** 用户名 */
  userName: string
  /** 方法 */
  method: string
  /** 参数 */
  parameters?: string
  /** 异常类型 */
  exceptionType: string
  /** 异常消息 */
  exceptionMessage: string
  /** 堆栈跟踪 */
  stackTrace?: string
  /** IP地址 */
  ipAddress: string
  /** 用户代理 */
  userAgent: string
}

/**
 * 异常日志导出参数
 */
export interface HbtExceptionLogExportDto extends HbtExceptionLogQueryDto {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
}

/**
 * 异常日志分页结果
 */
export type HbtExceptionLogPageResult = HbtPagedResult<HbtExceptionLogDto> 