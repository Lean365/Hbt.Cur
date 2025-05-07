/**
 * 工作流变量相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtWorkflowInstance } from './workflowInstance'
import type { HbtWorkflowNode } from './workflowNode'

/**
 * 工作流变量查询参数
 */
export interface HbtWorkflowVariableQuery extends HbtPagedQuery {
  workflowInstanceId?: number
  variableName?: string
  variableType?: string
  scope?: number
  nodeId?: number
}

/**
 * 工作流变量数据传输对象
 */
export interface HbtWorkflowVariable extends HbtBaseEntity {
  workflowInstanceId: number
  variableName: string
  variableType: string
  variableValue: string
  scope: number
  nodeId?: number
  workflowInstance?: HbtWorkflowInstance
  node?: HbtWorkflowNode
}

/**
 * 工作流变量创建参数
 */
export interface HbtWorkflowVariableCreate {
  workflowInstanceId: number
  variableName: string
  variableType: string
  variableValue: string
  scope: number
  nodeId?: number
  remark?: string
}

/**
 * 工作流变量更新参数
 */
export interface HbtWorkflowVariableUpdate extends HbtWorkflowVariableCreate {
  id: string
}

/**
 * 工作流变量状态更新参数
 */
export interface HbtWorkflowVariableStatus {
  id: string
  status: number
}

/**
 * 工作流变量导入参数
 */
export interface HbtWorkflowVariableImport {
  file: File
}

/**
 * 工作流变量导出参数
 */
export interface HbtWorkflowVariableExport extends HbtWorkflowVariableQuery {
  orderByColumn?: string
  isAsc?: string
}

/**
 * 工作流变量模板参数
 */
export interface HbtWorkflowVariableTemplate {
  workflowInstanceId: number
  variableName: string
  variableType: string
  variableValue: string
  scope: number
  nodeId?: number
}

/**
 * 工作流变量分页结果
 */
export type HbtWorkflowVariablePagedResult = HbtPagedResult<HbtWorkflowVariable>
