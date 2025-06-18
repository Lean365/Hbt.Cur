<template>
  <div class="workflow-instance-container">
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
      :show-add="true"
      :add-permission="['workflow:instance:create']"
      :show-edit="true"
      :edit-permission="['workflow:instance:update']"
      :show-delete="true"
      :delete-permission="['workflow:instance:delete']"
      :show-import="true"
      :import-permission="['workflow:instance:import']"
      :show-export="true"
      :export-permission="['workflow:instance:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
      @export="handleExport"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="visibleColumns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: HbtInstance) => String(record.instanceId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="workflow_instance_status" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['workflow:instance:update']"
            :show-delete="true"
            :delete-permission="['workflow:instance:delete']"
            :show-view="true"
            :view-permission="['workflow:instance:query']"
            :show-submit="true"
            :submit-permission="['workflow:instance:submit']"
            :show-withdraw="true"
            :withdraw-permission="['workflow:instance:withdraw']"
            :show-terminate="true"
            :terminate-permission="['workflow:instance:terminate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @view="handleView"
            @submit="handleSubmit"
            @withdraw="handleWithdraw"
            @terminate="handleTerminate"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 新增/编辑表单 -->
    <instance-form
      v-model:open="formVisible"
      :title="formTitle"
      :instance-id="selectedInstanceId"
      @success="handleSuccess"
    />

    <!-- 查看详情 -->
    <instance-detail
      v-model:open="detailVisible"
      :instance-id="selectedInstanceId"
    />

    <!-- 终止对话框 -->
    <a-modal
      v-model:open="terminateVisible"
      title="终止工作流实例"
      @ok="handleTerminateSubmit"
      @cancel="handleTerminateCancel"
    >
      <a-form :model="terminateForm" :rules="terminateRules">
        <a-form-item label="终止原因" name="reason">
          <a-textarea v-model:value="terminateForm.reason" placeholder="请输入终止原因" :rows="4" />
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
      :title="t('common.columnSetting')"
      placement="right"
      width="300"
      @close="columnSettingVisible = false"
    >
      <a-checkbox-group
        :value="Object.keys(columnSettings).filter(key => columnSettings[key])"
        @change="handleColumnSettingChange"
        class="column-setting-group"
      >
        <div v-for="col in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import { UploadOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import { useRouter } from 'vue-router'
import type { HbtInstance, HbtInstanceQuery } from '@/types/workflow/instance'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { 
  getWorkflowInstanceList, 
  deleteWorkflowInstance, 
  batchDeleteWorkflowInstance, 
  importWorkflowInstance, 
  exportWorkflowInstance, 
  getWorkflowInstanceTemplate,
  submitWorkflowInstance,
  withdrawWorkflowInstance,
  terminateWorkflowInstance
} from '@/api/workflow/instance'
import InstanceForm from './components/InstanceForm.vue'
import InstanceDetail from './components/InstanceDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// 表格列定义
const columns = [
  {
    title: t('workflow.instance.fields.instanceName'),
    dataIndex: 'instanceName',
    key: 'instanceName',
    width: 200
  },
  {
    title: t('workflow.instance.fields.businessKey'),
    dataIndex: 'businessKey',
    key: 'businessKey',
    width: 200
  },
  {
    title: t('workflow.instance.fields.currentNodeId'),
    dataIndex: 'currentNodeId',
    key: 'currentNodeId',
    width: 200
  },
  {
    title: t('workflow.instance.fields.initiatorId'),
    dataIndex: 'initiatorId',
    key: 'initiatorId',
    width: 150
  },
  {
    title: t('workflow.instance.fields.status'),
    dataIndex: 'status',
    key: 'status',
    width: 150
  },
  {
    title: t('workflow.instance.fields.startTime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 180
  },
  {
    title: t('workflow.instance.fields.endTime'),
    dataIndex: 'endTime',
    key: 'endTime',
    width: 180
  },
  {
    title: t('common.action'),
    key: 'action',
    fixed: 'right',
    width: 200
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'instanceName',
    label: t('workflow.instance.fields.instanceName'),
    type: 'input' as const
  },
  {
    name: 'businessKey',
    label: t('workflow.instance.fields.businessKey'),
    type: 'input' as const
  },
  {
    name: 'currentNodeId',
    label: t('workflow.instance.fields.currentNodeId'),
    type: 'input' as const
  },
  {
    name: 'initiatorId',
    label: t('workflow.instance.fields.initiatorId'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('workflow.instance.fields.status'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_instance_status',
      type: 'radio'
    }
  },
  {
    name: 'startTime',
    label: t('workflow.instance.fields.startTime'),
    type: 'dateRange' as const
  }
]

// 查询参数
const queryParams = ref<HbtInstanceQuery>({
  pageIndex: 1,
  pageSize: 10,
  definitionId: undefined,
  instanceName: undefined,
  currentNodeId: undefined,
  initiatorId: undefined,
  status: undefined,
  startTime: undefined,
  endTime: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtInstance[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 导入相关
const importLoading = ref(false)

// 终止相关
const terminateVisible = ref(false)
const terminateForm = ref({
  id: 0,
  reason: ''
})
const terminateRules = {
  reason: [
    { type: 'array' as const, required: true, message: t('workflow.instance.fields.terminateReason.required'), trigger: 'blur' as const }
  ]
}

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// 弹窗控制相关
const formVisible = ref(false)
const formTitle = ref('')
const selectedInstanceId = ref<number | undefined>(undefined)
const detailVisible = ref(false)

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getWorkflowInstanceList(queryParams.value)
    if (res.data) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
    }
  } catch (error) {
    console.error('获取工作流实例列表失败:', error)
    message.error('获取工作流实例列表失败')
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    definitionId: undefined,
    instanceName: undefined,
    currentNodeId: undefined,
    initiatorId: undefined,
    status: undefined,
    startTime: undefined,
    endTime: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理删除
const handleDelete = async (record: HbtInstance) => {
  try {
    const res = await deleteWorkflowInstance(Number(record.instanceId))
    if (res.data.code === 200) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportWorkflowInstance({
      ...queryParams.value
    })
    // 动态获取文件名
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    let fileName = ''
    if (disposition) {
      // 优先匹配 filename*（带中文）
      let match = disposition.match(/filename\*=UTF-8''([^;]+)/)
      if (match && match[1]) {
        fileName = decodeURIComponent(match[1])
      } else {
        // 再匹配 filename
        match = disposition.match(/filename="?([^";]+)"?/)
        if (match && match[1]) {
          fileName = match[1]
        }
      }
    }
    if (!fileName) {
      fileName = `工作流实例_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(res.data)
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('common.export.success'))
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || t('common.export.failed'))
  }
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteWorkflowInstance(Number(id))))
    const hasError = results.some(res => res.data.code !== 200)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('workflowInstanceColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('workflowInstanceColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: HbtInstance) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: HbtInstance) => {
  selectedInstanceId.value = record.instanceId
  detailVisible.value = true
}

// 处理提交
const handleSubmit = async (record: HbtInstance) => {
  try {
    const res = await submitWorkflowInstance(Number(record.instanceId))
    if (res.data.code === 200) {
      message.success(t('workflow.instance.submit.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('workflow.instance.submit.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.submit.failed'))
  }
}

// 处理撤回
const handleWithdraw = async (record: HbtInstance) => {
  try {
    const res = await withdrawWorkflowInstance(Number(record.instanceId))
    if (res.data.code === 200) {
      message.success(t('workflow.instance.withdraw.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('workflow.instance.withdraw.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.withdraw.failed'))
  }
}

// 处理终止
const handleTerminate = (record: HbtInstance) => {
  terminateForm.value.id = Number(record.instanceId)
  terminateVisible.value = true
}

// 处理终止提交
const handleTerminateSubmit = async () => {
  if (!terminateForm.value.reason) {
    message.warning(t('workflow.instance.fields.terminateReason.required'))
    return
  }

  try {
    const res = await terminateWorkflowInstance(terminateForm.value.id)
    if (res.data.code === 200) {
      message.success(t('workflow.instance.terminate.success'))
      terminateVisible.value = false
      terminateForm.value = {
        id: 0,
        reason: ''
      }
      fetchData()
    } else {
      message.error(res.data.msg || t('workflow.instance.terminate.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.terminate.failed'))
  }
}

// 处理终止取消
const handleTerminateCancel = () => {
  terminateVisible.value = false
  terminateForm.value = {
    id: 0,
    reason: ''
  }
}

// 处理导入
const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      importLoading.value = true
      try {
        const res = await importWorkflowInstance(file)
        if (res.data.code === 200) {
          message.success('导入成功')
          await fetchData()
        } else {
          message.error(res.data.msg || '导入失败')
        }
      } catch (error) {
        message.error('导入失败')
      } finally {
        importLoading.value = false
      }
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getWorkflowInstanceTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `工作流实例导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 处理页面变化
const handlePageChange = (page: number, pageSize?: number) => {
  queryParams.value.pageIndex = page
  if (pageSize) {
    queryParams.value.pageSize = pageSize
  }
  fetchData()
}

// 处理每页条数变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageSize = size
  fetchData()
}

// 处理新增
const handleAdd = () => {
  selectedInstanceId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: HbtInstance) => {
  selectedInstanceId.value = record.instanceId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 处理选中编辑
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    selectedInstanceId.value = selectedRowKeys.value[0] as number
    formTitle.value = t('common.title.edit')
    formVisible.value = true
  }
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedInstanceId.value = undefined
  fetchData()
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['workflow_instance_status'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-instance-container {
  height: 100%;
  background-color: #fff;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  padding: 8px;
  border-bottom: 1px solid var(--ant-color-split);
  
  &:last-child {
    border-bottom: none;
  }
}
</style> 