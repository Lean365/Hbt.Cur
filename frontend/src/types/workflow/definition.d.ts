/**
 * 工作流定义相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtNode } from './node'



/**
 * 工作流定义数据传输对象
 */
export interface HbtDefinition extends HbtBaseEntity {
  definitionId: number
  workflowName?: string
  workflowCategory?: string
  workflowVersion: string
  formId?: number
  formName?: string
  workflowConfig?: string
  status: number
  workflowNodes?: HbtNode[]
}
/**
 * 工作流定义查询参数
 */
export interface HbtDefinitionQuery extends HbtPagedQuery {
  workflowName?: string
  workflowCategory?: string
  workflowVersion?: string
  status?: number
}
/**
 * 工作流定义创建参数
 */
export interface HbtDefinitionCreate {
  /** 工作流名称 */
  workflowName: string
  /** 工作流类型 */
  workflowCategory: string
  /** 工作流版本 */
  workflowVersion: string
  /** 表单配置 */
  formId: number
  /** 工作流配置 */
  workflowConfig: any
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工作流定义更新参数
 */
export interface HbtDefinitionUpdate extends HbtDefinitionCreate {
  definitionId: number
}

/**
 * 工作流定义状态更新参数
 */
export interface HbtDefinitionStatus {
  definitionId: number
  status: number
}

/**
 * 工作流定义导入参数
 */
export interface HbtDefinitionImport {
  /** 工作流名称 */
  workflowName: string
  /** 工作流类型 */
  workflowCategory: string
  /** 工作流版本 */
  workflowVersion: string
  /** 表单配置 */
  formId: number
  /** 工作流配置 */
  workflowConfig: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工作流定义导出参数
 */
export interface HbtDefinitionExport extends HbtDefinitionQuery {
  /** 工作流名称 */
  workflowName: string
  /** 工作流类型 */
  workflowCategory: string
  /** 工作流版本 */
  workflowVersion: string
  /** 表单配置 */
  formId: number
  /** 工作流配置 */
  workflowConfig: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工作流定义模板参数
 */
export interface HbtDefinitionTemplate {
  /** 工作流名称 */
  workflowName: string
  /** 工作流类型 */
  workflowCategory: string
  /** 工作流版本 */
  workflowVersion: string
  /** 表单配置 */
  formId: number
  /** 工作流配置 */
  workflowConfig: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工作流定义分页结果
 */
export type HbtDefinitionPagedResult = HbtPagedResult<HbtDefinition>
