<template>
  <div class="template-container">
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
      :add-permission="['generator:template:create']"
      :show-edit="true"
      :edit-permission="['generator:template:update']"
      :show-delete="true"
      :delete-permission="['generator:template:delete']"
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
      :row-key="(record: HbtGenTemplateDto) => String(record.id)"
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
          <a-space>
            <a @click="handleEdit(record)" v-hasPermi="['generator:template:update']">修改</a>
            <a-divider type="vertical" />
            <a-popconfirm
              title="确定要删除该模板吗？"
              @confirm="handleDelete(record)"
              ok-text="确定"
              cancel-text="取消"
            >
              <a class="text-danger" v-hasPermi="['generator:template:delete']">删除</a>
            </a-popconfirm>
          </a-space>
        </template>
      </template>
    </hbt-table>

    <!-- 添加/修改模板对话框 -->
    <template-form
      v-model:visible="formVisible"
      :title="formTitle"
      :template-id="selectedTemplateId"
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
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtGenTemplateDto } from '@/types/generator/template'
import { 
  getPagedList, 
  getGenTemplate,
  createGenTemplate, 
  updateGenTemplate, 
  deleteGenTemplate
} from '@/api/generator/genTemplate'
import TemplateForm from './components/TemplateForm.vue'


// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'templateName',
    label: '模板名称',
    type: 'input',
    placeholder: '请输入模板名称'
  },
  {
    name: 'fileName',
    label: '文件名',
    type: 'input',
    placeholder: '请输入文件名'
  }
]

// 表格列定义
const columns = [
  {
    title: '模板名称',
    dataIndex: 'templateName',
    key: 'templateName',
    width: 120,
    ellipsis: true
  },
  {
    title: '文件名',
    dataIndex: 'fileName',
    key: 'fileName',
    width: 120,
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
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
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
const columnSettings = reactive(
  defaultColumns.reduce((acc, col) => {
    acc[col.key] = true
    return acc
  }, {} as Record<string, boolean>)
)

// 状态定义
const loading = ref(false)
const tableData = ref<HbtGenTemplateDto[]>([])
const total = ref(0)
const queryParams = reactive({
  pageIndex: 1,
  pageSize: 10,
  templateName: undefined,
  fileName: undefined
})
const selectedRowKeys = ref<string[]>([])
const selectedTemplateId = ref<number>()
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
    const res = await getPagedList(queryParams)
    if (res.data) {
      tableData.value = res.data
      total.value = res.data.length
    }
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.templateName = undefined
  queryParams.fileName = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 行点击事件 */
const handleRowClick = (record: HbtGenTemplateDto) => {
  selectedTemplateId.value = record.id
}

/** 新增按钮操作 */
const handleAdd = () => {
  selectedTemplateId.value = undefined
  formTitle.value = '新增代码模板'
  formVisible.value = true
}

/** 编辑选中行 */
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录')
    return
  }
  selectedTemplateId.value = Number(selectedRowKeys.value[0])
  formTitle.value = '修改代码模板'
  formVisible.value = true
}

/** 编辑按钮操作 */
const handleEdit = (record: HbtGenTemplateDto) => {
  selectedTemplateId.value = record.id
  formTitle.value = '修改代码模板'
  formVisible.value = true
}

/** 删除按钮操作 */
const handleDelete = async (record: HbtGenTemplateDto) => {
  if (!record.id) return
  try {
    await deleteGenTemplate(record.id)
    message.success('删除成功')
    fetchData()
  } catch (error) {
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
    await Promise.all(selectedRowKeys.value.map(id => deleteGenTemplate(Number(id))))
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量删除失败')
  }
}

/** 列设置变更 */
const handleColumnSettingChange = (checkedValues: any[]) => {
  Object.keys(columnSettings).forEach(key => {
    columnSettings[key] = checkedValues.includes(key)
  })
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
.template-container {
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