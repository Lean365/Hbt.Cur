<template>
  <div class="workflow-form-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['workflow:manage:form:create']"
      :show-edit="true"
      :edit-permission="['workflow:manage:form:update']"
      :show-delete="true"
      :delete-permission="['workflow:manage:form:delete']"
      :show-import="true"
      :import-permission="['workflow:manage:form:import']"
      :show-export="true"
      :export-permission="['workflow:manage:form:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
      @export="handleExport"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    >
    </hbt-toolbar>

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="visibleColumns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: HbtForm) => String(record.formId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 表单分类列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'formCategory'">
          <hbt-dict-tag dict-type="workflow_form_category" :value="record.formCategory" />
        </template>
      
        <!-- 表单类型列 -->
        <template v-if="column.dataIndex === 'formType'">
          <hbt-dict-tag dict-type="workflow_form_type" :value="record.formType" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="workflow_form_status" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['workflow:manage:form:update']"
            :show-delete="true"
            :delete-permission="['workflow:manage:form:delete']"
            :show-design="true"
            :design-permission="['workflow:manage:form:design']"
            :show-preview="true"
            :preview-permission="['workflow:manage:form:preview']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @design="handleDesign"
            @preview="handlePreview"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 表单表单对话框 -->
    <form-form
      v-model:open="formFormVisible"
      :form-id="selectedFormId ?? undefined"
      @success="handleFormSuccess"
    />

    <!-- 表单设计器对话框 -->
    <a-modal
      v-model:open="designerModalVisible"
      title="表单设计器"
      width="95%"
      :footer="null"
      :destroy-on-close="true"
    >
      <form-designer
        v-if="designerModalVisible"
        :form-id="selectedFormId ?? undefined"
        @save="handleDesignerSave"
        @cancel="designerModalVisible = false"
      />
    </a-modal>

    <!-- 表单预览对话框 -->
    <a-modal
      v-model:open="previewModalVisible"
      title="表单预览"
      width="80%"
      :footer="null"
    >
      <div class="preview-container">
        <hbt-form
          v-if="previewFormConfig"
          :model-value="previewFormConfig"
          :readonly="true"
          :height="'500px'"
        />
      </div>
    </a-modal>


  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { h } from 'vue'
import { useDictStore } from '@/stores/dict'
import FormForm from './components/FormForm.vue'
import FormDesigner from './components/FormDesigner.vue'
import { getFormList, getFormById, deleteForm, batchDeleteForm, importForm, exportForm, getFormTemplate, updateForm } from '@/api/workflow/form'
import type { HbtForm, HbtFormQuery } from '@/types/workflow/form'
import type { QueryField } from '@/types/components/query'

// 字典store
const dictStore = useDictStore()

// 响应式数据
const loading = ref(false)
const tableData = ref<HbtForm[]>([])
const selectedRowKeys = ref<string[]>([])
const total = ref(0)
const showSearch = ref(true)

// 对话框状态
const formFormVisible = ref(false)
const designerModalVisible = ref(false)
const previewModalVisible = ref(false)
const selectedFormId = ref<number | null>(null)
const previewFormConfig = ref('')

// 查询参数
const queryParams = reactive<HbtFormQuery>({
  pageIndex: 1,
  pageSize: 10,
  formName: '',
  formCategory: undefined,
  formType: undefined
})

// 查询字段配置
const queryFields: QueryField[] = [
  {
    name: 'formName',
    label: '表单名称',
    type: 'input' as const,
    props: {
      placeholder: '请输入表单名称'
    }
  },
  {
    name: 'formCategory',
    label: '表单分类',
    type: 'select' as const,
    props: {
      placeholder: '请选择表单分类',
      dictType: 'workflow_form_category'
    }
  },
  {
    name: 'formType',
    label: '表单类型',
    type: 'select' as const,
    props: {
      placeholder: '请选择表单类型',
      dictType: 'workflow_form_type'
    }
  }
]

// 表格列配置
const columns = [
  {
    title: '表单ID',
    dataIndex: 'formId',
    key: 'formId',
    width: 100,
    fixed: 'left'
  },
  {
    title: '表单名称',
    dataIndex: 'formName',
    key: 'formName',
    width: 200,
    ellipsis: true
  },
  {
    title: '表单键',
    dataIndex: 'formKey',
    key: 'formKey',
    width: 150
  },
  {
    title: '表单分类',
    dataIndex: 'formCategory',
    key: 'formCategory',
    width: 120
  },
  {
    title: '表单类型',
    dataIndex: 'formType',
    key: 'formType',
    width: 120
  },
  {
    title: '版本',
    dataIndex: 'version',
    key: 'version',
    width: 80
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
    width: 200,
    fixed: 'right'
  }
]

// 计算属性
const visibleColumns = computed(() => columns)

// 方法
const fetchData = async () => {
  loading.value = true
  try {
    const result = await getFormList(queryParams)
    if (result.data.code === 200) {
      tableData.value = result.data.data.rows || []
      total.value = result.data.data.totalNum || 0
    }
  } catch (error) {
    console.error('加载表单列表失败:', error)
    message.error('加载表单列表失败')
  } finally {
    loading.value = false
  }
}

const handleQuery = (params: any) => {
  Object.assign(queryParams, params)
  queryParams.pageIndex = 1
  fetchData()
}

const resetQuery = () => {
  Object.assign(queryParams, {
    pageIndex: 1,
    pageSize: 10,
    formName: '',
    formCategory: undefined,
    formType: undefined
  })
  fetchData()
}

const handleTableChange = (filters: any, sorter: any) => {
  // 处理表格变化
}

const handleRowClick = (record: HbtForm) => {
  // 处理行点击
}

const handlePageChange = (page: number) => {
  queryParams.pageIndex = page
  fetchData()
}

const handleSizeChange = (current: number, size: number) => {
  queryParams.pageIndex = 1
  queryParams.pageSize = size
  fetchData()
}

const handleAdd = () => {
  selectedFormId.value = null
  formFormVisible.value = true
}

const handleEdit = (record: HbtForm) => {
  selectedFormId.value = record.formId
  formFormVisible.value = true
}

const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => String(item.formId) === selectedRowKeys.value[0])
    if (record) {
      handleEdit(record)
    }
  }
}



const handleDesign = (record: HbtForm) => {
  selectedFormId.value = record.formId
  designerModalVisible.value = true
}

const handlePreview = async (record: HbtForm) => {
  try {
    const result = await getFormById(record.formId)
    if (result.data.code === 200) {
      previewFormConfig.value = result.data.data.formConfig
      previewModalVisible.value = true
    }
  } catch (error) {
    console.error('加载表单配置失败:', error)
    message.error('加载表单配置失败')
  }
}

const handleDelete = async (record: HbtForm) => {
  try {
    await deleteForm(record.formId)
    message.success('删除成功')
    fetchData()
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要删除的表单')
    return
  }
  
  try {
    const formIds = selectedRowKeys.value.map(key => Number(key))
    await batchDeleteForm(formIds)
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error('批量删除失败')
  }
}

const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      
      try {
        const res = await importForm(file)
        const { success = 0, fail = 0 } = (res.data as any).Data || {}
        
        if (success > 0 && fail === 0) {
          message.success(`导入成功${success}条，全部成功！`)
        } else if (success > 0 && fail > 0) {
          message.warning(`导入成功${success}条，失败${fail}条`)
        } else if (success === 0 && fail > 0) {
          message.error(`全部导入失败，共${fail}条`)
        } else {
          message.info('未读取到任何数据')
        }
        
        if (success > 0) fetchData()
      } catch (error: any) {
        console.error('导入失败:', error)
        message.error(error.message || '导入失败')
      }
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || '导入失败')
  }
}

const handleExport = async () => {
  try {
    const res = await exportForm(queryParams)
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `表单数据_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success('导出成功')
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || '导出失败')
  }
}

const handleTemplate = async () => {
  try {
    const res = await getFormTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `表单导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success('模板下载成功')
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || '下载模板失败')
  }
}

const handleColumnSetting = () => {
  // 列设置功能可以通过hbt-table组件的内置功能实现
  message.info('列设置功能已集成到表格组件中')
}

const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

const toggleFullscreen = () => {
  // 全屏功能可以通过浏览器API实现
  if (document.fullscreenElement) {
    document.exitFullscreen()
  } else {
    document.documentElement.requestFullscreen()
  }
}

const handleFormSuccess = () => {
  formFormVisible.value = false
  fetchData()
}

const handleDesignerSave = async (formConfig: string) => {
  try {
    if (!selectedFormId.value) {
      message.error('表单ID不能为空')
      return
    }
    
    // 更新表单配置
    const result = await updateForm({
      formId: selectedFormId.value,
      formConfig: formConfig
    } as any)
    
    if (result.data.code === 200) {
      message.success('表单设计保存成功')
      designerModalVisible.value = false
      fetchData()
    } else {
      message.error(result.data.msg || '保存失败')
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error('保存失败')
  }
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['workflow_form_category', 'workflow_form_type', 'workflow_form_status'])
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-form-container {
  height: 100%;
  display: flex;
  flex-direction: column;
  background-color: #f5f5f5;
}

.preview-container {
  .hbt-form {
    border: 1px solid #d9d9d9;
    border-radius: 4px;
  }
}
</style>
