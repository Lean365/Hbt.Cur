//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: exceptionLog.d.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 异常日志类型定义（字段与后端实体完全一致）
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 异常日志记录
 */
export interface HbtExceptionLogDto extends HbtBaseEntity {
  exceptionLogId: number
  logLevel: number
  userId: number
  tenantId?: number
  userName: string
  method: string
  parameters?: string
  exceptionType: string
  exceptionMessage: string
  stackTrace?: string
  ipAddress: string
  userAgent: string
}

/**
 * 异常日志查询参数
 */
export interface HbtExceptionLogQueryDto extends HbtPagedQuery {
  userName?: string
  method?: string
  exceptionType?: string
  startTime?: string
  endTime?: string
}

/**
 * 异常日志导出参数
 */
export interface HbtExceptionLogExportDto {
  exceptionLogId: number
  logLevel: number
  userId: number
  tenantId?: number
  userName: string
  method: string
  parameters?: string
  exceptionType: string
  exceptionMessage: string
  stackTrace?: string
  ipAddress: string
  userAgent: string
  createTime: string
}

/**
 * 异常日志分页结果
 */
export type HbtExceptionLogPageResult = HbtPagedResult<HbtExceptionLogDto> 