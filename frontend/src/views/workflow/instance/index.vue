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
      :show-delete="true"
      :delete-permission="['workflow:instance:delete']"
      :show-import="true"
      :import-permission="['workflow:instance:import']"
      :show-export="true"
      :export-permission="['workflow:instance:export']"
      :disabled-delete="selectedRowKeys.length === 0"
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
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: HbtWorkflowInstance) => String(record.id)"
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
            :show-delete="true"
            :delete-permission="['workflow:instance:delete']"
            :show-view="true"
            :view-permission="['workflow:instance:query']"
            :show-submit="record.status === 0"
            :submit-permission="['workflow:instance:submit']"
            :show-withdraw="record.status === 1"
            :withdraw-permission="['workflow:instance:withdraw']"
            :show-terminate="record.status === 1"
            :terminate-permission="['workflow:instance:terminate']"
            size="small"
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

    <!-- 导入对话框 -->
    <a-modal
      v-model:open="importVisible"
      title="导入工作流实例"
      @ok="handleImportSubmit"
      @cancel="handleImportCancel"
    >
      <a-upload
        :file-list="fileList"
        :before-upload="beforeUpload"
        :customRequest="customRequest"
      >
        <a-button>
          <upload-outlined />
          点击上传
        </a-button>
      </a-upload>
      <template #footer>
        <a-button key="back" @click="handleImportCancel">取消</a-button>
        <a-button key="submit" type="primary" :loading="importLoading" @click="handleImportSubmit">
          确定
        </a-button>
      </template>
    </a-modal>

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
      title="列设置"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>

    <!-- 新增/编辑表单 -->
    <workflow-instance-form
      v-model:visible="formVisible"
      :title="formTitle"
      :instance-id="selectedInstanceId"
      @success="handleSuccess"
    />

    <!-- 查看详情 -->
    <a-modal
      v-model:open="detailVisible"
      title="工作流实例详情"
    >
      <!-- 工作流实例详情内容 -->
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import { UploadOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import { useRouter } from 'vue-router'
import type { HbtWorkflowInstance, HbtWorkflowInstanceQuery } from '@/types/workflow/workflowInstance'
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
} from '@/api/workflow/workflowInstance'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// 表格列定义
const columns = [
  {
    title: t('workflow.instance.workflowDefinitionId'),
    dataIndex: 'workflowDefinitionId',
    key: 'workflowDefinitionId',
    width: 200
  },
  {
    title: t('workflow.instance.workflowTitle'),
    dataIndex: 'workflowTitle',
    key: 'workflowTitle',
    width: 200
  },
  {
    title: t('workflow.instance.currentNodeId'),
    dataIndex: 'currentNodeId',
    key: 'currentNodeId',
    width: 200
  },
  {
    title: t('workflow.instance.initiatorId'),
    dataIndex: 'initiatorId',
    key: 'initiatorId',
    width: 150
  },
  {
    title: t('workflow.instance.status'),
    dataIndex: 'status',
    key: 'status',
    width: 150
  },
  {
    title: t('workflow.instance.startTime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 180
  },
  {
    title: t('workflow.instance.endTime'),
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
    name: 'workflowDefinitionId',
    label: t('workflow.instance.fields.workflowDefinitionId.label'),
    type: 'input' as const
  },
  {
    name: 'workflowTitle',
    label: t('workflow.instance.fields.workflowTitle.label'),
    type: 'input' as const
  },
  {
    name: 'currentNodeId',
    label: t('workflow.instance.fields.currentNodeId.label'),
    type: 'input' as const
  },
  {
    name: 'initiatorId',
    label: t('workflow.instance.fields.initiatorId.label'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('workflow.instance.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_instance_status',
      type: 'radio'
    }
  },
  {
    name: 'startTime',
    label: t('workflow.instance.fields.startTime.label'),
    type: 'dateRange' as const
  }
]

// 查询参数
const queryParams = ref<HbtWorkflowInstanceQuery>({
  pageIndex: 1,
  pageSize: 10,
  workflowDefinitionId: undefined,
  workflowTitle: undefined,
  currentNodeId: undefined,
  initiatorId: undefined,
  status: undefined,
  startTime: undefined,
  endTime: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtWorkflowInstance[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 导入相关
const importVisible = ref(false)
const importLoading = ref(false)
const fileList = ref<any[]>([])

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
    workflowDefinitionId: undefined,
    workflowTitle: undefined,
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
const handleDelete = async (record: HbtWorkflowInstance) => {
  try {
    const res = await deleteWorkflowInstance(Number(record.id))
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
    const res = await exportWorkflowInstance(queryParams.value)
    if (res.data.code === 200) {
      message.success(t('common.export.success'))
    } else {
      message.error(res.data.msg || t('common.export.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.export.failed'))
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

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    settings[col.key] = checkedValue.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('workflowInstanceColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: HbtWorkflowInstance) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: HbtWorkflowInstance) => {
  selectedInstanceId.value = record.id
  detailVisible.value = true
}

// 处理提交
const handleSubmit = async (record: HbtWorkflowInstance) => {
  try {
    const res = await submitWorkflowInstance(Number(record.id))
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
const handleWithdraw = async (record: HbtWorkflowInstance) => {
  try {
    const res = await withdrawWorkflowInstance(Number(record.id))
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
const handleTerminate = (record: HbtWorkflowInstance) => {
  terminateForm.value.id = Number(record.id)
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
const handleImport = () => {
  importVisible.value = true
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getWorkflowInstanceTemplate()
    if (res.data.code === 200) {
      message.success(t('common.download.success'))
    } else {
      message.error(res.data.msg || t('common.download.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.download.failed'))
  }
}

// 处理导入提交
const handleImportSubmit = async () => {
  if (!fileList.value.length) {
    message.warning(t('common.upload.selectFile'))
    return
  }

  importLoading.value = true
  try {
    const res = await importWorkflowInstance(fileList.value[0])
    if (res.data.code === 200) {
      message.success(t('common.import.success'))
      importVisible.value = false
      fileList.value = []
      fetchData()
    } else {
      message.error(res.data.msg || t('common.import.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.import.failed'))
  }
  importLoading.value = false
}

// 处理导入取消
const handleImportCancel = () => {
  importVisible.value = false
  fileList.value = []
}

// 上传前处理
const beforeUpload = (file: any) => {
  const isExcel = file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || 
                  file.type === 'application/vnd.ms-excel'
  if (!isExcel) {
    message.error(t('common.upload.excelOnly'))
    return false
  }
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isLt2M) {
    message.error(t('common.upload.sizeLimit'))
    return false
  }
  fileList.value = [file]
  return false
}

// 自定义上传
const customRequest = (options: any) => {
  const { file } = options
  fileList.value = [file]
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
const handleEdit = (record: HbtWorkflowInstance) => {
  selectedInstanceId.value = record.id
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

// 初始化列设置
const initColumnSettings = () => {
  const savedSettings = localStorage.getItem('workflowInstanceColumnSettings')
  if (savedSettings) {
    columnSettings.value = JSON.parse(savedSettings)
  } else {
    columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, true]))
  }
}
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