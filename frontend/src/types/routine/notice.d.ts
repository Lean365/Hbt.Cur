/** 通知相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 通知查询参数 */
export interface HbtNoticeQueryParams extends HbtPagedQuery {
  /** 标题 */
  noticeTitle?: string
  /** 类型（1通知 2公告） */
  noticeType?: number
  /** 状态（0草稿 1发布 2关闭） */
  noticeStatus?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 通知数据传输对象 */
export interface HbtNoticeDto extends HbtBaseEntity {
  /** ID */
  noticeId: number | bigint
  /** 标题 */
  noticeTitle: string
  /** 内容 */
  noticeContent: string
  /** 类型（1通知 2公告） */
  noticeType: number
  /** 状态（0草稿 1发布 2关闭） */
  noticeStatus: number
  /** 发布时间 */
  noticePublishTime?: Date
  /** 截止时间 */
  noticeDeadline?: Date
  /** 附件 */
  noticeAttachments?: string
  /** 访问链接 */
  noticeAccessUrl?: string
  /** 已读数量 */
  noticeReadCount: number
  /** 已读用户ID列表 */
  noticeReadIds?: string
  /** 确认数量 */
  noticeConfirmCount: number
  /** 确认用户ID列表 */
  noticeConfirmIds?: string
  /** 最后回执时间 */
  noticeLastReceiptTime?: Date
}

/** 通知创建DTO */
export interface HbtNoticeCreateDto {
  /** 标题 */
  noticeTitle: string
  /** 内容 */
  noticeContent: string
  /** 类型（1通知 2公告） */
  noticeType: number
  /** 状态（0草稿 1发布 2关闭） */
  noticeStatus: number
  /** 发布时间 */
  noticePublishTime?: Date | null
  /** 截止时间 */
  noticeDeadline?: Date | null
  /** 附件 */
  noticeAttachments?: string | null
  /** 访问链接 */
  noticeAccessUrl?: string | null
}

/** 通知更新DTO */
export interface HbtNoticeUpdateDto extends HbtNoticeCreateDto {
  /** ID */
  noticeId: bigint
}

/** 通知分页结果 */
export type HbtNoticePageResult = HbtPagedResult<HbtNoticeDto> 