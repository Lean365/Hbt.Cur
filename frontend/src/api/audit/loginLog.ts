//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: loginLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 登录日志相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { HbtLoginLogDto, HbtLoginLogQueryDto } from '@/types/audit/loginLog'

/**
 * 获取登录日志列表
 * @param params 查询参数
 * @returns 登录日志列表
 */
export const getLoginLogs = (params: HbtLoginLogQueryDto) => {
  return request<HbtApiResponse<HbtPagedResult<HbtLoginLogDto>>>({
    url: '/api/HbtLoginLog/list',
    method: 'get',
    params
  })
}

/**
 * 获取登录日志详情
 * @param logId 日志ID
 * @returns 登录日志详情
 */
export function getLoginLog(logId: number) {
  return request<HbtApiResponse<HbtLoginLogDto>>({
    url: `/api/HbtLoginLog/${logId}`,
    method: 'get'
  })
}

/**
 * 导出登录日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportLoginLogs(query: HbtLoginLogQueryDto, sheetName: string = '登录日志') {
  return request({
    url: '/api/HbtLoginLog/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空登录日志
 * @returns 是否成功
 */
export function clearLoginLogs() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginLog/clear',
    method: 'delete'
  })
}