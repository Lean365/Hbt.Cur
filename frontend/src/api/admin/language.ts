import request from '@/utils/request'
import type { ApiResult, PageResult } from '@/types/base'
import type { 
  LanguageQuery, 
  Language, 
  LanguageCreate, 
  LanguageUpdate,
  LanguageStatus
} from '@/types/admin/language'

// 获取语言列表
export function getLanguageList(params: LanguageQuery) {
  return request<ApiResult<Language[]>>({
    url: '/api/HbtLanguage',
    method: 'get',
    params
  })
}

// 获取语言详情
export function getLanguage(langId: number) {
  return request<ApiResult<Language>>({
    url: `/api/HbtLanguage/${langId}`,
    method: 'get'
  })
}

// 创建语言
export function createLanguage(data: LanguageCreate) {
  return request<ApiResult<any>>({
    url: '/api/HbtLanguage',
    method: 'post',
    data
  })
}

// 更新语言
export function updateLanguage(data: LanguageUpdate) {
  return request<ApiResult<any>>({
    url: '/api/HbtLanguage',
    method: 'put',
    data
  })
}

// 删除语言
export function deleteLanguage(langId: number) {
  return request<ApiResult<any>>({
    url: `/api/HbtLanguage/${langId}`,
    method: 'delete'
  })
}

// 批量删除语言
export function batchDeleteLanguage(langIds: number[]) {
  return request<ApiResult<any>>({
    url: '/api/HbtLanguage/batch',
    method: 'delete',
    data: langIds
  })
}

// 更新语言状态
export function updateLanguageStatus(data: LanguageStatus) {
  return request<ApiResult<any>>({
    url: `/api/HbtLanguage/${data.langId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 导入语言数据
export function importLanguage(file: File, sheetName: string = '语言信息') {
  const formData = new FormData()
  formData.append('file', file)
  return request<ApiResult<any>>({
    url: '/api/HbtLanguage/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出语言数据
export function exportLanguage(params: LanguageQuery, sheetName: string = '语言信息') {
  return request({
    url: '/api/HbtLanguage/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取语言导入模板
export function getLanguageTemplate(sheetName: string = '语言信息') {
  return request({
    url: '/api/HbtLanguage/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 获取当前系统支持的语言列表
export function getSupportedLanguages() {
  return request<ApiResult<Language[]>>({
    url: '/api/HbtLanguage/supported',
    method: 'get'
  })
} 