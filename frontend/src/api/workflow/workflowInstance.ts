import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  WorkflowInstanceQuery, 
  WorkflowInstance,
  WorkflowInstanceCreate,
  WorkflowInstanceUpdate,
  WorkflowInstanceStatus
} from '@/types/workflow/workflowInstance'

// 获取工作流实例列表
export function getWorkflowInstanceList(params: WorkflowInstanceQuery) {
  return request<HbtApiResult<WorkflowInstance[]>>({
    url: '/api/HbtWorkflowInstance',
    method: 'get',
    params
  })
}

// 获取工作流实例详情
export function getWorkflowInstance(id: number) {
  return request<HbtApiResult<WorkflowInstance>>({
    url: `/api/HbtWorkflowInstance/${id}`,
    method: 'get'
  })
}

// 创建工作流实例
export function createWorkflowInstance(data: WorkflowInstanceCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowInstance',
    method: 'post',
    data
  })
}

// 更新工作流实例
export function updateWorkflowInstance(data: WorkflowInstanceUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowInstance',
    method: 'put',
    data
  })
}

// 删除工作流实例
export function deleteWorkflowInstance(id: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowInstance/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流实例
export function batchDeleteWorkflowInstance(ids: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowInstance/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流实例
export function importWorkflowInstance(instances: any[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowInstance/import',
    method: 'post',
    data: instances
  })
}

// 导出工作流实例
export function exportWorkflowInstance(params: WorkflowInstanceQuery) {
  return request({
    url: '/api/HbtWorkflowInstance/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

// 获取工作流实例导入模板
export function getWorkflowInstanceTemplate() {
  return request({
    url: '/api/HbtWorkflowInstance/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新工作流实例状态
export function updateWorkflowInstanceStatus(id: number, data: WorkflowInstanceStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowInstance/${id}/status`,
    method: 'put',
    data
  })
}

// 提交工作流实例
export function submitWorkflowInstance(id: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowInstance/${id}/submit`,
    method: 'post'
  })
}

// 撤回工作流实例
export function withdrawWorkflowInstance(id: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowInstance/${id}/withdraw`,
    method: 'post'
  })
}

// 终止工作流实例
export function terminateWorkflowInstance(id: number, reason: string) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowInstance/${id}/terminate`,
    method: 'post',
    params: { reason }
  })
} 