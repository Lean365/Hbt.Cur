<template>
  <hbt-modal
    v-model:visible="visible"
    title="我的审计日志"
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
      :row-key="(record: HbtAuditLogDto) => record.id"
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
    <audit-log-detail ref="detailRef" :audit-info="currentAudit" />
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, defineExpose } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtAuditLogDto, HbtAuditLogQueryDto } from '@/types/audit/auditLog'
import { getAuditLogs } from '@/api/audit/auditLog'
import { useUserStore } from '@/stores/user'
import AuditLogDetail from './AuditLogDetail.vue'

const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'module',
    label: '模块名称',
    type: 'input',
    placeholder: '请输入模块名称'
  },
  {
    name: 'operation',
    label: '操作类型',
    type: 'input',
    placeholder: '请输入操作类型'
  },
  {
    name: 'logLevel',
    label: '日志级别',
    type: 'select',
    placeholder: '请选择日志级别',
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
    title: '模块名称',
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 150
  },
  {
    title: '操作类型',
    dataIndex: 'operationType',
    key: 'operationType',
    width: 120
  },
  {
    title: '请求方法',
    dataIndex: 'requestMethod',
    key: 'requestMethod',
    width: 100
  },
  {
    title: 'IP地址',
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 130
  },
  {
    title: 'IP位置',
    dataIndex: 'ipLocation',
    key: 'ipLocation',
    width: 150
  },
  {
    title: '操作状态',
    dataIndex: 'success',
    key: 'success',
    width: 100
  },
  {
    title: '请求时间',
    dataIndex: 'requestTime',
    key: 'requestTime',
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
const tableData = ref<HbtAuditLogDto[]>([])
const total = ref(0)
const showSearch = ref(true)
const detailRef = ref()
const currentAudit = ref<HbtAuditLogDto>()

const queryParams = reactive<HbtAuditLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  orderByColumn: undefined,
  orderType: undefined,
  userName: userStore.user?.userName,
  module: undefined,
  operation: undefined,
  logLevel: undefined,
  startTime: undefined,
  endTime: undefined
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getAuditLogs(queryParams)
    tableData.value = res.data.rows
    total.value = res.data.totalNum
  } catch (error) {
    console.error('获取审计日志失败:', error)
    message.error('获取审计日志失败')
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
  queryParams.module = undefined
  queryParams.operation = undefined
  queryParams.logLevel = undefined
  queryParams.startTime = undefined
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
const handleViewDetail = (record: HbtAuditLogDto) => {
  currentAudit.value = record
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