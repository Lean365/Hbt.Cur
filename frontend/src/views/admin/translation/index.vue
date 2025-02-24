//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译管理页面
//===================================================================

<template>
  <div class="p-6">
    <a-card>
      <!-- 搜索表单 -->
      <a-form layout="inline" :model="queryParams">
        <a-form-item label="语言代码">
          <a-input v-model:value="queryParams.langCode" placeholder="请输入语言代码" allowClear />
        </a-form-item>
        <a-form-item label="模块名称">
          <a-input v-model:value="queryParams.moduleName" placeholder="请输入模块名称" allowClear />
        </a-form-item>
        <a-form-item label="翻译键">
          <a-input v-model:value="queryParams.transKey" placeholder="请输入翻译键" allowClear />
        </a-form-item>
        <a-form-item label="翻译值">
          <a-input v-model:value="queryParams.transValue" placeholder="请输入翻译值" allowClear />
        </a-form-item>
        <a-form-item label="状态">
          <a-select v-model:value="queryParams.status" placeholder="请选择" allowClear>
            <a-select-option :value="0">正常</a-select-option>
            <a-select-option :value="1">停用</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" @click="handleSearch" v-hasPermi="['admin:trans:list']">查询</a-button>
            <a-button @click="handleReset">重置</a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="mb-4 flex justify-between">
        <div>
          <a-space>
            <a-button type="primary" @click="handleAdd" v-hasPermi="['admin:trans:create']">
              <plus-outlined />新增
            </a-button>
            <a-button 
              :disabled="!selectedRowKeys.length"
              @click="handleBatchDelete"
              v-hasPermi="['admin:trans:delete']"
            >
              <delete-outlined />批量删除
            </a-button>
            <a-upload
              name="file"
              :show-upload-list="false"
              :before-upload="handleImport"
              v-hasPermi="['admin:trans:import']"
            >
              <a-button>
                <upload-outlined />导入
              </a-button>
            </a-upload>
            <a-button @click="handleExport" v-hasPermi="['admin:trans:export']">
              <download-outlined />导出
            </a-button>
            <a-button @click="handleTemplate" v-hasPermi="['admin:trans:query']">
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

      <!-- 翻译表格 -->
      <a-table
        :loading="loading"
        :columns="columns"
        :data-source="translationList"
        :pagination="false"
        :row-selection="{
          selectedRowKeys: selectedRowKeys,
          onChange: onSelectChange
        }"
        row-key="id"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'status'">
            <a-switch
              :checked="(record as HbtTranslation).status === 0"
              :loading="(record as HbtTranslation).statusLoading"
              @change="(checked: string | number | boolean) => handleStatusChange(record as HbtTranslation, Boolean(checked))"
              v-hasPermi="['admin:trans:update']"
            />
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a @click="handleEdit(record as HbtTranslation)" v-hasPermi="['admin:trans:update']">编辑</a>
              <a-popconfirm
                title="确定要删除吗？"
                @confirm="handleDelete(record as HbtTranslation)"
                v-hasPermi="['admin:trans:delete']"
              >
                <a>删除</a>
              </a-popconfirm>
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

      <!-- 翻译表单 -->
      <translation-form
        v-model:visible="modalVisible"
        :title="modalTitle"
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
  getHbtTranslationList,
  getHbtTranslation,
  createHbtTranslation,
  updateHbtTranslation,
  deleteHbtTranslation,
  batchDeleteHbtTranslation,
  importHbtTranslation,
  exportHbtTranslation,
  getHbtTranslationTemplate,
  updateHbtTranslationStatus
} from '@/api/admin/hbtTranslation'
import type {
  HbtTranslation,
  HbtTranslationQuery,
  HbtTranslationCreate,
  HbtTranslationUpdate
} from '@/types/admin/hbtTranslation'
import TranslationForm from './components/TranslationForm.vue'
import HbtPagination from '@/components/pagination/index.vue'

// 查询参数
const queryParams = reactive<HbtTranslationQuery>({
  pageNum: 1,
  pageSize: 10,
  langCode: '',
  moduleName: '',
  transKey: '',
  transValue: '',
  status: undefined
})

// 状态定义
const loading = ref(false)
const translationList = ref<HbtTranslation[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const modalVisible = ref(false)
const modalTitle = ref('新增翻译')
const currentRecord = ref<HbtTranslation>()
const total = ref(0)

// 表格列定义
const columns: ColumnType<HbtTranslation>[] = [
  {
    title: '语言代码',
    dataIndex: 'langCode',
    key: 'langCode',
    width: 120
  },
  {
    title: '模块名称',
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 150
  },
  {
    title: '翻译键',
    dataIndex: 'transKey',
    key: 'transKey',
    width: 200,
    ellipsis: true
  },
  {
    title: '翻译值',
    dataIndex: 'transValue',
    key: 'transValue',
    width: 300,
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

// 加载翻译列表
const loadTranslationList = async () => {
  loading.value = true
  try {
    const { data } = await getHbtTranslationList(queryParams)
    if (data?.code === 200) {
      translationList.value = data.data.list
      total.value = data.data.total
    } else {
      message.error(data?.msg || '加载翻译列表失败')
      translationList.value = []
      total.value = 0
    }
  } catch (error) {
    console.error('加载翻译列表失败:', error)
    message.error('加载翻译列表失败')
    translationList.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 页码变化处理
const handlePageChange = (page: number, size: number) => {
  queryParams.pageNum = page
  queryParams.pageSize = size
  loadTranslationList()
}

// 选择变化处理
const onSelectChange = (keys: (string | number)[]) => {
  selectedRowKeys.value = keys
}

// 搜索处理
const handleSearch = () => {
  queryParams.pageNum = 1
  loadTranslationList()
}

// 重置处理
const handleReset = () => {
  queryParams.langCode = ''
  queryParams.moduleName = ''
  queryParams.transKey = ''
  queryParams.transValue = ''
  queryParams.status = undefined
  queryParams.pageNum = 1
  loadTranslationList()
}

// 刷新处理
const handleRefresh = () => {
  loadTranslationList()
}

// 新增处理
const handleAdd = () => {
  modalTitle.value = '新增翻译'
  currentRecord.value = undefined
  modalVisible.value = true
}

// 编辑处理
const handleEdit = async (record: HbtTranslation) => {
  try {
    const { data } = await getHbtTranslation(record.id)
    if (data?.code === 200) {
      modalTitle.value = '编辑翻译'
      currentRecord.value = data.data
      modalVisible.value = true
    } else {
      message.error(data?.msg || '获取翻译详情失败')
    }
  } catch (error) {
    console.error('获取翻译详情失败:', error)
    message.error('获取翻译详情失败')
  }
}

// 删除处理
const handleDelete = async (record: HbtTranslation) => {
  try {
    const { data } = await deleteHbtTranslation(record.id)
    if (data?.code === 200) {
      message.success('删除成功')
      loadTranslationList()
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
    const { data } = await batchDeleteHbtTranslation(selectedRowKeys.value.map(Number))
    if (data?.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      loadTranslationList()
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
    const { data } = await importHbtTranslation(file)
    if (data?.code === 200) {
      message.success('导入成功')
      loadTranslationList()
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
    const { data } = await exportHbtTranslation(queryParams)
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '翻译数据.xlsx'
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
    const { data } = await getHbtTranslationTemplate()
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '翻译导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

// 状态变更处理
const handleStatusChange = async (record: HbtTranslation, checked: boolean) => {
  try {
    record.statusLoading = true
    const { data } = await updateHbtTranslationStatus(record.id, checked ? 0 : 1)
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
const handleSubmit = async (formData: HbtTranslationCreate | HbtTranslationUpdate) => {
  try {
    const { data } = await (currentRecord.value
      ? updateHbtTranslation(formData as HbtTranslationUpdate)
      : createHbtTranslation(formData as HbtTranslationCreate))
      
    if (data?.code === 200) {
      message.success(`${currentRecord.value ? '更新' : '创建'}成功`)
      modalVisible.value = false
      loadTranslationList()
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
  loadTranslationList()
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