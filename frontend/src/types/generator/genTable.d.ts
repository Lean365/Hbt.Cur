//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : table.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成数据库表类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtGenColumn } from './genColumn'

/**
 * 代码生成数据库表
 */
export interface HbtGenTable extends HbtBaseEntity {
  /** 主键ID */
  genTableId: number
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 关联父表名 */
  subTableName?: string
  /** 本表关联父表的外键名 */
  subTableFkName?: string
  /** 树编码字段 */
  treeCode: string
  /** 树名称字段 */
  treeName: string
  /** 树父编码字段 */
  treeParentCode: string
  /** 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用HbtGenTemplate数据表中的模板） */
  tplType: string
  /** 使用的模板（crud单表操作 tree树表操作 sub主子表操作） */
  tplCategory: string
  /** 基本命名空间前缀 */
  baseNamespace: string
  /** 实体命名空间 */
  entityNamespace: string
  /** 实体类名 */
  entityClassName: string
  /** 对象命名空间 */
  dtoNamespace: string
  /** 对象类名 */
  dtoClassName: string
  /** 对象类型 */
  dtoType: string
  /** 服务命名空间 */
  serviceNamespace: string
  /** 服务接口类名称 */
  iServiceClassName: string
  /** 服务类名称 */
  serviceClassName: string
  /** 仓储接口命名空间 */
  iRepositoryNamespace: string
  /** 仓储接口类名称 */
  iRepositoryClassName: string
  /** 仓储命名空间 */
  repositoryNamespace: string
  /** 仓储类名称 */
  repositoryClassName: string
  /** 控制器命名空间 */
  controllerNamespace: string
  /** 控制器类名称 */
  controllerClassName: string
  /** 生成模块名 */
  moduleName: string
  /** 生成业务名 */
  businessName: string
  /** 生成功能名 */
  functionName: string
  /** 生成作者名 */
  author: string
  /** 生成代码方式（0zip压缩包 1自定义路径） */
  genMethod: string
  /** 代码生成存放位置 */
  genPath: string
  /** 上级菜单ID */
  parentMenuId: number
  /** 自动生成菜单 */
  generateMenu: number
  /** 排序类型 */
  sortType: string
  /** 排序字段 */
  sortField: string
  /** 权限前缀 */
  permsPrefix: string
  /** 前端模板 1、element ui 2、element plus */
  frontTpl: number
  /** 前端样式 12,24 */
  frontStyle: number
  /** 操作按钮样式 */
  btnStyle: number
  /** 状态 */
  status: number
  /** 代码生成列 */
  columns?: HbtGenColumn[]
  /** 子表信息 */
  subTable?: HbtGenTable
  /** 代码生成选项 */
  options?: CodeOptions
}

/**
 * 代码生成选项
 */
export interface CodeOptions {
  /** 是否启用SQL差异 */
  isSqlDiff: number
  /** 是否使用雪花id */
  isSnowflakeId: number
  /** 是否生成仓储层 */
  isRepository: number
  /** CRUD功能组 */
  crudGroup: number[]
}

/**
 * 代码生成数据库表查询参数
 */
export interface HbtGenTableQuery extends HbtPagedQuery {
  /** 表名 */
  tableName?: string
  /** 表描述 */
  tableComment?: string
}

/**
 * 代码生成数据库表创建参数
 */
export interface HbtGenTableCreate {
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 关联父表名 */
  subTableName?: string
  /** 本表关联父表的外键名 */
  subTableFkName?: string
  /** 树编码字段 */
  treeCode: string
  /** 树名称字段 */
  treeName: string
  /** 树父编码字段 */
  treeParentCode: string
  /** 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用HbtGenTemplate数据表中的模板） */
  tplType: string
  /** 使用的模板（crud单表操作 tree树表操作 sub主子表操作） */
  tplCategory: string
  /** 基本命名空间前缀 */
  baseNamespace: string
  /** 实体命名空间 */
  entityNamespace: string
  /** 实体类名 */
  entityClassName: string
  /** 对象命名空间 */
  dtoNamespace: string
  /** 对象类名 */
  dtoClassName: string
  /** 对象类型 */
  dtoType: string
  /** 服务命名空间 */
  serviceNamespace: string
  /** 服务接口类名称 */
  iServiceClassName: string
  /** 服务类名称 */
  serviceClassName: string
  /** 仓储接口命名空间 */
  iRepositoryNamespace: string
  /** 仓储接口类名称 */
  iRepositoryClassName: string
  /** 仓储命名空间 */
  repositoryNamespace: string
  /** 仓储类名称 */
  repositoryClassName: string
  /** 控制器命名空间 */
  controllerNamespace: string
  /** 控制器类名称 */
  controllerClassName: string
  /** 生成模块名 */
  moduleName: string
  /** 生成业务名 */
  businessName: string
  /** 生成功能名 */
  functionName: string
  /** 生成作者名 */
  author: string
  /** 生成代码方式（0zip压缩包 1自定义路径） */
  genMethod: string
  /** 代码生成存放位置 */
  genPath: string
  /** 上级菜单ID */
  parentMenuId: number
  /** 自动生成菜单 */
  generateMenu: number
  /** 排序类型 */
  sortType: string
  /** 排序字段 */
  sortField: string
  /** 权限前缀 */
  permsPrefix: string
  /** 前端模板 1、element ui 2、element plus */
  frontTpl: number
  /** 前端样式 12,24 */
  frontStyle: number
  /** 操作按钮样式 */
  btnStyle: number
  /** 状态 */
  status: number
  /** 代码生成列 */
  columns?: HbtGenColumn[]
  /** 代码生成选项 */
  options?: CodeOptions
}

/**
 * 代码生成数据库表更新参数
 */
export interface HbtGenTableUpdate extends HbtGenTableCreate {
  /** 表ID */
  id: number
}

/**
 * 批量删除代码生成数据库表参数
 */
export interface HbtGenTableBatchDelete {
  /** 表ID数组 */
  ids: number[]
}

/**
 * 更新代码生成数据库表状态参数
 */
export interface HbtGenTableStatusUpdate {
  /** 表ID */
  id: number
  /** 状态（0：停用，1：正常） */
  status: number
}

/**
 * 导入代码生成数据库表参数
 */
export interface HbtGenTableImport {
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 关联父表名 */
  subTableName?: string
  /** 本表关联父表的外键名 */
  subTableFkName?: string
  /** 树编码字段 */
  treeCode: string
  /** 树名称字段 */
  treeName: string
  /** 树父编码字段 */
  treeParentCode: string
  /** 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用HbtGenTemplate数据表中的模板） */
  tplType: string
  /** 使用的模板（crud单表操作 tree树表操作 sub主子表操作） */
  tplCategory: string
  /** 基本命名空间前缀 */
  baseNamespace: string
  /** 实体命名空间 */
  entityNamespace: string
  /** 实体类名 */
  entityClassName: string
  /** 对象命名空间 */
  dtoNamespace: string
  /** 对象类名 */
  dtoClassName: string
  /** 对象类型 */
  dtoType: string
  /** 服务命名空间 */
  serviceNamespace: string
  /** 服务接口类名称 */
  iServiceClassName: string
  /** 服务类名称 */
  serviceClassName: string
  /** 仓储接口命名空间 */
  iRepositoryNamespace: string
  /** 仓储接口类名称 */
  iRepositoryClassName: string
  /** 仓储命名空间 */
  repositoryNamespace: string
  /** 仓储类名称 */
  repositoryClassName: string
  /** 控制器命名空间 */
  controllerNamespace: string
  /** 控制器类名称 */
  controllerClassName: string
  /** 生成模块名 */
  moduleName: string
  /** 生成业务名 */
  businessName: string
  /** 生成功能名 */
  functionName: string
  /** 生成作者名 */
  author: string
  /** 生成代码方式（0zip压缩包 1自定义路径） */
  genMethod: string
  /** 代码生成存放位置 */
  genPath: string
  /** 上级菜单ID */
  parentMenuId: number
  /** 自动生成菜单 */
  generateMenu: number
  /** 排序类型 */
  sortType: string
  /** 排序字段 */
  sortField: string
  /** 权限前缀 */
  permsPrefix: string
  /** 前端模板 1、element ui 2、element plus */
  frontTpl: number
  /** 前端样式 12,24 */
  frontStyle: number
  /** 操作按钮样式 */
  btnStyle: number
  /** 状态 */
  status: number
  /** 代码生成列 */
  columns?: HbtGenColumn[]
  /** 代码生成选项 */
  options?: CodeOptions
}

/**
 * 导出代码生成数据库表参数
 */
export interface HbtGenTableExport extends HbtGenTableQuery {
  /** 工作表名称 */
  sheetName?: string
}

/**
 * 代码生成数据库表分页结果
 */
export type HbtGenTablePageResult = HbtPagedResult<HbtGenTable>

/**
 * 数据库信息
 */
export interface HbtGenTableDatabaseInfo {
  /** 数据库名称 */
  name: string
  /** 数据库注释 */
  comment: string
}

/**
 * 表信息
 */
export interface TableInfoDto {
  /** 表名 */
  name: string
  /** 表描述 */
  description: string
  /** 表类型 */
  tableType: string
}

/**
 * 表信息元组
 */
export type TableInfoTuple = [string, string] 