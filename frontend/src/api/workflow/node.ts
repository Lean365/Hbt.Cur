import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtNodeQuery, 
  HbtNode,
  HbtNodeCreate,
  HbtNodeUpdate,
  HbtNodePagedResult
} from '@/types/workflow/node'

// 获取工作流节点列表
export function getWorkflowNodeList(params: HbtNodeQuery) {
  return request<HbtApiResponse<HbtNodePagedResult>>({
    url: '/api/HbtNode/list',
    method: 'get',
    params
  })
}

// 获取工作流节点详情
export function getWorkflowNode(id: number) {
  return request<HbtApiResponse<HbtNode>>({
    url: `/api/HbtNode/${id}`,
    method: 'get'
  })
}

// 创建工作流节点
export function createWorkflowNode(data: HbtNodeCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtNode',
    method: 'post',
    data
  })
}

// 更新工作流节点
export function updateWorkflowNode(data: HbtNodeUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtNode',
    method: 'put',
    data
  })
}

// 删除工作流节点
export function deleteWorkflowNode(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtNode/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流节点
export function batchDeleteWorkflowNode(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtNode/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流节点
export function importWorkflowNode(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtNode/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流节点
export function exportWorkflowNode(params: HbtNodeQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtNode/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流节点导入模板
export function getWorkflowNodeTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtNode/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 获取工作流定义的所有节点
export function getNodesByWorkflowDefinition(workflowDefinitionId: number) {
  return request<HbtApiResponse<HbtNode[]>>({
    url: `/api/HbtNode/definition/${workflowDefinitionId}`,
    method: 'get'
  })
}

// 获取指定节点的子节点列表
export function getChildNodes(nodeId: number) {
  return request<HbtApiResponse<HbtNode[]>>({
    url: `/api/HbtNode/${nodeId}/children`,
    method: 'get'
  })
}

// 更新节点排序号
export function updateNodeSort(id: number, sort: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtNode/${id}/sort`,
    method: 'put',
    params: { sort }
  })
} 