<template>
  <a-modal
    v-model:visible="dialogVisible"
    title="发送消息"
    width="600px"
    @cancel="handleClose"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      layout="vertical"
    >
      <a-form-item label="接收用户" name="userId">
        <a-select
          v-model:value="formState.userId"
          show-search
          placeholder="请选择接收用户"
          :filter-option="false"
          :not-found-content="null"
          :options="userOptions"
          @search="handleSearch"
        />
      </a-form-item>
      <a-form-item label="消息内容" name="content">
        <a-textarea
          v-model:value="formState.content"
          placeholder="请输入消息内容"
          :rows="4"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, computed } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { searchUser } from '@/api/identity/user'
import { signalRService } from '@/utils/SignalR/service'
import { useUserStore } from '@/stores/user'

const props = defineProps<{
  visible: boolean
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const dialogVisible = computed({
  get: () => props.visible,
  set: (value) => emit('update:visible', value)
})

const formRef = ref<FormInstance>()
const userOptions = ref<{ label: string; value: number }[]>([])
const loading = ref(false)

const formState = reactive({
  userId: undefined as number | undefined,
  content: ''
})

const rules: Record<string, Rule[]> = {
  userId: [{ required: true, message: '请选择接收用户', trigger: 'change' }],
  content: [
    { required: true, message: '请输入消息内容', trigger: 'blur' },
    { min: 1, max: 500, message: '消息内容长度在1-500个字符之间', trigger: 'blur' }
  ]
}

const handleSearch = async (value: string) => {
  if (!value) {
    userOptions.value = []
    return
  }
  loading.value = true
  try {
    const res = await searchUser({ keyword: value })
    userOptions.value = res.data.rows.map(user => ({
      label: `${user.userName} (${user.nickName})`,
      value: user.userId
    }))
  } catch (error) {
    console.error('搜索用户失败:', error)
    message.error('搜索用户失败')
  } finally {
    loading.value = false
  }
}

const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    
    if (!formState.userId) {
      message.error('请选择接收用户')
      return
    }
    
    console.log('准备发送消息:', {
      userId: formState.userId,
      content: formState.content,
      currentUser: useUserStore().user
    })
    
    await signalRService.sendMessage({
      userId: formState.userId.toString(),  // 确保userId是字符串类型
      content: formState.content
    })
    message.success('消息发送成功')
    handleClose()
    emit('success')
  } catch (error) {
    console.error('发送消息失败:', error)
    message.error('发送消息失败')
  }
}

const handleClose = () => {
  formRef.value?.resetFields()
  userOptions.value = []
  dialogVisible.value = false
}
</script>

<style scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 