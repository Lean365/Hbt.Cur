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
        <a-form-item label="字典类别">
          <hbt-select
            v-model:value="queryParams.dictCategory"
            dict-type="sys_dict_category"
            type="radio"
            placeholder="请选择字典类别"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="状态">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            type="radio"
            placeholder="请选择状态"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="是否内置">
          <hbt-select
            v-model:value="queryParams.dictBuiltin"
            dict-type="sys_yes_no"
            type="select"
            placeholder="请选择是否内置"
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
      <!-- 字典类别列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'dictCategory'">
          <hbt-dict-tag type="sys_dict_category" :value="record.dictCategory" />
        </template>
        
        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 是否内置列 -->
        <template v-if="column.dataIndex === 'dictBuiltin'">
          <hbt-dict-tag dict-type="sys_yes_no" :value="record.dictBuiltin" />
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
              >
                字典数据
              </a-button>
              <a-button 
                type="link" 
                size="small" 
                @click="handleEdit(record)"
              >
                编辑
              </a-button>
              <a-button 
                type="link" 
                size="small" 
                @click="handleDelete(record)"
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
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useRouter, useRoute } from 'vue-router'
import { useDictData } from '@/hooks/useDictData'

// 引入业务组件
import DictTypeForm from './components/DictTypeForm.vue'
import DictTypeDetail from './components/DictTypeDetail.vue'

// 引入API和类型
import {
  getHbtDictTypeList,
  getHbtDictTypeById,
  createHbtDictType,
  updateHbtDictType,
  deleteHbtDictType,
  batchDeleteHbtDictType,
  exportHbtDictType,
  getHbtDictTypeByType
} from '@/api/admin/hbtDictType'
import type { HbtDictType, HbtDictTypeQuery, DictTypePageResult, HbtDictTypeCreate, HbtDictTypeUpdate } from '@/types/admin/hbtDictType'
import type { ApiResult, HbtStatus } from '@/types/base'
import type { HbtDictData } from '@/types/admin/hbtDictData'

// 引入类型
import type { QueryField } from '@/types/components/query'

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
  dictCategory: undefined,
  status: undefined,
  dictBuiltin: undefined
})

// === 分页配置 ===
const pagination = ref<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showQuickJumper: true
})

// === 字典数据 ===
const { dictDataMap, loading: dictLoading } = useDictData([
  'sys_dict_category',
  'sys_yes_no',
  'sys_normal_disable'
])

// 计算属性：字典类别选项
const dictCategoryOptions = computed(() => {
  return dictDataMap.value['sys_dict_category'] || []
})

// 计算属性：是否内置选项
const yesNoOptions = computed(() => {
  return dictDataMap.value['sys_yes_no'] || []
})

// 计算属性：状态选项
const normalDisableOptions = computed(() => {
  return dictDataMap.value['sys_normal_disable'] || []
})

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
    align: 'center'
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
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    align: 'center',
    sorter: true
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100,
    align: 'center'
  },
  {
    title: '是否内置',
    dataIndex: 'dictBuiltin',
    key: 'dictBuiltin',
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
    title: '操作',
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
    name: 'dictCategory',
    label: '字典类别',
    type: 'select' as const,
    props: {
      dictType: 'sys_dict_category',
      type: 'radio'
    }
  },
  {
    name: 'status',
    label: '状态',
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    }
  },
  {
    name: 'dictBuiltin',
    label: '是否内置',
    type: 'select' as const,
    props: {
      dictType: 'sys_yes_no',
      type: 'select'
    }
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
    dictCategory: undefined,
    status: undefined,
    dictBuiltin: undefined
  }
  handleQuery()
}

// 表格排序
const handleTableSort = (sorter: any) => {
  loadDictTypeList()
}

// 新增
const handleAdd = () => {
  formTitle.value = '新增字典类型'
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
    if (!record || !record.dictTypeId) {
      message.error('无效的字典类型ID')
      return
    }
    formLoading.value = true
    const { data: response } = await getHbtDictTypeById(record.dictTypeId)
    if (response.code === 200 && response.data) {
      formData.value = { ...response.data }  // 使用解构赋值创建新对象
      formTitle.value = '编辑字典类型'
      formVisible.value = true
    } else {
      message.error(response.msg || '获取详情失败')
    }
  } catch (error) {
    console.error('获取详情失败:', error)
    message.error('获取详情失败')
  } finally {
    formLoading.value = false
  }
}

// 查看
const handleView = async (record: HbtDictType) => {
  try {
    detailLoading.value = true
    const { data: response } = await getHbtDictTypeById(record.dictTypeId)
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
  
  const record = dictTypeList.value.find(item => {
    return item && String(item.dictTypeId) === String(selectedRowKeys.value[0])
  })
  
  if (record) {
    handleEdit(record)
  } else {
    message.error('未找到选中的记录')
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
    message.success('批量删除成功')
    selectedRowKeys.value = []
    loadDictTypeList()
  } catch (error) {
    message.error('批量删除失败')
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
      message.success('更新成功')
    } else {
      console.log('执行新增操作')
      const result = await createHbtDictType({
        ...values,
        tenantId: 0  // 确保新增时设置租户ID
      } as HbtDictTypeCreate)
      console.log('新增结果:', result)
      message.success('新增成功')
    }
    formVisible.value = false
    loadDictTypeList()
  } catch (error) {
    console.error('表单提交失败:', error)
    message.error(values.dictTypeId ? '更新失败' : '新增失败')
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
onMounted(async () => {
  try {
    console.log('[页面初始化] 开始加载页面')
    
    // 从路由中获取字典类型
    const dictType = route.query.dictType
    if (dictType && typeof dictType === 'string') {
      queryParams.value.dictType = dictType
      console.log('[页面初始化] 从路由获取到字典类型:', dictType)
    }
    
    // 加载字典类型列表
    console.log('[页面初始化] 开始加载字典类型列表')
    await loadDictTypeList()
    console.log('[页面初始化] 字典类型列表加载完成')
  } catch (error) {
    console.error('[页面错误] 页面初始化失败:', error)
    message.error('页面加载失败，请刷新重试')
  }
})
</script>

<style lang="less" scoped>
.hbt-dict-type {
  padding: 24px;
}
</style> 