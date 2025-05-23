//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : DictData.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据管理组件
//===================================================================

<template>
  <hbt-modal
    :open="visible"
    :title="t('core.dict.dictDatas.title') + ' - ' + props.dictType?.dictName"
    :footer="null"
    width="900px"
    append-to-body
    destroy-on-close
  >
    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['core:dict:create']"
      :show-edit="true"
      :edit-permission="['core:dict:update']"
      :show-delete="true"
      :delete-permission="['core:dict:delete']"
      :show-import="true"
      :import-permission="['core:dict:import']"
      :show-export="true"
      :export-permission="['core:dictdata:export']"
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
      :row-key="(record: HbtDictData) => String(record.dictDataId)"
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
            :view-permission="['core:dict:query']"
            :show-edit="true"
            :edit-permission="['core:dict:update']"
            :show-delete="true"
            :delete-permission="['core:dict:delete']"
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

    <!-- 字典数据表单对话框 -->
    <dict-data-form
      v-model:open="formVisible"
      :title="formTitle"
      :dict-type="props.dictType?.dictType || ''"
      :dict-data-id="selectedDictDataId"
      @success="handleSuccess"
    />

    <!-- 字典数据详情对话框 -->
    <dict-data-detail
      v-model:open="detailVisible"
      :dict-data-id="selectedDictDataId"
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
import type { HbtDictType } from '@/types/core/dictType'
import type { HbtDictData, HbtDictDataQuery } from '@/types/core/dictData'
import {
  getHbtDictDataList,
  getHbtDictData,
  createHbtDictData,
  updateHbtDictData,
  deleteHbtDictData,
  batchDeleteHbtDictData,
  importHbtDictData,
  exportHbtDictData,
  getHbtDictDataTemplate
} from '@/api/core/dictData'
import DictDataForm from './DictDataForm.vue'
import DictDataDetail from './DictDataDetail.vue'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  dictTypeId: {
    type: Number,
    default: undefined
  },
  dictType: {
    type: Object as () => HbtDictType,
    default: undefined
  }
})

const emit = defineEmits(['update:visible'])

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const tableData = ref<HbtDictData[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedDictDataId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const total = ref(0)
const dialogVisible = ref(props.visible)
const showSearch = ref(true)

// 列设置相关
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})

// === 查询参数 ===
const queryParams = ref<HbtDictDataQuery>({
  pageIndex: 1,
  pageSize: 10,
  dictType: props.dictType?.dictType || '',
  status: undefined,
  orderByColumn: undefined,
  orderType: undefined
})

// === 表格列定义 ===
const columns = [
  {
    title: t('core.dict.dictDatas.table.columns.dictDataId'),
    dataIndex: 'dictDataId',
    key: 'dictDataId',
    width: 100
  },
  {
    title: t('core.dict.dictDatas.table.columns.dictType'),
    dataIndex: 'dictType',
    key: 'dictType',
    width: 100
  },
  {
    title: t('core.dict.dictDatas.table.columns.dictLabel'),
    dataIndex: 'dictLabel',
    key: 'dictLabel',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.transKey'),
    dataIndex: 'transKey',
    key: 'transKey',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.listClass'),
    dataIndex: 'listClass',
    key: 'listClass',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.cssClass'),
    dataIndex: 'cssClass',
    key: 'cssClass',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.dictValue'),
    dataIndex: 'dictValue',
    key: 'dictValue',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.extLabel'),
    dataIndex: 'extLabel',
    key: 'extLabel',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.extValue'),
    dataIndex: 'extValue',
    key: 'extValue',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('core.dict.dictDatas.table.columns.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('core.dict.dictDatas.table.columns.tenantId'),
    dataIndex: 'tenantId',
    key: 'tenantId',
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

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  if (!props.dictTypeId) return
  
  try {
    loading.value = true
    const res = await getHbtDictDataList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载字典数据列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 新增
const handleAdd = () => {
  selectedDictDataId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.dictDataId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: HbtDictData) => {
  selectedDictDataId.value = record.dictDataId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 查看
const handleView = (record: HbtDictData) => {
  selectedDictDataId.value = record.dictDataId
  detailVisible.value = true
}

// 删除
const handleDelete = async (record: HbtDictData) => {
  try {
    const res = await deleteHbtDictData(record.dictDataId)
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
    const res = await batchDeleteHbtDictData(selectedRowKeys.value.map(key => Number(key)))
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
      const formData = new FormData()
      formData.append('file', file)
      const res = await importHbtDictData(formData)
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

// 下载模板
const handleTemplate = async () => {
  try {
    const res = await getHbtDictDataTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `字典数据导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 导出
const handleExport = async () => {
  try {
    const res = await exportHbtDictData({
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
      fileName = `字典数据_${new Date().getTime()}.xlsx`
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
  dialogVisible.value = false
}

// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  dialogVisible.value = val
})

watch(() => dialogVisible.value, (val) => {
  emit('update:visible', val)
})

// 监听字典类型变化，加载数据
watch(() => props.dictType?.dictType, (val) => {
  if (val) {
    queryParams.value.dictType = val
    fetchData()
  }
}, { immediate: true })

// === 生命周期 ===
onMounted(() => {
  // 初始化列设置
  initColumnSettings()
  if (props.dictTypeId) {
    fetchData()
  }
})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('dictDataColumnSettings')

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
  localStorage.setItem('dictDataColumnSettings', JSON.stringify(settings))
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
</script>

<style lang="less" scoped>
.dict-container {
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