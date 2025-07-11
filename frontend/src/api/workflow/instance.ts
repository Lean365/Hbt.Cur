import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtInstanceQuery, 
  HbtInstance,
  HbtInstanceCreate,
  HbtInstanceUpdate,
  HbtInstanceStatus,
  HbtInstancePagedResult
} from '@/types/workflow/instance'

// 获取工作流实例列表
export function getWorkflowInstanceList(params: HbtInstanceQuery) {
  return request<HbtApiResponse<HbtInstancePagedResult>>({
    url: '/api/HbtInstance/list',
    method: 'get',
    params
  })
}

// 获取工作流实例详情
export function getWorkflowInstance(id: number) {
  return request<HbtApiResponse<HbtInstance>>({
    url: `/api/HbtInstance/${id}`,
    method: 'get'
  })
}

// 创建工作流实例
export function createWorkflowInstance(data: HbtInstanceCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtInstance',
    method: 'post',
    data
  })
}

// 更新工作流实例
export function updateWorkflowInstance(data: HbtInstanceUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtInstance',
    method: 'put',
    data
  })
}

// 删除工作流实例
export function deleteWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtInstance/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流实例
export function batchDeleteWorkflowInstance(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtInstance/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流实例
export function importWorkflowInstance(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtInstance/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流实例
export function exportWorkflowInstance(params: HbtInstanceQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtInstance/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流实例导入模板
export function getWorkflowInstanceTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtInstance/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 更新工作流实例状态
export function updateWorkflowInstanceStatus(id: number, data: HbtInstanceStatus) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtInstance/${id}/status`,
    method: 'put',
    data
  })
}

// 提交工作流实例
export function submitWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtInstance/${id}/submit`,
    method: 'post'
  })
}

// 撤回工作流实例
export function withdrawWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtInstance/${id}/withdraw`,
    method: 'post'
  })
}

// 终止工作流实例
export function terminateWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtInstance/${id}/terminate`,
    method: 'post'
  })
}

// 启动工作流实例
export function startWorkflowInstance(data: any) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflow/start',
    method: 'post',
    data
  })
}

// 暂停工作流实例
export function suspendWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflow/suspend/${id}`,
    method: 'post'
  })
}

// 恢复工作流实例
export function resumeWorkflowInstance(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflow/resume/${id}`,
    method: 'post'
  })
} 