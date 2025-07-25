/** 新闻相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 新闻状态枚举 */
export enum HbtNewsStatus {
  Draft = 0,      // 草稿
  Published = 1,  // 已发布
  Offline = 2,    // 已下线
  Deleted = 3     // 已删除
}

/** 新闻查询参数 */
export interface HbtNewsQuery extends HbtPagedQuery {
  newsTitle?: string
  newsContent?: string
  newsCategory?: string
  newsTags?: string
  newsKeywords?: string
  status?: HbtNewsStatus
  isTop?: number
  isRecommend?: number
  isHot?: number
  authorId?: number
  authorName?: string
  publishDepartment?: string
  startTime?: Date
  endTime?: Date
}

/** 新闻数据传输对象 */
export interface HbtNews extends HbtBaseEntity {
  newsId: number
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsCover?: string
  newsImages?: string
  newsVideo?: string
  newsAudio?: string
  newsAttachments?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorId?: number
  authorName?: string
  authorAvatar?: string
  publishDepartment?: string
  publishTime?: Date
  expiryTime?: Date
  readCount: number
  likeCount: number
  commentCount: number
  shareCount: number
  recommendCount: number
  status: HbtNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
  newsTopicRelations?: HbtNewsTopicRelation[]
}

/** 新闻创建参数 */
export interface HbtNewsCreate {
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsCover?: string
  newsImages?: string
  newsVideo?: string
  newsAudio?: string
  newsAttachments?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorId?: number
  authorName?: string
  authorAvatar?: string
  publishDepartment?: string
  publishTime?: Date
  expiryTime?: Date
  status: HbtNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
}

/** 新闻更新参数 */
export interface HbtNewsUpdate extends HbtNewsCreate {
  newsId: number
}

/** 新闻状态更新参数 */
export interface HbtNewsStatusUpdate {
  newsId: number
  status: HbtNewsStatus
}

/** 新闻删除参数 */
export interface HbtNewsDelete {
  newsId: number
}

/** 新闻批量删除参数 */
export interface HbtNewsBatchDelete {
  newsIds: number[]
}

/** 新闻导入参数 */
export interface HbtNewsImport {
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorName?: string
  publishDepartment?: string
  publishTime?: Date
  status: HbtNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
}

/** 新闻导出参数 */
export interface HbtNewsExport {
  newsId: number
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorName?: string
  publishDepartment?: string
  publishTime?: Date
  readCount: number
  likeCount: number
  commentCount: number
  shareCount: number
  recommendCount: number
  status: HbtNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  createTime: Date
}

/** 新闻导入模板参数 */
export interface HbtNewsTemplate {
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorName?: string
  publishDepartment?: string
  publishTime?: Date
  status: HbtNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
}

/** 新闻分页结果 */
export type HbtNewsPagedResult = HbtPagedResult<HbtNews> 