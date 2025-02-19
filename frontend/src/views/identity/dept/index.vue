<template>
  <div class="app-container">
    <div class="mb-2">
      <a-button type="primary" @click="handleAdd">
        <template #icon><plus-outlined /></template>
        {{ t('identity.dept.add') }}
      </a-button>
      <a-button class="ml-2" @click="handleRefresh">
        <template #icon><reload-outlined /></template>
        {{ t('common.refresh') }}
      </a-button>
    </div>

    <a-card :bordered="false">
      <a-form layout="inline" class="mb-2">
        <a-form-item :label="t('identity.dept.deptName')">
          <a-input
            v-model:value="queryParams.deptName"
            :placeholder="t('identity.dept.deptName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('common.status')">
          <a-select
            v-model:value="queryParams.status"
            :placeholder="t('common.status.placeholder')"
            style="width: 200px"
            allow-clear
          >
            <a-select-option value="0">{{ t('common.status.normal') }}</a-select-option>
            <a-select-option value="1">{{ t('common.status.disabled') }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item>
          <a-button type="primary" @click="handleQuery">
            <template #icon><search-outlined /></template>
            {{ t('common.search') }}
          </a-button>
        </a-form-item>
      </a-form>

      <a-table
        :loading="loading"
        :columns="columns"
        :data-source="deptList"
        :row-key="(record) => record.id"
        :pagination="false"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'status'">
            <a-tag :color="record.status === '0' ? 'success' : 'error'">
              {{ record.status === '0' ? t('common.status.normal') : t('common.status.disabled') }}
            </a-tag>
          </template>
          <template v-else-if="column.key === 'createTime'">
            {{ formatDateTime(record.createTime) }}
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a-button type="link" @click="handleEdit(record)">
                {{ t('common.edit') }}
              </a-button>
              <a-popconfirm
                :title="t('identity.dept.delete.confirm')"
                @confirm="handleDelete(record)"
              >
                <a-button type="link" danger>
                  {{ t('common.delete') }}
                </a-button>
              </a-popconfirm>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <dept-form
      v-model:visible="formVisible"
      :title="formTitle"
      :form-data="formData"
      @success="handleSuccess"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import {
  PlusOutlined,
  ReloadOutlined,
  SearchOutlined
} from '@ant-design/icons-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { Dept, DeptQuery } from '@/types/identity/dept'
import { listDept, deleteDept } from '@/api/identity/dept'
import { formatDateTime } from '@/utils/datetime'
import DeptForm from './components/DeptForm.vue'

const { t } = useI18n()

// 查询参数
const queryParams = reactive<DeptQuery>({
  deptName: '',
  status: undefined
})

// 加载状态
const loading = ref(false)

// 部门列表
const deptList = ref<Dept[]>([])

// 表格列定义
const columns: TableColumnsType<Dept> = [
  {
    title: t('identity.dept.deptName'),
    dataIndex: 'deptName',
    key: 'deptName',
    width: 200
  },
  {
    title: t('identity.dept.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('identity.dept.leader'),
    dataIndex: 'leader',
    key: 'leader',
    width: 120
  },
  {
    title: t('identity.dept.phone'),
    dataIndex: 'phone',
    key: 'phone',
    width: 120
  },
  {
    title: t('identity.dept.email'),
    dataIndex: 'email',
    key: 'email',
    width: 180
  },
  {
    title: t('common.status'),
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
    title: t('common.operation'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Dept>()

// 加载部门列表
const loadDeptList = async () => {
  loading.value = true
  try {
    const res = await listDept(queryParams)
    if (res.code === 200) {
      deptList.value = res.data
    } else {
      message.error(res.msg)
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 查询
const handleQuery = () => {
  loadDeptList()
}

// 刷新
const handleRefresh = () => {
  queryParams.deptName = ''
  queryParams.status = undefined
  loadDeptList()
}

// 新增
const handleAdd = () => {
  formTitle.value = t('identity.dept.add')
  formData.value = undefined
  formVisible.value = true
}

// 编辑
const handleEdit = (record: Dept) => {
  formTitle.value = t('identity.dept.edit')
  formData.value = record
  formVisible.value = true
}

// 删除
const handleDelete = async (record: Dept) => {
  try {
    const res = await deleteDept(record.id)
    if (res.code === 200) {
      message.success(t('identity.dept.delete.success'))
      loadDeptList()
    } else {
      message.error(res.msg || t('identity.dept.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 表单提交成功
const handleSuccess = () => {
  loadDeptList()
}

// 组件挂载时加载数据
onMounted(() => {
  loadDeptList()
})
</script>

<style scoped>
.ml-2 {
  margin-left: 8px;
}
.mb-2 {
  margin-bottom: 16px;
}
</style> 