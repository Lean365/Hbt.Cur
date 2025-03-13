//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtTranslation.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译相关接口
//===================================================================

import request from '@/utils/request'
import type { HbtApiResult, HbtPagedResult } from '@/types/common'
import type { 
  HbtTranslation,
  HbtTranslationQuery,
  HbtTranslationCreate,
  HbtTranslationUpdate,
  HbtTranslationStatus,
  HbtTransposedData
} from '@/types/admin/hbtTranslation'

/**
 * 获取翻译分页列表
 * @param query 查询参数
 * @returns 翻译分页列表
 */
export function getHbtTranslationList(query: HbtTranslationQuery) {
  return request<HbtApiResult<HbtPagedResult<HbtTranslation>>>({
    url: '/api/HbtTranslation',
    method: 'get',
    params: query
  })
}

/**
 * 获取翻译详情
 * @param transId 翻译ID
 * @returns 翻译详情
 */
export function getHbtTranslation(transId: number) {
  return request<HbtApiResult<HbtTranslation>>({
    url: `/api/HbtTranslation/${transId}`,
    method: 'get'
  })
}

/**
 * 创建翻译
 * @param data 创建参数
 * @returns 翻译ID
 */
export function createHbtTranslation(data: HbtTranslationCreate) {
  return request<HbtApiResult<number>>({
    url: '/api/HbtTranslation',
    method: 'post',
    data
  })
}

/**
 * 更新翻译
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateHbtTranslation(data: HbtTranslationUpdate) {
  return request<HbtApiResult<boolean>>({
    url: '/api/HbtTranslation',
    method: 'put',
    data
  })
}

/**
 * 删除翻译
 * @param transId 翻译ID
 * @returns 是否成功
 */
export function deleteHbtTranslation(transId: number) {
  return request<HbtApiResult<boolean>>({
    url: `/api/HbtTranslation/${transId}`,
    method: 'delete'
  })
}

/**
 * 批量删除翻译
 * @param transIds 翻译ID列表
 * @returns 是否成功
 */
export function batchDeleteHbtTranslation(transIds: number[]) {
  return request<HbtApiResult<boolean>>({
    url: '/api/HbtTranslation/batch',
    method: 'delete',
    data: transIds
  })
}

/**
 * 导入翻译数据
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 是否成功
 */
export function importHbtTranslation(file: File, sheetName: string = '翻译数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<boolean>>({
    url: '/api/HbtTranslation/import',
    method: 'post',
    params: { sheetName },
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出翻译数据
 * @param query 查询参数
 * @returns 文件流
 */
export function exportHbtTranslation(query: HbtTranslationQuery) {
  return request<Blob>({
    url: '/api/HbtTranslation/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 * @returns 文件流
 */
export function getHbtTranslationTemplate() {
  return request<Blob>({
    url: '/api/HbtTranslation/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 更新翻译状态
 * @param translationId 翻译ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateHbtTranslationStatus(translationId: number, status: number) {
  return request<HbtApiResult<boolean>>({
    url: `/api/HbtTranslation/${translationId}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 获取指定语言的翻译值
 * @param langCode 语言代码
 * @param transKey 翻译键
 * @returns 翻译值
 */
export function getHbtTranslationValue(langCode: string, transKey: string) {
  return request<HbtApiResult<string>>({
    url: '/api/HbtTranslation/value',
    method: 'get',
    params: { langCode, transKey }
  })
}

/**
 * 获取指定模块的翻译列表
 * @param langCode 语言代码
 * @param moduleName 模块名称
 * @returns 翻译列表
 */
export function getHbtTranslationsByModule(langCode: string, moduleName: string) {
  return request<HbtApiResult<HbtTranslation[]>>({
    url: '/api/HbtTranslation/module',
    method: 'get',
    params: { langCode, moduleName }
  })
}

/**
 * 获取转置后的翻译数据
 * @param query 查询参数
 * @returns 转置后的翻译数据
 */
export function getHbtTransposedData(query: HbtTranslationQuery) {
  return request<HbtApiResult<HbtTransposedData>>({
    url: '/api/HbtTranslation/transposed',
    method: 'get',
    params: query
  })
} 