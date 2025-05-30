//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : genTableDefine.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述   : 代码生成表定义类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtGenColumnDefine } from './genColumnDefine'

/**
 * 代码生成表定义
 */
export interface HbtGenTableDefine extends HbtBaseEntity {
  /** 表ID */
  genTableDefineId: number
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: HbtGenColumnDefine[]
}

/**
 * 代码生成表定义查询对象
 */
export interface HbtGenTableDefineQuery extends HbtPagedQuery {
  /** 表名 */
  tableName?: string
  /** 表描述 */
  tableComment?: string
  /** 日期范围 */
  dateRange?: [string, string]
}

/**
 * 代码生成表定义分页结果
 */
export interface HbtGenTableDefinePageResult extends HbtPagedResult<HbtGenTableDefine> {}

/**
 * 代码生成表定义创建对象
 */
export interface HbtGenTableDefineCreate {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表前缀 */
  tablePrefix: string
  /** 表名第一部分（模块名称） */
  tableNameFirst: string
  /** 表名第二部分 */
  tableNameSecond: string
  /** 表名第三部分 */
  tableNameThird: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: HbtGenColumnDefine[]
}

/**
 * 代码生成表定义更新对象
 */
export interface HbtGenTableDefineUpdate extends HbtGenTableDefineCreate {
  /** 表ID */
  genTableDefineId: number
}

/**
 * 代码生成表定义导入对象
 */
export interface HbtGenTableDefineImport {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: HbtGenColumnDefine[]
}

/**
 * 代码生成表定义导出对象
 */
export interface HbtGenTableDefineExport {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: HbtGenColumnDefine[]
}

/**
 * 代码生成表定义模板对象
 */
export interface HbtGenTableDefineTemplate {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: HbtGenColumnDefine[]
} 