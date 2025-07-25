import request from '@/utils/request'
import type { HbtRegulation, HbtRegulationQuery, HbtRegulationCreate, HbtRegulationUpdate } from '@/types/routine/document/regulation'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取内部规章制度列表
 */
export function getRegulationList(params: HbtRegulationQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtRegulation>>>({
    url: '/api/HbtRegulation/list',
    method: 'get',
    params
  })
}

/**
 * 获取内部规章制度详情
 */
export function getRegulationById(id: number | bigint) {
  return request<HbtApiResponse<HbtRegulation>>({
    url: `/api/HbtRegulation/${id}`,
    method: 'get'
  })
}

/**
 * 创建内部规章制度
 */
export function createRegulation(data: HbtRegulationCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtRegulation',
    method: 'post',
    data
  })
}

/**
 * 更新内部规章制度
 */
export function updateRegulation(id: number | bigint, data: HbtRegulationUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRegulation/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除内部规章制度
 */
export function deleteRegulation(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRegulation/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除内部规章制度
 */
export function batchDeleteRegulation(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtRegulation/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出内部规章制度数据
 */
export function exportRegulation(params: HbtRegulationQuery, sheetName = '内部规章制度数据') {
  return request<Blob>({
    url: '/api/HbtRegulation/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入内部规章制度数据
 */
export function importRegulation(file: File, sheetName = '内部规章制度数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtRegulation/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取内部规章制度导入模板
 */
export function getRegulationTemplate(sheetName = '内部规章制度数据') {
  return request<Blob>({
    url: '/api/HbtRegulation/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 