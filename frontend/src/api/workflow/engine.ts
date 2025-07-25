import request from '@/utils/request'
import type { 
  HbtWorkflowStart, 
  HbtWorkflowApprove, 
  HbtInstanceStatus, 
  HbtTransition, 
  HbtNode,
  HbtTransitionQuery,
  HbtTransitionPagedResult
} from '@/types/workflow/engine'
import type { HbtInstanceTrans } from '@/types/workflow/trans'
import type { HbtInstanceOper } from '@/types/workflow/oper'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'

// 启动工作流实例
export function startWorkflow(data: HbtWorkflowStart) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtEngine/start',
    method: 'post',
    data
  })
}

// 暂停工作流实例
export function suspendWorkflow(instanceId: number, reason: string = '手动暂停') {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtEngine/${instanceId}/suspend`,
    method: 'post',
    params: { reason }
  })
}

// 恢复工作流实例
export function resumeWorkflow(instanceId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtEngine/${instanceId}/resume`,
    method: 'post'
  })
}

// 终止工作流实例
export function terminateWorkflow(instanceId: number, reason: string) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtEngine/${instanceId}/terminate`,
    method: 'post',
    params: { reason }
  })
}

// 审批工作流
export function approveWorkflow(data: HbtWorkflowApprove) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtEngine/approve',
    method: 'post',
    data
  })
}

// 获取工作流实例状态
export function getWorkflowStatus(instanceId: number) {
  return request<HbtApiResponse<HbtInstanceStatus>>({
    url: `/api/HbtEngine/${instanceId}/status`,
    method: 'get'
  })
}

// 获取工作流实例可用转换列表
export function getAvailableTransitions(instanceId: number) {
  return request<HbtApiResponse<HbtTransition[]>>({
    url: `/api/HbtEngine/${instanceId}/transitions`,
    method: 'get'
  })
}

// 获取工作流实例当前节点信息
export function getCurrentNode(instanceId: number) {
  return request<HbtApiResponse<HbtNode>>({
    url: `/api/HbtEngine/${instanceId}/current-node`,
    method: 'get'
  })
}

// 获取工作流实例变量
export function getWorkflowVariables(instanceId: number) {
  return request<HbtApiResponse<Record<string, any>>>({
    url: `/api/HbtEngine/${instanceId}/variables`,
    method: 'get'
  })
}

// 设置工作流实例变量
export function setWorkflowVariables(instanceId: number, variables: Record<string, any>) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtEngine/${instanceId}/variables`,
    method: 'put',
    data: variables
  })
}

// 获取工作流实例流转历史
export function getWorkflowHistory(instanceId: number) {
  return request<HbtApiResponse<HbtInstanceTrans[]>>({
    url: `/api/HbtEngine/${instanceId}/history`,
    method: 'get'
  })
}

// 获取工作流实例操作记录
export function getWorkflowOperations(instanceId: number) {
  return request<HbtApiResponse<HbtInstanceOper[]>>({
    url: `/api/HbtEngine/${instanceId}/operations`,
    method: 'get'
  })
}

// 转换历史相关API

// 获取转换历史列表
export function getTransitionList(params: HbtTransitionQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtTransition>>>({
    url: '/api/HbtEngine/transition/list',
    method: 'get',
    params
  })
}

// 获取转换历史详情
export function getTransition(transitionId: string) {
  return request<HbtApiResponse<HbtTransition>>({
    url: `/api/HbtEngine/transition/${transitionId}`,
    method: 'get'
  })
} 