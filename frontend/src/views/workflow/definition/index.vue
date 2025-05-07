<template>
  <div class="workflow-definition-container">
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
      :add-permission="['workflow:definition:create']"
      :show-edit="true"
      :edit-permission="['workflow:definition:update']"
      :show-delete="true"
      :delete-permission="['workflow:definition:delete']"
      :show-import="true"
      :import-permission="['workflow:definition:import']"
      :show-export="true"
      :export-permission="['workflow:definition:export']"
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
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: HbtWorkflowDefinition) => String(record.id)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 工作流类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'workflowType'">
          <hbt-dict-tag dict-type="workflow_type" :value="record.workflowType" />
        </template>
      
        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['workflow:definition:update']"
            :show-delete="true"
            :delete-permission="['workflow:definition:delete']"
            :show-view="true"
            :view-permission="['workflow:definition:query']"
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

    <!-- 工作流定义表单对话框 -->
    <workflow-definition-form
      v-model:open="formVisible"
      :title="formTitle"
      :workflow-definition-id="selectedWorkflowDefinitionId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <a-modal
      v-model:visible="importVisible"
      title="导入工作流定义"
      @ok="handleImportSubmit"
      @cancel="handleImportCancel"
    >
      <a-upload
        :file-list="fileList"
        :before-upload="beforeUpload"
        :customRequest="customRequest"
      >
        <a-button>
          <upload-outlined />
          点击上传
        </a-button>
      </a-upload>
      <template #footer>
        <a-button key="back" @click="handleImportCancel">取消</a-button>
        <a-button key="submit" type="primary" :loading="importLoading" @click="handleImportSubmit">
          确定
        </a-button>
      </template>
    </a-modal>

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

    <!-- 工作流定义详情对话框 -->
    <a-modal
      v-model:visible="detailVisible"
      title="工作流定义详情"
    >
      <!-- 工作流定义详情内容 -->
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import { UploadOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import { useRouter } from 'vue-router'
import type { HbtWorkflowDefinition, HbtWorkflowDefinitionQuery } from '@/types/workflow/workflowDefinition'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getWorkflowDefinitionList, getWorkflowDefinition, createWorkflowDefinition, updateWorkflowDefinition, deleteWorkflowDefinition, batchDeleteWorkflowDefinition, importWorkflowDefinition, exportWorkflowDefinition, getWorkflowDefinitionTemplate } from '@/api/workflow/workflowDefinition'
import WorkflowDefinitionForm from './components/WorkflowDefinitionForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// 表格列定义
const columns = [
  {
    title: t('workflow.definition.workflowId'),
    dataIndex: 'workflowId',
    key: 'workflowId',
    width: 200
  },
  {
    title: t('workflow.definition.workflowName'),
    dataIndex: 'workflowName',
    key: 'workflowName',
    width: 200
  },
  {
    title: t('workflow.definition.workflowCategory'),
    dataIndex: 'workflowCategory',
    key: 'workflowCategory',
    width: 150
  },
  {
    title: t('workflow.definition.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('workflow.definition.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('workflow.definition.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('common.action'),
    key: 'action',
    fixed: 'right',
    width: 200
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'workflowName',
    label: t('workflow.definition.fields.workflowName.label'),
    type: 'input' as const
  },
  {
    name: 'workflowCategory',
    label: t('workflow.definition.fields.workflowCategory.label'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_type',
      type: 'radio'
    }
  },
  {
    name: 'status',
    label: t('workflow.definition.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    }
  }
]

// 查询参数
const queryParams = ref<HbtWorkflowDefinitionQuery>({
  pageIndex: 1,
  pageSize: 10,
  workflowName: '',
  workflowCategory: undefined,
  status: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtWorkflowDefinition[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 弹窗控制相关
const formVisible = ref(false)
const formTitle = ref('')
const selectedWorkflowDefinitionId = ref<number | undefined>(undefined)
const detailVisible = ref(false)

// 导入相关
const importVisible = ref(false)
const importLoading = ref(false)
const fileList = ref<any[]>([])

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getWorkflowDefinitionList(queryParams.value)
    if (res.data) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
    }
  } catch (error) {
    console.error('获取工作流定义列表失败:', error)
    message.error('获取工作流定义列表失败')
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    workflowName: '',
    workflowCategory: undefined,
    status: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理新增
const handleAdd = () => {
  selectedWorkflowDefinitionId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: HbtWorkflowDefinition) => {
  selectedWorkflowDefinitionId.value = record.id
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: HbtWorkflowDefinition) => {
  try {
    const res = await deleteWorkflowDefinition(Number(record.id))
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

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportWorkflowDefinition(queryParams.value)
    if (res.data.code === 200) {
      message.success(t('common.export.success'))
    } else {
      message.error(res.data.msg || t('common.export.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.export.failed'))
  }
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    selectedWorkflowDefinitionId.value = selectedRowKeys.value[0] as number
    formTitle.value = t('common.title.edit')
    formVisible.value = true
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteWorkflowDefinition(Number(id))))
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

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    settings[col.key] = checkedValue.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('workflowDefinitionColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: HbtWorkflowDefinition) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: HbtWorkflowDefinition) => {
  selectedWorkflowDefinitionId.value = record.id
  detailVisible.value = true
}

// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getWorkflowDefinitionTemplate()
    if (res.data.code === 200) {
      message.success(t('common.download.success'))
    } else {
      message.error(res.data.msg || t('common.download.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.download.failed'))
  }
}

// 处理导入提交
const handleImportSubmit = async () => {
  if (!fileList.value.length) {
    message.warning(t('common.upload.selectFile'))
    return
  }

  importLoading.value = true
  try {
    const res = await importWorkflowDefinition(fileList.value[0], 'Sheet1')
    if (res.data.code === 200) {
      message.success(t('common.import.success'))
      importVisible.value = false
      fileList.value = []
      fetchData()
    } else {
      message.error(res.data.msg || t('common.import.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.import.failed'))
  }
  importLoading.value = false
}

// 处理导入取消
const handleImportCancel = () => {
  importVisible.value = false
  fileList.value = []
}

// 上传前处理
const beforeUpload = (file: any) => {
  const isExcel = file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || 
                  file.type === 'application/vnd.ms-excel'
  if (!isExcel) {
    message.error(t('common.upload.excelOnly'))
    return false
  }
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isLt2M) {
    message.error(t('common.upload.sizeLimit'))
    return false
  }
  fileList.value = [file]
  return false
}

// 自定义上传
const customRequest = (options: any) => {
  const { file } = options
  fileList.value = [file]
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

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['workflow_type', 'sys_normal_disable'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})

// 初始化列设置
const initColumnSettings = () => {
  const savedSettings = localStorage.getItem('workflowDefinitionColumnSettings')
  if (savedSettings) {
    columnSettings.value = JSON.parse(savedSettings)
  } else {
    columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, true]))
  }
}
</script>

<style lang="less" scoped>
.workflow-definition-container {
  height: 100%;
  background-color: #fff;
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