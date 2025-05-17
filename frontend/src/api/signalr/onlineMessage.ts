import request from '@/utils/request'
import type { HbtOnlineMessageQueryParams, HbtOnlineMessagePageResult, HbtOnlineMessageDto } from '@/types/signalr/onlineMessage'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/** 获取在线消息列表 */
export function getOnlineMessageList(params: HbtOnlineMessageQueryParams) {
  return request<HbtApiResponse<HbtPagedResult<HbtOnlineMessageDto>>>({
    url: '/api/HbtOnlineMessage/list',
    method: 'get',
    params
  })
}

/** 获取消息详情 */
export function getOnlineMessage(id: number | bigint) {
  return request<HbtApiResponse<HbtOnlineMessageDto>>({
    url: `/api/HbtOnlineMessage/${id}`,
    method: 'get'
  })
}

/** 保存消息 */
export function saveOnlineMessage(data: HbtOnlineMessageDto) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtOnlineMessage',
    method: 'post',
    data
  })
}

/** 删除在线消息 */
export function deleteOnlineMessage(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOnlineMessage/${id}`,
    method: 'delete'
  })
}

/** 导出在线消息数据 */
export function exportOnlineMessage(params: HbtOnlineMessageQueryParams, sheetName: string = '在线消息信息') {
  return request<HbtApiResponse<Blob>>({
    url: '/api/HbtOnlineMessage/export',
    method: 'get',
    params: { ...params, sheetName }
  })
}

/** 清理过期消息 */
export function cleanupExpiredMessages(days: number = 7) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOnlineMessage/cleanup',
    method: 'post',
    params: { days }
  })
}

/** 标记消息为已读 */
export function markMessageAsRead(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOnlineMessage/${id}/read`,
    method: 'put'
  })
}

/** 标记所有消息为已读 */
export function markAllMessagesAsRead() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOnlineMessage/read-all',
    method: 'put'
  })
}

/** 标记消息为未读 */
export function markMessageAsUnread(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOnlineMessage/${id}/unread`,
    method: 'put'
  })
}

/** 标记所有消息为未读 */
export function markAllMessagesAsUnread() {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOnlineMessage/unread-all',
    method: 'put'
  })
} 