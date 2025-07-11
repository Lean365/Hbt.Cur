/**
 * 工作流转换相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 工作流转换数据传输对象
 */
export interface HbtTransition extends HbtBaseEntity {
  transitionId: number
  sourceNodeId: number
  sourceNodeName: string
  targetNodeId: number
  targetNodeName: string
  condition?: string
  definitionId: number
  definitionName: string
  remark?: string
}

/**
 * 工作流转换查询参数
 */
export interface HbtTransitionQuery extends HbtPagedQuery {
  definitionId: number
  sourceNodeId: number
}

/**
 * 工作流转换创建参数
 */
export interface HbtTransitionCreate {
  definitionId: number
  sourceNodeId: number
  targetNodeId: number
  condition?: string
}

/**
 * 工作流转换更新参数
 */
export interface HbtTransitionUpdate extends HbtTransitionCreate {
  transitionId: number
}

/**
 * 工作流转换删除参数
 */
export interface HbtTransitionDelete {
  transitionId: number
}

/**
 * 工作流转换导入参数
 */
export interface HbtTransitionImport {
  definitionId: number
}

/**
 * 工作流转换导出参数
 */
export interface HbtTransitionExport {
  definitionId: number
}

/**
 * 工作流转换模板参数
 */
export interface HbtTransitionTemplate {
  definitionId: number
  sourceNodeId: number
  targetNodeId: number
}

/**
 * 工作流转换执行参数
 */
export interface HbtTransitionExecute {
  transitionId: number
  variables?: Record<string, any>
}

/**
 * 工作流转换分页结果
 */
export type HbtTransitionPagedResult = HbtPagedResult<HbtTransition> 