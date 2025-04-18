import request from '@/utils/request'
import type { HbtOnlineUserQueryParams, HbtOnlineUserPageResult } from '@/types/signalr/onlineUser'
import { HbtApiResponse } from '@/types/common'

/** 获取在线用户列表 */
export function getOnlineUserList(params: HbtOnlineUserQueryParams) {
  return request<HbtApiResponse<HbtOnlineUserPageResult>>({
    url: '/api/HbtOnlineUser/list',
    method: 'get',
    params
  })
}

/** 强制用户下线 */
export function forceOfflineUser(connectionId: string) {
  return request({
    url: `/api/HbtOnlineUser/force-offline/${connectionId}`,
    method: 'post'
  })
}

/** 导出在线用户数据 */
export function exportOnlineUser(params: HbtOnlineUserQueryParams, sheetName = '在线用户信息') {
  return request({
    url: '/api/HbtOnlineUser/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/** 清理过期用户 */
export function cleanupExpiredUsers(days: number) {
  return request({
    url: '/api/HbtOnlineUser/cleanup',
    method: 'post',
    data: { days }
  })
}