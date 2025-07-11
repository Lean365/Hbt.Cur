import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtDefinitionQuery, 
  HbtDefinition,
  HbtDefinitionCreate,
  HbtDefinitionUpdate,
  HbtDefinitionStatus,
  HbtDefinitionPagedResult
} from '@/types/workflow/definition'
import type { HbtSelectOption } from '@/types/common'

// 获取工作流定义列表
export function getWorkflowDefinitionList(params: HbtDefinitionQuery) {
  return request<HbtApiResponse<HbtDefinitionPagedResult>>({
    url: '/api/HbtDefinition/list',
    method: 'get',
    params
  })
}

// 获取工作流定义详情
export function getWorkflowDefinition(id: number) {
  return request<HbtApiResponse<HbtDefinition>>({
    url: `/api/HbtDefinition/${id}`,
    method: 'get'
  })
}

// 创建工作流定义
export function createWorkflowDefinition(data: HbtDefinitionCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtDefinition',
    method: 'post',
    data
  })
}

// 更新工作流定义
export function updateWorkflowDefinition(data: HbtDefinitionUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtDefinition',
    method: 'put',
    data
  })
}

// 删除工作流定义
export function deleteWorkflowDefinition(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtDefinition/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流定义
export function batchDeleteWorkflowDefinition(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtDefinition/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流定义
export function importWorkflowDefinition(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtDefinition/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流定义
export function exportWorkflowDefinition(params: HbtDefinitionQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtDefinition/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流定义导入模板
export function getWorkflowDefinitionTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtDefinition/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 更新工作流定义状态
export function updateWorkflowDefinitionStatus(id: number, data: HbtDefinitionStatus) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtDefinition/${id}/status`,
    method: 'put',
    data
  })
}

// 获取工作流定义选项列表
export function getWorkflowDefinitionOptions(includeDisabled: boolean = false) {
  return request<HbtApiResponse<HbtSelectOption[]>>({
    url: '/api/HbtDefinition/options',
    method: 'get',
    params: { includeDisabled }
  })
} 