import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 工作流定义实体
 */
export interface HbtScheme extends HbtBaseEntity {
  /** 工作流定义ID */
  schemeId: number
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他) */
  schemeCategory: number
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: number
  /** 状态(0:草稿 1:已发布 2:已停用) */
  status: number
  /** 描述 */
  description?: string
  /** 备注 */
  remark?: string
}

/**
 * 工作流定义查询参数
 */
export interface HbtSchemeQuery extends HbtPagedQuery {
  /** 流程定义键 */
  schemeKey?: string
  /** 流程定义名称 */
  schemeName?: string
  /** 流程分类 */
  schemeCategory?: number
  /** 状态 */
  status?: number
}

/**
 * 工作流定义创建参数
 */
export interface HbtSchemeCreate {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他) */
  schemeCategory: number
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: number
  /** 描述 */
  description?: string
  /** 备注 */
  remark?: string
}

/**
 * 工作流定义更新参数
 */
export interface HbtSchemeUpdate extends HbtSchemeCreate {
  /** 工作流定义ID */
  schemeId: number
}

/**
 * 工作流定义状态更新参数
 */
export interface HbtSchemeStatus {
  /** 工作流定义ID */
  schemeId: number
  /** 状态(0:草稿 1:已发布 2:已停用) */
  status: number
}

/**
 * 工作流定义模板参数
 */
export interface HbtSchemeTemplate {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: number
  /** 描述 */
  description?: string
}

/**
 * 工作流定义导入参数
 */
export interface HbtSchemeImport {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: number
  /** 描述 */
  description?: string
}

/**
 * 工作流定义导出参数
 */
export interface HbtSchemeExport {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类 */
  schemeCategory: string
  /** 流程定义版本 */
  version: string
  /** 状态 */
  status: string
  /** 描述 */
  description: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流定义分页结果
 */
export interface HbtSchemePagedResult extends HbtPagedResult<HbtScheme> {}
