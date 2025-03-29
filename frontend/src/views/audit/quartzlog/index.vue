<template>
  <div class="quartz-log-container">
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
      :show-refresh="true"
      :show-export="true"
      :show-delete="true"
      :delete-permission="['audit:quartzlog:delete']"
       :export-permission="['audit:quartzlog:export']"
      :disabled-delete="selectedRowKeys.length === 0"
      @refresh="fetchData"
      @export="handleExport"
      @delete="handleBatchDelete"
      @toggle-search="toggleSearch"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: HbtQuartzLogDto) => record.logId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 任务状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'status'">
          <a-tag :color="record.status === 1 ? 'success' : 'error'">
            {{ record.status === 1 ? '成功' : '失败' }}
          </a-tag>
        </template>
      </template>
    </hbt-table>

    <!-- 任务详情对话框 -->
    <a-modal
      v-model:visible="detailVisible"
      title="任务详情"
      width="800px"
      :footer="null"
    >
      <a-descriptions bordered>
        <a-descriptions-item label="任务名称" :span="3">
          {{ currentTask?.logTaskName }}
        </a-descriptions-item>
        <a-descriptions-item label="任务组" :span="3">
          {{ currentTask?.logGroupName }}
        </a-descriptions-item>
        <a-descriptions-item label="执行机器" :span="3">
          {{ currentTask?.logExecuteHost }}
        </a-descriptions-item>
        <a-descriptions-item label="执行参数" :span="3">
          {{ currentTask?.logExecuteParams }}
        </a-descriptions-item>
        <a-descriptions-item label="执行状态" :span="1">
          <a-tag :color="currentTask?.logStatus === 1 ? 'success' : 'error'">
            {{ currentTask?.logStatus === 1 ? '成功' : '失败' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="执行时间" :span="1">
          {{ currentTask?.logExecuteTime }}
        </a-descriptions-item>
        <a-descriptions-item label="执行耗时" :span="1">
          {{ currentTask?.logExecuteDuration }}ms
        </a-descriptions-item>
        <a-descriptions-item label="错误信息" :span="3">
          <pre v-if="currentTask?.logErrorInfo">{{ currentTask.logErrorInfo }}</pre>
          <span v-else>无</span>
        </a-descriptions-item>
      </a-descriptions>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, h } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtQuartzLogDto, HbtQuartzLogQueryDto } from '@/types/audit/quartzLog'
import { getQuartzLogs, exportQuartzLogs, clearQuartzLogs } from '@/api/audit/quartzLog'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'taskName',
    label: '任务名称',
    type: 'input',
    placeholder: '请输入任务名称'
  },
  {
    name: 'taskGroup',
    label: '任务组',
    type: 'input',
    placeholder: '请输入任务组'
  },
  {
    name: 'status',
    label: '执行状态',
    type: 'select',
    placeholder: '请选择执行状态',
    options: [
      { label: '成功', value: 1 },
      { label: '失败', value: 0 }
    ]
  },
  {
    name: 'startTime',
    label: '开始时间',
    type: 'date',
    placeholder: '请选择开始时间'
  },
  {
    name: 'endTime',
    label: '结束时间',
    type: 'date',
    placeholder: '请选择结束时间'
  }
]

// 表格列定义
const columns = [
  {
    title: '任务名称',
    dataIndex: 'taskName',
    key: 'taskName',
    width: 150
  },
  {
    title: '任务组',
    dataIndex: 'taskGroup',
    key: 'taskGroup',
    width: 120
  },
  {
    title: '调用目标',
    dataIndex: 'invokeTarget',
    key: 'invokeTarget',
    width: 200,
    ellipsis: true
  },
  {
    title: '任务参数',
    dataIndex: 'taskParams',
    key: 'taskParams',
    width: 200,
    ellipsis: true
  },
  {
    title: '执行状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '耗时(毫秒)',
    dataIndex: 'elapsed',
    key: 'elapsed',
    width: 100
  },
  {
    title: '执行时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 80,
    fixed: 'right',
    render: (_: any, record: HbtQuartzLogDto) => {
      return h('a', {
        onClick: () => handleViewDetail(record)
      }, '详情')
    }
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<HbtQuartzLogDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtQuartzLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  logTaskName: undefined,
  logGroupName: undefined,
  logStatus: undefined,
  beginTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const currentTask = ref<HbtQuartzLogDto>()

/** 查看任务详情 */
const handleViewDetail = (record: HbtQuartzLogDto) => {
  currentTask.value = record
  detailVisible.value = true
}

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getQuartzLogs(queryParams)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取任务日志失败:', error)
    message.error('获取任务日志失败')
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.logTaskName = undefined
  queryParams.logGroupName = undefined
  queryParams.logStatus = undefined
  queryParams.beginTime = undefined
  queryParams.endTime = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 导出任务日志 */
const handleExport = async () => {
  try {
    await exportQuartzLogs(queryParams, '任务日志')
    message.success('导出成功')
  } catch (error) {
    console.error('导出任务日志失败:', error)
    message.error('导出任务日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearQuartzLogs()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空任务日志失败:', error)
    message.error('清空任务日志失败')
  }
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 初始化加载数据
fetchData()
</script>

<style lang="less" scoped>
.quartz-log-container {
  padding: 16px;

  :deep(.ant-descriptions-item-content) {
    pre {
      margin: 0;
      padding: 8px;
      background-color: #f5f5f5;
      border-radius: 4px;
      max-height: 300px;
      overflow: auto;
      white-space: pre-wrap;
      word-wrap: break-word;
    }
  }
}
</style> 