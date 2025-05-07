//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtConfig.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置API
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type {
  HbtConfig,
  HbtConfigQuery,
  HbtConfigCreate,
  HbtConfigUpdate,
  HbtConfigImportResult
} from '@/types/core/config'
import type { AxiosProgressEvent, AxiosRequestConfig, AxiosResponse } from 'axios'
import { getToken } from '@/utils/auth'

/**
 * 获取配置分页列表
 * @param query 查询参数
 * @returns 配置分页列表
 */
export function getHbtConfigList(query: HbtConfigQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtConfig>>>({
    url: '/api/HbtConfig/list',
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
  return request<HbtApiResponse<HbtConfig>>({
    url: `/api/HbtConfig/${configId}`,
    method: 'get'
  })
}

/**
 * 创建配置
 * @param data 创建参数
 * @returns 配置ID
 */
export function createHbtConfig(data: HbtConfigCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtConfig',
    method: 'post',
    data
  })
}

/**
 * 更新配置
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateHbtConfig(data: HbtConfigUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtConfig',
    method: 'put',
    data,
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

/**
 * 删除配置
 * @param configId 配置ID
 * @returns 是否成功
 */
export function deleteHbtConfig(configId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtConfig/${configId}`,
    method: 'delete'
  })
}

/**
 * 批量删除配置
 * @param configIds 配置ID列表
 * @returns 是否成功
 */
export function batchDeleteHbtConfig(configIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtConfig/batch',
    method: 'delete',
    data: configIds
  })
}

/**
 * 更新配置状态
 * @param configId 配置ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateHbtConfigStatus(configId: number, status: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtConfig/${configId}/status`,
    method: 'put',
    params: { status }
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
 * 导入系统配置
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 导入结果
 */
export function importHbtConfig(file: File, sheetName: string = 'HbtConfig'): Promise<AxiosResponse<HbtApiResponse<HbtConfigImportResult>>> {
  console.log('开始导入配置，文件信息:', {
    name: file.name,
    size: file.size,
    type: file.type
  })

  // 验证文件对象
  if (!file || !(file instanceof File)) {
    console.error('无效的文件对象:', file)
    return Promise.reject(new Error('无效的文件对象'))
  }

  if (file.size === 0) {
    console.error('文件大小为0:', file)
    return Promise.reject(new Error('文件大小为0'))
  }

  // 创建 FormData 对象
  const formData = new FormData()
  formData.append('file', file)
  formData.append('sheetName', sheetName)

  // 验证 FormData 内容
  console.log('FormData 内容:')
  for (const [key, value] of formData.entries()) {
    console.log(`- ${key}:`, value)
    if (value instanceof File) {
      console.log(`  文件详情:`, {
        name: value.name,
        size: value.size,
        type: value.type,
        lastModified: value.lastModified
      })
    }
  }

  // 发送请求
  return request({
    url: '/api/HbtConfig/import',
    method: 'post',
    data: formData,
    headers: {
      'Authorization': `Bearer ${getToken()}`,
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取配置值
 * @param configKey 配置键
 * @returns 配置值
 */
export function getHbtConfigValue(configKey: string) {
  return request<HbtApiResponse<string>>({
    url: `/api/HbtConfig/value/${configKey}`,
    method: 'get'
  })
}