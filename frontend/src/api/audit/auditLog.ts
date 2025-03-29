//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: auditLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 审计日志相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { HbtAuditLogDto, HbtAuditLogQueryDto } from '@/types/audit/auditLog'

/**
 * 获取审计日志列表
 * @param query 查询参数
 * @returns 审计日志列表
 */
export function getAuditLogs(query: HbtAuditLogQueryDto) {
  return request<HbtApiResponse<HbtPagedResult<HbtAuditLogDto>>>({
    url: '/api/HbtAuditLog',
    method: 'get',
    params: query
  })
}

/**
 * 获取审计日志详情
 * @param logId 日志ID
 * @returns 审计日志详情
 */
export function getAuditLog(logId: number) {
  return request<HbtApiResponse<HbtAuditLogDto>>({
    url: `/api/HbtAuditLog/${logId}`,
    method: 'get'
  })
}

/**
 * 导出审计日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportAuditLogs(query: HbtAuditLogQueryDto, sheetName: string = '审计日志') {
  return request({
    url: '/api/HbtAuditLog/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空审计日志
 * @returns 是否成功
 */
export function clearAuditLogs() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtAuditLog/clear',
    method: 'delete'
  })
} 