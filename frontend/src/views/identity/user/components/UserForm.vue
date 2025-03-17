<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-tabs>
      <a-tab-pane :key="'basic'" :tab="t('identity.user.tabs.basic')">
        <a-form
          ref="formRef"
          :model="form"
          :rules="rules"
          :label-col="{ span: 8 }"
          :wrapper-col="{ span: 14 }"
        >
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.tenantId.label')" name="tenantId">
                <a-input-number
                  v-model:value="form.tenantId"
                  :placeholder="t('identity.user.tenantId.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.userName.label')" name="userName">
                <a-input
                  v-model:value="form.userName"
                  :placeholder="t('identity.user.userName.placeholder')"
                  :disabled="!!userId"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16" v-if="!userId">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.password.label')" name="password">
                <a-input-password
                  v-model:value="form.password"
                  :placeholder="t('identity.user.password.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.nickName.label')" name="nickName">
                <a-input
                  v-model:value="form.nickName"
                  :placeholder="t('identity.user.nickName.placeholder')"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.englishName.label')" name="englishName">
                <a-input
                  v-model:value="form.englishName"
                  :placeholder="t('identity.user.englishName.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.userType.label')" name="userType">
                <hbt-select
                  v-model:value="form.userType"
                  dict-type="sys_user_type"
                  type="radio"
                  :placeholder="t('identity.user.userType.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.email.label')" name="email">
                <a-input
                  v-model:value="form.email"
                  :placeholder="t('identity.user.email.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.phoneNumber.label')" name="phoneNumber">
                <a-input
                  v-model:value="form.phoneNumber"
                  :placeholder="t('identity.user.phoneNumber.placeholder')"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.gender.label')" name="gender">
                <hbt-select
                  v-model:value="form.gender"
                  dict-type="sys_gender"
                  type="radio"
                  :placeholder="t('identity.user.gender.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.status.label')" name="status">
                <hbt-select
                  v-model:value="form.status"
                  dict-type="sys_normal_disable"
                  type="radio"
                  :placeholder="t('identity.user.status.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.roles.label')" name="roleIds">
                <hbt-select
                  v-model:value="form.roleIds"
                  :options="roleOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.roles.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.posts.label')" name="postIds">
                <hbt-select
                  v-model:value="form.postIds"
                  :options="postOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.posts.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.depts.label')" name="deptIds">
                <hbt-select
                  v-model:value="form.deptIds"
                  :options="deptOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.depts.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.remark.label')" name="remark">
                <a-textarea
                  v-model:value="form.remark"
                  :placeholder="t('identity.user.remark.placeholder')"
                  :rows="4"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <a-tab-pane :key="'avatar'" :tab="t('identity.user.tabs.avatar')">
        <div class="avatar-container">
          <a-form-item :label="t('identity.user.avatar.label')" name="avatar">
            <hbt-image-upload
              :upload-url="uploadUrl"
              save-path="uploads/avatars"
              :max-size="5"
              :max-count="1"
              name-strategy="custom"
              :name-template="'{filename}{ext}'"
              :file-name="form.userName"
              :compress="{
                quality: 0.8,
                maxWidth: 200,
                maxHeight: 200
              }"
              :crop="{
                aspect: 1,
                width: 200,
                height: 200
              }"
              @success="handleUploadSuccess"
              @error="handleUploadError"
            />
          </a-form-item>
        </div>
      </a-tab-pane>
    </a-tabs>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import { PlusOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import type { UserForm } from '@/types/identity/user'
import { getUser, createUser, updateUser } from '@/api/identity/user'
import { getRoleOptions } from '@/api/identity/role'
import { getPostOptions } from '@/api/identity/post'
import { getDeptOptions } from '@/api/identity/dept'
import HbtModal from '@/components/Business/Modal/index.vue'
import HbtImageUpload from '@/components/Upload/imageUpload.vue'

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
  userId: undefined,
  tenantId: 0,
  userName: '',
  nickName: '',
  englishName: '',
  userType: 0,
  password: '',
  email: '',
  phoneNumber: '',
  gender: 0,
  avatar: '',
  status: 0,
  remark: '',
  roleIds: [],
  postIds: [],
  deptIds: []
})

// 角色选项
const roleOptions = ref<{ label: string; value: number }[]>([])
// 岗位选项
const postOptions = ref<{ label: string; value: number }[]>([])
// 部门选项
const deptOptions = ref<{ label: string; value: number }[]>([])

// 表单校验规则
const rules: Record<string, Rule[]> = {
  tenantId: [
    { required: true, message: t('identity.user.tenantId.validation.required') }
  ],
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
  englishName: [
    { min: 2, max: 30, message: t('identity.user.englishName.validation.length') }
  ],
  userType: [
    { required: true, message: t('identity.user.userType.validation.required') }
  ],
  email: [
    { required: true, message: t('identity.user.email.validation.required') },
    { pattern: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/, message: t('identity.user.email.validation.invalid') }
  ],
  phoneNumber: [
    { required: true, message: t('identity.user.phoneNumber.validation.required') },
    { pattern: /^1[3-9]\d{9}$/, message: t('identity.user.phoneNumber.validation.invalid') }
  ],
  gender: [
    { required: true, message: t('identity.user.gender.validation.required') }
  ],
  status: [
    { required: true, message: t('identity.user.status.validation.required') }
  ],
  roleIds: [
    { required: true, type: 'array', message: t('identity.user.roles.validation.required') }
  ]
}

// 加载状态
const loading = ref(false)

// 获取用户信息
const getInfo = async (userId: number) => {
  try {
    const res = await getUser(userId)
    if (res.code === 200) {
      Object.assign(form, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 获取选项数据
const fetchOptions = async () => {
  try {
    const [roles, posts, depts] = await Promise.all([
      getRoleOptions(),
      getPostOptions(),
      getDeptOptions()
    ])
    roleOptions.value = roles.data
    postOptions.value = posts.data
    deptOptions.value = depts.data
  } catch (error) {
    console.error('获取选项数据失败:', error)
  }
}

// 上传相关
const uploadUrl = `${import.meta.env.VITE_API_BASE_URL}/api/system/file/upload` // 使用环境变量

const handleUploadSuccess = (result: any) => {
  form.avatar = result.url
  message.success(t('identity.user.avatar.uploadSuccess'))
}

const handleUploadError = (error: any) => {
  message.error(t('identity.user.avatar.uploadError'))
}

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    loading.value = true
    try {
      let res
      if (props.userId) {
        res = await updateUser({ ...form, userId: props.userId })
      } else {
        res = await createUser(form)
      }
      if (res.code === 200) {
        message.success(t('common.success'))
        emit('success')
        handleVisibleChange(false)
      } else {
        message.error(res.msg || t('common.failed'))
      }
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

// 组件挂载时加载选项数据
onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable', 'sys_gender', 'sys_user_type'])
  // 加载选项数据
  fetchOptions()
})
</script>

<style lang="less" scoped>
.avatar-container {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 24px;
}

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
</style> 