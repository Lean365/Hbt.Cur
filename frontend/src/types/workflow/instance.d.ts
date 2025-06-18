/**
 * 工作流实例相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtDefinition } from './definition'
import type { HbtNode } from './node'

/**
 * 工作流实例数据传输对象
 */
export interface HbtInstance extends HbtBaseEntity {
  instanceId: number
  instanceName: string
  businessKey: string
  definitionId: number
  currentNodeId: number
  initiatorId: number
  formData: string
  status: number
  startTime: string
  endTime?: string
  workflowDefinition?: HbtDefinition
  currentNode?: HbtNode
  workflowCategory: string
  workflowVersion: string
  formId: number
  workflowConfig: string
  remark?: string
  workflowNodes: HbtNode[]
}

/**
 * 工作流实例查询参数
 */
export interface HbtInstanceQuery extends HbtPagedQuery {
  instanceName?: string
  definitionId?: number
  businessKey?: string
  currentNodeId?: number
  initiatorId?: number
  status?: number
  startTime?: string
  endTime?: string
}


/**
 * 工作流实例创建参数
 */
export interface HbtInstanceCreate {
  instanceName: string
  businessKey: string
  definitionId: number
  currentNodeId: number
  initiatorId: number
  formData: string
  status: number
  startTime: string
  endTime?: string
  remark?: string
  workflowCategory: string
  workflowVersion: string
  formId: number
  workflowConfig: string
}

/**
 * 工作流实例更新参数
 */
export interface HbtInstanceUpdate extends HbtInstanceCreate {
  instanceId: number
}

/**
 * 工作流实例状态更新参数
 */
export interface HbtInstanceStatus {
  instanceId: number
  status: number
}

/**
 * 工作流实例导入参数
 */
export interface HbtInstanceImport {
  instanceName: string
  businessKey: string
  definitionId: number
  currentNodeId: number
  initiatorId: number
  formData: string
  status: number
  startTime: string
  endTime?: string
}

/**
 * 工作流实例导出参数
 */
export interface HbtInstanceExport extends HbtInstanceQuery {
  instanceName: string
  businessKey: string
  definitionId: number
  currentNodeId: number
  initiatorId: number
  formData: string
  status: number
  startTime: string
  endTime?: string
}

/**
 * 工作流实例模板参数
 */
export interface HbtInstanceTemplate {
  instanceName: string
  businessKey: string
  definitionId: number
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
export type HbtInstancePagedResult = HbtPagedResult<HbtInstance>
