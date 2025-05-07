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
    :title="t('admin.language.translation.title', { name: language?.langName })"
    :open="dialogVisible"
    width="900px"
    append-to-body
    destroy-on-close
  >
    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['admin:translation:create']"
      :show-edit="true"
      :edit-permission="['admin:translation:update']"
      :show-delete="true"
      :delete-permission="['admin:translation:delete']"
      :show-import="true"
      :import-permission="['admin:translation:import']"
      :show-export="true"
      :export-permission="['admin:translation:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
      @export="handleExport"
      @refresh="fetchData"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content', y: 400 }"
      :row-key="(record: HbtTranslation) => String(record.id)"
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
            :view-permission="['admin:translation:query']"
            :show-edit="true"
            :edit-permission="['admin:translation:update']"
            :show-delete="true"
            :delete-permission="['admin:translation:delete']"
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
      v-model:visible="formVisible"
      :translation-id="selectedTranslationId"
      :lang-code="langCode"
      @success="handleSuccess"
    />

    <!-- 翻译详情对话框 -->
    <translation-detail
      v-model:visible="detailVisible"
      :translation-id="selectedTranslationId"
    />
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted, h } from 'vue'
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
    type: String,
    default: ''
  },
  language: {
    type: Object as () => HbtLanguage,
    default: undefined
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
const dialogVisible = ref(props.visible)

// === 查询参数 ===
const queryParams = ref<HbtTranslationQuery>({
  pageIndex: 1,
  pageSize: 10,
  langCode: props.langCode,
  transKey: '',
  transValue: '',
  status: undefined
})

// === 表格列定义 ===
const columns = [
  {
    title: t('admin.language.translation.moduleName'),
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('admin.language.translation.transKey'),
    dataIndex: 'transKey',
    key: 'transKey',
    width: 200,
    ellipsis: true
  },
  {
    title: t('admin.language.translation.transValue'),
    dataIndex: 'transValue',
    key: 'transValue',
    width: 300,
    ellipsis: true
  },
  {
    title: t('admin.language.translation.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('admin.language.translation.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('admin.language.translation.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: t('common.createTime'),
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

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  if (!props.langCode) return
  
  try {
    loading.value = true
    const res = await getHbtTranslationList(queryParams.value)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
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
  const record = tableData.value.find(item => String(item.id) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: HbtTranslation) => {
  selectedTranslationId.value = record.id
  formVisible.value = true
}

// 查看
const handleView = (record: HbtTranslation) => {
  selectedTranslationId.value = record.id
  detailVisible.value = true
}

// 删除
const handleDelete = async (record: HbtTranslation) => {
  try {
    const res = await deleteHbtTranslation(record.id)
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchData()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
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
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
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

// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  dialogVisible.value = val
})

// 监听对话框可见性变化，同步到父组件
watch(() => dialogVisible.value, (val) => {
  emit('update:visible', val)
})

// 监听语言代码变化，加载数据
watch(() => props.langCode, (val) => {
  if (val) {
    queryParams.value.langCode = val
    fetchData()
  }
}, { immediate: true })

// === 生命周期 ===
onMounted(() => {
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
</style> 