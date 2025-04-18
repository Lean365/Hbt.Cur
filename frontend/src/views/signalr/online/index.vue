<template>
  <div class="online-user-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-refresh="true"
      :show-delete="true"
      :delete-permission="['realtime:online:list']"
      :disabled-delete="selectedRowKeys.length === 0"
      @refresh="fetchData"
      @delete="handleBatchDelete"
      @toggle-search="toggleSearch"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="false"
      :row-key="(record: HbtOnlineUserDto) => record.connectionId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <hbt-operation
            :show-force-offline="true"
            :show-send-message="true"
            @force-offline="handleForceOffline(record)"
            @send-message="handleSendMessage(record)"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="total"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 发送消息表单 -->
    <SendForm
      v-model:visible="sendFormVisible"
      :default-user-id="selectedUserId"
      @success="handleSendSuccess"
    />
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtOnlineUserDto, HbtOnlineUserQueryParams } from '@/types/signalr/onlineUser'
import { getOnlineUserList, forceOfflineUser } from '@/api/signalr/onlineUser'
import { signalRService } from '@/utils/SignalR/service'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user'
import SendForm from './components/SendForm.vue'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userId',
    label: '用户ID',
    type: 'input',
    placeholder: '请输入用户ID'
  },
  {
    name: 'clientIp',
    label: '客户端IP',
    type: 'input',
    placeholder: '请输入客户端IP'
  }
]

// 表格列定义
const columns = [
  {
    title: '用户ID',
    dataIndex: 'userId',
    key: 'userId',
    width: 120
  },
  {
    title: '连接ID',
    dataIndex: 'connectionId',
    key: 'connectionId',
    width: 200,
    ellipsis: true
  },
  {
    title: '客户端IP',
    dataIndex: 'clientIp',
    key: 'clientIp',
    width: 150
  },
  {
    title: '用户代理',
    dataIndex: 'userAgent',
    key: 'userAgent',
    width: 200,
    ellipsis: true
  },
  {
    title: '最后活动时间',
    dataIndex: 'lastActivity',
    key: 'lastActivity',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 120,
    fixed: 'right'
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<HbtOnlineUserDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtOnlineUserQueryParams>({
  pageIndex: 1,
  pageSize: 10,
  userId: undefined,
  clientIp: undefined
})
const selectedRowKeys = ref<string[]>([])
const showSearch = ref(true)
const sendFormVisible = ref(false)
const selectedUserId = ref<number>()
const receivedMessages = reactive<any[]>([])

// 生命周期钩子
onMounted(() => {
  console.log('[在线用户] 组件挂载，SignalR 连接状态:', signalRService.getConnectionState())
  fetchData()
  // 监听用户上下线事件
  signalRService.on('UserOnline', handleUserOnline)
  signalRService.on('UserOffline', handleUserOffline)
  // 监听强制下线事件
  signalRService.on('ForceOffline', handleForceOfflineEvent)
  // 监听SignalR消息
  signalRService.on('ReceiveMessage', handleReceivedMessage)
})

onUnmounted(() => {
  console.log('[在线用户] 组件卸载，SignalR 连接状态:', signalRService.getConnectionState())
  // 移除事件监听
  signalRService.off('UserOnline', handleUserOnline)
  signalRService.off('UserOffline', handleUserOffline)
  signalRService.off('ForceOffline', handleForceOfflineEvent)
  signalRService.off('ReceiveMessage', handleReceivedMessage)
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getOnlineUserList(queryParams)
    if (res.code === 200) {
      tableData.value = res.data.rows || []
      total.value = res.data.totalNum || 0
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.userId = undefined
  queryParams.clientIp = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = () => {
  // 移除分页相关逻辑，由 HbtPagination 组件处理
}

/** 页码变化事件 */
const handlePageChange = (page: number) => {
  queryParams.pageIndex = page
  fetchData()
}

/** 每页条数变化事件 */
const handleSizeChange = (size: number) => {
  queryParams.pageSize = size
  queryParams.pageIndex = 1
  fetchData()
}

/** 用户上线事件 */
const handleUserOnline = (userId: number) => {
  fetchData()
}

/** 用户下线事件 */
const handleUserOffline = (userId: number) => {
  fetchData()
}

/** 强制下线事件处理 */
const handleForceOfflineEvent = async (msg: string) => {
  message.warning(msg || '您已被强制下线')
  await router.replace('/login')
  window.location.reload()
}

/** 强制下线 */
const handleForceOffline = async (record: HbtOnlineUserDto) => {
  try {
    const currentUser = userStore.user
    console.log('[在线用户] 当前用户:', currentUser)
    console.log('[在线用户] 被下线用户:', record)
    
    // 如果是当前用户被强制下线
    if (currentUser && currentUser.userId === record.userId) {
      message.warning('您已被强制下线，请重新登录')
      await userStore.logout()
      router.push('/login')
      window.location.reload()
      return
    }

    await forceOfflineUser(record.connectionId)
    message.success('强制下线成功')
    await fetchData()
  } catch (error) {
    console.error('[在线用户] 强制下线失败:', error)
    message.error('强制下线失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要下线的用户')
    return
  }
  try {
    await Promise.all(selectedRowKeys.value.map(id => forceOfflineUser(id)))
    message.success('批量下线成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量下线失败')
  }
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 发送消息 */
const handleSendMessage = (record: HbtOnlineUserDto) => {
  selectedUserId.value = record.userId
  sendFormVisible.value = true
}

/** 发送消息成功 */
const handleSendSuccess = () => {
  message.success('消息发送成功')
  sendFormVisible.value = false
}

/** 处理接收到的消息 */
const handleReceivedMessage = (message: any) => {
  console.log('收到消息:', message)
  const newMessage = {
    id: Date.now().toString(),
    senderId: message.senderId,
    receiverId: message.receiverId,
    content: message.content,
    timestamp: new Date()
  }
  receivedMessages.push(newMessage)
}
</script>

<style scoped>
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.header-actions {
  display: flex;
  gap: 10px;
}
</style>