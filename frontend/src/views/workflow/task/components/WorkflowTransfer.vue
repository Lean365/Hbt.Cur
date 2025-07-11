<template>
  <a-modal
    v-model:open="visible"
    title="转办任务"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :confirm-loading="loading"
  >
    <a-form :model="form" layout="vertical">
      <a-form-item label="转办人员" name="assigneeId">
        <a-select
          v-model:value="form.assigneeId"
          placeholder="请选择转办人员"
          show-search
          :filter-option="filterUserOption"
        >
          <a-select-option
            v-for="user in userList"
            :key="user.value"
            :value="user.value"
          >
            {{ user.label }}
          </a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="转办说明" name="comment">
        <a-textarea
          v-model:value="form.comment"
          :rows="4"
          placeholder="请输入转办说明"
          :maxlength="500"
          show-count
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { transferWorkflowTask } from '@/api/workflow/task'
import { getUserOptions } from '@/api/identity/user'

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
const userList = ref<{ label: string; value: number }[]>([])
const form = ref({
  assigneeId: undefined,
  comment: ''
})

watch(() => props.open, (newVal) => {
  visible.value = newVal
  if (newVal) {
    form.value.assigneeId = undefined
    form.value.comment = ''
    loadUserList()
  }
})

watch(visible, (newVal) => {
  emit('update:open', newVal)
})

const handleSubmit = async () => {
  if (!props.taskId) {
    message.error('请选择要转办的任务')
    return
  }

  if (!form.value.assigneeId) {
    message.error('请选择转办人员')
    return
  }

  loading.value = true
  try {
    const res = await transferWorkflowTask(props.taskId, form.value.assigneeId, form.value.comment)
    if (res.data.code === 200) {
      message.success('转办成功')
      visible.value = false
      emit('success')
    } else {
      message.error(res.data.msg || '转办失败')
    }
  } catch (error) {
    console.error(error)
    message.error('转办失败')
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  visible.value = false
  form.value.assigneeId = undefined
  form.value.comment = ''
}

const loadUserList = async () => {
  try {
    const res = await getUserOptions()
    if (res.data.code === 200) {
      userList.value = res.data.data
    } else {
      message.error(res.data.msg || '获取用户列表失败')
    }
  } catch (error) {
    console.error('获取用户列表失败:', error)
    message.error('获取用户列表失败')
  }
}

const filterUserOption = (input: string, option: any) => {
  return option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
}

onMounted(() => {
  loadUserList()
})
</script> 