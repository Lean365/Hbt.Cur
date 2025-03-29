<template>
  <hbt-modal
    v-model:visible="visible"
    title="我的任务日志"
    :width="1200"
    :footer="null"
  >
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
      :show-search="true"
      @refresh="fetchData"
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
      :scroll="{ y: 400 }"
      @change="handleTableChange"
    >
      <!-- 自定义列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'success'">
          <a-tag :color="record.success ? 'success' : 'error'">
            {{ record.success ? '成功' : '失败' }}
          </a-tag>
        </template>
        <template v-if="column.key === 'action'">
          <a @click="handleViewDetail(record)">详情</a>
        </template>
      </template>
    </hbt-table>

    <!-- 详情组件 -->
    <quartz-log-detail ref="detailRef" :quartz-info="currentQuartz" />
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, defineExpose } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtQuartzLogDto, HbtQuartzLogQueryDto } from '@/types/audit/quartzLog'
import { getQuartzLogs } from '@/api/audit/quartzLog'
import { useUserStore } from '@/store/modules/user'
import QuartzLogDetail from './QuartzLogDetail.vue'

const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'jobName',
    label: '任务名称',
    type: 'input',
    placeholder: '请输入任务名称'
  },
  {
    name: 'jobGroup',
    label: '任务组',
    type: 'input',
    placeholder: '请输入任务组'
  },
  {
    name: 'success',
    label: '执行状态',
    type: 'select',
    placeholder: '请选择执行状态',
    options: [
      { label: '成功', value: true },
      { label: '失败', value: false }
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
    dataIndex: 'jobName',
    key: 'jobName',
    width: 150
  },
  {
    title: '任务组',
    dataIndex: 'jobGroup',
    key: 'jobGroup',
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
    title: '执行状态',
    dataIndex: 'success',
    key: 'success',
    width: 100
  },
  {
    title: '执行时间',
    dataIndex: 'executeTime',
    key: 'executeTime',
    width: 100,
    customRender: ({ text }: { text: number }) => `${text}ms`
  },
  {
    title: '开始时间',
    dataIndex: 'startTime',
    key: 'startTime',
    width: 180
  },
  {
    title: '结束时间',
    dataIndex: 'endTime',
    key: 'endTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 80,
    fixed: 'right'
  }
]

// 状态定义
const visible = ref(false)
const loading = ref(false)
const tableData = ref<HbtQuartzLogDto[]>([])
const total = ref(0)
const showSearch = ref(true)
const detailRef = ref()
const currentQuartz = ref<HbtQuartzLogDto>()

const queryParams = reactive<HbtQuartzLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  orderByColumn: undefined,
  orderType: undefined,
  logTaskName: undefined,
  logGroupName: undefined,
  logStatus: undefined,
  beginTime: undefined,
  endTime: undefined
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getQuartzLogs(queryParams)
    tableData.value = res.rows
    total.value = res.totalNum
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

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 查看详情 */
const handleViewDetail = (record: HbtQuartzLogDto) => {
  currentQuartz.value = record
  detailRef.value?.open()
}

/** 打开弹窗 */
const open = () => {
  visible.value = true
  fetchData()
}

/** 关闭弹窗 */
const close = () => {
  visible.value = false
}

// 暴露方法给父组件
defineExpose({
  open,
  close
})
</script>

<style lang="less" scoped>
:deep(.ant-modal-body) {
  padding: 16px;
  max-height: 800px;
  overflow-y: auto;
}
</style> 