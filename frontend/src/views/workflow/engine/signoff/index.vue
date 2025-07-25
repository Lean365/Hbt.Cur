<template>
  <div class="workflow-signoff-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-refresh="true"
      :show-column-setting="true"
      :show-export="true"
      :export-permission="['workflow:manage:instance:export']"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @export="handleExport"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    >
    </hbt-toolbar>

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="visibleColumns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: HbtInstance) => String(record.instanceId)"
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

        <!-- 等待时长列 -->
        <template v-if="column.dataIndex === 'waitTime'">
          {{ getWaitTime(record.startTime) }}
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :show-approve="true"
            :show-edit="true"
            :show-suspend="true"
            :show-resume="record.status === 3"
            :show-terminate="true"
            :view-permission="['workflow:manage:instance:query']"
            :approve-permission="['workflow:engine:signoff:approve']"
            :edit-permission="['workflow:manage:instance:edit']"
            :suspend-permission="['workflow:manage:instance:suspend']"
            :resume-permission="['workflow:manage:instance:resume']"
            :terminate-permission="['workflow:manage:instance:terminate']"
            size="small"
            @view="handleView"
            @approve="handleApprove"
            @edit="handleEdit"
            @suspend="handleSuspend"
            @resume="handleResume"
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

    <!-- 审批对话框 -->
    <a-modal
      v-model:open="approveVisible"
      title="审批操作"
      width="600px"
      @ok="confirmApprove"
      @cancel="approveVisible = false"
    >
      <a-form :model="approveForm" :rules="approveRules" ref="approveFormRef" layout="vertical">
        <a-form-item label="审批意见" name="operOpinion">
          <a-textarea
            v-model:value="approveForm.operOpinion"
            :rows="4"
            placeholder="请输入审批意见"
          />
        </a-form-item>

        <a-form-item label="操作类型" name="operType">
          <a-radio-group v-model:value="approveForm.operType">
            <a-radio :value="1">
              <CheckCircleOutlined style="color: #52c41a" />
              同意
            </a-radio>
            <a-radio :value="2">
              <CloseCircleOutlined style="color: #ff4d4f" />
              拒绝
            </a-radio>
            <a-radio :value="3">
              <RollbackOutlined style="color: #faad14" />
              退回
            </a-radio>
            <a-radio :value="4">
              <SwapOutlined style="color: #1890ff" />
              转办
            </a-radio>
            <a-radio :value="5">
              <UserSwitchOutlined style="color: #722ed1" />
              委托
            </a-radio>
          </a-radio-group>
        </a-form-item>

        <!-- 转办/委托目标用户 -->
        <a-form-item
          v-if="approveForm.operType === 4 || approveForm.operType === 5"
          label="目标用户"
          name="targetUserId"
        >
          <a-select
            v-model:value="approveForm.targetUserId"
            placeholder="请选择目标用户"
            show-search
            :filter-option="filterUserOption"
          >
            <a-select-option
              v-for="user in userList"
              :key="user.userId"
              :value="user.userId"
            >
              {{ user.userName }} ({{ user.deptName }})
            </a-select-option>
          </a-select>
        </a-form-item>

        <a-form-item label="审批数据" name="operData">
          <a-textarea
            v-model:value="approveForm.operData"
            :rows="3"
            placeholder="请输入审批数据（JSON格式）"
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
import type { HbtInstance, HbtInstanceQuery } from '@/types/workflow/instance'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import {
  CheckCircleOutlined,
  CloseCircleOutlined,
  RollbackOutlined,
  SwapOutlined,
  UserSwitchOutlined
} from '@ant-design/icons-vue'
import { getInstanceList } from '@/api/workflow/instance'
import { approveWorkflow, suspendWorkflow, resumeWorkflow } from '@/api/workflow/engine'
import { formatDateTime } from '@/utils/format'

const { t } = useI18n()
const router = useRouter()

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
    title: '等待时长',
    dataIndex: 'waitTime',
    key: 'waitTime',
    width: 120
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
    width: 300,
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
const showSearch = ref(true)

// 详情对话框
const detailVisible = ref(false)
const currentRecord = ref<HbtInstance | null>(null)

// 审批对话框
const approveVisible = ref(false)
const approveFormRef = ref<FormInstance>()
const approveForm = ref({
  instanceId: 0,
  nodeId: '',
  operOpinion: '',
  operType: 1,
  targetUserId: undefined,
  operData: ''
})
const approveRules = {
  operOpinion: [
    { required: true, message: '请输入审批意见', trigger: 'blur' as const }
  ],
  operType: [
    { required: true, message: '请选择操作类型', trigger: 'change' as const }
  ],
  targetUserId: [
    { 
      required: true, 
      message: '请选择目标用户', 
      trigger: 'change' as const,
      validator: (rule: any, value: any) => {
        if ((approveForm.value.operType === 4 || approveForm.value.operType === 5) && !value) {
          return Promise.reject('请选择目标用户')
        }
        return Promise.resolve()
      }
    }
  ]
}

// 用户列表（模拟数据）
const userList = ref([
  { userId: 1, userName: '张三', deptName: '技术部' },
  { userId: 2, userName: '李四', deptName: '产品部' },
  { userId: 3, userName: '王五', deptName: '运营部' }
])

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    // 只获取运行中的实例（待审批）
    const params = { ...queryParams.value, status: 1 }
    const res = await getInstanceList(params)
    if (res.data) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
    }
  } catch (error) {
    console.error('获取待审批任务列表失败:', error)
    message.error('获取待审批任务列表失败')
  } finally {
    loading.value = false
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

// 审批
const handleApprove = (record: HbtInstance) => {
  approveForm.value = {
    instanceId: record.instanceId,
    nodeId: record.currentNodeId || '',
    operOpinion: '',
    operType: 1,
    targetUserId: undefined,
    operData: ''
  }
  approveVisible.value = true
}

// 确认审批
const confirmApprove = async () => {
  try {
    await approveFormRef.value?.validate()
    
    const result = await approveWorkflow(approveForm.value)
    if (result.data.code === 200) {
      message.success('审批操作成功')
      approveVisible.value = false
      fetchData()
    } else {
      message.error(result.data.msg || '审批失败')
    }
  } catch (error) {
    console.error('审批失败:', error)
    message.error('审批失败')
  }
}

// 编辑
const handleEdit = (record: HbtInstance) => {
  router.push(`/workflow/engine/edit/${record.instanceId}`)
}

// 暂停
const handleSuspend = async (record: HbtInstance) => {
  try {
    await suspendWorkflow(record.instanceId, '审批页面手动暂停')
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
  message.info('终止功能待实现')
  console.log('终止工作流实例:', record)
}

// 导出数据
const handleExport = () => {
  message.info('导出功能待实现')
  console.log('导出待审批任务数据')
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

// 获取等待时长
const getWaitTime = (startTime?: string) => {
  if (!startTime) return '0分钟'
  const start = new Date(startTime)
  const now = new Date()
  const diff = now.getTime() - start.getTime()
  const minutes = Math.floor(diff / (1000 * 60))
  const hours = Math.floor(minutes / 60)
  const days = Math.floor(hours / 24)
  
  if (days > 0) return `${days}天${hours % 24}小时`
  if (hours > 0) return `${hours}小时${minutes % 60}分钟`
  return `${minutes}分钟`
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

// 用户筛选
const filterUserOption = (input: string, option: any) => {
  return option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
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
onMounted(() => {
  initColumnSettings()
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-signoff-container {
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