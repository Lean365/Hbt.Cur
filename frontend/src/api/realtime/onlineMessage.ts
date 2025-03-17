import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  OnlineMessageQuery, 
  OnlineMessage,
  OnlineMessageCreate
} from '@/types/realtime/onlineMessage'

// 获取在线消息列表
export function getOnlineMessageList(params: OnlineMessageQuery) {
  return request<HbtApiResponse<OnlineMessage[]>>({
    url: '/api/HbtOnlineMessage',
    method: 'get',
    params
  })
}

// 获取消息详情
export function getOnlineMessage(id: number) {
  return request<HbtApiResponse<OnlineMessage>>({
    url: `/api/HbtOnlineMessage/${id}`,
    method: 'get'
  })
}

// 导出在线消息
export function exportOnlineMessage(params: OnlineMessageQuery, sheetName: string = '在线消息信息') {
  return request<Blob>({
    url: '/api/HbtOnlineMessage/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 保存消息
export function saveOnlineMessage(data: OnlineMessageCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtOnlineMessage',
    method: 'post',
    data
  })
}

// 删除消息
export function deleteOnlineMessage(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOnlineMessage/${id}`,
    method: 'delete'
  })
}

// 清理过期消息
export function cleanupExpiredMessages(days: number = 7) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtOnlineMessage/cleanup',
    method: 'post',
    params: { days }
  })
} 