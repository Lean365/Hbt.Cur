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
  processTaskId: number
  taskId?: number // 前端兼容字段，映射到processTaskId
  instanceId: number
  instanceName: string
  nodeId: number
  nodeName: string
  taskName: string
  taskType: number
  status: number
  assigneeId?: number
  assigneeName: string
  comment?: string
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
  taskName?: string
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
  taskName: string
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
  processTaskId: number
}

/**
 * 工作流任务状态更新参数
 */
export interface HbtTaskStatus {
  processTaskId: number
  status: number
}

/**
 * 工作流任务导入参数
 */
export interface HbtTaskImport {
  instanceId: number
  nodeId: number
  taskName: string
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
  taskName: string
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
  taskName: string
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
