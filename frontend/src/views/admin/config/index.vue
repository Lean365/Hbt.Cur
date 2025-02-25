//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置管理页面
//===================================================================

<template>
  <div class="p-6">
    <a-card>
      <!-- 搜索表单 -->
      <a-form layout="inline" :model="queryParams">
        <a-form-item label="配置名称">
          <a-input v-model:value="queryParams.configName" placeholder="请输入配置名称" allowClear />
        </a-form-item>
        <a-form-item label="配置键名">
          <a-input v-model:value="queryParams.configKey" placeholder="请输入配置键名" allowClear />
        </a-form-item>
        <a-form-item label="系统内置">
          <a-select v-model:value="queryParams.configBuiltin" placeholder="请选择" allowClear>
            <a-select-option :value="0">否</a-select-option>
            <a-select-option :value="1">是</a-select-option>
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
            <a-button type="primary" @click="handleSearch" v-hasPermi="['admin:config:list']">查询</a-button>
            <a-button @click="handleReset">重置</a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="mb-4 flex justify-between">
        <div>
          <a-space>
            <a-button type="primary" @click="handleAdd" v-hasPermi="['admin:config:create']">
              <plus-outlined />新增
            </a-button>
            <a-button 
              :disabled="!selectedRowKeys.length"
              @click="handleBatchDelete"
              v-hasPermi="['admin:config:delete']"
            >
              <delete-outlined />批量删除
            </a-button>
            <a-upload
              name="file"
              :show-upload-list="false"
              :before-upload="handleImport"
              v-hasPermi="['admin:config:import']"
            >
              <a-button>
                <upload-outlined />导入
              </a-button>
            </a-upload>
            <a-button @click="handleExport" v-hasPermi="['admin:config:export']">
              <download-outlined />导出
            </a-button>
            <a-button @click="handleTemplate" v-hasPermi="['admin:config:query']">
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

      <!-- 配置表格 -->
      <a-table
        :loading="loading"
        :columns="columns"
        :data-source="configList"
        :pagination="false"
        :row-selection="{
          selectedRowKeys: selectedRowKeys,
          onChange: (selectedKeys: (string | number)[], selectedRows: HbtConfig[]) => onSelectChange(selectedKeys)
        }"
        row-key="configId"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'configBuiltin'">
            {{ record.configBuiltin === 1 ? '是' : '否' }}
          </template>
          <template v-else-if="column.key === 'status'">
            <span v-hasPermi="['admin:config:update']">
              <a-switch
                :checked="(record as HbtConfig).status === 0"
                :loading="(record as HbtConfig).statusLoading"
                @change="(checked: any) => handleStatusChange(record as HbtConfig, Boolean(checked))"
              />
            </span>
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a @click="handleEdit(record as HbtConfig)" v-hasPermi="['admin:config:update']">编辑</a>
              <a-button 
                type="link" 
                danger
                v-hasPermi="['admin:config:delete']"
              >
                <a-popconfirm
                  title="确定要删除吗？"
                  @confirm="handleDelete(record as HbtConfig)"
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
        v-model:Current="queryParams.pageIndex"
        v-model:PageSize="queryParams.pageSize"
        :Total="total"
        :ShowTotal="showTotal"
        ShowQuickJumper
        ShowSizeChanger
        @change="handlePageChange"
      />

      <!-- 配置表单 -->
      <config-form
        v-model:visible="modalVisible"
        :title="modalTitle"
        :record="currentRecord"
        @submit="handleSubmit"
      />
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, h } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { ColumnType } from 'ant-design-vue/es/table'
import type { Switch } from 'ant-design-vue'
import { 
  PlusOutlined, 
  DeleteOutlined,
  UploadOutlined,
  DownloadOutlined,
  FileOutlined,
  ReloadOutlined 
} from '@ant-design/icons-vue'
import {
  getHbtConfigList,
  getHbtConfig,
  createHbtConfig,
  updateHbtConfig,
  deleteHbtConfig,
  batchDeleteHbtConfig,
  importHbtConfig,
  exportHbtConfig,
  getHbtConfigTemplate,
  updateHbtConfigStatus
} from '@/api/admin/hbtConfig'
import type {
  HbtConfig,
  HbtConfigQuery,
  HbtConfigCreate,
  HbtConfigUpdate
} from '@/types/admin/hbtConfig'
import ConfigForm from './components/ConfigForm.vue'
import HbtPagination from '@/components/pagination/index.vue'

// 使用i18n
const { t } = useI18n()

// 查询参数
const queryParams = reactive<HbtConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  configName: '',
  configKey: '',
  configBuiltin: undefined,
  status: undefined,
  beginTime: '',
  endTime: ''
})

// 状态定义
const loading = ref(false)
const configList = ref<HbtConfig[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const modalVisible = ref(false)
const modalTitle = ref('新增配置')
const currentRecord = ref<HbtConfig>()
const total = ref(0)

// 表格列定义
const columns: ColumnType<HbtConfig>[] = [
  {
    title: '配置名称',
    dataIndex: 'configName',
    key: 'configName',
    width: 200
  },
  {
    title: '配置键名',
    dataIndex: 'configKey',
    key: 'configKey',
    width: 200
  },
  {
    title: '配置键值',
    dataIndex: 'configValue',
    key: 'configValue',
    width: 200,
    ellipsis: true
  },
  {
    title: '系统内置',
    dataIndex: 'configBuiltin',
    key: 'configBuiltin',
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

// 加载配置列表
const loadConfigList = async () => {
  loading.value = true
  try {
    const params = {
      ...queryParams
    }
    console.log('发送请求参数:', {
      pageIndex: params.pageIndex,
      pageSize: params.pageSize,
      其他参数: params
    })
    
    const { data } = await getHbtConfigList(params)
    console.log('配置列表响应:', {
      总数: data.totalNum,
      当前页: params.pageIndex,
      每页条数: params.pageSize,
      数据条数: data.rows?.length
    })
    
    configList.value = data.rows
    total.value = data.totalNum
  } catch (error) {
    console.error('加载配置列表失败:', error)
    message.error('加载配置列表失败')
    configList.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 页码变化处理
const handlePageChange = (page: number, size: number) => {
  console.log('页码变化:', { 
    原页码: queryParams.pageIndex,
    新页码: page,
    原每页条数: queryParams.pageSize,
    新每页条数: size
  })
  queryParams.pageIndex = page
  queryParams.pageSize = size
  loadConfigList()
}

// 选择变化处理
const onSelectChange = (keys: (string | number)[]) => {
  selectedRowKeys.value = keys
}

// 搜索处理
const handleSearch = () => {
  queryParams.pageIndex = 1
  loadConfigList()
}

// 重置处理
const handleReset = () => {
  queryParams.configName = ''
  queryParams.configKey = ''
  queryParams.configBuiltin = undefined
  queryParams.status = undefined
  queryParams.beginTime = ''
  queryParams.endTime = ''
  queryParams.pageIndex = 1
  loadConfigList()
}

// 刷新处理
const handleRefresh = () => {
  loadConfigList()
}

// 新增处理
const handleAdd = () => {
  modalTitle.value = '新增配置'
  currentRecord.value = undefined
  modalVisible.value = true
}

// 编辑处理
const handleEdit = async (record: HbtConfig) => {
  try {
    console.log('[编辑配置] 开始编辑配置:', record)
    
    // 参数验证
    if (!record) {
      message.error('配置记录不能为空')
      return
    }
    
    if (typeof record.configId !== 'number' || record.configId <= 0) {
      message.error('无效的配置ID')
      return
    }

    // 调用API
    const { data } = await getHbtConfig(record.configId)
    console.log('[编辑配置] API响应:', data)
    
    if (data?.code === 200) {
      modalTitle.value = '编辑配置'
      currentRecord.value = data.data
      modalVisible.value = true
    } else {
      message.error(data?.msg || '获取配置详情失败')
    }
  } catch (error: any) {
    console.error('[编辑配置] 获取配置详情失败:', error)
    message.error(error?.response?.data?.msg || error?.message || '获取配置详情失败')
  }
}

// 删除处理
const handleDelete = async (record: HbtConfig) => {
  try {
    const { data } = await deleteHbtConfig(record.configId)
    if (data?.code === 200) {
      message.success('删除成功')
      loadConfigList()
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
    const { data } = await batchDeleteHbtConfig(selectedRowKeys.value.map(Number))
    if (data?.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      loadConfigList()
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
    const { data } = await importHbtConfig(file)
    if (data?.code === 200) {
      message.success('导入成功')
      loadConfigList()
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
    const { data } = await exportHbtConfig(queryParams)
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '系统配置.xlsx'
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
    const { data } = await getHbtConfigTemplate()
    const url = window.URL.createObjectURL(new Blob([data]))
    const link = document.createElement('a')
    link.href = url
    link.download = '系统配置导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error('下载模板失败')
  }
}

// 状态变更处理
const handleStatusChange = async (record: HbtConfig, checked: boolean) => {
  try {
    record.statusLoading = true
    const { data } = await updateHbtConfigStatus(record.configId, checked ? 0 : 1)
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
const handleSubmit = async (formData: HbtConfigCreate | HbtConfigUpdate) => {
  try {
    const { data } = await (currentRecord.value
      ? updateHbtConfig(formData as HbtConfigUpdate)
      : createHbtConfig(formData as HbtConfigCreate))
      
    if (data?.code === 200) {
      message.success(`${currentRecord.value ? '更新' : '创建'}成功`)
      modalVisible.value = false
      loadConfigList()
    } else {
      message.error(data?.msg || `${currentRecord.value ? '更新' : '创建'}失败`)
    }
  } catch (error) {
    console.error(`${currentRecord.value ? '更新' : '创建'}失败:`, error)
    message.error(`${currentRecord.value ? '更新' : '创建'}失败`)
  }
}

// 显示总数
const showTotal = (total: number, range: [number, number]) => {
  return h('span', `第 ${range[0]}-${range[1]} 条，共 ${total} 条`)
}

// 组件挂载时加载数据
onMounted(() => {
  loadConfigList()
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