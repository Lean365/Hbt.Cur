//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtDictType.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型相关类型定义
//===================================================================

import type { HbtStatus } from '@/types/base'

/**
 * 字典类型对象
 */
export interface HbtDictType {
  /** 字典类型ID */
  dictTypeId: number
  /** 字典名称 */
  dictName: string
  /** 字典类型 */
  dictType: string
  /** 字典类别（0系统 1SQL） */
  dictCategory: number
  /** SQL脚本 */
  sqlScript?: string
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: HbtStatus
  /** 状态名称 */
  statusName: string
  /** 创建时间 */
  createTime: string
  /** 状态加载中 */
  statusLoading?: boolean
}

/**
 * 字典类型查询参数
 */
export interface HbtDictTypeQuery {
  /** 页码 */
  pageNum: number
  /** 每页条数 */
  pageSize: number
  /** 字典名称 */
  dictName?: string
  /** 字典类型 */
  dictType?: string
  /** 字典类别（0系统 1SQL） */
  dictCategory?: number
  /** 状态（0正常 1停用） */
  status?: HbtStatus
}

/**
 * 字典类型创建参数
 */
export interface HbtDictTypeCreate {
  /** 字典名称 */
  dictName: string
  /** 字典类型 */
  dictType: string
  /** 字典类别（0系统 1SQL） */
  dictCategory: number
  /** SQL脚本 */
  sqlScript?: string
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: HbtStatus
}

/**
 * 字典类型更新参数
 */
export interface HbtDictTypeUpdate extends HbtDictTypeCreate {
  /** 字典类型ID */
  dictTypeId: number
}

/**
 * 字典类型导入参数
 */
export interface HbtDictTypeImport {
  /** 字典名称 */
  dictName: string
  /** 字典类型 */
  dictType: string
  /** 字典类别（0系统 1SQL） */
  dictCategory: number
  /** SQL脚本 */
  sqlScript?: string
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: string
}

/**
 * 字典类型状态更新参数
 */
export interface HbtDictTypeStatus {
  /** 字典类型ID */
  dictTypeId: number
  /** 状态（0正常 1停用） */
  status: HbtStatus
} 