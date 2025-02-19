// 审计日志查询参数
export interface AuditLogQuery {
  pageNum?: number;
  pageSize?: number;
  userName?: string;
  ipaddr?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 审计日志对象
export interface AuditLog {
  id: number;
  userId: number;
  userName: string;
  operationType: string;
  operationName: string;
  operationPath: string;
  operationParam: string;
  operationResult: string;
  operationTime: string;
  operationIp: string;
  operationLocation: string;
  status: string;
  errorMsg: string;
  createTime: string;
}

// 审计日志导出参数
export interface AuditLogExport extends AuditLogQuery {
  orderByColumn?: string;
  isAsc?: string;
} 