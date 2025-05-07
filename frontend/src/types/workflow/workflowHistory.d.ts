/** 工作流历史记录相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 工作流历史记录查询参数 */
export interface HbtWorkflowHistoryQuery extends HbtPagedQuery {
  workflowInstanceId?: number
  nodeId?: number
  operationType?: number
  operatorId?: number
  operatorName?: string
  operationResult?: number
  startTime?: string
  endTime?: string
}

/** 工作流历史记录数据传输对象 */
export interface HbtWorkflowHistory extends HbtBaseEntity {
  workflowInstanceId: number
  nodeId: number
  operationType: number
  operatorId: number
  operatorName: string
  operationResult?: number
  operationComment?: string
  operationTime: string
  workflowInstance?: HbtWorkflowInstance
  node?: HbtWorkflowNode
}

/** 工作流历史记录创建参数 */
export interface HbtWorkflowHistoryCreate {
  workflowInstanceId: number
  nodeId: number
  operationType: number
  operatorId: number
  operatorName: string
  operationResult?: number
  operationComment?: string
  operationTime: string
}

/** 工作流历史记录更新参数 */
export interface HbtWorkflowHistoryUpdate extends HbtWorkflowHistoryCreate {
  id: string
}

/** 工作流历史记录状态更新参数 */
export interface HbtWorkflowHistoryStatus {
  id: string
  status: string
}

/** 工作流历史记录导入参数 */
export interface HbtWorkflowHistoryImport {
  file: File
}

/** 工作流历史记录导出参数 */
export interface HbtWorkflowHistoryExport extends HbtWorkflowHistoryQuery {
  orderByColumn?: string
  isAsc?: string
}

/** 工作流历史记录模板参数 */
export interface HbtWorkflowHistoryTemplate {
  workflowInstanceId: number
  nodeId: number
  operationType: number
  operatorId: number
  operatorName: string
  operationResult?: number
  operationComment?: string
  operationTime: string
}

/** 工作流历史记录分页结果 */
export type HbtWorkflowHistoryPagedResult = HbtPagedResult<HbtWorkflowHistory>
