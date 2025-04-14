//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtLanguage.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type {
  HbtLanguage,
  HbtLanguageQuery,
  HbtLanguageCreate,
  HbtLanguageUpdate,
  HbtLanguageStatus
} from '@/types/admin/language'

/**
 * 获取语言分页列表
 * @param query 查询参数
 * @returns 语言分页列表
 */
export function getHbtLanguageList(query: HbtLanguageQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtLanguage>>>({
    url: '/api/HbtLanguage',
    method: 'get',
    params: query
  })
}

/**
 * 获取语言详情
 * @param languageId 语言ID
 * @returns 语言详情
 */
export function getHbtLanguage(languageId: number) {
  return request<HbtApiResponse<HbtLanguage>>({
    url: `/api/HbtLanguage/${languageId}`,
    method: 'get'
  })
}

/**
 * 创建语言
 * @param data 创建参数
 * @returns 语言ID
 */
export function createHbtLanguage(data: HbtLanguageCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtLanguage',
    method: 'post',
    data
  })
}

/**
 * 更新语言
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateHbtLanguage(data: HbtLanguageUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLanguage',
    method: 'put',
    data
  })
}

/**
 * 删除语言
 * @param languageId 语言ID
 * @returns 是否成功
 */
export function deleteHbtLanguage(languageId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLanguage/${languageId}`,
    method: 'delete'
  })
}

/**
 * 批量删除语言
 * @param languageIds 语言ID列表
 * @returns 是否成功
 */
export function batchDeleteHbtLanguage(languageIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLanguage/batch',
    method: 'delete',
    data: languageIds
  })
}

/**
 * 导入语言
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 是否成功
 */
export function importHbtLanguage(file: File, sheetName: string = '语言') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtLanguage/import',
    method: 'post',
    params: { sheetName },
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出语言
 * @param query 查询参数
 * @returns 文件流
 */
export function exportHbtLanguage(query: HbtLanguageQuery) {
  return request<HbtApiResponse<Blob>>({
    url: '/api/HbtLanguage/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 * @returns 文件流
 */
export function getHbtLanguageTemplate() {
  return request<HbtApiResponse<Blob>>({
    url: '/api/HbtLanguage/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 更新语言状态
 * @param languageId 语言ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateHbtLanguageStatus(languageId: number, status: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtLanguage/${languageId}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 获取支持的语言列表
 * @returns 语言列表
 */
export function getSupportedLanguages() {
  return request<HbtApiResponse<HbtLanguage[]>>({
    url: '/api/HbtLanguage/supported',
    method: 'get'
  })
} 