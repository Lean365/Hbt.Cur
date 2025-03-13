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
        <a-form-item :label="t('identity.user.userName.label')" name="userName">
          <a-input
            v-model:value="searchForm.userName"
            :placeholder="t('identity.user.userName.placeholder')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.phoneNumber.label')" name="phoneNumber">
          <a-input
            v-model:value="searchForm.phoneNumber"
            :placeholder="t('identity.user.phoneNumber.placeholder')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.status.label')" name="status">
          <a-select
            v-model:value="searchForm.status"
            :placeholder="t('common.status.placeholder')"
            style="width: 120px"
            allow-clear
          >
            <a-select-option
              v-for="dict in dictStore.getDictDataByType('sys_normal_disable')"
              :key="dict.dictValue"
              :value="dict.dictValue"
            >
              {{ dict.dictLabel }}
            </a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" html-type="submit">
              <template #icon><search-outlined /></template>
              {{ t('common.search') }}
            </a-button>
            <a-button @click="handleReset">
              <template #icon><redo-outlined /></template>
              {{ t('common.reset') }}
            </a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="table-toolbar">
        <a-space>
          <a-button type="primary" @click="handleAdd" v-hasPermi="['identity:user:add']">
            <template #icon><plus-outlined /></template>
            {{ t('common.add') }}
          </a-button>
          <a-button @click="handleExport" v-hasPermi="['identity:user:export']">
            <template #icon><export-outlined /></template>
            {{ t('common.export') }}
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
        row-key="userId"
      >
        <template #bodyCell="{ column, record }">
          <!-- 用户类型列 -->
          <template v-if="column.key === 'userType'">
            <dict-tag
              :options="dictStore.getDictDataByType('sys_user_type')"
              :value="record.userType"
            />
          </template>
          
          <!-- 性别列 -->
          <template v-if="column.key === 'sex'">
            <dict-tag
              :options="dictStore.getDictDataByType('sys_gender')"
              :value="record.sex"
            />
          </template>
          
          <!-- 状态列 -->
          <template v-if="column.key === 'status'">
            <dict-tag
              :options="dictStore.getDictDataByType('sys_normal_disable')"
              :value="record.status"
            />
          </template>
          
          <!-- 操作列 -->
          <template v-if="column.key === 'action'">
            <a-space>
              <a @click="handleEdit(record)" v-hasPermi="['identity:user:edit']">
                {{ t('common.edit') }}
              </a>
              <a-divider type="vertical" />
              <a-popconfirm
                :title="t('common.delete.confirm')"
                @confirm="handleDelete(record)"
                v-hasPermi="['identity:user:delete']"
              >
                <a class="text-danger">{{ t('common.delete') }}</a>
              </a-popconfirm>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 用户表单对话框 -->
    <user-form
      v-model:visible="formVisible"
      :title="formTitle"
      :user-id="selectedUserId"
      @success="handleSuccess"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import {
  SearchOutlined,
  RedoOutlined,
  PlusOutlined,
  ExportOutlined
} from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import type { User, UserQuery } from '@/types/identity/user'
import { getPagedList, deleteUser } from '@/api/identity/user'
import UserForm from './components/UserForm.vue'
import DictTag from '@/components/DictTag/index.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 表格列定义
const columns = [
  {
    title: t('identity.user.userName.label'),
    dataIndex: 'userName',
    key: 'userName',
    width: 120
  },
  {
    title: t('identity.user.nickName.label'),
    dataIndex: 'nickName',
    key: 'nickName',
    width: 120
  },
  {
    title: t('identity.user.userType.label'),
    dataIndex: 'userType',
    key: 'userType',
    width: 100
  },
  {
    title: t('identity.user.phoneNumber.label'),
    dataIndex: 'phoneNumber',
    key: 'phoneNumber',
    width: 120
  },
  {
    title: t('identity.user.email.label'),
    dataIndex: 'email',
    key: 'email',
    width: 180
  },
  {
    title: t('identity.user.gender.label'),
    dataIndex: 'sex',
    key: 'sex',
    width: 80
  },
  {
    title: t('identity.user.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('common.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('common.action'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 搜索表单数据
const searchForm = reactive<UserQuery>({
  pageNum: 1,
  pageSize: 10,
  userName: '',
  phoneNumber: '',
  status: undefined
})

// 表格数据
const loading = ref(false)
const tableData = ref<User[]>([])
const pagination = reactive<TablePaginationConfig>({
  current: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true
})

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedUserId = ref<number>()

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getPagedList(searchForm)
    tableData.value = res.data.rows
    pagination.total = res.data.total
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 处理搜索
const handleSearch = () => {
  searchForm.pageNum = 1
  fetchData()
}

// 处理重置
const handleReset = () => {
  searchForm.userName = ''
  searchForm.phoneNumber = ''
  searchForm.status = undefined
  handleSearch()
}

// 处理表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  searchForm.pageNum = pag.current as number
  searchForm.pageSize = pag.pageSize as number
  fetchData()
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('common.add')
  selectedUserId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: User) => {
  formTitle.value = t('common.edit')
  selectedUserId.value = record.userId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: User) => {
  try {
    await deleteUser(record.userId)
    message.success(t('common.delete.success'))
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理导出
const handleExport = () => {
  message.info(t('common.developing'))
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 组件挂载时加载数据
onMounted(() => {
  // 加载字典数据
  dictStore.loadDictData(['sys_normal_disable', 'sys_gender', 'sys_user_type'])
  // 加载表格数据
  fetchData()
})
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
