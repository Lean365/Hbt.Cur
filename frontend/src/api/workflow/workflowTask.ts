import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  WorkflowTaskQuery, 
  WorkflowTask,
  WorkflowTaskCreate,
  WorkflowTaskUpdate,
  WorkflowTaskStatus
} from '@/types/workflow/workflowTask'

// 获取工作流任务列表
export function getWorkflowTaskList(params: WorkflowTaskQuery) {
  return request<HbtApiResult<WorkflowTask[]>>({
    url: '/api/HbtWorkflowTask',
    method: 'get',
    params
  })
}

// 获取工作流任务详情
export function getWorkflowTask(id: number) {
  return request<HbtApiResult<WorkflowTask>>({
    url: `/api/HbtWorkflowTask/${id}`,
    method: 'get'
  })
}

// 创建工作流任务
export function createWorkflowTask(data: WorkflowTaskCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowTask',
    method: 'post',
    data
  })
}

// 更新工作流任务
export function updateWorkflowTask(data: WorkflowTaskUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowTask',
    method: 'put',
    data
  })
}

// 删除工作流任务
export function deleteWorkflowTask(id: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowTask/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流任务
export function batchDeleteWorkflowTask(ids: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowTask/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流任务
export function importWorkflowTask(tasks: any[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtWorkflowTask/import',
    method: 'post',
    data: tasks
  })
}

// 导出工作流任务
export function exportWorkflowTask(params: WorkflowTaskQuery) {
  return request({
    url: '/api/HbtWorkflowTask/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

// 获取工作流任务导入模板
export function getWorkflowTaskTemplate() {
  return request({
    url: '/api/HbtWorkflowTask/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新工作流任务状态
export function updateWorkflowTaskStatus(id: number, data: WorkflowTaskStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowTask/${id}/status`,
    method: 'put',
    data
  })
}

// 完成工作流任务
export function completeWorkflowTask(id: number, result: string, comment: string) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowTask/${id}/complete`,
    method: 'post',
    params: { result, comment }
  })
}

// 转办工作流任务
export function transferWorkflowTask(id: number, assigneeId: number, comment: string) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowTask/${id}/transfer`,
    method: 'post',
    params: { assigneeId, comment }
  })
}

// 退回工作流任务
export function rejectWorkflowTask(id: number, comment: string) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowTask/${id}/reject`,
    method: 'post',
    params: { comment }
  })
}

// 撤销工作流任务
export function cancelWorkflowTask(id: number, comment: string) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtWorkflowTask/${id}/cancel`,
    method: 'post',
    params: { comment }
  })
} 