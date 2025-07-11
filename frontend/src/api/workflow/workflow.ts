import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'

// 工作流仪表盘统计数据
export function getWorkflowDashboardStats() {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflow/dashboard-stats',
    method: 'get'
  })
}

// 工作流最近活动
export function getWorkflowRecentActivities() {
  return request<HbtApiResponse<any[]>>({
    url: '/api/HbtWorkflow/recent-activities',
    method: 'get'
  })
}

// 启动工作流实例
export function startWorkflow(data: any) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflow/start',
    method: 'post',
    data
  })
}

// 暂停工作流实例
export function suspendWorkflow(instanceId: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflow/suspend/${instanceId}`,
    method: 'post'
  })
}

// 恢复工作流实例
export function resumeWorkflow(instanceId: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflow/resume/${instanceId}`,
    method: 'post'
  })
}

// 终止工作流实例
export function terminateWorkflow(instanceId: number, reason: string = '用户终止') {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflow/terminate/${instanceId}`,
    method: 'post',
    params: { reason }
  })
}

// 获取工作流实例状态
export function getWorkflowStatus(instanceId: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtWorkflow/status/${instanceId}`,
    method: 'get'
  })
}

// 获取可用转换
export function getAvailableTransitions(instanceId: number) {
  return request<HbtApiResponse<any[]>>({
    url: `/api/HbtWorkflow/transitions/${instanceId}`,
    method: 'get'
  })
}

// 执行工作流转换
export function executeTransition(data: any) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtWorkflow/execute-transition',
    method: 'post',
    data
  })
}

// 获取当前用户的流程实例
export function getUserWorkflows(userId: number, status?: number) {
  return request<HbtApiResponse<any[]>>({
    url: '/api/HbtWorkflow/user-workflows',
    method: 'get',
    params: { userId, status }
  })
}

// 获取当前用户发起的流程实例
export function getUserInitiatedWorkflows(userId: number) {
  return request<HbtApiResponse<any[]>>({
    url: '/api/HbtWorkflow/user-initiated',
    method: 'get',
    params: { userId }
  })
}

// 获取当前用户参与的流程实例
export function getUserParticipatedWorkflows(userId: number) {
  return request<HbtApiResponse<any[]>>({
    url: '/api/HbtWorkflow/user-participated',
    method: 'get',
    params: { userId }
  })
} 