//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: NotificationCenter.vue
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 通知中心组件
//===================================================================

<template>
  <a-dropdown :trigger="['click']" placement="bottomRight">
    <a-badge :count="unreadCount" :dot="true">
      <a-button type="text">
        <template #icon>
          <bell-outlined />
        </template>
      </a-button>
    </a-badge>

    <template #overlay>
      <div class="notification-panel">
        <!-- 面板头部 -->
        <div class="panel-header">
          <h4>{{ t('header.notification.title') }}</h4>
          <a-space>
            <a-button type="text" size="small" @click="readAll">
              {{ t('header.notification.readAll') }}
            </a-button>
            <a-button type="text" size="small" @click="showSettings">
              <template #icon>
                <setting-outlined />
              </template>
            </a-button>
          </a-space>
        </div>

        <!-- 通知列表 -->
        <a-tabs v-model:activeKey="activeTab">
          <a-tab-pane key="all" :tab="t('header.notification.all')">
            <a-list
              class="notification-list"
              :data-source="notifications"
              :loading="loading"
              :locale="{ emptyText: t('header.notification.empty') }"
            >
              <!-- 通知项 -->
              <template #renderItem="{ item }">
                <a-list-item>
                  <notification-item
                    :data="item"
                    @read="markAsRead"
                    @delete="deleteNotification"
                  />
                </a-list-item>
              </template>

              <!-- 加载更多 -->
              <template #loadMore>
                <div class="load-more">
                  <a-button
                    v-if="hasMore"
                    type="text"
                    size="small"
                    @click="loadMore"
                  >
                    {{ t('header.notification.loadMore') }}
                  </a-button>
                  <span v-else class="no-more">
                    {{ t('header.notification.noMore') }}
                  </span>
                </div>
              </template>
            </a-list>
          </a-tab-pane>

          <a-tab-pane key="unread" :tab="t('header.notification.unread')">
            <a-list
              class="notification-list"
              :data-source="unreadNotifications"
              :loading="loading"
              :locale="{ emptyText: t('header.notification.emptyUnread') }"
            >
              <template #renderItem="{ item }">
                <a-list-item>
                  <notification-item
                    :data="item"
                    @read="markAsRead"
                    @delete="deleteNotification"
                  />
                </a-list-item>
              </template>
            </a-list>
          </a-tab-pane>
        </a-tabs>
      </div>
    </template>
  </a-dropdown>

  <!-- 设置抽屉 -->
  <a-drawer
    :title="t('header.notification.settings')"
    :visible="settingsVisible"
    @close="closeSettings"
    width="300"
    placement="right"
  >
    <div class="setting-block">
      <h4>{{ t('header.notification.preferences') }}</h4>
      <div class="setting-item">
        <span>{{ t('header.notification.system') }}</span>
        <a-switch
          v-model:checked="settings[MessageType.System]"
          @change="updateSettings(MessageType.System)"
        />
      </div>
      <div class="setting-item">
        <span>{{ t('header.notification.task') }}</span>
        <a-switch
          v-model:checked="settings[MessageType.Task]"
          @change="updateSettings(MessageType.Task)"
        />
      </div>
      <div class="setting-item">
        <span>{{ t('header.notification.message') }}</span>
        <a-switch
          v-model:checked="settings[MessageType.Personal]"
          @change="updateSettings(MessageType.Personal)"
        />
      </div>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { BellOutlined, SettingOutlined } from '@ant-design/icons-vue'
import NotificationItem from './NotificationItem.vue'
import { signalRService } from '@/utils/SignalR/service'
import { MessageType, type HbtOnlineMessageDto } from '@/types/realtime/onlineMessage'
import type { NotificationItem as NotificationItemType } from '@/types/settings'
import { getOnlineMessageList, deleteOnlineMessage, markMessageAsRead, markAllMessagesAsRead } from '@/api/realtime/onlineMessage'
import { getMailList, markMailAsRead } from '@/api/routine/mail'
import { getNoticeList, markNoticeAsRead } from '@/api/routine/notice'
import { useUserStore } from '@/stores/user'
import type { HbtMailDto } from '@/types/routine/mail'
import type { HbtNoticeDto } from '@/types/routine/notice'

const { t } = useI18n()
const userStore = useUserStore()
const currentUserId = userStore.user?.userId?.toString() || ''

// 状态管理
const activeTab = ref('all')
const loading = ref(false)
const hasMore = ref(true)
const settingsVisible = ref(false)
const notifications = ref<NotificationItemType[]>([])

// 计算属性
const unreadNotifications = computed(() => {
  return notifications.value.filter((item: NotificationItemType) => item.status === 'unread')
})
const unreadCount = computed(() => unreadNotifications.value.length)

// 通知设置
const settings = ref({
  [MessageType.System]: true,
  [MessageType.Broadcast]: true,
  [MessageType.Task]: true,
  [MessageType.Email]: true,
  [MessageType.Notification]: true,
  [MessageType.Personal]: true
})

/** 通知类型 */
type NotificationType = 'online' | 'mail' | 'notice'

// 将消息转换为统一的通知项格式
const convertToNotificationItem = (message: HbtOnlineMessageDto | HbtMailDto | HbtNoticeDto, type: NotificationType): NotificationItemType => {
  switch (type) {
    case 'online':
      const onlineMessage = message as HbtOnlineMessageDto
      return {
        id: `online_${onlineMessage.messageId}`,
        title: onlineMessage.messageType,
        content: onlineMessage.messageContent,
        createTime: onlineMessage.sendTime.toISOString(),
        status: onlineMessage.messageStatus === 0 ? 'unread' : 'read',
        type: 'system'
      }
    case 'mail':
      const mailMessage = message as HbtMailDto
      return {
        id: `mail_${mailMessage.mailId}`,
        title: mailMessage.mailSubject,
        content: mailMessage.mailBody,
        createTime: mailMessage.createTime,
        status: mailMessage.mailStatus === 0 ? 'unread' : 'read',
        type: 'message'
      }
    case 'notice':
      const noticeMessage = message as HbtNoticeDto
      return {
        id: `notice_${noticeMessage.noticeId}`,
        title: noticeMessage.noticeTitle,
        content: noticeMessage.noticeContent,
        createTime: noticeMessage.createTime,
        status: noticeMessage.noticeStatus === 0 ? 'unread' : 'read',
        type: 'task'
      }
    default:
      throw new Error(`未知的消息类型: ${type}`)
  }
}

// 加载通知列表
const loadNotifications = async () => {
  if (loading.value) return
  loading.value = true
  try {
    const [onlineMessages, mailMessages, noticeMessages] = await Promise.all([
      getOnlineMessageList({ pageIndex: 1, pageSize: 10 }),
      getMailList({ pageIndex: 1, pageSize: 10 }),
      getNoticeList({ pageIndex: 1, pageSize: 10 })
    ])

    // 合并消息并转换格式
    notifications.value = [
      ...(onlineMessages?.rows || []).map(msg => convertToNotificationItem(msg, 'online')),
      ...(mailMessages?.rows || []).map(msg => convertToNotificationItem(msg, 'mail')),
      ...(noticeMessages?.rows || []).map(msg => convertToNotificationItem(msg, 'notice'))
    ].sort((a, b) => new Date(b.createTime).getTime() - new Date(a.createTime).getTime()) // 按时间排序
  } catch (error) {
    console.error('加载消息失败:', error)
  } finally {
    loading.value = false
  }
}

// 加载更多通知
const loadMore = async () => {
  if (loading.value) return
  loading.value = true
  try {
    const result = await getOnlineMessageList({
      pageIndex: Math.ceil(notifications.value.length / 10) + 1,
      pageSize: 10
    })
    if (result.rows.length === 0) {
      hasMore.value = false
    } else {
      notifications.value.push(...result.rows.map(msg => convertToNotificationItem(msg, 'online')))
    }
  } catch (error) {
    console.error('Failed to load more notifications:', error)
  } finally {
    loading.value = false
  }
}

// 标记通知为已读
const markAsRead = async (id: string) => {
  const item = notifications.value.find((item: NotificationItemType) => item.id === id)
  if (!item) return

  const [type, realId] = item.id.split('_')
  try {
    switch (type) {
      case 'mail':
        await markMailAsRead(Number(realId))
        break
      case 'notice':
        await markNoticeAsRead(Number(realId))
        break
      case 'online':
        await markMessageAsRead(Number(realId))
        break
    }
    item.status = 'read'
  } catch (error) {
    console.error('标记已读失败:', error)
  }
}

// 标记所有通知为已读
const readAll = async () => {
  try {
    const promises = notifications.value
      .filter(item => item.status === 'unread')
      .map(item => {
        const [type, realId] = item.id.split('_')
        switch (type) {
          case 'mail':
            return markMailAsRead(Number(realId))
          case 'notice':
            return markNoticeAsRead(Number(realId))
          case 'online':
            return markMessageAsRead(Number(realId))
        }
      })

    await Promise.all(promises)
    notifications.value.forEach(item => {
      item.status = 'read'
    })
  } catch (error) {
    console.error('标记全部已读失败:', error)
  }
}

// 删除通知
const deleteNotification = async (id: string) => {
  const [type, realId] = id.split('_')
  const index = notifications.value.findIndex(item => item.id === id)
  if (index === -1) return

  try {
    switch (type) {
      case 'online':
        await deleteOnlineMessage(Number(realId))
        break
      // 暂不支持删除邮件和通知
      default:
        return
    }
    notifications.value.splice(index, 1)
  } catch (error) {
    console.error('删除通知失败:', error)
  }
}

// 显示设置抽屉
const showSettings = () => {
  settingsVisible.value = true
}

// 关闭设置抽屉
const closeSettings = () => {
  settingsVisible.value = false
}

// 更新通知设置
const updateSettings = (key: keyof typeof settings.value) => {
  settings.value[key] = !settings.value[key]
  localStorage.setItem('notification-settings', JSON.stringify(settings.value))
}

// 注册SignalR消息处理
const handleNewNotification = (message: any) => {
  loadNotifications()
}

const handleMailStatus = (notification: any) => {
  loadNotifications()
}

const handleNoticeStatus = (notification: any) => {
  loadNotifications()
}

onMounted(() => {
  // 加载通知设置
  const savedSettings = localStorage.getItem('notification-settings')
  if (savedSettings) {
    settings.value = JSON.parse(savedSettings)
  }

  // 加载通知列表
  loadNotifications()
  
  // 注册SignalR消息处理
  signalRService.on('ReceiveNotification', handleNewNotification)
  signalRService.on('ReceiveMailStatus', handleMailStatus)
  signalRService.on('ReceiveNoticeStatus', handleNoticeStatus)
})

onUnmounted(() => {
  // 注销SignalR消息处理
  signalRService.off('ReceiveNotification', handleNewNotification)
  signalRService.off('ReceiveMailStatus', handleMailStatus)
  signalRService.off('ReceiveNoticeStatus', handleNoticeStatus)
})
</script>

<style lang="less" scoped>
.notification-panel {
  width: 360px;
  background: var(--ant-color-bg-container);
  border-radius: 8px;
  box-shadow: var(--ant-box-shadow);
  
  .panel-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 12px 16px;
    border-bottom: 1px solid var(--ant-color-border);

    h4 {
      margin: 0;
      color: var(--ant-color-text);
      font-weight: 500;
    }
  }

  .notification-list {
    max-height: 400px;
    overflow-y: auto;
    padding: 8px 0;

    :deep(.ant-list-item) {
      padding: 12px 16px;
      transition: background-color 0.3s;

      &:hover {
        background-color: var(--ant-color-bg-container-hover);
      }
    }
  }

  .load-more {
    text-align: center;
    padding: 8px 0;
    color: var(--ant-color-text-secondary);
  }
}

.setting-block {
  h4 {
    margin-bottom: 16px;
    color: var(--ant-color-text);
    font-weight: 500;
  }

  .setting-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 16px;

    span {
      color: var(--ant-color-text);
    }
  }
}
</style> 