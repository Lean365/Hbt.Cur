/** 新闻话题相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 话题状态枚举 */
export enum HbtTopicStatus {
  Active = 1,     // 活跃
  Inactive = 2,   // 非活跃
  Archived = 3    // 已归档
}

/** 话题类型枚举 */
export enum HbtTopicType {
  Normal = 1,     // 普通话题
  Event = 2,      // 事件话题
  Special = 3     // 专题话题
}

/** 新闻话题查询参数 */
export interface HbtNewsTopicQuery extends HbtPagedQuery {
  topicName?: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  status?: HbtTopicStatus
  topicType?: HbtTopicType
  topicIsHot?: number
  topicIsRecommend?: number
  topicIsTop?: number
  topicCreatorId?: number
  topicCreatorName?: string
  startTime?: Date
  endTime?: Date
}

/** 新闻话题数据传输对象 */
export interface HbtNewsTopic extends HbtBaseEntity {
  topicId: number
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: HbtTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: HbtTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicParticipantCount: number
  topicNewsCount: number
  topicCommentCount: number
  topicLikeCount: number
  topicShareCount: number
  topicReadCount: number
  topicCreatorId?: number
  topicCreatorName?: string
  topicAdminIds?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
  topicNewsRelations?: HbtNewsTopicRelation[]
  topicParticipants?: HbtNewsTopicParticipant[]
}

/** 新闻话题创建参数 */
export interface HbtNewsTopicCreate {
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: HbtTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: HbtTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicCreatorId?: number
  topicCreatorName?: string
  topicAdminIds?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
}

/** 新闻话题更新参数 */
export interface HbtNewsTopicUpdate extends HbtNewsTopicCreate {
  topicId: number
}

/** 新闻话题状态更新参数 */
export interface HbtNewsTopicStatusUpdate {
  topicId: number
  status: HbtTopicStatus
}

/** 新闻话题删除参数 */
export interface HbtNewsTopicDelete {
  topicId: number
}

/** 新闻话题批量删除参数 */
export interface HbtNewsTopicBatchDelete {
  topicIds: number[]
}

/** 新闻话题导入参数 */
export interface HbtNewsTopicImport {
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: HbtTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: HbtTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicCreatorName?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
}

/** 新闻话题导出参数 */
export interface HbtNewsTopicExport {
  topicId: number
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: HbtTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: HbtTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicParticipantCount: number
  topicNewsCount: number
  topicCommentCount: number
  topicLikeCount: number
  topicShareCount: number
  topicReadCount: number
  topicCreatorName?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  createTime: Date
}

/** 新闻话题导入模板参数 */
export interface HbtNewsTopicTemplate {
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: HbtTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: HbtTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicCreatorName?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
}

/** 新闻话题分页结果 */
export type HbtNewsTopicPagedResult = HbtPagedResult<HbtNewsTopic>

/** 话题统计信息 */
export interface HbtNewsTopicStatistics {
  totalTopics: number
  activeTopics: number
  hotTopics: number
  recommendedTopics: number
  totalParticipants: number
  totalNews: number
  totalComments: number
  totalLikes: number
  totalShares: number
  totalReads: number
}