//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : Translation.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译管理组件
//===================================================================

<template>
  <hbt-modal
   :open="visible"
    :title="t('core.translation.title' )+ ' - ' + props.langCode?.langName"
    :footer="null"
    width="900px"
    append-to-body
    destroy-on-close
  >
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
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="visibleColumns"
      :pagination="false"
      :scroll="{ x: 'max-content', y: 400 }"
      :row-key="(record: HbtTranslation) => String(record.translationId)"
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
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
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
      :lang-code="props.langCode?.langCode || ''"
      @success="handleSuccess"
    />

    <!-- 翻译详情对话框 -->
    <translation-detail
      v-model:open="detailVisible"
      :translation-id="selectedTranslationId"
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
import { ref, reactive, watch, onMounted, h, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { HbtLanguage } from '@/types/core/language'
import type { HbtTranslation, HbtTranslationQuery } from '@/types/core/translation'
import {
  getHbtTranslationList,
  getHbtTranslation,
  createHbtTranslation,
  updateHbtTranslation,
  deleteHbtTranslation,
  batchDeleteHbtTranslation,
  importHbtTranslation,
  exportHbtTranslation,
  getHbtTranslationTemplate
} from '@/api/core/translation'
import TranslationForm from './TranslationForm.vue'
import TranslationDetail from './TranslationDetail.vue'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  langCode: {
    type: Object as () => HbtLanguage,
    required: true
  }
})
const emit = defineEmits(['update:visible'])

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const tableData = ref<HbtTranslation[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedTranslationId = ref<number | undefined>(undefined)
const formVisible = ref(false)
const detailVisible = ref(false)
const total = ref(0)
const showSearch = ref(true)
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})

// === 查询参数 ===
const queryParams = ref<HbtTranslationQuery>({
  pageIndex: 1,
  pageSize: 10,
  langCode: props.langCode?.langCode || '',
  transKey: '',
  transValue: '',
  status: undefined
})

// === 表格列定义 ===
const columns = [
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
  },
  {
    title: t('core.translation.table.columns.transValue'),
    dataIndex: 'transValue',
    key: 'transValue',
    width: 300,
    ellipsis: true
  },
  {
    title: t('core.translation.table.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('core.translation.table.columns.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('core.translation.table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: t('common.table.createTime'),
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

// 列设置相关
const defaultColumns = columns
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('translationColumnSettings')

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
  localStorage.setItem('translationColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  const element = document.documentElement
  if (document.fullscreenElement) {
    document.exitFullscreen()
  } else {
    element.requestFullscreen()
  }
}

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  if (!props.langCode) return
  
  try {
    loading.value = true
    const res = await getHbtTranslationList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
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
const handleEdit = (record: HbtTranslation) => {
  selectedTranslationId.value = record.translationId
  formVisible.value = true
}

// 查看
const handleView = (record: HbtTranslation) => {
  selectedTranslationId.value = record.translationId
  detailVisible.value = true
}

// 删除
const handleDelete = async (record: HbtTranslation) => {
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

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  if (!val) {
    formVisible.value = false
    detailVisible.value = false
  }
})

// 监听对话框可见性变化，同步到父组件
watch([formVisible, detailVisible], ([formVal, detailVal]) => {
  if (!formVal && !detailVal) {
    emit('update:visible', false)
  }
})

// 监听语言代码变化，加载数据
watch(() => props.langCode, (val) => {
  if (val) {
    queryParams.value.langCode = val?.langCode || ''
    fetchData()
  }
}, { immediate: true })

// === 生命周期 ===
onMounted(() => {
  initColumnSettings()
  if (props.langCode) {
    fetchData()
  }
})
</script>

<style lang="less" scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
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