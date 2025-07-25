import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 工作流实例操作记录实体
 */
export interface HbtInstanceOper extends HbtBaseEntity {
  /** 工作流实例操作记录ID */
  instanceOperId: number
  /** 工作流实例ID */
  instanceId: number
  /** 节点ID */
  nodeId?: string
  /** 节点名称 */
  nodeName?: string
  /** 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回) */
  operType: number
  /** 操作人ID */
  operatorId: number
  /** 操作人姓名 */
  operatorName: string
  /** 操作意见 */
  operOpinion?: string
  /** 操作数据(JSON格式) */
  operData?: string
  /** 备注 */
  remark?: string
}

/**
 * 工作流实例操作记录查询参数
 */
export interface HbtInstanceOperQuery extends HbtPagedQuery {
  /** 工作流实例ID */
  instanceId?: number
  /** 节点ID */
  nodeId?: string
  /** 节点名称 */
  nodeName?: string
  /** 操作类型 */
  operType?: number
  /** 操作人ID */
  operatorId?: number
  /** 操作人姓名 */
  operatorName?: string
}

/**
 * 工作流实例操作记录创建参数
 */
export interface HbtInstanceOperCreate {
  /** 工作流实例ID */
  instanceId: number
  /** 节点ID */
  nodeId?: string
  /** 节点名称 */
  nodeName?: string
  /** 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回) */
  operType: number
  /** 操作人ID */
  operatorId: number
  /** 操作人姓名 */
  operatorName: string
  /** 操作意见 */
  operOpinion?: string
  /** 操作数据(JSON格式) */
  operData?: string
  /** 备注 */
  remark?: string
}

/**
 * 工作流实例操作记录更新参数
 */
export interface HbtInstanceOperUpdate extends HbtInstanceOperCreate {
  /** 工作流实例操作记录ID */
  instanceOperId: number
}

/**
 * 工作流审批参数
 */
export interface HbtInstanceApprove {
  /** 工作流实例ID */
  instanceId: number
  /** 节点ID */
  nodeId?: string
  /** 操作类型(2:审批 3:驳回 4:转办) */
  operType: number
  /** 操作意见 */
  operOpinion?: string
  /** 操作数据(JSON格式) */
  operData?: string
}

/**
 * 工作流实例操作记录模板参数
 */
export interface HbtInstanceOperTemplate {
  /** 工作流实例ID */
  instanceId: number
  /** 节点ID */
  nodeId?: string
  /** 节点名称 */
  nodeName?: string
  /** 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回) */
  operType: number
  /** 操作人ID */
  operatorId: number
  /** 操作人姓名 */
  operatorName: string
  /** 操作意见 */
  operOpinion?: string
  /** 操作数据(JSON格式) */
  operData?: string
}

/**
 * 工作流实例操作记录导入参数
 */
export interface HbtInstanceOperImport {
  /** 工作流实例ID */
  instanceId: number
  /** 节点ID */
  nodeId?: string
  /** 节点名称 */
  nodeName?: string
  /** 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回) */
  operType: number
  /** 操作人ID */
  operatorId: number
  /** 操作人姓名 */
  operatorName: string
  /** 操作意见 */
  operOpinion?: string
  /** 操作数据(JSON格式) */
  operData?: string
}

/**
 * 工作流实例操作记录导出参数
 */
export interface HbtInstanceOperExport {
  /** 工作流实例ID */
  instanceId: number
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 操作类型 */
  operType: string
  /** 操作人姓名 */
  operatorName: string
  /** 操作意见 */
  operOpinion: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流实例操作记录分页结果
 */
export interface HbtInstanceOperPagedResult extends HbtPagedResult<HbtInstanceOper> {}
