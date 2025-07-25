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
 * 语言实体
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
  /** 系统内置(0=否,1=是) */
  isBuiltin: number
  /** 是否默认 */
  isDefault: number
  /** 状态(0=禁用,1=启用) */
  status: number
}

/**
 * 语言查询参数
 */
export interface HbtLanguageQuery extends HbtPagedQuery {
  /** 语言代码 */
  langCode?: string
  /** 语言名称 */
  langName?: string
  /** 系统内置(0=否,1=是) */
  isBuiltin?: number
  /** 是否默认 */
  isDefault?: number
  /** 状态(0=禁用,1=启用) */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 创建语言参数
 */
export interface HbtLanguageCreate {
  /** 语言代码 */
  langCode: string
  /** 语言名称 */
  langName: string
  /** 语言图标 */
  langIcon?: string
  /** 排序号 */
  orderNum: number
  /** 系统内置(0=否,1=是) */
  isBuiltin: number
  /** 是否默认 */
  isDefault: number
  /** 状态(0=禁用,1=启用) */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 更新语言参数
 */
export interface HbtLanguageUpdate extends HbtLanguageCreate {
  /** 语言ID */
  languageId: number
}

/**
 * 语言状态
 */
export enum HbtLanguageStatus {
  /** 禁用 */
  Disabled = 0,
  /** 启用 */
  Enabled = 1
}

/**
 * 语言分页结果
 */
export type HbtLanguagePageResult = HbtPagedResult<HbtLanguage>

/**
 * 语言选项DTO
 */
export interface HbtLanguageOptionDto {
  /** 语言ID */
  languageId: number
  /** 语言代码 */
  langCode: string
  /** 语言名称 */
  langName: string
  /** 语言图标 */
  langIcon?: string
  /** 是否默认 */
  isDefault: number
} 