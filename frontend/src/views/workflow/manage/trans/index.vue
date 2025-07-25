<template>
  <div class="workflow-trans-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-delete="true"
      :delete-permission="['workflow:trans:delete']"
      :show-export="true"
      :export-permission="['workflow:trans:export']"
      :disabled-delete="selectedRowKeys.length === 0"
      @delete="handleBatchDelete"
      @export="handleExport"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
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
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 流转类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'transState'">
          <hbt-dict-tag dict-type="workflow_trans_state" :value="record.transState" />
        </template>
      
        <!-- 是否完成列 -->
        <template v-if="column.dataIndex === 'isFinish'">
          <a-tag :color="record.isFinish === 1 ? 'green' : 'orange'">
            {{ record.isFinish === 1 ? '已完成' : '进行中' }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-delete="true"
            :delete-permission="['workflow:trans:delete']"
            :show-view="true"
            :view-permission="['workflow:trans:query']"
            size="small"
            @delete="handleDelete"
            @view="handleView"
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

    <!-- 查看详情 -->
    <instance-trans-detail
      v-model:open="detailVisible"
      :instance-trans-id="selectedTransId"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
      :title="t('common.columnSetting')"
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
import { useDictStore } from '@/stores/dict'
import { useRouter } from 'vue-router'
import type { HbtInstanceTrans, HbtInstanceTransQuery } from '@/types/workflow/trans'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getInstanceTransList, deleteInstanceTrans, batchDeleteInstanceTrans, exportInstanceTrans } from '@/api/workflow/trans'
import InstanceTransDetail from './components/InstanceTransDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// 表格列定义
const columns = [
  {
    title: t('table.columns.id'),
    dataIndex: 'instanceTransId',
    key: 'instanceTransId',
    width: 200
  },
  {
    title: t('workflow.trans.fields.instanceId'),
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 200
  },
  {
    title: t('workflow.trans.fields.startNodeId'),
    dataIndex: 'startNodeId',
    key: 'startNodeId',
    width: 150
  },
  {
    title: t('workflow.trans.fields.startNodeName'),
    dataIndex: 'startNodeName',
    key: 'startNodeName',
    width: 200
  },
  {
    title: t('workflow.trans.fields.startNodeType'),
    dataIndex: 'startNodeType',
    key: 'startNodeType',
    width: 150
  },
  {
    title: t('workflow.trans.fields.toNodeId'),
    dataIndex: 'toNodeId',
    key: 'toNodeId',
    width: 150
  },
  {
    title: t('workflow.trans.fields.toNodeName'),
    dataIndex: 'toNodeName',
    key: 'toNodeName',
    width: 200
  },
  {
    title: t('workflow.trans.fields.toNodeType'),
    dataIndex: 'toNodeType',
    key: 'toNodeType',
    width: 150
  },
  {
    title: t('workflow.trans.fields.transState'),
    dataIndex: 'transState',
    key: 'transState',
    width: 150
  },
  {
    title: t('workflow.trans.fields.isFinish'),
    dataIndex: 'isFinish',
    key: 'isFinish',
    width: 150
  },
  {
    title: t('workflow.trans.fields.transTime'),
    dataIndex: 'transTime',
    key: 'transTime',
    width: 180
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    align: 'center',
    ellipsis: true
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'instanceId',
    label: t('workflow.trans.fields.instanceId'),
    type: 'input' as const
  },
  {
    name: 'nodeId',
    label: t('workflow.trans.fields.nodeId'),
    type: 'input' as const
  },
  {
    name: 'nodeName',
    label: t('workflow.trans.fields.nodeName'),
    type: 'input' as const
  },
  {
    name: 'nodeType',
    label: t('workflow.trans.fields.nodeType'),
    type: 'input' as const
  },
  {
    name: 'transType',
    label: t('workflow.trans.fields.transType'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_trans_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'transResult',
    label: t('workflow.trans.fields.transResult'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_trans_result',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<HbtInstanceTransQuery>({
  pageIndex: 1,
  pageSize: 10,
  instanceId: undefined,
  startNodeId: '',
  startNodeName: '',
  startNodeType: undefined,
  toNodeId: '',
  toNodeName: '',
  toNodeType: undefined,
  transState: -1,
  isFinish: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtInstanceTrans[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const columnSettingVisible = ref(false)
const isFullscreen = ref(false)
const detailVisible = ref(false)
const selectedTransId = ref<number | undefined>(undefined)

// 列设置
const columnSettings = ref<Record<string, boolean>>({})
const defaultColumns = columns

// 可见列
const visibleColumns = computed(() => {
  return columns.filter(col => columnSettings.value[col.key])
})

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getInstanceTransList(queryParams.value)
    if (res.data) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
    }
  } catch (error) {
    console.error('获取流转历史失败:', error)
    message.error('获取流转历史失败')
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
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
    instanceId: undefined,
    startNodeId: '',
    startNodeName: '',
    startNodeType: undefined,
    toNodeId: '',
    toNodeName: '',
    toNodeType: undefined,
    transState: -1,
    isFinish: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理删除
const handleDelete = async (record: HbtInstanceTrans) => {
  try {
    const res = await deleteInstanceTrans(record.instanceTransId)
    if (res.data.code === 200) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error('删除流转历史失败:', error)
    message.error(t('common.delete.failed'))
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

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }
  try {
    const res = await batchDeleteInstanceTrans(selectedRowKeys.value.map(key => Number(key)))
    if (res.data.code === 200) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error('批量删除流转历史失败:', error)
    message.error(t('common.delete.failed'))
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
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
  localStorage.setItem('workflowTransColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: HbtInstanceTrans) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: HbtInstanceTrans) => {
  selectedTransId.value = record.instanceTransId
  detailVisible.value = true
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

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('workflowTransColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前10列（不包含操作列）
  const firstTenColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 10)

  // 设置前10列为true
  firstTenColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportInstanceTrans({
      ...queryParams.value
    })
    // 动态获取文件名
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    let fileName = ''
    if (disposition) {
      // 优先匹配 filename*（带中文）
      let match = disposition.match(/filename\*=UTF-8''([^;]+)/)
      if (match && match[1]) {
        fileName = decodeURIComponent(match[1])
      } else {
        // 再匹配 filename
        match = disposition.match(/filename="?([^";]+)"?/)
        if (match && match[1]) {
          fileName = match[1]
        }
      }
    }
    if (!fileName) {
      fileName = `工作流流转历史_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(res.data)
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('common.export.success'))
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || t('common.export.failed'))
  }
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['workflow_trans_type', 'workflow_trans_result'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-trans-container {
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