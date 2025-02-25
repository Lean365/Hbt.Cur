//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据管理页面
//===================================================================

<template>
  <div class="p-6">
    <a-card>
      <!-- 搜索表单 -->
      <a-form layout="inline" :model="queryParams">
        <a-form-item label="字典标签">
          <a-input v-model:value="queryParams.dictLabel" placeholder="请输入字典标签" allowClear />
        </a-form-item>
        <a-form-item label="字典键值">
          <a-input v-model:value="queryParams.dictValue" placeholder="请输入字典键值" allowClear />
        </a-form-item>
        <a-form-item label="状态">
          <a-select v-model:value="queryParams.status" placeholder="请选择" allowClear>
            <a-select-option :value="0">正常</a-select-option>
            <a-select-option :value="1">停用</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" @click="handleSearch" v-hasPermi="['admin:dict:list']">查询</a-button>
            <a-button @click="handleReset">重置</a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="mb-4 flex justify-between">
        <div>
          <a-space>
            <a-button type="primary" @click="handleAdd" v-hasPermi="['admin:dict:create']">
              <plus-outlined />新增
            </a-button>
            <a-button 
              :disabled="!selectedRowKeys.length"
              @click="handleBatchDelete"
              v-hasPermi="['admin:dict:delete']"
            >
              <delete-outlined />批量删除
            </a-button>
            <a-upload
              name="file"
              :show-upload-list="false"
              :before-upload="handleImport"
              v-hasPermi="['admin:dict:import']"
            >
              <a-button>
                <upload-outlined />导入
              </a-button>
            </a-upload>
            <a-button @click="handleExport" v-hasPermi="['admin:dict:export']">
              <download-outlined />导出
            </a-button>
            <a-button @click="handleTemplate" v-hasPermi="['admin:dict:query']">
              <file-outlined />模板
            </a-button>
          </a-space>
        </div>
        <div>
          <a-button @click="handleRefresh">
            <reload-outlined />刷新
          </a-button>
        </div>
      </div>

      <!-- 字典数据表格 -->
      <a-table
        :loading="loading"
        :columns="columns"
        :data-source="dictDataList"
        :pagination="false"
        :row-selection="{
          selectedRowKeys: selectedRowKeys,
          onChange: onSelectChange
        }"
        row-key="dictDataId"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'isDefault'">
            {{ record.isDefault === 1 ? '是' : '否' }}
          </template>
          <template v-else-if="column.key === 'status'">
            <span v-hasPermi="['admin:dict:update']">
              <a-switch
                :checked="(record as HbtDictData).status === 0"
                :loading="(record as HbtDictData).statusLoading"
                @change="(checked: string | number | boolean) => handleStatusChange(record as HbtDictData, Boolean(checked))"
              />
            </span>
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a-button type="link" @click="handleEdit(record as HbtDictData)" v-hasPermi="['admin:dict:update']">编辑</a-button>
              <a-button 
                type="link" 
                danger
                v-hasPermi="['admin:dict:delete']"
              >
                <a-popconfirm
                  title="确定要删除吗？"
                  @confirm="handleDelete(record as HbtDictData)"
                >
                  <template #default>
                    <span>删除</span>
                  </template>
                </a-popconfirm>
              </a-button>
            </a-space>
          </template>
        </template>
      </a-table>

      <!-- 分页组件 -->
      <hbt-pagination
        v-model:current="queryParams.pageNum"
        v-model:pageSize="queryParams.pageSize"
        :total="total"
        @change="handlePageChange"
      />

      <!-- 字典数据表单 -->
      <dict-data-form
        v-model:visible="modalVisible"
        :title="modalTitle"
        :dict-type-id="dictTypeId"
        :record="currentRecord"
        @submit="handleSubmit"
      />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { ColumnType } from 'ant-design-vue/es/table'
import { 
  PlusOutlined, 
  DeleteOutlined,
  UploadOutlined,
  DownloadOutlined,
  FileOutlined,
  ReloadOutlined 
} from '@ant-design/icons-vue'
import {
  getHbtDictDataList,
  getHbtDictData,
  createHbtDictData,
  updateHbtDictData,
  deleteHbtDictData,
  batchDeleteHbtDictData,
  importHbtDictData,
  exportHbtDictData,
  getHbtDictDataTemplate,
  updateHbtDictDataStatus
} from '@/api/admin/hbtDictData'
import type {
  HbtDictData,
  HbtDictDataQuery,
  HbtDictDataCreate,
  HbtDictDataUpdate
} from '@/types/admin/hbtDictData'
import DictDataForm from './components/DictDataForm.vue'
import HbtPagination from '@/components/pagination/index.vue'

// 查询参数
const queryParams = reactive<HbtDictDataQuery>({
  pageNum: 1,
  pageSize: 10,
  dictTypeId: undefined,
  dictLabel: '',
  dictValue: '',
  status: undefined
})

// 状态定义
const loading = ref(false)
const dictDataList = ref<HbtDictData[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const modalVisible = ref(false)
const modalTitle = ref('新增字典数据')
const currentRecord = ref<HbtDictData>()
const total = ref(0)
const dictTypeId = ref<number>()

// 表格列定义
const columns: ColumnType<HbtDictData>[] = [
  {
    title: '字典标签',
    dataIndex: 'dictLabel',
    key: 'dictLabel',
    width: 200
  },
  {
    title: '字典键值',
    dataIndex: 'dictValue',
    key: 'dictValue',
    width: 200
  },
  {
    title: '字典排序',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: '样式属性',
    dataIndex: 'cssClass',
    key: 'cssClass',
    width: 150
  },
  {
    title: '表格样式',
    dataIndex: 'listClass',
    key: 'listClass',
    width: 150
  },
  {
    title: '是否默认',
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    ellipsis: true
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right' as const
  }
]

// 加载字典数据列表
const loadDictDataList = async () => {
  loading.value = true
  try {
    const { data } = await getHbtDictDataList(queryParams)
    if (data?.code === 200) {
      dictDataList.value = data.data.list
      total.value = data.data.total
    } else {
      message.error(data?.msg || '加载字典数据列表失败')
      dictDataList.value = []
      total.value = 0
    }
  } catch (error) {
    console.error('加载字典数据列表失败:', error)
    message.error('加载字典数据列表失败')
    dictDataList.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 页码变化处理
const handlePageChange = (page: number, size: number) => {
  queryParams.pageNum = page
  queryParams.pageSize = size
  loadDictDataList()
}

// 选择变化处理
const onSelectChange = (keys: (string | number)[]) => {
  selectedRowKeys.value = keys
}

// 搜索处理
const handleSearch = () => {
  queryParams.pageNum = 1
  loadDictDataList()
}

// 重置处理
const handleReset = () => {
  queryParams.dictLabel = ''
  queryParams.dictValue = ''
  queryParams.status = undefined
  queryParams.pageNum = 1
  loadDictDataList()
}

// 刷新处理
const handleRefresh = () => {
  loadDictDataList()
}

// 新增处理
const handleAdd = () => {
  modalTitle.value = '新增字典数据'
  currentRecord.value = undefined
  modalVisible.value = true
}

// 编辑处理
const handleEdit = async (record: HbtDictData) => {
  try {
    const { data } = await getHbtDictData(record.dictDataId)
    if (data?.code === 200) {
      modalTitle.value = '编辑字典数据'
      currentRecord.value = data.data
      modalVisible.value = true
    } else {
      message.error(data?.msg || '获取字典数据详情失败')
    }
  } catch (error) {
    console.error('获取字典数据详情失败:', error)
    message.error('获取字典数据详情失败')
  }
}

// 删除处理
const handleDelete = async (record: HbtDictData) => {
  try {
    const { data } = await deleteHbtDictData(record.dictDataId)
    if (data?.code === 200) {
      message.success('删除成功')
      loadDictDataList()
    } else {
      message.error(data?.msg || '删除失败')
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

// 批量删除处理
const handleBatchDelete = async () => {
  try {
    const { data } = await batchDeleteHbtDictData(selectedRowKeys.value.map(Number))
    if (data?.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      loadDictDataList()
    } else {
      message.error(data?.msg || '批量删除失败')
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error('批量删除失败')
  }
}

// 导入处理
const handleImport = async (file: File) => {
  try {
    const { data } = await importHbtDictData(file)
    if (data?.code === 200) {
      message.success('导入成功')
      loadDictDataList()
    } else {
      message.error(data?.msg || '导入失败')
    }
  } catch (error) {
    console.error('导入失败:', error)
    message.error('导入失败')
  }
  return false
}

// 导出处理
const handleExport = async () => {
  try {
    const { data } = await exportHbtDictData(queryParams)
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '字典数据.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('导出失败:', error)
    message.error('导出失败')
  }
}

// 下载模板
const handleTemplate = async () => {
  try {
    const { data } = await getHbtDictDataTemplate()
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '字典数据导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

// 状态变更处理
const handleStatusChange = async (record: HbtDictData, checked: boolean) => {
  try {
    record.statusLoading = true
    const { data } = await updateHbtDictDataStatus(record.dictDataId, checked ? 0 : 1)
    if (data?.code === 200) {
      message.success('状态更新成功')
      record.status = checked ? 0 : 1
    } else {
      message.error(data?.msg || '状态更新失败')
    }
  } catch (error) {
    console.error('状态更新失败:', error)
    message.error('状态更新失败')
  } finally {
    record.statusLoading = false
  }
}

// 表单提交处理
const handleSubmit = async (formData: HbtDictDataCreate | HbtDictDataUpdate) => {
  try {
    const { data } = await (currentRecord.value
      ? updateHbtDictData(formData as HbtDictDataUpdate)
      : createHbtDictData(formData as HbtDictDataCreate))
      
    if (data?.code === 200) {
      message.success(`${currentRecord.value ? '更新' : '创建'}成功`)
      modalVisible.value = false
      loadDictDataList()
    } else {
      message.error(data?.msg || `${currentRecord.value ? '更新' : '创建'}失败`)
    }
  } catch (error) {
    console.error(`${currentRecord.value ? '更新' : '创建'}失败:`, error)
    message.error(`${currentRecord.value ? '更新' : '创建'}失败`)
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadDictDataList()
})
</script>

<style scoped>
.ant-form {
  margin-bottom: 24px;
}

.ant-table {
  margin-top: 24px;
}
</style> 