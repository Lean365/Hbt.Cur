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
  id: number
  /** 模板名称 */
  templateName: string
  /** 模板类型 */
  templateType: number
  /** 模板内容 */
  templateContent: string
  /** 生成路径 */
  genPath: string
  /** 生成类型 */
  genType: number
  /** 备注 */
  remark?: string
  /** 状态（0：停用，1：正常） */
  status: number
}

/**
 * 代码生成配置查询参数
 */
export interface HbtGenConfigQuery extends HbtPagedQuery {
  /** 模板名称 */
  templateName?: string
  /** 模板类型 */
  templateType?: number
  /** 生成类型 */
  genType?: number
  /** 状态 */
  status?: number
  /** 日期范围 */
  dateRange?: [string, string]
}

/**
 * 代码生成配置创建参数
 */
export interface HbtGenConfigCreate {
  /** 模板名称 */
  templateName: string
  /** 模板类型 */
  templateType: number
  /** 模板内容 */
  templateContent: string
  /** 生成路径 */
  genPath: string
  /** 生成类型 */
  genType: number
  /** 备注 */
  remark?: string
  /** 状态 */
  status: number
}

/**
 * 代码生成配置更新参数
 */
export interface HbtGenConfigUpdate extends HbtGenConfigCreate {
  /** 配置ID */
  id: number
}

/**
 * 代码生成配置导入参数
 */
export interface HbtGenConfigImport {
  /** 文件对象 */
  file: File
  /** 工作表名称 */
  sheetName?: string
}

/**
 * 代码生成配置导出参数
 */
export interface HbtGenConfigExport extends HbtGenConfigQuery {
  /** 工作表名称 */
  sheetName?: string
}

/**
 * 代码生成配置分页结果
 */
export type HbtGenConfigPageResult = HbtPagedResult<HbtGenConfig> 