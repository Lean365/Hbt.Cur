<template>
  <div class="workflow-todo-container">
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
      :export-permission="['workflow:manage:trans:export']"
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
      :row-key="(record: HbtInstanceTrans) => String(record.instanceTransId)"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 转化状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'transState'">
          <a-tag :color="record.transState === 0 ? 'orange' : record.transState === 1 ? 'green' : 'blue'">
            {{ record.transState === 0 ? '待处理' : record.transState === 1 ? '已处理' : '处理中' }}
          </a-tag>
        </template>

        <!-- 是否完成列 -->
        <template v-if="column.dataIndex === 'isFinish'">
          <a-tag :color="record.isFinish === 1 ? 'green' : 'red'">
            {{ record.isFinish === 1 ? '是' : '否' }}
          </a-tag>
        </template>

        <!-- 转化时间列 -->
        <template v-if="column.dataIndex === 'transTime'">
          {{ record.transTime ? formatDateTime(record.transTime) : '-' }}
        </template>

        <!-- 创建时间列 -->
        <template v-if="column.dataIndex === 'createTime'">
          {{ record.createTime ? formatDateTime(record.createTime) : '-' }}
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['workflow:manage:trans:query']"
            :show-process="record.transState === 0"
            :process-permission="['workflow:engine:signoff:approve']"
            size="small"
            @view="handleView"
            @process="handleProcess"
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
      title="实例详情"
      width="800px"
      :footer="null"
    >
      <a-descriptions :column="2" bordered>
        <a-descriptions-item label="流转ID">
          {{ currentRecord?.instanceTransId }}
        </a-descriptions-item>
        <a-descriptions-item label="实例ID">
          {{ currentRecord?.instanceId }}
        </a-descriptions-item>
        <a-descriptions-item label="开始节点">
          {{ currentRecord?.startNodeName || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="目标节点">
          {{ currentRecord?.toNodeName || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="转化状态">
          <a-tag :color="currentRecord?.transState === 0 ? 'orange' : currentRecord?.transState === 1 ? 'green' : 'blue'">
            {{ currentRecord?.transState === 0 ? '待处理' : currentRecord?.transState === 1 ? '已处理' : '处理中' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="是否完成">
          <a-tag :color="currentRecord?.isFinish === 1 ? 'green' : 'red'">
            {{ currentRecord?.isFinish === 1 ? '是' : '否' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="转化时间">
          {{ currentRecord?.transTime ? formatDateTime(currentRecord.transTime) : '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="创建时间">
          {{ currentRecord?.createTime ? formatDateTime(currentRecord.createTime) : '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="备注" :span="2">
          {{ currentRecord?.remark || '无' }}
        </a-descriptions-item>
      </a-descriptions>
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
import { useDictStore } from '@/stores/dict'
import type { HbtInstanceTrans, HbtInstanceTransQuery } from '@/types/workflow/trans'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getMyTodoList } from '@/api/workflow/trans'
import { formatDateTime } from '@/utils/format'

const { t } = useI18n()
const router = useRouter()

// 字典store
const dictStore = useDictStore()



// 表格列定义
const columns = [
  {
    title: '流转ID',
    dataIndex: 'instanceTransId',
    key: 'instanceTransId',
    width: 120,
    ellipsis: true
  },
  {
    title: '实例ID',
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 120,
    ellipsis: true
  },
  {
    title: '开始节点',
    dataIndex: 'startNodeName',
    key: 'startNodeName',
    width: 150,
    ellipsis: true
  },
  {
    title: '目标节点',
    dataIndex: 'toNodeName',
    key: 'toNodeName',
    width: 150,
    ellipsis: true
  },
  {
    title: '转化状态',
    dataIndex: 'transState',
    key: 'transState',
    width: 100,
    align: 'center' as const
  },
  {
    title: '是否完成',
    dataIndex: 'isFinish',
    key: 'isFinish',
    width: 100,
    align: 'center' as const
  },
  {
    title: '转化时间',
    dataIndex: 'transTime',
    key: 'transTime',
    width: 160,
    ellipsis: true
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 160,
    ellipsis: true
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right' as const,
    align: 'center' as const
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'transState',
    label: '转化状态',
    type: 'select' as const,
    props: {
      options: [
        { label: '待处理', value: 0 },
        { label: '已处理', value: 1 },
        { label: '处理中', value: 2 }
      ],
      placeholder: '请选择转化状态',
      allowClear: true
    }
  },
  {
    name: 'startNodeName',
    label: '开始节点',
    type: 'input' as const,
    props: {
      placeholder: '请输入开始节点名称',
      allowClear: true
    }
  },
  {
    name: 'toNodeName',
    label: '目标节点',
    type: 'input' as const,
    props: {
      placeholder: '请输入目标节点名称',
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<HbtInstanceTransQuery>({
  pageIndex: 1,
  pageSize: 10,
  transState: 0 // 默认查询待处理的任务
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtInstanceTrans[]>([])
const showSearch = ref(true)

// 详情对话框
const detailVisible = ref(false)
const currentRecord = ref<HbtInstanceTrans | null>(null)

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
    const res = await getMyTodoList(queryParams.value)
    if (res.data) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
    }
  } catch (error) {
    console.error('获取待办任务列表失败:', error)
    message.error('获取待办任务列表失败')
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    // 移除日期范围处理，因为HbtInstanceQuery不支持时间范围查询
    delete values.startTimeRange
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
    transState: 0
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
const handleView = (record: HbtInstanceTrans) => {
  currentRecord.value = record
  detailVisible.value = true
}

// 处理任务
const handleProcess = (record: HbtInstanceTrans) => {
  router.push(`/workflow/engine/approve/${record.instanceId}`)
}

// 导出数据
const handleExport = () => {
  message.info('导出功能待实现')
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
const handleRowClick = (record: HbtInstanceTrans) => {
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
  initColumnSettings()
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-todo-container {
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
</style> 