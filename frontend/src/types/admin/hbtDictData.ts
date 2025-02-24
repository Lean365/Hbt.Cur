//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtDictData.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据相关类型定义
//===================================================================

import type { HbtStatus } from '@/types/base'

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
export interface HbtDictDataQuery {
  /** 页码 */
  pageNum: number
  /** 每页条数 */
  pageSize: number
  /** 字典类型ID */
  dictTypeId?: number
  /** 字典标签 */
  dictLabel?: string
  /** 字典键值 */
  dictValue?: string
  /** 状态（0正常 1停用） */
  status?: HbtStatus
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