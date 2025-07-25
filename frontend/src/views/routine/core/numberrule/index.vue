//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置管理页面
//===================================================================

<template>
  <div class="config-container">
    <!-- 查询区域 -->
    <hbt-query
      v-model:show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >

    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['core:config:create']"
      :show-edit="true"
      :edit-permission="['core:config:update']"
      :show-delete="true"
      :delete-permission="['core:config:delete']"
      :show-import="true"
      :import-permission="['core:config:import']"
      :show-export="true"
      :export-permission="['core:config:export']"
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
      :scroll="{ x: 1000 }"
      :row-key="(record: HbtConfig) => String(record.configId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
      @selectChange="onSelectChange"
    >
      <!-- 配置类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'isBuiltin'">
          <hbt-dict-tag dict-type="sys_yes_no" :value="record.isBuiltin ?? 0" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['core:config:query']"
            :show-edit="true"
            :edit-permission="['core:config:update']"
            :show-delete="true"
            :delete-permission="['core:config:delete']"
            size="small"
            @view="handleView"
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

    <!-- 配置表单对话框 -->
    <config-form
      v-model:open="formVisible"
      :title="formTitle"
      :config-id="selectedConfigId"
      @success="handleSuccess"
    />

    <!-- 配置详情对话框 -->
    <config-detail
      v-model:open="detailVisible"
      :config-id="selectedConfigId"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
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
import type { TablePaginationConfig } from 'ant-design-vue'
import type { UploadFile } from 'ant-design-vue'
import { useDictStore } from '@/stores/dict'
import type { HbtConfig, HbtConfigQuery } from '@/types/routine/core/config'
import type { QueryField } from '@/types/components/query'
import {
  getHbtConfigList,
  getHbtConfig,
  createHbtConfig,
  updateHbtConfig,
  deleteHbtConfig,
  batchDeleteHbtConfig,
  importHbtConfig,
  exportHbtConfig,
  getHbtConfigTemplate
} from '@/api/routine/core/config'
import ConfigForm from './components/ConfigForm.vue'
import ConfigDetail from './components/ConfigDetail.vue'
import { formatDateTime } from '@/utils/datetime'
import request from '@/utils/request'
import { getToken } from '@/utils/auth'

const { t } = useI18n()
const dictStore = useDictStore()

// === 状态定义 ===
const loading = ref(false)
const showSearch = ref(false)
const tableData = ref<HbtConfig[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedConfigId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const columnSettingVisible = ref(false)
const total = ref(0)

// === 查询参数 ===
const queryParams = ref<HbtConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  configName: '',
  configKey: '',
  configValue: '',
  isBuiltin: -1,
  status: -1,
  isEncrypted: -1,
})

// === 查询字段定义 ===
const queryFields: QueryField[] = [
  {
    name: 'configName',
    label: t('core.config.fields.configName.label'),
    placeholder: t('core.config.fields.configName.placeholder'),
    type: 'input' as const
  },
  {
    name: 'configKey',
    label: t('core.config.fields.configKey.label'),
    placeholder: t('core.config.fields.configKey.placeholder'),
    type: 'input' as const
  },
  {
    name: 'configValue',
    label: t('core.config.fields.configValue.label'),
    placeholder: t('core.config.fields.configValue.placeholder'),
    type: 'input' as const
  },
  {
    name: 'isBuiltin',
    label: t('core.config.fields.isBuiltin.label'),
    placeholder: t('core.config.fields.isBuiltin.placeholder'),
    type: 'select' as const,
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: t('core.config.fields.status.label'),
    placeholder: t('core.config.fields.status.placeholder'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'isEncrypted',
    label: t('core.config.fields.isEncrypted.label'),
    placeholder: t('core.config.fields.isEncrypted.placeholder'),
    type: 'select' as const,
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true
    }
  }
]

// === 表格列定义 ===
const defaultColumns = [
  {
    title: t('core.config.table.columns.configName'),
    dataIndex: 'configName',
    key: 'configName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.config.table.columns.configKey'),
    dataIndex: 'configKey',
    key: 'configKey',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.config.table.columns.configValue'),
    dataIndex: 'configValue',
    key: 'configValue',
    width: 300,
    ellipsis: true
  },
  {
    title: t('core.config.table.columns.isBuiltin'),
    dataIndex: 'isBuiltin',
    key: 'isBuiltin',
    width: 100
  },
  {
    title: t('core.config.table.columns.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('core.config.table.columns.isEncrypted'),
    dataIndex: 'isEncrypted',
    key: 'isEncrypted',
    width: 100
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('table.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120
  },
  {
    title: t('table.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180
  },
  {
    title: t('table.columns.operation'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 列设置
const columnSettings = ref<Record<string, boolean>>({})
const columns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    console.log('查询参数:', {
      ...queryParams.value
    })
    const res = await getHbtConfigList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载配置列表失败:', error)
    message.error(t('common.failed'))
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

// 重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    configName: '',
    configKey: '',
    configValue: '',
    isBuiltin: -1,
    status: -1,
    isEncrypted: -1,
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  if (pag.current) {
    queryParams.value.pageIndex = pag.current
  }
  if (pag.pageSize) {
    queryParams.value.pageSize = pag.pageSize
  }
  fetchData()
}

// 行点击
const handleRowClick = (record: HbtConfig) => {
  selectedConfigId.value = record.configId
}

// 选择行变化
const onSelectChange = (keys: string[]) => {
  selectedRowKeys.value = keys
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('configColumnSettings')

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
  localStorage.setItem('configColumnSettings', JSON.stringify(settings))
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 新增
const handleAdd = () => {
  selectedConfigId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.configId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: HbtConfig) => {
  selectedConfigId.value = record.configId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 查看
const handleView = (record: HbtConfig) => {
  selectedConfigId.value = record.configId
  detailVisible.value = true
}

// 删除
const handleDelete = async (record: HbtConfig) => {
  try {
    const res = await deleteHbtConfig(record.configId)
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.message.selectRecord'))
    return
  }
  try {
    const res = await batchDeleteHbtConfig(selectedRowKeys.value.map(key => Number(key)))
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 导入
const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      const res = await importHbtConfig(file)
      const { success = 0, fail = 0 } = (res.data as any).Data || {}
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

// 导出
const handleExport = async () => {
  try {
    const res = await exportHbtConfig({
      ...queryParams.value
    })
    // 动态获取文件名
    console.log('res.headers', res.headers)
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    console.log('disposition', disposition)
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
      fileName = `系统配置_${new Date().getTime()}.xlsx`
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

// 下载模板
const handleTemplate = async () => {
  try {
    const res = await getHbtConfigTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `系统配置导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 页码变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 每页条数变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = current
  queryParams.value.pageSize = size
  fetchData()
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedConfigId.value = undefined
  selectedRowKeys.value = []
  fetchData()
}

// === 生命周期 ===
onMounted(() => {
  // 初始化列设置
  initColumnSettings()
  // 加载数据
  fetchData()
})
</script>

<style lang="less" scoped>
.config-container {
  padding: 24px;
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

.upload-tip {
  margin-top: 16px;
  color: rgba(0, 0, 0, 0.45);
  font-size: 14px;
}
</style>

