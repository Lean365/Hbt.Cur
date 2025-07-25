import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 工作流实例流转历史实体
 */
export interface HbtInstanceTrans extends HbtBaseEntity {
  /** 工作流实例流转历史ID */
  instanceTransId: number
  /** 工作流实例ID */
  instanceId: number
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点类型 */
  startNodeType: number
  /** 开始节点名称 */
  startNodeName: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点类型 */
  toNodeType: number
  /** 目标节点名称 */
  toNodeName: string
  /** 转化状态 */
  transState: number
  /** 是否完成 */
  isFinish: number
  /** 转化时间 */
  transTime: string
}

/**
 * 工作流实例流转历史查询参数
 */
export interface HbtInstanceTransQuery extends HbtPagedQuery {
  /** 工作流实例ID */
  instanceId?: number
  /** 开始节点ID */
  startNodeId?: string
  /** 开始节点名称 */
  startNodeName?: string
  /** 开始节点类型 */
  startNodeType?: number
  /** 目标节点ID */
  toNodeId?: string
  /** 目标节点名称 */
  toNodeName?: string
  /** 目标节点类型 */
  toNodeType?: number
  /** 转化状态 */
  transState?: number
  /** 是否完成 */
  isFinish?: number
  /** 创建人（用于查询我的待办/已办） */
  createBy?: string
}

/**
 * 工作流实例流转历史创建参数
 */
export interface HbtInstanceTransCreate {
  /** 工作流实例ID */
  instanceId: number
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点类型 */
  startNodeType: number
  /** 开始节点名称 */
  startNodeName: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点类型 */
  toNodeType: number
  /** 目标节点名称 */
  toNodeName: string
  /** 转化状态 */
  transState: number
  /** 是否完成 */
  isFinish: number
  /** 转化时间 */
  transTime: string
}

/**
 * 工作流实例流转历史更新参数
 */
export interface HbtInstanceTransUpdate extends HbtInstanceTransCreate {
  /** 工作流实例流转历史ID */
  instanceTransId: number
}

/**
 * 工作流实例流转历史模板参数
 */
export interface HbtInstanceTransTemplate {
  /** 工作流实例ID */
  instanceId: number
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点类型 */
  startNodeType: number
  /** 开始节点名称 */
  startNodeName: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点类型 */
  toNodeType: number
  /** 目标节点名称 */
  toNodeName: string
  /** 转化状态 */
  transState: number
  /** 是否完成 */
  isFinish: number
  /** 转化时间 */
  transTime: string
}

/**
 * 工作流实例流转历史导入参数
 */
export interface HbtInstanceTransImport {
  /** 工作流实例ID */
  instanceId: number
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点类型 */
  startNodeType: number
  /** 开始节点名称 */
  startNodeName: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点类型 */
  toNodeType: number
  /** 目标节点名称 */
  toNodeName: string
  /** 转化状态 */
  transState: number
  /** 是否完成 */
  isFinish: number
  /** 转化时间 */
  transTime: string
}

/**
 * 工作流实例流转历史导出参数
 */
export interface HbtInstanceTransExport {
  /** 工作流实例ID */
  instanceId: number
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点类型 */
  startNodeType: number
  /** 开始节点名称 */
  startNodeName: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点类型 */
  toNodeType: number
  /** 目标节点名称 */
  toNodeName: string
  /** 转化状态 */
  transState: number
  /** 是否完成 */
  isFinish: number
  /** 转化时间 */
  transTime: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流实例流转历史分页结果
 */
export interface HbtInstanceTransPagedResult extends HbtPagedResult<HbtInstanceTrans> {}
