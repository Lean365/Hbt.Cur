<template>
  <div class="workflow-task-container">
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
      :show-add="false"
      :show-edit="false"
      :show-delete="false"
      :show-import="false"
      :show-export="true"
      :export-permission="['workflow:task:export']"
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
      :row-key="(record: HbtTask) => String(record.processTaskId)"
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
          <hbt-dict-tag dict-type="workflow_task_status" :value="record.status" />
        </template>
        
        <template v-if="column.dataIndex === 'taskType'">
          <hbt-dict-tag dict-type="workflow_task_type" :value="record.taskType" />
        </template>
        
        <template v-if="column.dataIndex === 'priority'">
          <hbt-dict-tag dict-type="workflow_task_priority" :value="record.priority || 0" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['workflow:task:query']"
            :show-approve="OPERABLE_STATUS.includes(record.status)"
            :approve-permission="['workflow:task:approve']"
            :show-reject="OPERABLE_STATUS.includes(record.status)"
            :reject-permission="['workflow:task:reject']"
            :show-transfer="OPERABLE_STATUS.includes(record.status)"
            :transfer-permission="['workflow:task:transfer']"
            :show-revoke="OPERABLE_STATUS.includes(record.status)"
            :revoke-permission="['workflow:task:update']"
            size="small"
            @view="handleView"
            @approve="handleApprove"
            @reject="handleReject"
            @transfer="handleTransfer"
            @revoke="handleCancel"
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

    <!-- 查看详情 -->
    <a-modal
      v-model:open="detailVisible"
      title="工作流任务详情"
    >
      <!-- 工作流任务详情内容 -->
    </a-modal>

    <!-- 审批组件 -->
    <workflow-approve
      v-model:open="approveVisible"
      :task-id="selectedWorkflowTaskId"
      @success="handleSuccess"
    />

    <workflow-reject
      v-model:open="rejectVisible"
      :task-id="selectedWorkflowTaskId"
      @success="handleSuccess"
    />

    <workflow-transfer
      v-model:open="transferVisible"
      :task-id="selectedWorkflowTaskId"
      @success="handleSuccess"
    />

    <workflow-cancel
      v-model:open="cancelVisible"
      :task-id="selectedWorkflowTaskId"
      @success="handleSuccess"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      v-model:open="columnSettingVisible"
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
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import { useDictStore } from '@/stores/dict'
import { useRouter } from 'vue-router'
import type { HbtTask, HbtTaskQuery } from '@/types/workflow/task'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { 
  getWorkflowTaskList, 
  exportWorkflowTask, 
  getWorkflowTaskTemplate
} from '@/api/workflow/task'
import WorkflowApprove from './components/WorkflowApprove.vue'
import WorkflowReject from './components/WorkflowReject.vue'
import WorkflowTransfer from './components/WorkflowTransfer.vue'
import WorkflowCancel from './components/WorkflowCancel.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// 任务状态常量定义
const TASK_STATUS = {
  PENDING: 0,      // 待处理
  PROCESSING: 1,   // 处理中
  APPROVED: 2,     // 已同意
  REJECTED: 3,     // 已拒绝
  RETURNED: 4,     // 已退回
  TRANSFERRED: 5,  // 已转办
  CANCELLED: 6     // 已取消
} as const

// 可操作任务的状态（只有待处理和处理中的任务可以操作）
const OPERABLE_STATUS = [TASK_STATUS.PENDING, TASK_STATUS.PROCESSING]

// 状态验证辅助方法
const isTaskOperable = (status: number): boolean => OPERABLE_STATUS.includes(status as any)

const getStatusText = (status: number): string => {
  switch (status) {
    case TASK_STATUS.PENDING: return '待处理'
    case TASK_STATUS.PROCESSING: return '处理中'
    case TASK_STATUS.APPROVED: return '已同意'
    case TASK_STATUS.REJECTED: return '已拒绝'
    case TASK_STATUS.RETURNED: return '已退回'
    case TASK_STATUS.TRANSFERRED: return '已转办'
    case TASK_STATUS.CANCELLED: return '已取消'
    default: return '未知状态'
  }
}

// 表格列定义
const columns = [
  {
    title: t('workflow.task.fields.processTaskId'),
    dataIndex: 'processTaskId',
    key: 'processTaskId',
    width: 100
  },
  {
    title: t('workflow.task.fields.instanceName'),
    dataIndex: 'instanceName',
    key: 'instanceName',
    width: 200
  },
  {
    title: t('workflow.task.fields.nodeName'),
    dataIndex: 'nodeName',
    key: 'nodeName',
    width: 200
  },
  {
    title: t('workflow.task.fields.taskName'),
    dataIndex: 'taskName',
    key: 'taskName',
    width: 200
  },
  {
    title: t('workflow.task.fields.taskType'),
    dataIndex: 'taskType',
    key: 'taskType',
    width: 150
  },
  {
    title: t('workflow.task.fields.assigneeName'),
    dataIndex: 'assigneeName',
    key: 'assigneeName',
    width: 200
  },
  {
    title: t('workflow.task.fields.status'),
    dataIndex: 'status',
    key: 'status',
    width: 150
  },
  {
    title: t('workflow.task.fields.comment'),
    dataIndex: 'comment',
    key: 'comment',
    width: 300
  },
  {
    title: t('workflow.task.fields.completeTime'),
    dataIndex: 'completeTime',
    key: 'completeTime',
    width: 200
  },
  {
    title: t('workflow.task.fields.dueTime'),
    dataIndex: 'dueTime',
    key: 'dueTime',
    width: 200
  },
  {
    title: t('workflow.task.fields.reminderTime'),
    dataIndex: 'reminderTime',
    key: 'reminderTime',
    width: 200
  },
  {
    title: t('workflow.task.fields.priority'),
    dataIndex: 'priority',
    key: 'priority',
    width: 150
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    align: 'center',
    ellipsis: true
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'taskName',
    label: t('workflow.task.fields.taskName'),
    type: 'input' as const
  },
  {
    name: 'taskType',
    label: t('workflow.task.fields.taskType'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_task_type',
      //type: 'radio'
    }
  },
  {
    name: 'status',
    label: t('workflow.task.fields.status'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_task_status',
      //type: 'radio'
    }
  }
]

// 查询参数
const queryParams = ref<HbtTaskQuery>({
  pageIndex: 1,
  pageSize: 10,
  taskName: undefined,
  taskType: undefined,
  status: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtTask[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 弹窗控制相关
const selectedWorkflowTaskId = ref<number | undefined>(undefined)
const detailVisible = ref(false)
const approveVisible = ref(false)
const rejectVisible = ref(false)
const transferVisible = ref(false)
const cancelVisible = ref(false)

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getWorkflowTaskList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
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
    taskName: undefined,
    taskType: undefined,
    status: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportWorkflowTask(queryParams.value)
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
  localStorage.setItem('workflowTaskColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: HbtTask) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: HbtTask) => {
  selectedWorkflowTaskId.value = record.processTaskId
  detailVisible.value = true
}

// 处理同意
const handleApprove = (record: HbtTask) => {
  if (!isTaskOperable(record.status)) {
    message.warning(`任务状态为"${getStatusText(record.status)}"，无法进行操作`)
    return
  }
  approveVisible.value = true
  selectedWorkflowTaskId.value = record.processTaskId
}

// 处理拒绝
const handleReject = (record: HbtTask) => {
  if (!isTaskOperable(record.status)) {
    message.warning(`任务状态为"${getStatusText(record.status)}"，无法进行操作`)
    return
  }
  rejectVisible.value = true
  selectedWorkflowTaskId.value = record.processTaskId
}

// 处理转办
const handleTransfer = (record: HbtTask) => {
  if (!isTaskOperable(record.status)) {
    message.warning(`任务状态为"${getStatusText(record.status)}"，无法进行操作`)
    return
  }
  transferVisible.value = true
  selectedWorkflowTaskId.value = record.processTaskId
}

// 处理撤销
const handleCancel = (record: HbtTask) => {
  if (!isTaskOperable(record.status)) {
    message.warning(`任务状态为"${getStatusText(record.status)}"，无法进行操作`)
    return
  }
  cancelVisible.value = true
  selectedWorkflowTaskId.value = record.processTaskId
}

// 分页处理
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  fetchData()
}

// 处理表单提交成功
const handleSuccess = () => {
  fetchData()
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['workflow_task_status', 'workflow_task_type', 'workflow_task_priority'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})

// 初始化列设置
const initColumnSettings = () => {
  const savedSettings = localStorage.getItem('workflowTaskColumnSettings')
  if (savedSettings) {
    columnSettings.value = JSON.parse(savedSettings)
  } else {
    columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, true]))
  }
}
</script>

<style lang="less" scoped>
.workflow-task-container {
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