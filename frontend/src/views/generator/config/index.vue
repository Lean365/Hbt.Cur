<template>
  <div class="config-container">
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
      :add-permission="['generator:config:create']"
      :show-edit="true"
      :edit-permission="['generator:config:update']"
      :show-delete="true"
      :delete-permission="['generator:config:delete']"
      :show-export="true"
      :show-import="true"
      :export-permission="['generator:config:export']"
      :import-permission="['generator:config:import']"
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
    />

    <!-- 数据表格 -->
    <hbt-table
      :columns="columns.filter(col => col.key && columnSettings[col.key])"
      :data-source="tableData"
      :loading="loading"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      row-key="genConfigId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :show-delete="true"
            :edit-permission="['generator:config:update']"
            :delete-permission="['generator:config:delete']"
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

    <!-- 新增/编辑表单 -->
    <config-form
      v-model:open="formVisible"
      :title="formTitle"
      :config-id="selectedConfigId"
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
import { ref, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtGenConfig, HbtGenConfigQuery, HbtGenConfigPageResult } from '@/types/generator/genConfig'
import { 
  getGenConfigList,
  getGenConfig,
  createGenConfig,
  updateGenConfig,
  deleteGenConfig,
  batchDeleteGenConfig,
  exportGenConfig,
  downloadTemplate,
  importGenConfig
} from '@/api/generator/genConfig'
import ConfigForm from './components/ConfigForm.vue'

const { t } = useI18n()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'genConfigName',
    label: '配置名称',
    type: 'input',
    placeholder: '请输入配置名称'
  },
  {
    name: 'author',
    label: '作者',
    type: 'input',
    placeholder: '请输入作者'
  },
  {
    name: 'moduleName',
    label: '模块名',
    type: 'input',
    placeholder: '请输入模块名'
  },
  {
    name: 'packageName',
    label: '包名',
    type: 'input',
    placeholder: '请输入包名'
  },
  {
    name: 'businessName',
    label: '业务名',
    type: 'input',
    placeholder: '请输入业务名'
  },
  {
    name: 'functionName',
    label: '功能名',
    type: 'input',
    placeholder: '请输入功能名'
  },
  {
    name: 'genType',
    label: '生成类型',
    type: 'select',
    placeholder: '请选择生成类型'
  },
  {
    name: 'genTemplateType',
    label: '模板选用方式',
    type: 'select',
    placeholder: '请选择模板选用方式'
  },
  {
    name: 'genPath',
    label: '生成路径',
    type: 'input',
    placeholder: '请输入生成路径'
  },
  {
    name: 'status',
    label: '状态',
    type: 'select',
    placeholder: '请选择状态'
  },
  {
    name: 'dateRange',
    label: '创建时间',
    type: 'dateRange',
    placeholder: '请选择时间范围'
  }
]

// 表格列定义
const columns = [
  {
    title: '配置名称',
    dataIndex: 'genConfigName',
    key: 'genConfigName',
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
    title: '模块名',
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 120,
    ellipsis: true
  },
  {
    title: '包名',
    dataIndex: 'packageName',
    key: 'packageName',
    width: 150,
    ellipsis: true
  },
  {
    title: '业务名',
    dataIndex: 'businessName',
    key: 'businessName',
    width: 120,
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
    title: '生成类型',
    dataIndex: 'genType',
    key: 'genType',
    width: 100
  },
  {
    title: '模板选用方式',
    dataIndex: 'genTemplateType',
    key: 'genTemplateType',
    width: 150
  },
  {
    title: '生成路径',
    dataIndex: 'genPath',
    key: 'genPath',
    width: 200,
    ellipsis: true
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 180,
    fixed: 'right'
  }
]

// 默认列设置
const defaultColumns = columns.map(col => ({
  key: col.key,
  title: col.title
}))

// 列设置状态
const columnSettings = ref<Record<string, boolean>>(
  defaultColumns.reduce((acc, col) => {
    acc[col.key] = true
    return acc
  }, {} as Record<string, boolean>)
)

// 状态定义
const loading = ref(false)
const tableData = ref<HbtGenConfig[]>([])
const total = ref(0)
const queryParams = ref<HbtGenConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  genConfigName: '',
  author: '',
  moduleName: '',
  packageName: '',
  businessName: '',
  functionName: '',
  genType: undefined,
  genTemplateType: undefined,
  genPath: '',
  status: undefined,
  dateRange: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const selectedConfigId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const columnSettingVisible = ref(false)
const showSearch = ref(true)

// 生命周期钩子
onMounted(() => {
  fetchData()
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getGenConfigList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || '获取数据失败')
    }
  } catch (error) {
    console.error('获取数据失败:', error)
    message.error('获取数据失败')
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  selectedRowKeys.value = []
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    genConfigName: '',
    author: '',
    moduleName: '',
    packageName: '',
    businessName: '',
    functionName: '',
    genType: undefined,
    genTemplateType: undefined,
    genPath: '',
    status: undefined,
    dateRange: undefined
  }
  selectedRowKeys.value = []
  handleQuery()
}

/** 表格变化事件 */
const handleTableChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

/** 行点击事件 */
const handleRowClick = (record: HbtGenConfig) => {
  selectedConfigId.value = record.genConfigId
}

/** 新增按钮操作 */
const handleAdd = () => {
  selectedConfigId.value = undefined
  formTitle.value = '新增生成配置'
  formVisible.value = true
}

/** 编辑选中行 */
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录')
    return
  }
  selectedConfigId.value = Number(selectedRowKeys.value[0])
  formTitle.value = '修改生成配置'
  formVisible.value = true
}

/** 编辑按钮操作 */
const handleEdit = (record: HbtGenConfig) => {
  selectedConfigId.value = record.genConfigId
  formTitle.value = '修改生成配置'
  formVisible.value = true
}

/** 删除按钮操作 */
const handleDelete = async (record: HbtGenConfig) => {
  if (!record.genConfigId) {
    message.error('无效的配置ID')
    return
  }
  try {
    const res = await deleteGenConfig(record.genConfigId)
    if (res.data.code === 200) {
      message.success('删除成功')
      fetchData()
    } else {
      message.error(res.data.msg || '删除失败')
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

/** 批量删除按钮操作 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    const res = await batchDeleteGenConfig(selectedRowKeys.value.map(Number))
    if (res.data.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.data.msg || '批量删除失败')
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error('批量删除失败')
  }
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
  localStorage.setItem('configColumnSettings', JSON.stringify(settings))
}

/** 列设置 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
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
    const res = await exportGenConfig(queryParams.value)
    if (res.data.code === 200) {
      message.success(t('common.export.success'))
    } else {
      message.error(res.data.msg || t('common.export.failed'))
    }
  } catch (error: any) {
    console.error(error)
    if (error.response?.status === 500) {
      message.error(t('common.export.failed'))
    } else {
      message.error(error.response?.data?.msg || t('common.export.failed'))
    }
  }
}

/** 处理下载模板 */
const handleTemplate = async () => {
  try {
    const res = await downloadTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data.data]))
    link.download = `配置导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
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
      const res = await importGenConfig(file)
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
</script>

<style lang="less" scoped>
.config-container {
  padding: 16px;
}
.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}
.column-setting-item {
  padding: 4px 0;
}
</style> 