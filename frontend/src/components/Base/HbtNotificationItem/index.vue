<template>
  <div class="notification-item" :class="{ unread: data.status === 'unread' }">
    <div class="item-icon">
      <template v-if="data.type === 'system'">
        <notification-outlined />
      </template>
      <template v-else-if="data.type === 'task'">
        <schedule-outlined />
      </template>
      <template v-else>
        <message-outlined />
      </template>
    </div>
    <div class="item-content">
      <div class="item-title">{{ formatTitle(data.title) }}</div>
      <div class="item-body">{{ formatContent(data.content) }}</div>
      <div class="item-footer">
        <span class="item-time">{{ formatTime(data.createTime) }}</span>
        <div class="action-buttons">
          <a-button
            v-if="data.status === 'unread'"
            type="text"
            size="small"
            class="action-btn"
            @click.stop="$emit('read', data.id)"
          >
            {{ t('notification.markAsRead') }}
          </a-button>
          <a-divider type="vertical" v-if="data.status === 'unread'" />
          <a-button
            type="text"
            size="small"
            class="action-btn delete-btn"
            @click.stop="$emit('delete', data.id)"
          >
            {{ t('notification.delete') }}
          </a-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { NotificationOutlined, ScheduleOutlined, MessageOutlined } from '@ant-design/icons-vue'
import type { NotificationItem } from '@/types/settings'
import dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime'

dayjs.extend(relativeTime)

const { t } = useI18n()

// Props
interface Props {
  data: NotificationItem
}

const props = defineProps<Props>()

// Emits
defineEmits<{
  (e: 'read', id: string): void
  (e: 'delete', id: string): void
}>()

// 格式化时间
const formatTime = (time: string) => {
  return dayjs(time).fromNow()
}

// 格式化消息内容
const formatContent = (content: string) => {
  try {
    // 尝试解析JSON内容
    const data = JSON.parse(content)
    
    // 如果是消息格式
    if (data.senderId && data.receiverId) {
      return `发送者: ${data.senderId}
接收者: ${data.receiverId}
内容: ${data.content || data.Content || '无内容'}`
    }
    
    // 如果有标准内容字段
    if (data.Content || data.content) {
      return data.Content || data.content
    }

    // 如果是通知格式
    if (data.type || data.messageType) {
      const type = data.type || data.messageType
      const title = data.title || data.messageTitle || ''
      const msg = data.message || data.content || data.Content || ''
      return `类型: ${t(`components.notification.types.${type}.label`) || type}
${title ? `标题: ${title}\n` : ''}${msg ? `消息: ${msg}` : ''}`
    }

    // 如果是任务格式
    if (data.taskId || data.jobId) {
      return `任务ID: ${data.taskId || data.jobId}
状态: ${data.status || data.state || '未知'}
${data.message ? `详情: ${data.message}` : ''}`
    }

    // 如果是普通对象，格式化显示
    const formatted = Object.entries(data)
      .filter(([_, value]) => value !== undefined && value !== null)
      .map(([key, value]) => {
        // 处理时间戳
        if (key.toLowerCase().includes('time') || key.toLowerCase().includes('date')) {
          if (typeof value === 'string' || typeof value === 'number' || value instanceof Date) {
            try {
              return `${key}: ${dayjs(value).format('YYYY-MM-DD HH:mm:ss')}`
            } catch {
              return `${key}: ${value}`
            }
          }
        }
        return `${key}: ${value}`
      })
      .join('\n')
    
    return formatted || '无内容'
  } catch {
    // 如果不是JSON，直接返回原内容
    return content || '无内容'
  }
}

// 格式化标题
const formatTitle = (title: string) => {
  try {
    const data = JSON.parse(title)
    // 优先使用标准标题字段
    if (data.Title || data.title) {
      return data.Title || data.title
    }
    // 根据消息类型生成标题
    if (data.type || data.messageType) {
      const type = data.type || data.messageType
      return t(`components.notification.types.${type}.label`) || '新消息'
    }
    // 如果是任务相关
    if (data.taskId || data.jobId) {
      return `任务通知 #${data.taskId || data.jobId}`
    }
    return '新消息'
  } catch {
    return title || '新消息'
  }
}
</script>

<style lang="less" scoped>
.notification-item {
  display: flex;
  padding: 16px;
  cursor: pointer;
  transition: all 0.3s;
  width: 100%;
  border-bottom: 1px solid var(--ant-color-border);
  background: var(--ant-color-bg-container);

  &:last-child {
    border-bottom: none;
  }

  .item-icon {
    flex-shrink: 0;
    margin-right: 16px;
    width: 40px;
    height: 40px;
    border-radius: 8px;
    display: flex;
    align-items: center;
    justify-content: center;
    background: var(--ant-color-primary-bg);
    
    :deep(.anticon) {
      font-size: 20px;
      color: var(--ant-color-primary);
    }
  }

  .item-content {
    flex: 1;
    min-width: 0;

    .item-title {
      margin-bottom: 8px;
      color: var(--ant-color-text);
      font-size: 15px;
      font-weight: 500;
      line-height: 1.4;
    }

    .item-body {
      margin-bottom: 12px;
      color: var(--ant-color-text-secondary);
      font-size: 14px;
      line-height: 1.8;
      white-space: pre-line;
      word-break: break-word;
      background: var(--ant-color-bg-container-hover);
      padding: 12px;
      border-radius: 6px;
      border: 1px solid var(--ant-color-border);
    }

    .item-footer {
      display: flex;
      justify-content: space-between;
      align-items: center;

      .item-time {
        color: var(--ant-color-text-quaternary);
        font-size: 13px;
      }

      .action-buttons {
        display: flex;
        align-items: center;
        
        .action-btn {
          padding: 4px 8px;
          height: 24px;
          font-size: 13px;
          border: none;
          background: transparent;
          
          &:hover {
            background: rgba(0, 0, 0, 0.04);
          }
          
          &.delete-btn {
            color: #ff4d4f;
            
            &:hover {
              color: #ff7875;
              background: rgba(255, 77, 79, 0.1);
            }
          }
        }

        :deep(.ant-divider-vertical) {
          margin: 0 12px;
          height: 14px;
          border-color: rgba(5, 5, 5, 0.06);
        }
      }
    }
  }

  &.unread {
    background-color: var(--ant-color-primary-bg);
    
    .item-title {
      color: var(--ant-color-primary);
    }

    .item-body {
      background: var(--ant-color-bg-container);
    }
  }

  &:hover {
    background-color: var(--ant-color-bg-container-hover);
  }

  :deep(.ant-space) {
    display: none;
  }
}
</style> 