/** 邮件相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 邮件查询参数 */
export interface HbtMailQueryParams extends HbtPagedQuery {
  /** 收件人 */
  mailTo?: string
  /** 主题 */
  mailSubject?: string
  /** 发送状态（0失败 1成功） */
  mailStatus?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 邮件数据传输对象 */
export interface HbtMailDto extends HbtBaseEntity {
  /** ID */
  mailId: number | bigint
  /** 发件人 */
  mailFrom: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml: boolean
  /** 抄送 */
  mailCc?: string
  /** 附件 */
  mailAttachments?: string
  /** 发送状态（0失败 1成功） */
  mailStatus: number
  /** 发送时间 */
  mailSendTime?: Date
  /** 错误信息 */
  mailErrorInfo?: string
}

/** 邮件创建DTO */
export interface HbtMailCreateDto {
  /** 发件人 */
  mailFrom: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml: boolean
  /** 抄送 */
  mailCc?: string | null
  /** 附件 */
  mailAttachments?: string | null
}

/** 邮件更新DTO */
export interface HbtMailUpdateDto extends HbtMailCreateDto {
  /** ID */
  mailId: bigint
}

/** 邮件发送DTO */
export interface HbtMailSendDto {
  /** 发件人 */
  mailFrom: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml: boolean
  /** 抄送 */
  mailCc?: string | null
  /** 附件 */
  mailAttachments?: string | null
}

/** 邮件分页结果 */
export type HbtMailPageResult = HbtPagedResult<HbtMailDto> 