<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="600"
    :loading="loading"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item label="工作流名称" name="workflowName">
        <a-input v-model:value="formState.workflowName" placeholder="请输入工作流名称" />
      </a-form-item>
      <a-form-item label="工作流类型" name="workflowCategory">
        <a-input v-model:value="formState.workflowCategory" placeholder="请输入工作流类型" />
      </a-form-item>
      <a-form-item label="版本" name="workflowVersion">
        <a-input v-model:value="formState.workflowVersion" placeholder="请输入版本号" />
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-radio-group v-model:value="formState.status">
          <a-radio :value="0">正常</a-radio>
          <a-radio :value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="4" />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { getWorkflowDefinition, createWorkflowDefinition, updateWorkflowDefinition } from '@/api/workflow/workflowDefinition'
import type { HbtWorkflowDefinition, HbtWorkflowDefinitionCreate, HbtWorkflowDefinitionUpdate } from '@/types/workflow/workflowDefinition'

const props = defineProps<{
  open: boolean
  title: string
  workflowDefinitionId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)

const formState = reactive<HbtWorkflowDefinitionCreate>({
  workflowName: '',
  workflowCategory: '',
  workflowVersion: 1,
  formConfig: '',
  workflowConfig: '',
  status: 0,
  remark: ''
})

const rules: Record<string, any[]> = {
  workflowName: [
    { required: true, message: t('workflow.definition.fields.workflowName.required'), trigger: 'blur' }
  ],
  workflowCategory: [
    { required: true, message: t('workflow.definition.fields.workflowCategory.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('workflow.definition.fields.status.required'), trigger: 'change' }
  ]
}

// 获取工作流定义详情
const getDetail = async (id: number) => {
  try {
    const res = await getWorkflowDefinition(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.workflowName = data.workflowName || ''
      formState.workflowCategory = data.workflowCategory || ''
      formState.workflowVersion = data.workflowVersion
      formState.formConfig = data.formConfig || ''
      formState.workflowConfig = data.workflowConfig || ''
      formState.status = data.status
      formState.remark = data.remark || ''
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听工作流定义ID变化
watch(() => props.workflowDefinitionId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    formState.workflowName = ''
    formState.workflowCategory = ''
    formState.workflowVersion = 1
    formState.formConfig = ''
    formState.workflowConfig = ''
    formState.status = 0
    formState.remark = ''
  }
}, { immediate: true })

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    const data: HbtWorkflowDefinitionCreate = {
      workflowName: formState.workflowName,
      workflowCategory: formState.workflowCategory,
      workflowVersion: formState.workflowVersion,
      formConfig: formState.formConfig,
      workflowConfig: formState.workflowConfig,
      status: formState.status,
      remark: formState.remark
    }
    let res
    if (props.workflowDefinitionId) {
      const updateData: HbtWorkflowDefinitionUpdate = {
        ...data,
        id: props.workflowDefinitionId
      }
      res = await updateWorkflowDefinition(updateData)
    } else {
      res = await createWorkflowDefinition(data)
    }
    if (res.data.code === 200) {
      message.success(t('common.success'))
      emit('success')
      handleVisibleChange(false)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleVisibleChange = (value: boolean) => {
  emit('update:open', value)
  if (!value) {
    formRef.value?.resetFields()
  }
}
</script>
