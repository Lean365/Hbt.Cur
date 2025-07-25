/** 新闻点赞相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtNews } from './news'

/** 新闻点赞查询参数 */
export interface HbtNewsLikeQuery extends HbtPagedQuery {
  newsId?: number
  userId?: number
  userName?: string
  likeTime?: Date
  startTime?: Date
  endTime?: Date
}

/** 新闻点赞数据传输对象 */
export interface HbtNewsLike extends HbtBaseEntity {
  id: number
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  remark?: string
  news?: HbtNews
}

/** 新闻点赞创建参数 */
export interface HbtNewsLikeCreate {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  remark?: string
}

/** 新闻点赞更新参数 */
export interface HbtNewsLikeUpdate extends HbtNewsLikeCreate {
  id: number
}

/** 新闻点赞删除参数 */
export interface HbtNewsLikeDelete {
  id: number
}

/** 新闻点赞批量删除参数 */
export interface HbtNewsLikeBatchDelete {
  ids: number[]
}

/** 新闻点赞导入参数 */
export interface HbtNewsLikeImport {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻点赞导出参数 */
export interface HbtNewsLikeExport {
  id: number
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  createTime: Date
}

/** 新闻点赞导入模板参数 */
export interface HbtNewsLikeTemplate {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻点赞分页结果 */
export type HbtNewsLikePagedResult = HbtPagedResult<HbtNewsLike>

/** 新闻点赞状态更新参数 */
export interface HbtNewsLikeStatusUpdate {
  id: number
  status: number
}

/** 点赞新闻请求参数 */
export interface HbtNewsLikeRequest {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
}

/** 取消点赞新闻请求参数 */
export interface HbtNewsUnlikeRequest {
  newsId: number
  userId: number
}

/** 检查用户点赞状态请求参数 */
export interface HbtNewsCheckLikedRequest {
  newsId: number
  userId: number
}

/** 获取用户点赞的新闻请求参数 */
export interface HbtNewsGetLikedNewsRequest {
  userId: number
  pageIndex?: number
  pageSize?: number
}

/** 获取新闻点赞用户请求参数 */
export interface HbtNewsGetLikedUsersRequest {
  newsId: number
  pageIndex?: number
  pageSize?: number
}