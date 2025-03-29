<template>
  <div class="p-6">
    <a-card>
      <!-- 搜索表单 -->
      <a-form layout="inline" :model="queryParams">
        <a-form-item label="语言代码">
          <a-input v-model:value="queryParams.langCode" placeholder="请输入语言代码" allowClear />
        </a-form-item>
        <a-form-item label="语言名称">
          <a-input v-model:value="queryParams.langName" placeholder="请输入语言名称" allowClear />
        </a-form-item>
        <a-form-item label="状态">
          <a-select v-model:value="queryParams.status" placeholder="请选择" allowClear>
            <a-select-option :value="0">正常</a-select-option>
            <a-select-option :value="1">停用</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" @click="handleSearch" v-hasPermi="['admin:lang:list']">查询</a-button>
            <a-button @click="handleReset">重置</a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="mb-4 flex justify-between">
        <div>
          <a-space>
            <a-button type="primary" @click="handleAdd" v-hasPermi="['admin:lang:create']">
              <plus-outlined />新增
            </a-button>
            <a-button 
              :disabled="!selectedRowKeys.length"
              @click="handleBatchDelete"
              v-hasPermi="['admin:lang:delete']"
            >
              <delete-outlined />批量删除
            </a-button>
            <a-upload
              name="file"
              :show-upload-list="false"
              :before-upload="handleImport"
              v-hasPermi="['admin:lang:import']"
            >
              <a-button>
                <upload-outlined />导入
              </a-button>
            </a-upload>
            <a-button @click="handleExport" v-hasPermi="['admin:lang:export']">
              <download-outlined />导出
            </a-button>
            <a-button @click="handleTemplate" v-hasPermi="['admin:lang:query']">
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

      <!-- 语言表格 -->
      <a-table
        :loading="loading"
        :columns="columns"
        :data-source="languageList"
        :pagination="false"
        :row-selection="{
          selectedRowKeys: selectedRowKeys,
          onChange: onSelectChange
        }"
        row-key="id"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'isDefault'">
            {{ record.isDefault ? '是' : '否' }}
          </template>
          <template v-else-if="column.key === 'status'">
            <span v-hasPermi="['admin:lang:update']">
              <a-switch
                :checked="(record as HbtLanguage).status === 0"
                :loading="(record as HbtLanguage).statusLoading"
                @change="(checked: string | number | boolean) => handleStatusChange(record as HbtLanguage, Boolean(checked))"
              />
            </span>
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a-button type="link" @click="handleEdit(record as HbtLanguage)" v-hasPermi="['admin:lang:update']">编辑</a-button>
              <a-button 
                type="link" 
                danger
                v-hasPermi="['admin:lang:delete']"
              >
                <a-popconfirm
                  title="确定要删除吗？"
                  @confirm="handleDelete(record as HbtLanguage)"
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

      <!-- 语言表单 -->
      <language-form
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
  getHbtLanguageList,
  getHbtLanguage,
  createHbtLanguage,
  updateHbtLanguage,
  deleteHbtLanguage,
  batchDeleteHbtLanguage,
  importHbtLanguage,
  exportHbtLanguage,
  getHbtLanguageTemplate,
  updateHbtLanguageStatus
} from '@/api/admin/hbtLanguage'
import type {
  HbtLanguage,
  HbtLanguageQuery,
  HbtLanguageCreate,
  HbtLanguageUpdate
} from '@/types/admin/language'
import LanguageForm from './components/LanguageForm.vue'


// 查询参数
const queryParams = reactive<HbtLanguageQuery>({
  pageIndex: 1,
  pageSize: 10,
  langCode: '',
  langName: '',
  status: undefined
})

// 状态定义
const loading = ref(false)
const languageList = ref<HbtLanguage[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const modalVisible = ref(false)
const modalTitle = ref('新增语言')
const currentRecord = ref<HbtLanguage>()
const total = ref(0)

// 表格列定义
const columns: ColumnType<HbtLanguage>[] = [
  {
    title: '语言代码',
    dataIndex: 'langCode',
    key: 'langCode',
    width: 150
  },
  {
    title: '语言名称',
    dataIndex: 'langName',
    key: 'langName',
    width: 150
  },
  {
    title: '排序号',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
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

// 加载语言列表
const loadLanguageList = async () => {
  loading.value = true
  try {
    const { data } = await getHbtLanguageList({
      ...queryParams,
      pageIndex: queryParams.pageNum,
      pageSize: queryParams.pageSize
    })
    if (data?.code === 200) {
      languageList.value = data.data.rows
      total.value = data.data.totalNum
    } else {
      message.error(data?.msg || '加载语言列表失败')
      languageList.value = []
      total.value = 0
    }
  } catch (error) {
    console.error('加载语言列表失败:', error)
    message.error('加载语言列表失败')
    languageList.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 页码变化处理
const handlePageChange = (page: number, size: number) => {
  queryParams.pageNum = page
  queryParams.pageSize = size
  loadLanguageList()
}

// 选择变化处理
const onSelectChange = (keys: (string | number)[]) => {
  selectedRowKeys.value = keys
}

// 搜索处理
const handleSearch = () => {
  queryParams.pageNum = 1
  loadLanguageList()
}

// 重置处理
const handleReset = () => {
  queryParams.langCode = ''
  queryParams.langName = ''
  queryParams.status = undefined
  queryParams.pageNum = 1
  loadLanguageList()
}

// 刷新处理
const handleRefresh = () => {
  loadLanguageList()
}

// 新增处理
const handleAdd = () => {
  modalTitle.value = '新增语言'
  currentRecord.value = undefined
  modalVisible.value = true
}

// 编辑处理
const handleEdit = async (record: HbtLanguage) => {
  try {
    const { data } = await getHbtLanguage(record.id)
    if (data?.code === 200) {
      modalTitle.value = '编辑语言'
      currentRecord.value = data.data
      modalVisible.value = true
    } else {
      message.error(data?.msg || '获取语言详情失败')
    }
  } catch (error) {
    console.error('获取语言详情失败:', error)
    message.error('获取语言详情失败')
  }
}

// 删除处理
const handleDelete = async (record: HbtLanguage) => {
  try {
    const { data } = await deleteHbtLanguage(record.id)
    if (data?.code === 200) {
      message.success('删除成功')
      loadLanguageList()
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
    const { data } = await batchDeleteHbtLanguage(selectedRowKeys.value.map(Number))
    if (data?.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      loadLanguageList()
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
    const { data } = await importHbtLanguage(file)
    if (data?.code === 200) {
      message.success('导入成功')
      loadLanguageList()
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
    const { data } = await exportHbtLanguage(queryParams)
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '语言数据.xlsx'
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
    const { data } = await getHbtLanguageTemplate()
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '语言导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

// 状态变更处理
const handleStatusChange = async (record: HbtLanguage, checked: boolean) => {
  try {
    record.statusLoading = true
    const { data } = await updateHbtLanguageStatus(record.id, checked ? 0 : 1)
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
const handleSubmit = async (formData: HbtLanguageCreate | HbtLanguageUpdate) => {
  try {
    const { data } = await (currentRecord.value
      ? updateHbtLanguage(formData as HbtLanguageUpdate)
      : createHbtLanguage(formData as HbtLanguageCreate))
      
    if (data?.code === 200) {
      message.success(`${currentRecord.value ? '更新' : '创建'}成功`)
      modalVisible.value = false
      loadLanguageList()
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
  loadLanguageList()
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