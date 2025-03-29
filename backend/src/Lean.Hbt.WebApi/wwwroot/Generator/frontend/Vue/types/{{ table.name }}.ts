//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : {{ table.name }}.ts
// 创建者 : CodeGenerator
// 创建时间: {{ datetime }}
// 版本号 : v1.0.0
// 描述    : {{ table.comment }}类型定义
//===================================================================

import type { HbtPageQuery } from '@/types/common'

/**
 * {{ table.comment }}实体
 */
export interface {{ pascal_case table.table_name }} {
  {{~ for column in table.columns ~}}
  /** {{ column.comment }} */
  {{ column.column_name }}{{ column.is_nullable ? "?" : "" }}: {{ get_ts_type column.data_type }}
  {{~ end ~}}
}

/**
 * {{ table.comment }}查询参数
 */
export interface {{ pascal_case table.table_name }}Query extends HbtPageQuery {
  {{~ for column in table.columns ~}}
  {{~ if column.is_query ~}}
  /** {{ column.comment }} */
  {{ column.column_name }}?: {{ get_ts_type column.data_type }}
  {{~ end ~}}
  {{~ end ~}}
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * {{ table.comment }}创建参数
 */
export interface {{ pascal_case table.table_name }}Create {
  {{~ for column in table.columns ~}}
  {{~ if not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
  /** {{ column.comment }} */
  {{ column.column_name }}{{ column.is_nullable ? "?" : "" }}: {{ get_ts_type column.data_type }}
  {{~ end ~}}
  {{~ end ~}}
}

/**
 * {{ table.comment }}更新参数
 */
export interface {{ pascal_case table.table_name }}Update extends {{ pascal_case table.table_name }}Create {
  /** {{ get_pk_comment table }} */
  {{ get_pk_name table }}: number
}

/**
 * {{ table.comment }}状态参数
 */
export interface {{ pascal_case table.table_name }}Status {
  /** {{ get_pk_comment table }} */
  {{ get_pk_name table }}: number
  /** 状态(0:正常 1:停用) */
  status: number
}

/**
 * {{ table.comment }}导入参数
 */
export interface {{ pascal_case table.table_name }}Import {
  /** 文件 */
  file: File
} 