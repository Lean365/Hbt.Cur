import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 工作流实例实体
 */
export interface HbtInstance extends HbtBaseEntity {
  /** 工作流实例ID */
  instanceId: number
  /** 流程定义ID */
  schemeId: number
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey?: string
  /** 发起人ID */
  initiatorId: number
  /** 当前节点ID */
  currentNodeId?: string
  /** 当前节点名称 */
  currentNodeName?: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止) */
  status: number
  /** 优先级(1:低 2:普通 3:高 4:紧急 5:特急) */
  priority: number
  /** 紧急程度(1:普通 2:加急 3:特急) */
  urgency: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
  /** 流程变量(JSON格式) */
  variables?: string
  /** 备注 */
  remark?: string
}

/**
 * 工作流实例查询参数
 */
export interface HbtInstanceQuery extends HbtPagedQuery {
  /** 实例标题 */
  instanceTitle?: string
  /** 业务键 */
  businessKey?: string
  /** 流程定义ID */
  schemeId?: number
  /** 发起人ID */
  initiatorId?: number
  /** 状态 */
  status?: number
  /** 优先级 */
  priority?: number
  /** 紧急程度 */
  urgency?: number
}

/**
 * 工作流实例创建参数
 */
export interface HbtInstanceCreate {
  /** 流程定义ID */
  schemeId: number
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey?: string
  /** 发起人ID */
  initiatorId: number
  /** 优先级(1:低 2:普通 3:高 4:紧急 5:特急) */
  priority: number
  /** 紧急程度(1:普通 2:加急 3:特急) */
  urgency: number
  /** 流程变量(JSON格式) */
  variables?: string
  /** 备注 */
  remark?: string
}

/**
 * 工作流实例更新参数
 */
export interface HbtInstanceUpdate extends HbtInstanceCreate {
  /** 工作流实例ID */
  instanceId: number
}

/**
 * 工作流实例状态更新参数
 */
export interface HbtInstanceStatus {
  /** 工作流实例ID */
  instanceId: number
  /** 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止) */
  status: number
}

/**
 * 工作流实例启动参数
 */
export interface HbtInstanceStart {
  /** 流程定义ID */
  schemeId: number
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey?: string
  /** 流程变量(JSON格式) */
  variables?: string
}

/**
 * 工作流实例模板参数
 */
export interface HbtInstanceTemplate {
  /** 流程定义ID */
  schemeId: number
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey?: string
  /** 发起人ID */
  initiatorId: number
  /** 流程变量(JSON格式) */
  variables?: string
}

/**
 * 工作流实例导入参数
 */
export interface HbtInstanceImport {
  /** 流程定义ID */
  schemeId: number
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey?: string
  /** 发起人ID */
  initiatorId: number
  /** 流程变量(JSON格式) */
  variables?: string
}

/**
 * 工作流实例导出参数
 */
export interface HbtInstanceExport {
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey: string
  /** 发起人ID */
  initiatorId: number
  /** 状态 */
  status: string
  /** 优先级 */
  priority: string
  /** 紧急程度 */
  urgency: string
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流实例分页结果
 */
export interface HbtInstancePagedResult extends HbtPagedResult<HbtInstance> {}
