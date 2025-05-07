//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : genTableDefine.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成表定义类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtGenColumnDefine } from './genColumnDefine'

/**
 * 代码生成表定义
 */
export interface HbtGenTableDefine extends HbtBaseEntity {
  // 基本信息
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string

  // 类型信息
  /** 实体类名 */
  className: string
  /** 命名空间 */
  namespace: string
  /** 基本命名空间前缀 */
  baseNamespace: string
  /** C#类名 */
  csharpTypeName: string

  // 关联信息
  /** 关联父表名 */
  parentTableName?: string
  /** 本表关联父表的外键名 */
  parentTableFkName?: string

  // 生成配置信息
  /** 使用的模板 */
  templateType: number
  /** 生成模块名 */
  moduleName: string
  /** 生成业务名 */
  businessName: string
  /** 生成功能名 */
  functionName: string
  /** 生成作者名 */
  author: string

  // 生成选项
  /** 生成代码方式 */
  genMode: number
  /** 代码生成存放位置 */
  genPath: string
  /** 其他生成选项 */
  options?: string

  /** 字段定义列表 */
  columns?: HbtGenColumnDefine[]
}

/**
 * 代码生成表定义查询对象
 */
export interface HbtGenTableDefineQuery extends HbtPagedQuery {
  /** 表名 */
  tableName?: string
  /** 表描述 */
  tableComment?: string
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 代码生成表定义分页结果
 */
export interface HbtGenTableDefinePageResult extends HbtPagedResult<HbtGenTableDefine> {}

/**
 * 代码生成表定义创建对象
 */
export type HbtGenTableDefineCreate = Omit<HbtGenTableDefine, keyof HbtBaseEntity>

/**
 * 代码生成表定义更新对象
 */
export interface HbtGenTableDefineUpdate extends HbtGenTableDefineCreate {
  /** 表ID */
  tableId: number
}

/**
 * 代码生成表定义导入对象
 */
export type HbtGenTableDefineImport = Omit<HbtGenTableDefine, keyof HbtBaseEntity>

/**
 * 代码生成表定义导出对象
 */
export type HbtGenTableDefineExport = Omit<HbtGenTableDefine, keyof HbtBaseEntity>

/**
 * 代码生成表定义模板
 */
export interface HbtGenTableDefineTemplate {
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 实体类名 */
  className: string
  /** 命名空间 */
  namespace: string
  /** 基本命名空间前缀 */
  baseNamespace: string
  /** C#类名 */
  csharpTypeName: string
  /** 关联父表名 */
  parentTableName?: string
  /** 本表关联父表的外键名 */
  parentTableFkName?: string
  /** 使用的模板 */
  templateType: string
  /** 生成模块名 */
  moduleName: string
  /** 生成业务名 */
  businessName: string
  /** 生成功能名 */
  functionName: string
  /** 生成作者名 */
  author: string
  /** 生成代码方式 */
  genMode: string
  /** 代码生成存放位置 */
  genPath: string
  /** 其他生成选项 */
  options?: string
  /** 字段定义列表 */
  columns?: any[]
} 