<template>
  <div class="table-define-container">
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
      :add-permission="['generator:tabledefine:create']"
      :show-edit="true"
      :edit-permission="['generator:tabledefine:update']"
      :show-delete="true"
      :delete-permission="['generator:tabledefine:delete']"
      :show-sync="true"
      :sync-permission="['generator:tabledefine:sync']"
      :show-import="true"
      :import-permission="['generator:tabledefine:import']"
      :show-export="true"
      :export-permission="['generator:tabledefine:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      :disabled-sync="selectedRowKeys.length !== 1"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @sync="handleSync"
      @import="handleImport"
      @export="handleExport"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :columns="columns"
      :data-source="tableData"
      :loading="loading"
      :pagination="pagination"
      :row-selection="rowSelection"
      @change="handleTableChange"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <a-space>
            <a-button type="link" @click="handleEdit(record)">编辑</a-button>
            <a-button type="link" @click="handleDelete(record)">删除</a-button>
            <a-button type="link" @click="handleSync(record)">同步</a-button>
          </a-space>
        </template>
      </template>
    </hbt-table>

    <!-- 代码生成表定义表单对话框 -->
    <table-define-form
      v-model:visible="formVisible"
      :title="formTitle"
      :table-id="selectedTableId"
      @success="handleSuccess"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :visible="columnSettingVisible"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue/es/table/interface'
import type { HbtGenTableDefineDto, HbtGenTableDefineQuery } from '@/types/generator/tableDefine'
import type { QueryField } from '@/types/components/query'
import {
  getPagedList,
  deleteTableDefine,
  batchDeleteTableDefine,
  importTableDefine,
  exportTableDefine,
  getTemplate,
  syncTable,
  initializeTable
} from '@/api/generator/tableDefine'
import TableDefineForm from './components/TableDefineForm.vue'

/** 生成类型选项 */
const genTypeOptions = [
  { label: '单表', value: 1 },
  { label: '主从表', value: 2 }
]

/** 状态选项 */
const statusOptions = [
  { label: '正常', value: 1 },
  { label: '停用', value: 0 }
]

/** 查询字段 */
const queryFields: QueryField[] = [
  { name: 'tableName', label: '表名', type: 'input', placeholder: '请输入表名' },
  { name: 'tableComment', label: '表注释', type: 'input', placeholder: '请输入表注释' },
  { name: 'className', label: '实体类名', type: 'input', placeholder: '请输入实体类名' },
  { name: 'moduleName', label: '模块名', type: 'input', placeholder: '请输入模块名' },
  { name: 'businessName', label: '业务名', type: 'input', placeholder: '请输入业务名' },
  { name: 'functionName', label: '功能名', type: 'input', placeholder: '请输入功能名' },
  { name: 'author', label: '作者', type: 'input', placeholder: '请输入作者' },
  { name: 'genType', label: '生成类型', type: 'select', options: genTypeOptions },
  { name: 'status', label: '状态', type: 'select', options: statusOptions },
  { name: 'dateRange', label: '创建时间', type: 'dateRange' }
]

// 表格列定义
const columns = [
  {
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName',
    width: 120,
    ellipsis: true
  },
  {
    title: '表注释',
    dataIndex: 'tableComment',
    key: 'tableComment',
    width: 120,
    ellipsis: true
  },
  {
    title: '实体类名',
    dataIndex: 'className',
    key: 'className',
    width: 120,
    ellipsis: true
  },
  {
    title: '模块名',
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 100,
    ellipsis: true
  },
  {
    title: '包名',
    dataIndex: 'packageName',
    key: 'packageName',
    width: 180,
    ellipsis: true
  },
  {
    title: '业务名',
    dataIndex: 'businessName',
    key: 'businessName',
    width: 100,
    ellipsis: true
  },
  {
    title: '功能名',
    dataIndex: 'functionName',
    key: 'functionName',
    width: 120,
    ellipsis: true
  },
  {
    title: '作者',
    dataIndex: 'author',
    key: 'author',
    width: 100,
    ellipsis: true
  },
  {
    title: '生成类型',
    dataIndex: 'genType',
    key: 'genType',
    width: 100,
    customRender: ({ text }: { text: number }) => {
      return text === 1 ? '单表' : '主从表'
    }
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 80,
    customRender: ({ text }: { text: number }) => {
      return text === 1 ? '正常' : '停用'
    }
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '更新时间',
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: '操作',
    key: 'action',
    width: 240,
    fixed: 'right'
  }
]

// 默认列设置
const defaultColumns = columns.map(col => ({
  key: col.key,
  title: col.title
}))

// 列设置状态
const columnSettings = reactive(
  defaultColumns.reduce((acc, col) => {
    acc[col.key] = true
    return acc
  }, {} as Record<string, boolean>)
)

// 状态定义
const loading = ref(false)
const tableData = ref<HbtGenTableDefineDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtGenTableDefineQuery>({
  pageIndex: 1,
  pageSize: 10,
  tableName: '',
  tableComment: '',
  className: '',
  moduleName: '',
  businessName: '',
  functionName: '',
  author: '',
  genType: undefined,
  status: undefined,
  dateRange: undefined
})
const selectedRowKeys = ref<string[]>([])
const selectedTableId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const columnSettingVisible = ref(false)
const showSearch = ref(true)
const pagination = reactive<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showQuickJumper: true,
  showTotal: (total: number) => `共 ${total} 条`
})
const rowSelection = ref({
  type: 'checkbox',
  columnWidth: 60
})

// 生命周期钩子
onMounted(() => {
  fetchData()
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getPagedList({
      ...queryParams,
      pageIndex: pagination.current ?? 1,
      pageSize: pagination.pageSize ?? 10
    })
    if (res.data) {
      tableData.value = res.data.rows
      pagination.total = res.data.totalNum
    }
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
  queryParams.tableName = ''
  queryParams.tableComment = ''
  queryParams.className = ''
  queryParams.moduleName = ''
  queryParams.businessName = ''
  queryParams.functionName = ''
  queryParams.author = ''
  queryParams.genType = undefined
  queryParams.status = undefined
  queryParams.dateRange = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current ?? 1
  pagination.pageSize = pag.pageSize ?? 10
  fetchData()
}

/** 行点击事件 */
const handleRowClick = (record: HbtGenTableDefineDto) => {
  selectedTableId.value = record.id
}

/** 新增按钮操作 */
const handleAdd = () => {
  selectedTableId.value = undefined
  formTitle.value = '新增代码生成表定义'
  formVisible.value = true
}

/** 编辑选中行 */
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录')
    return
  }
  selectedTableId.value = Number(selectedRowKeys.value[0])
  formTitle.value = '修改代码生成表定义'
  formVisible.value = true
}

/** 编辑按钮操作 */
const handleEdit = (record: HbtGenTableDefineDto) => {
  selectedTableId.value = record.id
  formTitle.value = '修改代码生成表定义'
  formVisible.value = true
}

/** 删除按钮操作 */
const handleDelete = async (record: HbtGenTableDefineDto) => {
  Modal.confirm({
    title: '确认删除',
    content: `是否确认删除表名为"${record.tableName}"的数据项？`,
    async onOk() {
      try {
        await deleteTableDefine(record.id)
        message.success('删除成功')
        if (tableData.value.length === 1 && queryParams.pageIndex > 1) {
          queryParams.pageIndex--
        }
        fetchData()
      } catch (error) {
        message.error('删除失败')
      }
    }
  })
}

/** 批量删除按钮操作 */
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要删除的数据')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `是否确认删除选中的${selectedRowKeys.value.length}条数据？`,
    async onOk() {
      try {
        await batchDeleteTableDefine(selectedRowKeys.value.map(id => Number(id)))
        message.success('删除成功')
        if (tableData.value.length === selectedRowKeys.value.length && queryParams.pageIndex > 1) {
          queryParams.pageIndex--
        }
        selectedRowKeys.value = []
        fetchData()
      } catch (error) {
        message.error('删除失败')
      }
    }
  })
}

/** 同步按钮操作 */
const handleSync = async (record: HbtGenTableDefineDto) => {
  Modal.confirm({
    title: '确认同步',
    content: '是否确认同步表结构？',
    async onOk() {
      try {
        await syncTable(record.id)
        message.success('同步成功')
        fetchData()
      } catch (error) {
        message.error('同步失败')
      }
    }
  })
}

/** 初始化按钮操作 */
const handleInitialize = async (record: HbtGenTableDefineDto) => {
  Modal.confirm({
    title: '确认初始化',
    content: `是否确认初始化表名为"${record.tableName}"的表结构？`,
    async onOk() {
      try {
        await initializeTable(record)
        message.success('初始化成功')
        fetchData()
      } catch (error) {
        message.error('初始化失败')
      }
    }
  })
}

/** 导入按钮操作 */
const handleImport = async () => {
  try {
    const res = await getTemplate()
    const blob = new Blob([res.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(blob)
    link.download = '代码生成表定义导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success('下载模板成功')
  } catch (error) {
    message.error('下载模板失败')
  }
}

/** 导出按钮操作 */
const handleExport = async () => {
  try {
    const res = await exportTableDefine()
    const blob = new Blob([res.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(blob)
    link.download = `代码生成表定义_${new Date().toLocaleString()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success('导出成功')
  } catch (error) {
    message.error('导出失败')
  }
}

/** 列设置变更 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

/** 列设置确认 */
const handleColumnSettingChange = (checkedValues: any[]) => {
  Object.keys(columnSettings).forEach(key => {
    columnSettings[key] = checkedValues.includes(key)
  })
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 切换全屏显示状态 */
const toggleFullscreen = () => {
  const element = document.documentElement
  if (document.fullscreenElement) {
    document.exitFullscreen()
  } else {
    element.requestFullscreen()
  }
}

/** 表单提交成功回调 */
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}
</script>

<style lang="less" scoped>
.table-define-container {
  padding: 16px;
  background-color: #fff;
  
  .column-setting-group {
    display: flex;
    flex-direction: column;
    gap: 8px;
  }
  
  .column-setting-item {
    padding: 4px 0;
  }
}
</style> 