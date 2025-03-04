//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtTranslation.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译相关类型定义
//===================================================================

import type { HbtStatus } from '@/types/base'

/**
 * 翻译对象
 */
export interface HbtTranslation {
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
  status: HbtStatus
  /** 创建者 */
  createBy?: string
  /** 创建时间 */
  createTime?: string
  /** 更新者 */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
  /** 备注 */
  remark?: string
  /** 状态加载中 */
  statusLoading?: boolean
}

/**
 * 翻译查询参数
 */
export interface HbtTranslationQuery {
  /** 页码 */
  pageNum?: number
  /** 每页条数 */
  pageSize?: number
  /** 页码(后端参数) */
  pageIndex?: number
  /** 语言代码 */
  langCode?: string
  /** 模块名称 */
  moduleName?: string
  /** 翻译键 */
  transKey?: string
  /** 翻译值 */
  transValue?: string
  /** 状态（0正常 1停用） */
  status?: HbtStatus
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
  status: HbtStatus
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
  status: HbtStatus
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