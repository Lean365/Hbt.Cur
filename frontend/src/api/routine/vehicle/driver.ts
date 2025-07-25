import request from '@/utils/request'
import type {
  HbtDriver,
  HbtDriverQuery,
  HbtDriverCreate,
  HbtDriverUpdate,
  HbtDriverBatchDelete,
  HbtDriverImport,
  HbtDriverExport,
  HbtDriverTemplate,
  HbtDriverStatistics
} from '@/types/routine/vehicle/driver'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取驾驶员列表
 */
export function getDriverList(params: HbtDriverQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtDriver>>>({
    url: '/api/HbtDriver/list',
    method: 'get',
    params
  })
}

/**
 * 获取驾驶员详情
 */
export function getDriverById(id: number) {
  return request<HbtApiResponse<HbtDriver>>({
    url: `/api/HbtDriver/${id}`,
    method: 'get'
  })
}

/**
 * 创建驾驶员
 */
export function createDriver(data: HbtDriverCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtDriver',
    method: 'post',
    data
  })
}

/**
 * 更新驾驶员
 */
export function updateDriver(data: HbtDriverUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDriver',
    method: 'put',
    data
  })
}

/**
 * 删除驾驶员
 */
export function deleteDriver(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDriver/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除驾驶员
 */
export function batchDeleteDriver(data: HbtDriverBatchDelete) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDriver/batch',
    method: 'delete',
    data
  })
}

/**
 * 导入驾驶员数据
 */
export function importDriver(file: File, sheetName = '驾驶员信息') {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('sheetName', sheetName)

  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDriver/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出驾驶员数据
 */
export function exportDriver(params: HbtDriverQuery, sheetName = '驾驶员信息') {
  return request<Blob>({
    url: '/api/HbtDriver/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getDriverTemplate(sheetName = '驾驶员信息') {
  return request<Blob>({
    url: '/api/HbtDriver/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取驾驶员统计信息
 */
export function getDriverStatistics() {
  return request<HbtApiResponse<HbtDriverStatistics>>({
    url: '/api/HbtDriver/statistics',
    method: 'get'
  })
} 