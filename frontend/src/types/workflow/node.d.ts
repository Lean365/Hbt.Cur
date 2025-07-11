/**
 * 工作流节点相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtDefinition } from './definition'



/**
 * 工作流节点数据传输对象
 */
export interface HbtNode extends HbtBaseEntity {
  nodeId: number
  instanceId: number
  nodeName: string
  nodeType: number
  definitionId: number
  definitionName: string
  parentNodeId?: number
  parentNodeName: string
  nodeConfig: string
  orderNum: number
  status: number
  startTime: string
  endTime: string
  workflowDefinition?: HbtDefinition
  childNodes?: HbtNode[]
}
/**
 * 工作流节点查询参数
 */
export interface HbtNodeQuery extends HbtPagedQuery {
  nodeId?: number
  definitionId?: number
  nodeName?: string
  nodeType?: number
  parentNodeId?: number
  status?: number
  startTime?: string
  endTime?: string
}
/**
 * 工作流节点创建参数
 */
export interface HbtNodeCreate {
  instanceId: number
  nodeName: string
  nodeType: number
  definitionId: number
  parentNodeId?: number
  nodeConfig: string
  status: number
  startTime: string
  endTime: string
  orderNum: number
  remark?: string
}

/**
 * 工作流节点更新参数
 */
export interface HbtNodeUpdate extends HbtNodeCreate {
  nodeId: number
}

/**
 * 工作流节点状态更新参数
 */
export interface HbtNodeStatus {
  nodeId: number
  status: number
}

/**
 * 工作流节点导入参数
 */
export interface HbtNodeImport {
  instanceId: number
  nodeName: string
  nodeType: number
  definitionId: number
  parentNodeId?: number
  nodeConfig: string
  status: number
  startTime: string
  endTime: string
  orderNum: number
  remark?: string
}

/**
 * 工作流节点导出参数
 */
export interface HbtNodeExport extends HbtNodeQuery {
  instanceId: number
  nodeName: string
  nodeType: number
  definitionId: number
  parentNodeId?: number
  nodeConfig: string
  status: number
  startTime: string
  endTime: string
  orderNum: number
  remark?: string
}

/**
 * 工作流节点模板参数
 */
export interface HbtNodeTemplate {
  instanceId: number
  nodeName: string
  nodeType: number
  definitionId: number
  parentNodeId?: number
  nodeConfig: string
  status: number
  startTime: string
  endTime: string
  orderNum: number
  remark?: string
}

/**
 * 工作流节点分页结果
 */
export type HbtNodePagedResult = HbtPagedResult<HbtNode>
