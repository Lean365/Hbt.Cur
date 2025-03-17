import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  WorkflowVariableQuery, 
  WorkflowVariable,
  WorkflowVariableCreate,
  WorkflowVariableUpdate
} from '@/types/workflow/workflowVariable'

// 获取工作流变量列表
export function getWorkflowVariableList(params: WorkflowVariableQuery) {
  return request<HbtApiResponse<WorkflowVariable[]>>({
    url: '/api/HbtWorkflowVariable',
    method: 'get',
    params
  })
}

// 获取工作流变量详情
export function getWorkflowVariable(id: number) {
  return request<HbtApiResponse<WorkflowVariable>>({
    url: `/api/HbtWorkflowVariable/${id}`,
    method: 'get'
  })
}

// 创建工作流变量
export function createWorkflowVariable(data: WorkflowVariableCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowVariable',
    method: 'post',
    data
  })
}

// 更新工作流变量
export function updateWorkflowVariable(data: WorkflowVariableUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowVariable',
    method: 'put',
    data
  })
}

// 删除工作流变量
export function deleteWorkflowVariable(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflowVariable/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流变量
export function batchDeleteWorkflowVariable(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowVariable/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流变量
export function importWorkflowVariable(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowVariable/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流变量
export function exportWorkflowVariable(data: WorkflowVariable[], sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowVariable/export',
    method: 'get',
    params: { data, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流变量导入模板
export function getWorkflowVariableTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtWorkflowVariable/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 获取工作流实例的所有变量
export function getVariablesByWorkflowInstance(workflowInstanceId: number) {
  return request<HbtApiResponse<WorkflowVariable[]>>({
    url: `/api/HbtWorkflowVariable/instance/${workflowInstanceId}`,
    method: 'get'
  })
}

// 获取工作流节点的所有变量
export function getVariablesByWorkflowNode(workflowNodeId: number) {
  return request<HbtApiResponse<WorkflowVariable[]>>({
    url: `/api/HbtWorkflowVariable/node/${workflowNodeId}`,
    method: 'get'
  })
}

// 获取工作流变量值
export function getVariableValue(workflowInstanceId: number, variableName: string) {
  return request<HbtApiResponse<string>>({
    url: '/api/HbtWorkflowVariable/value',
    method: 'get',
    params: { workflowInstanceId, variableName }
  })
}

// 设置工作流变量值
export function setVariableValue(workflowInstanceId: number, variableName: string, variableValue: string) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflowVariable/value',
    method: 'put',
    params: { workflowInstanceId, variableName },
    data: variableValue
  })
} 