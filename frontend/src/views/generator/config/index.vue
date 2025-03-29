<template>
  <div class="config-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['generator:config:create']"
      :show-edit="true"
      :edit-permission="['generator:config:update']"
      :show-delete="true"
      :delete-permission="['generator:config:delete']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @refresh="fetchData"
      @column-setting="handleColumnSettingChange"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns.filter(col => columnSettings[col.key])"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: HbtGenConfigDto) => String(record.id)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"     
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :edit-permission="['generator:config:update']"
            :delete-permission="['generator:config:delete']"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 添加/修改配置对话框 -->
    <config-form
      v-model:visible="formVisible"
      :title="formTitle"
      :config-id="selectedConfigId"
      @success="handleSuccess"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :visible="columnSettingVisible"
      title="列设置"
      placement="right"
      width="300"
      @close="columnSettingVisible = false"
    >
      <a-checkbox-group
        :value="Object.keys(columnSettings).filter(key => columnSettings[key])"
        @change="handleColumnSettingChange"
        class="column-setting-group"
      >
        <div v-for="col in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtGenConfigDto, HbtGenConfigQuery, HbtGenConfigPagedResult } from '@/types/generator/config'
import { 
  getPagedList,
  getGenConfig,
  createGenConfig,
  updateGenConfig,
  deleteGenConfig,
  batchDeleteGenConfig
} from '@/api/generator/genConfig'
import ConfigForm from './components/ConfigForm.vue'

const { t } = useI18n()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'tableName',
    label: '表名',
    type: 'input',
    placeholder: '请输入表名'
  },
  {
    name: 'author',
    label: '作者',
    type: 'input',
    placeholder: '请输入作者'
  },
  {
    name: 'moduleName',
    label: '模块名',
    type: 'input',
    placeholder: '请输入模块名'
  },
  {
    name: 'businessName',
    label: '业务名',
    type: 'input',
    placeholder: '请输入业务名'
  }
]

// 表格列定义
const columns = [
  {
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName',
    width: 120,
    ellipsis: true
  },
  {
    title: '作者',
    dataIndex: 'author',
    key: 'author',
    width: 100,
    ellipsis: true
  },
  {
    title: '模块名',
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 120,
    ellipsis: true
  },
  {
    title: '包名',
    dataIndex: 'packageName',
    key: 'packageName',
    width: 150,
    ellipsis: true
  },
  {
    title: '业务名',
    dataIndex: 'businessName',
    key: 'businessName',
    width: 120,
    ellipsis: true
  },
  {
    title: '功能名',
    dataIndex: 'functionName',
    key: 'functionName',
    width: 120,
    ellipsis: true
  },
  {
    title: '生成类型',
    dataIndex: 'genType',
    key: 'genType',
    width: 100
  },
  {
    title: '生成路径',
    dataIndex: 'genPath',
    key: 'genPath',
    width: 200,
    ellipsis: true
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '更新时间',
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 180,
    fixed: 'right'
  }
]

// 默认列设置
const defaultColumns = columns.map(col => ({
  key: col.key,
  title: col.title
}))

// 列设置状态
const columnSettings = ref<Record<string, boolean>>(
  defaultColumns.reduce((acc, col) => {
    acc[col.key] = true
    return acc
  }, {} as Record<string, boolean>)
)

// 状态定义
const loading = ref(false)
const tableData = ref<HbtGenConfigDto[]>([])
const total = ref(0)
const queryParams = ref<HbtGenConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  tableName: '',
  author: '',
  moduleName: '',
  businessName: ''
})
const selectedRowKeys = ref<(string | number)[]>([])
const selectedConfigId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const columnSettingVisible = ref(false)
const showSearch = ref(true)

// 生命周期钩子
onMounted(() => {
  fetchData()
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getPagedList(queryParams.value)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || '获取数据失败')
    }
  } catch (error) {
    console.error('获取数据失败:', error)
    message.error('获取数据失败')
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  selectedRowKeys.value = []
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    tableName: '',
    author: '',
    moduleName: '',
    businessName: ''
  }
  selectedRowKeys.value = []
  handleQuery()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 行点击事件 */
const handleRowClick = (record: HbtGenConfigDto) => {
  selectedConfigId.value = record.id
}

/** 新增按钮操作 */
const handleAdd = () => {
  selectedConfigId.value = undefined
  formTitle.value = '新增生成配置'
  formVisible.value = true
}

/** 编辑选中行 */
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录')
    return
  }
  selectedConfigId.value = Number(selectedRowKeys.value[0])
  formTitle.value = '修改生成配置'
  formVisible.value = true
}

/** 编辑按钮操作 */
const handleEdit = (record: HbtGenConfigDto) => {
  selectedConfigId.value = record.id
  formTitle.value = '修改生成配置'
  formVisible.value = true
}

/** 删除按钮操作 */
const handleDelete = async (record: HbtGenConfigDto) => {
  if (!record.id) {
    message.error('无效的配置ID')
    return
  }
  try {
    const res = await deleteGenConfig(record.id)
    if (res.code === 200) {
      message.success('删除成功')
      fetchData()
    } else {
      message.error(res.msg || '删除失败')
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

/** 批量删除按钮操作 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    const res = await batchDeleteGenConfig(selectedRowKeys.value.map(Number))
    if (res.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.msg || '批量删除失败')
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error('批量删除失败')
  }
}

/** 列设置变更 */
const handleColumnSettingChange = (checkedValues: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    settings[col.key] = checkedValues.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('configColumnSettings', JSON.stringify(settings))
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 切换全屏显示状态 */
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

/** 表单提交成功 */
const handleSuccess = () => {
  fetchData()
}
</script>

<style lang="less" scoped>
.config-container {
  padding: 16px;
}
.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}
.column-setting-item {
  padding: 4px 0;
}
</style> 