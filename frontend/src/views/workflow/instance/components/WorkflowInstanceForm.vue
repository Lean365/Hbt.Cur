<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="600"
    :loading="loading"
    @update:open="handleVisibleChange"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item
        :label="t('workflow.instance.fields.workflowDefinitionId.label')"
        name="workflowDefinitionId"
      >
        <a-input-number
          v-model:value="formState.workflowDefinitionId"
          :placeholder="t('workflow.instance.fields.workflowDefinitionId.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.instance.fields.workflowTitle.label')"
        name="workflowTitle"
      >
        <a-input
          v-model:value="formState.workflowTitle"
          :placeholder="t('workflow.instance.fields.workflowTitle.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.instance.fields.currentNodeId.label')"
        name="currentNodeId"
      >
        <a-input-number
          v-model:value="formState.currentNodeId"
          :placeholder="t('workflow.instance.fields.currentNodeId.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.instance.fields.initiatorId.label')"
        name="initiatorId"
      >
        <a-input-number
          v-model:value="formState.initiatorId"
          :placeholder="t('workflow.instance.fields.initiatorId.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.instance.fields.formData.label')"
        name="formData"
      >
        <a-textarea
          v-model:value="formState.formData"
          :placeholder="t('workflow.instance.fields.formData.placeholder')"
          :rows="4"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.instance.fields.status.label')"
        name="status"
      >
        <hbt-select
          v-model:value="formState.status"
          dict-type="workflow_instance_status"
          :placeholder="t('workflow.instance.fields.status.placeholder')"
        />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtWorkflowInstance, HbtWorkflowInstanceCreate, HbtWorkflowInstanceUpdate } from '@/types/workflow/workflowInstance'
import { getWorkflowInstance, createWorkflowInstance, updateWorkflowInstance } from '@/api/workflow/workflowInstance'
import { useDictStore } from '@/stores/dict'

const props = defineProps<{
  visible: boolean
  title: string
  instanceId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const dictStore = useDictStore()
const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单状态
const formState = reactive<HbtWorkflowInstanceCreate>({
  workflowDefinitionId: 0,
  workflowTitle: '',
  currentNodeId: 0,
  initiatorId: 0,
  formData: '',
  status: 0,
  startTime: new Date().toISOString()
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  workflowDefinitionId: [
    { required: true, message: t('workflow.instance.fields.workflowDefinitionId.required'), trigger: 'blur', type: 'number' }
  ],
  workflowTitle: [
    { required: true, message: t('workflow.instance.fields.workflowTitle.required'), trigger: 'blur', type: 'string' }
  ],
  currentNodeId: [
    { required: true, message: t('workflow.instance.fields.currentNodeId.required'), trigger: 'blur', type: 'number' }
  ],
  initiatorId: [
    { required: true, message: t('workflow.instance.fields.initiatorId.required'), trigger: 'blur', type: 'number' }
  ],
  formData: [
    { required: true, message: t('workflow.instance.fields.formData.required'), trigger: 'blur', type: 'string' }
  ],
  status: [
    { required: true, message: t('workflow.instance.fields.status.required'), trigger: 'change', type: 'number' }
  ]
}

// 获取实例详情
const getDetail = async (id: number) => {
  try {
    const res = await getWorkflowInstance(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.workflowDefinitionId = data.workflowDefinitionId
      formState.workflowTitle = data.workflowTitle
      formState.currentNodeId = data.currentNodeId
      formState.initiatorId = data.initiatorId
      formState.formData = data.formData
      formState.status = data.status
      formState.startTime = data.startTime
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听ID变化
watch(() => props.instanceId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    formState.workflowDefinitionId = 0
    formState.workflowTitle = ''
    formState.currentNodeId = 0
    formState.initiatorId = 0
    formState.formData = ''
    formState.status = 0
    formState.startTime = new Date().toISOString()
  }
}, { immediate: true })

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    let res
    if (props.instanceId) {
      res = await updateWorkflowInstance({
        ...formState,
        id: String(props.instanceId)
      })
    } else {
      res = await createWorkflowInstance(formState)
    }

    if (res.data.code === 200) {
      message.success(t('common.success'))
      emit('success')
      handleCancel()
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
const handleCancel = () => {
  emit('update:visible', false)
  formRef.value?.resetFields()
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:visible', value)
}
</script> 