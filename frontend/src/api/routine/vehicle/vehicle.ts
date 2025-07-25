import request from '@/utils/request'
import type { 
  HbtVehicle, 
  HbtVehicleQuery, 
  HbtVehicleCreate, 
  HbtVehicleUpdate,
  HbtVehicleBatchDelete,
  HbtVehicleImport,
  HbtVehicleExport,
  HbtVehicleTemplate,
  HbtVehicleStatistics
} from '@/types/routine/vehicle/vehicle'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取用车列表
 */
export function getVehicleList(params: HbtVehicleQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtVehicle>>>({
    url: '/api/HbtVehicle/list',
    method: 'get',
    params
  })
}

/**
 * 获取用车详情
 */
export function getVehicleById(id: number) {
  return request<HbtApiResponse<HbtVehicle>>({
    url: `/api/HbtVehicle/${id}`,
    method: 'get'
  })
}

/**
 * 创建用车
 */
export function createVehicle(data: HbtVehicleCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtVehicle',
    method: 'post',
    data
  })
}

/**
 * 更新用车
 */
export function updateVehicle(data: HbtVehicleUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtVehicle',
    method: 'put',
    data
  })
}

/**
 * 删除用车
 */
export function deleteVehicle(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtVehicle/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除用车
 */
export function batchDeleteVehicle(data: HbtVehicleBatchDelete) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtVehicle/batch',
    method: 'delete',
    data
  })
}

/**
 * 导入用车数据
 */
export function importVehicle(file: File, sheetName = '用车信息') {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('sheetName', sheetName)
  
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtVehicle/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出用车数据
 */
export function exportVehicle(params: HbtVehicleQuery, sheetName = '用车信息') {
  return request<Blob>({
    url: '/api/HbtVehicle/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getVehicleTemplate(sheetName = '用车信息') {
  return request<Blob>({
    url: '/api/HbtVehicle/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取用车统计信息
 */
export function getVehicleStatistics() {
  return request<HbtApiResponse<HbtVehicleStatistics>>({
    url: '/api/HbtVehicle/statistics',
    method: 'get'
  })
} 