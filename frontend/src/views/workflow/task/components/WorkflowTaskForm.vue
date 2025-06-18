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
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item label="任务标题" name="taskTitle">
        <a-input v-model:value="formState.taskTitle" placeholder="请输入任务标题" />
      </a-form-item>
      <a-form-item label="任务类型" name="taskType">
        <a-select v-model:value="formState.taskType" placeholder="请选择任务类型">
          <a-select-option v-for="item in taskTypeOptions" :key="item.value" :value="item.value">
            {{ item.label }}
          </a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-radio-group v-model:value="formState.status">
          <a-radio :value="0">正常</a-radio>
          <a-radio :value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="处理人" name="assigneeId">
        <a-select v-model:value="formState.assigneeId" placeholder="请选择处理人">
          <a-select-option v-for="item in assigneeOptions" :key="item.value" :value="item.value">
            {{ item.label }}
          </a-select-option>
        </a-select>
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useDictStore } from '@/stores/dict'
import { getWorkflowTask, createWorkflowTask, updateWorkflowTask } from '@/api/workflow/task'
import type { HbtWorkflowTask, HbtWorkflowTaskCreate, HbtWorkflowTaskUpdate } from '@/types/workflow/task'

const props = defineProps<{
  open: boolean
  title: string
  taskId?: number
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
const dictStore = useDictStore()
const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单状态
const formState = reactive<HbtWorkflowTaskCreate>({
  workflowInstanceId: 0,
  nodeId: 0,
  taskTitle: '',
  taskType: 0,
  status: 0,
  assigneeId: undefined,
  priority: 0
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  taskTitle: [
    { required: true, message: t('workflow.task.fields.taskTitle.required'), trigger: 'blur', type: 'string' }
  ],
  taskType: [
    { required: true, message: t('workflow.task.fields.taskType.required'), trigger: 'change', type: 'number' }
  ],
  status: [
    { required: true, message: t('workflow.task.fields.status.required'), trigger: 'change', type: 'number' }
  ]
}

// 任务类型选项
const taskTypeOptions = computed(() => {
  return dictStore.getDictOptions('workflow_task_type')
})

// 处理人选项
const assigneeOptions = ref<{ label: string; value: number }[]>([])

// 获取任务详情
const getDetail = async (id: number) => {
  try {
    const res = await getWorkflowTask(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.workflowInstanceId = data.workflowInstanceId
      formState.nodeId = data.nodeId
      formState.taskTitle = data.taskTitle
      formState.taskType = data.taskType
      formState.status = data.status
      formState.assigneeId = data.assigneeId
      formState.priority = data.priority
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听任务ID变化
watch(() => props.taskId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    formState.workflowInstanceId = 0
    formState.nodeId = 0
    formState.taskTitle = ''
    formState.taskType = 0
    formState.status = 0
    formState.assigneeId = undefined
    formState.priority = 0
  }
}, { immediate: true })

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    const data: HbtWorkflowTaskCreate = {
      workflowInstanceId: formState.workflowInstanceId,
      nodeId: formState.nodeId,
      taskTitle: formState.taskTitle,
      taskType: formState.taskType,
      status: formState.status,
      assigneeId: formState.assigneeId,
      priority: formState.priority
    }
    let res
    if (props.taskId) {
      const updateData: HbtWorkflowTaskUpdate = {
        ...data,
        id: String(props.taskId)
      }
      res = await updateWorkflowTask(updateData)
    } else {
      res = await createWorkflowTask(data)
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
  emit('update:open', false)
  formRef.value?.resetFields()
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:open', value)
}

// 加载字典数据
onMounted(() => {
  dictStore.loadDicts(['workflow_task_type', 'workflow_task_status'])
})
</script> 