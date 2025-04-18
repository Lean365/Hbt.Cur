//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: quartzLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 任务日志相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { HbtQuartzLogDto, HbtQuartzLogQueryDto } from '@/types/audit/quartzLog'

/**
 * 获取任务日志列表
 * @param query 查询参数
 * @returns 任务日志列表
 */
export function getQuartzLogs(query: HbtQuartzLogQueryDto) {
  return request<HbtApiResponse<HbtPagedResult<HbtQuartzLogDto>>>({
    url: '/api/HbtQuartzTask/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取任务日志详情
 * @param logId 日志ID
 * @returns 任务日志详情
 */
export function getQuartzLog(logId: number) {
  return request<HbtApiResponse<HbtQuartzLogDto>>({
    url: `/api/HbtQuartzTask/${logId}`,
    method: 'get'
  })
}

/**
 * 导出任务日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportQuartzLogs(query: HbtQuartzLogQueryDto, sheetName: string = '任务日志') {
  return request({
    url: '/api/HbtQuartzTask/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空任务日志
 * @returns 是否成功
 */
export function clearQuartzLogs() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtQuartzTask/clear',
    method: 'delete'
  })
} 