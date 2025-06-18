import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtHistoryQuery, 
  HbtHistory,
  HbtHistoryCreate,
  HbtHistoryUpdate,
  HbtHistoryPagedResult
} from '@/types/workflow/history'

// 获取工作流历史列表
export function getWorkflowHistoryList(params: HbtHistoryQuery) {
  return request<HbtApiResponse<HbtHistoryPagedResult>>({
    url: '/api/HbtHistory/list',
    method: 'get',
    params
  })
}

// 获取工作流历史详情
export function getWorkflowHistory(id: number) {
  return request<HbtApiResponse<HbtHistory>>({
    url: `/api/HbtHistory/${id}`,
    method: 'get'
  })
}

// 创建工作流历史
export function createWorkflowHistory(data: HbtHistoryCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtHistory',
    method: 'post',
    data
  })
}

// 更新工作流历史
export function updateWorkflowHistory(data: HbtHistoryUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtHistory',
    method: 'put',
    data
  })
}

// 删除工作流历史
export function deleteWorkflowHistory(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtHistory/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流历史
export function batchDeleteWorkflowHistory(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtHistory/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流历史
export function importWorkflowHistory(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtHistory/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流历史
export function exportWorkflowHistory(params: HbtHistoryQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtHistory/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流历史导入模板
export function getWorkflowHistoryTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtHistory/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 获取工作流实例的历史记录
export function getHistoriesByWorkflowInstance(workflowInstanceId: number) {
  return request<HbtApiResponse<HbtHistory[]>>({
    url: `/api/HbtHistory/instance/${workflowInstanceId}`,
    method: 'get'
  })
}

// 获取工作流节点的历史记录
export function getHistoriesByWorkflowNode(workflowNodeId: number) {
  return request<HbtApiResponse<HbtHistory[]>>({
    url: `/api/HbtHistory/node/${workflowNodeId}`,
    method: 'get'
  })
}

// 获取用户的操作历史记录
export function getHistoriesByOperator(operatorId: number) {
  return request<HbtApiResponse<HbtHistory[]>>({
    url: `/api/HbtHistory/operator/${operatorId}`,
    method: 'get'
  })
}

// 清理历史记录
export function cleanupHistories(days: number) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtHistory/cleanup',
    method: 'post',
    params: { days }
  })
} 