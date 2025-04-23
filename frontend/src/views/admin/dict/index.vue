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
    <a-row :gutter="16">
      <!-- 字典类型列表 -->
      <a-col :span="8">
        <a-card title="字典类型列表" :bordered="false">
          <!-- 查询表单 -->
          <hbt-query
            :loading="dictTypeLoading"
            :query-fields="dictTypeSearchFormItems"
            v-model:form-data="dictTypeQueryParams"
            @search="handleSearchDictType"
            @reset="handleResetDictType"
          />

          <!-- 工具栏 -->
          <hbt-toolbar
            :show-add="true"
            :add-permission="['admin:dict:create']"
            :show-edit="true"
            :edit-permission="['admin:dict:update']"
            :show-delete="true"
            :delete-permission="['admin:dict:delete']"
            :show-import="true"
            :import-permission="['admin:dict:import']"
            :show-export="true"
            :export-permission="['admin:dict:export']"
            :disabled-edit="selectedDictTypeId === undefined"
            :disabled-delete="selectedDictTypeId === undefined"
            @add="handleAddDictType"
            @edit="handleEditDictType"
            @delete="handleDeleteDictType"
            @import="handleImportDictType"
            @template="handleTemplateDictType"
            @export="handleExportDictType"
            @refresh="fetchDictTypeList"
          />

          <!-- 数据表格 -->
          <hbt-table
            :loading="dictTypeLoading"
            :data-source="dictTypeList"
            :columns="dictTypeColumns"
            :pagination="false"
            :scroll="{ y: 'calc(100vh - 350px)' }"
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

              <!-- 操作列 -->
              <template v-if="column.key === 'action'">
                <hbt-operation
                  :record="record"
                  :show-view="true"
                  :view-permission="['admin:dicttype:query']"
                  :show-edit="true"
                  :edit-permission="['admin:dicttype:update']"
                  :show-delete="true"
                  :delete-permission="['admin:dicttype:delete']"
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
            v-model:current="dictTypeQueryParams.pageIndex"
            v-model:pageSize="dictTypeQueryParams.pageSize"
            :total="dictTypeTotal"
            :show-size-changer="true"
            :show-quick-jumper="true"
            :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
            @change="handleDictTypePageChange"
            @showSizeChange="handleDictTypeSizeChange"
          />
        </a-card>
      </a-col>

      <!-- 字典数据列表 -->
      <a-col :span="16">
        <a-card :title="dictDataTitle" :bordered="false">
          <!-- 查询表单 -->
          <hbt-query
            :loading="dictDataLoading"
            :query-fields="dictDataSearchFormItems"
            v-model:form-data="dictDataQueryParams"
            @search="handleSearchDictData"
            @reset="handleResetDictData"
          />

          <!-- 工具栏 -->
          <hbt-toolbar
            :show-add="true"
            :add-permission="['admin:dictdata:create']"
            :show-edit="true"
            :edit-permission="['admin:dictdata:update']"
            :show-delete="true"
            :delete-permission="['admin:dictdata:delete']"
            :show-import="true"
            :import-permission="['admin:dictdata:import']"
            :show-export="true"
            :export-permission="['admin:dictdata:export']"
            :disabled-add="selectedDictTypeId === undefined"
            :disabled-edit="selectedDictDataKeys.length !== 1"
            :disabled-delete="selectedDictDataKeys.length === 0"
            :disabled-import="selectedDictTypeId === undefined"
            :disabled-export="selectedDictTypeId === undefined"
            @add="handleAddDictData"
            @edit="handleEditDictData"
            @delete="handleBatchDeleteDictData"
            @import="handleImportDictData"
            @template="handleTemplateDictData"
            @export="handleExportDictData"
            @refresh="fetchDictDataList"
          />

          <!-- 数据表格 -->
          <hbt-table
            :loading="dictDataLoading"
            :data-source="dictDataList"
            :columns="dictDataColumns"
            :pagination="false"
            :scroll="{ y: 'calc(100vh - 350px)' }"
            :row-key="(record: HbtDictData) => String(record.dictDataId)"
            v-model:selectedRowKeys="selectedDictDataKeys"
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
                  :view-permission="['admin:dictdata:query']"
                  :show-edit="true"
                  :edit-permission="['admin:dictdata:update']"
                  :show-delete="true"
                  :delete-permission="['admin:dictdata:delete']"
                  size="small"
                  @view="handleViewDictData"
                  @edit="handleEditDictData"
                  @delete="handleDeleteDictData"
                />
              </template>
            </template>
          </hbt-table>

          <!-- 分页组件 -->
          <hbt-pagination
            v-model:current="dictDataQueryParams.pageIndex"
            v-model:pageSize="dictDataQueryParams.pageSize"
            :total="dictDataTotal"
            :show-size-changer="true"
            :show-quick-jumper="true"
            :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
            @change="handleDictDataPageChange"
            @showSizeChange="handleDictDataSizeChange"
          />
        </a-card>
      </a-col>
    </a-row>

    <!-- 字典类型表单对话框 -->
    <dict-type-form
      v-model:visible="dictTypeFormVisible"
      :title="dictTypeFormTitle"
      :dict-type-id="selectedDictTypeId"
      @success="handleDictTypeSuccess"
    />

    <!-- 字典类型详情对话框 -->
    <dict-type-detail
      v-model:visible="dictTypeDetailVisible"
      :dict-type-id="selectedDictTypeId"
    />

    <!-- 字典数据表单对话框 -->
    <dict-data-form
      v-model:visible="dictDataFormVisible"
      :title="dictDataFormTitle"
      :dict-type="selectedDictType"
      :dict-data-id="selectedDictDataId"
      @success="handleDictDataSuccess"
    />

    <!-- 字典数据详情对话框 -->
    <dict-data-detail
      v-model:visible="dictDataDetailVisible"
      :dict-data-id="selectedDictDataId"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtDictType, HbtDictTypeQuery } from '@/types/admin/dictType'
import type { HbtDictData, HbtDictDataQuery } from '@/types/admin/dictData'
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
} from '@/api/admin/dictType'
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
} from '@/api/admin/dictData'
import DictTypeForm from './components/DictTypeForm.vue'
import DictTypeDetail from './components/DictTypeDetail.vue'
import DictDataForm from './components/DictDataForm.vue'
import DictDataDetail from './components/DictDataDetail.vue'

const { t } = useI18n()

// === 字典类型状态定义 ===
const dictTypeLoading = ref(false)
const dictTypeList = ref<HbtDictType[]>([])
const selectedDictTypeKeys = ref<string[]>([])
const selectedDictTypeId = ref<number>()
const selectedDictType = ref<string>('')
const dictTypeFormVisible = ref(false)
const dictTypeFormTitle = ref('')
const dictTypeDetailVisible = ref(false)
const dictTypeTotal = ref(0)

// === 字典数据状态定义 ===
const dictDataLoading = ref(false)
const dictDataList = ref<HbtDictData[]>([])
const selectedDictDataKeys = ref<string[]>([])
const selectedDictDataId = ref<number>()
const dictDataFormVisible = ref(false)
const dictDataFormTitle = ref('')
const dictDataDetailVisible = ref(false)
const dictDataTotal = ref(0)

// === 字典类型查询参数 ===
const dictTypeQueryParams = ref<HbtDictTypeQuery>({
  pageIndex: 1,
  pageSize: 10,
  dictName: '',
  dictType: '',
  status: undefined,
  orderByColumn: undefined,
  orderType: undefined
})

// === 字典数据查询参数 ===
const dictDataQueryParams = ref<HbtDictDataQuery>({
  pageIndex: 1,
  pageSize: 10,
  dictType: '',
  dictLabel: '',
  dictValue: '',
  status: undefined,
  orderByColumn: undefined,
  orderType: undefined
})

// === 字典数据标题 ===
const dictDataTitle = computed(() => {
  return selectedDictType.value ? `字典数据列表 - ${selectedDictType.value}` : '字典数据列表'
})

// === 字典类型搜索表单配置 ===
const dictTypeSearchFormItems = [
  {
    type: 'input' as const,
    name: 'dictName',
    label: t('admin.dict.dictName.label'),
    placeholder: t('admin.dict.dictName.placeholder')
  },
  {
    type: 'input' as const,
    name: 'dictType',
    label: t('admin.dict.dictType.label'),
    placeholder: t('admin.dict.dictType.placeholder')
  },
  {
    type: 'select' as const,
    name: 'status',
    label: t('admin.dict.status.label'),
    placeholder: t('admin.dict.status.placeholder'),
    options: [
      { label: t('admin.dict.status.normal'), value: 0 },
      { label: t('admin.dict.status.disable'), value: 1 }
    ]
  }
]

// === 字典数据搜索表单配置 ===
const dictDataSearchFormItems = [
  {
    type: 'input' as const,
    name: 'dictLabel',
    label: t('admin.dict.dictLabel.label'),
    placeholder: t('admin.dict.dictLabel.placeholder')
  },
  {
    type: 'input' as const,
    name: 'dictValue',
    label: t('admin.dict.dictValue.label'),
    placeholder: t('admin.dict.dictValue.placeholder')
  },
  {
    type: 'select' as const,
    name: 'status',
    label: t('admin.dict.status.label'),
    placeholder: t('admin.dict.status.placeholder'),
    options: [
      { label: t('admin.dict.status.normal'), value: 0 },
      { label: t('admin.dict.status.disable'), value: 1 }
    ]
  }
]

// === 字典类型表格列定义 ===
const dictTypeColumns = [
  {
    title: t('admin.dict.dictName.label'),
    dataIndex: 'dictName',
    key: 'dictName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('admin.dict.dictType.label'),
    dataIndex: 'dictType',
    key: 'dictType',
    width: 150,
    ellipsis: true
  },
  {
    title: t('admin.dict.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('admin.dict.remark.label'),
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

// === 字典数据表格列定义 ===
const dictDataColumns = [
  {
    title: t('admin.dict.dictLabel.label'),
    dataIndex: 'dictLabel',
    key: 'dictLabel',
    width: 150,
    ellipsis: true
  },
  {
    title: t('admin.dict.dictValue.label'),
    dataIndex: 'dictValue',
    key: 'dictValue',
    width: 150,
    ellipsis: true
  },
  {
    title: t('admin.dict.orderNum.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    sorter: true
  },
  {
    title: t('admin.dict.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('admin.dict.remark.label'),
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

// === 字典类型方法定义 ===
// 获取字典类型列表
const fetchDictTypeList = async () => {
  try {
    dictTypeLoading.value = true
    const res = await getHbtDictTypeList(dictTypeQueryParams.value)
    if (res.code === 200) {
      dictTypeList.value = res.data.rows
      dictTypeTotal.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载字典类型列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    dictTypeLoading.value = false
  }
}

// 搜索字典类型
const handleSearchDictType = () => {
  dictTypeQueryParams.value.pageIndex = 1
  fetchDictTypeList()
}

// 重置字典类型搜索
const handleResetDictType = () => {
  dictTypeQueryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    dictName: '',
    dictType: '',
    status: undefined,
    orderByColumn: undefined,
    orderType: undefined
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
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      if (id === selectedDictTypeId.value) {
        selectedDictTypeId.value = undefined
        selectedDictType.value = ''
        fetchDictDataList()
      }
      fetchDictTypeList()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 批量删除字典类型
const handleBatchDeleteDictType = async () => {
  if (selectedDictTypeKeys.value.length === 0) {
    message.warning(t('common.message.selectRecord'))
    return
  }
  try {
    const res = await batchDeleteHbtDictType(selectedDictTypeKeys.value.map(key => Number(key)))
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      selectedDictTypeKeys.value = []
      if (selectedDictTypeKeys.value.includes(String(selectedDictTypeId.value))) {
        selectedDictTypeId.value = undefined
        selectedDictType.value = ''
        fetchDictDataList()
      }
      fetchDictTypeList()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 导入字典类型
const handleImportDictType = () => {
  // TODO: 实现导入功能
}

// 下载字典类型模板
const handleTemplateDictType = () => {
  // TODO: 实现下载模板功能
}

// 导出字典类型
const handleExportDictType = () => {
  // TODO: 实现导出功能
}

// 字典类型页码变化
const handleDictTypePageChange = (page: number) => {
  dictTypeQueryParams.value.pageIndex = page
  fetchDictTypeList()
}

// 字典类型每页条数变化
const handleDictTypeSizeChange = (current: number, size: number) => {
  dictTypeQueryParams.value.pageIndex = current
  dictTypeQueryParams.value.pageSize = size
  fetchDictTypeList()
}

// 字典类型选择变化
const handleDictTypeSelect = (selectedKeys: string[]) => {
  if (selectedKeys.length > 0) {
    const record = dictTypeList.value.find(item => String(item.dictTypeId) === selectedKeys[0])
    if (record) {
      selectedDictTypeId.value = record.dictTypeId
      selectedDictType.value = record.dictType
      dictDataQueryParams.value.dictType = record.dictType
      fetchDictDataList()
    }
  } else {
    selectedDictTypeId.value = undefined
    selectedDictType.value = ''
    dictDataQueryParams.value.dictType = ''
    dictDataList.value = []
    dictDataTotal.value = 0
  }
}

// 字典类型表单提交成功
const handleDictTypeSuccess = () => {
  dictTypeFormVisible.value = false
  fetchDictTypeList()
}

// === 字典数据方法定义 ===
// 获取字典数据列表
const fetchDictDataList = async () => {
  if (!selectedDictTypeId.value) {
    dictDataList.value = []
    dictDataTotal.value = 0
    return
  }

  try {
    dictDataLoading.value = true
    const res = await getHbtDictDataList(dictDataQueryParams.value)
    if (res.code === 200) {
      dictDataList.value = res.data.rows
      dictDataTotal.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载字典数据列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    dictDataLoading.value = false
  }
}

// 搜索字典数据
const handleSearchDictData = () => {
  dictDataQueryParams.value.pageIndex = 1
  fetchDictDataList()
}

// 重置字典数据搜索
const handleResetDictData = () => {
  dictDataQueryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    dictType: selectedDictType.value,
    dictLabel: '',
    dictValue: '',
    status: undefined,
    orderByColumn: undefined,
    orderType: undefined
  }
  fetchDictDataList()
}

// 新增字典数据
const handleAddDictData = () => {
  if (!selectedDictTypeId.value) {
    message.warning(t('common.message.selectDictType'))
    return
  }
  selectedDictDataId.value = undefined
  dictDataFormTitle.value = t('common.title.create')
  dictDataFormVisible.value = true
}

// 编辑字典数据
const handleEditDictData = (record?: HbtDictData) => {
  if (record) {
    selectedDictDataId.value = record.dictDataId
  } else if (selectedDictDataKeys.value.length === 1) {
    selectedDictDataId.value = Number(selectedDictDataKeys.value[0])
  } else {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  dictDataFormTitle.value = t('common.title.edit')
  dictDataFormVisible.value = true
}

// 查看字典数据
const handleViewDictData = (record: HbtDictData) => {
  selectedDictDataId.value = record.dictDataId
  dictDataDetailVisible.value = true
}

// 删除字典数据
const handleDeleteDictData = async (record: HbtDictData) => {
  try {
    const res = await deleteHbtDictData(record.dictDataId)
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchDictDataList()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 批量删除字典数据
const handleBatchDeleteDictData = async () => {
  if (selectedDictDataKeys.value.length === 0) {
    message.warning(t('common.message.selectRecord'))
    return
  }
  try {
    const res = await batchDeleteHbtDictData(selectedDictDataKeys.value.map(key => Number(key)))
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      selectedDictDataKeys.value = []
      fetchDictDataList()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 导入字典数据
const handleImportDictData = () => {
  // TODO: 实现导入功能
}

// 下载字典数据模板
const handleTemplateDictData = () => {
  // TODO: 实现下载模板功能
}

// 导出字典数据
const handleExportDictData = () => {
  // TODO: 实现导出功能
}

// 字典数据页码变化
const handleDictDataPageChange = (page: number) => {
  dictDataQueryParams.value.pageIndex = page
  fetchDictDataList()
}

// 字典数据每页条数变化
const handleDictDataSizeChange = (current: number, size: number) => {
  dictDataQueryParams.value.pageIndex = current
  dictDataQueryParams.value.pageSize = size
  fetchDictDataList()
}

// 字典数据表单提交成功
const handleDictDataSuccess = () => {
  dictDataFormVisible.value = false
  fetchDictDataList()
}

// === 生命周期 ===
onMounted(() => {
  fetchDictTypeList()
})
</script>

<style lang="less" scoped>
.dict-container {
  padding: 16px;
  background-color: #fff;
  border-radius: 4px;
}
</style> 