//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : table.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成数据库表类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 代码生成数据库表实体
 */
export interface HbtGenTable extends HbtBaseEntity {
  /** 表ID */
  tableId: number
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
  /** 状态（0：停用，1：正常） */
  status: number
  /** 使用的模板 */
  templateType: number
  /** 模块名称 */
  moduleName: string
  /** 业务名称 */
  businessName: string
  /** 功能名称 */
  functionName: string
  /** 作者名称 */
  author: string
  /** 生成方式 */
  genMode: number
  /** 存放位置 */
  genPath: string
  /** 其他选项 */
  options?: string
  /** 租户ID */
  tenantId: number
}

/**
 * 代码生成数据库表查询参数
 */
export interface HbtGenTableQuery extends HbtPagedQuery {
  /** 数据库名称 */
  databaseName?: string
  /** 表名 */
  tableName?: string
  /** 表描述 */
  tableComment?: string
  /** 实体类名 */
  className?: string
  /** 命名空间 */
  namespace?: string
  /** 基本命名空间前缀 */
  baseNamespace?: string
  /** C#类名 */
  csharpTypeName?: string
  /** 关联父表名 */
  parentTableName?: string
  /** 本表关联父表的外键名 */
  parentTableFkName?: string
  /** 状态（0：停用，1：正常） */
  status?: number
  /** 使用的模板 */
  templateType?: number
  /** 模块名称 */
  moduleName?: string
  /** 业务名称 */
  businessName?: string
  /** 功能名称 */
  functionName?: string
  /** 作者名称 */
  author?: string
  /** 生成方式 */
  genMode?: number
  /** 存放位置 */
  genPath?: string
  /** 其他选项 */
  options?: string
  /** 租户ID */
  tenantId?: number
  /** 日期范围 */
  dateRange?: [string, string]
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  orderType?: 'asc' | 'desc'
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
  /** 状态（0：停用，1：正常） */
  status: number
  /** 使用的模板 */
  templateType: number
  /** 模块名称 */
  moduleName: string
  /** 业务名称 */
  businessName: string
  /** 功能名称 */
  functionName: string
  /** 作者名称 */
  author: string
  /** 生成方式 */
  genMode: number
  /** 存放位置 */
  genPath: string
  /** 其他选项 */
  options?: string
  /** 租户ID */
  tenantId: number
}

/**
 * 代码生成数据库表更新参数
 */
export interface HbtGenTableUpdate extends HbtGenTableCreate {
  /** 表ID */
  tableId: number
}

/**
 * 批量删除代码生成数据库表参数
 */
export interface HbtGenTableBatchDelete {
  /** 表ID数组 */
  tableIds: number[]
}

/**
 * 更新代码生成数据库表状态参数
 */
export interface HbtGenTableStatusUpdate {
  /** 表ID */
  tableId: number
  /** 状态（0：停用，1：正常） */
  status: number
}

/**
 * 导入代码生成数据库表参数
 */
export interface HbtGenTableImport {
  /** 文件对象 */
  file: File
  /** 工作表名称 */
  sheetName?: string
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

declare namespace HbtGenTable {
  /** 数据库信息 */
  interface DatabaseInfo {
    /** 数据库名称 */
    name: string;
    /** 数据库注释 */
    comment: string;
  }

  /** 代码生成表 */
  interface Table {
    /** 表ID */
    id: number;
    /** 数据库名称 */
    databaseName: string;
    /** 表名称 */
    tableName: string;
    /** 表描述 */
    tableComment: string;
    /** 实体类名称 */
    className: string;
    /** 命名空间 */
    namespace: string;
    /** 基础命名空间 */
    baseNamespace: string;
    /** C#类型名称 */
    csharpTypeName: string;
    /** 生成模块名 */
    moduleName: string;
    /** 生成业务名 */
    businessName: string;
    /** 生成功能名 */
    functionName: string;
    /** 作者 */
    author: string;
    /** 生成方式 */
    genMode: string;
    /** 生成路径 */
    genPath: string;
    /** 生成选项 */
    options: string;
    /** 状态 */
    status: number;
    /** 备注 */
    remark: string;
    /** 创建者 */
    createBy: string;
    /** 创建时间 */
    createTime: string;
    /** 更新者 */
    updateBy: string;
    /** 更新时间 */
    updateTime: string;
    /** 字段列表 */
    columns?: HbtGenColumn.Column[];
  }

  /** 创建表参数 */
  interface Create extends Omit<Table, 'id' | 'createBy' | 'createTime' | 'updateBy' | 'updateTime'> {}

  /** 更新表参数 */
  interface Update extends Partial<Create> {
    /** 表ID */
    tableId: number;
  }

  /** 查询表参数 */
  interface Query {
    /** 表名称 */
    tableName?: string;
    /** 表描述 */
    tableComment?: string;
    /** 状态 */
    status?: number;
    /** 页码 */
    pageIndex: number;
    /** 每页记录数 */
    pageSize: number;
  }

  /** 分页结果 */
  interface PageResult {
    /** 数据列表 */
    rows: Table[];
    /** 总记录数 */
    totalNum: number;
    /** 页码 */
    pageIndex: number;
    /** 每页记录数 */
    pageSize: number;
  }
}

export interface TableInfoDto {
  name: string
  description: string
  tableType: string
}

export type TableInfoTuple = [string, string] 