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
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 实体类名 */
  entityClassName: string
  /** 实体命名空间 */
  entityNamespace: string
  /** 基本命名空间前缀 */
  baseNamespace: string
  /** DTO类型 */
  dtoType: string[]
  /** DTO命名空间 */
  dtoNamespace: string
  /** DTO类名 */
  dtoClassName: string
  /** 服务命名空间 */
  serviceNamespace: string
  /** 服务接口类名 */
  iServiceClassName: string
  /** 服务类名 */
  serviceClassName: string
  /** 仓储接口命名空间 */
  iRepositoryNamespace: string
  /** 仓储接口类名 */
  iRepositoryClassName: string
  /** 仓储命名空间 */
  repositoryNamespace: string
  /** 仓储类名 */
  repositoryClassName: string
  /** 控制器命名空间 */
  controllerNamespace: string
  /** 控制器类名 */
  controllerClassName: string
  /** 模板类型 */
  tplType: string
  /** 使用的模板 */
  tplCategory: string
  /** 关联子表名 */
  subTableName?: string
  /** 本表关联子表的外键名 */
  subTableFkName?: string
  /** 树编码字段 */
  treeCode: string
  /** 树名称字段 */
  treeName: string
  /** 树父编码字段 */
  treeParentCode: string
  /** 模块名称 */
  moduleName: string
  /** 业务名称 */
  businessName: string
  /** 功能名称 */
  functionName: string
  /** 作者名称 */
  author: string
  /** 生成代码方式 */
  genType: string
  /** 存放位置 */
  genPath: string
  /** 上级菜单ID */
  parentMenuId: number
  /** 排序类型 */
  sortType: string
  /** 排序字段 */
  sortField: string
  /** 权限前缀 */
  permsPrefix: string
  /** 是否生成菜单 */
  generateMenu: number
  /** 前端模板 */
  frontTpl: number
  /** 按钮样式 */
  btnStyle: number
  /** 前端样式 */
  frontStyle: number
  /** 状态（0：停用，1：正常） */
  status: number
  /** 基础选项 */
  options: CodeOptions
  /** 字段列表 */
  columns?: HbtGenColumn[]
  /** 子表信息 */
  subTable?: HbtGenTable
  /** 租户ID */
  tenantId: number
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
  /** CRUD组 */
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
  /** 实体类名 */
  entityClassName: string
  /** 实体命名空间 */
  entityNamespace: string
  /** 基本命名空间前缀 */
  baseNamespace: string
  /** DTO类型 */
  dtoType: string[]
  /** DTO命名空间 */
  dtoNamespace: string
  /** DTO类名 */
  dtoClassName: string
  /** 服务命名空间 */
  serviceNamespace: string
  /** 服务接口类名 */
  iServiceClassName: string
  /** 服务类名 */
  serviceClassName: string
  /** 仓储接口命名空间 */
  iRepositoryNamespace: string
  /** 仓储接口类名 */
  iRepositoryClassName: string
  /** 仓储命名空间 */
  repositoryNamespace: string
  /** 仓储类名 */
  repositoryClassName: string
  /** 控制器命名空间 */
  controllerNamespace: string
  /** 控制器类名 */
  controllerClassName: string
  /** 模板类型 */
  tplType: string
  /** 使用的模板 */
  tplCategory: string
  /** 关联子表名 */
  subTableName?: string
  /** 本表关联子表的外键名 */
  subTableFkName?: string
  /** 树编码字段 */
  treeCode: string
  /** 树名称字段 */
  treeName: string
  /** 树父编码字段 */
  treeParentCode: string
  /** 模块名称 */
  moduleName: string
  /** 业务名称 */
  businessName: string
  /** 功能名称 */
  functionName: string
  /** 作者名称 */
  author: string
  /** 生成代码方式 */
  genType: string
  /** 存放位置 */
  genPath: string
  /** 上级菜单ID */
  parentMenuId: number
  /** 排序类型 */
  sortType: string
  /** 排序字段 */
  sortField: string
  /** 权限前缀 */
  permsPrefix: string
  /** 是否生成菜单 */
  generateMenu: number
  /** 前端模板 */
  frontTpl: number
  /** 按钮样式 */
  btnStyle: number
  /** 前端样式 */
  frontStyle: number
  /** 状态（0：停用，1：正常） */
  status: number
  /** 基础选项 */
  options: CodeOptions
  /** 字段列表 */
  columns?: HbtGenColumn[]
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
    /** 基本命名空间前缀 */
    baseNamespace: string
      /** 实体命名空间 */
  entityNamespace: string
  /** 实体类名 */
  entityClassName: string



  /** DTO命名空间 */
  dtoNamespace: string
  /** DTO类名 */
  dtoClassName: string
    /** DTO类型 */
    dtoType: string[]
  /** 服务命名空间 */
  serviceNamespace: string
  /** 服务接口类名 */
  iServiceClassName: string
  /** 服务类名 */
  serviceClassName: string
  /** 仓储接口命名空间 */
  iRepositoryNamespace: string
  /** 仓储接口类名 */
  iRepositoryClassName: string
  /** 仓储命名空间 */
  repositoryNamespace: string
  /** 仓储类名 */
  repositoryClassName: string
  /** 控制器命名空间 */
  controllerNamespace: string
  /** 控制器类名 */
  controllerClassName: string
  /** 模板类型 */
  tplType: string
  /** 使用的模板 */
  tplCategory: string
  /** 关联子表名 */
  subTableName?: string
  /** 本表关联子表的外键名 */
  subTableFkName?: string
  /** 树编码字段 */
  treeCode: string
  /** 树名称字段 */
  treeName: string
  /** 树父编码字段 */
  treeParentCode: string
  /** 模块名称 */
  moduleName: string
  /** 业务名称 */
  businessName: string
  /** 功能名称 */
  functionName: string
  /** 作者名称 */
  author: string
  /** 生成代码方式 */
  genType: string
  /** 存放位置 */
  genPath: string
  /** 上级菜单ID */
  parentMenuId: number
  /** 排序类型 */
  sortType: string
  /** 排序字段 */
  sortField: string
  /** 权限前缀 */
  permsPrefix: string
  /** 是否生成菜单 */
  generateMenu: number
  /** 前端模板 */
  frontTpl: number
  /** 按钮样式 */
  btnStyle: number
  /** 前端样式 */
  frontStyle: number
  /** 状态（0：停用，1：正常） */
  status: number
  /** 基础选项 */
  options: CodeOptions
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