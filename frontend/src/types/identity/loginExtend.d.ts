// 登录扩展查询参数
export interface LoginExtendQuery {
  pageNum?: number;
  pageSize?: number;
  userName?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 登录扩展对象
export interface LoginExtend {
  id: number;
  userId: number;
  userName: string;
  loginTime: string;
  loginIp: string;
  loginLocation: string;
  browser: string;
  os: string;
  device: string;
  status: string;
  msg: string;
  createTime: string;
}

// 登录扩展导出参数
export interface LoginExtendExport extends LoginExtendQuery {
  orderByColumn?: string;
  isAsc?: string;
}

// 登录扩展更新对象
export interface LoginExtendUpdate {
  userId: number;
  loginTime: string;
  loginIp: string;
  loginLocation: string;
  browser: string;
  os: string;
  device: string;
  status: string;
  msg: string;
}

// 登录扩展在线时段更新对象
export interface LoginExtendOnlinePeriodUpdate {
  userId: number;
  startTime: string;
  endTime: string;
} 