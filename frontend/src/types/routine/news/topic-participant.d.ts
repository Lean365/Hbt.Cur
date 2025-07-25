/** 新闻话题参与者相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 参与类型枚举 */
export enum HbtParticipationType {
  Creator = 1,    // 创建者
  Admin = 2,      // 管理员
  Member = 3,     // 普通成员
  Guest = 4       // 访客
}

/** 参与状态枚举 */
export enum HbtParticipationStatus {
  Active = 1,     // 活跃
  Inactive = 2,   // 非活跃
  Banned = 3      // 被禁言
}

/** 通知类型枚举 */
export enum HbtNotificationType {
  All = 1,        // 全部通知
  Important = 2,  // 重要通知
  None = 3        // 不通知
}

/** 新闻话题参与者查询参数 */
export interface HbtNewsTopicParticipantQuery extends HbtPagedQuery {
  topicId?: number
  userId?: number
  userName?: string
  participationType?: HbtParticipationType
  participationStatus?: HbtParticipationStatus
  receiveNotification?: number
  notificationType?: HbtNotificationType
  startTime?: Date
  endTime?: Date
}

/** 新闻话题参与者数据传输对象 */
export interface HbtNewsTopicParticipant extends HbtBaseEntity {
  id: number
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: HbtParticipationType
  participationTime: Date
  participationStatus: HbtParticipationStatus
  contributionScore: number
  contentCount: number
  commentCount: number
  likeCount: number
  shareCount: number
  lastActiveTime?: Date
  participationRemark?: string
  receiveNotification: number
  notificationType: HbtNotificationType
  orderNum: number
  remark?: string
  topic?: HbtNewsTopic
}

/** 新闻话题参与者创建参数 */
export interface HbtNewsTopicParticipantCreate {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: HbtParticipationType
  participationStatus: HbtParticipationStatus
  participationRemark?: string
  receiveNotification: number
  notificationType: HbtNotificationType
  orderNum: number
  remark?: string
}

/** 新闻话题参与者更新参数 */
export interface HbtNewsTopicParticipantUpdate extends HbtNewsTopicParticipantCreate {
  id: number
}

/** 新闻话题参与者状态更新参数 */
export interface HbtNewsTopicParticipantStatusUpdate {
  id: number
  participationStatus: HbtParticipationStatus
}

/** 新闻话题参与者删除参数 */
export interface HbtNewsTopicParticipantDelete {
  id: number
}

/** 新闻话题参与者批量删除参数 */
export interface HbtNewsTopicParticipantBatchDelete {
  ids: number[]
}

/** 新闻话题参与者导入参数 */
export interface HbtNewsTopicParticipantImport {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: HbtParticipationType
  participationStatus: HbtParticipationStatus
  participationRemark?: string
  receiveNotification: number
  notificationType: HbtNotificationType
  orderNum: number
  remark?: string
}

/** 新闻话题参与者导出参数 */
export interface HbtNewsTopicParticipantExport {
  id: number
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: HbtParticipationType
  participationTime: Date
  participationStatus: HbtParticipationStatus
  contributionScore: number
  contentCount: number
  commentCount: number
  likeCount: number
  shareCount: number
  lastActiveTime?: Date
  participationRemark?: string
  receiveNotification: number
  notificationType: HbtNotificationType
  orderNum: number
  createTime: Date
}

/** 新闻话题参与者导入模板参数 */
export interface HbtNewsTopicParticipantTemplate {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: HbtParticipationType
  participationStatus: HbtParticipationStatus
  participationRemark?: string
  receiveNotification: number
  notificationType: HbtNotificationType
  orderNum: number
  remark?: string
}

/** 新闻话题参与者分页结果 */
export type HbtNewsTopicParticipantPagedResult = HbtPagedResult<HbtNewsTopicParticipant>

/** 用户参与话题请求参数 */
export interface HbtNewsTopicJoinRequest {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType?: HbtParticipationType
  participationRemark?: string
}

/** 用户退出话题请求参数 */
export interface HbtNewsTopicLeaveRequest {
  topicId: number
  userId: number
}

/** 检查用户参与状态请求参数 */
export interface HbtNewsTopicCheckJoinedRequest {
  topicId: number
  userId: number
}

/** 参与者统计信息 */
export interface HbtNewsTopicParticipantStatistics {
  totalParticipants: number
  activeParticipants: number
  creatorCount: number
  adminCount: number
  memberCount: number
  guestCount: number
  bannedCount: number
  totalContribution: number
  totalContent: number
  totalComments: number
  totalLikes: number
  totalShares: number
}