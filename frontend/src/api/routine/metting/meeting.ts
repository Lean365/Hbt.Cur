import request from '@/utils/request'
import type { 
  HbtMeeting, 
  HbtMeetingQuery, 
  HbtMeetingCreate, 
  HbtMeetingUpdate 
} from '@/types/routine/meeting/meeting'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取会议列表
 */
export function getMeetingList(params: HbtMeetingQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtMeeting>>>({
    url: '/api/HbtMeeting/list',
    method: 'get',
    params
  })
}

/**
 * 获取会议详情
 */
export function getMeetingById(id: number | bigint) {
  return request<HbtApiResponse<HbtMeeting>>({
    url: `/api/HbtMeeting/${id}`,
    method: 'get'
  })
}

/**
 * 创建会议
 */
export function createMeeting(data: HbtMeetingCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtMeeting',
    method: 'post',
    data
  })
}

/**
 * 更新会议
 */
export function updateMeeting(data: HbtMeetingUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMeeting',
    method: 'put',
    data
  })
}

/**
 * 删除会议
 */
export function deleteMeeting(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMeeting/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除会议
 */
export function batchDeleteMeeting(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMeeting/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 获取我的会议列表
 */
export function getMyMeetingList(params: HbtMeetingQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtMeeting>>>({
    url: '/api/HbtMeeting/my',
    method: 'get',
    params
  })
}

/**
 * 获取热门会议
 */
export function getHotMeetings(count = 10) {
  return request<HbtApiResponse<HbtMeeting[]>>({
    url: '/api/HbtMeeting/hot',
    method: 'get',
    params: { count }
  })
}

/**
 * 获取推荐会议
 */
export function getRecommendedMeetings(count = 10) {
  return request<HbtApiResponse<HbtMeeting[]>>({
    url: '/api/HbtMeeting/recommended',
    method: 'get',
    params: { count }
  })
} 