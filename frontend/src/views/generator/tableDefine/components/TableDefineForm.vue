<template>
  <a-modal
    :visible="visible"
    :title="title"
    :width="800"
    :confirm-loading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="表名" name="tableName">
        <a-input v-model:value="formData.tableName" placeholder="请输入表名" />
      </a-form-item>
      <a-form-item label="表注释" name="tableComment">
        <a-input v-model:value="formData.tableComment" placeholder="请输入表注释" />
      </a-form-item>
      <a-form-item label="实体类名" name="className">
        <a-input v-model:value="formData.className" placeholder="请输入实体类名" />
      </a-form-item>
      <a-form-item label="模块名" name="moduleName">
        <a-input v-model:value="formData.moduleName" placeholder="请输入模块名" />
      </a-form-item>
      <a-form-item label="包名" name="packageName">
        <a-input v-model:value="formData.packageName" placeholder="请输入包名" />
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
      <a-form-item label="生成类型" name="genType">
        <a-radio-group v-model:value="formData.genType">
          <a-radio :value="1">单表</a-radio>
          <a-radio :value="2">主从表</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="主表ID" name="parentTableId" v-if="formData.genType === 2">
        <a-input-number v-model:value="formData.parentTableId" placeholder="请输入主表ID" />
      </a-form-item>
      <a-form-item label="主表外键" name="parentTableFk" v-if="formData.genType === 2">
        <a-input v-model:value="formData.parentTableFk" placeholder="请输入主表外键" />
      </a-form-item>
      <a-form-item label="生成模板" name="templateType">
        <a-radio-group v-model:value="formData.templateType">
          <a-radio :value="1">基础模板</a-radio>
          <a-radio :value="2">自定义模板</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="自定义模板" name="customTemplateId" v-if="formData.templateType === 2">
        <a-input-number v-model:value="formData.customTemplateId" placeholder="请输入自定义模板ID" />
      </a-form-item>
      <a-form-item label="生成选项">
        <div>
          <a-checkbox v-model:checked="formData.enableQuery">查询</a-checkbox>
          <a-checkbox v-model:checked="formData.enableAdd">新增</a-checkbox>
          <a-checkbox v-model:checked="formData.enableEdit">修改</a-checkbox>
          <a-checkbox v-model:checked="formData.enableDelete">删除</a-checkbox>
          <a-checkbox v-model:checked="formData.enableImport">导入</a-checkbox>
          <a-checkbox v-model:checked="formData.enableExport">导出</a-checkbox>
          <a-checkbox v-model:checked="formData.enableBatchDelete">批量删除</a-checkbox>
          <a-checkbox v-model:checked="formData.enableBatchExport">批量导出</a-checkbox>
          <a-checkbox v-model:checked="formData.enableTree">树形结构</a-checkbox>
        </div>
      </a-form-item>
      <a-form-item label="树形父字段" name="treeParentField" v-if="formData.enableTree">
        <a-input v-model:value="formData.treeParentField" placeholder="请输入树形父字段" />
      </a-form-item>
      <a-form-item label="树形子字段" name="treeChildField" v-if="formData.enableTree">
        <a-input v-model:value="formData.treeChildField" placeholder="请输入树形子字段" />
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-radio-group v-model:value="formData.status">
          <a-radio :value="1">正常</a-radio>
          <a-radio :value="0">停用</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formData.remark" placeholder="请输入备注" :rows="4" />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtGenTableDefineDto, HbtGenTableDefineCreate, HbtGenTableDefineUpdate } from '@/types/generator/tableDefine'
import { getTableDefine, createTableDefine, updateTableDefine } from '@/api/generator/tableDefine'

const props = defineProps<{
  visible: boolean
  title: string
  tableId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单数据
const formData = reactive<HbtGenTableDefineCreate>({
  tableName: '',
  tableComment: '',
  className: '',
  moduleName: '',
  packageName: '',
  businessName: '',
  functionName: '',
  author: '',
  genType: 1,
  templateType: 1,
  enableQuery: true,
  enableAdd: true,
  enableEdit: true,
  enableDelete: true,
  enableImport: false,
  enableExport: true,
  enableBatchDelete: true,
  enableBatchExport: false,
  enableTree: false,
  status: 1
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  tableName: [{ required: true, message: '请输入表名', trigger: 'blur' }],
  tableComment: [{ required: true, message: '请输入表注释', trigger: 'blur' }],
  className: [{ required: true, message: '请输入实体类名', trigger: 'blur' }],
  moduleName: [{ required: true, message: '请输入模块名', trigger: 'blur' }],
  packageName: [{ required: true, message: '请输入包名', trigger: 'blur' }],
  businessName: [{ required: true, message: '请输入业务名', trigger: 'blur' }],
  functionName: [{ required: true, message: '请输入功能名', trigger: 'blur' }],
  author: [{ required: true, message: '请输入作者', trigger: 'blur' }],
  genType: [{ required: true, message: '请选择生成类型', trigger: 'change' }],
  templateType: [{ required: true, message: '请选择生成模板', trigger: 'change' }],
  status: [{ required: true, message: '请选择状态', trigger: 'change' }]
}

// 监听visible变化
watch(
  () => props.visible,
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
    const res = await getTableDefine(props.tableId)
    if (res.data) {
      Object.assign(formData, res.data)
    }
  } finally {
    loading.value = false
  }
}

/** 重置表单 */
const resetForm = () => {
  formRef.value?.resetFields()
  Object.assign(formData, {
    tableName: '',
    tableComment: '',
    className: '',
    moduleName: '',
    packageName: '',
    businessName: '',
    functionName: '',
    author: '',
    genType: 1,
    templateType: 1,
    enableQuery: true,
    enableAdd: true,
    enableEdit: true,
    enableDelete: true,
    enableImport: false,
    enableExport: true,
    enableBatchDelete: true,
    enableBatchExport: false,
    enableTree: false,
    status: 1
  })
}

/** 提交表单 */
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    if (props.tableId) {
      await updateTableDefine({
        ...formData,
        id: props.tableId
      })
    } else {
      await createTableDefine(formData)
    }
    emit('success')
    handleCancel()
  } catch (error) {
    console.error('表单提交失败:', error)
  } finally {
    loading.value = false
  }
}

/** 取消 */
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}
</script> 