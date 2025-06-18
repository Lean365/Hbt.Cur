<template>
  <div class="online-users">
    <a-popover
      v-model:open="isOpen"
      placement="bottomRight"
      trigger="click"
      :overlayStyle="{ width: '280px' }"
    >
      <template #content>
        <div class="users-list">
          <div class="users-header">
            <span>在线用户 ({{ onlineUsers.length }})</span>
            <a-button 
              type="link" 
              size="small" 
              :loading="loading"
              @click="refreshOnlineUsers"
            >
              刷新
            </a-button>
          </div>
          <a-spin :spinning="loading">
            <a-empty v-if="!loading && onlineUsers.length === 0" description="暂无在线用户" />
            <div v-else class="users-items">
              <div
                v-for="user in onlineUsers"
                :key="user.userId"
                class="user-item"
              >
                <a-avatar :src="user.avatar" :size="32">
                  <template #icon>
                    <team-outlined />
                  </template>
                </a-avatar>
                <div class="user-info">
                  <div class="user-name">{{ user.nickName || user.userName }}</div>
                  <div class="user-status">
                    <span class="status-dot" :style="getUserStatusStyle(user.status)"></span>
                    {{ getUserStatusText(user.status) }}
                    <span class="last-active" v-if="user.lastActiveTime">
                      ({{ formatLastActiveTime(user.lastActiveTime) }})
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </a-spin>
        </div>
      </template>
      <a-button type="text" class="online-users-btn">
        <team-outlined />
        <span v-if="onlineUsers.length > 0" class="online-count">{{ onlineUsers.length }}</span>
      </a-button>
    </a-popover>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted, watch } from 'vue'
import { signalRService } from '@/utils/SignalR/service'
import { TeamOutlined } from '@ant-design/icons-vue'
import { useUserStore } from '@/stores/user'
import { message } from 'ant-design-vue'
import { formatDistanceToNow } from 'date-fns'
import { zhCN } from 'date-fns/locale'
import { getOnlineUserList } from '@/api/signalr/onlineUser'
import type { HbtOnlineUserQueryParams, HbtOnlineUserDto } from '@/types/signalr/onlineUser'

interface OnlineUser {
  userId: number
  userName: string
  nickName: string
  avatar?: string
  status: 'online' | 'offline'
  lastActiveTime: string
}

interface Props {
  open: boolean
}

const props = defineProps<Props>()
const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
}>()

const isOpen = ref(props.open)
const userStore = useUserStore()
const onlineUsers = ref<OnlineUser[]>([])
const loading = ref(false)

// 监听open属性变化
watch(() => props.open, (newVal) => {
  isOpen.value = newVal
})

// 监听isOpen变化
watch(isOpen, (newVal) => {
  emit('update:open', newVal)
  if (newVal) {
    refreshOnlineUsers()
  }
})

// 将 HbtOnlineUserDto 转换为 OnlineUser
const convertToOnlineUser = (dto: HbtOnlineUserDto): OnlineUser => {
  return {
    userId: dto.userId,
    userName: '', // 暂时使用空字符串
    nickName: '', // 暂时使用空字符串
    avatar: '', // 暂时使用空字符串
    status: 'online', // 所有返回的用户都是在线状态
    lastActiveTime: dto.lastActivity
  }
}

// 刷新在线用户列表
const refreshOnlineUsers = async () => {
  try {
    loading.value = true
    const { data: response } = await getOnlineUserList({
      pageIndex: 1,
      pageSize: 100
    } as HbtOnlineUserQueryParams)
    
    console.log('[HbtOnlineUsers] 获取在线用户列表响应:', response)
    
    if (response.code === 200) {
      console.log('[HbtOnlineUsers] 在线用户数据:', response.data)
      const users = (response.data.rows || []).map(convertToOnlineUser)
      console.log('[HbtOnlineUsers] 转换后的用户数据:', users)
      onlineUsers.value = users
    } else {
      throw new Error(response.msg || '获取在线用户列表失败')
    }
  } catch (error) {
    console.error('[HbtOnlineUsers] 获取在线用户列表失败:', error)
    message.error('获取在线用户列表失败')
  } finally {
    loading.value = false
  }
}

// 处理用户上线
const handleUserOnline = (user: OnlineUser) => {
  console.log('[HbtOnlineUsers] 用户上线:', user)
  const existingUserIndex = onlineUsers.value.findIndex(u => u.userId === user.userId)
  if (existingUserIndex === -1) {
    onlineUsers.value.push(user)
  } else {
    onlineUsers.value[existingUserIndex] = { ...user }
  }
}

// 处理用户下线
const handleUserOffline = (userId: number) => {
  console.log('[HbtOnlineUsers] 用户下线:', userId)
  onlineUsers.value = onlineUsers.value.filter(u => u.userId !== userId)
}

// 处理用户状态更新
const handleUserStatusUpdate = (userId: number, status: OnlineUser['status']) => {
  console.log('[HbtOnlineUsers] 用户状态更新:', userId, status)
  const user = onlineUsers.value.find(u => u.userId === userId)
  if (user) {
    user.status = status
    user.lastActiveTime = new Date().toISOString()
  }
}

// 处理用户活动更新
const handleUserActivityUpdate = (userId: number, lastActiveTime: string) => {
  console.log('[HbtOnlineUsers] 用户活动更新:', userId, lastActiveTime)
  const user = onlineUsers.value.find(u => u.userId === userId)
  if (user) {
    user.lastActiveTime = lastActiveTime
  }
}

// 格式化最后活动时间
const formatLastActiveTime = (time: string) => {
  return formatDistanceToNow(new Date(time), { addSuffix: true, locale: zhCN })
}

// 获取用户状态文本
const getUserStatusText = (status: OnlineUser['status']) => {
  switch (status) {
    case 'online':
      return '在线'
    case 'offline':
      return '离线'
    default:
      return '未知'
  }
}

// 获取用户状态样式
const getUserStatusStyle = (status: OnlineUser['status']) => {
  switch (status) {
    case 'online':
      return { backgroundColor: '#52c41a' }
    case 'offline':
      return { backgroundColor: '#d9d9d9' }
    default:
      return { backgroundColor: '#d9d9d9' }
  }
}

onMounted(() => {
  // 注册SignalR事件处理
  signalRService.on('UserOnline', handleUserOnline)
  signalRService.on('UserOffline', handleUserOffline)
  signalRService.on('UserStatusUpdate', handleUserStatusUpdate)
  signalRService.on('UserActivityUpdate', handleUserActivityUpdate)
  signalRService.on('OnlineUsersList', (users: OnlineUser[]) => {
    console.log('[HbtOnlineUsers] 获取在线用户列表:', users)
    onlineUsers.value = users
  })

  // 如果组件挂载时弹窗是打开的，则获取在线用户列表
  if (isOpen.value) {
    refreshOnlineUsers()
  }
})

onUnmounted(() => {
  // 移除SignalR事件处理
  signalRService.off('UserOnline', handleUserOnline)
  signalRService.off('UserOffline', handleUserOffline)
  signalRService.off('UserStatusUpdate', handleUserStatusUpdate)
  signalRService.off('UserActivityUpdate', handleUserActivityUpdate)
})
</script>

<style lang="less" scoped>
.online-users {
  :deep(.online-users-btn) {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 32px;
    width: 32px;
    padding: 0;
    cursor: pointer;
    position: relative;

    .anticon {
      display: flex;
      align-items: center;
      justify-content: center;
      font-size: 14px;
      line-height: 1;
    }

    .online-count {
      position: absolute;
      top: -5px;
      right: -5px;
      background-color: #ff4d4f;
      color: white;
      border-radius: 10px;
      padding: 0 6px;
      font-size: 12px;
      line-height: 16px;
      min-width: 16px;
      text-align: center;
    }
  }

  .users-list {
    .users-header {
      padding: 8px 16px;
      border-bottom: 1px solid #f0f0f0;
      margin-bottom: 8px;
      font-weight: 500;
      display: flex;
      justify-content: space-between;
      align-items: center;
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
              margin-right: 6px;
            }

            .last-active {
              margin-left: 4px;
              color: #999;
            }
          }
        }
      }
    }
  }
}
</style> 