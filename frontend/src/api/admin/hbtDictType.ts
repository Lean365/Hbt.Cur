//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtDictType.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtStatus, ApiResult, PageResult } from '@/types/base'
import type { 
  HbtDictType,
  HbtDictTypeQuery,
  HbtDictTypeCreate,
  HbtDictTypeUpdate,
  HbtDictTypeStatus,
  HbtDictTypeImport
} from '@/types/admin/hbtDictType'

/**
 * 获取字典类型分页列表
 * @param query 查询参数
 * @returns 字典类型分页列表
 */
export function getHbtDictTypeList(query: HbtDictTypeQuery) {
  return request<ApiResult<PageResult<HbtDictType>>>({
    url: '/api/HbtDictType',
    method: 'get',
    params: query
  })
}

/**
 * 获取字典类型详情
 * @param dictTypeId 字典类型ID
 * @returns 字典类型详情
 */
export function getHbtDictType(dictTypeId: number) {
  return request<ApiResult<HbtDictType>>({
    url: `/api/HbtDictType/${dictTypeId}`,
    method: 'get'
  })
}

/**
 * 创建字典类型
 * @param data 创建参数
 * @returns 字典类型ID
 */
export function createHbtDictType(data: HbtDictTypeCreate) {
  return request<ApiResult<number>>({
    url: '/api/HbtDictType',
    method: 'post',
    data
  })
}

/**
 * 更新字典类型
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateHbtDictType(data: HbtDictTypeUpdate) {
  return request<ApiResult<boolean>>({
    url: '/api/HbtDictType',
    method: 'put',
    data
  })
}

/**
 * 删除字典类型
 * @param dictTypeId 字典类型ID
 * @returns 是否成功
 */
export function deleteHbtDictType(dictTypeId: number) {
  return request<ApiResult<boolean>>({
    url: `/api/HbtDictType/${dictTypeId}`,
    method: 'delete'
  })
}

/**
 * 批量删除字典类型
 * @param dictTypeIds 字典类型ID列表
 * @returns 是否成功
 */
export function batchDeleteHbtDictType(dictTypeIds: number[]) {
  return request<ApiResult<boolean>>({
    url: '/api/HbtDictType/batch',
    method: 'delete',
    data: dictTypeIds
  })
}

/**
 * 导入字典类型
 * @param data 导入数据
 * @returns 是否成功
 */
export function importHbtDictType(data: HbtDictTypeImport[]) {
  return request<ApiResult<boolean>>({
    url: '/api/HbtDictType/import',
    method: 'post',
    data
  })
}

/**
 * 导出字典类型
 * @param query 查询参数
 * @returns 导出数据
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
 * @returns 模板数据
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
 * @param dictTypeId 字典类型ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateHbtDictTypeStatus(dictTypeId: number, status: HbtStatus) {
  return request<ApiResult<boolean>>({
    url: `/api/HbtDictType/${dictTypeId}/status`,
    method: 'put',
    params: { status }
  })
} 