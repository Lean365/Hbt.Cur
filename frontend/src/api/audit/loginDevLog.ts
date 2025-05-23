import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  HbtLoginDevLog, 
  HbtLoginDevLogQuery, 
  HbtLoginDevLogCreate, 
  HbtLoginDevLogUpdate, 
  HbtLoginDevLogExport 
} from '@/types/audit/loginDevLog'

// 获取设备日志分页列表
export function getLoginDevLogList(query: HbtLoginDevLogQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtLoginDevLog>>>({
    url: '/api/HbtLoginDevLog/list',
    method: 'get',
    params: query
  })
}

// 获取设备日志详情
export function getLoginDevLog(id: number) {
  return request<HbtApiResponse<HbtLoginDevLog>>({
    url: `/api/HbtLoginDevLog/${id}`,
    method: 'get'
  })
}

// 创建设备日志
export function createLoginDevLog(data: HbtLoginDevLogCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtLoginDevLog',
    method: 'post',
    data
  })
}

// 更新设备日志
export function updateLoginDevLog(data: HbtLoginDevLogUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginDevLog',
    method: 'put',
    data
  })
}

// 删除设备日志
export function deleteLoginDevLog(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLoginDevLog/${id}`,
    method: 'delete'
  })
}

// 批量删除设备日志
export function batchDeleteLoginDevLog(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginDevLog/batch',
    method: 'delete',
    data: ids
  })
}

// 导入设备日志
export function importLoginDevLog(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtLoginDevLog/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出设备日志
export function exportLoginDevLog(query: HbtLoginDevLogQuery) {
  return request<Blob>({
    url: '/api/HbtLoginDevLog/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getTemplate() {
  return request<Blob>({
    url: '/api/HbtLoginDevLog/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 清空设备日志
export function clearLoginDevLog() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLoginDevLog/clear',
    method: 'delete'
  })
}

// 获取当前设备信息
export function getCurrentLoginDevLog() {
  return request<HbtApiResponse<HbtLoginDevLog>>({
    url: '/api/HbtLoginDevLog/current',
    method: 'get'
  })
}

// 更新设备离线信息
export function updateLoginDevLogOffline(userId: number, deviceId: string) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLoginDevLog/${userId}/${deviceId}/offline`,
    method: 'put'
  })
}