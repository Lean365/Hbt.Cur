/** 新闻评论点赞相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 新闻评论点赞查询参数 */
export interface HbtNewsCommentLikeQuery extends HbtPagedQuery {
  commentId?: number
  userId?: number
  userName?: string
  likeTime?: Date
  startTime?: Date
  endTime?: Date
}

/** 新闻评论点赞数据传输对象 */
export interface HbtNewsCommentLike extends HbtBaseEntity {
  id: number
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  remark?: string
  comment?: HbtNewsComment
}

/** 新闻评论点赞创建参数 */
export interface HbtNewsCommentLikeCreate {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  remark?: string
}

/** 新闻评论点赞更新参数 */
export interface HbtNewsCommentLikeUpdate extends HbtNewsCommentLikeCreate {
  id: number
}

/** 新闻评论点赞删除参数 */
export interface HbtNewsCommentLikeDelete {
  id: number
}

/** 新闻评论点赞批量删除参数 */
export interface HbtNewsCommentLikeBatchDelete {
  ids: number[]
}

/** 新闻评论点赞导入参数 */
export interface HbtNewsCommentLikeImport {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻评论点赞导出参数 */
export interface HbtNewsCommentLikeExport {
  id: number
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  createTime: Date
}

/** 新闻评论点赞导入模板参数 */
export interface HbtNewsCommentLikeTemplate {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻评论点赞分页结果 */
export type HbtNewsCommentLikePagedResult = HbtPagedResult<HbtNewsCommentLike>

/** 新闻评论点赞状态更新参数 */
export interface HbtNewsCommentLikeStatusUpdate {
  id: number
  status: number
}

/** 点赞评论请求参数 */
export interface HbtNewsCommentLikeRequest {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
}

/** 取消点赞评论请求参数 */
export interface HbtNewsCommentUnlikeRequest {
  commentId: number
  userId: number
}

/** 检查用户点赞评论状态请求参数 */
export interface HbtNewsCommentCheckLikedRequest {
  commentId: number
  userId: number
}

/** 获取用户点赞的评论请求参数 */
export interface HbtNewsCommentGetLikedCommentsRequest {
  userId: number
  pageIndex?: number
  pageSize?: number
}

/** 获取评论点赞用户请求参数 */
export interface HbtNewsCommentGetLikedUsersRequest {
  commentId: number
  pageIndex?: number
  pageSize?: number
}