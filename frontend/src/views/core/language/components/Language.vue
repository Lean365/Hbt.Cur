<template>
  <hbt-modal
    :title="t('core.language.title')"
    :open="open"
    width="800px"
    @cancel="handleCancel"
  >
    <!-- 查询表单 -->
    <hbt-query
      v-model:show="showSearch"
      :loading="languageLoading"
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
      :disabled-edit="selectedLanguageId === undefined"
      :disabled-delete="selectedLanguageId === undefined"
      @add="handleAddLanguage"
      @edit="handleEditLanguage"
      @delete="handleDeleteLanguage"
      @import="handleImportLanguage"
      @template="handleTemplateLanguage"
      @export="handleExportLanguage"
      @refresh="fetchLanguageList"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"      
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="languageLoading"
      :data-source="languageList"
      :columns="visibleColumns"
      :pagination="false"
      :scroll="{ y: 'calc(70vh - 200px)' }"
      :row-key="(record: HbtLanguage) => String(record.languageId)"
      v-model:selectedRowKeys="selectedLanguageKeys"
      :row-selection="{
        type: 'radio',
        columnWidth: 60
      }"
      @change="handleLanguageSelect"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <template v-else-if="column.dataIndex === 'langCode'">
          <a @click.stop.prevent="handleShowTranslationModal(record)">{{ record.langCode }}</a>
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
            @view="handleViewLanguage"
            @edit="handleEditLanguage"
            @delete="handleDeleteLanguage"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="languageTotal"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handleLanguagePageChange"
      @showSizeChange="handleLanguageSizeChange"
    />

    <!-- 语言表单对话框 -->
    <Language-form
      v-model:open="languageFormVisible"
      :title="languageFormTitle"
      :language-id="selectedLanguageId"
      @success="handleLanguageSuccess"
    />

    <!-- 语言详情对话框 -->
    <Language-detail
      v-model:open="languageDetailVisible"
      :language-id="selectedLanguageId || 0"
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
  </hbt-modal>
</template>


<script lang="ts" setup>
import { ref, reactive, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtLanguage, HbtLanguageQuery } from '@/types/core/language'
import {
  getHbtLanguageList,
  getHbtLanguage,
  createHbtLanguage,
  updateHbtLanguage,
  deleteHbtLanguage,
  batchDeleteHbtLanguage,
  importHbtLanguage,
  exportHbtLanguage,
  getHbtLanguageTemplate
} from '@/api/core/language'
import LanguageForm from './LanguageForm.vue'
import LanguageDetail from './LanguageDetail.vue'


const { t } = useI18n()

const props = defineProps<{
  open: boolean
}>()

const emit = defineEmits(['update:open'])

// === 语言状态定义 ===
const languageLoading = ref(false)
const languageList = ref<HbtLanguage[]>([])
const selectedLanguageKeys = ref<string[]>([])
const selectedLanguageId = ref<number>()
const languageFormVisible = ref(false)
const languageFormTitle = ref('')
const languageDetailVisible = ref(false)
const languageTotal = ref(0)

// === Translation相关状态 ===
const translationVisible = ref(false)
const currentLanguage = ref<HbtLanguage>()

// === 语言查询参数 ===
const queryParams = ref<HbtLanguageQuery>({
  pageIndex: 1,
  pageSize: 10,
  langCode: '',
  langName: '',
  isDefault: -1,
  isBuiltin: -1,
  status: -1
})

// === 语言搜索表单配置 ===
const queryFields = [
  {
    type: 'input' as const,
    name: 'langCode',
    label: t('core.language.fields.langCode.label'),
    placeholder: t('core.language.fields.langCode.placeholder')
  },
  {
    type: 'input' as const,
    name: 'langName',
    label: t('core.language.fields.langName.label'),
    placeholder: t('core.language.fields.langName.placeholder')
  },
  {
    type: 'select' as const,
    name: 'isBuiltin',
    label: t('core.language.fields.isBuiltin.label'),
    placeholder: t('core.language.fields.isBuiltin.placeholder'),
    props: {
      dictType: 'sys_yes_no',
      type: 'radio'
    }
  },
  {
    type: 'select' as const,
    name: 'isDefault',
    label: t('core.language.fields.isDefault.label'),
    placeholder: t('core.language.fields.isDefault.placeholder'),
    props: {
      dictType: 'sys_yes_no',
      type: 'radio'
    }
  },
  {
    type: 'select' as const,
    name: 'status',
    label: t('core.language.fields.status.label'),
    placeholder: t('core.language.fields.status.placeholder'),
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    }
  }
]

// === 语言表格列定义 ===
const columns = [
  {
    title: t('core.language.table.columns.langCode'),
    dataIndex: 'langCode',
    key: 'langCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.language.table.columns.langName'),
    dataIndex: 'langName',
    key: 'langName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.language.table.columns.langIcon'),
    dataIndex: 'langIcon',
    key: 'langIcon',
    width: 100
  },
  {
    title: t('core.language.table.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    sorter: true
  },
  {
    title: t('core.language.table.columns.status'),
    dataIndex: 'status',
    key: 'status',
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
    title: t('table.columns.isDeleted'),
    dataIndex: 'isDeleted',
    key: 'isDeleted',
    width: 100
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

// 列设置相关
const defaultColumns = columns
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// === 语言方法定义 ===
const fetchLanguageList = async () => {
  try {
    languageLoading.value = true
    const res = await getHbtLanguageList(queryParams.value)
    if (res.data.code === 200) {
      languageList.value = res.data.data.rows
      languageTotal.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载语言列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    languageLoading.value = false
  }
}

const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  fetchLanguageList()
}

const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    langCode: '',
    langName: '',
    isDefault: -1,
    isBuiltin: -1,
    status: -1
  }
  fetchLanguageList()
}

const handleAddLanguage = () => {
  selectedLanguageId.value = undefined
  languageFormTitle.value = t('common.title.create')
  languageFormVisible.value = true
}

const handleEditLanguage = (record?: HbtLanguage) => {
  if (record) {
    selectedLanguageId.value = record.languageId
  }
  languageFormTitle.value = t('common.title.edit')
  languageFormVisible.value = true
}

const handleViewLanguage = (record: HbtLanguage) => {
  selectedLanguageId.value = record.languageId
  languageDetailVisible.value = true
}

const handleDeleteLanguage = async (record?: HbtLanguage) => {
  const id = record ? record.languageId : selectedLanguageId.value
  if (!id) return
  try {
    const res = await deleteHbtLanguage(id)
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchLanguageList()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

const handleImportLanguage = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      const formData = new FormData()
      formData.append('file', file)
      const res = await importHbtLanguage(file)
      if (res.data.code === 200) {
        message.success(t('common.import.success'))
        fetchLanguageList()
      } else {
        message.error(res.data.msg || t('common.import.failed'))
      }
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}

const handleTemplateLanguage = async () => {
  try {
    const res = await getHbtLanguageTemplate()
    const blob = new Blob([res.data], { type: 'application/vnd.ms-excel' })
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(blob)
    link.download = '语言数据导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

const handleExportLanguage = async () => {
  try {
    const res = await exportHbtLanguage({
      ...queryParams.value
    })
    // 动态获取文件名
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    let fileName = ''
    if (disposition) {
      let match = disposition.match(/filename\*=UTF-8''([^;]+)/)
      if (match && match[1]) {
        fileName = decodeURIComponent(match[1])
      } else {
        match = disposition.match(/filename="?([^";]+)"?/)
        if (match && match[1]) {
          fileName = match[1]
        }
      }
    }
    if (!fileName) {
      fileName = `语言数据_${new Date().getTime()}.xlsx`
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

const handleLanguagePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchLanguageList()
}

const handleLanguageSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = current
  queryParams.value.pageSize = size
  fetchLanguageList()
}

const handleLanguageSuccess = () => {
  languageFormVisible.value = false
  fetchLanguageList()
}

const handleShowTranslationModal = (record: HbtLanguage) => {
  currentLanguage.value = record
  translationVisible.value = true

  console.log('currentLanguage:', currentLanguage.value) // 这里输出
}

const handleTranslationCancel = () => {
  translationVisible.value = false
  currentLanguage.value = undefined
}

// === 工具栏相关功能 ===
const showSearch = ref(false)
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('languageColumnSettings')

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
  localStorage.setItem('languageColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

const handleLanguageSelect = (selectedKeys: (string | number)[], selectedRows: any[]) => {
  if (selectedKeys.length > 0) {
    const record = languageList.value.find(item => String(item.languageId) === String(selectedKeys[0]))
    if (record) {
      selectedLanguageId.value = record.languageId
    }
  } else {
    selectedLanguageId.value = undefined
  }
}

const handleCancel = () => {
  emit('update:open', false)
}

// === 生命周期 ===
onMounted(() => {
  initColumnSettings()
  fetchLanguageList()
})
</script>

<style lang="less" scoped>
.language-container {
  padding: 16px;
  background-color: #fff;
  border-radius: 4px;
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
