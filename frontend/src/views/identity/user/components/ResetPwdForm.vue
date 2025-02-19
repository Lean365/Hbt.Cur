<template>
  <a-modal
    :title="t('identity.user.resetPwd')"
    :visible="visible"
    :confirm-loading="loading"
    @update:visible="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item :label="t('identity.user.password')" name="password">
        <a-input-password v-model:value="form.password" :placeholder="t('identity.user.password.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.user.confirmPassword')" name="confirmPassword">
        <a-input-password v-model:value="form.confirmPassword" :placeholder="t('identity.user.confirmPassword.placeholder')" />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { resetUserPassword } from '@/api/identity/user'
import type { ResetPwdForm } from '@/types/identity/user'
import type { Rule } from 'ant-design-vue/es/form'

const props = defineProps<{
  visible: boolean
  userId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive({
  password: '',
  confirmPassword: ''
})

// 表单校验规则
const validateConfirmPassword = async (_rule: any, value: string) => {
  if (value === '') {
    return Promise.reject(t('identity.user.confirmPassword.required'))
  }
  if (value !== form.password) {
    return Promise.reject(t('identity.user.confirmPassword.notMatch'))
  }
  return Promise.resolve()
}

const rules: Record<string, Rule[]> = {
  password: [
    { required: true, message: t('identity.user.password.required') },
    { min: 6, message: t('identity.user.password.length') }
  ],
  confirmPassword: [{ validator: validateConfirmPassword, trigger: 'change' }]
}

// 加载状态
const loading = ref(false)

// 提交表单
const handleSubmit = () => {
  if (!props.userId) {
    message.error(t('common.failed'))
    return
  }

  formRef.value?.validate().then(async () => {
    loading.value = true
    try {
      const data: ResetPwdForm = {
        userId: props.userId!,
        password: form.password
      }
      await resetUserPassword(data)
      message.success(t('common.success'))
      emit('success')
      handleVisibleChange(false)
    } catch (error) {
      console.error(error)
      message.error(t('common.failed'))
    }
    loading.value = false
  })
}

// 处理对话框显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    formRef.value?.resetFields()
  }
}
</script> 