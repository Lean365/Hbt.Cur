import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type {
  HbtGenTableDefineDto,
  HbtGenTableDefineQuery,
  HbtGenTableDefineCreate,
  HbtGenTableDefineUpdate,
  HbtGenTableDefineImport,
  HbtGenTableDefinePagedResult
} from '@/types/generator/tableDefine'

/**
 * 获取代码生成表定义列表
 */
export function getPagedList(params: HbtGenTableDefineQuery) {
  return request<HbtApiResponse<HbtGenTableDefinePagedResult>>({
    url: '/api/HbtGenTableDefine/list',
    method: 'get',
    params
  })
}

/**
 * 获取代码生成表定义详情
 */
export function getTableDefine(id: number) {
  return request<HbtApiResponse<HbtGenTableDefineDto>>({
    url: `/api/HbtGenTableDefine/${id}`,
    method: 'get'
  })
}

/**
 * 创建代码生成表定义
 */
export function createTableDefine(data: HbtGenTableDefineCreate) {
  return request<HbtApiResponse<HbtGenTableDefineDto>>({
    url: '/api/HbtGenTableDefine',
    method: 'post',
    data
  })
}

/**
 * 更新代码生成表定义
 */
export function updateTableDefine(data: HbtGenTableDefineUpdate) {
  return request<HbtApiResponse<HbtGenTableDefineDto>>({
    url: '/api/HbtGenTableDefine',
    method: 'put',
    data
  })
}

/**
 * 删除代码生成表定义
 */
export function deleteTableDefine(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTableDefine/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除代码生成表定义
 */
export function batchDeleteTableDefine(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtGenTableDefine/batch-delete',
    method: 'post',
    data: ids
  })
}

/**
 * 导入代码生成表定义
 */
export function importTableDefine(data: HbtGenTableDefineImport) {
  const formData = new FormData()
  formData.append('file', data.file)
  if (data.sheetName) {
    formData.append('sheetName', data.sheetName)
  }
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtGenTableDefine/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出代码生成表定义
 */
export function exportTableDefine() {
  return request<HbtApiResponse<Blob>>({
    url: '/api/HbtGenTableDefine/export',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getTemplate() {
  return request<HbtApiResponse<Blob>>({
    url: '/api/HbtGenTableDefine/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 初始化表结构
 */
export function initializeTable(data: HbtGenTableDefineCreate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtGenTableDefine/initialize',
    method: 'post',
    data
  })
}

/**
 * 同步表结构
 */
export function syncTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTableDefine/sync/${id}`,
    method: 'post'
  })
} 