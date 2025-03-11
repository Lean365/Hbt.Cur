//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtDictData.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据相关类型定义
//===================================================================

import type { HbtStatus } from '@/types/base'
import type { BaseQuery } from '@/types/base'

/**
 * 字典数据对象
 */
export interface HbtDictData {
  /** 字典数据ID */
  dictDataId: number
  /** 字典类型ID */
  dictTypeId: number
  /** 字典标签 */
  dictLabel: string
  /** 字典键值 */
  dictValue: string
  /** 字典排序 */
  orderNum: number
  /** 样式属性 */
  cssClass?: string
  /** 表格回显样式 */
  listClass?: string
  /** 是否默认（0否 1是） */
  isDefault: number
  /** 状态（0正常 1停用） */
  status: HbtStatus
  /** 扩展标签 */
  extLabel?: string
  /** 扩展值 */
  extValue?: string
  /** 国际化翻译键值 */
  transKey?: string
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
 * 字典数据查询参数
 */
export interface HbtDictDataQuery extends BaseQuery {
  /** 字典类型ID */
  dictTypeId?: number
  /** 字典类型 */
  dictType?: string
  /** 状态（0正常 1停用） */
  status?: number
  /** 关键词 */
  keyword?: string
}

/**
 * 字典数据创建参数
 */
export interface HbtDictDataCreate {
  /** 字典类型ID */
  dictTypeId: number
  /** 字典标签 */
  dictLabel: string
  /** 字典键值 */
  dictValue: string
  /** 字典排序 */
  orderNum: number
  /** 样式属性 */
  cssClass?: string
  /** 表格回显样式 */
  listClass?: string
  /** 是否默认（0否 1是） */
  isDefault: number
  /** 状态（0正常 1停用） */
  status: HbtStatus
  /** 扩展标签 */
  extLabel?: string
  /** 扩展值 */
  extValue?: string
  /** 国际化翻译键值 */
  transKey?: string
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
  status: HbtStatus
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