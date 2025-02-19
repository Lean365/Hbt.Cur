// 异常日志查询参数
export interface ExceptionLogQuery {
  pageNum?: number;
  pageSize?: number;
  userName?: string;
  serviceName?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 异常日志对象
export interface ExceptionLog {
  id: number;
  userName: string;
  serviceName: string;
  methodName: string;
  requestMethod: string;
  requestUrl: string;
  requestParams: string;
  stackTrace: string;
  exceptionType: string;
  exceptionMessage: string;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 异常日志导出参数
export interface ExceptionLogExport extends ExceptionLogQuery {
  orderByColumn?: string;
  isAsc?: string;
} 