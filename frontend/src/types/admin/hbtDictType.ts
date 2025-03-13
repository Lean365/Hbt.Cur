//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtDictType.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型相关类型定义
//===================================================================

import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 字典类型对象
 */
export interface HbtDictType extends HbtBaseEntity {
  /** ID */
  dictTypeId: number
  /** 字典名称 */
  dictName: string
  /** 字典类型 */
  dictType: string
  /** 字典类别（0系统 1SQL） */
  dictCategory: number
  /** 字典内置（0否 1是） */
  dictBuiltin: number
  /** SQL脚本 */
  sqlScript?: string
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: number
  /** 租户ID */
  tenantId: number
  /** 备注 */
  remark?: string
}

/**
 * 字典类型查询参数
 */
export interface HbtDictTypeQuery extends HbtPageQuery {
  /** 字典名称 */
  dictName?: string
  /** 字典类型 */
  dictType?: string
  /** 字典类别（0系统 1SQL） */
  dictCategory?: number
  /** 状态（0正常 1停用） */
  status?: number
  /** 是否内置（0否 1是） */
  dictBuiltin?: number
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
  /** 字典内置（0否 1是） */
  dictBuiltin: number
  /** SQL脚本 */
  sqlScript?: string
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: number
  /** 租户ID */
  tenantId: number
  /** 备注 */
  remark?: string
}

/**
 * 字典类型更新参数
 */
export interface HbtDictTypeUpdate extends HbtDictTypeCreate {
  /** ID */
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
  status: number
}

/**
 * 字典类型状态更新参数
 */
export interface HbtDictTypeStatus {
  /** ID */
  dictTypeId: number
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 字典类型分页结果
 */
export interface DictTypePageResult {
  /** 数据列表 */
  rows: HbtDictType[]
  /** 总数 */
  totalNum: number
} 