<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="800"
    :loading="loading"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="数据库名称" name="databaseName">
        <a-input v-model:value="formData.databaseName" placeholder="请输入数据库名称" />
      </a-form-item>
      <a-form-item label="表名" name="tableName">
        <a-input v-model:value="formData.tableName" placeholder="请输入表名" />
      </a-form-item>
      <a-form-item label="表注释" name="tableComment">
        <a-input v-model:value="formData.tableComment" placeholder="请输入表注释" />
      </a-form-item>
      <a-form-item label="实体类名" name="className">
        <a-input v-model:value="formData.className" placeholder="请输入实体类名" />
      </a-form-item>
      <a-form-item label="命名空间" name="namespace">
        <a-input v-model:value="formData.namespace" placeholder="请输入命名空间" />
      </a-form-item>
      <a-form-item label="基本命名空间" name="baseNamespace">
        <a-input v-model:value="formData.baseNamespace" placeholder="请输入基本命名空间" />
      </a-form-item>
      <a-form-item label="C#类名" name="csharpTypeName">
        <a-input v-model:value="formData.csharpTypeName" placeholder="请输入C#类名" />
      </a-form-item>
      <a-form-item label="生成类型" name="genMode">
        <a-radio-group v-model:value="formData.genMode">
          <a-radio :value="1">单表</a-radio>
          <a-radio :value="2">主从表</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="主表名称" name="parentTableName" v-if="formData.genMode === 2">
        <a-input v-model:value="formData.parentTableName" placeholder="请输入主表名称" />
      </a-form-item>
      <a-form-item label="主表外键" name="parentTableFkName" v-if="formData.genMode === 2">
        <a-input v-model:value="formData.parentTableFkName" placeholder="请输入主表外键" />
      </a-form-item>
      <a-form-item label="生成模板" name="templateType">
        <a-radio-group v-model:value="formData.templateType">
          <a-radio :value="1">基础模板</a-radio>
          <a-radio :value="2">自定义模板</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="模块名" name="moduleName">
        <a-input v-model:value="formData.moduleName" placeholder="请输入模块名" />
      </a-form-item>
      <a-form-item label="业务名" name="businessName">
        <a-input v-model:value="formData.businessName" placeholder="请输入业务名" />
      </a-form-item>
      <a-form-item label="功能名" name="functionName">
        <a-input v-model:value="formData.functionName" placeholder="请输入功能名" />
      </a-form-item>
      <a-form-item label="作者" name="author">
        <a-input v-model:value="formData.author" placeholder="请输入作者" />
      </a-form-item>
      <a-form-item label="生成路径" name="genPath">
        <a-input v-model:value="formData.genPath" placeholder="请输入生成路径" />
      </a-form-item>
      <a-form-item label="其他选项" name="options">
        <a-textarea v-model:value="formData.options" placeholder="请输入其他选项" :rows="4" />
      </a-form-item>
      <a-form-item label="生成选项">
        <div>
          <a-checkbox v-model:checked="options.enableQuery">查询</a-checkbox>
          <a-checkbox v-model:checked="options.enableAdd">新增</a-checkbox>
          <a-checkbox v-model:checked="options.enableEdit">修改</a-checkbox>
          <a-checkbox v-model:checked="options.enableDelete">删除</a-checkbox>
          <a-checkbox v-model:checked="options.enableImport">导入</a-checkbox>
          <a-checkbox v-model:checked="options.enableExport">导出</a-checkbox>
          <a-checkbox v-model:checked="options.enableBatchDelete">批量删除</a-checkbox>
          <a-checkbox v-model:checked="options.enableBatchExport">批量导出</a-checkbox>
          <a-checkbox v-model:checked="options.enableTree">树形结构</a-checkbox>
        </div>
      </a-form-item>
      <a-form-item label="树形父字段" name="treeParentField" v-if="options.enableTree">
        <a-input v-model:value="options.treeParentField" placeholder="请输入树形父字段" />
      </a-form-item>
      <a-form-item label="树形子字段" name="treeChildField" v-if="options.enableTree">
        <a-input v-model:value="options.treeChildField" placeholder="请输入树形子字段" />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted, computed } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtGenTable, HbtGenTableCreate, HbtGenTableUpdate } from '@/types/generator/genTable'
import { getTable, createTable, updateTable } from '@/api/generator/genTable'

const props = defineProps<{
  open: boolean
  title: string
  tableId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单数据
const formData = reactive<HbtGenTableCreate>({
  databaseName: '',
  tableName: '',
  tableComment: '',
  className: '',
  namespace: '',
  baseNamespace: '',
  csharpTypeName: '',
  parentTableName: '',
  parentTableFkName: '',
  templateType: 1,
  moduleName: '',
  businessName: '',
  functionName: '',
  author: '',
  genMode: 1,
  genPath: '',
  options: JSON.stringify({
    enableQuery: true,
    enableAdd: true,
    enableEdit: true,
    enableDelete: true,
    enableImport: false,
    enableExport: true,
    enableBatchDelete: true,
    enableBatchExport: false,
    enableTree: false,
    treeParentField: '',
    treeChildField: ''
  }),
  status: 1,
  tenantId: 1
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  tableName: [{ required: true, message: '请输入表名', trigger: 'blur' }],
  tableComment: [{ required: true, message: '请输入表注释', trigger: 'blur' }],
  className: [{ required: true, message: '请输入实体类名', trigger: 'blur' }],
  moduleName: [{ required: true, message: '请输入模块名', trigger: 'blur' }],
  namespace: [{ required: true, message: '请输入包名', trigger: 'blur' }],
  businessName: [{ required: true, message: '请输入业务名', trigger: 'blur' }],
  functionName: [{ required: true, message: '请输入功能名', trigger: 'blur' }],
  author: [{ required: true, message: '请输入作者', trigger: 'blur' }],
  genMode: [{ required: true, message: '请选择生成类型', trigger: 'change' }],
  templateType: [{ required: true, message: '请选择生成模板', trigger: 'change' }],
}

// 在 script 部分添加
const options = reactive({
  enableQuery: true,
  enableAdd: true,
  enableEdit: true,
  enableDelete: true,
  enableImport: false,
  enableExport: true,
  enableBatchDelete: true,
  enableBatchExport: false,
  enableTree: false,
  treeParentField: '',
  treeChildField: ''
})

// 监听 options 变化
watch(options, (newVal) => {
  formData.options = JSON.stringify(newVal)
}, { deep: true })

// 监听visible变化
watch(
  () => props.open,
  (val) => {
    if (val) {
      if (props.tableId) {
        getDetail()
      } else {
        resetForm()
      }
    }
  }
)

/** 获取详情 */
const getDetail = async () => {
  if (!props.tableId) return
  loading.value = true
  try {
    const res = await getTable(props.tableId)
    if (res.data?.data) {
      Object.assign(formData, res.data.data)
      if (res.data.data.options) {
        Object.assign(options, JSON.parse(res.data.data.options))
      }
    }
  } finally {
    loading.value = false
  }
}

/** 重置表单 */
const resetForm = () => {
  formRef.value?.resetFields()
  Object.assign(formData, {
    databaseName: '',
    tableName: '',
    tableComment: '',
    className: '',
    namespace: '',
    baseNamespace: '',
    csharpTypeName: '',
    parentTableName: '',
    parentTableFkName: '',
    templateType: 1,
    moduleName: '',
    businessName: '',
    functionName: '',
    author: '',
    genMode: 1,
    genPath: '',
    options: JSON.stringify({
      enableQuery: true,
      enableAdd: true,
      enableEdit: true,
      enableDelete: true,
      enableImport: false,
      enableExport: true,
      enableBatchDelete: true,
      enableBatchExport: false,
      enableTree: false,
      treeParentField: '',
      treeChildField: ''
    }),
    status: 1,
    tenantId: 1
  })
}

/** 提交表单 */
const handleSubmit = async () => {
  if (!formRef.value) return
  await formRef.value.validate()
  loading.value = true
  try {
    if (props.tableId) {
      await updateTable({
        ...formData,
        tableId: props.tableId
      })
    } else {
      await createTable(formData)
    }
    emit('success')
  } finally {
    loading.value = false
  }
}

/** 取消 */
const handleCancel = () => {
  emit('update:open', false)
  resetForm()
}
</script> 