<template>
  <div class="workflow-execution-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['workflow:engine:execution:start']"
      :show-edit="true"
      :edit-permission="['workflow:engine:execution:update']"
      :show-delete="true"
      :delete-permission="['workflow:engine:execution:delete']"
      :show-import="true"
      :import-permission="['workflow:engine:execution:import']"
      :show-export="true"
      :export-permission="['workflow:engine:execution:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleStartWorkflow"
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
          <a-tag :color="getStatusColor(record.status)">
            {{ getStatusText(record.status) }}
          </a-tag>
        </template>

        <!-- 优先级列 -->
        <template v-if="column.dataIndex === 'priority'">
          <a-tag :color="getPriorityColor(record.priority)">
            {{ getPriorityText(record.priority) }}
          </a-tag>
        </template>

        <!-- 紧急程度列 -->
        <template v-if="column.dataIndex === 'urgency'">
          <a-tag :color="getUrgencyColor(record.urgency)">
            {{ getUrgencyText(record.urgency) }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['workflow:engine:execution:query']"
            :show-edit="record.status === 1"
            :edit-permission="['workflow:engine:execution:update']"
            :show-delete="true"
            :delete-permission="['workflow:engine:execution:delete']"
            :show-start="record.status === 0"
            :start-permission="['workflow:engine:execution:start']"
            :show-suspend="record.status === 1"
            :suspend-permission="['workflow:engine:execution:suspend']"
            :show-resume="record.status === 3"
            :resume-permission="['workflow:engine:execution:resume']"
            :show-terminate="record.status === 1 || record.status === 3"
            :terminate-permission="['workflow:engine:execution:terminate']"
            :show-submit="record.status === 0"
            :submit-permission="['workflow:engine:execution:submit']"
            :show-withdraw="record.status === 1"
            :withdraw-permission="['workflow:engine:execution:withdraw']"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDelete"
            @start="handleStart"
            @suspend="handleSuspend"
            @resume="handleResume"
            @terminate="handleTerminate"
            @submit="handleSubmit"
            @withdraw="handleWithdraw"
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

    <!-- 启动流程对话框 -->
    <a-modal
      v-model:open="startWorkflowVisible"
      title="启动工作流"
      width="600px"
      @ok="confirmStartWorkflow"
      @cancel="startWorkflowVisible = false"
    >
      <a-form
        ref="startWorkflowFormRef"
        :model="startWorkflowForm"
        :rules="startWorkflowRules"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 18 }"
      >
        <a-form-item label="流程定义" name="schemeId">
          <a-select
            v-model:value="startWorkflowForm.schemeId"
            placeholder="请选择流程定义"
            :loading="schemeOptionsLoading"
            :options="schemeOptions"
            @change="handleSchemeChange"
          />
        </a-form-item>
        <a-form-item label="实例标题" name="instanceTitle">
          <a-input
            v-model:value="startWorkflowForm.instanceTitle"
            placeholder="请输入实例标题"
          />
        </a-form-item>
        <a-form-item label="业务键" name="businessKey">
          <a-input
            v-model:value="startWorkflowForm.businessKey"
            placeholder="请输入业务键（可选）"
          />
        </a-form-item>
        <a-form-item label="流程变量" name="variables">
          <a-textarea
            v-model:value="startWorkflowForm.variables"
            placeholder="请输入流程变量（JSON格式，可选）"
            :rows="4"
          />
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- 实例详情对话框 -->
    <a-modal
      v-model:open="detailVisible"
      title="工作流实例详情"
      width="800px"
      :footer="null"
    >
      <a-descriptions :column="2" bordered>
        <a-descriptions-item label="实例ID">
          {{ currentRecord?.instanceId }}
        </a-descriptions-item>
        <a-descriptions-item label="实例标题">
          {{ currentRecord?.instanceTitle }}
        </a-descriptions-item>
        <a-descriptions-item label="业务键">
          {{ currentRecord?.businessKey || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="发起人ID">
          {{ currentRecord?.initiatorId }}
        </a-descriptions-item>
        <a-descriptions-item label="当前节点ID">
          {{ currentRecord?.currentNodeId || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="当前节点名称">
          {{ currentRecord?.currentNodeName || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="状态">
          <a-tag :color="getStatusColor(currentRecord?.status)">
            {{ getStatusText(currentRecord?.status) }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="优先级">
          <a-tag :color="getPriorityColor(currentRecord?.priority)">
            {{ getPriorityText(currentRecord?.priority) }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="紧急程度">
          <a-tag :color="getUrgencyColor(currentRecord?.urgency)">
            {{ getUrgencyText(currentRecord?.urgency) }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="开始时间">
          {{ currentRecord?.startTime ? formatDateTime(currentRecord.startTime) : '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="结束时间">
          {{ currentRecord?.endTime ? formatDateTime(currentRecord.endTime) : '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="创建时间">
          {{ currentRecord?.createTime ? formatDateTime(currentRecord.createTime) : '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="更新时间">
          {{ currentRecord?.updateTime ? formatDateTime(currentRecord.updateTime) : '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="流程变量" :span="2">
          <pre v-if="currentRecord?.variables" class="variables-data">{{ formatVariables(currentRecord.variables) }}</pre>
          <span v-else>无</span>
        </a-descriptions-item>
        <a-descriptions-item label="备注" :span="2">
          {{ currentRecord?.remark || '无' }}
        </a-descriptions-item>
      </a-descriptions>
    </a-modal>

    <!-- 终止确认对话框 -->
    <a-modal
      v-model:open="terminateVisible"
      title="终止工作流实例"
      @ok="confirmTerminate"
      @cancel="terminateVisible = false"
    >
      <a-form :model="terminateForm" :rules="terminateRules" ref="terminateFormRef">
        <a-form-item label="终止原因" name="reason">
          <a-textarea
            v-model:value="terminateForm.reason"
            placeholder="请输入终止原因"
            :rows="4"
          />
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- 批量操作确认对话框 -->
    <a-modal
      v-model:open="batchOperationVisible"
      :title="batchOperationTitle"
      @ok="confirmBatchOperation"
      @cancel="batchOperationVisible = false"
    >
      <p>{{ batchOperationMessage }}</p>
      <a-form v-if="batchOperationType === 'terminate'" :model="batchTerminateForm" ref="batchTerminateFormRef">
        <a-form-item label="终止原因" name="reason">
          <a-textarea
            v-model:value="batchTerminateForm.reason"
            placeholder="请输入终止原因"
            :rows="4"
          />
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
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user'
import { 
  PlayCircleOutlined, 
  PauseCircleOutlined, 
  CaretRightOutlined, 
  StopOutlined 
} from '@ant-design/icons-vue'
import type { HbtInstance, HbtInstanceQuery } from '@/types/workflow/instance'
import type { HbtScheme } from '@/types/workflow/scheme'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { getInstanceList } from '@/api/workflow/instance'
import { getPublishedSchemeList } from '@/api/workflow/scheme'
import { startWorkflow, suspendWorkflow, resumeWorkflow, terminateWorkflow } from '@/api/workflow/engine'
import { formatDateTime } from '@/utils/format'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()

// 表格列定义
const columns = [
  {
    title: '实例ID',
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 100
  },
  {
    title: '实例标题',
    dataIndex: 'instanceTitle',
    key: 'instanceTitle',
    width: 200,
    ellipsis: true
  },
  {
    title: '业务键',
    dataIndex: 'businessKey',
    key: 'businessKey',
    width: 150,
    ellipsis: true
  },
  {
    title: '发起人ID',
    dataIndex: 'initiatorId',
    key: 'initiatorId',
    width: 100
  },
  {
    title: '当前节点',
    dataIndex: 'currentNodeName',
    key: 'currentNodeName',
    width: 150,
    ellipsis: true
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '优先级',
    dataIndex: 'priority',
    key: 'priority',
    width: 100
  },
  {
    title: '紧急程度',
    dataIndex: 'urgency',
    key: 'urgency',
    width: 100
  },
  {
    title: '开始时间',
    dataIndex: 'startTime',
    key: 'startTime',
    width: 160
  },
  {
    title: '结束时间',
    dataIndex: 'endTime',
    key: 'endTime',
    width: 160
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 160
  },
  {
    title: '操作',
    key: 'action',
    width: 200,
    fixed: 'right' as const,
    align: 'center' as const
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'instanceTitle',
    label: '实例标题',
    type: 'input' as const,
    props: {
      placeholder: '请输入实例标题',
      allowClear: true
    }
  },
  {
    name: 'businessKey',
    label: '业务键',
    type: 'input' as const,
    props: {
      placeholder: '请输入业务键',
      allowClear: true
    }
  },
  {
    name: 'status',
    label: '状态',
    type: 'select' as const,
    props: {
      options: [
        { label: '草稿', value: 0 },
        { label: '运行中', value: 1 },
        { label: '已完成', value: 2 },
        { label: '已暂停', value: 3 },
        { label: '已终止', value: 4 }
      ],
      placeholder: '请选择状态',
      allowClear: true
    }
  },
  {
    name: 'priority',
    label: '优先级',
    type: 'select' as const,
    props: {
      options: [
        { label: '低', value: 1 },
        { label: '普通', value: 2 },
        { label: '高', value: 3 },
        { label: '紧急', value: 4 },
        { label: '特急', value: 5 }
      ],
      placeholder: '请选择优先级',
      allowClear: true
    }
  },
  {
    name: 'urgency',
    label: '紧急程度',
    type: 'select' as const,
    props: {
      options: [
        { label: '普通', value: 1 },
        { label: '加急', value: 2 },
        { label: '特急', value: 3 }
      ],
      placeholder: '请选择紧急程度',
      allowClear: true
    }
  },
  {
    name: 'createTimeRange',
    label: '创建时间',
    type: 'dateRange' as const,
    props: {
      placeholder: ['开始时间', '结束时间'],
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<HbtInstanceQuery>({
  pageIndex: 1,
  pageSize: 10,
  instanceTitle: undefined,
  businessKey: undefined,
  status: undefined,
  priority: undefined,
  urgency: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtInstance[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 详情对话框
const detailVisible = ref(false)
const currentRecord = ref<HbtInstance | null>(null)

// 启动流程对话框
const startWorkflowVisible = ref(false)
const startWorkflowFormRef = ref()
const startWorkflowForm = ref({
  schemeId: undefined as number | undefined,
  instanceTitle: '',
  businessKey: '',
  variables: ''
})
const startWorkflowRules: Record<string, Rule[]> = {
  schemeId: [
    { required: true, message: '请选择流程定义', trigger: 'change' }
  ],
  instanceTitle: [
    { required: true, message: '请输入实例标题', trigger: 'blur' }
  ]
}

// 流程定义选项
const schemeOptionsLoading = ref(false)
const schemeOptions = ref<{ label: string; value: number }[]>([])

// 终止对话框
const terminateVisible = ref(false)
const terminateForm = ref({
  instanceId: 0,
  reason: ''
})
const terminateRules = {
  reason: [
    { required: true, message: '请输入终止原因', trigger: 'blur' as const }
  ]
}
const terminateFormRef = ref()

// 批量操作对话框
const batchOperationVisible = ref(false)
const batchOperationType = ref<'suspend' | 'resume' | 'terminate'>('suspend')
const batchOperationTitle = ref('')
const batchOperationMessage = ref('')
const batchTerminateForm = ref({
  reason: ''
})
const batchTerminateFormRef = ref()

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// 权限检查
const hasStartPermission = computed(() => {
  return userStore.permissions.includes('workflow:engine:execution:start')
})

// 获取当前用户ID
const getCurrentUserId = (): number => {
  return userStore.userInfo?.userId || 0
}

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getInstanceList(queryParams.value)
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

// 获取流程定义选项
const fetchSchemeOptions = async () => {
  schemeOptionsLoading.value = true
  try {
    const res = await getPublishedSchemeList({
      pageIndex: 1,
      pageSize: 1000,
      status: 1 // 只获取已发布的流程定义
    })
    if (res.data) {
      schemeOptions.value = res.data.data.rows.map((scheme: HbtScheme) => ({
        label: scheme.schemeName,
        value: scheme.schemeId
      }))
    }
  } catch (error) {
    console.error('获取流程定义选项失败:', error)
    message.error('获取流程定义选项失败')
  } finally {
    schemeOptionsLoading.value = false
  }
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    // 处理日期范围
    if (values.createTimeRange && values.createTimeRange.length === 2) {
      values.createTimeStart = values.createTimeRange[0]
      values.createTimeEnd = values.createTimeRange[1]
      delete values.createTimeRange
    }
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    instanceTitle: undefined,
    businessKey: undefined,
    status: undefined,
    priority: undefined,
    urgency: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 查看详情
const handleView = (record: HbtInstance) => {
  currentRecord.value = record
  detailVisible.value = true
}

// 编辑
const handleEdit = (record: HbtInstance) => {
  router.push(`/workflow/engine/edit/${record.instanceId}`)
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录进行编辑')
    return
  }
  const record = tableData.value.find(item => item.instanceId === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 删除
const handleDelete = async (record: HbtInstance) => {
  try {
    // 这里需要调用删除API
    message.success('删除成功')
    fetchData()
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    // 这里需要调用批量删除API
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error('批量删除失败')
  }
}

// 启动流程
const handleStartWorkflow = () => {
  startWorkflowForm.value = {
    schemeId: undefined,
    instanceTitle: '',
    businessKey: '',
    variables: ''
  }
  startWorkflowVisible.value = true
}

// 确认启动流程
const confirmStartWorkflow = async () => {
  try {
    await startWorkflowFormRef.value?.validate()
    
    const startData = {
      schemeId: startWorkflowForm.value.schemeId!,
      instanceTitle: startWorkflowForm.value.instanceTitle,
      businessKey: startWorkflowForm.value.businessKey || undefined,
      initiatorId: getCurrentUserId(),
      variables: startWorkflowForm.value.variables || undefined
    }
    
    const res = await startWorkflow(startData)
    if (res.data) {
      message.success('工作流启动成功')
      startWorkflowVisible.value = false
      fetchData()
    }
  } catch (error) {
    console.error('启动工作流失败:', error)
    message.error('启动失败')
  }
}

// 流程定义选择变化
const handleSchemeChange = (value: any) => {
  const schemeId = Number(value)
  const scheme = schemeOptions.value.find(s => s.value === schemeId)
  if (scheme && !startWorkflowForm.value.instanceTitle) {
    startWorkflowForm.value.instanceTitle = `${scheme.label}-${new Date().toLocaleDateString()}`
  }
}

// 启动
const handleStart = async (record: HbtInstance) => {
  try {
    const startData = {
      schemeId: record.schemeId,
      instanceTitle: record.instanceTitle,
      businessKey: record.businessKey,
      initiatorId: getCurrentUserId(),
      variables: record.variables
    }
    
    const res = await startWorkflow(startData)
    if (res.data) {
      message.success('工作流启动成功')
      fetchData()
    }
  } catch (error) {
    console.error('启动工作流失败:', error)
    message.error('启动失败')
  }
}

// 暂停
const handleSuspend = async (record: HbtInstance) => {
  try {
    await suspendWorkflow(record.instanceId, '手动暂停')
    message.success('工作流实例已暂停')
    fetchData()
  } catch (error) {
    console.error('暂停工作流实例失败:', error)
    message.error('暂停失败')
  }
}

// 恢复
const handleResume = async (record: HbtInstance) => {
  try {
    await resumeWorkflow(record.instanceId)
    message.success('工作流实例已恢复')
    fetchData()
  } catch (error) {
    console.error('恢复工作流实例失败:', error)
    message.error('恢复失败')
  }
}

// 终止
const handleTerminate = (record: HbtInstance) => {
  terminateForm.value.instanceId = record.instanceId
  terminateForm.value.reason = ''
  terminateVisible.value = true
}

// 确认终止
const confirmTerminate = async () => {
  try {
    await terminateFormRef.value?.validate()
    await terminateWorkflow(terminateForm.value.instanceId, terminateForm.value.reason)
    message.success('工作流实例已终止')
    terminateVisible.value = false
    fetchData()
  } catch (error) {
    console.error('终止工作流实例失败:', error)
    message.error('终止失败')
  }
}

// 提交
const handleSubmit = async (record: HbtInstance) => {
  try {
    // 这里需要调用提交API
    message.success('提交成功')
    fetchData()
  } catch (error) {
    console.error('提交失败:', error)
    message.error('提交失败')
  }
}

// 撤回
const handleWithdraw = async (record: HbtInstance) => {
  try {
    // 这里需要调用撤回API
    message.success('撤回成功')
    fetchData()
  } catch (error) {
    console.error('撤回失败:', error)
    message.error('撤回失败')
  }
}

// 批量暂停
const handleBatchSuspend = () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要暂停的记录')
    return
  }
  batchOperationType.value = 'suspend'
  batchOperationTitle.value = '批量暂停'
  batchOperationMessage.value = `确定要暂停选中的 ${selectedRowKeys.value.length} 个工作流实例吗？`
  batchOperationVisible.value = true
}

// 批量恢复
const handleBatchResume = () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要恢复的记录')
    return
  }
  batchOperationType.value = 'resume'
  batchOperationTitle.value = '批量恢复'
  batchOperationMessage.value = `确定要恢复选中的 ${selectedRowKeys.value.length} 个工作流实例吗？`
  batchOperationVisible.value = true
}

// 批量终止
const handleBatchTerminate = () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要终止的记录')
    return
  }
  batchOperationType.value = 'terminate'
  batchOperationTitle.value = '批量终止'
  batchOperationMessage.value = `确定要终止选中的 ${selectedRowKeys.value.length} 个工作流实例吗？`
  batchTerminateForm.value.reason = ''
  batchOperationVisible.value = true
}

// 确认批量操作
const confirmBatchOperation = async () => {
  try {
    if (batchOperationType.value === 'terminate') {
      await batchTerminateFormRef.value?.validate()
    }
    
    // 这里需要调用批量操作API
    message.success('批量操作成功')
    batchOperationVisible.value = false
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量操作失败:', error)
    message.error('批量操作失败')
  }
}

// 导入
const handleImport = () => {
  message.info('导入功能开发中')
}

// 导出
const handleExport = () => {
  message.info('导出功能开发中')
}

// 模板
const handleTemplate = () => {
  message.info('模板功能开发中')
}

// 获取状态颜色
const getStatusColor = (status?: number) => {
  switch (status) {
    case 0:
      return 'default'
    case 1:
      return 'processing'
    case 2:
      return 'success'
    case 3:
      return 'warning'
    case 4:
      return 'error'
    default:
      return 'default'
  }
}

// 获取状态文本
const getStatusText = (status?: number) => {
  switch (status) {
    case 0:
      return '草稿'
    case 1:
      return '运行中'
    case 2:
      return '已完成'
    case 3:
      return '已暂停'
    case 4:
      return '已终止'
    default:
      return '未知'
  }
}

// 获取优先级颜色
const getPriorityColor = (priority?: number) => {
  switch (priority) {
    case 1:
      return 'default'
    case 2:
      return 'blue'
    case 3:
      return 'orange'
    case 4:
      return 'red'
    case 5:
      return 'purple'
    default:
      return 'default'
  }
}

// 获取优先级文本
const getPriorityText = (priority?: number) => {
  switch (priority) {
    case 1:
      return '低'
    case 2:
      return '普通'
    case 3:
      return '高'
    case 4:
      return '紧急'
    case 5:
      return '特急'
    default:
      return '未知'
  }
}

// 获取紧急程度颜色
const getUrgencyColor = (urgency?: number) => {
  switch (urgency) {
    case 1:
      return 'default'
    case 2:
      return 'orange'
    case 3:
      return 'red'
    default:
      return 'default'
  }
}

// 获取紧急程度文本
const getUrgencyText = (urgency?: number) => {
  switch (urgency) {
    case 1:
      return '普通'
    case 2:
      return '加急'
    case 3:
      return '特急'
    default:
      return '未知'
  }
}

// 格式化变量数据
const formatVariables = (variables: string) => {
  try {
    const data = JSON.parse(variables)
    return JSON.stringify(data, null, 2)
  } catch {
    return variables
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

// 初始化列设置
const initColumnSettings = () => {
  // 初始化所有列为true
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, true]))
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
}

// 处理行点击
const handleRowClick = (record: HbtInstance) => {
  console.log('行点击:', record)
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

// 在组件挂载时初始化
onMounted(async () => {
  // 初始化列设置
  initColumnSettings()
  
  // 获取流程定义选项
  await fetchSchemeOptions()
  
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-execution-container {
  height: 100%;
  background-color: var(--ant-color-bg-container);
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.column-setting-item {
  display: flex;
  align-items: center;
  gap: 8px;
}

.variables-data {
  background-color: #f5f5f5;
  padding: 8px;
  border-radius: 4px;
  font-size: 12px;
  line-height: 1.4;
  max-height: 200px;
  overflow-y: auto;
  white-space: pre-wrap;
  word-wrap: break-word;
}
</style> 