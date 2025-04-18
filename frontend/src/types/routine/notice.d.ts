/** 通知相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 通知查询参数 */
export interface HbtNoticeQueryDto extends HbtPagedQuery {
  /** 通知标题 */
  noticeTitle?: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType?: number
  /** 通知状态（0未读 1已读） */
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
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType: number
  /** 通知状态（0未读 1已读） */
  noticeStatus: number
  /** 发送时间 */
  noticeSendTime?: Date
  /** 阅读时间 */
  noticeReadTime?: Date
  /** 接收人ID */
  noticeReceiverId: number | bigint
  /** 接收人名称 */
  noticeReceiverName: string
}

/** 通知创建DTO */
export interface HbtNoticeCreateDto {
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType: number
  /** 接收人ID */
  noticeReceiverId: number | bigint
  /** 接收人名称 */
  noticeReceiverName: string
}

/** 通知更新DTO */
export interface HbtNoticeUpdateDto extends HbtNoticeCreateDto {
  /** ID */
  noticeId: bigint
}

/** 通知分页结果 */
export type HbtNoticePagedResult = HbtPagedResult<HbtNoticeDto> 