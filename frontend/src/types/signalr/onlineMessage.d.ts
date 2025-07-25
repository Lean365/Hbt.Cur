import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 消息类型 */
export enum MessageType {
  /** 系统消息 */
  System = 'System',
  /** 邮件通知 */
  Email = 'Email',
  /** 通知状态 */
  Notification = 'Notification',
  /** 任务状态 */
  Task = 'Task',
  /** 个人通知 */
  Personal = 'Personal',
  /** 系统广播 */
  Broadcast = 'Broadcast'
}

/** 在线消息查询参数 */
export interface HbtOnlineMessageQueryParams extends HbtPagedQuery {
  /** 消息类型 */
  messageType?: string
  /** 消息状态 */
  messageStatus?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 在线消息数据传输对象 */
export interface HbtOnlineMessage extends HbtBaseEntity {
  /** ID */
  messageId: number | bigint
  /** 消息类型 */
  messageType: string
  /** 消息内容 */
  messageContent: string
  /** 消息状态（0未读 1已读） */
  messageStatus: number
  /** 发送时间 */
  sendTime: Date
  /** 发送者ID */
  senderId: number | bigint
  /** 发送者名称 */
  senderName: string
  /** 接收者ID */
  receiverId: number | bigint
  /** 接收者名称 */
  receiverName: string
}

/** 在线消息分页结果 */
export type HbtOnlineMessagePageResult = HbtPagedResult<HbtOnlineMessage> 