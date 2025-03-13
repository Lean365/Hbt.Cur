<template>
  <a-modal
    :title="title"
    :open="visible"
    :confirm-loading="loading"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item :label="t('identity.user.userName.label')" name="userName">
        <a-input
          v-model:value="form.userName"
          :placeholder="t('identity.user.userName.placeholder')"
          :disabled="!!userId"
        />
      </a-form-item>
      <a-form-item
        :label="t('identity.user.password.label')"
        name="password"
        v-if="!userId"
      >
        <a-input-password
          v-model:value="form.password"
          :placeholder="t('identity.user.password.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('identity.user.nickName.label')" name="nickName">
        <a-input
          v-model:value="form.nickName"
          :placeholder="t('identity.user.nickName.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('identity.user.phoneNumber.label')" name="phoneNumber">
        <a-input
          v-model:value="form.phoneNumber"
          :placeholder="t('identity.user.phoneNumber.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('identity.user.email.label')" name="email">
        <a-input
          v-model:value="form.email"
          :placeholder="t('identity.user.email.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('identity.user.gender.label')" name="sex">
        <a-select
          v-model:value="form.sex"
          :placeholder="t('identity.user.gender.placeholder')"
        >
          <a-select-option
            v-for="dict in dictStore.getDictDataByType('sys_gender')"
            :key="dict.dictValue"
            :value="dict.dictValue"
          >
            {{ dict.dictLabel }}
          </a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item :label="t('identity.user.userType.label')" name="userType">
        <a-select
          v-model:value="form.userType"
          :placeholder="t('identity.user.userType.placeholder')"
        >
          <a-select-option
            v-for="dict in dictStore.getDictDataByType('sys_user_type')"
            :key="dict.dictValue"
            :value="dict.dictValue"
          >
            {{ dict.dictLabel }}
          </a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item :label="t('identity.user.status.label')" name="status">
        <a-select
          v-model:value="form.status"
          :placeholder="t('common.status.placeholder')"
        >
          <a-select-option
            v-for="dict in dictStore.getDictDataByType('sys_normal_disable')"
            :key="dict.dictValue"
            :value="dict.dictValue"
          >
            {{ dict.dictLabel }}
          </a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item :label="t('common.remark')" name="remark">
        <a-textarea
          v-model:value="form.remark"
          :placeholder="t('common.remark.placeholder')"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { useDictStore } from '@/stores/dict'
import type { UserForm } from '@/types/identity/user'
import { getUser, createUser, updateUser } from '@/api/identity/user'

const props = defineProps<{
  visible: boolean
  title: string
  userId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const dictStore = useDictStore()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<UserForm>({
  userName: '',
  nickName: '',
  password: '',
  phoneNumber: '',
  email: '',
  sex: '0',
  userType: '1',
  status: '0',
  remark: ''
})

// 表单校验规则
const rules = {
  userName: [
    { required: true, message: t('identity.user.userName.validation.required') },
    { min: 2, max: 20, message: t('identity.user.userName.validation.length') }
  ],
  password: [
    { required: true, message: t('identity.user.password.validation.required') },
    { min: 6, max: 20, message: t('identity.user.password.validation.length') }
  ],
  nickName: [
    { required: true, message: t('identity.user.nickName.validation.required') },
    { min: 2, max: 30, message: t('identity.user.nickName.validation.length') }
  ],
  email: [
    { required: true, message: t('identity.user.email.validation.required') },
    { type: 'email', message: t('identity.user.email.validation.invalid') }
  ],
  phoneNumber: [
    { required: true, message: t('identity.user.phoneNumber.validation.required') },
    { pattern: /^1[3-9]\d{9}$/, message: t('identity.user.phoneNumber.validation.invalid') }
  ],
  sex: [
    { required: true, message: t('identity.user.gender.validation.required') }
  ],
  userType: [
    { required: true, message: t('identity.user.userType.validation.required') }
  ],
  status: [
    { required: true, message: t('common.status.validation.required') }
  ]
}

// 加载状态
const loading = ref(false)

// 获取用户信息
const getInfo = async (userId: number) => {
  try {
    const res = await getUser(userId)
    Object.assign(form, res.data)
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    loading.value = true
    try {
      if (props.userId) {
        await updateUser({ ...form, userId: props.userId })
      } else {
        await createUser(form)
      }
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

// 监听用户ID变化
watch(
  () => props.userId,
  (val) => {
    if (val) {
      getInfo(val)
    } else {
      formRef.value?.resetFields()
    }
  }
)
</script> 