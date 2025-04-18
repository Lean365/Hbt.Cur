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
    <a-badge :count="unreadCount" :dot="false" class="notification-badge">
      <a-button type="text">
        <template #icon>
          <bell-outlined />
        </template>
      </a-button>
    </a-badge>

    <template #overlay>
      <a-menu>
      <div class="notification-panel">
        <!-- 面板头部 -->
        <div class="panel-header">
          <div class="header-item">
            <h4>{{ t('notification.title') }}</h4>
          </div>
          <div class="header-item center">
            <a-button 
              type="primary"
              size="small" 
              @click="readAll"
              :disabled="unreadCount === 0"
              class="read-all-btn"
            >
              {{ t('notification.readAll') }}
            </a-button>
          </div>
          <div class="header-item right">
            <a-button type="text" size="small" @click="showSettings" class="settings-btn">
              <template #icon>
                <setting-outlined />
              </template>
            </a-button>
          </div>
        </div>

        <!-- 标签页 -->
        <a-tabs v-model:activeKey="activeTab">
          <a-tab-pane :key="TabKeys.ALL" :tab="t('notification.all')">
            <div class="notification-list">
              <a-empty v-if="notifications.length === 0" :description="t('notification.empty')" />
              <a-list v-else :dataSource="notifications">
                <template #renderItem="{ item }">
                  <a-list-item :data-unread="item.status === 'unread'">
                    <hbt-notification-item
                      :data="item"
                      @read="markAsRead"
                      @unread="markAsUnread"
                      @delete="deleteNotification"
                    />
                  </a-list-item>
                </template>
              </a-list>
            </div>
          </a-tab-pane>
          <a-tab-pane :key="TabKeys.UNREAD" :tab="t('notification.unread')">
            <div class="notification-list">
              <a-empty v-if="unreadNotifications.length === 0" :description="t('notification.emptyUnread')" />
              <a-list v-else :dataSource="unreadNotifications">
                <template #renderItem="{ item }">
                  <a-list-item :data-unread="true">
                    <hbt-notification-item
                      :data="item"
                      @read="markAsRead"
                      @unread="markAsUnread"
                      @delete="deleteNotification"
                    />
                  </a-list-item>
                </template>
              </a-list>
            </div>
          </a-tab-pane>
        </a-tabs>

        <!-- 加载更多 -->
        <div v-if="hasMore" class="load-more">
          <a-button type="text" block @click="loadMore">
            {{ t('notification.loadMore') }}
          </a-button>
        </div>
        <div v-else-if="notifications.length > 0" class="no-more">
          {{ t('notification.noMore') }}
        </div>
      </div>
    </a-menu>
    </template>
  </a-dropdown>

  <!-- 设置抽屉 -->
  <a-drawer
    v-model:open="showSettingsDrawer"
    :title="t('notification.settings.title')"
    placement="right"
    width="400"
  >
    <div class="settings-content">
      <h3>{{ t('notification.settings.preferences') }}</h3>
      <a-form layout="vertical">
        <a-form-item :label="t('notification.settings.system')">
          <a-switch v-model:checked="settings.system" />
        </a-form-item>
        <a-form-item :label="t('notification.settings.task')">
          <a-switch v-model:checked="settings.task" />
        </a-form-item>
        <a-form-item :label="t('notification.settings.message')">
          <a-switch v-model:checked="settings.message" />
        </a-form-item>
      </a-form>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted, onUnmounted, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { BellOutlined, SettingOutlined, ScheduleOutlined, MessageOutlined } from '@ant-design/icons-vue'
import { notification } from 'ant-design-vue'
import HbtNotificationItem from '../HbtNotificationItem/index.vue'

import { signalRService } from '@/utils/SignalR/service'
import { MessageType, type HbtOnlineMessageDto } from '@/types/signalr/onlineMessage'
import type { NotificationItem as NotificationItemType } from '@/types/settings'
import { getOnlineMessageList, deleteOnlineMessage, markMessageAsRead, markAllMessagesAsRead, markMessageAsUnread, markAllMessagesAsUnread } from '@/api/signalr/onlineMessage'
import { getMailList, markMailAsRead } from '@/api/routine/mail'
import { getNoticeList, markNoticeAsRead } from '@/api/routine/notice'
import { useUserStore } from '@/stores/user'
import type { HbtMailDto } from '@/types/routine/mail'
import type { HbtNoticeDto } from '@/types/routine/notice'

const { t } = useI18n()
const userStore = useUserStore()
const currentUserId = userStore.user?.userId?.toString() || ''

// 标签页类型
type TabKey = 'all' | 'unread'

const TabKeys = {
  ALL: 'all',
  UNREAD: 'unread'
} as const

// 状态管理
const activeTab = ref<TabKey>(TabKeys.ALL)
const loading = ref(false)
const hasMore = ref(true)
const showSettingsDrawer = ref(false)
const notifications = ref<NotificationItemType[]>([])

// 从本地存储加载通知
const loadNotificationsFromStorage = () => {
  const storedNotifications = localStorage.getItem('hbt_notifications')
  if (storedNotifications) {
    notifications.value = JSON.parse(storedNotifications)
  }
}

// 保存通知到本地存储
const saveNotificationsToStorage = () => {
  localStorage.setItem('hbt_notifications', JSON.stringify(notifications.value))
}

// 计算属性
const unreadNotifications = computed(() => {
  return notifications.value.filter((item: NotificationItemType) => item.status === 'unread')
})
const unreadCount = computed(() => unreadNotifications.value.length)

// 通知设置
const settings = ref({
  system: true,
  task: true,
  message: true
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
  let retryCount = 0
  const maxRetries = 3

  const loadWithRetry = async () => {
    try {
      const [onlineMessages, mailMessages, noticeMessages] = await Promise.all([
        getOnlineMessageList({ pageIndex: 1, pageSize: 10 }),
        getMailList({ pageIndex: 1, pageSize: 10 }),
        getNoticeList({ pageIndex: 1, pageSize: 10 })
      ])

      // 使用 Map 来去重，优先使用本地存储的状态
      const messageMap = new Map()
      const storedNotifications = JSON.parse(localStorage.getItem('hbt_notifications') || '[]')
      const storedMap = new Map(storedNotifications.map((n: NotificationItemType) => [n.id, n]))

      // 处理在线消息
      onlineMessages?.rows?.forEach(msg => {
        const item = convertToNotificationItem(msg, 'online')
        // 如果本地存储中有这条消息，使用本地存储的状态
        if (storedMap.has(item.id)) {
          messageMap.set(item.id, storedMap.get(item.id))
        } else {
          messageMap.set(item.id, item)
        }
      })

      // 处理邮件
      mailMessages?.rows?.forEach(msg => {
        const item = convertToNotificationItem(msg, 'mail')
        if (storedMap.has(item.id)) {
          messageMap.set(item.id, storedMap.get(item.id))
        } else {
          messageMap.set(item.id, item)
        }
      })

      // 处理通知
      noticeMessages?.rows?.forEach(msg => {
        const item = convertToNotificationItem(msg, 'notice')
        if (storedMap.has(item.id)) {
          messageMap.set(item.id, storedMap.get(item.id))
        } else {
          messageMap.set(item.id, item)
        }
      })

      // 转换为数组并按时间排序
      const newNotifications = Array.from(messageMap.values())
        .sort((a, b) => new Date(b.createTime).getTime() - new Date(a.createTime).getTime())
      
      notifications.value = newNotifications
      saveNotificationsToStorage()
    } catch (error) {
      console.error(`加载消息失败 (尝试 ${retryCount + 1}/${maxRetries}):`, error)
      if (retryCount < maxRetries) {
        retryCount++
        await new Promise(resolve => setTimeout(resolve, 1000))
        return loadWithRetry()
      }
      throw error
    }
  }

  try {
    await loadWithRetry()
  } catch (error) {
    console.error('加载消息最终失败:', error)
  } finally {
    loading.value = false
  }
}

// 加载更多通知
const loadMore = async () => {
  if (loading.value) return
  loading.value = true
  try {
    const pageIndex = Math.ceil(notifications.value.length / 10) + 1
    const pageSize = 10

    // 并行加载所有类型的消息
    const [onlineMessages, mailMessages, noticeMessages] = await Promise.all([
      getOnlineMessageList({ pageIndex, pageSize }),
      getMailList({ pageIndex, pageSize }),
      getNoticeList({ pageIndex, pageSize })
    ])

    // 使用 Map 来去重
    const messageMap = new Map()

    // 处理在线消息
    if (onlineMessages?.rows?.length > 0) {
      onlineMessages.rows.forEach(msg => {
        const item = convertToNotificationItem(msg, 'online')
        messageMap.set(item.id, item)
      })
    }

    // 处理邮件
    if (mailMessages?.rows?.length > 0) {
      mailMessages.rows.forEach(msg => {
        const item = convertToNotificationItem(msg, 'mail')
        messageMap.set(item.id, item)
      })
    }

    // 处理通知
    if (noticeMessages?.rows?.length > 0) {
      noticeMessages.rows.forEach(msg => {
        const item = convertToNotificationItem(msg, 'notice')
        messageMap.set(item.id, item)
      })
    }

    // 如果没有任何新消息，标记为没有更多
    if (messageMap.size === 0) {
      hasMore.value = false
      return
    }

    // 转换为数组并按时间排序
    const newNotifications = Array.from(messageMap.values())
      .sort((a, b) => new Date(b.createTime).getTime() - new Date(a.createTime).getTime())
    
    // 添加新通知
    notifications.value.push(...newNotifications)
  } catch (error) {
    console.error('加载更多消息失败:', error)
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
    saveNotificationsToStorage() // 保存更新后的状态到本地存储
    
    // 如果当前在未读标签页且没有更多未读消息，自动切换到全部标签页
    if (activeTab.value === TabKeys.UNREAD && unreadCount.value === 0) {
      activeTab.value = TabKeys.ALL
    }
  } catch (error) {
    console.error('标记已读失败:', error)
  }
}

// 标记所有通知为已读
const readAll = async () => {
  if (unreadCount.value === 0) return
  
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
      if (item.status === 'unread') {
        item.status = 'read'
      }
    })
    saveNotificationsToStorage() // 保存更新后的状态到本地存储
    
    // 标记全部已读后，自动切换到全部标签页
    activeTab.value = TabKeys.ALL
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

// 标记消息为未读
const markAsUnread = async (id: string) => {
  try {
    const messageId = id.split('_')[1]
    await markMessageAsUnread(Number(messageId))
    const index = notifications.value.findIndex(item => item.id === id)
    if (index !== -1) {
      notifications.value[index].status = 'unread'
      saveNotificationsToStorage()
    }
  } catch (error) {
    console.error('标记消息为未读失败:', error)
    notification.error({
      message: t('notification.error.unread'),
      description: t('notification.error.unreadDesc')
    })
  }
}

// 标记所有消息为未读
const markAllAsUnread = async () => {
  try {
    await markAllMessagesAsUnread()
    notifications.value.forEach(item => {
      item.status = 'unread'
    })
    saveNotificationsToStorage()
  } catch (error) {
    console.error('标记所有消息为未读失败:', error)
    notification.error({
      message: t('notification.error.unreadAll'),
      description: t('notification.error.unreadAllDesc')
    })
  }
}

// 显示设置抽屉
const showSettings = () => {
  showSettingsDrawer.value = true
}

// 关闭设置抽屉
const closeSettings = () => {
  showSettingsDrawer.value = false
}

// 处理新消息
const handleNewMessage = (message: any) => {
  console.log('[通知中心] 开始处理新消息')
  console.log('[通知中心] 收到的原始消息:', message)
  
  try {
    // 如果是字符串类型的消息
    if (typeof message === 'string') {
      // 检查是否是连接成功消息
      if (message === '连接成功') {
        console.log('[通知中心] 收到连接成功消息，跳过处理')
        return
      }
      
      // 尝试解析为JSON
      try {
        message = JSON.parse(message)
      } catch (e) {
        console.warn('[通知中心] 消息不是JSON格式，使用原始内容:', message)
        // 如果不是JSON格式，直接作为系统消息处理
        handleSystemMessage({
          type: 'system',
          content: message,
          timestamp: new Date().toISOString()
        })
        return
      }
    }
    
    // 处理不同类型的消息
    if (message && typeof message === 'object') {
      // 处理连接成功消息
      if (message.type === 'connection' && message.status === 'success') {
        console.log('[通知中心] 收到连接成功消息，跳过处理')
        return
      }
      
      // 处理通知消息
      if (message.type === 'notification') {
        handleNotification(message)
      }
      // 处理系统消息
      else if (message.type === 'system') {
        handleSystemMessage(message)
      }
      // 处理其他类型的消息
      else {
        console.log('[通知中心] 收到未知类型的消息:', message)
        handleSystemMessage({
          type: 'system',
          content: JSON.stringify(message),
          timestamp: new Date().toISOString()
        })
      }
    }
  } catch (error) {
    console.error('[通知中心] 处理消息时发生错误:', error)
    // 发生错误时，将消息作为系统消息处理
    handleSystemMessage({
      type: 'system',
      content: typeof message === 'string' ? message : JSON.stringify(message),
      timestamp: new Date().toISOString(),
      error: true
    })
  }
}

// 处理通知消息
const handleNotification = (message: any) => {
  console.log('[通知中心] 处理通知消息:', message)
  
  // 添加消息到通知列表
  notifications.value.push({
    id: Date.now(),
    type: 'notification',
    title: message.title || '通知',
    content: message.content || '',
    timestamp: message.timestamp || new Date().toISOString(),
    read: false,
    data: message.data || {}
  })
  
  // 更新未读消息数量
  updateUnreadCount()
  
  // 如果设置了自动显示通知，则显示通知
  if (props.autoShow) {
    showNotification(message)
  }
}

// 处理系统消息
const handleSystemMessage = (message: any) => {
  console.log('[通知中心] 处理系统消息:', message)
  
  // 添加消息到系统消息列表
  systemMessages.value.push({
    id: Date.now(),
    type: 'system',
    content: message.content || '',
    timestamp: message.timestamp || new Date().toISOString(),
    error: message.error || false
  })
  
  // 如果设置了自动显示系统消息，则显示消息
  if (props.autoShowSystem) {
    showSystemMessage(message)
  }
}

// 显示通知
const showNotification = (message: any) => {
  message.success({
    content: message.content || '',
    duration: props.notificationDuration,
    onClick: () => {
      // 点击通知时的处理
      handleNotificationClick(message)
    }
  })
}

// 显示系统消息
const showSystemMessage = (message: any) => {
  if (message.error) {
    message.error({
      content: message.content || '系统消息',
      duration: props.systemMessageDuration
    })
  } else {
    message.info({
      content: message.content || '系统消息',
      duration: props.systemMessageDuration
    })
  }
}

// 更新未读消息数量
const updateUnreadCount = () => {
  unreadCount.value = notifications.value.filter(n => !n.read).length
}

// 处理通知点击
const handleNotificationClick = (message: any) => {
  // 标记消息为已读
  const notification = notifications.value.find(n => n.id === message.id)
  if (notification) {
    notification.read = true
    updateUnreadCount()
  }
  
  // 执行自定义点击处理
  if (props.onNotificationClick) {
    props.onNotificationClick(message)
  }
}

// 处理新通知
const handleNewNotification = (notification: any) => {
  console.log('[通知中心] 收到新通知:', notification)
  
  // 创建新通知对象
  const newNotification: NotificationItemType = {
    id: `notification_${notification.senderId}_${new Date(notification.timestamp).getTime()}`,
    title: notification.title,
    content: notification.content,
    type: notification.type || 'notification',
    status: 'unread',
    createTime: notification.timestamp || new Date().toISOString()
  }

  // 检查是否已存在相同内容的通知
  const isDuplicate = notifications.value.some(item => 
    item.content === notification.content && 
    Math.abs(new Date(item.createTime).getTime() - new Date(notification.timestamp).getTime()) < 1000
  )

  if (!isDuplicate) {
    // 使用 nextTick 确保在 DOM 更新后添加新通知
    nextTick(() => {
      notifications.value = [newNotification, ...notifications.value]
      saveNotificationsToStorage()
      // 显示通知提醒
      showNotification(newNotification)
    })
  }
}

// 处理邮件状态更新
const handleMailStatus = (notification: any) => {
  console.log('[通知中心] 收到邮件状态更新:', notification)
  handleNewMessage(notification)
}

// 处理通知状态更新
const handleNoticeStatus = (notification: any) => {
  console.log('[通知中心] 收到通知状态更新:', notification)
  handleNewMessage(notification)
}

onMounted(() => {
  // 先从本地存储加载通知
  loadNotificationsFromStorage()
  
  // 然后加载服务器上的通知
  loadNotifications()
  
  // 确保 SignalR 连接已建立
  const initSignalR = async () => {
    try {
      // 等待 SignalR 连接建立
      if (!signalRService.getConnectionState()) {
        await signalRService.start()
      }

      // 注册消息处理
      signalRService.on('ReceivePersonalNotice', handleNewNotification)
      signalRService.on('ReceiveMailStatus', handleMailStatus)
      signalRService.on('ReceiveNoticeStatus', handleNoticeStatus)
      signalRService.on('ReceiveMessage', handleNewMessage)

      console.log('[通知中心] SignalR 连接已建立，消息处理已注册')
    } catch (error) {
      console.error('[通知中心] SignalR 初始化失败:', error)
    }
  }

  // 初始化 SignalR
  initSignalR()

  // 添加连接状态监听
  signalRService.on('ConnectionStateChanged', (state) => {
    console.log('[通知中心] SignalR 连接状态变化:', state)
    if (state) {
      // 重新注册事件监听
      signalRService.on('ReceivePersonalNotice', handleNewNotification)
      signalRService.on('ReceiveMailStatus', handleMailStatus)
      signalRService.on('ReceiveNoticeStatus', handleNoticeStatus)
      signalRService.on('ReceiveMessage', handleNewMessage)
    }
  })
})

onUnmounted(() => {
  // 注销所有消息处理
  signalRService.off('ReceivePersonalNotice', handleNewNotification)
  signalRService.off('ReceiveMailStatus', handleMailStatus)
  signalRService.off('ReceiveNoticeStatus', handleNoticeStatus)
  signalRService.off('ReceiveMessage', handleNewMessage)
})
</script>

<style lang="less" scoped>
.notification-panel {
  width: 480px;
  background: var(--ant-color-bg-container);
  border-radius: 8px;
  box-shadow: 0 6px 16px 0 rgba(0, 0, 0, 0.08);
  z-index: 1100;
  
  .panel-header {
    display: flex;
    align-items: center;
    padding: 16px 20px;
    border-bottom: 1px solid var(--ant-color-border);

    .header-item {
      flex: 1;
      display: flex;
      align-items: center;

      h4 {
        margin: 0;
        color: var(--ant-color-text);
        font-weight: 500;
        font-size: 16px;
      }

      &.center {
        justify-content: center;

        .read-all-btn {
          min-width: 72px;
          height: 28px;
          padding: 0 12px;
          border-radius: 4px;
          font-size: 13px;
          display: inline-flex;
          align-items: center;
          justify-content: center;
          line-height: 1;
          
          &:disabled {
            cursor: not-allowed;
            opacity: 0.65;
            background: var(--ant-color-bg-container-disabled);
            border-color: var(--ant-color-border-disabled);
            color: var(--ant-color-text-disabled);
          }

          &:not(:disabled):hover {
            background: var(--ant-color-primary-hover);
          }
        }
      }

      &.right {
        justify-content: flex-end;
      }
    }

    .settings-btn {
      width: 32px;
      height: 32px;
      padding: 0;
      border-radius: 50%;
      display: flex;
      align-items: center;
      justify-content: center;

      &:hover {
        background-color: var(--ant-color-bg-container-hover);
      }
    }
  }

  .notification-list {
    display: flex;
    flex-direction: column;
    min-height: 200px;
  }

  .notification-list {
    flex: 1;
    padding: 12px;
    
    :deep(.ant-list-item) {
      padding: 16px;
      margin: 0 0 12px;
      border-radius: 8px;
      background: var(--ant-color-bg-container-hover);
      border: 1px solid var(--ant-color-border);

      &:last-child {
        margin-bottom: 0;
      }

      &:hover {
        background-color: var(--ant-color-primary-bg-hover);
        border-color: var(--ant-color-primary-border-hover);
      }
    }
  }

  .load-more {
    text-align: center;
    padding: 12px;
    border-top: 1px solid var(--ant-color-border);
    background: var(--ant-color-bg-container);
    margin-top: auto;  // 将加载更多固定在底部

    .ant-btn {
      width: 100%;
      height: 32px;
      
      &:hover {
        background-color: var(--ant-color-primary-bg);
      }
    }
  }

  .no-more {
    text-align: center;
    padding: 12px;
    border-top: 1px solid var(--ant-color-border);
    background: var(--ant-color-bg-container);
    margin-top: auto;
    color: var(--ant-color-text-secondary);
    font-size: 13px;
  }
}

:deep(.ant-tabs) {
  .ant-tabs-nav {
    margin-bottom: 8px;
    padding: 0 20px;
  }

  .ant-tabs-content {
    padding: 0 8px;
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
    padding: 8px 12px;
    border-radius: 4px;
    transition: all 0.3s;

    &:hover {
      background-color: var(--ant-color-bg-container-hover);
    }

    span {
      color: var(--ant-color-text);
    }
  }
}

// 修改下拉菜单的样式
:deep(.ant-dropdown) {
  z-index: 1100;
}

:deep(.ant-dropdown-trigger) {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-btn) {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 32px;
  width: 32px;
  padding: 0;
  border-radius: 50%;
  
  &:hover {
    background-color: var(--ant-color-bg-container-hover);
  }

}

:deep(.anticon) {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
  line-height: 1;
}

// 全局通知样式
.hbt-notification {
  cursor: pointer;
  
  .ant-notification-notice-icon {
    font-size: 24px;
    margin-top: 4px;
  }
  
  .ant-notification-notice-message {
    margin-bottom: 8px;
    color: var(--ant-color-text);
    font-weight: 600;
    font-size: 16px;
  }
  
  .ant-notification-notice-description {
    color: var(--ant-color-text-secondary);
    font-size: 14px;
  }

  .notification-content {
    .message-content {
      margin-bottom: 12px;
      color: var(--ant-color-text-secondary);
    }
  }

  .notification-actions {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-top: 8px;

    .notification-prompt {
      color: var(--ant-color-text-secondary);
      font-size: 13px;
    }

    .read-now-btn {
      margin-left: 12px;
      height: 24px;
      padding: 0 12px;
      font-size: 13px;
      border-radius: 4px;
      
      &:hover {
        opacity: 0.85;
      }
    }
  }
}

// 警告样式通知
.warning-notification {
  .ant-notification-notice {
    background: var(--ant-color-warning-bg);
    border: 1px solid var(--ant-color-warning-border);
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
  }

  .ant-notification-notice-message {
    color: var(--ant-color-warning);
  }

  .ant-notification-notice-description {
    color: var(--ant-color-text);
  }

  .notification-content {
    .message-content {
      color: var(--ant-color-text);
    }
  }

  .notification-actions {
    .notification-prompt {
      color: var(--ant-color-text);
    }

    .read-now-btn {
      background: var(--ant-color-warning);
      border-color: var(--ant-color-warning);
      
      &:hover {
        background: var(--ant-color-warning-hover);
        border-color: var(--ant-color-warning-hover);
      }
    }
  }
}

.notification-badge {
  :deep(.ant-badge-count) {
    box-shadow: 0 0 0 1px #fff;
    font-size: 12px;
    height: 20px;
    min-width: 20px;
    line-height: 20px;
    padding: 0 6px;
  }
}
</style> 
