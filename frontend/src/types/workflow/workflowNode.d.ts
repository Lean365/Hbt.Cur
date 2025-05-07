/**
 * 工作流节点相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtWorkflowDefinition } from './workflowDefinition'

/**
 * 工作流节点查询参数
 */
export interface HbtWorkflowNodeQuery extends HbtPagedQuery {
  workflowDefinitionId?: number
  nodeName?: string
  nodeType?: number
  parentNodeId?: number
  startTime?: string
  endTime?: string
}

/**
 * 工作流节点数据传输对象
 */
export interface HbtWorkflowNode extends HbtBaseEntity {
  NodeName: string
  nodeType: number
  workflowDefinitionId: number
  parentNodeId?: number
  nodeConfig: string
  orderNum: number
  workflowDefinition?: HbtWorkflowDefinition
  childNodes?: HbtWorkflowNode[]
}

/**
 * 工作流节点创建参数
 */
export interface HbtWorkflowNodeCreate {
  NodeName: string
  nodeType: number
  workflowDefinitionId: number
  parentNodeId?: number
  nodeConfig: string
  orderNum: number
  remark?: string
}

/**
 * 工作流节点更新参数
 */
export interface HbtWorkflowNodeUpdate extends HbtWorkflowNodeCreate {
  id: string
}

/**
 * 工作流节点状态更新参数
 */
export interface HbtWorkflowNodeStatus {
  id: string
  status: number
}

/**
 * 工作流节点导入参数
 */
export interface HbtWorkflowNodeImport {
  file: File
}

/**
 * 工作流节点导出参数
 */
export interface HbtWorkflowNodeExport extends HbtWorkflowNodeQuery {
  orderByColumn?: string
  isAsc?: string
}

/**
 * 工作流节点模板参数
 */
export interface HbtWorkflowNodeTemplate {
  NodeName: string
  nodeType: number
  workflowDefinitionId: number
  parentNodeId?: number
  nodeConfig: string
  orderNum: number
}

/**
 * 工作流节点分页结果
 */
export type HbtWorkflowNodePagedResult = HbtPagedResult<HbtWorkflowNode>
