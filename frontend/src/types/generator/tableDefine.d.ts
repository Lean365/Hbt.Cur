import type { HbtPageQuery, HbtPagedResult } from '@/types/common'

/**
 * 代码生成表定义DTO
 */
export interface HbtGenTableDefineDto {
  /** 主键ID */
  id: number;
  /** 表名 */
  tableName: string;
  /** 表注释 */
  tableComment: string;
  /** 实体类名 */
  className: string;
  /** 模块名 */
  moduleName: string;
  /** 包名 */
  packageName: string;
  /** 业务名 */
  businessName: string;
  /** 功能名 */
  functionName: string;
  /** 作者 */
  author: string;
  /** 生成类型（1：单表，2：主从表） */
  genType: number;
  /** 主表ID */
  parentTableId?: number;
  /** 主表外键 */
  parentTableFk?: string;
  /** 生成模板类型（1：基础模板，2：自定义模板） */
  templateType: number;
  /** 自定义模板ID */
  customTemplateId?: number;
  /** 是否生成查询条件 */
  enableQuery: boolean;
  /** 是否生成新增功能 */
  enableAdd: boolean;
  /** 是否生成修改功能 */
  enableEdit: boolean;
  /** 是否生成删除功能 */
  enableDelete: boolean;
  /** 是否生成导入功能 */
  enableImport: boolean;
  /** 是否生成导出功能 */
  enableExport: boolean;
  /** 是否生成批量删除功能 */
  enableBatchDelete: boolean;
  /** 是否生成批量导出功能 */
  enableBatchExport: boolean;
  /** 是否生成树形结构 */
  enableTree: boolean;
  /** 树形结构父字段 */
  treeParentField?: string;
  /** 树形结构子字段 */
  treeChildField?: string;
  /** 备注 */
  remark?: string;
  /** 状态（0：停用，1：正常） */
  status: number;
  /** 创建时间 */
  createTime?: string;
  /** 更新时间 */
  updateTime?: string;
}

/**
 * 代码生成表定义查询参数
 */
export interface HbtGenTableDefineQuery extends HbtPageQuery {
  /** 表名 */
  tableName?: string;
  /** 表注释 */
  tableComment?: string;
  /** 实体类名 */
  className?: string;
  /** 模块名 */
  moduleName?: string;
  /** 业务名 */
  businessName?: string;
  /** 功能名 */
  functionName?: string;
  /** 作者 */
  author?: string;
  /** 生成类型 */
  genType?: number;
  /** 状态 */
  status?: number;
  /** 日期范围 */
  dateRange?: [string, string];
}

/**
 * 代码生成表定义创建参数
 */
export interface HbtGenTableDefineCreate {
  /** 表名 */
  tableName: string;
  /** 表注释 */
  tableComment: string;
  /** 实体类名 */
  className: string;
  /** 模块名 */
  moduleName: string;
  /** 包名 */
  packageName: string;
  /** 业务名 */
  businessName: string;
  /** 功能名 */
  functionName: string;
  /** 作者 */
  author: string;
  /** 生成类型 */
  genType: number;
  /** 主表ID */
  parentTableId?: number;
  /** 主表外键 */
  parentTableFk?: string;
  /** 生成模板类型 */
  templateType: number;
  /** 自定义模板ID */
  customTemplateId?: number;
  /** 是否生成查询条件 */
  enableQuery: boolean;
  /** 是否生成新增功能 */
  enableAdd: boolean;
  /** 是否生成修改功能 */
  enableEdit: boolean;
  /** 是否生成删除功能 */
  enableDelete: boolean;
  /** 是否生成导入功能 */
  enableImport: boolean;
  /** 是否生成导出功能 */
  enableExport: boolean;
  /** 是否生成批量删除功能 */
  enableBatchDelete: boolean;
  /** 是否生成批量导出功能 */
  enableBatchExport: boolean;
  /** 是否生成树形结构 */
  enableTree: boolean;
  /** 树形结构父字段 */
  treeParentField?: string;
  /** 树形结构子字段 */
  treeChildField?: string;
  /** 备注 */
  remark?: string;
  /** 状态 */
  status: number;
}

/**
 * 代码生成表定义更新参数
 */
export interface HbtGenTableDefineUpdate extends HbtGenTableDefineCreate {
  /** 主键ID */
  id: number;
}

/**
 * 代码生成表定义导入参数
 */
export interface HbtGenTableDefineImport {
  /** 文件对象 */
  file: File;
  /** 工作表名称 */
  sheetName?: string;
}

/**
 * 代码生成表定义分页结果
 */
export type HbtGenTableDefinePagedResult = HbtPagedResult<HbtGenTableDefineDto> 