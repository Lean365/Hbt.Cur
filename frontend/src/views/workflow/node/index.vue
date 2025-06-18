<template>
  <div class="workflow-node-container">
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
      :show-add="true"
      :add-permission="['workflow:node:create']"
      :show-edit="true"
      :edit-permission="['workflow:node:update']"
      :show-delete="true"
      :delete-permission="['workflow:node:delete']"
      :show-import="true"
      :import-permission="['workflow:node:import']"
      :show-export="true"
      :export-permission="['workflow:node:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
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
      :row-key="(record: HbtNode) => String(record.nodeId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 节点类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'nodeType'">
          <hbt-dict-tag dict-type="workflow_node_type" :value="record.nodeType" />
        </template>
      
        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="workflow_node_status" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['workflow:node:update']"
            :show-delete="true"
            :delete-permission="['workflow:node:delete']"
            :show-view="true"
            :view-permission="['workflow:node:query']"
            size="small"
            @edit="handleEdit"
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

    <!-- 节点表单对话框 -->
    <node-form
      v-model:open="formVisible"
      :title="formTitle"
      :node-id="selectedNodeId"
      @success="handleSuccess"
    />

    <!-- 节点详情对话框 -->
    <node-detail
      v-model:open="detailVisible"
      :node-id="selectedNodeId"
      @update:open="handleDetailClose"
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
import { UploadOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import { useRouter } from 'vue-router'
import type { HbtNode, HbtNodeQuery } from '@/types/workflow/node'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getWorkflowNodeList, getWorkflowNode, createWorkflowNode, updateWorkflowNode, deleteWorkflowNode, batchDeleteWorkflowNode, importWorkflowNode, exportWorkflowNode, getWorkflowNodeTemplate } from '@/api/workflow/node'
import NodeForm from './components/NodeForm.vue'
import NodeDetail from './components/NodeDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// 表格列定义
const columns = [
  {
    title: t('table.columns.id'),
    dataIndex: 'nodeId',
    key: 'nodeId',
    width: 200
  },
  {
    title: t('workflow.node.fields.nodeName'),
    dataIndex: 'nodeName',
    key: 'nodeName',
    width: 200
  },
  {
    title: t('workflow.node.fields.nodeType'),
    dataIndex: 'nodeType',
    key: 'nodeType',
    width: 150
  },
  {
    title: t('workflow.node.fields.definitionId'),
    dataIndex: 'definitionId',
    key: 'definitionId',
    width: 150
  },
  {
    title: t('workflow.node.fields.parentNodeId'),
    dataIndex: 'parentNodeId',
    key: 'parentNodeId',
    width: 150
  },
  {
    title: t('workflow.node.fields.nodeConfig'),
    dataIndex: 'nodeConfig',
    key: 'nodeConfig',
    width: 150,
    ellipsis: true,
    tooltip: true
  },
  {
    title: t('workflow.node.fields.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('workflow.node.fields.startTime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 180
  },
  {
    title: t('workflow.node.fields.endTime'),
    dataIndex: 'endTime',
    key: 'endTime',
    width: 180
  },
  {
    title: t('workflow.node.fields.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
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
    name: 'nodeName',
    label: t('workflow.node.fields.nodeName'),
    type: 'input' as const
  },
  {
    name: 'nodeType',
    label: t('workflow.node.fields.nodeType'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_node_type',
      type: 'select'
    }
  },
  {
    name: 'status',
    label: t('workflow.node.fields.status'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_node_status',
      type: 'select'
    }
  }
]

// 查询参数
const queryParams = ref<HbtNodeQuery>({
  pageIndex: 1,
  pageSize: 10,
  nodeName: '',
  nodeType: undefined,
  status: undefined,
  startTime: undefined,
  endTime: undefined
})

// 表格数据
const tableData = ref<HbtNode[]>([])
const loading = ref(false)
const total = ref(0)
const selectedRowKeys = ref<string[]>([])
const selectedNodeId = ref<number>(0)
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const showSearch = ref(true)
const columnSettingVisible = ref(false)
const isFullscreen = ref(false)

// 列设置
const columnSettings = ref<Record<string, boolean>>({})
const defaultColumns = columns

// 可见列
const visibleColumns = computed(() => {
  return columns.filter(col => columnSettings.value[col.key])
})

// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    const res = await getWorkflowNodeList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.total
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取节点列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    nodeName: '',
    nodeType: undefined,
    status: undefined,
    startTime: undefined,
    endTime: undefined
  }
  fetchData()
}

// 新增
const handleAdd = () => {
  selectedNodeId.value = 0
  formTitle.value = t('workflow.node.actions.add')
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.pleaseSelectOne'))
    return
  }
  const record = tableData.value.find(item => item.nodeId === Number(selectedRowKeys.value[0]))
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: HbtNode) => {
  selectedNodeId.value = record.nodeId
  formTitle.value = t('workflow.node.actions.edit')
  formVisible.value = true
}

// 查看详情
const handleView = (record: HbtNode) => {
  selectedNodeId.value = record.nodeId
  detailVisible.value = true
}

// 关闭详情
const handleDetailClose = (value: boolean) => {
  if (!value) {
    selectedNodeId.value = 0
  }
}

// 关闭表单
const handleFormClose = (value: boolean) => {
  if (!value) {
    selectedNodeId.value = 0
  }
}

// 删除
const handleDelete = async (record: HbtNode) => {
  try {
    const res = await deleteWorkflowNode(record.nodeId)
    if (res.data.code === 200) {
      message.success(t('common.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('删除节点失败:', error)
    message.error(t('common.failed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.pleaseSelect'))
    return
  }
  try {
    const res = await batchDeleteWorkflowNode(selectedRowKeys.value.map(key => Number(key)))
    if (res.data.code === 200) {
      message.success(t('common.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('批量删除节点失败:', error)
    message.error(t('common.failed'))
  }
}

// 导入
const handleImport = () => {
  // TODO: 实现导入功能
}

// 导出
const handleExport = async () => {
  try {
    const res = await exportWorkflowNode(queryParams.value)
    if (res.data.code === 200) {
      // TODO: 处理文件下载
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('导出节点失败:', error)
    message.error(t('common.failed'))
  }
}

// 下载模板
const handleTemplate = async () => {
  try {
    const res = await getWorkflowNodeTemplate()
    if (res.data.code === 200) {
      // TODO: 处理文件下载
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error(t('common.failed'))
  }
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 行点击
const handleRowClick = (record: HbtNode) => {
  // TODO: 实现行点击功能
}

// 页码变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 每页条数变化
const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  queryParams.value.pageIndex = 1
  fetchData()
}

// 列设置变化
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
  localStorage.setItem('workflow_node_columns', JSON.stringify(settings))
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  isFullscreen.value = !isFullscreen.value
}

// 成功回调
const handleSuccess = () => {
  fetchData()
  formVisible.value = false
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('workflow_node_columns')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

onMounted(() => {
  initColumnSettings()
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-node-container {
  padding: 24px;
  background-color: #fff;
  border-radius: 2px;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  display: flex;
  align-items: center;
  gap: 8px;
}
</style> 