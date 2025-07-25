import request from '@/utils/request'
import type { HbtInstanceOperQuery, HbtInstanceOper, HbtInstanceOperUpdate, HbtInstanceOperCreate, HbtInstanceApprove } from '@/types/workflow/oper'
import type { HbtPagedResult } from '@/types/common'
import type { HbtApiResponse } from '@/types/common'

// 获取工作流实例操作记录分页列表
export function getInstanceOperList(query: HbtInstanceOperQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceOper>>>({
    url: '/api/HbtInstanceOper/list',
    method: 'get',
    params: query
  })
}

// 获取工作流实例操作记录详情
export function getInstanceOperById(instanceOperId: number) {
  return request<HbtApiResponse<HbtInstanceOper>>({
    url: `/api/HbtInstanceOper/${instanceOperId}`,
    method: 'get'
  })
}

// 创建工作流实例操作记录
export function createInstanceOper(data: HbtInstanceOperCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtInstanceOper',
    method: 'post',
    data
  })
}

// 更新工作流实例操作记录
export function updateInstanceOper(data: HbtInstanceOperUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtInstanceOper',
    method: 'put',
    data
  })
}

// 删除工作流实例操作记录
export function deleteInstanceOper(instanceOperId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtInstanceOper/${instanceOperId}`,
    method: 'delete'
  })
}

// 批量删除工作流实例操作记录
export function batchDeleteInstanceOper(instanceOperIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtInstanceOper/batch',
    method: 'delete',
    data: instanceOperIds
  })
}

// 工作流审批
export function approveInstance(data: HbtInstanceApprove) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtInstanceOper/approve',
    method: 'post',
    data
  })
}

// 导入工作流实例操作记录
export function importInstanceOper(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtInstanceOper/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流实例操作记录
export function exportInstanceOper(query: HbtInstanceOperQuery) {
  return request<Blob>({
    url: '/api/HbtInstanceOper/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getInstanceOperTemplate() {
  return request<Blob>({
    url: '/api/HbtInstanceOper/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 获取工作流实例操作记录选项列表
export function getInstanceOperOptions() {
  return request<HbtApiResponse<{ label: string; value: number }[]>>({
    url: '/api/HbtInstanceOper/options',
    method: 'get'
  })
}

// 获取我的操作记录列表
export function getMyInstanceOperList(query: HbtInstanceOperQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceOper>>>({
    url: '/api/HbtInstanceOper/my',
    method: 'get',
    params: query
  })
}

// 获取工作流实例的操作记录列表
export function getInstanceOperListByInstance(instanceId: number, query: HbtInstanceOperQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtInstanceOper>>>({
    url: `/api/HbtInstanceOper/instance/${instanceId}`,
    method: 'get',
    params: query
  })
}
