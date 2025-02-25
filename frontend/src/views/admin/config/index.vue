//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置管理页面
//===================================================================

<template>
  <div class="config-container">
    <a-card :bordered="false">
      <!-- 搜索表单 -->
      <HbtQuery
        :items="queryItems"
        :loading="loading"
        @search="handleSearch"
        @reset="handleReset"
      />

      <!-- 操作按钮 -->
      <div class="table-operations">
        <a-space>
          <a-button type="primary" @click="handleAdd" v-hasPermi="['admin:config:create']">
            <template #icon><plus-outlined /></template>
            新增
          </a-button>
          <a-button 
            :disabled="!selectedRowKeys.length"
            @click="handleBatchDelete"
            v-hasPermi="['admin:config:delete']"
          >
            <template #icon><delete-outlined /></template>
            批量删除
          </a-button>
          <a-upload
            name="file"
            :show-upload-list="false"
            :before-upload="handleImport"
            v-hasPermi="['admin:config:import']"
          >
            <a-button>
              <template #icon><upload-outlined /></template>
              导入
            </a-button>
          </a-upload>
          <a-button @click="handleExport" v-hasPermi="['admin:config:export']">
            <template #icon><download-outlined /></template>
            导出
          </a-button>
          <a-button @click="handleTemplate" v-hasPermi="['admin:config:query']">
            <template #icon><file-outlined /></template>
            模板
          </a-button>
        </a-space>
      </div>

      <!-- 配置表格 -->
      <hbt-table
        ref="tableRef"
        :loading="loading"
        :columns="columns"
        :data-source="configList"
        show-selection
        :action-column="{
          width: 150,
          fixed: 'right',
          actions: [
            {
              key: 'edit',
              label: '',
              type: 'link',
              icon: EditOutlined,
              tooltip: '编辑配置',
              permission: 'admin:config:update'
            },
            {
              key: 'delete',
              label: '',
              type: 'danger',
              icon: DeleteOutlined,
              tooltip: '删除配置',
              permission: 'admin:config:delete',
              confirm: '确定要删除该配置吗？'
            }
          ]
        }"
        @select="onSelectChange"
        @action="handleAction"
        @change="handleTableSort"
      >
        <!-- 自定义列渲染 -->
        <template #configBuiltin="{ text }">
          {{ text === 1 ? '是' : '否' }}
        </template>
        <template #status="{ record }">
          <a-switch
            :checked="record.status === 0"
            :loading="record.statusLoading"
            @change="(checked: string | number | boolean) => handleStatusChange(record, Boolean(checked))"
            v-hasPermi="['admin:config:update']"
          />
        </template>
      </hbt-table>

      <!-- 分页组件 -->
      <hbt-pagination
        v-model:Current="pagination.Current"
        v-model:PageSize="pagination.PageSize"
        :Total="pagination.Total"
        :ShowSizeChanger="pagination.ShowSizeChanger"
        :ShowQuickJumper="pagination.ShowQuickJumper"
        :ShowTotal="(total: number, range: [number, number]) => h('span', {}, `共 ${total} 条`)"
        @change="handleTableChange"
      />

      <!-- 配置表单 -->
      <config-form
        v-model:visible="modalVisible"
        :title="modalTitle"
        :record="currentRecord"
        @submit="handleSubmit"
      />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { HbtTableColumn } from '@/types/components/table'
import type { IHbtQueryItem } from '@/types/components/query'
import {
  PlusOutlined,
  DeleteOutlined,
  UploadOutlined,
  DownloadOutlined,
  FileOutlined,
  EditOutlined
} from '@ant-design/icons-vue'
import HbtQuery from '@/components/query/index.vue'
import HbtTable from '@/components/table/index.vue'
import HbtPagination from '@/components/pagination/index.vue'
import {
  getHbtConfigList,
  getHbtConfig,
  createHbtConfig,
  updateHbtConfig,
  deleteHbtConfig,
  batchDeleteHbtConfig,
  importHbtConfig,
  exportHbtConfig,
  getHbtConfigTemplate,
  updateHbtConfigStatus
} from '@/api/admin/hbtConfig'
import type {
  HbtConfig,
  HbtConfigQuery,
  HbtConfigCreate,
  HbtConfigUpdate
} from '@/types/admin/hbtConfig'
import ConfigForm from './components/ConfigForm.vue'

// 使用i18n
const { t } = useI18n()

// 查询参数
const queryParams = reactive<HbtConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  configName: '',
  configKey: '',
  configBuiltin: undefined,
  status: undefined
})

// 表格状态
const loading = ref(false)
const configList = ref<HbtConfig[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const pagination = reactive({
  Current: 1,
  PageSize: 10,
  Total: 0,
  ShowSizeChanger: true,
  ShowQuickJumper: true,
  ShowTotal: (total: number) => `共 ${total} 条`
})

// 表单状态
const modalVisible = ref(false)
const modalTitle = ref('新增配置')
const currentRecord = ref<HbtConfig>()
const tableRef = ref()

// 表格列定义
const columns: HbtTableColumn[] = [
  {
    title: '配置名称',
    dataIndex: 'configName',
    width: 200,
    align: 'left'
  },
  {
    title: '配置键名',
    dataIndex: 'configKey',
    width: 200,
    align: 'left'
  },
  {
    title: '配置值',
    dataIndex: 'configValue',
    width: 300,
    ellipsis: true,
    align: 'left'
  },
  {
    title: '系统内置',
    dataIndex: 'configBuiltin',
    width: 100,
    align: 'center'
  },
  {
    title: '备注',
    dataIndex: 'remark',
    ellipsis: true,
    align: 'left'
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 100,
    align: 'center'
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    width: 180,
    align: 'center',
    valueType: 'date',
    dateFormat:'YYYY-MM-DD',
    sorter: true
  }
]

// 查询条件配置
const queryItems: IHbtQueryItem[] = [
  {
    type: 'input',
    name: 'configName',
    label: '配置名称',
    placeholder: '请输入配置名称'
  },
  {
    type: 'input',
    name: 'configKey',
    label: '配置键名',
    placeholder: '请输入配置键名'
  },
  {
    type: 'select',
    name: 'configBuiltin',
    label: '系统内置',
    placeholder: '请选择',
    options: [
      { label: '是', value: 1 },
      { label: '否', value: 0 }
    ]
  },
  {
    type: 'select',
    name: 'status',
    label: '状态',
    placeholder: '请选择',
    options: [
      { label: '正常', value: 0 },
      { label: '停用', value: 1 }
    ]
  }
]

// 加载配置列表
const loadConfigList = async () => {
  loading.value = true
  try {
    const params = {
      pageIndex: pagination.Current,
      pageSize: pagination.PageSize,
      configName: queryParams.configName || undefined,
      configKey: queryParams.configKey || undefined,
      configBuiltin: queryParams.configBuiltin,
      status: queryParams.status
    }
    const { data: pageData } = await getHbtConfigList(params)
    console.log('API Response:', pageData)
    if (pageData && pageData.rows) {
      configList.value = pageData.rows
      pagination.Total = pageData.totalNum
    } else {
      message.error('加载配置列表失败')
      configList.value = []
      pagination.Total = 0
    }
  } catch (error) {
    console.error('加载配置列表失败:', error)
    message.error('加载配置列表失败')
    configList.value = []
    pagination.Total = 0
  } finally {
    loading.value = false
  }
}

// 查询处理
const handleSearch = (values: Record<string, any>) => {
  Object.assign(queryParams, values)
  pagination.Current = 1
  loadConfigList()
}

// 重置处理
const handleReset = () => {
  queryParams.configName = ''
  queryParams.configKey = ''
  queryParams.configBuiltin = undefined
  queryParams.status = undefined
  handleSearch(queryParams)
}

// 选择变化处理
const onSelectChange = (keys: (string | number)[]) => {
  selectedRowKeys.value = keys
}

// 表格变化处理
const handleTableChange = (page: number, pageSize: number) => {
  pagination.Current = page
  pagination.PageSize = pageSize
  loadConfigList()
}

// 表格排序和筛选处理
const handleTableSort = (pagination: any, filters: any, sorter: any) => {
  loadConfigList()
}

// 新增处理
const handleAdd = () => {
  modalTitle.value = '新增配置'
  currentRecord.value = undefined
  modalVisible.value = true
}

// 编辑处理
const handleEdit = async (record: HbtConfig) => {
  try {
    const { data } = await getHbtConfig(record.configId)
    if (data?.code === 200) {
      modalTitle.value = '编辑配置'
      currentRecord.value = data.data
      modalVisible.value = true
    } else {
      message.error(data?.msg || '获取配置详情失败')
    }
  } catch (error) {
    console.error('获取配置详情失败:', error)
    message.error('获取配置详情失败')
  }
}

// 删除处理
const handleDelete = async (record: HbtConfig) => {
  try {
    const { data } = await deleteHbtConfig(record.configId)
    if (data?.code === 200) {
      message.success('删除成功')
      loadConfigList()
    } else {
      message.error(data?.msg || '删除失败')
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

// 批量删除处理
const handleBatchDelete = async () => {
  try {
    const { data } = await batchDeleteHbtConfig(selectedRowKeys.value.map(Number))
    if (data?.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      loadConfigList()
    } else {
      message.error(data?.msg || '批量删除失败')
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error('批量删除失败')
  }
}

// 导入处理
const handleImport = async (file: File) => {
  const formData = new FormData()
  formData.append('file', file)
  try {
    const { data } = await importHbtConfig(formData)
    if (data?.code === 200) {
      message.success('导入成功')
      loadConfigList()
    } else {
      message.error(data?.msg || '导入失败')
    }
  } catch (error) {
    console.error('导入失败:', error)
    message.error('导入失败')
  }
  return false
}

// 导出处理
const handleExport = async () => {
  try {
    const { data } = await exportHbtConfig(queryParams)
    if (data) {
      const blob = new Blob([data], { type: 'application/vnd.ms-excel' })
      const url = window.URL.createObjectURL(blob)
      const link = document.createElement('a')
      link.href = url
      link.download = '系统配置.xlsx'
      link.click()
      window.URL.revokeObjectURL(url)
    }
  } catch (error) {
    console.error('导出失败:', error)
    message.error('导出失败')
  }
}

// 下载模板
const handleTemplate = async () => {
  try {
    const { data } = await getHbtConfigTemplate()
    if (data) {
      const blob = new Blob([data], { type: 'application/vnd.ms-excel' })
      const url = window.URL.createObjectURL(blob)
      const link = document.createElement('a')
      link.href = url
      link.download = '系统配置导入模板.xlsx'
      link.click()
      window.URL.revokeObjectURL(url)
    }
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

// 状态变更处理
const handleStatusChange = async (record: HbtConfig, checked: boolean) => {
  try {
    record.statusLoading = true
    const { data } = await updateHbtConfigStatus(record.configId, checked ? 0 : 1)
    if (data?.code === 200) {
      message.success('状态更新成功')
      record.status = checked ? 0 : 1
    } else {
      message.error(data?.msg || '状态更新失败')
    }
  } catch (error) {
    console.error('状态更新失败:', error)
    message.error('状态更新失败')
  } finally {
    record.statusLoading = false
  }
}

// 处理操作按钮点击
const handleAction = async (action: string, record: HbtConfig) => {
  switch (action) {
    case 'edit':
      await handleEdit(record)
      break
    case 'delete':
      await handleDelete(record)
      break
  }
}

// 表单提交处理
const handleSubmit = async (formData: HbtConfigCreate | HbtConfigUpdate) => {
  try {
    const { data } = await (currentRecord.value
      ? updateHbtConfig(formData as HbtConfigUpdate)
      : createHbtConfig(formData as HbtConfigCreate))
      
    if (data?.code === 200) {
      message.success(`${currentRecord.value ? '更新' : '创建'}成功`)
      modalVisible.value = false
      loadConfigList()
    } else {
      message.error(data?.msg || `${currentRecord.value ? '更新' : '创建'}失败`)
    }
  } catch (error) {
    console.error(`${currentRecord.value ? '更新' : '创建'}失败:`, error)
    message.error(`${currentRecord.value ? '更新' : '创建'}失败`)
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadConfigList()
})
</script>

<style lang="less" scoped>
.config-container {
  .search-form {
    margin-bottom: 24px;
  }

  .table-operations {
    margin-bottom: 16px;
  }

  :deep(.success-text) {
    color: #52c41a;
  }

  :deep(.danger-text) {
    color: #ff4d4f;
  }
}
</style>