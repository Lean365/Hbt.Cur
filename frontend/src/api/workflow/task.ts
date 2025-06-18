import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtTaskQuery, 
  HbtTask,
  HbtTaskCreate,
  HbtTaskUpdate,
  HbtTaskStatus,
  HbtTaskPagedResult
} from '@/types/workflow/task'

// 获取工作流任务列表
export function getWorkflowTaskList(params: HbtTaskQuery) {
  return request<HbtApiResponse<HbtTaskPagedResult>>({
    url: '/api/HbtTask/list',
    method: 'get',
    params
  })
}

// 获取工作流任务详情
export function getWorkflowTask(id: number) {
  return request<HbtApiResponse<HbtTask>>({
    url: `/api/HbtTask/${id}`,
    method: 'get'
  })
}

// 创建工作流任务
export function createWorkflowTask(data: HbtTaskCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtTask',
    method: 'post',
    data
  })
}

// 更新工作流任务
export function updateWorkflowTask(data: HbtTaskUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtTask',
    method: 'put',
    data
  })
}

// 删除工作流任务
export function deleteWorkflowTask(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtTask/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流任务
export function batchDeleteWorkflowTask(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtTask/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流任务
export function importWorkflowTask(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtTask/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流任务
export function exportWorkflowTask(params: HbtTaskQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtTask/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流任务导入模板
export function getWorkflowTaskTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtTask/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 更新工作流任务状态
export function updateWorkflowTaskStatus(id: number, data: HbtTaskStatus) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtTask/${id}/status`,
    method: 'put',
    data
  })
}

// 完成工作流任务
export function completeWorkflowTask(id: number, result: string, comment: string) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtTask/${id}/complete`,
    method: 'post',
    params: { result, comment }
  })
}

// 转办工作流任务
export function transferWorkflowTask(id: number, assigneeId: number, comment: string) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtTask/${id}/transfer`,
    method: 'post',
    params: { assigneeId, comment }
  })
}

// 退回工作流任务
export function rejectWorkflowTask(id: number, comment: string) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtTask/${id}/reject`,
    method: 'post',
    params: { comment }
  })
}

// 撤销工作流任务
export function cancelWorkflowTask(id: number, comment: string) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtTask/${id}/cancel`,
    method: 'post',
    params: { comment }
  })
} 