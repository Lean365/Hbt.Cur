//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : dictData.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据 API
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type {
  HbtDictData,
  HbtDictDataQuery,
  HbtDictDataCreate,
  HbtDictDataUpdate,
  HbtDictDataPageResult
} from '@/types/admin/dictData'

/**
 * 获取字典数据分页列表
 */
export function getHbtDictDataList(query: HbtDictDataQuery) {
  return request<HbtApiResponse<HbtDictDataPageResult>>({
    url: '/api/HbtDictData/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取字典数据详情
 */
export function getHbtDictData(id: number) {
  return request<HbtApiResponse<HbtDictData>>({
    url: `/api/HbtDictData/${id}`,
    method: 'get'
  })
}

/**
 * 根据字典类型获取字典数据列表
 */
export function getHbtDictDataByType(dictType: string) {
  return request<HbtApiResponse<HbtDictData[]>>({
    url: `/api/HbtDictData/type/${dictType}`,
    method: 'get'
  })
}

/**
 * 创建字典数据
 */
export function createHbtDictData(data: HbtDictDataCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtDictData',
    method: 'post',
    data
  })
}

/**
 * 更新字典数据
 */
export function updateHbtDictData(data: HbtDictDataUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictData',
    method: 'put',
    data
  })
}

/**
 * 删除字典数据
 */
export function deleteHbtDictData(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDictData/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除字典数据
 */
export function batchDeleteHbtDictData(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictData/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导入字典数据
 */
export function importHbtDictData(data: FormData) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictData/import',
    method: 'post',
    data,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出字典数据
 */
export function exportHbtDictData(query: HbtDictDataQuery) {
  return request<Blob>({
    url: '/api/HbtDictData/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getHbtDictDataTemplate() {
  return request<Blob>({
    url: '/api/HbtDictData/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 更新字典数据状态
 */
export function updateHbtDictDataStatus(id: number, status: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDictData/${id}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 更新字典数据默认值
 */
export function updateHbtDictDataDefault(id: number, isDefault: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDictData/${id}/default`,
    method: 'put',
    params: { isDefault }
  })
} 