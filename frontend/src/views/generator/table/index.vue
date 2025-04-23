<template>
  <div class="table-container">
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
      :add-permission="['generator:table:create']"
      :show-edit="true"
      :edit-permission="['generator:table:update']"
      :show-delete="true"
      :delete-permission="['generator:table:delete']"
      :show-sync="true"
      :sync-permission="['generator:table:sync']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      :disabled-sync="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @sync="handleSync"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns.filter(col => columnSettings[col.key])"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: HbtGenTableDto) => String(record.id)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"     
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['generator:table:update']"
            :show-delete="true"
            :delete-permission="['generator:table:delete']"
            :show-preview="true"
            :preview-permission="['generator:table:preview']"
            :show-generate="true"
            :generate-permission="['generator:table:generate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @preview="handlePreview"
            @generate="handleGenerate"
          >
            <!-- 下载按钮 -->
            <template #extra>
              <a-tooltip title="下载代码">
                <a-button
                  v-hasPermi="['generator:table:download']"
                  type="link"
                  size="small"
                  @click.stop="handleDownload(record)"
                >
                  <template #icon><download-outlined /></template>
                </a-button>
              </a-tooltip>
              <!-- 同步按钮 -->
              <a-tooltip title="同步数据库">
                <a-button
                  v-hasPermi="['generator:table:sync']"
                  type="link"
                  size="small"
                  @click.stop="handleSync([record.id])"
                >
                  <template #icon><sync-outlined /></template>
                </a-button>
              </a-tooltip>
            </template>
          </hbt-operation>
        </template>
      </template>
    </hbt-table>

    <!-- 代码生成表表单对话框 -->
    <table-form
      v-model:visible="formVisible"
      :title="formTitle"
      :table-id="selectedTableId"
      @success="handleSuccess"
    />

    <!-- 代码预览对话框 -->
    <preview-modal
      v-model:open="previewVisible"
      :loading="previewLoading"
      :preview-data="previewData"
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
import type { TablePaginationConfig } from 'ant-design-vue'
import { DownloadOutlined, SyncOutlined } from '@ant-design/icons-vue'
import type { HbtGenTableDto, HbtGenTableQueryDto, HbtGenTablePageResultDto, HbtGenTablePreviewDto } from '@/types/generator/table'
import type { QueryField } from '@/types/components/query'
import { 
  getPagedList, 
  deleteGenTable, 
  generateGenTable, 
  previewGenTable, 
  downloadGenTable,
  syncGenTable
} from '@/api/generator/genTable'
import TableForm from './components/TableForm.vue'
import PreviewModal from './components/PreviewModal.vue'

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'tableName',
    label: '表名',
    type: 'input',
    placeholder: '请输入表名'
  }
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
    dataIndex: 'functionAuthor',
    key: 'functionAuthor',
    width: 100,
    ellipsis: true
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
const tableData = ref<HbtGenTableDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtGenTableQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  tableName: undefined
})
const selectedRowKeys = ref<string[]>([])
const selectedTableId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const columnSettingVisible = ref(false)
const showSearch = ref(true)
const previewVisible = ref(false)
const previewLoading = ref(false)
const previewData = ref<HbtGenTablePreviewDto>({})

// 生命周期钩子
onMounted(() => {
  fetchData()
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getPagedList(queryParams)
    if (res.data) {
      tableData.value = res.data.data.items
      total.value = res.data.data.total
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
  queryParams.tableName = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 行点击事件 */
const handleRowClick = (record: HbtGenTableDto) => {
  selectedTableId.value = record.id
}

/** 新增按钮操作 */
const handleAdd = () => {
  selectedTableId.value = undefined
  formTitle.value = '新增代码生成表'
  formVisible.value = true
}

/** 编辑选中行 */
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录')
    return
  }
  selectedTableId.value = Number(selectedRowKeys.value[0])
  formTitle.value = '修改代码生成表'
  formVisible.value = true
}

/** 编辑按钮操作 */
const handleEdit = (record: HbtGenTableDto) => {
  selectedTableId.value = record.id
  formTitle.value = '修改代码生成表'
  formVisible.value = true
}

/** 删除按钮操作 */
const handleDelete = async (record: HbtGenTableDto) => {
  Modal.confirm({
    title: '确认删除',
    content: `是否确认删除表名为"${record.tableName}"的数据项？`,
    async onOk() {
      try {
        await deleteGenTable(record.id as number)
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
        const promises = selectedRowKeys.value.map(id => deleteGenTable(Number(id)))
        await Promise.all(promises)
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

/** 预览按钮操作 */
const handlePreview = async (record: HbtGenTableDto) => {
  previewLoading.value = true
  try {
    const res = await previewGenTable(record.id as number)
    if (res.data) {
      previewData.value = res.data.data
      previewVisible.value = true
    }
  } catch (error) {
    message.error('预览失败')
  } finally {
    previewLoading.value = false
  }
}

/** 生成代码按钮操作 */
const handleGenerate = async (record: HbtGenTableDto) => {
  Modal.confirm({
    title: '确认生成',
    content: `是否确认生成表名为"${record.tableName}"的代码？`,
    async onOk() {
      try {
        await generateGenTable(record.id as number)
        message.success('生成成功')
      } catch (error) {
        message.error('生成失败')
      }
    }
  })
}

/** 下载按钮操作 */
const handleDownload = async (record: HbtGenTableDto) => {
  try {
    const res = await downloadGenTable(record.id as number)
    const blob = new Blob([res.data], { type: 'application/zip' })
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(blob)
    link.download = `code-${record.tableName}.zip`
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success('下载成功')
  } catch (error) {
    message.error('下载失败')
  }
}

/** 同步按钮操作 */
const handleSync = async (ids: number[]) => {
  Modal.confirm({
    title: '确认同步',
    content: '是否确认同步数据库？',
    async onOk() {
      try {
        const promises = ids.map(id => syncGenTable(id))
        await Promise.all(promises)
        message.success('同步成功')
        fetchData()
      } catch (error) {
        message.error('同步失败')
      }
    }
  })
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
.table-container {
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