<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: type/index.vue
创建日期: 2024-03-20
描述: 字典类型管理页面
=================================================================== 
-->

<template>
  <div class="hbt-dict-type">
    <!-- 查询区域 -->
    <hbt-query
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item label="字典名称">
          <a-input
            v-model:value="queryParams.dictName"
            placeholder="请输入字典名称"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="字典类型">
          <a-input
            v-model:value="queryParams.dictType"
            placeholder="请输入字典类型"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="状态">
          <hbt-select
            v-model:value="queryParams.status"
            :options="statusOptions"
            placeholder="请选择状态"
            show-search
            :filter-option="(input: string, option: any) => {
              return option.label.toLowerCase().indexOf(input.toLowerCase()) >= 0
            }"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['admin:dicttype:create']"
      :show-edit="true"
      :edit-permission="['admin:dicttype:update']"
      :show-delete="true"
      :delete-permission="['admin:dicttype:delete']"
      :show-export="true"
      :export-permission="['admin:dicttype:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @refresh="loadDictTypeList"
      @column-setting="handleColumnSetting"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="dictTypeList"
      :columns="columns"
      :pagination="{
        total: total,
        current: queryParams.pageNum,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: HbtDictType) => String(record.dictTypeId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <a-tag :color="record.status === 0 ? 'success' : 'error'">
            {{ record.status === 0 ? t('common.status.normal') : t('common.status.disabled') }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-else-if="column.dataIndex === 'operation'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :show-edit="true"
            :show-delete="true"
            button-type="link"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #buttons>
              <a-button 
                type="link" 
                size="small" 
                @click="handleDictData(record)"
                v-hasPermi="['admin:dictdata:list']"
              >
                字典数据
              </a-button>
              <a-button 
                type="link" 
                size="small" 
                @click="handleEdit(record)"
                v-hasPermi="['admin:dicttype:edit']"
              >
                编辑
              </a-button>
              <a-button 
                type="link" 
                size="small" 
                @click="handleDelete(record)"
                v-hasPermi="['admin:dicttype:delete']"
              >
                删除
              </a-button>
            </template>
          </hbt-operation>
        </template>
      </template>
    </hbt-table>

    <!-- 表单弹窗 -->
    <dict-type-form
      v-model:open="formVisible"
      :title="formTitle"
      :loading="formLoading"
      :model="formData"
      @ok="handleFormOk"
      @cancel="handleFormCancel"
    />

    <!-- 详情弹窗 -->
    <dict-type-detail
      v-model:open="detailVisible"
      :loading="detailLoading"
      :model="detailData"
      @close="handleDetailClose"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { h } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { hasPermi } from '@/directives/permission'
import { useRouter, useRoute } from 'vue-router'

// 引入标准组件
import HbtQuery from '@/components/Business/Query/index.vue'
import HbtToolbar from '@/components/Business/Toolbar/index.vue'
import HbtTable from '@/components/Business/Table/index.vue'
import HbtOperation from '@/components/Business/Operation/index.vue'
import HbtPagination from '@/components/Business/Pagination/index.vue'
import HbtSelect from '@/components/Business/Select/index.vue'

// 引入业务组件
import DictTypeForm from './components/DictTypeForm.vue'
import DictTypeDetail from './components/DictTypeDetail.vue'

// 引入API和类型
import {
  getHbtDictTypeList,
  getHbtDictType,
  createHbtDictType,
  updateHbtDictType,
  deleteHbtDictType,
  batchDeleteHbtDictType,
  exportHbtDictType
} from '@/api/admin/hbtDictType'
import type { HbtDictType, HbtDictTypeQuery, DictTypePageResult, HbtDictTypeCreate, HbtDictTypeUpdate } from '@/types/admin/hbtDictType'
import type { ApiResult, HbtStatus } from '@/types/base'

// 引入类型
import type { QueryField } from '@/types/components/query'

const { t } = useI18n()
const router = useRouter()
const route = useRoute()

// === 状态定义 ===
const loading = ref(false)
const total = ref(0)
const dictTypeList = ref<HbtDictType[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formLoading = ref(false)
const formData = ref<Partial<HbtDictTypeUpdate>>({})
const detailVisible = ref(false)
const detailLoading = ref(false)
const detailData = ref<HbtDictType>()

// === 查询参数 ===
const queryParams = ref<HbtDictTypeQuery>({
  pageNum: 1,
  pageSize: 10,
  dictName: '',
  dictType: '',
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
  { label: t('common.status.normal'), value: 0 },
  { label: t('common.status.disabled'), value: 1 }
]

// === 表格列定义 ===
const columns = [
  {
    title: '字典名称',
    dataIndex: 'dictName',
    key: 'dictName',
    width: 200,
    ellipsis: true,
    sorter: true
  },
  {
    title: '字典类型',
    dataIndex: 'dictType',
    key: 'dictType',
    width: 200,
    ellipsis: true,
    sorter: true
  },
  {
    title: '字典类别',
    dataIndex: 'dictCategory',
    key: 'dictCategory',
    width: 100,
    align: 'center',
    customRender: ({ text }: { text: number }) => text === 0 ? '系统' : 'SQL'
  },
  {
    title: '租户ID',
    dataIndex: 'tenantId',
    key: 'tenantId',
    width: 100,
    align: 'center'
  },
  {
    title: 'SQL脚本',
    dataIndex: 'sqlScript',
    key: 'sqlScript',
    width: 200,
    ellipsis: true,
    customRender: ({ text, record }: { text: string, record: HbtDictType }) => {
      if (record.dictCategory === 1 && text) {
        return h('a-tooltip', { title: text }, () => [
          h('span', text)
        ])
      }
      return '-'
    }
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100,
    align: 'center'
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    sorter: true
  },
  {
    title: t('common.table.header.operation'),
    dataIndex: 'operation',
    key: 'operation',
    width: 220,
    fixed: 'right',
    align: 'center'
  }
]

// === 查询字段定义 ===
const queryFields: QueryField[] = [
  {
    name: 'dictName',
    label: '字典名称',
    type: 'input' as const
  },
  {
    name: 'dictType',
    label: '字典类型',
    type: 'input' as const
  },
  {
    name: 'status',
    label: '状态',
    type: 'select' as const,
    options: statusOptions
  }
]

// === 方法定义 ===
/**
 * 处理分页变化
 * @param page 新的页码
 * @param size 新的每页条数
 */
const handleTableChange = (pagination: any, filters: any, sorter: any) => {
  console.log('表格变化:', { pagination, filters, sorter })
  queryParams.value.pageNum = pagination.current
  queryParams.value.pageSize = pagination.pageSize
  loadDictTypeList()
}

// === 加载字典类型列表 ===
const loadDictTypeList = async () => {
  try {
    loading.value = true
    const { data: pageResult } = await getHbtDictTypeList(queryParams.value)
    console.log('加载数据:', pageResult)
    
    // 更新数据和分页信息
    dictTypeList.value = pageResult.rows
    total.value = pageResult.totalNum
    
    // 如果当前页没有数据且不是第一页，则加载上一页
    if (dictTypeList.value.length === 0 && queryParams.value.pageNum > 1) {
      queryParams.value.pageNum--
      await loadDictTypeList()
    }
  } catch (error) {
    console.error('加载字典类型列表失败:', error)
    message.error('加载字典类型列表失败')
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageNum = 1
  loadDictTypeList()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageNum: 1,
    pageSize: 10,
    dictName: '',
    dictType: '',
    status: undefined
  }
  handleQuery()
}

// 表格排序
const handleTableSort = (sorter: any) => {
  loadDictTypeList()
}

// 新增
const handleAdd = () => {
  formTitle.value = t('common.title.create')
  formData.value = {
    dictName: '',
    dictType: '',
    dictCategory: 0,
    dictBuiltin: 0,
    sqlScript: '',
    orderNum: 0,
    status: 0,
    remark: '',
    tenantId: 0  // 添加租户ID字段
  }
  formVisible.value = true
}

// 编辑
const handleEdit = async (record: HbtDictType) => {
  try {
    console.log('=== 开始处理编辑操作 ===')
    formLoading.value = true
    const { data: response } = await getHbtDictType(record.dictTypeId)
    if (response.code === 200 && response.data) {
      formData.value = response.data
      formTitle.value = '编辑字典类型'
      console.log('编辑数据:', formData.value)
      console.log('当前 formVisible 状态:', formVisible.value)
      formVisible.value = true
      console.log('设置后 formVisible 状态:', formVisible.value)
    } else {
      message.error(response.msg || '获取详情失败')
    }
  } catch (error) {
    console.error('获取详情失败:', error)
    message.error('获取详情失败')
  } finally {
    formLoading.value = false
    console.log('=== 编辑操作处理完成 ===')
  }
}

// 查看
const handleView = async (record: HbtDictType) => {
  try {
    detailLoading.value = true
    const { data: response } = await getHbtDictType(record.dictTypeId)
    if (response.code === 200 && response.data) {
      detailData.value = response.data
      detailVisible.value = true
    } else {
      message.error(response.msg || '获取详情失败')
    }
  } catch (error) {
    console.error('获取详情失败:', error)
    message.error('获取详情失败')
  } finally {
    detailLoading.value = false
  }
}

// 删除
const handleDelete = async (record: HbtDictType) => {
  try {
    const { data: response } = await deleteHbtDictType(record.dictTypeId)
    if (response.code === 200) {
      message.success('删除成功')
      await loadDictTypeList()
    } else {
      message.error(response.msg || '删除失败')
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录进行编辑')
    return
  }
  
  const record = dictTypeList.value.find(item => String(item.dictTypeId) === String(selectedRowKeys.value[0]))
  if (record) {
    handleEdit(record)
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await batchDeleteHbtDictType(selectedRowKeys.value.map(key => Number(key)))
    message.success(t('common.message.deleteSuccess'))
    selectedRowKeys.value = []
    loadDictTypeList()
  } catch (error) {
    message.error(t('common.message.deleteFailed'))
  }
}

// 导出
const handleExport = async () => {
  try {
    await exportHbtDictType(queryParams.value)
    message.success('导出成功')
  } catch (error) {
    message.error('导出失败')
  }
}

// 跳转到字典数据页面
const handleDictData = (record: HbtDictType) => {
  router.push({
    path: '/admin/dictdata',
    query: {
      dictType: record.dictType
    }
  })
}

// 表单确认
const handleFormOk = async (values: Partial<HbtDictType>) => {
  try {
    console.log('表单确认，提交数据:', values)
    formLoading.value = true
    if (values.dictTypeId) {
      console.log('执行更新操作')
      const result = await updateHbtDictType(values as HbtDictTypeUpdate)
      console.log('更新结果:', result)
      message.success(t('common.message.updateSuccess'))
    } else {
      console.log('执行新增操作')
      const result = await createHbtDictType({
        ...values,
        tenantId: 0  // 确保新增时设置租户ID
      } as HbtDictTypeCreate)
      console.log('新增结果:', result)
      message.success(t('common.message.createSuccess'))
    }
    formVisible.value = false
    loadDictTypeList()
  } catch (error) {
    console.error('表单提交失败:', error)
    message.error(values.dictTypeId ? t('common.message.updateFailed') : t('common.message.createFailed'))
  } finally {
    formLoading.value = false
  }
}

// 表单取消
const handleFormCancel = () => {
  console.log('=== 处理表单取消 ===')
  formVisible.value = false
  formData.value = {}
}

// 详情关闭
const handleDetailClose = () => {
  detailVisible.value = false
  detailData.value = undefined
}

// 列设置
const handleColumnSetting = () => {
  // TODO: 实现列设置功能
}

// === 事件处理函数 ===
const handleRowClick = (record: HbtDictType) => {
  console.log('行点击事件:', record)
}

// === 表格选择相关 ===
const handleSelectionChange = (selectedKeys: (string | number)[], selectedRows: HbtDictType[]) => {
  console.log('选择变化:', selectedKeys, selectedRows)
  // 不需要在这里处理，因为使用了 v-model
}

// === 生命周期钩子 ===
onMounted(() => {
  // 从路由中获取字典类型
  const dictType = route.query.dictType as string
  if (dictType) {
    queryParams.value.dictType = dictType
  }
  loadDictTypeList()
})
</script>

<style lang="less" scoped>
.hbt-dict-type {
  padding: 24px;
}
</style> 