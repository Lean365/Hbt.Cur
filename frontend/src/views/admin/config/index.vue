//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置管理页面
//===================================================================

<template>
  <div class="hbt-config">
    <!-- 查询区域 -->
    <hbt-query
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item label="配置名称">
          <a-input
            v-model:value="queryParams.configName"
            placeholder="请输入配置名称"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="配置键名">
          <a-input
            v-model:value="queryParams.configKey"
            placeholder="请输入配置键名"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="配置类型">
          <hbt-select
            v-model:value="queryParams.configType"
            :options="configTypeOptions"
            placeholder="请选择配置类型"
            allow-clear
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
      :show-import="true"
      :show-export="true"
      v-hasPermi="['admin:config:add', 'admin:config:edit', 'admin:config:delete', 'admin:config:import', 'admin:config:export']"
      @add="handleAdd"
      @edit="handleBatchEdit"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
      @export="handleExport"
      @refresh="loadConfigList"
      @column-setting="handleColumnSetting"
    />

    <!-- 数据表格 -->
    <hbt-table
      :columns="columns"
      :data-source="configList"
      :loading="loading"
      :row-selection="{ selectedRowKeys, onChange: onSelectChange }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 配置类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'configType'">
          <a-tag :color="record.configType === 0 ? 'blue' : 'green'">
            {{ configTypeOptions.find(item => item.value === record.configType)?.label || record.configType }}
          </a-tag>
        </template>

        <!-- 状态列 -->
        <template v-else-if="column.dataIndex === 'status'">
          <a-tag :color="record.status ? 'success' : 'error'">
            {{ statusOptions.find(item => item.value === record.status)?.label || (record.status ? t('common.status.normal') : t('common.status.disabled')) }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-else-if="column.dataIndex === 'operation'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :show-edit="true"
            :show-delete="true"
            v-hasPermi="['admin:config:query', 'admin:config:edit', 'admin:config:delete']"
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
      :Total="pagination.total"
      @change="handleTableChange"
    />

    <!-- 表单弹窗 -->
    <config-form
      v-model:visible="formVisible"
      :title="formTitle"
      :loading="formLoading"
      :model="formData"
      :config-type-options="configTypeOptions"
      :status-options="statusOptions"
      @ok="handleFormOk"
      @cancel="handleFormCancel"
    />

    <!-- 详情弹窗 -->
    <config-detail
      v-model:visible="detailVisible"
      :loading="detailLoading"
      :model="detailData"
      @close="handleDetailClose"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { hasPermi } from '@/directives/permission'
import { useDictData } from '@/hooks/useDictData'

// 引入标准组件
import HbtQuery from '@/components/Business/Query/index.vue'
import HbtToolbar from '@/components/Business/Toolbar/index.vue'
import HbtTable from '@/components/Business/Table/index.vue'
import HbtOperation from '@/components/Business/Operation/index.vue'
import HbtPagination from '@/components/Business/Pagination/index.vue'
import HbtSelect from '@/components/Business/Select/index.vue'

// 引入业务组件
import ConfigForm from './components/ConfigForm.vue'
import ConfigDetail from './components/ConfigDetail.vue'

// 引入API和类型
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
  updateHbtConfigStatus,
  batchUpdateHbtConfigStatus
} from '@/api/admin/hbtConfig'
import type { HbtConfig, HbtConfigQuery } from '@/types/admin/config'
import { getDictDataList } from '@/api/admin/dictData'
import type { DictData } from '@/types/admin/dictData'

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const configList = ref<HbtConfig[]>([])
const selectedRowKeys = ref<number[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formLoading = ref(false)
const formData = ref<Partial<HbtConfig>>({})
const detailVisible = ref(false)
const detailLoading = ref(false)
const detailData = ref<HbtConfig>()

// === 字典数据 ===
const { dictDataMap, loading: dictLoading } = useDictData([
  'sys_config_type',
  'sys_normal_disable'
])

// 计算属性：配置类型选项
const configTypeOptions = computed(() => {
  return dictDataMap.value['sys_config_type'] || []
})

// 计算属性：状态选项
const statusOptions = computed(() => {
  return dictDataMap.value['sys_normal_disable'] || []
})

// === 查询参数 ===
const queryParams = ref<HbtConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  configName: '',
  configKey: '',
  configType: undefined,
  configBuiltin: undefined,
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

// === 表格列定义 ===
const columns = [
  {
    title: '配置名称',
    dataIndex: 'configName',
    width: 200,
    ellipsis: true
  },
  {
    title: '配置键名',
    dataIndex: 'configKey',
    width: 200,
    ellipsis: true
  },
  {
    title: '配置值',
    dataIndex: 'configValue',
    width: 300,
    ellipsis: true
  },
  {
    title: '配置类型',
    dataIndex: 'configType',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 100
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
    name: 'configName',
    label: '配置名称',
    type: 'input'
  },
  {
    name: 'configKey',
    label: '配置键名',
    type: 'input'
  },
  {
    name: 'configType',
    label: '配置类型',
    type: 'select',
    options: configTypeOptions
  },
  {
    name: 'status',
    label: '状态',
    type: 'select',
    options: statusOptions
  }
]

// === 方法定义 ===
// 加载配置列表
const loadConfigList = async () => {
  try {
    loading.value = true
    const res = await getHbtConfigList(queryParams.value)
    configList.value = res.data.rows
    pagination.value.total = res.data.total
  } catch (error) {
    console.error('加载配置列表失败:', error)
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  loadConfigList()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    configName: '',
    configKey: '',
    configType: undefined,
    configBuiltin: undefined,
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
  loadConfigList()
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
  formData.value = {}
  formVisible.value = true
}

// 编辑
const handleEdit = async (record: HbtConfig) => {
  try {
    formTitle.value = t('common.title.edit')
    formLoading.value = true
    const res = await getHbtConfig(record.configId)
    formData.value = res.data
    formVisible.value = true
  } catch (error) {
    message.error(t('common.message.loadFailed'))
  } finally {
    formLoading.value = false
  }
}

// 查看
const handleView = async (record: HbtConfig) => {
  try {
    detailLoading.value = true
    const res = await getHbtConfig(record.configId)
    detailData.value = res.data
    detailVisible.value = true
  } catch (error) {
    message.error(t('common.message.loadFailed'))
  } finally {
    detailLoading.value = false
  }
}

// 删除
const handleDelete = async (record: HbtConfig) => {
  try {
    await deleteHbtConfig(record.configId)
    message.success(t('common.message.deleteSuccess'))
    loadConfigList()
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
    await batchDeleteHbtConfig(selectedRowKeys.value)
    message.success(t('common.message.deleteSuccess'))
    selectedRowKeys.value = []
    loadConfigList()
  } catch (error) {
    message.error(t('common.message.deleteFailed'))
  }
}

// 导入
const handleImport = () => {
  // TODO: 实现导入功能
}

// 下载模板
const handleTemplate = () => {
  // TODO: 实现下载模板功能
}

// 导出
const handleExport = () => {
  // TODO: 实现导出功能
}

// 表单确认
const handleFormOk = async () => {
  try {
    formLoading.value = true
    if (formData.value.configId) {
      await updateHbtConfig(formData.value as HbtConfig)
      message.success(t('common.message.updateSuccess'))
    } else {
      await createHbtConfig(formData.value as HbtConfig)
      message.success(t('common.message.createSuccess'))
    }
    formVisible.value = false
    loadConfigList()
  } catch (error) {
    message.error(formData.value.configId ? t('common.message.updateFailed') : t('common.message.createFailed'))
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
  loadConfigList()
})
</script>

<style lang="less" scoped>
.hbt-config {
  padding: 24px;
  //background-color: #fff;
}
</style>