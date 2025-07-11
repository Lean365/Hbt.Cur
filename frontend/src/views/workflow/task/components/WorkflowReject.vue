<template>
  <a-modal
    v-model:open="visible"
    title="拒绝审批"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :confirm-loading="loading"
  >
    <a-form :model="form" layout="vertical">
      <a-form-item label="拒绝原因" name="comment">
        <a-textarea
          v-model:value="form.comment"
          :rows="4"
          placeholder="请输入拒绝原因"
          :maxlength="500"
          show-count
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import { rejectWorkflowTask } from '@/api/workflow/task'

interface Props {
  open: boolean
  taskId?: number
}

interface Emits {
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const visible = ref(false)
const loading = ref(false)
const form = ref({
  comment: ''
})

watch(() => props.open, (newVal) => {
  visible.value = newVal
  if (newVal) {
    form.value.comment = ''
  }
})

watch(visible, (newVal) => {
  emit('update:open', newVal)
})

const handleSubmit = async () => {
  if (!props.taskId) {
    message.error('请选择要审批的任务')
    return
  }

  loading.value = true
  try {
    const res = await rejectWorkflowTask(props.taskId, form.value.comment)
    if (res.data.code === 200) {
      message.success('拒绝成功')
      visible.value = false
      emit('success')
    } else {
      message.error(res.data.msg || '拒绝失败')
    }
  } catch (error) {
    console.error(error)
    message.error('拒绝失败')
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  visible.value = false
  form.value.comment = ''
}
</script> 