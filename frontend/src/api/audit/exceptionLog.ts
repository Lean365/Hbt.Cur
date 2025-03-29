//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: exceptionLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 异常日志相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { HbtExceptionLogDto, HbtExceptionLogQueryDto } from '@/types/audit/exceptionLog'

/**
 * 获取异常日志列表
 * @param query 查询参数
 * @returns 异常日志列表
 */
export function getExceptionLogs(query: HbtExceptionLogQueryDto) {
  return request<HbtApiResponse<HbtPagedResult<HbtExceptionLogDto>>>({
    url: '/api/HbtExceptionLog',
    method: 'get',
    params: query
  })
}

/**
 * 获取异常日志详情
 * @param logId 日志ID
 * @returns 异常日志详情
 */
export function getExceptionLog(logId: number) {
  return request<HbtApiResponse<HbtExceptionLogDto>>({
    url: `/api/HbtExceptionLog/${logId}`,
    method: 'get'
  })
}

/**
 * 导出异常日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportExceptionLogs(query: HbtExceptionLogQueryDto, sheetName: string = '异常日志') {
  return request({
    url: '/api/HbtExceptionLog/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空异常日志
 * @returns 是否成功
 */
export function clearExceptionLogs() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtExceptionLog/clear',
    method: 'delete'
  })
}
