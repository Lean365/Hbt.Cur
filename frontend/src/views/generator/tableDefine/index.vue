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
      :show-import="true"
      :import-permission="['generator:tabledefine:import']"
      :show-export="true"
      :export-permission="['generator:tabledefine:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      :disabled-sync="selectedRowKeys.length !== 1"
      :disabled-initialize="selectedRowKeys.length !== 1"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
      @export="handleExport"
      @initialize="handleInitialize"
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
      :pagination="false"
      row-key="genTableDefineId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'tableName'">
          <a @click="handleOpenColumn(record)">{{ record.tableName }}</a>
        </template>
        <template v-else-if="column.key === 'dbType'">
          <hbt-dict-tag dict-type="gen_db_type" :value="record.dbType" />
        </template>
        <template v-else-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['generator:tabledefine:update']"
            :show-delete="true"
            :delete-permission="['generator:tabledefine:delete']"
            :show-sync="true"
            :sync-permission="['generator:tabledefine:sync']"
            :show-initialize="true"
            :initialize-permission="['generator:tabledefine:initialize']"
            @edit="handleEdit"
            @delete="handleDelete"
            @sync="handleSync"
            @initialize="handleInitialize"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 分页 -->
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

    <!-- 代码生成表定义表单对话框 -->
    <table-define-form
      v-model:open="formVisible"
      :title="formTitle"
      :table-id="selectedTableId"
      @success="handleSuccess"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      v-model:open="columnSettingVisible"
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
        <div v-for="col in columns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>

    <!-- 列定义弹窗 -->
    <gen-column-define
      v-model:open="columnDrawerVisible"
      :table-id="currentTableId || 0"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message, Modal, TablePaginationConfig } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { HbtGenTableDefine, HbtGenTableDefineQuery, HbtGenTableDefineImport, HbtGenTableDefineCreate } from '@/types/generator/genTableDefine'
import {
  getTableDefineList,
  deleteTableDefine,
  batchDeleteTableDefine,
  importTableDefine,
  exportTableDefine,
  getTableDefineTemplate,
  syncTableDefine,
  initializeTableDefine,
} from '@/api/generator/genTableDefine'
import type { QueryField } from '@/types/components/query'
import TableDefineForm from './components/TableDefineForm.vue'
import GenColumnDefine from './components/GenColumnDefine.vue'
import {
  PlusOutlined,
  UploadOutlined,
  DownloadOutlined,
  FileOutlined,
  DeleteOutlined
} from '@ant-design/icons-vue'


const { t } = useI18n()

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
  {
    name: 'tableName',
    label: '表名',
    type: 'input',
    placeholder: '请输入表名'
  },
  {
    name: 'tableComment',
    label: '表注释',
    type: 'input',
    placeholder: '请输入表注释'
  },
  {
    name: 'databaseName',
    label: '数据库名',
    type: 'input',
    placeholder: '请输入数据库名'
  },
  {
    name: 'tableType',
    label: '表类型',
    type: 'select',
    options: [
      { label: '普通表', value: 1 },
      { label: '视图', value: 2 }
    ]
  },
  {
    name: 'status',
    label: '状态',
    type: 'select',
    options: [
      { label: '正常', value: 1 },
      { label: '停用', value: 0 }
    ]
  },
  {
    name: 'dateRange',
    label: '创建时间',
    type: 'dateRange',
    placeholder: '请选择时间范围'
  }
]

/** 表格列 */
const columns = [
  {
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName',
    width: 120,
    ellipsis: true
  },
  {
    title: '表描述',
    dataIndex: 'tableComment',
    key: 'tableComment',
    width: 120,
    ellipsis: true
  },
  {
    title: '数据库名',
    dataIndex: 'databaseName',
    key: 'databaseName',
    width: 120,
    ellipsis: true
  },
  {
    title: '数据库类型',
    dataIndex: 'dbType',
    key: 'dbType',
    width: 100
  },
  {
    title: '作者',
    dataIndex: 'author',
    key: 'author',
    width: 100,
    ellipsis: true
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

// 状态定义
const loading = ref(false)
const tableData = ref<HbtGenTableDefine[]>([])
const total = ref(0)
const queryParams = reactive<HbtGenTableDefineQuery>({
  pageIndex: 1,
  pageSize: 10,
  tableName: '',
  tableComment: '',
  dateRange: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const selectedTableId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const columnSettingVisible = ref(false)
const showSearch = ref(true)

// 列定义相关
const columnDrawerVisible = ref(false)
const currentTableId = ref<number>()

// 列设置相关
const columnSettings = ref<Record<string, boolean>>({})

// 生命周期钩子
onMounted(() => {
  fetchData()
  initColumnSettings()
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', queryParams)
    const res = await getTableDefineList(queryParams)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

/** 查询方法 */
const handleQuery = (values?: Partial<HbtGenTableDefineQuery>) => {
  if (values) {
    Object.assign(queryParams, values)
  }
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置查询 */
const resetQuery = () => {
  Object.assign(queryParams, {
    pageIndex: 1,
    pageSize: 10,
    tableName: '',
    tableComment: '',
    dateRange: undefined
  })
  fetchData()
}

/** 表格变化 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current ?? 1
  queryParams.pageSize = pagination.pageSize ?? 10
  fetchData()
}

/** 处理新增 */
const handleAdd = () => {
  formTitle.value = t('common.add')
  selectedTableId.value = undefined
  formVisible.value = true
}

/** 处理编辑 */
const handleEdit = (record: HbtGenTableDefine) => {
  formTitle.value = t('common.edit')
  selectedTableId.value = record.genTableDefineId
  formVisible.value = true
}

/** 处理删除 */
const handleDelete = async (record: HbtGenTableDefine) => {
  try {
    const res = await deleteTableDefine(record.genTableDefineId)
    if (res.data.code === 200) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

/** 处理导入 */
const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      const res = await importTableDefine(file)
      const { success = 0, fail = 0 } = (res.data as any).Data || {}
      console.log(
        'fail:',
        (res.data as any).Data?.fail,
        'success:',
        (res.data as any).Data?.success
      )

      if (success > 0 && fail === 0) {
        message.success(`导入成功${success}条，全部成功！`)
      } else if (success > 0 && fail > 0) {
        message.warning(`导入成功${success}条，失败${fail}条`)
      } else if (success === 0 && fail > 0) {
        message.error(`全部导入失败，共${fail}条`)
      } else {
        message.info('未读取到任何数据')
      }
      if (success > 0) fetchData()
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}

/** 处理导出 */
const handleExport = async () => {
  try {
    const res = await exportTableDefine()
    const blob = res.data
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(blob)
    link.download = `代码生成表定义_${new Date().toLocaleString()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('common.export.success'))
  } catch (error) {
    console.error(error)
    message.error(t('common.export.failed'))
  }
}

/** 处理表单提交成功 */
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

/** 编辑选中记录 */
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(item => String(item.genTableDefineId) === String(selectedRowKeys.value[0]))
  if (record) {
    formTitle.value = t('common.edit')
    selectedTableId.value = record.genTableDefineId
    formVisible.value = true
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteTableDefine(Number(id))))
    const hasError = results.some(res => res.data.code !== 200)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

/** 处理同步 */
const handleSync = async (record: HbtGenTableDefine) => {
  try {
    const res = await syncTableDefine(record.genTableDefineId)
    if (res.data.code === 200) {
      message.success(t('common.sync.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.sync.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.sync.failed'))
  }
}

/** 处理初始化 */
const handleInitialize = async (record: HbtGenTableDefine) => {
  try {
    const res = await initializeTableDefine(record as unknown as HbtGenTableDefineCreate)
    if (res.data.code === 200) {
      message.success(t('common.initialize.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.initialize.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.initialize.failed'))
  }
}

/** 初始化列设置 */
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('tableDefineColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(columns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = columns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

/** 处理列设置变更 */
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  columns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('tableDefineColumnSettings', JSON.stringify(settings))
}

/** 列设置 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

/** 切换搜索区域显示状态 */
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

/** 切换全屏显示状态 */
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 打开列定义
const handleOpenColumn = (record: HbtGenTableDefine) => {
  currentTableId.value = record.genTableDefineId
  columnDrawerVisible.value = true
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTableDefineTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `表定义导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

/** 处理页码变化 */
const handlePageChange = (page: number) => {
  queryParams.pageIndex = page
  fetchData()
}

/** 处理每页条数变化 */
const handleSizeChange = (size: number) => {
  queryParams.pageSize = size
  fetchData()
}

</script>

<style lang="less" scoped>
.table-define-container {
  padding: 16px;
  background-color: var(--ant-color-bg-container);
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  padding: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  &:last-child {
    border-bottom: none;
  }
}
</style> 