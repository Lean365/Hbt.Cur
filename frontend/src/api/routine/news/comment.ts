import request from '@/utils/request'
import type { 
  HbtNewsComment, 
  HbtNewsCommentQuery, 
  HbtNewsCommentCreate, 
  HbtNewsCommentUpdate,
  HbtNewsCommentAudit,
  HbtNewsCommentBatchAudit,
  HbtCommentLengthLimit,
  HbtCommentValidationResult,
  HbtCommentAuditStatistics,
  HbtCommentAuditorWorkload
} from '@/types/routine/news/comment'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取新闻评论列表
 */
export function getNewsCommentList(params: HbtNewsCommentQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtNewsComment>>>({
    url: '/api/HbtNewsComment/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻评论详情
 */
export function getNewsCommentById(id: number | bigint) {
  return request<HbtApiResponse<HbtNewsComment>>({
    url: `/api/HbtNewsComment/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻评论
 */
export function createNewsComment(data: HbtNewsCommentCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtNewsComment',
    method: 'post',
    data
  })
}

/**
 * 更新新闻评论
 */
export function updateNewsComment(data: HbtNewsCommentUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsComment',
    method: 'put',
    data
  })
}

/**
 * 删除新闻评论
 */
export function deleteNewsComment(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNewsComment/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻评论
 */
export function batchDeleteNewsComment(ids: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsComment/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 获取新闻评论列表
 */
export function getCommentsByNewsId(newsId: number | bigint, pageIndex = 1, pageSize = 20) {
  return request<HbtApiResponse<HbtPagedResult<HbtNewsComment>>>({
    url: `/api/HbtNewsComment/news/${newsId}`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 获取评论回复列表
 */
export function getRepliesByCommentId(commentId: number | bigint, pageIndex = 1, pageSize = 20) {
  return request<HbtApiResponse<HbtPagedResult<HbtNewsComment>>>({
    url: `/api/HbtNewsComment/${commentId}/replies`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 审核新闻评论
 */
export function auditNewsComment(data: HbtNewsCommentAudit) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsComment/audit',
    method: 'post',
    data
  })
}

/**
 * 批量审核新闻评论
 */
export function batchAuditNewsComment(data: HbtNewsCommentBatchAudit) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNewsComment/batch-audit',
    method: 'post',
    data
  })
}

/**
 * 通过新闻评论
 */
export function approveNewsComment(commentId: number | bigint, auditRemark?: string) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNewsComment/${commentId}/approve`,
    method: 'post',
    params: { auditRemark }
  })
}

/**
 * 拒绝新闻评论
 */
export function rejectNewsComment(commentId: number | bigint, auditRemark?: string) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNewsComment/${commentId}/reject`,
    method: 'post',
    params: { auditRemark }
  })
}

/**
 * 获取待审核评论列表
 */
export function getPendingAuditList(pageIndex = 1, pageSize = 20) {
  return request<HbtApiResponse<HbtPagedResult<HbtNewsComment>>>({
    url: '/api/HbtNewsComment/pending-audit',
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 获取已审核评论列表
 */
export function getAuditedList(pageIndex = 1, pageSize = 20) {
  return request<HbtApiResponse<HbtPagedResult<HbtNewsComment>>>({
    url: '/api/HbtNewsComment/audited',
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 处理评论审核工作流
 */
export function processAuditWorkflow(commentId: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNewsComment/${commentId}/audit-workflow`,
    method: 'post'
  })
}

/**
 * 获取审核统计信息
 */
export function getAuditStatistics() {
  return request<HbtApiResponse<HbtCommentAuditStatistics>>({
    url: '/api/HbtNewsComment/audit-statistics',
    method: 'get'
  })
}

/**
 * 获取审核员工作量统计
 */
export function getAuditorWorkload(startDate: Date, endDate: Date) {
  return request<HbtApiResponse<HbtCommentAuditorWorkload[]>>({
    url: '/api/HbtNewsComment/auditor-workload',
    method: 'get',
    params: { startDate, endDate }
  })
}

/**
 * 获取评论字数限制
 */
export function getCommentLengthLimit() {
  return request<HbtApiResponse<HbtCommentLengthLimit>>({
    url: '/api/HbtNewsComment/length-limit',
    method: 'get'
  })
}

/**
 * 验证评论内容
 */
export function validateCommentContent(content: string) {
  return request<HbtApiResponse<HbtCommentValidationResult>>({
    url: '/api/HbtNewsComment/validate-content',
    method: 'post',
    data: content
  })
}