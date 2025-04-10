<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: data/index.vue
创建日期: 2024-03-20
描述: 字典数据管理页面
=================================================================== 
-->

<template>
  <div class="hbt-dict-data">
    <!-- 查询区域 -->
    <hbt-query
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item label="字典标签">
          <a-input
            v-model:value="queryParams.dictLabel"
            placeholder="请输入字典标签"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="字典键值">
          <a-input
            v-model:value="queryParams.dictValue"
            placeholder="请输入字典键值"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="状态">
          <hbt-select
            v-model:value="queryParams.status"
            :options="statusOptions"
            placeholder="请选择状态"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      v-hasPermi="['admin:dict:data:add', 'admin:dict:data:edit', 'admin:dict:data:delete', 'admin:dict:data:export']"
      @add="handleAdd"
      @edit="handleBatchEdit"
      @delete="handleBatchDelete"
      @export="handleExport"
      @refresh="loadDictDataList"
      @column-setting="handleColumnSetting"
    />

    <!-- 数据表格 -->
    <hbt-table
      :columns="columns"
      :data-source="dictDataList"
      :loading="loading"
      :row-selection="{ selectedRowKeys, onChange: onSelectChange }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <a-tag :color="record.status ? 'success' : 'error'">
            {{ record.status ? t('common.status.normal') : t('common.status.disabled') }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-else-if="column.dataIndex === 'operation'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :show-edit="true"
            :show-delete="true"
            v-hasPermi="['admin:dict:data:query', 'admin:dict:data:edit', 'admin:dict:data:delete']"
            button-type="link"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 分页 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="pagination.total"
      @change="handleTableChange"
    />

    <!-- 表单弹窗 -->
    <dict-data-form
      v-model:visible="formVisible"
      :title="formTitle"
      :loading="formLoading"
      :model="formData"
      :dict-type="dictType"
      @ok="handleFormOk"
      @cancel="handleFormCancel"
    />

    <!-- 详情弹窗 -->
    <dict-data-detail
      v-model:visible="detailVisible"
      :loading="detailLoading"
      :model="detailData"
      @close="handleDetailClose"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { hasPermi } from '@/directives/permission'
import { useRoute } from 'vue-router'
// 引入标准组件


// 引入API和类型
import {
  getHbtDictDataList,
  getHbtDictData,
  createHbtDictData,
  updateHbtDictData,
  deleteHbtDictData,
  batchDeleteHbtDictData,
  exportHbtDictData
} from '@/api/admin/hbtDictData'
import type { HbtDictData, HbtDictDataQuery } from '@/types/admin/dictData'

const { t } = useI18n()
const route = useRoute()

// === 状态定义 ===
const loading = ref(false)
const dictDataList = ref<HbtDictData[]>([])
const selectedRowKeys = ref<number[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formLoading = ref(false)
const formData = ref<Partial<HbtDictData>>({})
const detailVisible = ref(false)
const detailLoading = ref(false)
const detailData = ref<HbtDictData>()
const dictType = ref(route.query.dictType as string)

// === 查询参数 ===
const queryParams = ref<HbtDictDataQuery>({
  pageIndex: 1,
  pageSize: 10,
  dictType: dictType.value,
  dictLabel: '',
  dictValue: '',
  status: undefined
})

// === 分页配置 ===
const pagination = ref<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showQuickJumper: true
})

// === 状态选项 ===
const statusOptions = [
  { label: t('common.status.normal'), value: 1 },
  { label: t('common.status.disabled'), value: 0 }
]

// === 表格列定义 ===
const columns = [
  {
    title: '字典标签',
    dataIndex: 'dictLabel',
    width: 200,
    ellipsis: true
  },
  {
    title: '字典键值',
    dataIndex: 'dictValue',
    width: 200,
    ellipsis: true
  },
  {
    title: '字典排序',
    dataIndex: 'dictSort',
    width: 100,
    sorter: true
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 100
  },
  {
    title: '样式属性',
    dataIndex: 'cssClass',
    width: 150,
    ellipsis: true
  },
  {
    title: '备注',
    dataIndex: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    width: 180,
    sorter: true
  },
  {
    title: t('common.table.header.operation'),
    dataIndex: 'operation',
    width: 180,
    fixed: 'right'
  }
]

// === 查询字段定义 ===
const queryFields = [
  {
    name: 'dictLabel',
    label: '字典标签',
    type: 'input'
  },
  {
    name: 'dictValue',
    label: '字典键值',
    type: 'input'
  },
  {
    name: 'status',
    label: '状态',
    type: 'select',
    options: statusOptions
  }
]

// === 方法定义 ===
// 加载字典数据列表
const loadDictDataList = async () => {
  try {
    loading.value = true
    const res = await getHbtDictDataList(queryParams.value)
    dictDataList.value = res.data.rows
    pagination.value.total = res.data.total
  } catch (error) {
    console.error('加载字典数据列表失败:', error)
    message.error(t('common.message.loadFailed'))
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  loadDictDataList()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    dictType: dictType.value,
    dictLabel: '',
    dictValue: '',
    status: undefined
  }
  handleQuery()
}

// 表格变化
const handleTableChange = (pag: any, filters: any, sorter: any) => {
  queryParams.value.pageIndex = pag.current
  queryParams.value.pageSize = pag.pageSize
  if (sorter.field) {
    queryParams.value.orderByColumn = sorter.field
    queryParams.value.isAsc = sorter.order === 'ascend' ? 'asc' : 'desc'
  }
  loadDictDataList()
}

// 选择行变化
const onSelectChange = (keys: number[]) => {
  selectedRowKeys.value = keys
}

// 列设置
const handleColumnSetting = () => {
  // TODO: 实现列设置功能
}

// 新增
const handleAdd = () => {
  formTitle.value = t('common.title.create')
  formData.value = {
    dictType: dictType.value,
    status: 1
  }
  formVisible.value = true
}

// 编辑
const handleEdit = async (record: HbtDictData) => {
  try {
    formTitle.value = t('common.title.edit')
    formLoading.value = true
    const res = await getHbtDictData(record.dictId)
    formData.value = res.data
    formVisible.value = true
  } catch (error) {
    message.error(t('common.message.loadFailed'))
  } finally {
    formLoading.value = false
  }
}

// 查看
const handleView = async (record: HbtDictData) => {
  try {
    detailLoading.value = true
    const res = await getHbtDictData(record.dictId)
    detailData.value = res.data
    detailVisible.value = true
  } catch (error) {
    message.error(t('common.message.loadFailed'))
  } finally {
    detailLoading.value = false
  }
}

// 删除
const handleDelete = async (record: HbtDictData) => {
  try {
    await deleteHbtDictData(record.dictId)
    message.success(t('common.message.deleteSuccess'))
    loadDictDataList()
  } catch (error) {
    message.error(t('common.message.deleteFailed'))
  }
}

// 批量编辑
const handleBatchEdit = () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要编辑的记录')
    return
  }
  // TODO: 实现批量编辑功能
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await batchDeleteHbtDictData(selectedRowKeys.value)
    message.success(t('common.message.deleteSuccess'))
    selectedRowKeys.value = []
    loadDictDataList()
  } catch (error) {
    message.error(t('common.message.deleteFailed'))
  }
}

// 导出
const handleExport = async () => {
  try {
    await exportHbtDictData(queryParams.value)
    message.success('导出成功')
  } catch (error) {
    message.error('导出失败')
  }
}

// 表单确认
const handleFormOk = async () => {
  try {
    formLoading.value = true
    if (formData.value.dictId) {
      await updateHbtDictData(formData.value as HbtDictData)
      message.success(t('common.message.updateSuccess'))
    } else {
      await createHbtDictData(formData.value as HbtDictData)
      message.success(t('common.message.createSuccess'))
    }
    formVisible.value = false
    loadDictDataList()
  } catch (error) {
    message.error(formData.value.dictId ? t('common.message.updateFailed') : t('common.message.createFailed'))
  } finally {
    formLoading.value = false
  }
}

// 表单取消
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
}

// 详情关闭
const handleDetailClose = () => {
  detailVisible.value = false
  detailData.value = undefined
}

// === 生命周期 ===
onMounted(() => {
  loadDictDataList()
})
</script>

<style lang="less" scoped>
.hbt-dict-data {
  padding: 24px;
}
</style> 