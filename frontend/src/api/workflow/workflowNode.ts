import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  WorkflowNodeQuery, 
  WorkflowNode,
  WorkflowNodeCreate,
  WorkflowNodeUpdate
} from '@/types/workflow/workflowNode'

// 获取工作流节点列表
export function getWorkflowNodeList(params: WorkflowNodeQuery) {
  return request<HbtApiResponse<WorkflowNode[]>>({
    url: '/api/HbtWorkflowNode',
    method: 'get',
    params
  })
}

// 获取工作流节点详情
export function getWorkflowNode(id: number) {
  return request<HbtApiResponse<WorkflowNode>>({
    url: `/api/HbtWorkflowNode/${id}`,
    method: 'get'
  })
}

// 创建工作流节点
export function createWorkflowNode(data: WorkflowNodeCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowNode',
    method: 'post',
    data
  })
}

// 更新工作流节点
export function updateWorkflowNode(data: WorkflowNodeUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowNode',
    method: 'put',
    data
  })
}

// 删除工作流节点
export function deleteWorkflowNode(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowNode/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流节点
export function batchDeleteWorkflowNode(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowNode/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流节点
export function importWorkflowNode(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowNode/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流节点
export function exportWorkflowNode(params: WorkflowNodeQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowNode/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流节点导入模板
export function getWorkflowNodeTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowNode/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 获取工作流定义的所有节点
export function getNodesByWorkflowDefinition(workflowDefinitionId: number) {
  return request<HbtApiResponse<WorkflowNode[]>>({
    url: `/api/HbtWorkflowNode/definition/${workflowDefinitionId}`,
    method: 'get'
  })
}

// 获取指定节点的子节点列表
export function getChildNodes(nodeId: number) {
  return request<HbtApiResponse<WorkflowNode[]>>({
    url: `/api/HbtWorkflowNode/${nodeId}/children`,
    method: 'get'
  })
}

// 更新节点排序号
export function updateNodeSort(id: number, sort: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowNode/${id}/sort`,
    method: 'put',
    params: { sort }
  })
} 