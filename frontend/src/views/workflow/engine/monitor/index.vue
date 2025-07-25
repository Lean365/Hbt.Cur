<template>
  <div class="workflow-monitor-container">
    <!-- 顶部统计卡片 -->
    <div class="statistics-section">
      <a-row :gutter="16">
        <a-col :span="6">
          <a-card class="stat-card">
            <div class="stat-content">
              <div class="stat-icon running">
                <PlayCircleOutlined />
              </div>
              <div class="stat-info">
                <div class="stat-number">{{ statistics.running }}</div>
                <div class="stat-label">运行中</div>
              </div>
            </div>
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card class="stat-card">
            <div class="stat-content">
              <div class="stat-icon completed">
                <CheckCircleOutlined />
              </div>
              <div class="stat-info">
                <div class="stat-number">{{ statistics.completed }}</div>
                <div class="stat-label">已完成</div>
              </div>
            </div>
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card class="stat-card">
            <div class="stat-content">
              <div class="stat-icon suspended">
                <PauseCircleOutlined />
              </div>
              <div class="stat-info">
                <div class="stat-number">{{ statistics.suspended }}</div>
                <div class="stat-label">已暂停</div>
              </div>
            </div>
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card class="stat-card">
            <div class="stat-content">
              <div class="stat-icon terminated">
                <StopOutlined />
              </div>
              <div class="stat-info">
                <div class="stat-number">{{ statistics.terminated }}</div>
                <div class="stat-label">已终止</div>
              </div>
            </div>
          </a-card>
        </a-col>
      </a-row>
    </div>

    <!-- 主要内容区域 -->
    <div class="monitor-content">
      <a-row :gutter="24">
        <!-- 左侧表格监控 -->
        <a-col :span="16">
          <a-card title="流程监控" class="monitor-card">
            <template #extra>
              <a-space>
                <a-button size="small" @click="handleRefresh">
                  <template #icon><ReloadOutlined /></template>
                  刷新
                </a-button>
                <a-button size="small" @click="handleAutoRefresh">
                  <template #icon><SyncOutlined /></template>
                  {{ autoRefresh ? '停止自动刷新' : '自动刷新' }}
                </a-button>
              </a-space>
            </template>
            
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

                <!-- 进度列 -->
                <template v-if="column.dataIndex === 'progress'">
                  <a-progress
                    :percent="getProgress(record)"
                    :status="getProgressStatus(record.status)"
                    size="small"
                  />
                </template>

                <!-- 运行时长列 -->
                <template v-if="column.dataIndex === 'duration'">
                  {{ getDuration(record.startTime) }}
                </template>

                <!-- 操作列 -->
                <template v-if="column.key === 'action'">
                  <hbt-operation
                    :record="record"
                    :show-view="true"
                    :show-edit="record.status === 1"
                    :show-suspend="record.status === 1"
                    :show-resume="record.status === 3"
                    :show-terminate="record.status === 1 || record.status === 3"
                    :show-delete="record.status === 4"
                    :view-permission="['workflow:manage:instance:query']"
                    :edit-permission="['workflow:manage:instance:edit']"
                    :suspend-permission="['workflow:manage:instance:suspend']"
                    :resume-permission="['workflow:manage:instance:resume']"
                    :terminate-permission="['workflow:manage:instance:terminate']"
                    :delete-permission="['workflow:manage:instance:delete']"
                    size="small"
                    @view="handleView"
                    @edit="handleEdit"
                    @suspend="handleSuspend"
                    @resume="handleResume"
                    @terminate="handleTerminate"
                    @delete="handleDelete"
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
          </a-card>
        </a-col>

        <!-- 右侧统计图表 -->
        <a-col :span="8">
          <!-- 状态分布饼图 -->
          <a-card title="状态分布" class="chart-card">
            <div class="chart-container">
              <div ref="statusChartRef" class="chart"></div>
            </div>
          </a-card>

          <!-- 异常流程 -->
          <a-card title="异常流程" class="chart-card">
            <div class="exception-list">
              <div
                v-for="exception in exceptionList"
                :key="exception.instanceId"
                class="exception-item"
              >
                <div class="exception-header">
                  <span class="exception-title">{{ exception.instanceTitle }}</span>
                  <a-tag color="red">异常</a-tag>
                </div>
                <div class="exception-info">
                  <div>节点：{{ exception.currentNodeName || '-' }}</div>
                  <div>异常时间：{{ exception.createTime || '-' }}</div>
                  <div>异常原因：{{ exception.remark || '无' }}</div>
                </div>
                <div class="exception-actions">
                  <a-button size="small" type="primary" @click="handleFixException(exception)">
                    处理异常
                  </a-button>
                </div>
              </div>
            </div>
          </a-card>
        </a-col>
      </a-row>
    </div>

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
import { ref, computed, onMounted, onUnmounted, h } from 'vue'
import { useRouter } from 'vue-router'
import { useDictStore } from '@/stores/dict'
import {
  PlayCircleOutlined,
  CheckCircleOutlined,
  PauseCircleOutlined,
  StopOutlined,
  ReloadOutlined,
  SyncOutlined
} from '@ant-design/icons-vue'
import type { HbtInstance, HbtInstanceQuery } from '@/types/workflow/instance'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getInstanceList } from '@/api/workflow/instance'
import { suspendWorkflow, resumeWorkflow, terminateWorkflow } from '@/api/workflow/engine'
import { formatDateTime } from '@/utils/format'

const { t } = useI18n()
const router = useRouter()
const dictStore = useDictStore()

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
    title: '进度',
    dataIndex: 'progress',
    key: 'progress',
    width: 120
  },
  {
    title: '运行时长',
    dataIndex: 'duration',
    key: 'duration',
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
    width: 250,
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

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// 统计相关
const statistics = ref({
  running: 0,
  completed: 0,
  suspended: 0,
  terminated: 0
})

// 异常流程
const exceptionList = ref<HbtInstance[]>([])

// 自动刷新相关
const autoRefresh = ref(false)
const refreshTimer = ref<NodeJS.Timeout | null>(null)

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getInstanceList(queryParams.value)
    if (res.data) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
      updateStatistics()
      updateExceptionList()
    }
  } catch (error) {
    console.error('获取工作流实例列表失败:', error)
    message.error('获取工作流实例列表失败')
  } finally {
    loading.value = false
  }
}

// 更新统计数据
const updateStatistics = () => {
  const instances = tableData.value
  statistics.value.running = instances.filter(i => i.status === 1).length
  statistics.value.completed = instances.filter(i => i.status === 2).length
  statistics.value.suspended = instances.filter(i => i.status === 3).length
  statistics.value.terminated = instances.filter(i => i.status === 4).length
}

// 更新异常列表
const updateExceptionList = () => {
  // 简化的异常检测逻辑，实际应该根据业务规则判断
  exceptionList.value = tableData.value.filter(instance => 
    instance.status === 4 || // 已终止
    (instance.status === 1 && !instance.currentNodeName) // 运行中但没有当前节点
  ).slice(0, 5) // 只显示前5个异常
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

// 暂停
const handleSuspend = async (record: HbtInstance) => {
  try {
    await suspendWorkflow(record.instanceId, '监控页面手动暂停')
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

// 获取运行时长
const getDuration = (startTime?: string) => {
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

// 获取进度
const getProgress = (instance: HbtInstance) => {
  // 简化的进度计算，实际应该根据节点数量计算
  const statusProgress: Record<number, number> = {
    1: 50, // 运行中
    2: 100, // 已完成
    3: 30, // 已暂停
    4: 0 // 已终止
  }
  return statusProgress[instance.status || 1] || 0
}

// 获取进度状态
const getProgressStatus = (status?: number) => {
  if (status === 2) return 'success'
  if (status === 4) return 'exception'
  return 'active'
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

// 刷新
const handleRefresh = () => {
  fetchData()
}

// 自动刷新
const handleAutoRefresh = () => {
  autoRefresh.value = !autoRefresh.value
  if (autoRefresh.value) {
    refreshTimer.value = setInterval(() => {
      fetchData()
    }, 30000) // 30秒刷新一次
  } else if (refreshTimer.value) {
    clearInterval(refreshTimer.value)
    refreshTimer.value = null
  }
}

// 处理异常
const handleFixException = (exception: HbtInstance) => {
  message.info('异常处理功能待实现')
}

// 删除
const handleDelete = (record: HbtInstance) => {
  message.info('删除功能待实现')
  console.log('删除工作流实例:', record)
}

// 导出数据
const handleExport = () => {
  message.info('导出功能待实现')
  console.log('导出工作流实例数据')
}

// 在组件挂载时初始化
onMounted(async () => {
  // 加载字典数据
  await dictStore.loadDicts([
    'workflow_instance_status',
    'workflow_instance_priority',
    'workflow_instance_urgency'
  ])
  
  initColumnSettings()
  fetchData()
})

// 组件卸载时清理定时器
onUnmounted(() => {
  if (refreshTimer.value) {
    clearInterval(refreshTimer.value)
  }
})
</script>

<style lang="less" scoped>
.workflow-monitor-container {
  height: 100vh;
  display: flex;
  flex-direction: column;
  background-color: #f5f5f5;
}

.statistics-section {
  padding: 24px 24px 0 24px;

  .stat-card {
    .stat-content {
      display: flex;
      align-items: center;

      .stat-icon {
        width: 48px;
        height: 48px;
        border-radius: 8px;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-right: 16px;
        font-size: 24px;

        &.running {
          background-color: #e6f7ff;
          color: #1890ff;
        }

        &.completed {
          background-color: #f6ffed;
          color: #52c41a;
        }

        &.suspended {
          background-color: #fff7e6;
          color: #faad14;
        }

        &.terminated {
          background-color: #fff2f0;
          color: #ff4d4f;
        }
      }

      .stat-info {
        .stat-number {
          font-size: 24px;
          font-weight: 600;
          color: #333;
          line-height: 1;
        }

        .stat-label {
          font-size: 14px;
          color: #666;
          margin-top: 4px;
        }
      }
    }
  }
}

.monitor-content {
  flex: 1;
  padding: 24px;
  overflow-y: auto;
}

.monitor-card {
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
}

.chart-card {
  margin-bottom: 16px;

  .chart-container {
    height: 200px;
    display: flex;
    align-items: center;
    justify-content: center;

    .chart {
      width: 100%;
      height: 100%;
    }
  }

  .exception-list {
    .exception-item {
      border: 1px solid #ffccc7;
      border-radius: 4px;
      padding: 12px;
      margin-bottom: 8px;
      background-color: #fff2f0;

      .exception-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 8px;

        .exception-title {
          font-weight: 500;
          color: #333;
        }
      }

      .exception-info {
        font-size: 12px;
        color: #666;
        margin-bottom: 8px;

        div {
          margin-bottom: 2px;
        }
      }

      .exception-actions {
        text-align: right;
      }
    }
  }
}
</style> 