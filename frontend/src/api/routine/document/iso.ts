import request from '@/utils/request'
import type { HbtIsoDocument, HbtIsoDocumentQuery, HbtIsoDocumentCreate, HbtIsoDocumentUpdate } from '@/types/routine/document/iso'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取ISO标准文档列表
 */
export function getIsoDocumentList(params: HbtIsoDocumentQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtIsoDocument>>>({
    url: '/api/HbtIsoDocument/list',
    method: 'get',
    params
  })
}

/**
 * 获取ISO标准文档详情
 */
export function getIsoDocumentById(id: number | bigint) {
  return request<HbtApiResponse<HbtIsoDocument>>({
    url: `/api/HbtIsoDocument/${id}`,
    method: 'get'
  })
}

/**
 * 创建ISO标准文档
 */
export function createIsoDocument(data: HbtIsoDocumentCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtIsoDocument',
    method: 'post',
    data
  })
}

/**
 * 更新ISO标准文档
 */
export function updateIsoDocument(id: number | bigint, data: HbtIsoDocumentUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtIsoDocument/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ISO标准文档
 */
export function deleteIsoDocument(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtIsoDocument/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ISO标准文档
 */
export function batchDeleteIsoDocument(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtIsoDocument/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出ISO标准文档数据
 */
export function exportIsoDocument(params: HbtIsoDocumentQuery, sheetName = 'ISO标准文档数据') {
  return request<Blob>({
    url: '/api/HbtIsoDocument/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入ISO标准文档数据
 */
export function importIsoDocument(file: File, sheetName = 'ISO标准文档数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtIsoDocument/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取ISO标准文档导入模板
 */
export function getIsoDocumentTemplate(sheetName = 'ISO标准文档数据') {
  return request<Blob>({
    url: '/api/HbtIsoDocument/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 