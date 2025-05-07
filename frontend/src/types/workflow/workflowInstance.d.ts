/**
 * 工作流实例相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtWorkflowDefinition } from './workflowDefinition'
import type { HbtWorkflowNode } from './workflowNode'

/**
 * 工作流实例查询参数
 */
export interface HbtWorkflowInstanceQuery extends HbtPagedQuery {
  workflowDefinitionId?: number
  workflowTitle?: string
  currentNodeId?: number
  initiatorId?: number
  status?: number
  startTime?: string
  endTime?: string
}

/**
 * 工作流实例数据传输对象
 */
export interface HbtWorkflowInstance extends HbtBaseEntity {
  workflowDefinitionId: number
  workflowTitle: string
  currentNodeId: number
  initiatorId: number
  formData: string
  status: number
  startTime: string
  endTime?: string
  workflowDefinition?: HbtWorkflowDefinition
  currentNode?: HbtWorkflowNode
}

/**
 * 工作流实例创建参数
 */
export interface HbtWorkflowInstanceCreate {
  workflowDefinitionId: number
  workflowTitle: string
  currentNodeId: number
  initiatorId: number
  formData: string
  status: number
  startTime: string
  endTime?: string
}

/**
 * 工作流实例更新参数
 */
export interface HbtWorkflowInstanceUpdate extends HbtWorkflowInstanceCreate {
  id: string
}

/**
 * 工作流实例状态更新参数
 */
export interface HbtWorkflowInstanceStatus {
  id: string
  status: number
}

/**
 * 工作流实例导入参数
 */
export interface HbtWorkflowInstanceImport {
  file: File
}

/**
 * 工作流实例导出参数
 */
export interface HbtWorkflowInstanceExport extends HbtWorkflowInstanceQuery {
  orderByColumn?: string
  isAsc?: string
}

/**
 * 工作流实例模板参数
 */
export interface HbtWorkflowInstanceTemplate {
  workflowDefinitionId: number
  workflowTitle: string
  currentNodeId: number
  initiatorId: number
  formData: string
  status: number
  startTime: string
  endTime?: string
}

/**
 * 工作流实例分页结果
 */
export type HbtWorkflowInstancePagedResult = HbtPagedResult<HbtWorkflowInstance>
