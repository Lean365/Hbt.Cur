/** 新闻话题关系相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 新闻话题关系查询参数 */
export interface HbtNewsTopicRelationQuery extends HbtPagedQuery {
  newsId?: number
  topicId?: number
  relationType?: number
  relationWeight?: number
  relationUserId?: number
  relationUserName?: string
  isAutoRelation?: number
  relationStatus?: number
  startTime?: Date
  endTime?: Date
}

/** 新闻话题关系数据传输对象 */
export interface HbtNewsTopicRelation extends HbtBaseEntity {
  id: number
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationTime: Date
  relationUserId?: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
  news?: HbtNews
  topic?: HbtNewsTopic
}

/** 新闻话题关系创建参数 */
export interface HbtNewsTopicRelationCreate {
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationUserId?: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
}

/** 新闻话题关系更新参数 */
export interface HbtNewsTopicRelationUpdate extends HbtNewsTopicRelationCreate {
  id: number
}

/** 新闻话题关系状态更新参数 */
export interface HbtNewsTopicRelationStatusUpdate {
  id: number
  relationStatus: number
}

/** 新闻话题关系删除参数 */
export interface HbtNewsTopicRelationDelete {
  id: number
}

/** 新闻话题关系批量删除参数 */
export interface HbtNewsTopicRelationBatchDelete {
  ids: number[]
}

/** 新闻话题关系导入参数 */
export interface HbtNewsTopicRelationImport {
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
}

/** 新闻话题关系导出参数 */
export interface HbtNewsTopicRelationExport {
  id: number
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationTime: Date
  relationUserId?: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  createTime: Date
}

/** 新闻话题关系导入模板参数 */
export interface HbtNewsTopicRelationTemplate {
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
}

/** 新闻话题关系分页结果 */
export type HbtNewsTopicRelationPagedResult = HbtPagedResult<HbtNewsTopicRelation>

/** 自动关联请求参数 */
export interface HbtNewsTopicAutoRelateRequest {
  newsId: number
  newsTitle: string
  newsContent: string
  newsKeywords?: string
}

/** 关键词匹配话题结果 */
export interface HbtNewsTopicKeywordMatch {
  topicId: number
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  matchScore: number
  relationType: number
  relationWeight: number
}