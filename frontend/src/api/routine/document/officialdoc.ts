import request from '@/utils/request'
import type { HbtOfficialDocument, HbtOfficialDocumentQuery, HbtOfficialDocumentCreate, HbtOfficialDocumentUpdate } from '@/types/routine/document/officialdoc'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取公文文档列表
 */
export function getOfficialDocumentList(params: HbtOfficialDocumentQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtOfficialDocument>>>({
    url: '/api/HbtOfficialDocument/list',
    method: 'get',
    params
  })
}

/**
 * 获取公文文档详情
 */
export function getOfficialDocumentById(id: number | bigint) {
  return request<HbtApiResponse<HbtOfficialDocument>>({
    url: `/api/HbtOfficialDocument/${id}`,
    method: 'get'
  })
}

/**
 * 创建公文文档
 */
export function createOfficialDocument(data: HbtOfficialDocumentCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtOfficialDocument',
    method: 'post',
    data
  })
}

/**
 * 更新公文文档
 */
export function updateOfficialDocument(id: number | bigint, data: HbtOfficialDocumentUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOfficialDocument/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除公文文档
 */
export function deleteOfficialDocument(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOfficialDocument/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除公文文档
 */
export function batchDeleteOfficialDocument(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOfficialDocument/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出公文文档数据
 */
export function exportOfficialDocument(params: HbtOfficialDocumentQuery, sheetName = '公文文档数据') {
  return request<Blob>({
    url: '/api/HbtOfficialDocument/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入公文文档数据
 */
export function importOfficialDocument(file: File, sheetName = '公文文档数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtOfficialDocument/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取公文文档导入模板
 */
export function getOfficialDocumentTemplate(sheetName = '公文文档数据') {
  return request<Blob>({
    url: '/api/HbtOfficialDocument/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 