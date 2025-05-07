//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : column.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成列定义类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 代码生成列定义实体
 */
export interface HbtGenColumn extends HbtBaseEntity {
  /** 列ID */
  id: number
  /** 表ID */
  tableId: number
  /** 列名 */
  columnName: string
  /** 列注释 */
  columnComment: string
  /** 列类型 */
  columnType: string
  /** C#类型 */
  csharpType: string
  /** TypeScript类型 */
  tsType: string
  /** 主键（0否 1是） */
  isPk: number
  /** 自增（0否 1是） */
  isIncrement: number
  /** 必填（0否 1是） */
  isRequired: number
  /** 插入（0否 1是） */
  isInsert: number
  /** 编辑（0否 1是） */
  isEdit: number
  /** 列表（0否 1是） */
  isList: number
  /** 查询（0否 1是） */
  isQuery: number
  /** 排序（0否 1是） */
  isSort: number
  /** 查询方式（eq等于、ne不等于、gt大于、ge大于等于、lt小于、le小于等于、like模糊、between范围） */
  queryType: string
  /** 显示类型（input文本框、textarea文本域、select下拉框、checkbox复选框、radio单选框、datetime日期控件、upload上传控件） */
  htmlType: string
  /** 字典类型 */
  dictType?: string
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark?: string
  /** 状态（0：停用，1：正常） */
  status: number
}

/**
 * 代码生成列定义查询参数
 */
export interface HbtGenColumnQuery extends HbtPagedQuery {
  /** 表ID */
  tableId?: number
  /** 列名 */
  columnName?: string
  /** 列注释 */
  columnComment?: string
  /** 列类型 */
  columnType?: string
  /** 状态 */
  status?: number
  /** 日期范围 */
  dateRange?: [string, string]
}

/**
 * 代码生成列定义创建参数
 */
export interface HbtGenColumnCreate {
  /** 表ID */
  tableId: number
  /** 列名 */
  columnName: string
  /** 列注释 */
  columnComment: string
  /** 列类型 */
  columnType: string
  /** C#类型 */
  csharpType: string
  /** TypeScript类型 */
  tsType: string
  /** 主键（0否 1是） */
  isPk: number
  /** 自增（0否 1是） */
  isIncrement: number
  /** 必填（0否 1是） */
  isRequired: number
  /** 插入（0否 1是） */
  isInsert: number
  /** 编辑（0否 1是） */
  isEdit: number
  /** 列表（0否 1是） */
  isList: number
  /** 查询（0否 1是） */
  isQuery: number
  /** 排序（0否 1是） */
  isSort: number
  /** 查询方式 */
  queryType: string
  /** 显示类型 */
  htmlType: string
  /** 字典类型 */
  dictType?: string
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark?: string
  /** 状态 */
  status: number
}

/**
 * 代码生成列定义更新参数
 */
export interface HbtGenColumnUpdate extends HbtGenColumnCreate {
  /** 列ID */
  id: number
}

/**
 * 代码生成列定义导入参数
 */
export interface HbtGenColumnImport {
  /** 文件对象 */
  file: File
  /** 工作表名称 */
  sheetName?: string
}

/**
 * 代码生成列定义导出参数
 */
export interface HbtGenColumnExport extends HbtGenColumnQuery {
  /** 工作表名称 */
  sheetName?: string
}

/**
 * 代码生成列定义分页结果
 */
export type HbtGenColumnPageResult = HbtPagedResult<HbtGenColumn>

declare namespace HbtGenColumn {
  /** 代码生成字段 */
  interface Column {
    /** 字段ID */
    id: number;
    /** 表ID */
    tableId: number;
    /** 字段名称 */
    columnName: string;
    /** 字段描述 */
    columnComment: string;
    /** 字段类型 */
    columnType: string;
    /** C#类型 */
    csharpType: string;
    /** C#字段名 */
    csharpField: string;
    /** 是否主键 */
    isPk: boolean;
    /** 是否自增 */
    isIncrement: boolean;
    /** 是否必填 */
    isRequired: boolean;
    /** 是否为插入字段 */
    isInsert: boolean;
    /** 是否编辑字段 */
    isEdit: boolean;
    /** 是否列表字段 */
    isList: boolean;
    /** 是否查询字段 */
    isQuery: boolean;
    /** 查询方式 */
    queryType: string;
    /** 显示类型 */
    htmlType: string;
    /** 字典类型 */
    dictType: string;
    /** 排序 */
    sort: number;
    /** 创建者 */
    createBy: string;
    /** 创建时间 */
    createTime: string;
    /** 更新者 */
    updateBy: string;
    /** 更新时间 */
    updateTime: string;
  }
} 