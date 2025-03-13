<template>
  <div class="user-profile">
    <a-card :bordered="false">
      <a-tabs>
        <a-tab-pane key="basic" :tab="t('identity.user.profile.basic')">
          <a-form
            ref="formRef"
            :model="form"
            :rules="rules"
            :label-col="{ span: 4 }"
            :wrapper-col="{ span: 18 }"
          >
            <a-form-item :label="t('identity.user.profile.avatar')">
              <a-upload
                name="avatar"
                list-type="picture-card"
                class="avatar-uploader"
                :show-upload-list="false"
                :before-upload="beforeUpload"
                @change="handleAvatarChange"
              >
                <img v-if="form.avatar" :src="form.avatar" alt="avatar" />
                <div v-else>
                  <loading-outlined v-if="uploading" />
                  <plus-outlined v-else />
                  <div class="ant-upload-text">{{ t('identity.user.profile.upload') }}</div>
                </div>
              </a-upload>
            </a-form-item>
            <a-form-item :label="t('identity.user.nickName.label')" name="nickName">
              <a-input v-model:value="form.nickName" :placeholder="t('identity.user.nickName.placeholder')" />
            </a-form-item>
            <a-form-item :label="t('identity.user.phoneNumber.label')" name="phoneNumber">
              <a-input v-model:value="form.phoneNumber" :placeholder="t('identity.user.phoneNumber.placeholder')" />
            </a-form-item>
            <a-form-item :label="t('identity.user.email.label')" name="email">
              <a-input v-model:value="form.email" :placeholder="t('identity.user.email.placeholder')" />
            </a-form-item>
            <a-form-item :wrapper-col="{ span: 18, offset: 4 }">
              <a-button type="primary" :loading="submitting" @click="handleSubmit">
                {{ t('common.save') }}
              </a-button>
            </a-form-item>
          </a-form>
        </a-tab-pane>
      </a-tabs>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { useUserStore } from '@/stores/user'
import { PlusOutlined, LoadingOutlined } from '@ant-design/icons-vue'
import { getInfo } from '@/api/identity/auth'
import type { UserInfo } from '@/types/identity/auth'

const { t } = useI18n()
const userStore = useUserStore()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<UserInfo>({
  userId: 0,
  userName: '',
  nickName: '',
  tenantId: 0,
  tenantName: '',
  roles: [],
  permissions: []
})

// 表单校验规则
const rules = {
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
  ]
}

// 上传状态
const uploading = ref(false)
const submitting = ref(false)

// 获取用户信息
const getUserInfo = async () => {
  try {
    const res = await getInfo()
    Object.assign(form, res.data)
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 上传前校验
const beforeUpload = (file: File) => {
  const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png'
  if (!isJpgOrPng) {
    message.error(t('identity.user.profile.avatar.type'))
    return false
  }
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isLt2M) {
    message.error(t('identity.user.profile.avatar.size'))
    return false
  }
  return true
}

// 处理头像变更
const handleAvatarChange = (info: any) => {
  if (info.file.status === 'uploading') {
    uploading.value = true
    return
  }
  if (info.file.status === 'done') {
    uploading.value = false
    form.avatar = info.file.response.url
  }
}

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    submitting.value = true
    try {
      // TODO: 调用更新个人信息API
      message.success(t('common.success'))
      // 更新用户信息
      await userStore.getInfo()
    } catch (error) {
      console.error(error)
      message.error(t('common.failed'))
    }
    submitting.value = false
  })
}

// 组件挂载时获取用户信息
onMounted(() => {
  getUserInfo()
})
</script>

<style lang="less" scoped>
.user-profile {
  padding: 24px;

  .avatar-uploader {
    :deep(.ant-upload) {
      width: 128px;
      height: 128px;
      
      img {
        width: 100%;
        height: 100%;
        object-fit: cover;
      }
    }
  }
}
</style> 