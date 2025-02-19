// 操作日志查询参数
export interface OperLogQuery {
  pageNum?: number;
  pageSize?: number;
  title?: string;
  operName?: string;
  businessType?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 操作日志对象
export interface OperLog {
  operId: number;
  title: string;
  businessType: number;
  method: string;
  requestMethod: string;
  operatorType: number;
  operName: string;
  deptName: string;
  operUrl: string;
  operIp: string;
  operLocation: string;
  operParam: string;
  jsonResult: string;
  status: number;
  errorMsg: string;
  operTime: string;
}

// 操作日志导出参数
export interface OperLogExport extends OperLogQuery {
  orderByColumn?: string;
  isAsc?: string;
} 