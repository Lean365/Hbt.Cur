<template>
  <a-modal
    v-model:open="visible"
    title="撤销任务"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :confirm-loading="loading"
  >
    <a-form :model="form" layout="vertical">
      <a-form-item label="撤销原因" name="comment">
        <a-textarea
          v-model:value="form.comment"
          :rows="4"
          placeholder="请输入撤销原因"
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
import { cancelWorkflowTask } from '@/api/workflow/task'

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
    message.error('请选择要撤销的任务')
    return
  }

  loading.value = true
  try {
    const res = await cancelWorkflowTask(props.taskId, form.value.comment)
    if (res.data.code === 200) {
      message.success('撤销成功')
      visible.value = false
      emit('success')
    } else {
      message.error(res.data.msg || '撤销失败')
    }
  } catch (error) {
    console.error(error)
    message.error('撤销失败')
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  visible.value = false
  form.value.comment = ''
}
</script> 