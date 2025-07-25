import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 表单实体
 */
export interface HbtForm extends HbtBaseEntity {
  /** 表单ID */
  formId: number
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他) */
  formType: number
  /** 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他) */
  formCategory: number
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 工作流实例ID */
  instanceId?: number
  /** 表单数据(JSON格式) */
  formData?: string
  /** 状态(0:草稿 1:已发布 2:已作废) */
  status: number
  /** 描述 */
  description?: string
  /** 备注 */
  remark?: string
}

/**
 * 表单查询参数
 */
export interface HbtFormQuery extends HbtPagedQuery {
  /** 表单键 */
  formKey?: string
  /** 表单名称 */
  formName?: string
  /** 表单类型 */
  formType?: number
  /** 表单分类 */
  formCategory?: number
  /** 工作流实例ID */
  instanceId?: number
  /** 状态 */
  status?: number
}

/**
 * 表单创建参数
 */
export interface HbtFormCreate {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他) */
  formType: number
  /** 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他) */
  formCategory: number
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 工作流实例ID */
  instanceId?: number
  /** 表单数据(JSON格式) */
  formData?: string
  /** 描述 */
  description?: string
  /** 备注 */
  remark?: string
}

/**
 * 表单更新参数
 */
export interface HbtFormUpdate extends HbtFormCreate {
  /** 表单ID */
  formId: number
}

/**
 * 表单状态更新参数
 */
export interface HbtFormStatus {
  /** 表单ID */
  formId: number
  /** 状态(0:草稿 1:已发布 2:已作废) */
  status: number
}

/**
 * 表单模板参数
 */
export interface HbtFormTemplate {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 描述 */
  description?: string
}

/**
 * 表单导入参数
 */
export interface HbtFormImport {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 描述 */
  description?: string
}

/**
 * 表单导出参数
 */
export interface HbtFormExport {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单类型 */
  formType: string
  /** 表单分类 */
  formCategory: string
  /** 表单版本 */
  version: string
  /** 状态 */
  status: string
  /** 描述 */
  description: string
  /** 创建时间 */
  createTime: string
}

/**
 * 表单分页结果
 */
export interface HbtFormPagedResult extends HbtPagedResult<HbtForm> {}


