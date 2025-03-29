//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtLanguage.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言相关类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 语言对象
 */
export interface HbtLanguage extends HbtBaseEntity {
  /** 语言ID */
  languageId: number
  /** 语言代码 */
  langCode: string
  /** 语言名称 */
  langName: string
  /** 语言图标 */
  langIcon?: string
  /** 排序号 */
  orderNum: number
  /** 是否默认 */
  isDefault: boolean
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 语言查询参数
 */
export interface HbtLanguageQuery extends HbtPagedQuery {
  /** 语言代码 */
  langCode?: string
  /** 语言名称 */
  langName?: string
  /** 状态（0正常 1停用） */
  status?: number
}

/**
 * 语言创建参数
 */
export interface HbtLanguageCreate {
  /** 语言代码 */
  langCode: string
  /** 语言名称 */
  langName: string
  /** 排序号 */
  orderNum: number
  /** 是否默认 */
  isDefault: boolean
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 语言更新参数
 */
export interface HbtLanguageUpdate extends HbtLanguageCreate {
  /** 语言ID */
  id: number
}

/**
 * 语言状态更新参数
 */
export interface HbtLanguageStatus {
  /** 语言ID */
  languageId: number
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 语言分页结果
 */
export type HbtLanguagePageResult = HbtPagedResult<HbtLanguage> 