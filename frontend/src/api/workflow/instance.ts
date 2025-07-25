import request from '@/utils/request'
import type { HbtInstanceQuery, HbtInstance, HbtInstanceStatus, HbtInstanceUpdate, HbtInstanceCreate } from '@/types/workflow/instance'
import type { HbtPagedResult } from '@/types/common'
import type { HbtApiResponse } from '@/types/common'

// 获取工作流实例分页列表
export function getInstanceList(query: HbtInstanceQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstance>>>({
    url: '/api/HbtInstance/list',
    method: 'get',
    params: query
  })
}

// 获取工作流实例详情
export function getInstanceById(instanceId: number) {
  return request<HbtApiResponse<HbtInstance>>({
    url: `/api/HbtInstance/${instanceId}`,
    method: 'get'
  })
}

// 根据业务键获取工作流实例
export function getInstanceByBusinessKey(businessKey: string) {
  return request<HbtApiResponse<HbtInstance>>({
    url: `/api/HbtInstance/business/${businessKey}`,
    method: 'get'
  })
}

// 创建工作流实例
export function createInstance(data: HbtInstanceCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtInstance',
    method: 'post',
    data
  })
}

// 更新工作流实例
export function updateInstance(data: HbtInstanceUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtInstance',
    method: 'put',
    data
  })
}

// 删除工作流实例
export function deleteInstance(instanceId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtInstance/${instanceId}`,
    method: 'delete'
  })
}

// 批量删除工作流实例
export function batchDeleteInstance(instanceIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtInstance/batch',
    method: 'delete',
    data: instanceIds
  })
}

// 导入工作流实例
export function importInstance(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtInstance/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流实例
export function exportInstance(query: HbtInstanceQuery) {
  return request<Blob>({
    url: '/api/HbtInstance/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getInstanceTemplate() {
  return request<Blob>({
    url: '/api/HbtInstance/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新工作流实例状态
export function updateInstanceStatus(data: HbtInstanceStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtInstance/${data.instanceId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 设置工作流实例变量
export function setInstanceVariables(instanceId: number, variables: Record<string, any>) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtInstance/${instanceId}/variables`,
    method: 'put',
    data: variables
  })
}

// 获取我的工作流实例列表
export function getMyInstances(query: HbtInstanceQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstance>>>({
    url: '/api/HbtInstance/my',
    method: 'get',
    params: query
  })
}
