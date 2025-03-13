//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtDictType.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResult, HbtPagedResult } from '@/types/common'
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
  return request<HbtApiResult<HbtPagedResult<HbtDictType>>>({
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
  return request<HbtApiResult<HbtDictType>>({
    url: `/api/HbtDictType/${dictTypeId}`,
    method: 'get'
  })
}

/**
 * 根据字典类型获取详情
 * @param type 字典类型
 * @returns 字典类型详情
 */
export function getHbtDictTypeByType(type: string) {
  return request<HbtApiResult<HbtDictType>>({
    url: `/api/HbtDictType/type/${type}`,
    method: 'get'
  })
}

/**
 * 创建字典类型
 * @param data 创建参数
 * @returns 字典类型ID
 */
export function createHbtDictType(data: HbtDictTypeCreate) {
  return request<HbtApiResult<number>>({
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
  return request<HbtApiResult<boolean>>({
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
  return request<HbtApiResult<boolean>>({
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
  return request<HbtApiResult<boolean>>({
    url: '/api/HbtDictType/batch',
    method: 'delete',
    data: dictTypeIds
  })
}

/**
 * 导入字典类型
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 是否成功
 */
export function importHbtDictType(file: File, sheetName: string = '字典类型') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<boolean>>({
    url: '/api/HbtDictType/import',
    method: 'post',
    params: { sheetName },
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出字典类型
 * @param query 查询参数
 * @returns 文件流
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
 * @returns 文件流
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
export function updateHbtDictTypeStatus(dictTypeId: number, status: number) {
  return request<HbtApiResult<boolean>>({
    url: `/api/HbtDictType/${dictTypeId}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 执行字典SQL脚本
 * @param dictTypeId 字典类型ID
 * @returns 返回SQL执行结果
 */
export function executeDictSql(dictTypeId: number) {
  return request<HbtApiResult<any[]>>({
    url: `/api/HbtDictType/executeSql/${dictTypeId}`,
    method: 'get'
  })
} 