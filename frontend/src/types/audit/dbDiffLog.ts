// 数据库差异日志查询参数
export interface DbDiffLogQuery {
  pageNum?: number;
  pageSize?: number;
  tableName?: string;
  operationType?: string;
  operationUser?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 数据库差异日志对象
export interface DbDiffLog {
  id: number;
  tableName: string;
  operationType: string;
  operationUser: string;
  beforeData: string;
  afterData: string;
  diffResult: string;
  status: string;
  errorMsg: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 数据库差异日志导出参数
export interface DbDiffLogExport extends DbDiffLogQuery {
  orderByColumn?: string;
  isAsc?: string;
} 