<template>
  <div class="translation-manager">
    <!-- 查询表单 -->
    <hbt-query
      v-model:show="showSearch"
      :loading="loading"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['core:language:create']"
      :show-edit="true"
      :edit-permission="['core:language:update']"
      :show-delete="true"
      :delete-permission="['core:language:delete']"
      :show-import="true"
      :import-permission="['core:language:import']"
      :show-export="true"
      :export-permission="['core:language:export']"
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
      <template #extra>
        <a-button
          type="default"
          class="hbt-btn-save"
          @click="handleLang"
        >
          <template #icon><plus-outlined /></template>
          {{ t('core.language.title') }}
        </a-button>
      </template>
    </hbt-toolbar>

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content', y: 400 }"
      :row-key="(record) => String(record.translationId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['core:language:query']"
            :show-edit="true"
            :edit-permission="['core:language:update']"
            :show-delete="true"
            :delete-permission="['core:language:delete']"
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
      :current="queryParams.pageIndex"
      :pageSize="queryParams.pageSize"
      :total="total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 翻译表单对话框 -->
    <translation-form
      v-model:open="formVisible"
      :translation-id="selectedTranslationId"
      :trans-key="selectedTransKey"
      @success="handleSuccess"
    />

    <!-- 翻译详情对话框 -->
    <translation-detail
      v-model:open="detailVisible"
      :translation-id="selectedTranslationId"
      :trans-key="selectedTransKey"
    />

    <!-- 语言表单对话框 -->
    <language
      v-model:open="languageFormVisible"
      @success="handleLanguageSuccess"
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
        <div v-for="col in dynamicColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted, h, computed, defineExpose } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import {
  getHbtTranslationList,
  getHbtTranslation,
  createHbtTranslation,
  updateHbtTranslation,
  deleteHbtTranslation,
  batchDeleteHbtTranslation,
  importHbtTranslation,
  exportHbtTranslation,
  getHbtTranslationTemplate,
  getHbtTransposedData
} from '@/api/routine/core/translation'
import TranslationForm from './components/TranslationForm.vue'
import TranslationDetail from './components/TranslationDetail.vue'
import Language from './components/Language.vue'
import type { CheckboxValueType } from 'ant-design-vue/es/checkbox/interface'
import type { HbtTransposedData } from '@/types/routine/core/translation'
import type { HbtPagedResult } from '@/types/common'

const { t } = useI18n()

interface TableColumn {
  title: string
  dataIndex?: string
  key: string
  width: number
  ellipsis?: boolean
  fixed?: string
}

interface TableRow {
  moduleName: string
  transKey: string
  translationId: number
  [key: string]: string | number
}

interface QueryValues {
  moduleName?: string
  transKey?: string
  transValue?: string
  status?: number
}

// === 状态定义 ===
const loading = ref(false)
const tableData = ref<TableRow[]>([])
const selectedRowKeys = ref([])
const selectedTranslationId = ref<number | undefined>(undefined)
const selectedTransKey = ref('')
const formVisible = ref(false)
const detailVisible = ref(false)
const languageFormVisible = ref(false)
const total = ref(0)
const showSearch = ref(false)
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})
const langCode = ref('zh_CN')

// === 查询参数 ===
const queryParams = ref({
  pageIndex: 1,
  pageSize: 10,
  moduleName: '',
  transKey: '',
  transValue: '',
  status: -1
})

// === 查询字段定义 ===
const queryFields = [
  {
    type: 'input' as const,
    name: 'transKey',
    label: t('core.translation.table.columns.transKey'),
    placeholder: t('core.translation.table.columns.transKey'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'input' as const,
    name: 'transValue',
    label: t('core.translation.table.columns.transValue'),
    placeholder: t('core.translation.table.columns.transValue'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'select' as const,
    name: 'moduleName',
    label: t('core.translation.table.columns.moduleName'),
    placeholder: t('core.translation.table.columns.moduleName'),
    props: {
      dictType: 'sys_translation_module',
      type: 'radio',
      allowClear: true
    }
  },
  {
    type: 'select' as const,
    name: 'status',
    label: t('core.translation.table.columns.status'),
    placeholder: t('core.translation.table.columns.status'),   
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      allowClear: true
    }
  }
]

// === 表格列定义 ===
const defaultColumns = ref<TableColumn[]>([
  {
    title: t('core.translation.table.columns.moduleName'),
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.translation.table.columns.transKey'),
    dataIndex: 'transKey',
    key: 'transKey',
    width: 200,
    ellipsis: true
  }
])

// 新增：动态列
const dynamicColumns = ref<TableColumn[]>([])

// 列设置相关
const columns = computed(() => {
  return dynamicColumns.value.filter(col => columnSettings.value[col.key])
})

// 初始化列设置
const initColumnSettings = () => {
  // 从localStorage获取列设置
  const savedSettings = localStorage.getItem('translationColumnSettings')
  if (savedSettings) {
    columnSettings.value = JSON.parse(savedSettings)
  } else {
    // 初始化所有列为true
    columnSettings.value = Object.fromEntries(dynamicColumns.value.map(col => [col.key, true]))
  }
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: CheckboxValueType[]) => {
  const settings: Record<string, boolean> = {}
  dynamicColumns.value.forEach(col => {
    settings[col.key] = checkedValue.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('translationColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  if (!dynamicColumns.value.length) {
    fetchData().then(() => {
      initColumnSettings()
      columnSettingVisible.value = true
    })
  } else {
    initColumnSettings()
    columnSettingVisible.value = true
  }
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    console.log('查询参数:', {
      ...queryParams.value
    })
    const res = await getHbtTransposedData(queryParams.value)
    if (res.data.code === 200) {
      const data = res.data.data as unknown as HbtPagedResult<HbtTransposedData>
      const rows = data.rows
      // 获取所有语言代码
      const langCodes = rows.length > 0 ? Object.keys(rows[0].translations || {}) : []
      
      // 生成动态列（只更新 dynamicColumns，不动 defaultColumns）
      dynamicColumns.value = [
        ...defaultColumns.value,
        ...langCodes.map(code => ({
          title: code,
          dataIndex: code,
          key: code,
          width: 200,
          ellipsis: true
        })),
        {
          title: t('table.columns.action'),
          key: 'action',
          width: 180,
          fixed: 'right'
        }
      ]
      initColumnSettings()
      
      // 更新表格数据
      tableData.value = rows.map(row => {
        const baseData = {
          moduleName: row.moduleName,
          transKey: row.transKey,
          translationId: row.translations?.[langCodes[0]]?.translationId
        }
        
        // 添加每个语言的翻译值
        if (row.translations) {
          Object.entries(row.translations).forEach(([langCode, translation]) => {
            (baseData as Record<string, string | number>)[langCode] = translation.transValue
          })
        }
        
        return baseData
      })
      
      total.value = data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载翻译列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = (values?: QueryValues) => {
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
    moduleName: '',
    transKey: '',
    transValue: '',
    status: -1
  }
  fetchData()
}

// 新增
const handleAdd = () => {
  selectedTranslationId.value = undefined
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.translationId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = async (record: TableRow) => {
  try {
    // 检查 translationId 是否有效
    if (!record.translationId) {
      // 如果没有 translationId，直接进入新建模式
      selectedTranslationId.value = undefined
      selectedTransKey.value = record.transKey
      formVisible.value = true
      return
    }

    // 检查是否存在该翻译
    const res = await getHbtTranslation(record.translationId)
    if (res.data.code === 200) {
      // 存在则更新
      selectedTranslationId.value = record.translationId
      selectedTransKey.value = record.transKey
      formVisible.value = true
    } else {
      // 不存在则新建
      selectedTranslationId.value = undefined
      selectedTransKey.value = record.transKey
      formVisible.value = true
    }
  } catch (error: any) {
    console.error('检查翻译记录失败:', error)
    // 如果是记录不存在的错误，直接进入新建模式
    if (error.response?.data?.code === 'Core.Translation.NotFound') {
      selectedTranslationId.value = undefined
      selectedTransKey.value = record.transKey
      formVisible.value = true
    } else {
      message.error(t('common.failed'))
    }
  }
}

// 查看
const handleView = (record: TableRow) => {
  selectedTranslationId.value = record.translationId
  selectedTransKey.value = record.transKey
  detailVisible.value = true
}

// 删除
const handleDelete = async (record: TableRow) => {
  try {
    const res = await deleteHbtTranslation(record.translationId)
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
    const res = await batchDeleteHbtTranslation(selectedRowKeys.value.map(key => Number(key)))
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
  // TODO: 实现导入功能
}

// 下载模板
const handleTemplate = () => {
  // TODO: 实现下载模板功能
}

// 导出
const handleExport = () => {
  // TODO: 实现导出功能
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

// 新增语言
const handleLang = () => {
  languageFormVisible.value = true
}

// 语言表单提交成功
const handleLanguageSuccess = () => {
  languageFormVisible.value = false
  fetchData()
}

// === 生命周期 ===
onMounted(() => {
  initColumnSettings()
  fetchData()
})

defineExpose({})
</script>

<style lang="less" scoped>
.translation-manager {
  padding: 24px;
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