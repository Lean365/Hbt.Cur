<template>
  <div class="exception-log-container">
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
      :delete-permission="['audit:exceptionlog:delete']"
      :export-permission="['audit:exceptionlog:export']"
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
      :row-key="(record: HbtExceptionLogDto) => record.id"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 异常类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'exceptionType'">
          <a-tag color="error">{{ record.exceptionType }}</a-tag>
        </template>
        <template v-if="column.key === 'action'">
          <a @click="handleViewDetail(record)">详情</a>
        </template>
      </template>
    </hbt-table>

    <!-- 异常详情对话框 -->
    <a-modal
      v-model:visible="detailVisible"
      title="异常详情"
      width="800px"
      :footer="null"
    >
      <a-descriptions bordered>
        <a-descriptions-item label="异常类型" :span="3">
          {{ currentException?.exceptionType }}
        </a-descriptions-item>
        <a-descriptions-item label="异常消息" :span="3">
          {{ currentException?.exceptionMessage }}
        </a-descriptions-item>
        <a-descriptions-item label="堆栈跟踪" :span="3">
          <pre>{{ currentException?.stackTrace }}</pre>
        </a-descriptions-item>
        <a-descriptions-item label="请求方法" :span="3">
          {{ currentException?.method }}
        </a-descriptions-item>
        <a-descriptions-item label="请求参数" :span="3">
          {{ currentException?.parameters }}
        </a-descriptions-item>
        <a-descriptions-item label="操作人员" :span="1">
          {{ currentException?.userName }}
        </a-descriptions-item>
        <a-descriptions-item label="IP地址" :span="1">
          {{ currentException?.ipAddress }}
        </a-descriptions-item>
        <a-descriptions-item label="操作时间" :span="1">
          {{ currentException?.createTime }}
        </a-descriptions-item>
      </a-descriptions>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtExceptionLogDto, HbtExceptionLogQueryDto } from '@/types/audit/exceptionLog'
import { getExceptionLogs, exportExceptionLogs, clearExceptionLogs } from '@/api/audit/exceptionLog'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: '操作人员',
    type: 'input',
    placeholder: '请输入操作人员'
  },
  {
    name: 'method',
    label: '请求方法',
    type: 'input',
    placeholder: '请输入请求方法'
  },
  {
    name: 'exceptionType',
    label: '异常类型',
    type: 'input',
    placeholder: '请输入异常类型'
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
    title: '操作人员',
    dataIndex: 'userName',
    key: 'userName',
    width: 120
  },
  {
    title: '异常类型',
    dataIndex: 'exceptionType',
    key: 'exceptionType',
    width: 200,
    ellipsis: true
  },
  {
    title: '异常消息',
    dataIndex: 'exceptionMessage',
    key: 'exceptionMessage',
    width: 300,
    ellipsis: true
  },
  {
    title: '请求方法',
    dataIndex: 'method',
    key: 'method',
    width: 150,
    ellipsis: true
  },
  {
    title: 'IP地址',
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 130
  },
  {
    title: '操作时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 80,
    fixed: 'right',
    slots: {
      customRender: 'action'
    }
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<HbtExceptionLogDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtExceptionLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  userName: undefined,
  method: undefined,
  exceptionType: undefined,
  startTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const currentException = ref<HbtExceptionLogDto>()

/** 查看异常详情 */
const handleViewDetail = (record: HbtExceptionLogDto) => {
  currentException.value = record
  detailVisible.value = true
}

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getExceptionLogs(queryParams)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取异常日志失败:', error)
    message.error('获取异常日志失败')
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
  queryParams.method = undefined
  queryParams.exceptionType = undefined
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

/** 导出异常日志 */
const handleExport = async () => {
  try {
    await exportExceptionLogs(queryParams, '异常日志')
    message.success('导出成功')
  } catch (error) {
    console.error('导出异常日志失败:', error)
    message.error('导出异常日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearExceptionLogs()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空异常日志失败:', error)
    message.error('清空异常日志失败')
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
.exception-log-container {
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