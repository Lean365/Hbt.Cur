import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  HbtVariableQuery, 
  HbtVariable,
  HbtVariableCreate,
  HbtVariableUpdate,
  HbtVariablePagedResult
} from '@/types/workflow/variable'

// 获取工作流变量列表
export function getWorkflowVariableList(params: HbtVariableQuery) {
  return request<HbtApiResponse<HbtVariablePagedResult>>({
    url: '/api/HbtVariable/list',
    method: 'get',
    params
  })
}

// 获取工作流变量详情
export function getWorkflowVariable(id: number) {
  return request<HbtApiResponse<HbtVariable>>({
    url: `/api/HbtVariable/${id}`,
    method: 'get'
  })
}

// 创建工作流变量
export function createWorkflowVariable(data: HbtVariableCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtVariable',
    method: 'post',
    data
  })
}

// 更新工作流变量
export function updateWorkflowVariable(data: HbtVariableUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtVariable',
    method: 'put',
    data
  })
}

// 删除工作流变量
export function deleteWorkflowVariable(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtVariable/${id}`,
    method: 'delete'
  })
}

// 批量删除工作流变量
export function batchDeleteWorkflowVariable(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtVariable/batch',
    method: 'delete',
    data: ids
  })
}

// 导入工作流变量
export function importWorkflowVariable(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtVariable/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流变量
export function exportWorkflowVariable(params: HbtVariableQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtVariable/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取工作流变量导入模板
export function getWorkflowVariableTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtVariable/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 获取工作流实例的所有变量
export function getVariablesByWorkflowInstance(workflowInstanceId: number) {
  return request<HbtApiResponse<HbtVariable[]>>({
    url: `/api/HbtVariable/instance/${workflowInstanceId}`,
    method: 'get'
  })
}

// 获取工作流节点的所有变量
export function getVariablesByWorkflowNode(workflowNodeId: number) {
  return request<HbtApiResponse<HbtVariable[]>>({
    url: `/api/HbtVariable/node/${workflowNodeId}`,
    method: 'get'
  })
}

// 获取工作流变量值
export function getVariableValue(workflowInstanceId: number, variableName: string) {
  return request<HbtApiResponse<string>>({
    url: '/api/HbtVariable/value',
    method: 'get',
    params: { workflowInstanceId, variableName }
  })
}

// 设置工作流变量值
export function setVariableValue(workflowInstanceId: number, variableName: string, variableValue: string) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtVariable/value',
    method: 'put',
    params: { workflowInstanceId, variableName },
    data: variableValue
  })
} 