import request from '@/utils/request'
import type { HbtLaw, HbtLawQuery, HbtLawCreate, HbtLawUpdate } from '@/types/routine/document/law'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取法律法规列表
 */
export function getLawList(params: HbtLawQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtLaw>>>({
    url: '/api/HbtLaw/list',
    method: 'get',
    params
  })
}

/**
 * 获取法律法规详情
 */
export function getLawById(id: number | bigint) {
  return request<HbtApiResponse<HbtLaw>>({
    url: `/api/HbtLaw/${id}`,
    method: 'get'
  })
}

/**
 * 创建法律法规
 */
export function createLaw(data: HbtLawCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtLaw',
    method: 'post',
    data
  })
}

/**
 * 更新法律法规
 */
export function updateLaw(id: number | bigint, data: HbtLawUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLaw/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除法律法规
 */
export function deleteLaw(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLaw/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除法律法规
 */
export function batchDeleteLaw(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLaw/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出法律法规数据
 */
export function exportLaw(params: HbtLawQuery, sheetName = '法律法规数据') {
  return request<Blob>({
    url: '/api/HbtLaw/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入法律法规数据
 */
export function importLaw(file: File, sheetName = '法律法规数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtLaw/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取法律法规导入模板
 */
export function getLawTemplate(sheetName = '法律法规数据') {
  return request<Blob>({
    url: '/api/HbtLaw/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 