<template>
  <a-dropdown :trigger="['click']" placement="bottomRight" class="notification-dropdown">
    <a-badge :count="unreadCount" :dot="true" class="notification-badge">
      <a-button type="text">
        <template #icon>
          <bell-outlined />
        </template>
      </a-button>
    </a-badge>
    <template #overlay>
      <div class="notification-panel">
        <div class="notification-header">
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
        <a-tabs v-model:activeKey="activeTab">
          <a-tab-pane key="all" :tab="t('header.notification.all')">
            <a-list
              class="notification-list"
              :data-source="notifications"
              :loading="loading"
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
              <template #empty>
                <div class="empty">
                  {{ t('header.notification.empty') }}
                </div>
              </template>
            </a-list>
          </a-tab-pane>
          <a-tab-pane key="unread" :tab="t('header.notification.unread')">
            <!-- 未读消息列表 -->
          </a-tab-pane>
        </a-tabs>
      </div>
    </template>
  </a-dropdown>

  <!-- 通知设置抽屉 -->
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
          v-model:checked="settings.system"
          @change="updateSettings('system')"
        />
      </div>
      <div class="setting-item">
        <span>{{ t('header.notification.task') }}</span>
        <a-switch
          v-model:checked="settings.task"
          @change="updateSettings('task')"
        />
      </div>
      <div class="setting-item">
        <span>{{ t('header.notification.message') }}</span>
        <a-switch
          v-model:checked="settings.message"
          @change="updateSettings('message')"
        />
      </div>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { BellOutlined, SettingOutlined } from '@ant-design/icons-vue'
import NotificationItem from './NotificationItem.vue'

const { t } = useI18n()
const activeTab = ref('all')
const unreadCount = ref(0)
const loading = ref(false)
const hasMore = ref(true)
const settingsVisible = ref(false)
const notifications = ref([])

// 通知设置
const settings = ref({
  system: true,
  task: true,
  message: true
})

// 加载通知列表
const loadNotifications = async () => {
  loading.value = true
  try {
    // TODO: 从API获取通知列表
    // const response = await fetchNotifications()
    // notifications.value = response.data
    // unreadCount.value = response.unreadCount
  } catch (error) {
    console.error('Failed to load notifications:', error)
  } finally {
    loading.value = false
  }
}

// 加载更多
const loadMore = () => {
  // TODO: 实现加载更多逻辑
}

// 标记为已读
const markAsRead = (id: string) => {
  // TODO: 实现标记已读逻辑
}

// 全部标记为已读
const readAll = () => {
  // TODO: 实现全部标记已读逻辑
}

// 删除通知
const deleteNotification = (id: string) => {
  // TODO: 实现删除通知逻辑
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
const updateSettings = (key: 'system' | 'task' | 'message') => {
  settings.value[key] = !settings.value[key]
  localStorage.setItem('notification-settings', JSON.stringify(settings.value))
}

onMounted(() => {
  // 加载通知设置
  const savedSettings = localStorage.getItem('notification-settings')
  if (savedSettings) {
    settings.value = JSON.parse(savedSettings)
  }

  // 加载通知列表
  loadNotifications()
})
</script>

<style lang="less" scoped>
.notification-dropdown {
  display: flex;
  align-items: center;
  justify-content: center;
}

.notification-badge {
  display: flex;
  align-items: center;
  justify-content: center;
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
}

:deep(.anticon) {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  line-height: 1;
}

.notification-panel {
  width: 360px;
  background: var(--ant-color-bg-container);
  border-radius: 8px;
  box-shadow: var(--ant-box-shadow);
  
  .notification-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 12px 16px;
    border-bottom: 1px solid var(--ant-color-border);

    h4 {
      margin: 0;
      color: var(--ant-color-text);
      font-size: 14px;
    }
  }

  .notification-list {
    max-height: 400px;
    overflow-y: auto;
  }

  .load-more {
    text-align: center;
    padding: 8px 0;
    color: var(--ant-color-text-secondary);
  }
}

.setting-block {
  h4 {
    margin: 0 0 16px;
    color: var(--ant-color-text);
    font-size: 14px;
  }
}

.setting-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;

  span {
    color: var(--ant-color-text);
    font-size: 14px;
  }
}
</style> 