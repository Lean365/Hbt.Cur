import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  LoginLogQuery, 
  LoginLog,
  LoginLogExport
} from '@/types/audit/loginLog'

// 获取登录日志列表
export function getLoginLogList(params: LoginLogQuery) {
  return request<HbtApiResult<LoginLog[]>>({
    url: '/api/HbtLoginLog',
    method: 'get',
    params
  })
}

// 获取登录日志详情
export function getLoginLog(id: number) {
  return request<HbtApiResult<LoginLog>>({
    url: `/api/HbtLoginLog/${id}`,
    method: 'get'
  })
}

// 清空登录日志
export function clearLoginLog() {
  return request<HbtApiResult<any>>({
    url: '/api/HbtLoginLog/clear',
    method: 'delete'
  })
}

// 导出登录日志
export function exportLoginLog(params: LoginLogExport, sheetName: string = '登录日志') {
  return request({
    url: '/api/HbtLoginLog/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 解锁用户
export function unlockUser(userId: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtLoginLog/unlock/${userId}`,
    method: 'post'
  })
}