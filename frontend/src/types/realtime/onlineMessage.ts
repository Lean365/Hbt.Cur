// 在线消息查询参数
export interface OnlineMessageQuery {
  pageNum?: number;
  pageSize?: number;
  senderId?: number;
  receiverId?: number;
  messageType?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 在线消息对象
export interface OnlineMessage {
  messageId: number;
  senderId: number;
  senderName: string;
  receiverId: number;
  receiverName: string;
  messageType: string;
  messageContent: string;
  readTime?: string;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建在线消息参数
export interface OnlineMessageCreate {
  senderId: number;
  receiverId: number;
  messageType: string;
  messageContent: string;
  remark?: string;
}

// 更新在线消息参数
export interface OnlineMessageUpdate extends OnlineMessageCreate {
  messageId: number;
}

// 在线消息状态更新参数
export interface OnlineMessageStatus {
  messageId: number;
  status: string;
}

// 在线消息已读更新参数
export interface OnlineMessageRead {
  messageId: number;
  readTime: string;
} 