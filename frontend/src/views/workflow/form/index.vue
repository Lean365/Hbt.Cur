<template>
  <div class="workflow-form-container">
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
      :add-permission="['workflow:form:create']"
      :show-edit="true"
      :edit-permission="['workflow:form:update']"
      :show-delete="true"
      :delete-permission="['workflow:form:delete']"
      :show-import="true"
      :import-permission="['workflow:form:import']"
      :show-export="true"
      :export-permission="['workflow:form:export']"
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
      :row-key="(record: HbtForm) => String(record.formId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 表单分类列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'formCategory'">
          <hbt-dict-tag dict-type="workflow_form_category" :value="record.formCategory" />
        </template>

        <!-- 表单版本列 -->
        <template v-if="column.dataIndex === 'formVersion'">
          <hbt-dict-tag dict-type="workflow_form_version" :value="record.formVersion" />
</template>

      <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="workflow_form_status" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['workflow:form:update']"
            :show-delete="true"
            :delete-permission="['workflow:form:delete']"
            :show-view="true"
            :view-permission="['workflow:form:query']"
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

    <!-- 表单对话框 -->
    <form-form
      v-model:open="formVisible"
      :title="formTitle"
      :form-id="selectedFormId"
      @success="handleSuccess"
    />

    <!-- 表单详情对话框 -->
    <form-detail
      v-if="selectedFormId"
      v-model:open="detailVisible"
      :form-id="selectedFormId"
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
import type { HbtForm, HbtFormQuery } from '@/types/workflow/form'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getFormList, getForm, createForm, updateForm, deleteForm, batchDeleteForm, importForm, exportForm, getFormTemplate } from '@/api/workflow/form'
import FormForm from './components/FormForm.vue'
import FormDetail from './components/FormDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// 表格列定义
const columns = [
  {
    title: t('table.columns.id'),
    dataIndex: 'formId',
    key: 'formId',
    width: 200
  },
  {
    title: t('workflow.form.fields.formName'),
    dataIndex: 'formName',
    key: 'formName',
    width: 200
  },
  {
    title: t('workflow.form.fields.formDesc'),
    dataIndex: 'formDesc',
    key: 'formDesc',
    width: 200
  },
  {
    title: t('workflow.form.fields.formCategory'),
    dataIndex: 'formCategory',
    key: 'formCategory',
    width: 150
  },
  {
    title: t('workflow.form.fields.formVersion'),
    dataIndex: 'formVersion',
    key: 'formVersion',
    width: 150
  },
  {
    title: t('workflow.form.fields.formConfig'),
    dataIndex: 'formConfig',
    key: 'formConfig',
    width: 150,
    ellipsis: true,
    tooltip: true
  },
  {
    title: t('workflow.form.fields.status'),
    dataIndex: 'status',
    key: 'status',
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
    name: 'formName',
    label: t('workflow.form.fields.formName'),
    type: 'input' as const
  },
  {
    name: 'formCategory',
    label: t('workflow.form.fields.formCategory'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_form_category',
      type: 'select'
    }
  },
  {
    name: 'status',
    label: t('workflow.form.fields.status'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_form_status',
      type: 'select'
    }
  }
]

// 查询参数
const queryParams = ref<HbtFormQuery>({
  pageIndex: 1,
  pageSize: 10,
  formName: '',
  formCategory: undefined,
  status: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtForm[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 弹窗控制相关
const formVisible = ref(false)
const formTitle = ref('')
const selectedFormId = ref<number | undefined>(undefined)

// 详情对话框
const detailVisible = ref(false)

// 导入相关
const importLoading = ref(false)

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
    const res = await getFormList(queryParams.value)
    if (res.data) {
      tableData.value = res.data.data.rows || []
      total.value = res.data.data.totalNum || 0
    }
  } catch (error) {
    console.error('获取表单列表失败:', error)
    message.error('获取表单列表失败')
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
    formName: '',
    formCategory: undefined,
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
  selectedFormId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: HbtForm) => {
  selectedFormId.value = record.formId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: HbtForm) => {
  try {
    const res = await deleteForm(Number(record.formId))
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

// 处理导入
const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      importLoading.value = true
      try {
        const res = await importForm(file)
        if (res.data.code === 200) {
          message.success('导入成功')
          await fetchData()
        } else {
          message.error(res.data.msg || '导入失败')
        }
      } catch (error) {
        message.error('导入失败')
      } finally {
        importLoading.value = false
      }
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getFormTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `工作流表单导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportForm({
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
      fileName = `工作流表单_${new Date().getTime()}.xlsx`
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
    selectedFormId.value = selectedRowKeys.value[0] as number
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteForm(Number(id))))
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

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('workflowFormColumnSettings')

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
  localStorage.setItem('workflowFormColumnSettings', JSON.stringify(settings))
}

// 在组件挂载时初始化列设置
onMounted(() => {
  initColumnSettings()
  fetchData()
})

// 处理行点击
const handleRowClick = (record: HbtForm) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: HbtForm) => {
  selectedFormId.value = record.formId
  detailVisible.value = true
  console.log('查看详情:', record)
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

// 处理详情关闭
const handleDetailClose = () => {
  detailVisible.value = false
}
</script>

<style lang="less" scoped>
.workflow-form-container {
  height: 100%;
  background-color: #fff;
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
