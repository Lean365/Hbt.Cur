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
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleSearch"
      @reset="handleReset"
    >
      <template #queryForm>
        <a-form-item :label="t('admin.config.name')">
          <a-input
            v-model:value="queryParams.configName"
            :placeholder="t('admin.config.placeholder.name')"
            allow-clear
            @keyup.enter="handleSearch"
          />
        </a-form-item>
        <a-form-item :label="t('admin.config.key')">
          <a-input
            v-model:value="queryParams.configKey"
            :placeholder="t('admin.config.placeholder.key')"
            allow-clear
            @keyup.enter="handleSearch"
          />
        </a-form-item>
        <a-form-item :label="t('admin.config.value')">
          <a-input
            v-model:value="queryParams.configValue"
            :placeholder="t('admin.config.placeholder.value')"
            allow-clear
            @keyup.enter="handleSearch"
          />
        </a-form-item>
        <a-form-item :label="t('admin.config.builtin')">
          <hbt-select
            v-model:value="queryParams.isBuiltin"
            dict-type="sys_yes_no"
            type="radio"
            :placeholder="t('admin.config.placeholder.builtin')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('admin.config.status')">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            type="radio"
            :placeholder="t('admin.config.placeholder.status')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('admin.config.isEncrypted')">
          <hbt-select
            v-model:value="queryParams.isEncrypted"
            dict-type="sys_yes_no"
            type="radio"
            :placeholder="t('admin.config.placeholder.isEncrypted')"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['admin:config:create']"
      :show-edit="true"
      :edit-permission="['admin:config:update']"
      :show-delete="true"
      :delete-permission="['admin:config:delete']"
      :show-import="true"
      :import-permission="['admin:config:import']"
      :show-export="true"
      :export-permission="['admin:config:export']"
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
            :view-permission="['admin:config:query']"
            :show-edit="true"
            :edit-permission="['admin:config:update']"
            :show-delete="true"
            :delete-permission="['admin:config:delete']"
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

    <!-- 导入对话框 -->
    <hbt-import-dialog
      v-model:open="importVisible"
      :uploadMethod="(file) => importHbtConfig(file, 'HbtConfig')"
      :templateMethod="async () => (await getHbtConfigTemplate()).data"
      :templateFileName="`系统配置导入模板_${formatDateTime(new Date(), 'yyyyMMddHHmmss')}.xlsx`"
      @success="handleImportSuccess"
    />
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
import type { HbtConfig, HbtConfigQuery } from '@/types/admin/config'
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
} from '@/api/admin/config'
import ConfigForm from './components/ConfigForm.vue'
import ConfigDetail from './components/ConfigDetail.vue'
import { formatDateTime } from '@/utils/datetime'

const { t } = useI18n()
const dictStore = useDictStore()

// === 状态定义 ===
const loading = ref(false)
const showSearch = ref(true)
const tableData = ref<HbtConfig[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedConfigId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const columnSettingVisible = ref(false)
const total = ref(0)
const importVisible = ref(false)
const importLoading = ref(false)
const fileList = ref<UploadFile[]>([])
const templateUrl = ref('/api/HbtConfig/template')

// === 查询参数 ===
const queryParams = ref<HbtConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  configName: '',
  configKey: '',
  configValue: '',
  isBuiltin: -1,
  status: -1,
  isEncrypted: -1
})

// === 查询字段定义 ===
const queryFields = computed<QueryField[]>(() => [
  {
    name: 'configName',
    label: t('admin.config.configName'),
    type: 'input',
    placeholder: t('admin.config.placeholder.configName')
  },
  {
    name: 'configKey',
    label: t('admin.config.configKey'),
    type: 'input',
    placeholder: t('admin.config.placeholder.configKey')
  },
  {
    name: 'configValue',
    label: t('admin.config.configValue'),
    type: 'input',
    placeholder: t('admin.config.placeholder.configValue')
  },
  {
    name: 'isBuiltin',
    label: t('admin.config.builtin'),
    type: 'select',
    props: {
      dictType: 'sys_yes_no',
      type: 'radio'
    },
    placeholder: t('admin.config.placeholder.builtin')
  },
  {
    name: 'status',
    label: t('admin.config.status'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    },
    placeholder: t('admin.config.placeholder.status')
  },
  {
    name: 'isEncrypted',
    label: t('admin.config.isEncrypted'),
    type: 'select',
    props: {
      dictType: 'sys_yes_no',
      type: 'radio'
    },
    placeholder: t('admin.config.placeholder.isEncrypted')
  }
])

// === 表格列定义 ===
const defaultColumns = [
  {
    title: t('admin.config.name'),
    dataIndex: 'configName',
    key: 'configName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('admin.config.key'),
    dataIndex: 'configKey',
    key: 'configKey',
    width: 200,
    ellipsis: true
  },
  {
    title: t('admin.config.value'),
    dataIndex: 'configValue',
    key: 'configValue',
    width: 300,
    ellipsis: true
  },
  {
    title: t('admin.config.builtin'),
    dataIndex: 'isBuiltin',
    key: 'isBuiltin',
    width: 100
  },
  {
    title: t('admin.config.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('admin.config.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: t('admin.config.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    sorter: true
  },
  {
    title: t('common.table.header.operation'),
    key: 'action',
    width: 180,
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

// 查询
const handleSearch = () => {
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
    isEncrypted: -1
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

// 列设置变化
const handleColumnSettingChange = (checkedValues: (string | number | boolean)[]) => {
  defaultColumns.forEach(col => {
    columnSettings.value[col.key] = checkedValues.includes(col.key)
  })
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // TODO: 实现全屏切换
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
const handleImport = () => {
  importVisible.value = true
  fileList.value = []
}

// 导入取消
const handleImportCancel = () => {
  importVisible.value = false
  fileList.value = []
  importLoading.value = false
}

// 导入成功
const handleImportSuccess = () => {
  handleImportCancel()
  fetchData()
}

// 下载模板
const handleTemplate = async () => {
  try {
    loading.value = true
    const res = await getHbtConfigTemplate()
    if (res instanceof Blob) {
      // 创建下载链接
      const url = window.URL.createObjectURL(res)
      const link = document.createElement('a')
      link.href = url
      link.download = `系统配置导入模板_${formatDateTime(new Date(), 'yyyyMMddHHmmss')}.xlsx`
      document.body.appendChild(link)
      link.click()
      document.body.removeChild(link)
      window.URL.revokeObjectURL(url)
      message.success(t('common.message.downloadSuccess'))
    } else {
      message.error(t('common.message.downloadFailed'))
    }
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error(t('common.message.downloadFailed'))
  } finally {
    loading.value = false
  }
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    const res = await exportHbtConfig(queryParams.value)
    if (res instanceof Blob) {
      // 创建下载链接
      const url = window.URL.createObjectURL(res)
      const link = document.createElement('a')
      link.href = url
      link.download = `系统配置_${formatDateTime(new Date(), 'yyyyMMddHHmmss')}.xlsx`
      document.body.appendChild(link)
      link.click()
      document.body.removeChild(link)
      window.URL.revokeObjectURL(url)
      message.success(t('common.message.exportSuccess'))
    } else {
      message.error(t('common.message.exportFailed'))
    }
  } catch (error) {
    console.error('导出失败:', error)
    message.error(t('common.message.exportFailed'))
  } finally {
    loading.value = false
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

// 表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// === 生命周期 ===
onMounted(() => {
  // 初始化列设置
  defaultColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })
  // 加载数据
  fetchData()
})
</script>

<style lang="less" scoped>
.config-container {
  padding: 24px;
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

.upload-tip {
  margin-top: 16px;
  color: rgba(0, 0, 0, 0.45);
  font-size: 14px;
}
</style>