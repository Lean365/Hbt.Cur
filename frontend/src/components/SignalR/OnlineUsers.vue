<template>
  <div class="online-users">
    <a-popover
      v-model:visible="visible"
      placement="bottomRight"
      trigger="click"
      :overlayStyle="{ width: '250px' }"
    >
      <template #content>
        <div class="users-list">
          <div class="users-header">
            <span>在线用户 ({{ onlineUsers.length }})</span>
          </div>
          <a-empty v-if="onlineUsers.length === 0" description="暂无在线用户" />
          <div v-else class="users-items">
            <div
              v-for="user in onlineUsers"
              :key="user.id"
              class="user-item"
            >
              <a-avatar :src="user.avatar" :size="32">
                {{ user.name.charAt(0).toUpperCase() }}
              </a-avatar>
              <div class="user-info">
                <div class="user-name">{{ user.name }}</div>
                <div class="user-status">
                  <span class="status-dot"></span>
                  {{ user.status }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
      <a-button type="text">
        <a-icon type="team" />
      </a-button>
    </a-popover>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { signalRService } from '@/utils/SignalR/service'

interface OnlineUser {
  id: string
  name: string
  avatar?: string
  status: '在线' | '离开' | '忙碌'
}

const visible = ref(false)
const onlineUsers = ref<OnlineUser[]>([])

// 处理用户上线
const handleUserOnline = (user: OnlineUser) => {
  if (!onlineUsers.value.find(u => u.id === user.id)) {
    onlineUsers.value.push(user)
  }
}

// 处理用户下线
const handleUserOffline = (userId: string) => {
  onlineUsers.value = onlineUsers.value.filter(u => u.id !== userId)
}

// 处理用户状态更新
const handleUserStatusUpdate = (userId: string, status: OnlineUser['status']) => {
  const user = onlineUsers.value.find(u => u.id === userId)
  if (user) {
    user.status = status
  }
}

onMounted(() => {
  signalRService.on('UserOnline', handleUserOnline)
  signalRService.on('UserOffline', handleUserOffline)
  signalRService.on('UserStatusUpdate', handleUserStatusUpdate)
})

onUnmounted(() => {
  signalRService.off('UserOnline', handleUserOnline)
  signalRService.off('UserOffline', handleUserOffline)
  signalRService.off('UserStatusUpdate', handleUserStatusUpdate)
})
</script>

<style lang="less" scoped>
.online-users {
  .users-list {
    .users-header {
      padding: 8px 16px;
      border-bottom: 1px solid #f0f0f0;
      margin-bottom: 8px;
    }

    .users-items {
      max-height: 400px;
      overflow-y: auto;

      .user-item {
        display: flex;
        align-items: center;
        padding: 8px 16px;
        transition: background-color 0.3s;

        &:hover {
          background-color: #f5f5f5;
        }

        .user-info {
          margin-left: 12px;
          flex: 1;

          .user-name {
            font-weight: 500;
            margin-bottom: 4px;
          }

          .user-status {
            display: flex;
            align-items: center;
            color: #666;
            font-size: 12px;

            .status-dot {
              width: 6px;
              height: 6px;
              border-radius: 50%;
              background-color: #52c41a;
              margin-right: 6px;
            }
          }
        }
      }
    }
  }
}
</style> 