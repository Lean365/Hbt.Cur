import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtWorkflowDefinitionQuery, 
  HbtWorkflowDefinition,
  HbtWorkflowDefinitionCreate,
  HbtWorkflowDefinitionUpdate,
  HbtWorkflowDefinitionStatus,
  HbtWorkflowDefinitionPagedResult
} from '@/types/workflow/workflowDefinition'

// 获取工作流定义列表
export function getWorkflowDefinitionList(params: HbtWorkflowDefinitionQuery) {
  return request<HbtApiResponse<HbtWorkflowDefinitionPagedResult>>({
    url: '/api/HbtWorkflowDefinition/list',
    method: 'get',
    params
  })
}

// 获取工作流定义详情
export function getWorkflowDefinition(id: number) {
  return request<HbtApiResponse<HbtWorkflowDefinition>>({
    url: `/api/HbtWorkflowDefinition/${id}`,
    method: 'get'
  })
}

// 创建工作流定义
export function createWorkflowDefinition(data: HbtWorkflowDefinitionCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowDefinition',
    method: 'post',
    data
  })
}

// 更新工作流定义
export function updateWorkflowDefinition(data: HbtWorkflowDefinitionUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowDefinition',
    method: 'put',
    data
  })
}

// 删除工作流定义
export function deleteWorkflowDefinition(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowDefinition/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流定义
export function batchDeleteWorkflowDefinition(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowDefinition/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流定义
export function importWorkflowDefinition(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowDefinition/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流定义
export function exportWorkflowDefinition(params: HbtWorkflowDefinitionQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowDefinition/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流定义导入模板
export function getWorkflowDefinitionTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowDefinition/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 更新工作流定义状态
export function updateWorkflowDefinitionStatus(id: number, data: HbtWorkflowDefinitionStatus) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowDefinition/${id}/status`,
    method: 'put',
    data
  })
} 