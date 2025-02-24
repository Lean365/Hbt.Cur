//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtLanguage.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言相关接口
//===================================================================

import request from '@/utils/request'
import type { ApiResult, PageResult } from '@/types/base'
import type { 
  HbtLanguage,
  HbtLanguageQuery,
  HbtLanguageCreate,
  HbtLanguageUpdate,
  HbtLanguageStatus
} from '@/types/admin/hbtLanguage'

/**
 * 获取语言分页列表
 * @param query 查询参数
 * @returns 语言分页列表
 */
export function getHbtLanguageList(query: HbtLanguageQuery) {
  return request<ApiResult<PageResult<HbtLanguage>>>({
    url: '/api/HbtLanguage',
    method: 'get',
    params: query
  })
}

/**
 * 获取语言详情
 * @param langId 语言ID
 * @returns 语言详情
 */
export function getHbtLanguage(langId: number) {
  return request<ApiResult<HbtLanguage>>({
    url: `/api/HbtLanguage/${langId}`,
    method: 'get'
  })
}

/**
 * 创建语言
 * @param data 创建参数
 * @returns 语言ID
 */
export function createHbtLanguage(data: HbtLanguageCreate) {
  return request<ApiResult<number>>({
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
  return request<ApiResult<boolean>>({
    url: '/api/HbtLanguage',
    method: 'put',
    data
  })
}

/**
 * 删除语言
 * @param langId 语言ID
 * @returns 是否成功
 */
export function deleteHbtLanguage(langId: number) {
  return request<ApiResult<boolean>>({
    url: `/api/HbtLanguage/${langId}`,
    method: 'delete'
  })
}

/**
 * 批量删除语言
 * @param langIds 语言ID列表
 * @returns 是否成功
 */
export function batchDeleteHbtLanguage(langIds: number[]) {
  return request<ApiResult<boolean>>({
    url: '/api/HbtLanguage/batch',
    method: 'delete',
    data: langIds
  })
}

/**
 * 导入语言数据
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 是否成功
 */
export function importHbtLanguage(file: File, sheetName: string = '语言数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<ApiResult<boolean>>({
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
 * 导出语言数据
 * @param query 查询参数
 * @returns 文件流
 */
export function exportHbtLanguage(query: HbtLanguageQuery) {
  return request<Blob>({
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
  return request<Blob>({
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
  return request<ApiResult<boolean>>({
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
  return request<ApiResult<HbtLanguage[]>>({
    url: '/api/HbtLanguage/supported',
    method: 'get'
  })
} 