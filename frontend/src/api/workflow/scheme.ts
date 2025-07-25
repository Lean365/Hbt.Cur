import request from '@/utils/request'
import type { HbtSchemeQuery, HbtScheme, HbtSchemeStatus, HbtSchemeUpdate, HbtSchemeCreate } from '@/types/workflow/scheme'
import type { HbtPagedResult } from '@/types/common'
import type { HbtApiResponse } from '@/types/common'

// 获取工作流定义分页列表
export function getSchemeList(query: HbtSchemeQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtScheme>>>({
    url: '/api/HbtScheme/list',
    method: 'get',
    params: query
  })
}

// 获取工作流定义详情
export function getSchemeById(schemeId: number) {
  return request<HbtApiResponse<HbtScheme>>({
    url: `/api/HbtScheme/${schemeId}`,
    method: 'get'
  })
}

// 根据键获取工作流定义
export function getSchemeByKey(schemeKey: string) {
  return request<HbtApiResponse<HbtScheme>>({
    url: `/api/HbtScheme/key/${schemeKey}`,
    method: 'get'
  })
}

// 创建工作流定义
export function createScheme(data: HbtSchemeCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtScheme',
    method: 'post',
    data
  })
}

// 更新工作流定义
export function updateScheme(data: HbtSchemeUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtScheme',
    method: 'put',
    data
  })
}

// 删除工作流定义
export function deleteScheme(schemeId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtScheme/${schemeId}`,
    method: 'delete'
  })
}

// 批量删除工作流定义
export function batchDeleteScheme(schemeIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtScheme/batch',
    method: 'delete',
    data: schemeIds
  })
}

// 导入工作流定义
export function importScheme(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtScheme/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流定义
export function exportScheme(query: HbtSchemeQuery) {
  return request<Blob>({
    url: '/api/HbtScheme/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getSchemeTemplate() {
  return request<Blob>({
    url: '/api/HbtScheme/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新工作流定义状态
export function updateSchemeStatus(data: HbtSchemeStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtScheme/${data.schemeId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 获取我的工作流定义列表
export function getMySchemes(query: HbtSchemeQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtScheme>>>({
    url: '/api/HbtScheme/my',
    method: 'get',
    params: query
  })
}

// 获取工作流定义选项列表
export function getSchemeOptions() {
  return request<HbtApiResponse<{ label: string; value: number }[]>>({
    url: '/api/HbtScheme/options',
    method: 'get'
  })
}

// 获取已发布的工作流定义列表
export function getPublishedSchemeList(query: HbtSchemeQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtScheme>>>({
    url: '/api/HbtScheme/published',
    method: 'get',
    params: query
  })
}

// 复制工作流定义
export function copyScheme(schemeId: number, newSchemeName: string) {
  return request<HbtApiResponse<number>>({
    url: `/api/HbtScheme/${schemeId}/copy`,
    method: 'post',
    data: {
      newSchemeName
    }
  })
}

// 获取工作流定义版本历史
export function getSchemeVersions(schemeKey: string) {
  return request<HbtApiResponse<HbtScheme[]>>({
    url: `/api/HbtScheme/${schemeKey}/versions`,
    method: 'get'
  })
}
