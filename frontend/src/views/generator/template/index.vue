<template>
  <div class="template-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-model:show="showSearch"
            :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      v-model:show-search="showSearch"
      :selected-count="selectedRowKeys.length"
      :show-select-count="true"
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :show-import="true"
      :add-permission="['generator:template:create']"
      :edit-permission="['generator:template:update']"
      :delete-permission="['generator:template:delete']"
      :export-permission="['generator:template:export']"
      :import-permission="['generator:template:import']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @import="handleImport"
      @template="handleTemplate"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    >
    </hbt-toolbar>

    <!-- 数据表格 -->
    <hbt-table
      :columns="columns.filter(col => col.key && columnSettings[col.key])"
      :data-source="tableData"
      :loading="loading"
      :pagination="false"
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
      row-key="genTemplateId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="onTableChange"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :show-delete="true"
            :edit-permission="['generator:template:update']"
            :delete-permission="['generator:template:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
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

    <!-- 添加/修改模板对话框 -->
    <template-form
      v-model:visible="formVisible"
      :title="formTitle"
      :template-id="selectedTemplateId"
      @success="handleSuccess"
      @cancel="formVisible = false"
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
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, computed, nextTick } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtGenTemplate, HbtGenTemplateQuery } from '@/types/generator/genTemplate'
import { 
  getGenTemplateList, 
  getGenTemplate,
  createGenTemplate, 
  updateGenTemplate, 
  deleteGenTemplate,
  batchDeleteGenTemplate,
  exportGenTemplate,
  downloadTemplate,
  importGenTemplate
} from '@/api/generator/genTemplate'
import TemplateForm from './components/TemplateForm.vue'

const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'templateName',
    label: t('generator.template.fields.templateName'),
    type: 'input',
    props: {
      placeholder: t('generator.template.placeholder.templateName'),
      allowClear: true
    }
  },
  {
    name: 'templateOrmType',
    label: t('generator.template.fields.templateOrmType'),
    type: 'select',
    props: {
      dictType: 'gen_orm_framework',

      showAll: true
    }
  },
  {
    name: 'templateCodeType',
    label: t('generator.template.fields.templateCodeType'),
    type: 'select',
    props: {
      dictType: 'gen_code_type',

      showAll: true
    }
  },
  {
    name: 'templateCategory',
    label: t('generator.template.fields.templateCategory'),
    type: 'select',
    props: {
      dictType: 'gen_template_category',

      showAll: true
    }
  },
  {
    name: 'status',
    label: t('generator.template.fields.status'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 表格列定义
const columns = [
  {
    title: t('table.columns.id'),
    dataIndex: 'genTemplateId',
    key: 'genTemplateId',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.template.fields.templateName'),
    dataIndex: 'templateName',
    key: 'templateName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.template.fields.fileName'),
    dataIndex: 'fileName',
    key: 'fileName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('generator.template.fields.templateOrmType'),
    dataIndex: 'templateOrmType',
    key: 'templateOrmType',
    width: 100,
    ellipsis: true
  },
    {
    title: t('generator.template.fields.templateCodeType'),
    dataIndex: 'templateCodeType',
    key: 'templateCodeType',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.template.fields.templateCategory'),
    dataIndex: 'templateCategory',
    key: 'templateCategory',
    width: 100,
    ellipsis: true
  },
  {
    title: t('generator.template.fields.status'),
    dataIndex: 'status',
    key: 'status',
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

// 默认列设置
const defaultColumns = columns.map(col => ({
  key: col.key,
  title: col.title
}))

// 列设置状态
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('templateColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    if (col.key) {
      columnSettings.value[col.key] = true
    }
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

/** 列设置变更 */
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else if (col.key) {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('templateColumnSettings', JSON.stringify(settings))
}

/** 列设置 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 状态定义
const loading = ref(false)
const tableData = ref<HbtGenTemplate[]>([])
const total = ref(0)
const queryParams = ref<HbtGenTemplateQuery>({
  pageIndex: 1,
  pageSize: 10,
  templateName: undefined,
  templateOrmType: undefined,
  templateCodeType: undefined,
  templateCategory: undefined,
  status: -1
})
const selectedRowKeys = ref<(string | number)[]>([])
const selectedTemplateId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const columnSettingVisible = ref(false)

// 选中的模板ID
const selectedKeys = ref<(string | number)[]>([])

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: HbtGenTemplate[]) => {
  selectedKeys.value = keys
}

// 生命周期钩子
onMounted(() => {
  initColumnSettings()
  fetchData()
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })
    const res = await getGenTemplateList(queryParams.value)
    if (res.data.code === 200) {
      console.log('[模板管理] 获取数据:', res.data.data.rows)
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('failed'))
    }
  } catch (error: any) {
    console.error('[模板管理] 获取模板列表出错:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error(t('failed'))
    }
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = (values: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  selectedRowKeys.value = []
  fetchData()
}

/** 重置按钮操作 */
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    templateName: undefined,
    templateOrmType: undefined,
    templateCodeType: undefined,
    templateCategory: undefined,
    status: -1
  }
  fetchData()
}

/** 表格变化事件 */
const onTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 页码变化事件 */
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

/** 每页条数变化事件 */
const handleSizeChange = (pageSize: number) => {
  queryParams.value.pageSize = pageSize
  queryParams.value.pageIndex = 1
  fetchData()
}

/** 行点击事件 */
const handleRowClick = (record: HbtGenTemplate) => {
  selectedTemplateId.value = record.genTemplateId
}

/** 新增按钮操作 */
const handleAdd = () => {
  selectedTemplateId.value = undefined
  formTitle.value = t('generator.template.dialog.create')
  formVisible.value = true
}

/** 编辑选中行 */
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('select.one'))
    return
  }
  const id = Number(selectedRowKeys.value[0])
  console.log('[模板管理] 编辑选中行, id:', id)
  if (!id) {
    message.error(t('invalid.id'))
    return
  }
  selectedTemplateId.value = id
  formTitle.value = t('generator.template.dialog.update')
  formVisible.value = true
}

/** 编辑按钮操作 */
const handleEdit = (record: HbtGenTemplate) => {
  console.log('[模板管理] 编辑按钮点击, record:', record)
  if (!record.genTemplateId) {
    message.error(t('invalid.id'))
    return
  }
  selectedTemplateId.value = record.genTemplateId
  formTitle.value = t('generator.template.dialog.update')
  // 确保在设置 ID 后再显示表单
  nextTick(() => {
    formVisible.value = true
  })
}

/** 删除按钮操作 */
const handleDelete = async (record: HbtGenTemplate) => {
  if (!record.genTemplateId) return
  try {
    await deleteGenTemplate(record.genTemplateId)
    message.success(t('delete.success'))
    fetchData()
  } catch (error) {
    message.error(t('delete.failed'))
  }
}

/** 批量删除按钮操作 */
const handleBatchDelete = () => {
  Modal.confirm({
    title: t('delete.confirm'),
    content: t('delete.message', { count: selectedRowKeys.value.length }),
    async onOk() {
      try {
        await batchDeleteGenTemplate(selectedRowKeys.value.map(Number))
        message.success(t('delete.success'))
        selectedRowKeys.value = []
        fetchData()
      } catch (error) {
        console.error(error)
        message.error(t('delete.failed'))
      }
    }
  })
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 切换全屏显示状态 */
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

/** 表单提交成功 */
const handleSuccess = () => {
  fetchData()
}

/** 处理导出 */
const handleExport = async () => {
  try {
    const res = await exportGenTemplate(queryParams.value)
    if (res.data.code === 200) {
      message.success(t('export.success'))
    } else {
      message.error(res.data.msg || t('export.failed'))
    }
  } catch (error: any) {
    console.error(error)
    if (error.response?.status === 500) {
      message.error(t('export.failed'))
    } else {
      message.error(error.response?.data?.msg || t('export.failed'))
    }
  }
}

/** 处理下载模板 */
const handleTemplate = async () => {
  try {
    const res = await downloadTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `模板导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('template.failed'))
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
      const res = await importGenTemplate(file)
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
    message.error(error.message || t('import.failed'))
  }
}
</script>

<style lang="less" scoped>
.template-container {
  padding: 16px;
  background-color: var(--ant-color-bg-container);
  height: 100%;
  display: flex;
  flex-direction: column;

  .ant-table-wrapper {
    flex: 1;
  }
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