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
import type { 
  HbtLoginLog, 
  HbtLoginLogQuery, 
  HbtLoginLogCreate, 
  HbtLoginLogUpdate, 
  HbtLoginLogExport 
} from '@/types/audit/loginLog'

/**
 * 获取登录日志列表
 * @param params 查询参数
 * @returns 登录日志列表
 */
export function getLoginLogList(query: HbtLoginLogQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtLoginLog>>>({
    url: '/api/HbtLoginLog/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取登录日志详情
 * @param id 日志ID
 * @returns 登录日志详情
 */
export function getLoginLog(id: number) {
  return request<HbtApiResponse<HbtLoginLog>>({
    url: `/api/HbtLoginLog/${id}`,
    method: 'get'
  })
}

/**
 * 创建登录日志
 * @param data 创建参数
 * @returns 创建结果
 */
export function createLoginLog(data: HbtLoginLogCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtLoginLog',
    method: 'post',
    data
  })
}

/**
 * 更新登录日志
 * @param data 更新参数
 * @returns 更新结果
 */
export function updateLoginLog(data: HbtLoginLogUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginLog',
    method: 'put',
    data
  })
}

/**
 * 删除登录日志
 * @param id 日志ID
 * @returns 删除结果
 */
export function deleteLoginLog(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLoginLog/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除登录日志
 * @param ids 日志ID数组
 * @returns 删除结果
 */
export function batchDeleteLoginLog(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginLog/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 清空登录日志
 * @returns 清空结果
 */
export function clearLoginLog() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginLog/clear',
    method: 'delete'
  })
}

/**
 * 导入登录日志
 * @param file 上传的文件
 * @returns 导入结果
 */
export function importLoginLog(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtLoginLog/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出登录日志
 * @param query 查询参数
 * @returns Excel文件
 */
export function exportLoginLog(query: HbtLoginLogQuery) {
  return request<Blob>({
    url: '/api/HbtLoginLog/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 * @returns 导入模板文件
 */
export function getTemplate() {
  return request<Blob>({
    url: '/api/HbtLoginLog/template',
    method: 'get',
    responseType: 'blob'
  })
}