//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtTranslation.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译相关类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 翻译对象
 */
export interface HbtTranslation extends HbtBaseEntity {
  /** 翻译ID */
  translationId: number
  /** 语言代码 */
  langCode: string
  /** 模块名称 */
  moduleName: string
  /** 翻译键 */
  transKey: string
  /** 翻译值 */
  transValue: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 翻译查询参数
 */
export interface HbtTranslationQuery extends HbtPagedQuery {
  /** 语言代码 */
  langCode?: string
  /** 模块名称 */
  moduleName?: string
  /** 翻译键 */
  transKey?: string
  /** 翻译值 */
  transValue?: string
  /** 状态（0正常 1停用） */
  status?: number
}

/**
 * 翻译创建参数
 */
export interface HbtTranslationCreate {
  /** 语言代码 */
  langCode: string
  /** 模块名称 */
  moduleName: string
  /** 翻译键 */
  transKey: string
  /** 翻译值 */
  transValue: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 翻译更新参数
 */
export interface HbtTranslationUpdate extends HbtTranslationCreate {
  /** 翻译ID */
  id: number
}

/**
 * 翻译状态更新参数
 */
export interface HbtTranslationStatus {
  /** 翻译ID */
  translationId: number
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 翻译语言信息
 */
export interface HbtTranslationLang {
  /** 翻译ID */
  translationId: number
  /** 语言代码 */
  langCode: string
  /** 翻译值 */
  transValue: string
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 转置后的翻译数据
 */
export interface HbtTransposedData {
  /** 翻译键 */
  transKey: string
  /** 模块名称 */
  moduleName: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark: string
  /** 创建者 */
  createBy: string
  /** 创建时间 */
  createTime: string
  /** 更新者 */
  updateBy: string
  /** 更新时间 */
  updateTime: string
  /** 各语言翻译信息 */
  translations: Record<string, HbtTranslationLang>
}

/**
 * 翻译分页结果
 */
export type HbtTranslationPageResult = HbtPagedResult<HbtTranslation>

/**
 * 转置查询参数
 */
export interface HbtTransposedQueryDto extends HbtPagedQuery {
  /** 翻译键 */
  transKey?: string
  /** 模块名称 */
  moduleName?: string
  /** 状态（0正常 1停用 -1全部） */
  status?: number
} 