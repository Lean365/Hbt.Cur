/** 工作流历史记录相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'



/** 工作流历史记录数据传输对象 */
export interface HbtHistory extends HbtBaseEntity {
  historyId: number
  instanceId: number
  nodeId: number
  operationType: number
  operationResult?: number
  operationComment?: string
  workflowInstance?: HbtInstance
  node?: HbtNode
}
/** 工作流历史记录查询参数 */
export interface HbtHistoryQuery extends HbtPagedQuery {

  instanceId?: number
  nodeId?: number
  operationType?: string
  operationResult?: number
  startTime?: string
  endTime?: string
}
/** 工作流历史记录创建参数 */
export interface HbtHistoryCreate {
  instanceId: number
  nodeId: number
  operationType: number
  operationResult?: number
  operationComment?: string
}

/** 工作流历史记录更新参数 */
export interface HbtHistoryUpdate extends HbtHistoryCreate {
  historyId: number
}

/** 工作流历史记录状态更新参数 */
export interface HbtHistoryStatus {
  historyId: number
  status: string
}

/** 工作流历史记录导入参数 */
export interface HbtHistoryImport {
  instanceId: number
  nodeId: number
  operationType: number
  operationResult?: number
  operationComment?: string
}

/** 工作流历史记录导出参数 */
export interface HbtHistoryExport extends HbtHistoryQuery {
  instanceId: number
  nodeId: number
  operationType: number
  operationResult?: number
  operationComment?: string
}

/** 工作流历史记录模板参数 */
export interface HbtHistoryTemplate {
  instanceId: number
  nodeId: number
  operationType: number
  operationResult?: number
  operationComment?: string
}

/** 工作流历史记录分页结果 */
export type HbtHistoryPagedResult = HbtPagedResult<HbtHistory>
