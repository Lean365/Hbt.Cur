//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtTranslation.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译相关类型定义
//===================================================================

import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

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
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 翻译查询参数
 */
export interface HbtTranslationQuery extends HbtPageQuery {
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
  /** 排序号 */
  orderNum: number
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
 * 转置后的翻译数据
 */
export interface HbtTransposedData {
  /** 数据行 */
  rows: {
    /** 翻译键 */
    transKey: string
    /** 各语言的翻译值 */
    [langCode: string]: string
  }[]
  /** 总数 */
  totalNum: number
  /** 页码 */
  pageIndex: number
  /** 每页条数 */
  pageSize: number
} 