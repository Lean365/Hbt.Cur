//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtConfig.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置API接口
//===================================================================

import request from '@/utils/request'
import type { ApiResult } from '@/types/base'
import type { 
  HbtConfigQuery, 
  HbtConfig, 
  HbtConfigCreate, 
  HbtConfigUpdate,
  HbtConfigBatchDelete,
  HbtConfigStatusUpdate,
  HbtConfigImport,
  HbtConfigExport,
  HbtPageResponse
} from '@/types/admin/hbtConfig'

// 获取系统配置列表
export function getHbtConfigList(params: HbtConfigQuery) {
  return request<HbtPageResponse<HbtConfig>>({
    url: '/api/HbtConfig',
    method: 'get',
    params
  })
}

// 获取系统配置详情
export function getHbtConfig(configId: number) {
  return request<ApiResult<HbtConfig>>({
    url: `/api/HbtConfig/${configId}`,
    method: 'get'
  })
}

// 创建系统配置
export function createHbtConfig(data: HbtConfigCreate) {
  return request<ApiResult<any>>({
    url: '/api/HbtConfig',
    method: 'post',
    data
  })
}

// 更新系统配置
export function updateHbtConfig(data: HbtConfigUpdate) {
  return request<ApiResult<any>>({
    url: '/api/HbtConfig',
    method: 'put',
    data
  })
}

// 删除系统配置
export function deleteHbtConfig(configId: number) {
  return request<ApiResult<any>>({
    url: `/api/HbtConfig/${configId}`,
    method: 'delete'
  })
}

// 批量删除系统配置
export function batchDeleteHbtConfig(configIds: number[]) {
  return request<ApiResult<any>>({
    url: '/api/HbtConfig/batch',
    method: 'delete',
    data: configIds
  })
}

// 导入系统配置
export function importHbtConfig(file: File, sheetName: string = '系统配置信息') {
  const formData = new FormData()
  formData.append('file', file)
  return request<ApiResult<any>>({
    url: '/api/HbtConfig/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出系统配置
export function exportHbtConfig(params: HbtConfigQuery, sheetName: string = '系统配置信息') {
  return request<Blob>({
    url: '/api/HbtConfig/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取导入模板
export function getHbtConfigTemplate(sheetName: string = '系统配置信息') {
  return request<Blob>({
    url: '/api/HbtConfig/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 更新系统配置状态
export function updateHbtConfigStatus(configId: number, status: number) {
  return request<ApiResult<any>>({
    url: `/api/HbtConfig/${configId}/status`,
    method: 'put',
    params: { status }
  })
} 