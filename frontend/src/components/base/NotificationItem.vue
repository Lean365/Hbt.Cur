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
      <div class="item-title">{{ data.title }}</div>
      <div class="item-body">{{ data.content }}</div>
      <div class="item-footer">
        <span class="item-time">{{ formatTime(data.createTime) }}</span>
        <a-space>
          <a-button
            v-if="data.status === 'unread'"
            type="link"
            size="small"
            @click="$emit('read', data.id)"
          >
            {{ t('notification.markAsRead') }}
          </a-button>
          <a-button
            type="link"
            size="small"
            @click="$emit('delete', data.id)"
          >
            {{ t('notification.delete') }}
          </a-button>
        </a-space>
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

defineProps<Props>()

// Emits
defineEmits<{
  (e: 'read', id: string): void
  (e: 'delete', id: string): void
}>()

// 格式化时间
const formatTime = (time: string) => {
  return dayjs(time).fromNow()
}
</script>

<style lang="less" scoped>
.notification-item {
  display: flex;
  padding: 12px 16px;
  cursor: pointer;
  transition: all 0.3s;

  &:hover {
    background: var(--ant-color-bg-container-hover);
  }

  &.unread {
    background: var(--ant-color-primary-bg);

    &:hover {
      background: var(--ant-color-primary-bg-hover);
    }

    .item-title {
      color: var(--ant-color-primary);
      font-weight: 500;
    }
  }

  .item-icon {
    margin-right: 12px;
    font-size: 24px;
    color: var(--ant-color-primary);
  }

  .item-content {
    flex: 1;
    overflow: hidden;

    .item-title {
      margin-bottom: 4px;
      color: var(--ant-color-text);
      font-size: 14px;
      line-height: 22px;
    }

    .item-body {
      margin-bottom: 4px;
      color: var(--ant-color-text-secondary);
      font-size: 12px;
      line-height: 20px;
    }

    .item-footer {
      display: flex;
      justify-content: space-between;
      align-items: center;

      .item-time {
        color: var(--ant-color-text-secondary);
        font-size: 12px;
      }
    }
  }
}
</style> 