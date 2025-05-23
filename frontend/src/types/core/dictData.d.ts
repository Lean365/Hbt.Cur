//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : dictData.d.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 字典数据实体
 */
export interface HbtDictData extends HbtBaseEntity {
  /** 字典数据ID */
  dictDataId: number
  /** 字典类型 */
  dictType: string
  /** 字典标签 */
  dictLabel: string
  /** 字典键值 */
  dictValue: string
  /** 排序号 */
  orderNum: number
  /** 样式属性 */
  cssClass?: string
  /** 表格回显样式 */
  listClass?: string
  /** 是否默认（0否 1是） */
  isDefault: number
  /** 状态（0正常 1停用） */
  status: number
  /** 扩展标签 */
  extLabel?: string
  /** 扩展值 */
  extValue?: string
  /** 翻译键 */
  transKey?: string
  /** 租户ID */
  tenantId: number

}

/**
 * 字典数据查询参数
 */
export interface HbtDictDataQuery extends HbtPagedQuery {
  /** 字典类型 */
  dictType: string
  /** 字典标签 */
  dictLabel?: string
  /** 字典键值 */
  dictValue?: string
  /** 状态（0正常 1停用） */
  status?: number
}

/**
 * 字典数据创建参数
 */
export interface HbtDictDataCreate {
    /** 字典类型 */
    dictType: string
    /** 字典标签 */
    dictLabel: string
    /** 字典键值 */
    dictValue: string
    /** 扩展标签 */
    extLabel?: string
    /** 扩展值 */
    extValue?: string
    /** 翻译键 */
    transKey?: string
    /** 排序号 */
    orderNum: number
    /** 样式属性 */
    cssClass?: string
    /** 表格回显样式 */
    listClass?: string
    /** 状态（0正常 1停用） */
    status: number
    /** 租户ID */
    tenantId: number
    /** 备注 */
    remark?: string
}

/**
 * 字典数据更新参数
 */
export interface HbtDictDataUpdate extends HbtDictDataCreate {
  /** 字典数据ID */
  dictDataId: number
}

/**
 * 字典数据状态更新参数
 */
export interface HbtDictDataStatus {
  /** 字典数据ID */
  dictDataId: number
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 字典数据选项
 */
export interface DictOption {
  /** 标签 */
  label: string
  /** 值 */
  value: number
  /** CSS类名 */
  cssClass?: number
  /** 列表类名 */
  listClass?: number
  /** 扩展标签 */
  extLabel?: string
  /** 扩展值 */
  extValue?: string
  /** 翻译键 */
  transKey?: string
}

/**
 * 字典数据分页结果
 */
export type HbtDictDataPageResult = HbtPagedResult<HbtDictData> 