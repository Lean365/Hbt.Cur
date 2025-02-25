//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型管理页面
//===================================================================

<template>
  <div class="p-6">
    <a-card>
      <!-- 搜索表单 -->
      <a-form layout="inline" :model="queryParams">
        <a-form-item label="字典名称">
          <a-input v-model:value="queryParams.dictName" placeholder="请输入字典名称" allowClear />
        </a-form-item>
        <a-form-item label="字典类型">
          <a-input v-model:value="queryParams.dictType" placeholder="请输入字典类型" allowClear />
        </a-form-item>
        <a-form-item label="字典类别">
          <a-select v-model:value="queryParams.dictCategory" placeholder="请选择" allowClear>
            <a-select-option :value="0">系统</a-select-option>
            <a-select-option :value="1">SQL</a-select-option>
          </a-select>
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

      <!-- 字典类型表格 -->
      <a-table
        :loading="loading"
        :columns="columns"
        :data-source="dictTypeList"
        :pagination="false"
        :row-selection="{
          selectedRowKeys: selectedRowKeys,
          onChange: onSelectChange
        }"
        row-key="dictTypeId"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'dictCategory'">
            {{ record.dictCategory === 1 ? 'SQL' : '系统' }}
          </template>
          <template v-else-if="column.key === 'status'">
            <a-switch
              :checked="(record as HbtDictType).status === 0"
              :loading="(record as HbtDictType).statusLoading"
              @change="(checked: string | number | boolean) => handleStatusChange(record as HbtDictType, Boolean(checked))"
              v-hasPermi="['admin:dict:update']"
            />
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a-button 
                type="link" 
                @click="handleEdit(record as HbtDictType)" 
                v-hasPermi="['admin:dict:update']"
              >
                编辑
              </a-button>
              <a-button 
                type="link" 
                danger
                v-hasPermi="['admin:dict:delete']"
              >
                <a-popconfirm
                  title="确定要删除吗？"
                  @confirm="handleDelete(record as HbtDictType)"
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

      <!-- 字典类型表单 -->
      <dict-type-form
        v-model:visible="modalVisible"
        :title="modalTitle"
        :record="currentRecord"
        @submit="handleSubmit"
      />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, computed } from 'vue'
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
  getHbtDictTypeList,
  getHbtDictType,
  createHbtDictType,
  updateHbtDictType,
  deleteHbtDictType,
  batchDeleteHbtDictType,
  importHbtDictType,
  exportHbtDictType,
  getHbtDictTypeTemplate,
  updateHbtDictTypeStatus
} from '@/api/admin/hbtDictType'
import type {
  HbtDictType,
  HbtDictTypeQuery,
  HbtDictTypeCreate,
  HbtDictTypeUpdate
} from '@/types/admin/hbtDictType'
import DictTypeForm from './components/DictTypeForm.vue'
import HbtPagination from '@/components/pagination/index.vue'

// 查询参数
const queryParams = reactive<HbtDictTypeQuery>({
  pageNum: 1,
  pageSize: 10,
  dictName: '',
  dictType: '',
  dictCategory: undefined,
  status: undefined
})

// 状态定义
const loading = ref(false)
const dictTypeList = ref<HbtDictType[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const modalVisible = ref(false)
const modalTitle = ref('新增字典类型')
const currentRecord = ref<HbtDictType>()
const total = ref(0)

// 表格列定义
const columns: ColumnType<HbtDictType>[] = [
  {
    title: '字典名称',
    dataIndex: 'dictName',
    key: 'dictName',
    width: 200
  },
  {
    title: '字典类型',
    dataIndex: 'dictType',
    key: 'dictType',
    width: 200
  },
  {
    title: '字典类别',
    dataIndex: 'dictCategory',
    key: 'dictCategory',
    width: 100
  },
  {
    title: 'SQL脚本',
    dataIndex: 'sqlScript',
    key: 'sqlScript',
    width: 200,
    ellipsis: true
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
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

// 加载字典类型列表
const loadDictTypeList = async () => {
  loading.value = true
  try {
    const { data } = await getHbtDictTypeList(queryParams)
    if (data?.code === 200) {
      dictTypeList.value = data.data.list
      total.value = data.data.total
    } else {
      message.error(data?.msg || '加载字典类型列表失败')
      dictTypeList.value = []
      total.value = 0
    }
  } catch (error) {
    console.error('加载字典类型列表失败:', error)
    message.error('加载字典类型列表失败')
    dictTypeList.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 页码变化处理
const handlePageChange = (page: number, size: number) => {
  queryParams.pageNum = page
  queryParams.pageSize = size
  loadDictTypeList()
}

// 选择变化处理
const onSelectChange = (keys: (string | number)[]) => {
  selectedRowKeys.value = keys
}

// 搜索处理
const handleSearch = () => {
  queryParams.pageNum = 1
  loadDictTypeList()
}

// 重置处理
const handleReset = () => {
  queryParams.dictName = ''
  queryParams.dictType = ''
  queryParams.dictCategory = undefined
  queryParams.status = undefined
  queryParams.pageNum = 1
  loadDictTypeList()
}

// 刷新处理
const handleRefresh = () => {
  loadDictTypeList()
}

// 新增处理
const handleAdd = () => {
  modalTitle.value = '新增字典类型'
  currentRecord.value = undefined
  modalVisible.value = true
}

// 编辑处理
const handleEdit = async (record: HbtDictType) => {
  try {
    const { data } = await getHbtDictType(record.dictTypeId)
    if (data?.code === 200) {
      modalTitle.value = '编辑字典类型'
      currentRecord.value = data.data
      modalVisible.value = true
    } else {
      message.error(data?.msg || '获取字典类型详情失败')
    }
  } catch (error) {
    console.error('获取字典类型详情失败:', error)
    message.error('获取字典类型详情失败')
  }
}

// 删除处理
const handleDelete = async (record: HbtDictType) => {
  try {
    const { data } = await deleteHbtDictType(record.dictTypeId)
    if (data?.code === 200) {
      message.success('删除成功')
      loadDictTypeList()
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
    const { data } = await batchDeleteHbtDictType(selectedRowKeys.value.map(Number))
    if (data?.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      loadDictTypeList()
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
    const fileReader = new FileReader()
    fileReader.onload = async (e) => {
      const data = JSON.parse(e.target?.result as string)
      const { data: res } = await importHbtDictType(data)
      if (res?.code === 200) {
        message.success('导入成功')
        loadDictTypeList()
      } else {
        message.error(res?.msg || '导入失败')
      }
    }
    fileReader.readAsText(file)
  } catch (error) {
    console.error('导入失败:', error)
    message.error('导入失败')
  }
  return false
}

// 导出处理
const handleExport = async () => {
  try {
    const { data } = await exportHbtDictType(queryParams)
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '字典类型.xlsx'
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
    const { data } = await getHbtDictTypeTemplate()
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '字典类型导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

// 状态变更处理
const handleStatusChange = async (record: HbtDictType, checked: boolean) => {
  try {
    record.statusLoading = true
    const { data } = await updateHbtDictTypeStatus(record.dictTypeId, checked ? 0 : 1)
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
const handleSubmit = async (formData: HbtDictTypeCreate | HbtDictTypeUpdate) => {
  try {
    const { data } = await (currentRecord.value
      ? updateHbtDictType(formData as HbtDictTypeUpdate)
      : createHbtDictType(formData as HbtDictTypeCreate))
      
    if (data?.code === 200) {
      message.success(`${currentRecord.value ? '更新' : '创建'}成功`)
      modalVisible.value = false
      loadDictTypeList()
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
  loadDictTypeList()
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