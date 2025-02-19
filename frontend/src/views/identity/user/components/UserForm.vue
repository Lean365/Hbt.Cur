<template>
  <a-modal
    :title="title"
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
      <a-form-item :label="t('identity.user.deptName.label')" name="deptId">
        <dept-tree-select v-model:value="form.deptId" />
      </a-form-item>
      <a-form-item :label="t('identity.user.userName.label')" name="userName">
        <a-input v-model:value="form.userName" :placeholder="t('identity.user.userName.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.user.nickName.label')" name="nickName">
        <a-input v-model:value="form.nickName" :placeholder="t('identity.user.nickName.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.user.password.label')" name="password" v-if="!form.userId">
        <a-input-password v-model:value="form.password" :placeholder="t('identity.user.password.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.user.phoneNumber.label')" name="phoneNumber">
        <a-input v-model:value="form.phoneNumber" :placeholder="t('identity.user.phoneNumber.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.user.email.label')" name="email">
        <a-input v-model:value="form.email" :placeholder="t('identity.user.email.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.user.gender.label')" name="sex">
        <a-select v-model:value="form.sex" :placeholder="t('identity.user.gender.placeholder')">
          <a-select-option value="0">{{ t('identity.user.gender.unknown') }}</a-select-option>
          <a-select-option value="1">{{ t('identity.user.gender.male') }}</a-select-option>
          <a-select-option value="2">{{ t('identity.user.gender.female') }}</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item :label="t('identity.user.status.label')" name="status">
        <a-select v-model:value="form.status" :placeholder="t('common.status.placeholder')">
          <a-select-option value="0">{{ t('identity.user.status.normal') }}</a-select-option>
          <a-select-option value="1">{{ t('identity.user.status.disabled') }}</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item :label="t('identity.user.post.label')" name="postIds">
        <a-select
          v-model:value="form.postIds"
          mode="multiple"
          :placeholder="t('identity.user.post.placeholder')"
          :options="postOptions"
          :field-names="{ label: 'postName', value: 'postId' }"
        />
      </a-form-item>
      <a-form-item :label="t('identity.user.role.label')" name="roleIds">
        <a-select
          v-model:value="form.roleIds"
          mode="multiple"
          :placeholder="t('identity.user.role.placeholder')"
          :options="roleOptions"
          :field-names="{ label: 'roleName', value: 'roleId' }"
        />
      </a-form-item>
      <a-form-item :label="t('common.remark')" name="remark">
        <a-textarea v-model:value="form.remark" :placeholder="t('common.remark.placeholder')" />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { UserForm } from '@/types/identity/user'
import type { Role, Post } from '@/types/identity/user'
import { getUser, createUser, updateUser } from '@/api/identity/user'
import { listRole } from '@/api/identity/role'
import { listPost } from '@/api/identity/post'
import DeptTreeSelect from '@/components/DeptTreeSelect/index.vue'

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
  status: '0',
  remark: '',
  postIds: [],
  roleIds: []
})

// 表单校验规则
const rules = {
  deptId: [{ required: true, message: t('identity.user.deptName.validation.required') }],
  userName: [
    { required: true, message: t('identity.user.userName.validation.required') },
    { min: 2, max: 20, message: t('identity.user.userName.validation.length') }
  ],
  nickName: [
    { required: true, message: t('identity.user.nickName.validation.required') },
    { min: 2, max: 30, message: t('identity.user.nickName.validation.length') }
  ],
  password: [
    { required: true, message: t('identity.user.password.validation.required') },
    { min: 6, max: 20, message: t('identity.user.password.validation.length') }
  ],
  email: [
    { required: true, message: t('identity.user.email.validation.required') },
    { validator: (_: any, value: string) => {
      const emailRegex = /^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/
      if (!value || emailRegex.test(value)) {
        return Promise.resolve()
      }
      return Promise.reject(t('identity.user.email.validation.invalid'))
    }}
  ],
  phoneNumber: [
    { required: true, message: t('identity.user.phoneNumber.validation.required') },
    { validator: (_: any, value: string) => {
      const phoneRegex = /^1[3-9]\d{9}$/
      if (!value || phoneRegex.test(value)) {
        return Promise.resolve()
      }
      return Promise.reject(t('identity.user.phoneNumber.validation.invalid'))
    }}
  ]
}

// 加载状态
const loading = ref(false)

// 角色选项
const roleOptions = ref<Role[]>([])

// 岗位选项
const postOptions = ref<Post[]>([])

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

// 获取角色列表
const getRoleOptions = async () => {
  try {
    const res = await listRole()
    roleOptions.value = res.data
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 获取岗位列表
const getPostOptions = async () => {
  try {
    const res = await listPost()
    postOptions.value = res.data
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

// 组件挂载时获取选项数据
onMounted(() => {
  getRoleOptions()
  getPostOptions()
})
</script> 