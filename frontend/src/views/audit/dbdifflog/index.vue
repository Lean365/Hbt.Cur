<template>
  <div class="db-diff-log-container">
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
      :delete-permission="['audit:dbdifflog:delete']"
      :export-permission="['audit:dbdifflog:export']"
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
      :row-key="(record: HbtDbDiffLogDto) => record.id"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 变更类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'changeType'">
          <a-tag :color="getChangeTypeColor(record.changeType)">
            {{ record.changeType }}
          </a-tag>
        </template>
      </template>
    </hbt-table>

    <!-- 差异详情对话框 -->
    <a-modal
      v-model:visible="detailVisible"
      title="差异详情"
      width="800px"
      :footer="null"
    >
      <a-descriptions bordered>
        <a-descriptions-item label="表名" :span="3">
          {{ currentDiff?.tableName }}
        </a-descriptions-item>
        <a-descriptions-item label="变更类型" :span="3">
          {{ currentDiff?.changeType }}
        </a-descriptions-item>
        <a-descriptions-item label="列名" :span="3">
          {{ currentDiff?.columnName }}
        </a-descriptions-item>
        <a-descriptions-item label="原数据类型" :span="1">
          {{ currentDiff?.oldDataType }}
        </a-descriptions-item>
        <a-descriptions-item label="新数据类型" :span="1">
          {{ currentDiff?.newDataType }}
        </a-descriptions-item>
        <a-descriptions-item label="变更描述" :span="1">
          {{ currentDiff?.changeDescription }}
        </a-descriptions-item>
        <a-descriptions-item label="执行的SQL" :span="3">
          <pre>{{ currentDiff?.executeSql }}</pre>
        </a-descriptions-item>
        <a-descriptions-item label="SQL参数" :span="3">
          <pre>{{ currentDiff?.sqlParameters }}</pre>
        </a-descriptions-item>
        <a-descriptions-item label="变更前数据" :span="3">
          <pre>{{ currentDiff?.beforeData }}</pre>
        </a-descriptions-item>
        <a-descriptions-item label="变更后数据" :span="3">
          <pre>{{ currentDiff?.afterData }}</pre>
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
import type { HbtDbDiffLogDto, HbtDbDiffLogQueryDto } from '@/types/audit/dbDiffLog'
import { getDbDiffLogs, exportDbDiffLogs, clearDbDiffLogs } from '@/api/audit/dbDiffLog'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'tableName',
    label: '表名',
    type: 'input',
    placeholder: '请输入表名'
  },
  {
    name: 'changeType',
    label: '变更类型',
    type: 'select',
    placeholder: '请选择变更类型',
    options: [
      { label: '新增表', value: 'CREATE_TABLE' },
      { label: '删除表', value: 'DROP_TABLE' },
      { label: '新增字段', value: 'ADD_COLUMN' },
      { label: '修改字段', value: 'MODIFY_COLUMN' },
      { label: '删除字段', value: 'DROP_COLUMN' }
    ]
  },
  {
    name: 'columnName',
    label: '列名',
    type: 'input',
    placeholder: '请输入列名'
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
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName',
    width: 150,
    sorter: true
  },
  {
    title: '变更类型',
    dataIndex: 'changeType',
    key: 'changeType',
    width: 120,
    sorter: true
  },
  {
    title: '列名',
    dataIndex: 'columnName',
    key: 'columnName',
    width: 120,
    sorter: true
  },
  {
    title: '变更描述',
    dataIndex: 'changeDescription',
    key: 'changeDescription',
    width: 200,
    ellipsis: true
  },
  {
    title: '执行的SQL',
    dataIndex: 'executeSql',
    key: 'executeSql',
    width: 200,
    ellipsis: true
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
const tableData = ref<HbtDbDiffLogDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtDbDiffLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  orderByColumn: undefined,
  orderType: undefined,
  tableName: undefined,
  changeType: undefined,
  columnName: undefined,
  startTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const currentDiff = ref<HbtDbDiffLogDto | null>(null)

/** 获取变更类型颜色 */
const getChangeTypeColor = (type: string) => {
  const colors: { [key: string]: string } = {
    'CREATE_TABLE': 'green',
    'DROP_TABLE': 'red',
    'ADD_COLUMN': 'blue',
    'MODIFY_COLUMN': 'orange',
    'DROP_COLUMN': 'red'
  }
  return colors[type] || 'default'
}

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getDbDiffLogs(queryParams)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取数据库差异日志失败:', error)
    message.error('获取数据库差异日志失败')
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
  queryParams.tableName = undefined
  queryParams.changeType = undefined
  queryParams.columnName = undefined
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

/** 导出数据库差异日志 */
const handleExport = async () => {
  try {
    await exportDbDiffLogs(queryParams, '数据库差异日志')
    message.success('导出成功')
  } catch (error) {
    console.error('导出数据库差异日志失败:', error)
    message.error('导出数据库差异日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearDbDiffLogs()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空数据库差异日志失败:', error)
    message.error('清空数据库差异日志失败')
  }
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 查看详情 */
const handleViewDetail = (record: HbtDbDiffLogDto) => {
  currentDiff.value = record
  detailVisible.value = true
}

// 初始化加载数据
fetchData()
</script>

<style lang="less" scoped>
.db-diff-log-container {
  padding: 16px;
}
</style>
