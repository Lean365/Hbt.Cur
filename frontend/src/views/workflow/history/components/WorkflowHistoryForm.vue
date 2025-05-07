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
        :label="t('workflow.history.fields.workflowInstanceId.label')"
        name="workflowInstanceId"
      >
        <a-input-number
          v-model:value="formState.workflowInstanceId"
          :placeholder="t('workflow.history.fields.workflowInstanceId.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.history.fields.nodeId.label')"
        name="nodeId"
      >
        <a-input-number
          v-model:value="formState.nodeId"
          :placeholder="t('workflow.history.fields.nodeId.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.history.fields.operationType.label')"
        name="operationType"
      >
        <hbt-select
          v-model:value="formState.operationType"
          dict-type="workflow_operation_type"
          :placeholder="t('workflow.history.fields.operationType.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.history.fields.operatorId.label')"
        name="operatorId"
      >
        <a-input-number
          v-model:value="formState.operatorId"
          :placeholder="t('workflow.history.fields.operatorId.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.history.fields.operatorName.label')"
        name="operatorName"
      >
        <a-input
          v-model:value="formState.operatorName"
          :placeholder="t('workflow.history.fields.operatorName.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.history.fields.operationResult.label')"
        name="operationResult"
      >
        <hbt-select
          v-model:value="formState.operationResult"
          dict-type="workflow_operation_result"
          :placeholder="t('workflow.history.fields.operationResult.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('workflow.history.fields.operationComment.label')"
        name="operationComment"
      >
        <a-textarea
          v-model:value="formState.operationComment"
          :placeholder="t('workflow.history.fields.operationComment.placeholder')"
          :rows="4"
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
import type { HbtWorkflowHistory, HbtWorkflowHistoryCreate, HbtWorkflowHistoryUpdate } from '@/types/workflow/workflowHistory'
import { getWorkflowHistory, createWorkflowHistory, updateWorkflowHistory } from '@/api/workflow/workflowHistory'

const props = defineProps<{
  visible: boolean
  title: string
  historyId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单状态
const formState = reactive<HbtWorkflowHistoryCreate>({
  workflowInstanceId: 0,
  nodeId: 0,
  operationType: 0,
  operatorId: 0,
  operatorName: '',
  operationResult: undefined,
  operationComment: undefined,
  operationTime: new Date().toISOString()
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  workflowInstanceId: [
    { required: true, message: t('workflow.history.fields.workflowInstanceId.required'), trigger: 'blur', type: 'number' }
  ],
  nodeId: [
    { required: true, message: t('workflow.history.fields.nodeId.required'), trigger: 'blur', type: 'number' }
  ],
  operationType: [
    { required: true, message: t('workflow.history.fields.operationType.required'), trigger: 'change', type: 'number' }
  ],
  operatorId: [
    { required: true, message: t('workflow.history.fields.operatorId.required'), trigger: 'blur', type: 'number' }
  ],
  operatorName: [
    { required: true, message: t('workflow.history.fields.operatorName.required'), trigger: 'blur', type: 'string' }
  ]
}

// 获取历史记录详情
const getDetail = async (id: number) => {
  try {
    const res = await getWorkflowHistory(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.workflowInstanceId = data.workflowInstanceId
      formState.nodeId = data.nodeId
      formState.operationType = data.operationType
      formState.operatorId = data.operatorId
      formState.operatorName = data.operatorName
      formState.operationResult = data.operationResult
      formState.operationComment = data.operationComment
      formState.operationTime = data.operationTime
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听ID变化
watch(() => props.historyId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    formState.workflowInstanceId = 0
    formState.nodeId = 0
    formState.operationType = 0
    formState.operatorId = 0
    formState.operatorName = ''
    formState.operationResult = undefined
    formState.operationComment = undefined
    formState.operationTime = new Date().toISOString()
  }
}, { immediate: true })

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    let res
    if (props.historyId) {
      res = await updateWorkflowHistory({
        ...formState,
        id: String(props.historyId)
      })
    } else {
      res = await createWorkflowHistory(formState)
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