//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : {{ table.name }}.ts
// 创建者 : CodeGenerator
// 创建时间: {{ datetime }}
// 版本号 : v1.0.0
// 描述    : {{ table.comment }}API
//===================================================================

import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type {
  {{ pascal_case table.table_name }},
  {{ pascal_case table.table_name }}Query,
  {{ pascal_case table.table_name }}Create,
  {{ pascal_case table.table_name }}Update,
  {{ pascal_case table.table_name }}Status,
  {{ pascal_case table.table_name }}Import
} from '@/types/{{ table.module_name }}/{{ table.name }}'

/**
 * 获取{{ table.comment }}分页列表
 */
export function getPagedList(params: {{ pascal_case table.table_name }}Query) {
  return request<HbtApiResponse<HbtPagedResult<{{ pascal_case table.table_name }}>>>({
    url: '/api/{{ table.module_name }}/{{ table.name }}',
    method: 'get',
    params
  })
}

/**
 * 获取{{ table.comment }}详情
 */
export function getDetail(id: number) {
  return request<HbtApiResponse<{{ pascal_case table.table_name }}>>({
    url: `/api/{{ table.module_name }}/{{ table.name }}/${id}`,
    method: 'get'
  })
}

/**
 * 创建{{ table.comment }}
 */
export function create(data: {{ pascal_case table.table_name }}Create) {
  return request<HbtApiResponse<void>>({
    url: '/api/{{ table.module_name }}/{{ table.name }}',
    method: 'post',
    data
  })
}

/**
 * 更新{{ table.comment }}
 */
export function update(data: {{ pascal_case table.table_name }}Update) {
  return request<HbtApiResponse<void>>({
    url: '/api/{{ table.module_name }}/{{ table.name }}',
    method: 'put',
    data
  })
}

/**
 * 删除{{ table.comment }}
 */
export function remove(id: number) {
  return request<HbtApiResponse<void>>({
    url: `/api/{{ table.module_name }}/{{ table.name }}/${id}`,
    method: 'delete'
  })
}

/**
 * 导出{{ table.comment }}
 */
export function exportData(params?: {{ pascal_case table.table_name }}Query) {
  return request({
    url: '/api/{{ table.module_name }}/{{ table.name }}/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 下载{{ table.comment }}导入模板
 */
export function downloadTemplate() {
  return request({
    url: '/api/{{ table.module_name }}/{{ table.name }}/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 导入{{ table.comment }}
 */
export function importData(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<void>>({
    url: '/api/{{ table.module_name }}/{{ table.name }}/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 更新{{ table.comment }}状态
 */
export function updateStatus(data: {{ pascal_case table.table_name }}Status) {
  return request<HbtApiResponse<void>>({
    url: `/api/{{ table.module_name }}/{{ table.name }}/status`,
    method: 'put',
    data
  })
} 