//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : CodeGenerator
// 创建时间: {{ datetime }}
// 版本号 : v1.0.0
// 描述    : {{ table.comment }}列表页面
//===================================================================

<template>
  <div class="app-container">
    <!-- 查询表单 -->
    <hbt-query
      :query-fields="queryFields"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    >
      <template #buttons>
        <a-space>
          <a-button
            v-if="checkPermission('{{ table.module_name }}:{{ table.name }}:add')"
            type="primary"
            @click="handleAdd"
          >
            <template #icon>
              <plus-outlined />
            </template>
            {{ t('{{ table.module_name }}.{{ table.name }}.button.add') }}
          </a-button>
          <a-button
            v-if="checkPermission('{{ table.module_name }}:{{ table.name }}:export')"
            :loading="exportLoading"
            @click="handleExport"
          >
            <template #icon>
              <download-outlined />
            </template>
            {{ t('{{ table.module_name }}.{{ table.name }}.button.export') }}
          </a-button>
          <a-button
            v-if="checkPermission('{{ table.module_name }}:{{ table.name }}:import')"
            @click="handleImport"
          >
            <template #icon>
              <upload-outlined />
            </template>
            {{ t('{{ table.module_name }}.{{ table.name }}.button.import') }}
          </a-button>
          <a-button
            v-if="checkPermission('{{ table.module_name }}:{{ table.name }}:delete')"
            danger
            :disabled="!selectedRowKeys.length"
            @click="handleBatchDelete"
          >
            <template #icon>
              <delete-outlined />
            </template>
            {{ t('{{ table.module_name }}.{{ table.name }}.button.delete') }}
          </a-button>
        </a-space>
      </template>
    </hbt-query>

    <!-- 数据表格 -->
    <a-table
      row-key="{{ get_pk_name table }}"
      :loading="loading"
      :columns="columns"
      :data-source="dataSource"
      :pagination="pagination"
      :row-selection="rowSelection"
      @change="handleTableChange"
    >
      <!-- 状态列 -->
      <template #status="{ text }">
        <dict-tag dict-type="sys_normal_disable" :value="text" />
      </template>

      <!-- 操作列 -->
      <template #action="{ record }">
        <a-space>
          <a
            v-if="checkPermission('{{ table.module_name }}:{{ table.name }}:edit')"
            @click="handleEdit(record)"
          >
            {{ t('{{ table.module_name }}.{{ table.name }}.button.edit') }}
          </a>
          <a-divider type="vertical" />
          <a-popconfirm
            :title="t('{{ table.module_name }}.{{ table.name }}.message.confirm.delete')"
            @confirm="handleDelete(record)"
          >
            <a
              v-if="checkPermission('{{ table.module_name }}:{{ table.name }}:delete')"
              class="text-danger"
            >
              {{ t('{{ table.module_name }}.{{ table.name }}.button.delete') }}
            </a>
          </a-popconfirm>
          {{~ if has_status_column table ~}}
          <a-divider type="vertical" />
          <a-switch
            v-if="checkPermission('{{ table.module_name }}:{{ table.name }}:edit')"
            :checked="record.status === 0"
            :loading="record.statusLoading"
            @change="(checked: boolean) => handleStatusChange(record, checked)"
          />
          {{~ end ~}}
        </a-space>
      </template>
    </a-table>

    <!-- 表单弹窗 -->
    <{{ pascal_case table.table_name }}Form
      v-model:visible="formVisible"
      :title="formTitle"
      :loading="formLoading"
      :model="formData"
      @ok="handleFormOk"
    />

    <!-- 导入弹窗 -->
    <hbt-import
      v-model:visible="importVisible"
      :title="t('{{ table.module_name }}.{{ table.name }}.import.title')"
      :template-name="{{ pascal_case table.table_name }}"
      :upload-api="importData"
      @success="handleImportSuccess"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { PlusOutlined, DownloadOutlined, UploadOutlined, DeleteOutlined } from '@ant-design/icons-vue'
import { checkPermission } from '@/utils/permission'
import type { QueryField } from '@/components/Business/Query'
import type { {{ pascal_case table.table_name }}, {{ pascal_case table.table_name }}Query } from '@/types/{{ table.module_name }}/{{ table.name }}'
import {
  getPagedList,
  getDetail,
  create,
  update,
  remove,
  exportData,
  importData,
  updateStatus
} from '@/api/{{ table.module_name }}/{{ table.name }}'
import {{ pascal_case table.table_name }}Form from './components/{{ pascal_case table.table_name }}Form.vue'

const { t } = useI18n()

// === 查询表单配置 ===
const queryFields = computed<QueryField[]>(() => [
  {{~ for column in table.columns ~}}
  {{~ if column.is_query ~}}
  {
    name: '{{ column.column_name }}',
    label: t('{{ table.module_name }}.{{ table.name }}.fields.{{ column.column_name }}.label'),
    {{~ if column.data_type == "string" ~}}
    type: 'input',
    {{~ else if column.data_type == "int" or column.data_type == "long" ~}}
    type: 'number',
    {{~ else if column.data_type == "datetime" ~}}
    type: 'dateRange',
    {{~ else if column.column_name == "status" ~}}
    type: 'select',
    props: {
      dictType: 'sys_normal_disable'
    }
    {{~ end ~}}
  },
  {{~ end ~}}
  {{~ end ~}}
])

// === 表格列配置 ===
const columns = [
  {{~ for column in table.columns ~}}
  {{~ if column.is_list ~}}
  {
    title: t('{{ table.module_name }}.{{ table.name }}.fields.{{ column.column_name }}.label'),
    dataIndex: '{{ column.column_name }}',
    {{~ if column.column_name == "status" ~}}
    slots: { customRender: 'status' }
    {{~ end ~}}
  },
  {{~ end ~}}
  {{~ end ~}}
  {
    title: t('common.action'),
    key: 'action',
    width: 200,
    slots: { customRender: 'action' }
  }
]

// === 状态定义 ===
const loading = ref(false)
const exportLoading = ref(false)
const formVisible = ref(false)
const formLoading = ref(false)
const importVisible = ref(false)
const dataSource = ref<{{ pascal_case table.table_name }}[]>([])
const selectedRowKeys = ref<number[]>([])
const formTitle = ref('')
const formData = ref<Partial<{{ pascal_case table.table_name }}>>({})

// === 分页配置 ===
const pagination = reactive<TablePaginationConfig>({
  current: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true
})

// === 查询参数 ===
const queryParams = reactive<{{ pascal_case table.table_name }}Query>({
  pageIndex: 1,
  pageSize: 10
})

// === 表格选择配置 ===
const rowSelection = {
  selectedRowKeys: computed(() => selectedRowKeys.value),
  onChange: (keys: number[]) => {
    selectedRowKeys.value = keys
  }
}

// === 方法定义 ===
// 加载数据
const loadData = async () => {
  try {
    loading.value = true
    const { data } = await getPagedList(queryParams)
    dataSource.value = data.rows
    pagination.total = data.totalNum
  } finally {
    loading.value = false
  }
}

// 查询
const handleSearch = (values: any) => {
  queryParams.pageIndex = 1
  Object.assign(queryParams, values)
  loadData()
}

// 重置
const handleReset = () => {
  queryParams.pageIndex = 1
  queryParams.pageSize = 10
  loadData()
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  queryParams.pageIndex = pag.current as number
  queryParams.pageSize = pag.pageSize as number
  pagination.current = pag.current as number
  pagination.pageSize = pag.pageSize as number
  loadData()
}

// 新增
const handleAdd = () => {
  formTitle.value = t('{{ table.module_name }}.{{ table.name }}.button.add')
  formData.value = {}
  formVisible.value = true
}

// 编辑
const handleEdit = async (record: {{ pascal_case table.table_name }}) => {
  try {
    formLoading.value = true
    formTitle.value = t('{{ table.module_name }}.{{ table.name }}.button.edit')
    const { data } = await getDetail(record.{{ get_pk_name table }})
    formData.value = data
    formVisible.value = true
  } finally {
    formLoading.value = false
  }
}

// 删除
const handleDelete = async (record: {{ pascal_case table.table_name }}) => {
  try {
    await remove(record.{{ get_pk_name table }})
    message.success(t('{{ table.module_name }}.{{ table.name }}.message.success.delete'))
    loadData()
  } catch (error) {
    // 删除失败
    message.error(t('{{ table.module_name }}.{{ table.name }}.message.error.delete'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  try {
    await Promise.all(selectedRowKeys.value.map((id) => remove(id)))
    message.success(t('{{ table.module_name }}.{{ table.name }}.message.success.delete'))
    selectedRowKeys.value = []
    loadData()
  } catch (error) {
    // 删除失败
    message.error(t('{{ table.module_name }}.{{ table.name }}.message.error.delete'))
  }
}

// 导出
const handleExport = async () => {
  try {
    exportLoading.value = true
    await exportData({
      ...queryParams,
      exportType: selectedRowKeys.value.length ? 2 : 3,
      ids: selectedRowKeys.value
    })
  } finally {
    exportLoading.value = false
  }
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 导入成功
const handleImportSuccess = () => {
  importVisible.value = false
  loadData()
}

// 表单确认
const handleFormOk = () => {
  formVisible.value = false
  loadData()
}

{{~ if has_status_column table ~}}
// 更新状态
const handleStatusChange = async (record: {{ pascal_case table.table_name }}, checked: boolean) => {
  try {
    record.statusLoading = true
    await updateStatus({
      {{ get_pk_name table }}: record.{{ get_pk_name table }},
      status: checked ? 0 : 1
    })
    message.success(t('{{ table.module_name }}.{{ table.name }}.message.success.update'))
    loadData()
  } catch (error) {
    // 更新失败
    message.error(t('{{ table.module_name }}.{{ table.name }}.message.error.update'))
  } finally {
    record.statusLoading = false
  }
}
{{~ end ~}}

// 初始化
loadData()
</script> 