import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  HbtLoginEnvLog, 
  HbtLoginEnvLogQuery, 
  HbtLoginEnvLogCreate, 
  HbtLoginEnvLogUpdate, 
  HbtLoginEnvLogExport 
} from '@/types/audit/loginEnvLog'

// 获取环境日志分页列表
export function getLoginEnvLogList(query: HbtLoginEnvLogQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtLoginEnvLog>>>({
    url: '/api/HbtLoginEnvLog/list',
    method: 'get',
    params: query
  })
}

// 获取环境日志详情
export function getLoginEnvLog(id: number) {
  return request<HbtApiResponse<HbtLoginEnvLog>>({
    url: `/api/HbtLoginEnvLog/${id}`,
    method: 'get'
  })
}

// 创建环境日志
export function createLoginEnvLog(data: HbtLoginEnvLogCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtLoginEnvLog',
    method: 'post',
    data
  })
}

// 更新环境日志
export function updateLoginEnvLog(data: HbtLoginEnvLogUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginEnvLog',
    method: 'put',
    data
  })
}

// 删除环境日志
export function deleteLoginEnvLog(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLoginEnvLog/${id}`,
    method: 'delete'
  })
}

// 批量删除环境日志
export function batchDeleteLoginEnvLog(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginEnvLog/batch',
    method: 'delete',
    data: ids
  })
}

// 导入环境日志
export function importLoginEnvLog(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtLoginEnvLog/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出环境日志
export function exportLoginEnvLog(query: HbtLoginEnvLogQuery) {
  return request<Blob>({
    url: '/api/HbtLoginEnvLog/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getTemplate() {
  return request<Blob>({
    url: '/api/HbtLoginEnvLog/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 清空环境日志
export function clearLoginEnvLog() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginEnvLog/clear',
    method: 'delete'
  })
}

// 更新环境日志离线信息
export function updateLoginEnvLogOffline(userId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLoginEnvLog/${userId}/offline`,
    method: 'put'
  })
} 