//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : {{ table.name }}.d.ts
// 创建者 : CodeGenerator
// 创建时间: {{ datetime }}
// 版本号 : v1.0.0
// 描述    : {{ table.comment }}类型定义
//===================================================================

/** {{ table.comment }}实体 */
export interface {{ pascal_case table.table_name }} {
  {{~ for column in table.columns ~}}
  /** {{ column.comment }} */
  {{ column.column_name }}{{ column.is_nullable ? "?" : "" }}: {{ get_ts_type column.data_type }}
  {{~ end ~}}
}

/** {{ table.comment }}查询参数 */
export interface {{ pascal_case table.table_name }}Query {
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
  /** 页码 */
  pageNum?: number
  /** 每页数量 */
  pageSize?: number
}

/** {{ table.comment }}创建参数 */
export interface {{ pascal_case table.table_name }}Create {
  {{~ for column in table.columns ~}}
  {{~ if not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
  /** {{ column.comment }} */
  {{ column.column_name }}{{ column.is_nullable ? "?" : "" }}: {{ get_ts_type column.data_type }}
  {{~ end ~}}
  {{~ end ~}}
}

/** {{ table.comment }}更新参数 */
export interface {{ pascal_case table.table_name }}Update extends {{ pascal_case table.table_name }}Create {
  /** {{ get_pk_comment table }} */
  {{ get_pk_name table }}: number
}

/** {{ table.comment }}导入参数 */
export interface {{ pascal_case table.table_name }}Import {
  /** 文件 */
  file: File
}

/** {{ table.comment }}导出参数 */
export interface {{ pascal_case table.table_name }}Export extends {{ pascal_case table.table_name }}Query {
  /** 导出类型(1:当前页 2:选中行 3:所有) */
  exportType: number
  /** 选中行的ID列表 */
  ids?: number[]
} 