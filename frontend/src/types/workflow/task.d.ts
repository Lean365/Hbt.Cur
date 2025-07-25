import type { HbtBaseEntity } from '@/types/common'

/**
 * 工作流任务实体
 */
export interface HbtTask extends HbtBaseEntity {
  /** 任务ID */
  taskId: string
  /** 任务名称 */
  taskName: string
  /** 任务状态 0:待处理 1:处理中 2:已完成 3:已拒绝 4:已转办 */
  taskState: number
  /** 优先级 0:低 1:中 2:高 3:紧急 */
  priority: number
  /** 流程实例ID */
  instanceId: string
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 处理人ID */
  assigneeId: string
  /** 处理人姓名 */
  assigneeName: string
  /** 处理时间 */
  processTime?: string
  /** 完成时间 */
  finishTime?: string
  /** 处理意见 */
  processOpinion?: string
  /** 任务描述 */
  description?: string
  /** 任务类型 */
  taskType?: number
  /** 任务来源 */
  taskSource?: string
  /** 任务参数 */
  taskParams?: string
  /** 任务结果 */
  taskResult?: string
  /** 任务备注 */
  remark?: string
}

/**
 * 工作流任务查询参数
 */
export interface HbtTaskQuery {
  /** 页码 */
  pageIndex: number
  /** 页大小 */
  pageSize: number
  /** 任务状态 */
  taskState?: number
  /** 优先级 */
  priority?: number
  /** 任务名称 */
  taskName?: string
  /** 流程实例ID */
  instanceId?: string
  /** 节点名称 */
  nodeName?: string
  /** 处理人ID */
  assigneeId?: string
  /** 处理人姓名 */
  assigneeName?: string
  /** 处理时间开始 */
  processTimeStart?: string
  /** 处理时间结束 */
  processTimeEnd?: string
  /** 创建时间开始 */
  createTimeStart?: string
  /** 创建时间结束 */
  createTimeEnd?: string
  /** 任务类型 */
  taskType?: number
  /** 任务来源 */
  taskSource?: string
}

/**
 * 工作流任务创建参数
 */
export interface HbtTaskCreate {
  /** 任务名称 */
  taskName: string
  /** 流程实例ID */
  instanceId: string
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 处理人ID */
  assigneeId: string
  /** 处理人姓名 */
  assigneeName: string
  /** 优先级 */
  priority?: number
  /** 任务描述 */
  description?: string
  /** 任务类型 */
  taskType?: number
  /** 任务来源 */
  taskSource?: string
  /** 任务参数 */
  taskParams?: string
  /** 任务备注 */
  remark?: string
}

/**
 * 工作流任务更新参数
 */
export interface HbtTaskUpdate {
  /** 任务ID */
  taskId: string
  /** 任务名称 */
  taskName?: string
  /** 任务状态 */
  taskState?: number
  /** 优先级 */
  priority?: number
  /** 处理人ID */
  assigneeId?: string
  /** 处理人姓名 */
  assigneeName?: string
  /** 处理时间 */
  processTime?: string
  /** 完成时间 */
  finishTime?: string
  /** 处理意见 */
  processOpinion?: string
  /** 任务描述 */
  description?: string
  /** 任务参数 */
  taskParams?: string
  /** 任务结果 */
  taskResult?: string
  /** 任务备注 */
  remark?: string
}

/**
 * 工作流任务处理参数
 */
export interface HbtTaskProcess {
  /** 任务ID */
  taskId: string
  /** 处理动作 1:同意 2:拒绝 3:转办 4:退回 */
  action: number
  /** 处理意见 */
  opinion?: string
  /** 转办人ID */
  transferUserId?: string
  /** 转办人姓名 */
  transferUserName?: string
  /** 处理参数 */
  processParams?: string
}

/**
 * 工作流任务批量处理参数
 */
export interface HbtTaskBatchProcess {
  /** 任务ID列表 */
  taskIds: string[]
  /** 处理动作 */
  action: number
  /** 处理意见 */
  opinion?: string
  /** 转办人ID */
  transferUserId?: string
  /** 转办人姓名 */
  transferUserName?: string
  /** 处理参数 */
  processParams?: string
} 