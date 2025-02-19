import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  TranslationQuery, 
  Translation, 
  TranslationCreate, 
  TranslationUpdate,
  TranslationImport,
  TranslationExport,
  TranslationStatus
} from '@/types/admin/translation'

// 获取翻译列表
export function getTranslationList(params: TranslationQuery) {
  return request<HbtApiResult<Translation[]>>({
    url: '/api/HbtTranslation',
    method: 'get',
    params
  })
}

// 获取翻译详情
export function getTranslation(translationId: number) {
  return request<HbtApiResult<Translation>>({
    url: `/api/HbtTranslation/${translationId}`,
    method: 'get'
  })
}

// 创建翻译
export function createTranslation(data: TranslationCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtTranslation',
    method: 'post',
    data
  })
}

// 更新翻译
export function updateTranslation(data: TranslationUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtTranslation',
    method: 'put',
    data
  })
}

// 删除翻译
export function deleteTranslation(translationId: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtTranslation/${translationId}`,
    method: 'delete'
  })
}

// 批量删除翻译
export function batchDeleteTranslation(translationIds: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtTranslation/batch',
    method: 'delete',
    data: translationIds
  })
}

// 更新翻译状态
export function updateTranslationStatus(data: TranslationStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtTranslation/${data.translationId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 导入翻译
export function importTranslation(file: File, sheetName: string = '翻译信息') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<any>>({
    url: '/api/HbtTranslation/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出翻译
export function exportTranslation(params: TranslationExport, sheetName: string = '翻译信息') {
  return request({
    url: '/api/HbtTranslation/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取翻译导入模板
export function getTranslationTemplate(sheetName: string = '翻译信息') {
  return request({
    url: '/api/HbtTranslation/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 刷新翻译缓存
export function refreshTranslationCache() {
  return request<HbtApiResult<any>>({
    url: '/api/HbtTranslation/refresh',
    method: 'post'
  })
}

// 获取指定语言的翻译值
export function getTranslationValue(langCode: string, transKey: string) {
  return request<HbtApiResult<string>>({
    url: '/api/HbtTranslation/value',
    method: 'get',
    params: { langCode, transKey }
  })
}

// 获取指定模块的翻译列表
export function getTranslationsByModule(langCode: string, moduleName: string) {
  return request<HbtApiResult<Translation[]>>({
    url: '/api/HbtTranslation/module',
    method: 'get',
    params: { langCode, moduleName }
  })
}

// 获取转置后的翻译数据
export function getTransposedTranslations(params: TranslationQuery) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtTranslation/transposed',
    method: 'get',
    params
  })
}