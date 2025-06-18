/**
 * 工作流任务相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtInstance } from './instance'
import type { HbtNode } from './node'



/**
 * 工作流任务数据传输对象
 */
export interface HbtTask extends HbtBaseEntity {
  taskId: number
  instanceId: number
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
  workflowInstance?: HbtInstance
  node?: HbtNode
}
/**
 * 工作流任务查询参数
 */
export interface HbtTaskQuery extends HbtPagedQuery {
  instanceId?: number
  nodeId?: number
  taskTitle?: string
  taskType?: number
  status?: number
  assigneeId?: number
  startTime?: string
  endTime?: string
}
/**
 * 工作流任务创建参数
 */
export interface HbtTaskCreate {
  instanceId: number
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
export interface HbtTaskUpdate extends HbtTaskCreate {
  taskId: number
}

/**
 * 工作流任务状态更新参数
 */
export interface HbtTaskStatus {
  taskId: number
  status: number
}

/**
 * 工作流任务导入参数
 */
export interface HbtTaskImport {
  instanceId: number
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
 * 工作流任务导出参数
 */
export interface HbtTaskExport extends HbtTaskQuery {
  instanceId: number
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
 * 工作流任务模板参数
 */
export interface HbtTaskTemplate {
  instanceId: number
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
export type HbtTaskPagedResult = HbtPagedResult<HbtTask>
