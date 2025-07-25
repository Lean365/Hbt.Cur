<template>
  <hbt-form-dialog
    v-model:open="dialogVisible"
    :title="dialogTitle"
    :loading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      layout="vertical"
    >
      <a-form-item label="表单名称" name="formName">
        <a-input v-model:value="formData.formName" placeholder="请输入表单名称" />
      </a-form-item>
      <a-form-item label="表单键" name="formKey">
        <a-input v-model:value="formData.formKey" placeholder="请输入表单键" />
      </a-form-item>
      <a-form-item label="表单分类" name="formCategory">
        <hbt-select
          v-model:value="formData.formCategory"
          dict-type="workflow_form_category"
          placeholder="请选择表单分类"
        />
      </a-form-item>
      <a-form-item label="表单类型" name="formType">
        <hbt-select
          v-model:value="formData.formType"
          dict-type="workflow_form_type"
          placeholder="请选择表单类型"
        />
      </a-form-item>
      <a-form-item label="表单描述" name="description">
        <a-textarea v-model:value="formData.description" :rows="3" placeholder="请输入表单描述" />
      </a-form-item>
    </a-form>
  </hbt-form-dialog>
</template>

<script setup lang="ts">
import { ref, reactive, computed, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { getFormById, createForm, updateForm } from '@/api/workflow/form'
import type { HbtFormCreate, HbtFormUpdate } from '@/types/workflow/form'

// Props
interface Props {
  open: boolean
  formId?: number
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  formId: undefined
})

// Emits
const emit = defineEmits<{
  'update:open': [value: boolean]
  'success': []
}>()

// 响应式数据
const loading = ref(false)
const formRef = ref<FormInstance>()

// 表单数据
const formData = reactive<HbtFormCreate>({
  formKey: '',
  formName: '',
  formCategory: 1,
  formType: 1,
  version: '1.0',
  formConfig: '',
  description: ''
})

// 表单验证规则
const formRules: Record<string, any> = {
  formName: [
    { required: true, message: '请输入表单名称', trigger: 'blur' }
  ],
  formKey: [
    { required: true, message: '请输入表单键', trigger: 'blur' }
  ],
  formCategory: [
    { required: true, message: '请选择表单分类', trigger: 'change' }
  ],
  formType: [
    { required: true, message: '请选择表单类型', trigger: 'change' }
  ]
}

// 计算属性
const dialogVisible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const dialogTitle = computed(() => props.formId ? '编辑表单' : '新增表单')
const isEdit = computed(() => !!props.formId)

// 方法
const loadFormData = async () => {
  if (!props.formId) return
  
  loading.value = true
  try {
    const result = await getFormById(props.formId)
    if (result.data.code === 200) {
      const form = result.data.data
      Object.assign(formData, {
        formKey: form.formKey,
        formName: form.formName,
        formCategory: form.formCategory,
        formType: form.formType,
        version: form.version,
        formConfig: form.formConfig,
        description: form.description
      })
    }
  } catch (error) {
    console.error('加载表单数据失败:', error)
    message.error('加载表单数据失败')
  } finally {
    loading.value = false
  }
}

const resetForm = () => {
  Object.assign(formData, {
    formKey: '',
    formName: '',
    formCategory: 1,
    formType: 1,
    version: '1.0',
    formConfig: '',
    description: ''
  })
  formRef.value?.clearValidate()
}

const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    
    loading.value = true
    const result = isEdit.value 
      ? await updateForm({ ...formData, formId: props.formId! } as HbtFormUpdate)
      : await createForm(formData)
    
    if (result.data.code === 200) {
      message.success(isEdit.value ? '更新成功' : '创建成功')
      emit('success')
    } else {
      message.error(result.data.msg || (isEdit.value ? '更新失败' : '创建失败'))
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error('保存失败')
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  dialogVisible.value = false
  resetForm()
}

// 监听器
watch(() => props.open, (newVal) => {
  if (newVal) {
    if (props.formId) {
      loadFormData()
    } else {
      resetForm()
    }
  }
})
</script>

<style lang="less" scoped>
</style>
  