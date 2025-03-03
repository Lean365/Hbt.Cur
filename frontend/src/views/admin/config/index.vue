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
        :query-fields="queryItems"
        :loading="loading"
        @search="handleSearch"
        @reset="handleReset"
      />

      <!-- 配置表格 -->
      <hbt-table
        ref="tableRef"
        :loading="loading"
        :columns="columns"
        :data-source="configList"
        :pagination="pagination"
        show-selection
        show-toolbar
        :row-selection="{
          selectedRowKeys: selectedRowKeys,
          onChange: onSelectChange
        }"
        :toolbar-buttons="toolbarButtons"
        :action-column="actionColumn"
        @select="onSelectChange"
        @action="handleAction"
        @toolbar-action="handleToolbarAction"
        @change="handleTableChange"
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
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { HbtTableColumn } from '@/types/components/table'
import type { IHbtQueryItem } from '@/types/components/query'
import {
  PlusOutlined,
  DeleteOutlined,
  ImportOutlined,
  ExportOutlined,
  FileOutlined,
  EditOutlined,
  CheckOutlined,
  UndoOutlined
} from '@ant-design/icons-vue'
import HbtQuery from '@/components/Business/Query/index.vue'
import HbtTable from '@/components/Business/Table/index.vue'
import { useUserStore } from '@/stores/user'
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
  updateHbtConfigStatus,
  batchUpdateHbtConfigStatus
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
const userStore = useUserStore()

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
  pageIndex: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true,
  showTotal: (total: number) => `共 ${total} 条`
})

// 表单状态
const modalVisible = ref(false)
const modalTitle = ref('新增配置')
const currentRecord = ref<HbtConfig>()
const tableRef = ref()

// 工具栏按钮配置
const toolbarButtons = computed(() => [])

// 操作列配置
const actionColumn = computed(() => ({
  width: 150,
  fixed: 'right' as const,
  actions: [
    {
      key: 'edit',
      label: '',
      type: 'link' as const,
      icon: EditOutlined,
      tooltip: '编辑配置',
      permission: 'admin:config:update'
    },
    {
      key: 'delete',
      label: '',
      type: 'danger' as const,
      icon: DeleteOutlined,
      tooltip: '删除配置',
      permission: 'admin:config:delete',
      confirm: '确定要删除该配置吗？'
    }
  ]
}))

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
    dateFormat:'yyyy-MM-dd',
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
  try {
    loading.value = true
    
    console.log('[HbtConfig] 原始查询参数:', queryParams)
    
    // 构建请求参数，过滤掉 undefined、null 和空字符串值
    const params = Object.entries(queryParams).reduce((acc, [key, value]) => {
      console.log(`[HbtConfig] 处理参数 ${key}:`, value)
      if (value !== undefined && value !== null && value !== '') {
        acc[key] = value
      }
      return acc
    }, {} as Record<string, any>)
    
    console.log('[HbtConfig] 处理后的请求参数:', params)
    
    // 只显示配置管理相关的权限
    const configPermissions = userStore.permissions.filter(p => p.startsWith('admin:config:'))
    console.log('[HbtConfig] 配置管理权限:', configPermissions)

    const response = await getHbtConfigList(params)
    console.log('[HbtConfig] 获取配置列表响应:', response)
    
    if (response.code === 200) {
      configList.value = response.data.rows
      pagination.total = response.data.totalNum
      pagination.pageIndex = response.data.pageIndex
      pagination.pageSize = response.data.pageSize
      console.log('[HbtConfig] 更新列表数据成功:', {
        总数: response.data.totalNum,
        当前页: response.data.pageIndex,
        每页条数: response.data.pageSize,
        数据条数: response.data.rows.length
      })
    } else {
      message.error(response.msg || t('common.message.loadError'))
      console.error('[HbtConfig] 获取列表失败:', response.msg)
    }
  } catch (error: any) {
    console.error('[HbtConfig] 加载配置列表出错:', error)
    message.error(error.message || t('common.message.loadError'))
  } finally {
    loading.value = false
  }
}

// 查询处理
const handleSearch = (values: Record<string, any>) => {
  Object.assign(queryParams, values)
  pagination.pageIndex = 1
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
const handleTableChange = (pag: any, filters: any, sorter: any) => {
  pagination.pageIndex = pag.pageIndex
  pagination.pageSize = pag.pageSize
  loadConfigList()
}

// 编辑处理
const handleEdit = async (record: HbtConfig) => {
  console.log('[HbtConfig] 开始编辑配置:', record)
  try {
    const { data } = await getHbtConfig(record.configId)
    console.log('[HbtConfig] 获取配置详情响应:', data)
    if (data?.code === 200) {
      modalTitle.value = '编辑配置'
      currentRecord.value = data.data
      modalVisible.value = true
      console.log('[HbtConfig] 打开编辑模态框')
    } else {
      message.error(data?.msg || '获取配置详情失败')
      console.error('[HbtConfig] 获取配置详情失败:', data?.msg)
    }
  } catch (error) {
    console.error('[HbtConfig] 编辑配置失败:', error)
    message.error('获取配置详情失败')
  }
}

// 删除处理
const handleDelete = async (record: HbtConfig) => {
  console.log('[HbtConfig] 开始删除配置:', record)
  try {
    const { data } = await deleteHbtConfig(record.configId)
    console.log('[HbtConfig] 删除配置响应:', data)
    if (data?.code === 200) {
      message.success('删除成功')
      console.log('[HbtConfig] 删除成功')
      loadConfigList()
    } else {
      message.error(data?.msg || '删除失败')
      console.error('[HbtConfig] 删除失败:', data?.msg)
    }
  } catch (error) {
    console.error('[HbtConfig] 删除配置失败:', error)
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
  console.log('[HbtConfig] 开始更新状态:', record, '新状态:', checked)
  try {
    record.statusLoading = true
    const { data } = await updateHbtConfigStatus(record.configId, checked ? 0 : 1)
    console.log('[HbtConfig] 更新状态响应:', data)
    if (data?.code === 200) {
      message.success('状态更新成功')
      record.status = checked ? 0 : 1
      console.log('[HbtConfig] 状态更新成功')
    } else {
      message.error(data?.msg || '状态更新失败')
      console.error('[HbtConfig] 状态更新失败:', data?.msg)
    }
  } catch (error) {
    console.error('[HbtConfig] 更新状态失败:', error)
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

// 处理工具栏按钮点击
const handleToolbarAction = async (action: string) => {
  switch (action) {
    case 'add':
      modalTitle.value = '新增配置'
      currentRecord.value = undefined
      modalVisible.value = true
      break
    case 'delete':
      await handleBatchDelete()
      break
    case 'import':
      // 触发文件选择
      const input = document.createElement('input')
      input.type = 'file'
      input.accept = '.xlsx,.xls'
      input.onchange = async (e: Event) => {
        const file = (e.target as HTMLInputElement).files?.[0]
        if (file) {
          await handleImport(file)
        }
      }
      input.click()
      break
    case 'export':
      await handleExport()
      break
    case 'template':
      await handleTemplate()
      break
    case 'approve':
      await handleBatchApprove()
      break
    case 'revoke':
      await handleBatchRevoke()
      break
  }
}

// 批量审核处理
const handleBatchApprove = async () => {
  try {
    const { data } = await batchUpdateHbtConfigStatus(
      selectedRowKeys.value.map(Number),
      0
    )
    if (data?.code === 200) {
      message.success('批量审核成功')
      selectedRowKeys.value = []
      loadConfigList()
    } else {
      message.error(data?.msg || '批量审核失败')
    }
  } catch (error) {
    console.error('批量审核失败:', error)
    message.error('批量审核失败')
  }
}

// 批量撤销处理
const handleBatchRevoke = async () => {
  try {
    const { data } = await batchUpdateHbtConfigStatus(
      selectedRowKeys.value.map(Number),
      1
    )
    if (data?.code === 200) {
      message.success('批量撤销成功')
      selectedRowKeys.value = []
      loadConfigList()
    } else {
      message.error(data?.msg || '批量撤销失败')
    }
  } catch (error) {
    console.error('批量撤销失败:', error)
    message.error('批量撤销失败')
  }
}

// 表单提交处理
const handleSubmit = async (formData: HbtConfigCreate | HbtConfigUpdate) => {
  console.log('[HbtConfig] 开始提交表单:', formData)
  try {
    const { data } = await (currentRecord.value
      ? updateHbtConfig(formData as HbtConfigUpdate)
      : createHbtConfig(formData as HbtConfigCreate))
    console.log('[HbtConfig] 提交表单响应:', data)
    if (data?.code === 200) {
      message.success(`${currentRecord.value ? '更新' : '创建'}成功`)
      modalVisible.value = false
      console.log('[HbtConfig] 表单提交成功')
      loadConfigList()
    } else {
      message.error(data?.msg || `${currentRecord.value ? '更新' : '创建'}失败`)
      console.error('[HbtConfig] 表单提交失败:', data?.msg)
    }
  } catch (error) {
    console.error('[HbtConfig] 提交表单失败:', error)
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
  :deep(.success-text) {
    color: #52c41a;
  }

  :deep(.danger-text) {
    color: #ff4d4f;
  }
}
</style>