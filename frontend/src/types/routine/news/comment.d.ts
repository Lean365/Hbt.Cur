/** 新闻评论相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 评论审核类型枚举 */
export enum HbtCommentAuditType {
  EditorAudit = 1,  // 责任编辑审核
  SystemAudit = 2,  // 系统自动审核
  ManualAudit = 3   // 人工审核
}

/** 新闻评论查询参数 */
export interface HbtNewsCommentQuery extends HbtPagedQuery {
  newsId?: number
  commentContent?: string
  commentUserId?: number
  commentUserName?: string
  parentCommentId?: number
  commentStatus?: number
  auditType?: HbtCommentAuditType
  auditorBy?: string
  startTime?: Date
  endTime?: Date
}

/** 新闻评论数据传输对象 */
export interface HbtNewsComment extends HbtBaseEntity {
  commentId: number
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  commentUserIp?: string
  commentUserAgent?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  auditType?: HbtCommentAuditType
  auditorBy?: string
  auditTime?: Date
  auditRemark?: string
  likeCount: number
  replyCount: number
  reportCount: number
  remark?: string
  parentComment?: HbtNewsComment
  replies?: HbtNewsComment[]
}

/** 新闻评论创建参数 */
export interface HbtNewsCommentCreate {
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  commentUserIp?: string
  commentUserAgent?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  remark?: string
}

/** 新闻评论更新参数 */
export interface HbtNewsCommentUpdate extends HbtNewsCommentCreate {
  commentId: number
}

/** 新闻评论审核参数 */
export interface HbtNewsCommentAudit {
  commentId: number
  commentStatus: number
  auditRemark?: string
}

/** 新闻评论批量审核参数 */
export interface HbtNewsCommentBatchAudit {
  commentIds: number[]
  commentStatus: number
  auditRemark?: string
}

/** 新闻评论删除参数 */
export interface HbtNewsCommentDelete {
  commentId: number
}

/** 新闻评论批量删除参数 */
export interface HbtNewsCommentBatchDelete {
  commentIds: number[]
}

/** 新闻评论导入参数 */
export interface HbtNewsCommentImport {
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  remark?: string
}

/** 新闻评论导出参数 */
export interface HbtNewsCommentExport {
  commentId: number
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  commentUserIp?: string
  commentUserAgent?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  auditType?: HbtCommentAuditType
  auditorBy?: string
  auditTime?: Date
  auditRemark?: string
  likeCount: number
  replyCount: number
  reportCount: number
  createTime: Date
}

/** 新闻评论导入模板参数 */
export interface HbtNewsCommentTemplate {
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  remark?: string
}

/** 新闻评论分页结果 */
export type HbtNewsCommentPagedResult = HbtPagedResult<HbtNewsComment>

/** 评论字数限制信息 */
export interface HbtCommentLengthLimit {
  minLength: number
  maxLength: number
}

/** 评论内容验证结果 */
export interface HbtCommentValidationResult {
  isValid: boolean
  message: string
}

/** 审核统计信息 */
export interface HbtCommentAuditStatistics {
  totalPending: number
  totalApproved: number
  totalRejected: number
  todayPending: number
  todayApproved: number
  todayRejected: number
}

/** 审核员工作量统计 */
export interface HbtCommentAuditorWorkload {
  auditorId: number
  auditorName: string
  totalAudited: number
  approvedCount: number
  rejectedCount: number
  approvalRate: number
}