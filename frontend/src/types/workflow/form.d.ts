/**
 * 工作流表单相关类型定义
 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 工作流表单数据传输对象
 */
export interface HbtForm extends HbtBaseEntity {
  formId: number
  formName: string
  formDesc?: string
  formConfig: string
  formVersion: string
  formCategory: number
  status: number
  definitionId?: number
}

/**
 * 工作流表单查询参数
 */
export interface HbtFormQuery extends HbtPagedQuery {
  formName?: string
  formVersion?: string
  formCategory?: number
  status?: number
}

/**
 * 工作流表单创建参数
 */
export interface HbtFormCreate {
  /** 表单名称 */
  formName: string
  /** 表单描述 */
  formDesc?: string
  /** 表单配置 */
  formConfig: string
  /** 表单版本 */
  formVersion: string
  /** 表单分类 */
  formCategory: number
  /** 状态（0草稿 1已发布 2已停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工作流表单更新参数
 */
export interface HbtFormUpdate extends HbtFormCreate {
  formId: number
}

/**
 * 工作流表单状态更新参数
 */
export interface HbtFormStatus {
  formId: number
  status: number
}

/**
 * 工作流表单导入参数
 */
export interface HbtFormImport {
  /** 表单名称 */
  formName: string
  /** 表单描述 */
  formDesc?: string
  /** 表单配置 */
  formConfig: string
  /** 表单版本 */
  formVersion: string
  /** 表单分类 */
  formCategory: number
  /** 状态（0草稿 1已发布 2已停用） */
  status: number
  /** 关联定义ID */
  definitionId?: number
}

/**
 * 工作流表单导出参数
 */
export interface HbtFormExport extends HbtFormQuery {
  /** 表单名称 */
  formName: string
  /** 表单描述 */
  formDesc?: string
  /** 表单配置 */
  formConfig: string
  /** 表单版本 */
  formVersion: string
  /** 表单分类 */
  formCategory: number
  /** 状态（0草稿 1已发布 2已停用） */
  status: number
  /** 关联定义ID */
  definitionId?: number
}

/**
 * 工作流表单模板参数
 */
export interface HbtFormTemplate {
  /** 表单名称 */
  formName: string
  /** 表单描述 */
  formDesc?: string
  /** 表单配置 */
  formConfig: string
  /** 表单版本 */
  formVersion: string
  /** 表单分类 */
  formCategory: number
  /** 状态（0草稿 1已发布 2已停用） */
  status: number
  /** 关联定义ID */
  definitionId?: number
}

/**
 * 工作流表单分页结果
 */
export type HbtFormPagedResult = HbtPagedResult<HbtForm> 