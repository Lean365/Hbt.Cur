<template>
  <div class="user-container">
    <a-card :bordered="false">
      <!-- 搜索表单 -->
      <a-form
        layout="inline"
        :model="searchForm"
        @finish="handleSearch"
        class="search-form"
      >
        <a-form-item label="用户名" name="userName">
          <a-input
            v-model:value="searchForm.userName"
            placeholder="请输入用户名"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="状态" name="status">
          <a-select
            v-model:value="searchForm.status"
            placeholder="请选择状态"
            style="width: 120px"
            allow-clear
          >
            <a-select-option value="normal">正常</a-select-option>
            <a-select-option value="disabled">禁用</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" html-type="submit">
              <template #icon><search-outlined /></template>
              查询
            </a-button>
            <a-button @click="handleReset">
              <template #icon><redo-outlined /></template>
              重置
            </a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="table-toolbar">
        <a-space>
          <a-button type="primary" @click="handleAdd">
            <template #icon><plus-outlined /></template>
            新增
          </a-button>
          <a-button @click="handleExport">
            <template #icon><export-outlined /></template>
            导出
          </a-button>
        </a-space>
      </div>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="tableData"
        :loading="loading"
        :pagination="pagination"
        @change="handleTableChange"
        row-key="id"
      >
        <template #bodyCell="{ column, record }">
          <!-- 状态列 -->
          <template v-if="column.key === 'status'">
            <a-tag :color="record.status === 'normal' ? 'success' : 'error'">
              {{ record.status === 'normal' ? '正常' : '禁用' }}
            </a-tag>
          </template>
          
          <!-- 操作列 -->
          <template v-if="column.key === 'action'">
            <a-space>
              <a @click="handleEdit(record)">编辑</a>
              <a-divider type="vertical" />
              <a-popconfirm
                title="确定要删除此用户吗？"
                @confirm="handleDelete(record)"
              >
                <a class="text-danger">删除</a>
              </a-popconfirm>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 编辑对话框 -->
    <a-modal
      v-model:visible="modalVisible"
      :title="modalTitle"
      @ok="handleModalOk"
      @cancel="handleModalCancel"
    >
      <a-form
        ref="formRef"
        :model="formData"
        :rules="formRules"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 16 }"
      >
        <a-form-item label="用户名" name="userName">
          <a-input
            v-model:value="formData.userName"
            placeholder="请输入用户名"
            :disabled="!!formData.id"
          />
        </a-form-item>
        <a-form-item
          label="密码"
          name="password"
          :rules="[{ required: !formData.id, message: '请输入密码' }]"
        >
          <a-input-password
            v-model:value="formData.password"
            placeholder="请输入密码"
          />
        </a-form-item>
        <a-form-item label="显示名称" name="displayName">
          <a-input
            v-model:value="formData.displayName"
            placeholder="请输入显示名称"
          />
        </a-form-item>
        <a-form-item label="邮箱" name="email">
          <a-input
            v-model:value="formData.email"
            placeholder="请输入邮箱"
          />
        </a-form-item>
        <a-form-item label="状态" name="status">
          <a-select
            v-model:value="formData.status"
            placeholder="请选择状态"
          >
            <a-select-option value="normal">正常</a-select-option>
            <a-select-option value="disabled">禁用</a-select-option>
          </a-select>
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import type { FormInstance, TableColumnsType } from 'ant-design-vue'
import {
  SearchOutlined,
  RedoOutlined,
  PlusOutlined,
  ExportOutlined
} from '@ant-design/icons-vue'

interface UserInfo {
  id: number
  userName: string
  displayName: string
  email: string
  status: string
  createdTime: string
}

// 表格列定义
const columns: TableColumnsType = [
  {
    title: '用户名',
    dataIndex: 'userName',
    key: 'userName',
    width: 150
  },
  {
    title: '显示名称',
    dataIndex: 'displayName',
    key: 'displayName',
    width: 150
  },
  {
    title: '邮箱',
    dataIndex: 'email',
    key: 'email',
    width: 200
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '创建时间',
    dataIndex: 'createdTime',
    key: 'createdTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right' as const
  }
]

// 搜索表单数据
const searchForm = reactive({
  userName: '',
  status: undefined as string | undefined
})

// 表格数据
const loading = ref(false)
const tableData = ref<UserInfo[]>([])
const pagination = reactive({
  current: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true
})

// 编辑表单数据
const modalVisible = ref(false)
const modalTitle = ref('')
const formRef = ref<FormInstance>()
const formData = reactive<UserInfo & { password?: string }>({
  id: 0,
  userName: '',
  password: '',
  displayName: '',
  email: '',
  status: 'normal',
  createdTime: ''
})

// 表单验证规则
const formRules = {
  userName: [
    { required: true, message: '请输入用户名' },
    { min: 3, max: 20, message: '用户名长度必须在3-20个字符之间' }
  ],
  displayName: [
    { required: true, message: '请输入显示名称' }
  ],
  email: [
    { required: true, message: '请输入邮箱' },
    { validator: async (_rule: any, value: string) => {
      if (value && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
        return Promise.reject('请输入正确的邮箱格式')
      }
      return Promise.resolve()
    }}
  ],
  status: [
    { required: true, message: '请选择状态' }
  ]
}

// 处理搜索
const handleSearch = () => {
  pagination.current = 1
  fetchData()
}

// 处理重置
const handleReset = () => {
  searchForm.userName = ''
  searchForm.status = undefined
  handleSearch()
}

// 处理表格变化
const handleTableChange = (pag: any) => {
  pagination.current = pag.current
  pagination.pageSize = pag.pageSize
  fetchData()
}

// 处理新增
const handleAdd = () => {
  modalTitle.value = '新增用户'
  formData.id = 0
  formData.userName = ''
  formData.password = ''
  formData.displayName = ''
  formData.email = ''
  formData.status = 'normal'
  formData.createdTime = ''
  modalVisible.value = true
}

// 处理编辑
const handleEdit = (record: any) => {
  modalTitle.value = '编辑用户'
  Object.assign(formData, record)
  formData.password = ''
  modalVisible.value = true
}

// 处理删除
const handleDelete = async (record: any) => {
  try {
    // 调用删除API
    console.log('删除用户:', record.id)
    // 重新加载数据
    fetchData()
  } catch (error) {
    console.error('删除用户失败:', error)
  }
}

// 处理导出
const handleExport = () => {
  console.log('导出用户数据')
}

// 处理模态框确认
const handleModalOk = () => {
  formRef.value?.validate().then(() => {
    // 调用保存API
    console.log('保存用户:', formData)
    modalVisible.value = false
    // 重新加载数据
    fetchData()
  })
}

// 处理模态框取消
const handleModalCancel = () => {
  modalVisible.value = false
}

// 获取表格数据
const fetchData = () => {
  loading.value = true
  // 模拟API调用
  setTimeout(() => {
    tableData.value = [
      {
        id: 1,
        userName: 'admin',
        displayName: '管理员',
        email: 'admin@example.com',
        status: 'normal',
        createdTime: '2024-01-22 10:00:00'
      }
    ]
    pagination.total = 1
    loading.value = false
  }, 500)
}

// 初始化
fetchData()
</script>

<style lang="less" scoped>
.user-container {
  .search-form {
    margin-bottom: 16px;
  }

  .table-toolbar {
    margin-bottom: 16px;
  }

  .text-danger {
    color: #ff4d4f;
  }
}
</style> 