/**
 * 工作流定义相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtWorkflowNode } from './workflowNode'

/**
 * 工作流定义查询参数
 */
export interface HbtWorkflowDefinitionQuery extends HbtPagedQuery {
  workflowName?: string
  workflowCategory?: string
  workflowVersion?: number
  status?: number
}

/**
 * 工作流定义数据传输对象
 */
export interface HbtWorkflowDefinition extends HbtBaseEntity {
  workflowName?: string
  workflowCategory?: string
  workflowVersion: number
  formConfig?: string
  workflowConfig?: string
  status: number
  workflowNodes?: HbtWorkflowNode[]
}

/**
 * 工作流定义创建参数
 */
export interface HbtWorkflowDefinitionCreate {
  /** 工作流名称 */
  workflowName: string
  /** 工作流类型 */
  workflowCategory: string
  /** 工作流版本 */
  workflowVersion: number
  /** 表单配置 */
  formConfig: string
  /** 工作流配置 */
  workflowConfig: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工作流定义更新参数
 */
export interface HbtWorkflowDefinitionUpdate extends HbtWorkflowDefinitionCreate {
  id: number
}

/**
 * 工作流定义状态更新参数
 */
export interface HbtWorkflowDefinitionStatus {
  id: number
  status: number
}

/**
 * 工作流定义导入参数
 */
export interface HbtWorkflowDefinitionImport {
  file: File
}

/**
 * 工作流定义导出参数
 */
export interface HbtWorkflowDefinitionExport extends HbtWorkflowDefinitionQuery {
  orderByColumn?: string
  isAsc?: string
}

/**
 * 工作流定义模板参数
 */
export interface HbtWorkflowDefinitionTemplate {
  workflowName?: string
  workflowCategory?: string
  workflowVersion: number
  formConfig?: string
  workflowConfig?: string
  status: number
}

/**
 * 工作流定义分页结果
 */
export type HbtWorkflowDefinitionPagedResult = HbtPagedResult<HbtWorkflowDefinition>
