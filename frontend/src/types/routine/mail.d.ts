/** 邮件相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 邮件查询参数 */
export interface HbtMailQueryDto {
  pageIndex: number
  pageSize: number
  mailTo?: string
  mailSubject?: string
  mailStatus?: number
  startTime?: Date
  endTime?: Date
}

/** 邮件数据传输对象 */
export interface HbtMailDto extends HbtBaseEntity {
  /** ID */
  mailId: bigint
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
  /** 创建时间 */
  createTime?: Date
  /** 更新时间 */
  updateTime?: Date
  /** 备注 */
  remark?: string
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
  mailIsHtml: number
  /** 抄送 */
  mailCc?: string
  /** 附件 */
  mailAttachments?: string
  /** 发送状态（0失败 1成功） */
  mailStatus: number
  /** 备注 */
  remark?: string
}

/** 邮件更新DTO */
export interface HbtMailUpdateDto {
  /** ID */
  mailId: bigint
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
  /** 备注 */
  remark?: string
}

/** 邮件发送DTO */
export interface HbtMailSendDto {
  /** ID */
  mailId?: bigint
  /** 发件人 */
  mailFrom?: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml?: boolean
  /** 抄送 */
  mailCc?: string
  /** 附件 */
  mailAttachments?: string
}

/** 邮件分页结果 */
export interface HbtMailPagedResult {
  rows: HbtMailDto[]
  totalNum: number
} 