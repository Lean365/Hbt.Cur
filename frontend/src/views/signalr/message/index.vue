<template>
  <div class="online-message-container">
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
      :show-add="true"
      :show-edit="true"
      :add-permission="['realtime:message:create']"
      :edit-permission="['realtime:message:create']"
      :delete-permission="['realtime:message:delete']"
      :disabled-delete="selectedRowKeys.length === 0"
      @refresh="fetchData"
      @delete="handleBatchDelete"
      @add="handleSendMessage"
      @edit="handleSendTestMessage"
      @toggle-search="toggleSearch"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: HbtOnlineMessageDto) => record.id.toString()"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 消息类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'messageType'">
          <a-tag :color="getMessageTypeColor(record.messageType)">
            {{ getMessageTypeText(record.messageType) }}
          </a-tag>
        </template>
        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <a-space>
            <a-popconfirm
              title="确定要删除该消息吗？"
              @confirm="handleDelete(record)"
              ok-text="确定"
              cancel-text="取消"
            >
              <a class="text-danger" v-hasPermi="['realtime:message:delete']">删除</a>
            </a-popconfirm>
          </a-space>
        </template>
      </template>
    </hbt-table>

    <SendForm
      v-model:visible="sendFormVisible"
      @success="handleSearch"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, onUnmounted, computed } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtOnlineMessageDto } from '@/types/signalr/onlineMessage'
import { getOnlineMessageList, deleteOnlineMessage } from '@/api/signalr/onlineMessage'
import { signalRService } from '@/utils/SignalR/service'
import { useUserStore, type UserInfoResponse } from '@/stores/user'
import SendForm from './components/SendForm.vue'

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userId',
    label: '用户ID',
    type: 'input',
    placeholder: '请输入用户ID'
  },
  {
    name: 'messageType',
    label: '消息类型',
    type: 'select',
    placeholder: '请选择消息类型',
    options: [
      { label: '系统消息', value: 'System' },
      { label: '邮件通知', value: 'Email' },
      { label: '通知状态', value: 'Notification' },
      { label: '任务状态', value: 'Task' },
      { label: '个人通知', value: 'Personal' },
      { label: '系统广播', value: 'Broadcast' }
    ]
  }
]

// 表格列定义
const columns = [
  {
    title: '消息ID',
    dataIndex: 'id',
    key: 'id',
    width: 120
  },
  {
    title: '用户ID',
    dataIndex: 'userId',
    key: 'userId',
    width: 120
  },
  {
    title: '消息类型',
    dataIndex: 'messageType',
    key: 'messageType',
    width: 120
  },
  {
    title: '消息内容',
    dataIndex: 'content',
    key: 'content',
    ellipsis: true
  },
  {
    title: '发送时间',
    dataIndex: 'sendTime',
    key: 'sendTime',
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
const tableData = ref<HbtOnlineMessageDto[]>([])
const total = ref(0)
const queryParams = reactive({
  pageIndex: 1,
  pageSize: 10,
  userId: undefined,
  messageType: undefined
})
const selectedRowKeys = ref<string[]>([])
const showSearch = ref(true)
const sendFormVisible = ref(false)

// 获取用户信息
const userStore = useUserStore()
const currentUser = computed(() => userStore.userInfo)

// 生命周期钩子
onMounted(async () => {
  // 获取用户信息
  if (!userStore.userInfo) {
    try {
      await userStore.getUserInfo()
      console.log('用户信息获取成功:', userStore.userInfo)
    } catch (error) {
      console.error('获取用户信息失败:', error)
      message.error('获取用户信息失败，请重新登录')
      return
    }
  }

  fetchData()
  // 监听新消息事件
  signalRService.on('ReceiveMessage', handleNewMessage)
})

onUnmounted(() => {
  // 移除事件监听
  signalRService.off('ReceiveMessage', handleNewMessage)
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const { data: res } = await getOnlineMessageList(queryParams)
    tableData.value = res.rows || []
    total.value = res.totalNum || 0
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
  queryParams.messageType = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 新消息事件 */
const handleNewMessage = () => {
  fetchData()
}

/** 删除消息 */
const handleDelete = async (record: HbtOnlineMessageDto) => {
  try {
    await deleteOnlineMessage(record.id)
    message.success('删除成功')
    fetchData()
  } catch (error) {
    message.error('删除失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的消息')
    return
  }
  try {
    await Promise.all(selectedRowKeys.value.map(id => deleteOnlineMessage(Number(id))))
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量删除失败')
  }
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 获取消息类型颜色 */
const getMessageTypeColor = (type: string) => {
  const colors: Record<string, string> = {
    System: 'blue',
    Email: 'green',
    Notification: 'orange',
    Task: 'purple',
    Personal: 'cyan',
    Broadcast: 'red'
  }
  return colors[type] || 'default'
}

/** 获取消息类型文本 */
const getMessageTypeText = (type: string) => {
  const texts: Record<string, string> = {
    System: '系统消息',
    Email: '邮件通知',
    Notification: '通知状态',
    Task: '任务状态',
    Personal: '个人通知',
    Broadcast: '系统广播'
  }
  return texts[type] || type
}

/** 处理搜索成功 */
const handleSearch = () => {
  queryParams.pageIndex = 1
  fetchData()
}

const handleSendMessage = () => {
  sendFormVisible.value = true
}

const handleSendTestMessage = async () => {
  const userInfo = userStore.userInfo as UserInfoResponse
  console.log('当前用户信息:', userInfo)
  
  if (!userInfo) {
    message.warning('用户信息未获取到，请刷新页面重试')
    return
  }

  if (!userInfo.userId) {
    message.warning('用户ID未获取到，请重新登录')
    return
  }

  try {
    const testContent = `测试消息 ${new Date().toLocaleString()}`
    console.log('发送测试消息:', {
      userId: userInfo.userId,
      userName: userInfo.userName,
      content: testContent
    })
    
    await signalRService.sendMessage({
      userId: userInfo.userId.toString(),
      content: testContent
    })
    message.success('测试消息发送成功')
  } catch (error) {
    console.error('发送测试消息失败:', error)
    message.error('发送测试消息失败')
  }
}
</script>

<style lang="less" scoped>
.online-message-container {
  padding: 16px;
}

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