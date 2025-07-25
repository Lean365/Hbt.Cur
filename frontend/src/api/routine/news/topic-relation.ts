import request from '@/utils/request'
import type { 
  HbtNewsTopicRelation, 
  HbtNewsTopicRelationQuery, 
  HbtNewsTopicRelationCreate, 
  HbtNewsTopicRelationUpdate,
  HbtNewsTopicRelationStatusUpdate,
  HbtNewsTopicAutoRelateRequest,
  HbtNewsTopicKeywordMatch
} from '@/types/routine/news/topic-relation'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取新闻话题关系列表
 */
export function getNewsTopicRelationList(params: HbtNewsTopicRelationQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtNewsTopicRelation>>>({
    url: '/api/HbtNewsTopicRelation/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻话题关系详情
 */
export function getNewsTopicRelationById(id: number | bigint) {
  return request<HbtApiResponse<HbtNewsTopicRelation>>({
    url: `/api/HbtNewsTopicRelation/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻话题关系
 */
export function createNewsTopicRelation(data: HbtNewsTopicRelationCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtNewsTopicRelation',
    method: 'post',
    data
  })
}

/**
 * 更新新闻话题关系
 */
export function updateNewsTopicRelation(data: HbtNewsTopicRelationUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsTopicRelation',
    method: 'put',
    data
  })
}

/**
 * 删除新闻话题关系
 */
export function deleteNewsTopicRelation(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNewsTopicRelation/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻话题关系
 */
export function batchDeleteNewsTopicRelation(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsTopicRelation/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新新闻话题关系状态
 */
export function updateNewsTopicRelationStatus(data: HbtNewsTopicRelationStatusUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsTopicRelation/status',
    method: 'put',
    data
  })
}

/**
 * 关联新闻到话题
 */
export function relateNewsToTopic(newsId: number | bigint, topicId: number | bigint, relationType = 1, relationWeight = 1, isAutoRelation = false) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsTopicRelation/relate',
    method: 'post',
    data: { newsId, topicId, relationType, relationWeight, isAutoRelation }
  })
}

/**
 * 解除新闻与话题的关联
 */
export function unrelateNewsFromTopic(newsId: number | bigint, topicId: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsTopicRelation/unrelate',
    method: 'post',
    data: { newsId, topicId }
  })
}

/**
 * 批量关联新闻到话题
 */
export function batchRelateNewsToTopic(newsIds: (number | bigint)[], topicId: number | bigint, relationType = 1, relationWeight = 1, isAutoRelation = false) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtNewsTopicRelation/batch-relate',
    method: 'post',
    data: { newsIds, topicId, relationType, relationWeight, isAutoRelation }
  })
}

/**
 * 批量解除新闻与话题的关联
 */
export function batchUnrelateNewsFromTopic(newsIds: (number | bigint)[], topicId: number | bigint) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtNewsTopicRelation/batch-unrelate',
    method: 'post',
    data: { newsIds, topicId }
  })
}

/**
 * 获取新闻关联的话题列表
 */
export function getTopicsByNewsId(newsId: number | bigint) {
  return request<HbtApiResponse<HbtNewsTopicRelation[]>>({
    url: `/api/HbtNewsTopicRelation/news/${newsId}/topics`,
    method: 'get'
  })
}

/**
 * 获取话题关联的新闻列表
 */
export function getNewsByTopicId(topicId: number | bigint) {
  return request<HbtApiResponse<HbtNewsTopicRelation[]>>({
    url: `/api/HbtNewsTopicRelation/topic/${topicId}/news`,
    method: 'get'
  })
}

/**
 * 检查新闻是否关联到话题
 */
export function isNewsRelatedToTopic(newsId: number | bigint, topicId: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsTopicRelation/check-related',
    method: 'get',
    params: { newsId, topicId }
  })
}

/**
 * 获取新闻话题数量
 */
export function getNewsTopicCount(newsId: number | bigint) {
  return request<HbtApiResponse<number>>({
    url: `/api/HbtNewsTopicRelation/news/${newsId}/count`,
    method: 'get'
  })
}

/**
 * 获取话题新闻数量
 */
export function getTopicNewsCount(topicId: number | bigint) {
  return request<HbtApiResponse<number>>({
    url: `/api/HbtNewsTopicRelation/topic/${topicId}/count`,
    method: 'get'
  })
}

/**
 * 自动关联新闻到话题
 */
export function autoRelateNewsToTopics(data: HbtNewsTopicAutoRelateRequest) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtNewsTopicRelation/auto-relate',
    method: 'post',
    data
  })
}

/**
 * 根据关键词匹配话题
 */
export function matchTopicsByKeywords(keywords: string, count = 5) {
  return request<HbtApiResponse<HbtNewsTopicKeywordMatch[]>>({
    url: '/api/HbtNewsTopicRelation/match-topics',
    method: 'get',
    params: { keywords, count }
  })
}