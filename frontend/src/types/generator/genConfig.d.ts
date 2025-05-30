//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : config.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成配置类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 代码生成配置实体
 */
export interface HbtGenConfig extends HbtBaseEntity {
  /** 配置ID */
  genConfigId: number
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 包名 */
  packageName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成类型 */
  genType: number
  /** 模板选用方式（0：使用wwwroot/Generator/*.scriban模板，1：使用HbtGenTemplate表中的模板） */
  genTemplateType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 代码生成配置查询参数
 */
export interface HbtGenConfigQuery extends HbtPagedQuery {
  /** 配置名称 */
  genConfigName?: string
  /** 作者 */
  author?: string
  /** 模块名 */
  moduleName?: string
  /** 包名 */
  packageName?: string
  /** 业务名 */
  businessName?: string
  /** 功能名 */
  functionName?: string
  /** 生成类型 */
  genType?: number
  /** 模板选用方式 */
  genTemplateType?: number
  /** 生成路径 */
  genPath?: string
  /** 状态 */
  status?: number
  /** 日期范围 */
  dateRange?: [string, string]
}

/**
 * 代码生成配置创建参数
 */
export interface HbtGenConfigCreate {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 包名 */
  packageName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成类型 */
  genType: number
  /** 模板选用方式 */
  genTemplateType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态 */
  status: number
}

/**
 * 代码生成配置更新参数
 */
export interface HbtGenConfigUpdate extends HbtGenConfigCreate {
  /** 配置ID */
  genConfigId: number
}

/**
 * 代码生成配置模板
 */
export interface HbtGenConfigTemplate {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 包名 */
  packageName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成类型 */
  genType: string
  /** 模板选用方式 */
  genTemplateType: string
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options: string
  /** 状态 */
  status: string
}

/**
 * 代码生成配置导入参数
 */
export interface HbtGenConfigImport {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 包名 */
  packageName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成类型 */
  genType: number
  /** 模板选用方式 */
  genTemplateType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态 */
  status: number
}

/**
 * 代码生成配置导出参数
 */
export interface HbtGenConfigExport {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 包名 */
  packageName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成类型 */
  genType: number
  /** 模板选用方式 */
  genTemplateType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态 */
  status: number
  /** 创建时间 */
  createTime: string
}

/**
 * 代码生成配置分页结果
 */
export type HbtGenConfigPageResult = HbtPagedResult<HbtGenConfig> 