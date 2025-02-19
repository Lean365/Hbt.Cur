// 登录日志查询参数
export interface LoginLogQuery {
  pageNum?: number;
  pageSize?: number;
  ipaddr?: string;
  userName?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 登录日志对象
export interface LoginLog {
  infoId: number;
  userName: string;
  ipaddr: string;
  loginLocation: string;
  browser: string;
  os: string;
  status: string;
  msg: string;
  loginTime: string;
}

// 登录日志导出参数
export interface LoginLogExport extends LoginLogQuery {
  orderByColumn?: string;
  isAsc?: string;
} 