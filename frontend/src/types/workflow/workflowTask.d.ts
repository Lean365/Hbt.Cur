/**
 * 工作流任务相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtWorkflowInstance } from './workflowInstance'
import type { HbtWorkflowNode } from './workflowNode'

/**
 * 工作流任务查询参数
 */
export interface HbtWorkflowTaskQuery extends HbtPagedQuery {
  workflowInstanceId?: number
  nodeId?: number
  taskTitle?: string
  taskType?: number
  status?: number
  assigneeId?: number
  startTime?: string
  endTime?: string
}

/**
 * 工作流任务数据传输对象
 */
export interface HbtWorkflowTask extends HbtBaseEntity {
  workflowInstanceId: number
  nodeId: number
  taskTitle: string
  taskType: number
  status: number
  assigneeId?: number
  comment?: string
  result?: string
  completeTime?: string
  dueTime?: string
  reminderTime?: string
  priority: number
  workflowInstance?: HbtWorkflowInstance
  node?: HbtWorkflowNode
}

/**
 * 工作流任务创建参数
 */
export interface HbtWorkflowTaskCreate {
  workflowInstanceId: number
  nodeId: number
  taskTitle: string
  taskType: number
  status: number
  assigneeId?: number
  comment?: string
  result?: string
  completeTime?: string
  dueTime?: string
  reminderTime?: string
  priority: number
  remark?: string
}

/**
 * 工作流任务更新参数
 */
export interface HbtWorkflowTaskUpdate extends HbtWorkflowTaskCreate {
  id: string
}

/**
 * 工作流任务状态更新参数
 */
export interface HbtWorkflowTaskStatus {
  id: string
  status: number
}

/**
 * 工作流任务导入参数
 */
export interface HbtWorkflowTaskImport {
  file: File
}

/**
 * 工作流任务导出参数
 */
export interface HbtWorkflowTaskExport extends HbtWorkflowTaskQuery {
  orderByColumn?: string
  isAsc?: string
}

/**
 * 工作流任务模板参数
 */
export interface HbtWorkflowTaskTemplate {
  workflowInstanceId: number
  nodeId: number
  taskTitle: string
  taskType: number
  status: number
  assigneeId?: number
  comment?: string
  result?: string
  completeTime?: string
  dueTime?: string
  reminderTime?: string
  priority: number
}

/**
 * 工作流任务分页结果
 */
export type HbtWorkflowTaskPagedResult = HbtPagedResult<HbtWorkflowTask>
