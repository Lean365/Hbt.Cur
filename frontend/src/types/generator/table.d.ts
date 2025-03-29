/**
 * 代码生成表DTO
 */
export interface HbtGenTableDto {
  id?: number;
  tableName: string;
  tableComment: string;
  createTime?: string;
  updateTime?: string;
  className?: string;
  moduleName?: string;
  packageName?: string;
  businessName?: string;
  functionName?: string;
  functionAuthor?: string;
  remark?: string;
  columns: HbtGenColumnDto[];
  options?: string[];
  pageNum?: number;
  pageSize?: number;
  parentMenuId?: number;
  tplCategory?: string;
  genPath?: string;
}

export interface HbtGenTableQueryDto {
  pageIndex: number;
  pageSize: number;
  tableName?: string;
}

export interface HbtGenTablePageResultDto {
  items: HbtGenTableDto[];
  total: number;
}

export interface HbtGenTablePreviewDto {
  [key: string]: string;
}

/**
 * 代码生成列DTO
 */
export interface HbtGenColumnDto {
  id?: number;
  tableId: number;
  columnName: string;
  columnComment: string;
  dbColumnType: string;
  csharpType: string;
  csharpColumn: string;
  csharpLength: number;
  csharpDecimalDigits: number;
  isIncrement: number;
  isPrimaryKey: number;
  isRequired: number;
  isInsert: number;
  isEdit: number;
  isList: number;
  isQuery: number;
  queryType: string;
  isSort: number;
  isExport: number;
  displayType: string;
  dictType: string;
  orderNum: number;
  createTime?: string;
  updateTime?: string;
} 