//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtConfig.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置API
//===================================================================

import request from '@/utils/request'
import type {
  HbtConfig,
  HbtConfigQuery,
  HbtConfigCreate,
  HbtConfigUpdate,
  ApiResponse,
  HbtPageResponse
} from '@/types/admin/hbtConfig'

/**
 * 获取配置列表
 * @param query 查询参数
 * @returns 配置列表
 */
export function getHbtConfigList(query: HbtConfigQuery) {
  return request<HbtPageResponse<HbtConfig>>({
    url: '/api/HbtConfig',
    method: 'get',
    params: query
  })
}

/**
 * 获取配置详情
 * @param configId 配置ID
 * @returns 配置详情
 */
export function getHbtConfig(configId: number) {
  return request<ApiResponse<HbtConfig>>({
    url: `/api/HbtConfig/${configId}`,
    method: 'get'
  })
}

/**
 * 创建配置
 * @param data 创建参数
 * @returns 创建结果
 */
export function createHbtConfig(data: HbtConfigCreate) {
  return request<ApiResponse<null>>({
    url: '/api/HbtConfig',
    method: 'post',
    data
  })
}

/**
 * 更新配置
 * @param data 更新参数
 * @returns 更新结果
 */
export function updateHbtConfig(data: HbtConfigUpdate) {
  return request<ApiResponse<null>>({
    url: '/api/HbtConfig',
    method: 'put',
    data
  })
}

/**
 * 删除配置
 * @param configId 配置ID
 * @returns 删除结果
 */
export function deleteHbtConfig(configId: number) {
  return request<ApiResponse<null>>({
    url: `/api/HbtConfig/${configId}`,
    method: 'delete'
  })
}

/**
 * 批量删除配置
 * @param configIds 配置ID列表
 * @returns 删除结果
 */
export function batchDeleteHbtConfig(configIds: number[]) {
  return request<ApiResponse<null>>({
    url: '/api/HbtConfig/batch',
    method: 'delete',
    data: configIds
  })
}

/**
 * 导入配置
 * @param file 文件
 * @returns 导入结果
 */
export function importHbtConfig(file: FormData) {
  return request<ApiResponse<null>>({
    url: '/api/HbtConfig/import',
    method: 'post',
    data: file,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出配置
 * @param query 查询参数
 * @returns 文件流
 */
export function exportHbtConfig(query: HbtConfigQuery) {
  return request<Blob>({
    url: '/api/HbtConfig/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 * @returns 文件流
 */
export function getHbtConfigTemplate() {
  return request<Blob>({
    url: '/api/HbtConfig/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 更新配置状态
 * @param configId 配置ID
 * @param status 状态
 * @returns 更新结果
 */
export function updateHbtConfigStatus(configId: number, status: number) {
  return request<ApiResponse<null>>({
    url: `/api/HbtConfig/${configId}/status`,
    method: 'put',
    data: { status }
  })
} 