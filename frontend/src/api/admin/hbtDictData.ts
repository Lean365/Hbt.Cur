//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtDictData.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type {
  HbtDictData,
  HbtDictDataQuery,
  HbtDictDataCreate,
  HbtDictDataUpdate,
  HbtDictDataStatus,
  DictOption
} from '@/types/admin/hbtDictData'

/**
 * 获取字典数据分页列表
 * @param query 查询参数
 * @returns 字典数据分页列表
 */
export function getHbtDictDataList(query: HbtDictDataQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtDictData>>>({
    url: '/api/HbtDictData',
    method: 'get',
    params: query
  })
}

/**
 * 获取字典数据详情
 * @param dictDataId 字典数据ID
 * @returns 字典数据详情
 */
export function getHbtDictData(dictDataId: number) {
  return request<HbtApiResponse<HbtDictData>>({
    url: `/api/HbtDictData/${dictDataId}`,
    method: 'get'
  })
}

/**
 * 创建字典数据
 * @param data 创建参数
 * @returns 字典数据ID
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
 * @param data 更新参数
 * @returns 是否成功
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
 * @param dictDataId 字典数据ID
 * @returns 是否成功
 */
export function deleteHbtDictData(dictDataId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDictData/${dictDataId}`,
    method: 'delete'
  })
}

/**
 * 批量删除字典数据
 * @param dictDataIds 字典数据ID列表
 * @returns 是否成功
 */
export function batchDeleteHbtDictData(dictDataIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictData/batch',
    method: 'delete',
    data: dictDataIds
  })
}

/**
 * 更新字典数据状态
 * @param dictDataId 字典数据ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateHbtDictDataStatus(dictDataId: number, status: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtDictData/${dictDataId}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 获取字典数据选项列表
 * @param dictTypeId 字典类型ID
 * @returns 字典数据选项列表
 */
export function getHbtDictDataOptions(dictTypeId: number) {
  return request<HbtApiResponse<DictOption[]>>({
    url: '/api/HbtDictData/options',
    method: 'get',
    params: { dictTypeId }
  })
}

/**
 * 导出字典数据
 * @param query 查询参数
 * @returns 文件流
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
 * @returns 文件流
 */
export function getHbtDictDataTemplate() {
  return request<Blob>({
    url: '/api/HbtDictData/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 导入字典数据
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 是否成功
 */
export function importHbtDictData(file: File, sheetName: string = '字典数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtDictData/import',
    method: 'post',
    params: { sheetName },
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 根据字典类型获取字典数据列表
 * @param dictType 字典类型
 * @returns 字典数据列表
 */
export function getHbtDictDataByType(dictType: string) {
  return request<HbtApiResponse<HbtDictData[]>>({
    url: `/api/HbtDictData/type/${dictType}`,
    method: 'get'
  })
} 