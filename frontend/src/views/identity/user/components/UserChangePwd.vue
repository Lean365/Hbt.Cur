<template>
  <div class="user-change-password">
    <a-card :bordered="false">
      <a-form
        ref="formRef"
        :model="form"
        :rules="rules"
        :label-col="{ span: 4 }"
        :wrapper-col="{ span: 18 }"
      >
        <a-form-item :label="t('identity.user.password.old')" name="oldPassword">
          <a-input-password
            v-model:value="form.oldPassword"
            :placeholder="t('identity.user.password.old.placeholder')"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.password.new')" name="newPassword">
          <a-input-password
            v-model:value="form.newPassword"
            :placeholder="t('identity.user.password.new.placeholder')"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.password.confirm')" name="confirmPassword">
          <a-input-password
            v-model:value="form.confirmPassword"
            :placeholder="t('identity.user.password.confirm.placeholder')"
          />
        </a-form-item>
        <a-form-item :wrapper-col="{ span: 18, offset: 4 }">
          <a-button type="primary" :loading="submitting" @click="handleSubmit">
            {{ t('common.save') }}
          </a-button>
        </a-form-item>
      </a-form>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { useUserStore } from '@/stores/user'
import { changePassword } from '@/api/identity/auth'

const { t } = useI18n()
const userStore = useUserStore()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive({
  oldPassword: '',
  newPassword: '',
  confirmPassword: ''
})

// 表单校验规则
const validateConfirmPassword = async (_rule: any, value: string) => {
  if (value === '') {
    return Promise.reject(t('identity.user.password.confirm.validation.required'))
  }
  if (value !== form.newPassword) {
    return Promise.reject(t('identity.user.password.confirm.validation.notMatch'))
  }
  return Promise.resolve()
}

const rules = {
  oldPassword: [
    { required: true, message: t('identity.user.password.old.validation.required') }
  ],
  newPassword: [
    { required: true, message: t('identity.user.password.new.validation.required') },
    { min: 6, max: 20, message: t('identity.user.password.new.validation.length') }
  ],
  confirmPassword: [{ validator: validateConfirmPassword, trigger: 'change' }]
}

// 提交状态
const submitting = ref(false)

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    submitting.value = true
    try {
      await changePassword({
        userId: userStore.user?.userId!,
        oldPassword: form.oldPassword,
        newPassword: form.newPassword
      })
      message.success(t('identity.user.password.success'))
      formRef.value?.resetFields()
    } catch (error) {
      console.error(error)
      message.error(t('identity.user.password.failed'))
    }
    submitting.value = false
  })
}
</script>

<style lang="less" scoped>
.user-change-password {
  padding: 24px;
}
</style> 