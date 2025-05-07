//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : genColumnDefine.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成字段定义类型定义
//===================================================================

import type { HbtBaseEntity } from '@/types/common'

/**
 * 代码生成字段定义
 */
export interface HbtGenColumnDefine extends HbtBaseEntity {
  /** 表ID */
  tableId: number
  /** 字段名 */
  columnName: string
  /** 字段描述 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** C#类型 */
  csharpType: string
  /** C#列名（首字母大写） */
  csharpColumn: string
  /** C#长度（字符串长度、数值类型的整数位数） */
  csharpLength: number
  /** C#小数位数（decimal等数值类型的小数位数） */
  csharpDecimalDigits: number
  /** C#字段名（首字母小写） */
  csharpField: string
  /** 是否自增（1是） */
  isIncrement: number
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否为新增字段（1是） */
  isInsert: number
  /** 是否编辑字段（1是） */
  isEdit: number
  /** 是否列表字段（1是） */
  isList: number
  /** 是否查询字段（1是） */
  isQuery: number
  /** 查询方式（等于、不等于、大于、小于、范围） */
  queryType: string
  /** 是否排序字段（1是） */
  isSort: number
  /** 是否导出字段（1是） */
  isExport: number
  /** 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件） */
  displayType: string
  /** 字典类型 */
  dictType: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成字段定义查询对象
 */
export interface HbtGenColumnDefineQuery {
  /** 表ID */
  tableId?: number
  /** 字段名 */
  columnName?: string
  /** 字段描述 */
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
export type HbtGenColumnDefineCreate = Omit<HbtGenColumnDefine, keyof HbtBaseEntity>

/**
 * 代码生成字段定义更新对象
 */
export interface HbtGenColumnDefineUpdate extends HbtGenColumnDefineCreate {
  /** 字段ID */
  genColumnId: number
}

/**
 * 代码生成字段定义导入对象
 */
export interface HbtGenColumnDefineImport {
  /** 表ID */
  tableId: number
  /** 字段名列表 */
  columnNames: string[]
}

/**
 * 代码生成字段定义导出对象
 */
export interface HbtGenColumnDefineExport {
  /** 字段ID列表 */
  genColumnIds: number[]
} 