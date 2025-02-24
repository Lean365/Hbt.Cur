//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtLanguage.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言相关类型定义
//===================================================================

import type { HbtStatus } from '@/types/base'

/**
 * 语言对象
 */
export interface HbtLanguage {
  /** 语言ID */
  id: number
  /** 语言代码 */
  langCode: string
  /** 语言名称 */
  langName: string
  /** 排序号 */
  orderNum: number
  /** 是否默认 */
  isDefault: boolean
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
 * 语言查询参数
 */
export interface HbtLanguageQuery {
  /** 页码 */
  pageNum: number
  /** 每页条数 */
  pageSize: number
  /** 语言代码 */
  langCode?: string
  /** 语言名称 */
  langName?: string
  /** 状态（0正常 1停用） */
  status?: HbtStatus
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
  status: HbtStatus
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
  status: HbtStatus
} 