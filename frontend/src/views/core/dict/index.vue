//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典管理主页面
//===================================================================

<template>
  <div class="dict-container">

      <!-- 查询表单 -->
      <hbt-query
        v-model:show="showSearch"
        :loading="dictTypeLoading"
        :query-fields="queryFields"
        @search="handleQuery"
        @reset="handleResetDictType"
      />

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
        :export-permission="['core:dict:export']"
        :disabled-edit="selectedDictTypeId === undefined"
        :disabled-delete="selectedDictTypeId === undefined"
        @add="handleAddDictType"
        @edit="handleEditDictType"
        @delete="handleDeleteDictType"
        @import="handleImportDictType"
        @template="handleTemplateDictType"
        @export="handleExportDictType"
        @refresh="fetchDictTypeList"
        @column-setting="handleColumnSetting"
        @toggle-search="handleSearchClick"
        @toggle-fullscreen="toggleFullscreen"
      />

      <!-- 数据表格 -->
      <hbt-table
        :loading="dictTypeLoading"
        :data-source="dictTypeList"
        :columns="columns"
        :pagination="false"
        :scroll="{ y: 'calc(70vh - 200px)' }"
        :row-key="(record: HbtDictType) => String(record.dictTypeId)"
        v-model:selectedRowKeys="selectedDictTypeKeys"
        :row-selection="{
          type: 'radio',
          columnWidth: 60
        }"
        @change="handleDictTypeSelect"
      >
        <!-- 状态列 -->
        <template #bodyCell="{ column, record }">
          <template v-if="column.dataIndex === 'status'">
            <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
          </template>
          <template v-else-if="column.dataIndex === 'dictType'">
            <a @click.stop="handleShowDictDataModal(record)">{{ record.dictType }}</a>
          </template>
          <!-- 操作列 -->
          <template v-if="column.key === 'action'">
            <hbt-operation
              :record="record"
              :show-view="true"
              :view-permission="['core:dicttype:query']"
              :show-edit="true"
              :edit-permission="['core:dicttype:update']"
              :show-delete="true"
              :delete-permission="['core:dicttype:delete']"
              size="small"
              @view="handleViewDictType"
              @edit="handleEditDictType"
              @delete="handleDeleteDictType"
            />
          </template>
        </template>
      </hbt-table>

      <!-- 分页组件 -->
      <hbt-pagination
        v-model:current="queryParams.pageIndex"
        v-model:pageSize="queryParams.pageSize"
        :total="dictTypeTotal"
        :show-size-changer="true"
        :show-quick-jumper="true"
        :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
        @change="handleDictTypePageChange"
        @showSizeChange="handleDictTypeSizeChange"
      />


    <!-- 字典类型表单对话框 -->
    <dict-type-form
      v-model:open="dictTypeFormVisible"
      :title="dictTypeFormTitle"
      :dict-type-id="selectedDictTypeId"
      @success="handleDictTypeSuccess"
    />

    <!-- 字典类型详情对话框 -->
    <dict-type-detail
      v-model:open="dictTypeDetailVisible"
      :dict-type-id="selectedDictTypeId"
    />

    <!-- 字典数据表格对话框 -->
    <DictData
      v-model:open="dictDataVisible"
      :dict-type-id="currentDictType?.dictTypeId"
      :dict-type="currentDictType"
      @cancel="handleDictDataCancel"
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
import { ref, reactive, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtDictType, HbtDictTypeQuery } from '@/types/core/dictType'
import {
  getHbtDictTypeList,
  getHbtDictType,
  createHbtDictType,
  updateHbtDictType,
  deleteHbtDictType,
  batchDeleteHbtDictType,
  importHbtDictType,
  exportHbtDictType,
  getHbtDictTypeTemplate
} from '@/api/core/dictType'
import DictTypeForm from './components/DictTypeForm.vue'
import DictTypeDetail from './components/DictTypeDetail.vue'
import DictData from './components/DictData.vue'

const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(false)

// === 字典类型状态定义 ===
const dictTypeLoading = ref(false)
const dictTypeList = ref<HbtDictType[]>([])
const selectedDictTypeKeys = ref<string[]>([])
const selectedDictTypeId = ref<number>()
const dictTypeFormVisible = ref(false)
const dictTypeFormTitle = ref('')
const dictTypeDetailVisible = ref(false)
const dictTypeTotal = ref(0)

// === DictData相关状态 ===
const dictDataVisible = ref(false)
const currentDictType = ref<HbtDictType>()

// === 字典类型查询参数 ===
const queryParams = ref<HbtDictTypeQuery>({
  pageIndex: 1,
  pageSize: 10,
  dictName: '',
  dictType: '',
  dictCategory: -1,
  isBuiltin: -1,
  status: -1,
})

// === 字典类型搜索表单配置 ===
const queryFields = [
  {
    type: 'input' as const,
    name: 'dictName',
    label: t('core.dict.dictTypes.fields.dictName.label'),
    placeholder: t('core.dict.dictTypes.fields.dictName.placeholder')
  },
  {
    type: 'input' as const,
    name: 'dictType',
    label: t('core.dict.dictTypes.fields.dictType.label'),
    placeholder: t('core.dict.dictTypes.fields.dictType.placeholder')
  },
  {
    type: 'select' as const,
    name: 'dictCategory',
    label: t('core.dict.dictTypes.fields.dictCategory.label'),
    placeholder: t('core.dict.dictTypes.fields.dictCategory.placeholder'),
    props: {
      dictType: 'sys_dict_category',
      type: 'radio'
    }
  },
  {
    type: 'select' as const,
    name: 'isBuiltin',
    label: t('core.dict.dictTypes.fields.isBuiltin.label'),
    placeholder: t('core.dict.dictTypes.fields.isBuiltin.placeholder'),
    props: {
      dictType: 'sys_yes_no',
      type: 'radio'
    }
  },
  {
    type: 'select' as const,
    name: 'status',
    label: t('core.dict.dictTypes.fields.status.label'),
    placeholder: t('core.dict.dictTypes.fields.status.placeholder'),
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    }
  }
]

// === 字典类型表格列定义 ===
const dictTypeColumns = [
  {
    title: t('core.dict.dictTypes.table.columns.dictName'),
    dataIndex: 'dictName',
    key: 'dictName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.dict.dictTypes.table.columns.dictType'),
    dataIndex: 'dictType',
    key: 'dictType',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.dict.dictTypes.table.columns.dictCategory'),
    dataIndex: 'dictCategory',
    key: 'dictCategory',
    width: 100
  },
  {
    title: t('core.dict.dictTypes.table.columns.isBuiltin'),
    dataIndex: 'isBuiltin',
    key: 'isBuiltin',
    width: 100
  },
  {
    title: t('core.dict.dictTypes.table.columns.sqlScript'),
    dataIndex: 'sqlScript',
    key: 'sqlScript',
    width: 100
  },
  {
    title: t('core.dict.dictTypes.table.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('core.dict.dictTypes.table.columns.status'),
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
const columnSettingVisible = ref(false)
const defaultColumns = dictTypeColumns
const columnSettings = ref<Record<string, boolean>>({})
const columns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('dictTypeColumnSettings')

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
  localStorage.setItem('dictTypeColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
  if (!visible) {
    handleResetDictType()
  }
}

// 监听搜索按钮点击
const handleSearchClick = () => {
  toggleSearch(!showSearch.value)
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// === 字典类型方法定义 ===
// 获取字典类型列表
const fetchDictTypeList = async () => {
  try {
    dictTypeLoading.value = true
    console.log('查询参数:', {
      ...queryParams.value
    })
    const res = await getHbtDictTypeList(queryParams.value)
    if (res.data.code === 200) {
      dictTypeList.value = res.data.data.rows
      dictTypeTotal.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载字典类型列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    dictTypeLoading.value = false
  }
}

// 搜索字典类型
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  fetchDictTypeList()
}

// 重置字典类型搜索
const handleResetDictType = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    dictName: '',
    dictType: '',
    dictCategory: -1,
    isBuiltin: -1,
    status: -1,
  }
  fetchDictTypeList()
}

// 新增字典类型
const handleAddDictType = () => {
  selectedDictTypeId.value = undefined
  dictTypeFormTitle.value = t('common.title.create')
  dictTypeFormVisible.value = true
}

// 编辑字典类型
const handleEditDictType = (record?: HbtDictType) => {
  if (record) {
    selectedDictTypeId.value = record.dictTypeId
  }
  dictTypeFormTitle.value = t('common.title.edit')
  dictTypeFormVisible.value = true
}

// 查看字典类型
const handleViewDictType = (record: HbtDictType) => {
  selectedDictTypeId.value = record.dictTypeId
  dictTypeDetailVisible.value = true
}

// 删除字典类型
const handleDeleteDictType = async (record?: HbtDictType) => {
  const id = record ? record.dictTypeId : selectedDictTypeId.value
  if (!id) return

  try {
    const res = await deleteHbtDictType(id)
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchDictTypeList()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 导入字典类型
const handleImportDictType = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      const formData = new FormData()
      formData.append('file', file)
      const res = await importHbtDictType(formData)
      if (res.data.code === 200) {
        message.success(t('common.import.success'))
        fetchDictTypeList()
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

// 下载字典类型模板
const handleTemplateDictType = async () => {
  try {
    const res = await getHbtDictTypeTemplate()
    const blob = new Blob([res.data], { type: 'application/vnd.ms-excel' })
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(blob)
    link.download = '字典类型导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

// 导出字典类型
const handleExportDictType = async () => {
  try {
    const res = await exportHbtDictType({
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

// 字典类型页码变化
const handleDictTypePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchDictTypeList()
}

// 字典类型每页条数变化
const handleDictTypeSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = current
  queryParams.value.pageSize = size
  fetchDictTypeList()
}

// 字典类型选择变化
const handleDictTypeSelect = (selectedKeys: string[]) => {
  
  if (selectedKeys.length > 0) {
    const record = dictTypeList.value.find(item => String(item.dictTypeId) === selectedKeys[0])
    if (record) {
      selectedDictTypeId.value = record.dictTypeId
      
    }
  } else {
    selectedDictTypeId.value = undefined
  }
}

// 字典类型表单提交成功
const handleDictTypeSuccess = () => {
  dictTypeFormVisible.value = false
  fetchDictTypeList()
}

// 显示字典数据对话框
const handleShowDictDataModal = (record: HbtDictType) => {
  currentDictType.value = record
  dictDataVisible.value = true
}

// 字典数据对话框取消
const handleDictDataCancel = () => {
  dictDataVisible.value = false
  currentDictType.value = undefined
}

// === 生命周期 ===
onMounted(() => {
  initColumnSettings()
  fetchDictTypeList()
})
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