/**
 * 工作流变量相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtInstance } from './instance'
import type { HbtNode } from './node'



/**
 * 工作流变量数据传输对象
 */
export interface HbtVariable extends HbtBaseEntity {
  variableId: number
  workflowInstanceId: number
  variableName: string
  variableType: string
  variableValue: string
  scope: number
  nodeId?: number
  workflowInstance?: HbtInstance
  node?: HbtNode
}
/**
 * 工作流变量查询参数
 */
export interface HbtVariableQuery extends HbtPagedQuery {
  workflowInstanceId?: number
  variableName?: string
  variableType?: string
  scope?: number
  nodeId?: number
}
/**
 * 工作流变量创建参数
 */
export interface HbtVariableCreate {
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
export interface HbtVariableUpdate extends HbtVariableCreate {
  variableId: number
}

/**
 * 工作流变量状态更新参数
 */
export interface HbtVariableStatus {
  variableId: number
  status: number
}

/**
 * 工作流变量导入参数
 */
export interface HbtVariableImport {
  workflowInstanceId: number
  variableName: string
  variableType: string
  variableValue: string
  scope: number
  nodeId?: number
}

/**
 * 工作流变量导出参数
 */
export interface HbtVariableExport extends HbtVariableQuery {
  variableName: string
  variableType: string
  variableValue: string
  scope: number
  nodeId?: number
}

/**
 * 工作流变量模板参数
 */
export interface HbtVariableTemplate {
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
export type HbtVariablePagedResult = HbtPagedResult<HbtVariable>
