//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : genColumnDefine.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述   : 代码生成字段定义类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery } from '@/types/common'

/**
 * 代码生成字段定义
 */
export interface HbtGenColumnDefine extends HbtBaseEntity {
  /** 字段ID */
  genColumnDefineId: number
  /** 表ID */
  tableId: number
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
  /** 是否处于编辑状态 */
  isEditing?: boolean
}

/**
 * 代码生成字段定义查询对象
 */
export interface HbtGenColumnDefineQuery extends HbtPagedQuery {
  /** 表ID */
  tableId?: number
  /** 字段名 */
  columnName?: string
  /** 字段说明 */
  columnComment?: string
}

/**
 * 代码生成字段定义分页结果
 */
export interface HbtGenColumnDefinePageResult {
  /** 总记录数 */
  total: number
  /** 列表数据 */
  rows: HbtGenColumnDefine[]
}

/**
 * 代码生成字段定义创建对象
 */
export interface HbtGenColumnDefineCreate {
  /** 表ID */
  tableId: number
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成字段定义更新对象
 */
export interface HbtGenColumnDefineUpdate extends HbtGenColumnDefineCreate {
  /** 字段ID */
  genColumnDefineId: number
}

/**
 * 代码生成字段定义导入对象
 */
export interface HbtGenColumnDefineImport {
  /** 表ID */
  tableId: number
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成字段定义导出对象
 */
export interface HbtGenColumnDefineExport {
  /** 表ID */
  tableId: number
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成字段定义模板对象
 */
export interface HbtGenColumnDefineTemplate {
  /** 表ID */
  tableId: number
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
} 