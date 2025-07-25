//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : dictType.d.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 字典类型实体
 */
export interface HbtDictType extends HbtBaseEntity {
  /** 字典类型ID */
  dictTypeId: number
  /** 字典名称 */
  dictName: string
  /** 字典类型 */
  dictType: string
  /** 字典分类 */
  dictCategory: number
  /** 系统内置（0否 1是） */
  isBuiltin: number
  /** SQL脚本 */
  sqlScript?: string
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: number

}

/**
 * 字典类型查询参数
 */
export interface HbtDictTypeQuery extends HbtPagedQuery {
  /** 字典名称 */
  dictName?: string
  /** 字典类型 */
  dictType?: string
  /** 字典分类 */
  dictCategory?: number
  /** 系统内置（0否 1是） */
  isBuiltin?: number
  /** 状态（0正常 1停用） */
  status?: number
}

/**
 * 字典类型创建参数
 */
export interface HbtDictTypeCreate {
  /** 字典名称 */
  dictName: string
  /** 字典类型 */
  dictType: string
  /** 字典分类 */
  dictCategory: number
  /** 系统内置（0否 1是） */
  isBuiltin: number
  /** SQL脚本 */
  sqlScript?: string
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
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
export type HbtDictTypePageResult = HbtPagedResult<HbtDictType> 