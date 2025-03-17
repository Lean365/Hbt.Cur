import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  OnlineUserQuery, 
  OnlineUser,
  ForceOffline,
  OnlineUserExport
} from '@/types/realtime/onlineUser'

// 获取在线用户列表
export function getOnlineUserList(params: OnlineUserQuery) {
  return request<HbtApiResponse<OnlineUser[]>>({
    url: '/api/HbtOnlineUser',
    method: 'get',
    params
  })
}

// 获取在线用户详情
export function getOnlineUser(sessionId: string) {
  return request<HbtApiResponse<OnlineUser>>({
    url: `/api/online-user/${sessionId}`,
    method: 'get'
  })
}

// 强制用户下线
export function forceOffline(data: ForceOffline) {
  return request<HbtApiResponse<any>>({
    url: `/api/online-user/${data.sessionId}/offline`,
    method: 'post',
    data: { reason: data.reason }
  })
}

// 批量强制用户下线
export function batchForceOffline(sessionIds: string[], reason: string) {
  return request<HbtApiResponse<any>>({
    url: '/api/online-user/batch/offline',
    method: 'post',
    data: { sessionIds, reason }
  })
}

// 获取在线用户统计信息
export function getOnlineUserStats() {
  return request<HbtApiResponse<{
    total: number;
    activeCount: number;
    idleCount: number;
  }>>({
    url: '/api/online-user/stats',
    method: 'get'
  })
}

// 导出在线用户
export function exportOnlineUser(params: OnlineUserQuery, sheetName: string = '在线用户信息') {
  return request({
    url: '/api/HbtOnlineUser/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取当前用户的在线状态
export function getCurrentUserStatus() {
  return request<HbtApiResponse<OnlineUser>>({
    url: '/api/online-user/current',
    method: 'get'
  })
}

// 强制用户下线
export function forceOfflineUser(connectionId: string) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtOnlineUser/${connectionId}`,
    method: 'delete'
  })
}

// 清理过期用户
export function cleanupExpiredUsers(minutes: number = 20) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtOnlineUser/cleanup',
    method: 'post',
    params: { minutes }
  })
} 