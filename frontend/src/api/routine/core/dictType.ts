//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : dictType.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型 API
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type {
  HbtDictType,
  HbtDictTypeQuery,
  HbtDictTypeCreate,
  HbtDictTypeUpdate,
  HbtDictTypePageResult
} from '@/types/routine/core/dictType'

/**
 * 获取字典类型分页列表
 */
export function getHbtDictTypeList(query: HbtDictTypeQuery) {
  return request<HbtApiResponse<HbtDictTypePageResult>>({
    url: '/api/HbtDictType/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取字典类型详情
 */
export function getHbtDictType(id: number) {
  return request<HbtApiResponse<HbtDictType>>({
    url: `/api/HbtDictType/${id}`,
    method: 'get'
  })
}

/**
 * 创建字典类型
 */
export function createHbtDictType(data: HbtDictTypeCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtDictType',
    method: 'post',
    data
  })
}

/**
 * 更新字典类型
 */
export function updateHbtDictType(data: HbtDictTypeUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictType',
    method: 'put',
    data
  })
}

/**
 * 删除字典类型
 */
export function deleteHbtDictType(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDictType/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除字典类型
 */
export function batchDeleteHbtDictType(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictType/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导入字典类型
 */
export function importHbtDictType(data: FormData) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictType/import',
    method: 'post',
    data,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出字典类型
 */
export function exportHbtDictType(query: HbtDictTypeQuery) {
  return request<Blob>({
    url: '/api/HbtDictType/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getHbtDictTypeTemplate() {
  return request<Blob>({
    url: '/api/HbtDictType/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 更新字典类型状态
 */
export function updateHbtDictTypeStatus(id: number, status: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDictType/${id}/status`,
    method: 'put',
    params: { status }
  })
} 