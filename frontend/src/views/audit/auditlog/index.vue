<template>
  <div class="audit-log-container">
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
      :delete-permission="['audit:auditlog:delete']"
      :export-permission="['audit:auditlog:export']"
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
      :row-key="(record: HbtAuditLogDto) => record.id"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 日志级别列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'logLevel'">
          <a-tag :color="getLogLevelColor(record.logLevel)">
            {{ getLogLevelText(record.logLevel) }}
          </a-tag>
        </template>
      </template>
    </hbt-table>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtAuditLogDto, HbtAuditLogQueryDto } from '@/types/audit/auditLog'
import { getAuditLogs, exportAuditLogs, clearAuditLogs } from '@/api/audit/auditLog'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: '用户名称',
    type: 'input',
    placeholder: '请输入用户名称'
  },
  {
    name: 'module',
    label: '所属模块',
    type: 'input',
    placeholder: '请输入所属模块'
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
      { label: '信息', value: 0 },
      { label: '警告', value: 1 },
      { label: '错误', value: 2 },
      { label: '调试', value: 3 }
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
    title: '用户名称',
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    sorter: true
  },
  {
    title: '所属模块',
    dataIndex: 'module',
    key: 'module',
    width: 120,
    sorter: true
  },
  {
    title: '操作类型',
    dataIndex: 'operation',
    key: 'operation',
    width: 120,
    sorter: true
  },
  {
    title: '日志级别',
    dataIndex: 'logLevel',
    key: 'logLevel',
    width: 100,
    sorter: true
  },
  {
    title: '请求方法',
    dataIndex: 'method',
    key: 'method',
    width: 150,
    ellipsis: true,
    sorter: true
  },
  {
    title: '请求参数',
    dataIndex: 'parameters',
    key: 'parameters',
    width: 200,
    ellipsis: true
  },
  {
    title: '返回结果',
    dataIndex: 'result',
    key: 'result',
    width: 200,
    ellipsis: true
  },
  {
    title: '耗时(毫秒)',
    dataIndex: 'elapsed',
    key: 'elapsed',
    width: 100,
    sorter: true
  },
  {
    title: 'IP地址',
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 130,
    sorter: true
  },
  {
    title: '请求URL',
    dataIndex: 'requestUrl',
    key: 'requestUrl',
    width: 200,
    ellipsis: true
  },
  {
    title: '请求方法',
    dataIndex: 'requestMethod',
    key: 'requestMethod',
    width: 100,
    sorter: true
  },
  {
    title: '操作时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    sorter: true
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<HbtAuditLogDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtAuditLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  orderByColumn: undefined,
  orderType: undefined,
  userName: undefined,
  module: undefined,
  operation: undefined,
  logLevel: undefined,
  startTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

/** 获取日志级别颜色 */
const getLogLevelColor = (level: number) => {
  const colors: { [key: number]: string } = {
    0: 'blue',    // 信息
    1: 'orange',  // 警告
    2: 'red',     // 错误
    3: 'green'    // 调试
  }
  return colors[level] || 'default'
}

/** 获取日志级别文本 */
const getLogLevelText = (level: number) => {
  const texts: { [key: number]: string } = {
    0: '信息',
    1: '警告',
    2: '错误',
    3: '调试'
  }
  return texts[level] || '未知'
}

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getAuditLogs(queryParams)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
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
  queryParams.userName = undefined
  queryParams.module = undefined
  queryParams.operation = undefined
  queryParams.logLevel = undefined
  queryParams.startTime = undefined
  queryParams.endTime = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (
  pagination: TablePaginationConfig,
  filters: Record<string, (string | number)[] | null>,
  sorter: { field?: string; order?: 'ascend' | 'descend' }
) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  
  // 处理排序
  if (sorter.field) {
    queryParams.orderByColumn = sorter.field
    queryParams.orderType = sorter.order === 'ascend' ? 'asc' : 'desc'
  } else {
    queryParams.orderByColumn = undefined
    queryParams.orderType = undefined
  }
  
  fetchData()
}

/** 导出审计日志 */
const handleExport = async () => {
  try {
    await exportAuditLogs(queryParams, '审计日志')
    message.success('导出成功')
  } catch (error) {
    console.error('导出审计日志失败:', error)
    message.error('导出审计日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearAuditLogs()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空审计日志失败:', error)
    message.error('清空审计日志失败')
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
.audit-log-container {
  padding: 16px;
}
</style> 