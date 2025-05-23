<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :confirm-loading="confirmLoading"
    width="1200px"
    @update:visible="(val: boolean) => $emit('update:visible', val)"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <div class="post-user-container">
      <!-- 左侧：已分配用户列表 -->
      <div class="user-list">
        <div class="list-header">
          <h3>已分配用户</h3>
          <a-input-search
            v-model:value="selectedSearchText"
            placeholder="搜索用户"
            style="width: 200px"
            @change="handleSelectedSearch"
          />
        </div>
        <hbt-table
          :columns="columns"
          :data-source="selectedUsers"
          :loading="loading"
          :pagination="false"
          size="small"
          :scroll="{ y: 400 }"
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.dataIndex === 'action'">
              <a-button type="link" danger @click="handleRemoveUser(record as HbtUser)">
                移除
              </a-button>
            </template>
          </template>
        </hbt-table>
      </div>

      <!-- 右侧：可选用户列表 -->
      <div class="user-list">
        <div class="list-header">
          <h3>可选用户</h3>
          <a-input-search
            v-model:value="availableSearchText"
            placeholder="搜索用户"
            style="width: 200px"
            @change="handleAvailableSearch"
          />
        </div>
        <hbt-table
          :columns="columns"
          :data-source="availableUsers"
          :loading="loading"
          :pagination="false"
          size="small"
          :scroll="{ y: 400 }"
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.dataIndex === 'action'">
              <a-button type="link" @click="handleAddUser(record as HbtUser)">
                添加
              </a-button>
            </template>
          </template>
        </hbt-table>
      </div>
    </div>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { HbtUser } from '@/types/identity/user'
import { getPostUsers, assignPostUsers, removePostUsers } from '@/api/identity/post'


interface Props {
  visible: boolean
  postId?: number
}

interface Emits {
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

// 标题
const title = computed(() => `分配岗位用户 - ${props.postId}`)

// 加载状态
const loading = ref(false)

// 确认加载
const confirmLoading = ref(false)

// 搜索文本
const selectedSearchText = ref('')
const availableSearchText = ref('')

// 用户列表
const selectedUsers = ref<HbtUser[]>([])
const availableUsers = ref<HbtUser[]>([])

// 表格列定义
const columns: TableColumnsType = [
  {
    title: '用户名',
    dataIndex: 'userName',
    key: 'userName',
    width: 200,
    ellipsis: true
  },
  {
    title: '昵称',
    dataIndex: 'nickName',
    key: 'nickName',
    width: 200,
    ellipsis: true
  },
  {
    title: '操作',
    dataIndex: 'action',
    key: 'action',
    width: 80,
    fixed: 'right',
    align: 'center'
  }
]

interface PostUserResponse {
  assignedUsers: HbtUser[]
  optionalUsers: HbtUser[]
}

// 获取岗位用户列表
const getPostUserList = async () => {
  try {
    console.log('开始获取岗位用户列表, postId:', props.postId)
    if (!props.postId) {
      console.error('postId is undefined')
      return
    }
    loading.value = true
    const res = await getPostUsers(props.postId)
    console.log('API响应:', res)
    if (res.data.code === 200) {
      const data = res.data.data
      console.log('解析后的数据:', data)
      
      // 获取已分配用户列表
      const assignedUsers = data.assignedUsers || []
      // 获取可选用户列表
      const optionalUsers = data.optionalUsers || []
      
      console.log('已分配用户:', assignedUsers)
      console.log('可选用户:', optionalUsers)
      
      selectedUsers.value = assignedUsers
      availableUsers.value = optionalUsers
      
      console.log('更新后的用户列表:', { 
        selectedUsers: selectedUsers.value, 
        availableUsers: availableUsers.value 
      })
    } else {
      message.error(res.data.msg || '获取岗位用户列表失败')
    }
  } catch (error: any) {
    console.error('获取岗位用户列表失败:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      // 触发重新登录
      window.location.href = '/login'
    } else {
      message.error('获取岗位用户列表失败')
    }
  } finally {
    loading.value = false
  }
}

// 处理添加用户
const handleAddUser = async (user: HbtUser) => {
  try {
    if (!user.userId) {
      message.error('用户ID不能为空')
      return
    }
    const res = await assignPostUsers(props.postId!, [user.userId])
    if (res.data.code === 200) {
      message.success('添加用户成功')
      // 更新列表
      selectedUsers.value.push(user)
      availableUsers.value = availableUsers.value.filter(u => u.userId !== user.userId)
      // 触发父组件刷新
      emit('success')
    } else {
      message.error(res.data.msg || '添加用户失败')
    }
  } catch (error: any) {
    console.error('添加用户失败:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error('添加用户失败')
    }
  }
}

// 处理移除用户
const handleRemoveUser = async (user: HbtUser) => {
  try {
    if (!user.userId) {
      message.error('用户ID不能为空')
      return
    }
    const res = await removePostUsers(props.postId!, [user.userId])
    if (res.data.code === 200) {
      message.success('移除用户成功')
      // 更新列表
      selectedUsers.value = selectedUsers.value.filter(u => u.userId !== user.userId)
      availableUsers.value.push(user)
      // 触发父组件刷新
      emit('success')
    } else {
      message.error(res.data.msg || '移除用户失败')
    }
  } catch (error: any) {
    console.error('移除用户失败:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error('移除用户失败')
    }
  }
}

// 处理已分配用户搜索
const handleSelectedSearch = () => {
  if (!selectedSearchText.value) {
    getPostUserList()
    return
  }
  const searchText = selectedSearchText.value.toLowerCase()
  selectedUsers.value = selectedUsers.value.filter(user => 
    user.userName.toLowerCase().includes(searchText) ||
    user.nickName?.toLowerCase().includes(searchText) ||
    user.phoneNumber?.includes(searchText)
  )
}

// 处理可选用户搜索
const handleAvailableSearch = () => {
  if (!availableSearchText.value) {
    getPostUserList()
    return
  }
  const searchText = availableSearchText.value.toLowerCase()
  availableUsers.value = availableUsers.value.filter(user => 
    user.userName.toLowerCase().includes(searchText) ||
    user.nickName?.toLowerCase().includes(searchText) ||
    user.phoneNumber?.includes(searchText)
  )
}

// 提交表单
const handleSubmit = () => {
  // 触发父组件刷新
  emit('success')
  emit('update:visible', false)
}

// 取消处理
const handleCancel = () => {
  emit('update:visible', false)
  selectedUsers.value = []
  availableUsers.value = []
  selectedSearchText.value = ''
  availableSearchText.value = ''
}

// 监听可见性变化
watch(
  () => props.visible,
  (val) => {
    console.log('visible changed:', val, 'postId:', props.postId)
    if (val && props.postId) {
      getPostUserList()
    }
  },
  { immediate: true }
)

// 监听postId变化
watch(
  () => props.postId,
  (val) => {
    console.log('postId changed:', val, 'visible:', props.visible)
    if (val && props.visible) {
      getPostUserList()
    }
  },
  { immediate: true }
)

onMounted(() => {
  console.log('PostUserForm mounted, visible:', props.visible, 'postId:', props.postId)
  if (props.visible && props.postId) {
    getPostUserList()
  }
})
</script>

<style lang="less" scoped>
.post-user-container {
  display: flex;
  gap: 16px;
  height: 500px;

  .user-list {
    flex: 1;
    display: flex;
    flex-direction: column;
    border: 1px solid var(--ant-color-border);
    border-radius: 4px;
    padding: 16px;

    .list-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 16px;

      h3 {
        margin: 0;
        font-size: 16px;
        font-weight: 500;
      }
    }

    :deep(.hbt-table) {
      flex: 1;
      overflow: hidden;
    }
  }
}
</style> 