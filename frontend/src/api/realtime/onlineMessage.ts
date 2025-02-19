import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  OnlineMessageQuery, 
  OnlineMessage,
  OnlineMessageDto
} from '@/types/realtime/onlineMessage'

// 获取在线消息列表
export function getOnlineMessageList(params: OnlineMessageQuery) {
  return request<HbtApiResult<OnlineMessage[]>>({
    url: '/api/HbtOnlineMessage',
    method: 'get',
    params
  })
}

// 获取消息详情
export function getOnlineMessage(id: number) {
  return request<HbtApiResult<OnlineMessage>>({
    url: `/api/HbtOnlineMessage/${id}`,
    method: 'get'
  })
}

// 导出在线消息
export function exportOnlineMessage(params: OnlineMessageQuery, sheetName: string = '在线消息信息') {
  return request({
    url: '/api/HbtOnlineMessage/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 保存消息
export function saveOnlineMessage(data: OnlineMessageDto) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtOnlineMessage',
    method: 'post',
    data
  })
}

// 删除消息
export function deleteOnlineMessage(id: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtOnlineMessage/${id}`,
    method: 'delete'
  })
}

// 清理过期消息
export function cleanupExpiredMessages(days: number = 7) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtOnlineMessage/cleanup',
    method: 'post',
    params: { days }
  })
} 