<template>
  <hbt-modal
    :title="t('identity.user.resetPwd')"
    :open="visible"
    :loading="loading"
    @update:open="handleCancel"
    @ok="handleOk"
  >
    <p>{{ t('identity.user.messages.resetPasswordConfirm') }}</p>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { resetPassword } from '@/api/identity/user'
import type { HbtUserResetPwd } from '@/types/identity/user'
import { useConfigStore } from '@/stores/config'

const props = defineProps<{
  visible: boolean
  userId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const loading = ref(false)

const formData = ref({
  userId: props.userId,
  password: useConfigStore().userDefault.password // 从配置中获取默认密码
})

const handleOk = async () => {
  const userId = props.userId
  if (typeof userId !== 'number') {
    message.error(t('common.failed'))
    return
  }

  loading.value = true
  try {
    const data: HbtUserResetPwd = {
      userId: userId,
      password: formData.value.password // 使用从配置中获取的默认密码
    }
    await resetPassword(data)
    message.success(t('identity.user.messages.resetPasswordSuccess'))
    emit('success')
    emit('update:visible', false)
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  emit('update:visible', false)
}
</script> 
