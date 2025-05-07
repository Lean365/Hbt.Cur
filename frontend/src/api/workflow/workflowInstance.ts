import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtWorkflowInstanceQuery, 
  HbtWorkflowInstance,
  HbtWorkflowInstanceCreate,
  HbtWorkflowInstanceUpdate,
  HbtWorkflowInstancePagedResult
} from '@/types/workflow/workflowInstance'

// 获取工作流实例列表
export function getWorkflowInstanceList(params: HbtWorkflowInstanceQuery) {
  return request<HbtApiResponse<HbtWorkflowInstancePagedResult>>({
    url: '/api/HbtWorkflowInstance/list',
    method: 'get',
    params
  })
}

// 获取工作流实例详情
export function getWorkflowInstance(id: number) {
  return request<HbtApiResponse<HbtWorkflowInstance>>({
    url: `/api/HbtWorkflowInstance/${id}`,
    method: 'get'
  })
}

// 创建工作流实例
export function createWorkflowInstance(data: HbtWorkflowInstanceCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowInstance',
    method: 'post',
    data
  })
}

// 更新工作流实例
export function updateWorkflowInstance(data: HbtWorkflowInstanceUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowInstance',
    method: 'put',
    data
  })
}

// 删除工作流实例
export function deleteWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowInstance/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流实例
export function batchDeleteWorkflowInstance(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowInstance/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流实例
export function importWorkflowInstance(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowInstance/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流实例
export function exportWorkflowInstance(params: HbtWorkflowInstanceQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowInstance/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流实例导入模板
export function getWorkflowInstanceTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowInstance/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 更新工作流实例状态
export function updateWorkflowInstanceStatus(id: number, data: HbtWorkflowInstanceStatus) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowInstance/${id}/status`,
    method: 'put',
    data
  })
}

// 提交工作流实例
export function submitWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowInstance/${id}/submit`,
    method: 'post'
  })
}

// 撤回工作流实例
export function withdrawWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowInstance/${id}/withdraw`,
    method: 'post'
  })
}

// 终止工作流实例
export function terminateWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowInstance/${id}/terminate`,
    method: 'post'
  })
}

// 暂停工作流实例
export function suspendWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowInstance/${id}/suspend`,
    method: 'post'
  })
}

// 恢复工作流实例
export function resumeWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowInstance/${id}/resume`,
    method: 'post'
  })
} 