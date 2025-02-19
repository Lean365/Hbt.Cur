// 在线用户查询参数
export interface OnlineUserQuery {
  pageNum?: number;
  pageSize?: number;
  userId?: number;
  userName?: string;
  ipAddress?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 在线用户对象
export interface OnlineUser {
  sessionId: string;
  userId: number;
  userName: string;
  deptName: string;
  ipAddress: string;
  loginLocation: string;
  browser: string;
  os: string;
  deviceType: string;
  status: string;
  lastAccessTime: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 强制下线参数
export interface ForceOffline {
  sessionId: string;
  reason: string;
}

// 在线用户导出参数
export interface OnlineUserExport extends OnlineUserQuery {
  orderByColumn?: string;
  isAsc?: string;
} 