import request from '@/utils/request'
import type { HbtInstanceTransQuery, HbtInstanceTrans, HbtInstanceTransUpdate, HbtInstanceTransCreate } from '@/types/workflow/trans'
import type { HbtPagedResult } from '@/types/common'
import type { HbtApiResponse } from '@/types/common'

// 获取工作流实例流转历史分页列表
export function getInstanceTransList(query: HbtInstanceTransQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceTrans>>>({
    url: '/api/HbtInstanceTrans/list',
    method: 'get',
    params: query
  })
}

// 获取工作流实例流转历史详情
export function getInstanceTransById(instanceTransId: number) {
  return request<HbtApiResponse<HbtInstanceTrans>>({
    url: `/api/HbtInstanceTrans/${instanceTransId}`,
    method: 'get'
  })
}

// 创建工作流实例流转历史
export function createInstanceTrans(data: HbtInstanceTransCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtInstanceTrans',
    method: 'post',
    data
  })
}

// 更新工作流实例流转历史
export function updateInstanceTrans(data: HbtInstanceTransUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtInstanceTrans',
    method: 'put',
    data
  })
}

// 删除工作流实例流转历史
export function deleteInstanceTrans(instanceTransId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtInstanceTrans/${instanceTransId}`,
    method: 'delete'
  })
}

// 批量删除工作流实例流转历史
export function batchDeleteInstanceTrans(instanceTransIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtInstanceTrans/batch',
    method: 'delete',
    data: instanceTransIds
  })
}

// 导入工作流实例流转历史
export function importInstanceTrans(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtInstanceTrans/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流实例流转历史
export function exportInstanceTrans(query: HbtInstanceTransQuery) {
  return request<Blob>({
    url: '/api/HbtInstanceTrans/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getInstanceTransTemplate() {
  return request<Blob>({
    url: '/api/HbtInstanceTrans/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 获取工作流实例流转历史选项列表
export function getInstanceTransOptions() {
  return request<HbtApiResponse<{ label: string; value: number }[]>>({
    url: '/api/HbtInstanceTrans/options',
    method: 'get'
  })
}

// 获取工作流实例的流转历史列表
export function getInstanceTransListByInstance(instanceId: number, query: HbtInstanceTransQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceTrans>>>({
    url: `/api/HbtInstanceTrans/instance/${instanceId}`,
    method: 'get',
    params: query
  })
}

// 获取工作流实例流转路径
export function getInstanceTransPath(instanceId: number) {
  return request<HbtApiResponse<HbtInstanceTrans[]>>({
    url: `/api/HbtInstanceTrans/instance/${instanceId}/path`,
    method: 'get'
  })
}

// 获取工作流实例流转统计
export function getInstanceTransStats(instanceId: number) {
  return request<HbtApiResponse<{
    totalNodes: number
    completedNodes: number
    pendingNodes: number
    failedNodes: number
  }>>({
    url: `/api/HbtInstanceTrans/instance/${instanceId}/stats`,
    method: 'get'
  })
}

// 获取我的待办任务列表
export function getMyTodoList(query: HbtInstanceTransQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceTrans>>>({
    url: '/api/HbtInstanceTrans/my/todo',
    method: 'get',
    params: query
  })
}

// 获取我的已办任务列表
export function getMyDoneList(query: HbtInstanceTransQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceTrans>>>({
    url: '/api/HbtInstanceTrans/my/done',
    method: 'get',
    params: query
  })
}

// 获取我的流程列表
export function getMyProcessList(query: HbtInstanceTransQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceTrans>>>({
    url: '/api/HbtInstanceTrans/my/process',
    method: 'get',
    params: query
  })
}
