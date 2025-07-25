import request from '@/utils/request'
import type { 
  HbtNews, 
  HbtNewsQuery, 
  HbtNewsCreate, 
  HbtNewsUpdate, 
  HbtNewsStatusUpdate 
} from '@/types/routine/news/news'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取新闻列表
 */
export function getNewsList(params: HbtNewsQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtNews>>>({
    url: '/api/HbtNews/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻详情
 */
export function getNewsById(id: number | bigint) {
  return request<HbtApiResponse<HbtNews>>({
    url: `/api/HbtNews/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻
 */
export function createNews(data: HbtNewsCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtNews',
    method: 'post',
    data
  })
}

/**
 * 更新新闻
 */
export function updateNews(data: HbtNewsUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNews',
    method: 'put',
    data
  })
}

/**
 * 删除新闻
 */
export function deleteNews(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNews/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻
 */
export function batchDeleteNews(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNews/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新新闻状态
 */
export function updateNewsStatus(data: HbtNewsStatusUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNews/status',
    method: 'put',
    data
  })
}

/**
 * 获取热门新闻
 */
export function getHotNews(count = 10) {
  return request<HbtApiResponse<HbtNews[]>>({
    url: '/api/HbtNews/hot',
    method: 'get',
    params: { count }
  })
}

/**
 * 获取推荐新闻
 */
export function getRecommendedNews(count = 10) {
  return request<HbtApiResponse<HbtNews[]>>({
    url: '/api/HbtNews/recommended',
    method: 'get',
    params: { count }
  })
}

/**
 * 搜索新闻
 */
export function searchNews(keyword: string, count = 20) {
  return request<HbtApiResponse<HbtNews[]>>({
    url: '/api/HbtNews/search',
    method: 'get',
    params: { keyword, count }
  })
}

/**
 * 导出新闻数据
 */
export function exportNews(params: HbtNewsQuery) {
  return request<Blob>({
    url: '/api/HbtNews/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入新闻数据
 */
export function importNews(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtNews/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取新闻导入模板
 */
export function getNewsTemplate() {
  return request<Blob>({
    url: '/api/HbtNews/template',
    method: 'get',
    responseType: 'blob'
  })
} 