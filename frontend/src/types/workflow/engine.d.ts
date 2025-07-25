import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 工作流转换历史实体
 */
export interface HbtTransition extends HbtBaseEntity {
  /** 转换ID */
  instanceTransId: string
  /** 实例ID */
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
 * 工作流节点实体
 */
export interface HbtNode extends HbtBaseEntity {
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 节点类型 */
  nodeType: string
  /** 节点配置(JSON格式) */
  nodeConfig?: string
  /** 审批人类型(1:指定用户 2:角色 3:部门 4:动态) */
  approverType: number
  /** 审批人配置(JSON格式) */
  approverConfig?: string
}

/**
 * 工作流审批参数
 */
export interface HbtWorkflowApprove {
  /** 工作流实例ID */
  instanceId: number
  /** 节点ID */
  nodeId: string
  /** 操作类型(1:同意 2:拒绝 3:退回 4:转办 5:委托) */
  operType: number
  /** 操作意见 */
  operOpinion: string
  /** 操作数据(JSON格式) */
  operData?: string
  /** 目标用户ID(转办/委托时使用) */
  targetUserId?: number
}

/**
 * 工作流启动参数
 */
export interface HbtWorkflowStart {
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
 * 工作流实例状态参数
 */
export interface HbtInstanceStatus {
  /** 工作流实例ID */
  instanceId: number
  /** 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止) */
  status: number
}

/**
 * 工作流转换查询参数
 */
export interface HbtTransitionQuery extends HbtPagedQuery {
  /** 实例ID */
  instanceId?: number
  /** 开始节点ID */
  startNodeId?: string
  /** 目标节点ID */
  toNodeId?: string
  /** 转化状态 */
  transState?: number
  /** 是否完成 */
  isFinish?: number
}

/**
 * 工作流节点查询参数
 */
export interface HbtNodeQuery extends HbtPagedQuery {
  /** 节点名称 */
  nodeName?: string
  /** 节点类型 */
  nodeType?: string
  /** 审批人类型 */
  approverType?: number
}



/**
 * 工作流节点创建参数
 */
export interface HbtNodeCreate {
  /** 节点名称 */
  nodeName: string
  /** 节点类型 */
  nodeType: string
  /** 节点配置(JSON格式) */
  nodeConfig?: string
  /** 审批人类型(1:指定用户 2:角色 3:部门 4:动态) */
  approverType: number
  /** 审批人配置(JSON格式) */
  approverConfig?: string
}

/**
 * 工作流节点更新参数
 */
export interface HbtNodeUpdate extends HbtNodeCreate {
  /** 节点ID */
  nodeId: string
}

/**
 * 工作流转换分页结果
 */
export interface HbtTransitionPagedResult extends HbtPagedResult<HbtTransition> {}

/**
 * 工作流节点分页结果
 */
export interface HbtNodePagedResult extends HbtPagedResult<HbtNode> {} 