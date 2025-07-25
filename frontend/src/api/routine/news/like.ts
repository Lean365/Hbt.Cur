import request from '@/utils/request'
import type { 
  HbtNewsLike, 
  HbtNewsLikeQuery, 
  HbtNewsLikeCreate, 
  HbtNewsLikeUpdate,
  HbtNewsLikeRequest,
  HbtNewsUnlikeRequest,
  HbtNewsCheckLikedRequest,
  HbtNewsGetLikedNewsRequest,
  HbtNewsGetLikedUsersRequest
} from '@/types/routine/news/like'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取新闻点赞列表
 */
export function getNewsLikeList(params: HbtNewsLikeQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtNewsLike>>>({
    url: '/api/HbtNewsLike/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻点赞详情
 */
export function getNewsLikeById(id: number | bigint) {
  return request<HbtApiResponse<HbtNewsLike>>({
    url: `/api/HbtNewsLike/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻点赞
 */
export function createNewsLike(data: HbtNewsLikeCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtNewsLike',
    method: 'post',
    data
  })
}

/**
 * 更新新闻点赞
 */
export function updateNewsLike(data: HbtNewsLikeUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsLike',
    method: 'put',
    data
  })
}

/**
 * 删除新闻点赞
 */
export function deleteNewsLike(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNewsLike/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻点赞
 */
export function batchDeleteNewsLike(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsLike/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 点赞新闻
 */
export function likeNews(data: HbtNewsLikeRequest) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsLike/like',
    method: 'post',
    data
  })
}

/**
 * 取消点赞新闻
 */
export function unlikeNews(data: HbtNewsUnlikeRequest) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsLike/unlike',
    method: 'post',
    data
  })
}

/**
 * 检查用户是否已点赞新闻
 */
export function isUserLiked(data: HbtNewsCheckLikedRequest) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsLike/check-liked',
    method: 'post',
    data
  })
}

/**
 * 获取用户点赞的新闻列表
 */
export function getLikedNewsByUserId(params: HbtNewsGetLikedNewsRequest) {
  return request<HbtApiResponse<HbtPagedResult<any>>>({
    url: '/api/HbtNewsLike/user-liked-news',
    method: 'get',
    params
  })
}

/**
 * 获取新闻点赞用户列表
 */
export function getLikedUsersByNewsId(params: HbtNewsGetLikedUsersRequest) {
  return request<HbtApiResponse<HbtPagedResult<any>>>({
    url: '/api/HbtNewsLike/news-liked-users',
    method: 'get',
    params
  })
}