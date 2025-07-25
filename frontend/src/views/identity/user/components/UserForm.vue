<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-tabs>
      <a-tab-pane :key="'basic'" :tab="t('common.tab.basic')">
        <a-form
          ref="formRef"
          :model="form"
          :rules="rules"
          :label-col="{ span: 8 }"
          :wrapper-col="{ span: 14 }"
        >
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.userName.label')" name="userName">
                <a-input
                  v-model:value="form.userName"
                  :placeholder="t('identity.user.fields.userName.placeholder')"
                  :disabled="!!props.userId"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16" v-if="!props.userId">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.password.label')" name="password">
                <a-input-password
                  v-model:value="form.password"
                  :placeholder="t('identity.user.fields.password.placeholder')"
                  :disabled="true"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.nickName.label')" name="nickName">
                <a-input
                  v-model:value="form.nickName"
                  :placeholder="t('identity.user.fields.nickName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.realName.label')" name="realName">
                <a-input
                  v-model:value="form.realName"
                  :placeholder="t('identity.user.fields.realName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.fullName.label')" name="fullName">
                <a-input
                  v-model:value="form.fullName"
                  :placeholder="t('identity.user.fields.fullName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.englishName.label')" name="englishName">
                <a-input
                  v-model:value="form.englishName"
                  :placeholder="t('identity.user.fields.englishName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.userType.label')" name="userType">
                <hbt-select
                  v-model:value="form.userType"
                  dict-type="sys_user_type"
                  
                  :placeholder="t('identity.user.fields.userType.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.email.label')" name="email">
                <a-input
                  v-model:value="form.email"
                  :placeholder="t('identity.user.fields.email.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.phoneNumber.label')" name="phoneNumber">
                <a-input
                  v-model:value="form.phoneNumber"
                  :placeholder="t('identity.user.fields.phoneNumber.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.gender.label')" name="gender">
                <hbt-select
                  v-model:value="form.gender"
                  dict-type="sys_gender"
                  type="radio"
                  :placeholder="t('identity.user.fields.gender.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.status.label')" name="status">
                <hbt-select
                  v-model:value="form.status"
                  dict-type="sys_normal_disable"
                  type="radio"
                  :placeholder="t('identity.user.fields.status.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.deptName.label')" name="deptIds">
                <hbt-select
                  v-model:value="form.deptIds"
                  :options="deptOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.fields.deptName.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.role.label')" name="roleIds">
                <hbt-select
                  v-model:value="form.roleIds"
                  :options="roleOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.fields.role.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.post.label')" name="postIds">
                <hbt-select
                  v-model:value="form.postIds"
                  :options="postOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.fields.post.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.remark.label')" name="remark">
                <a-textarea
                  v-model:value="form.remark"
                  :placeholder="t('identity.user.fields.remark.placeholder')"
                  :rows="4"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <a-tab-pane :key="'avatar'" :tab="t('common.tab.avatar')">
        <div class="avatar-container">
          <a-form-item :label="t('identity.user.fields.avatar.label')" name="avatar">
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
import { ref, reactive, watch, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import { PlusOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import type { HbtUser, HbtUserForm, HbtUserCreate, HbtUserUpdate } from '@/types/identity/user'
import { getUser, createUser, updateUser } from '@/api/identity/user'
import { getRoleOptions } from '@/api/identity/role'
import { getPostOptions } from '@/api/identity/post'
import { getDeptOptions } from '@/api/identity/dept'
import { getSalt } from '@/api/identity/auth/auth'

import { PasswordEncryptor } from '@/utils/crypto'
import { RegexUtils } from '@/utils/regex'
import { id } from 'date-fns/locale'


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
const form = reactive<HbtUserForm>({  
  userName: '',
  nickName: '',
  realName: '',
  fullName: '',
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

// 标记是否已经初始化过
const isInitialized = ref(false)

// 角色选项
const roleOptions = ref<{ label: string; value: number }[]>([])
// 岗位选项
const postOptions = ref<{ label: string; value: number }[]>([])
// 部门选项
const deptOptions = ref<{ label: string; value: number }[]>([])

// 表单校验规则
const rules: Record<string, Rule[]> = {
  userName: [
    { required: true, message: t('identity.user.fields.userName.validation.required') },
    {
      validator: (_, value) => {
        if (value &&!RegexUtils.isValidUsername(value)) {
          return Promise.reject(t('identity.user.fields.userName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  nickName: [
    { required: true, message: t('identity.user.fields.nickName.validation.required') },
    {
      validator: (_, value) => {
        if (value &&!RegexUtils.isValidNickname(value)) {
          return Promise.reject(t('identity.user.fields.nickName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  englishName: [
    { required: true, message: t('identity.user.fields.englishName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidEnglishName(value)) {
          return Promise.reject(t('identity.user.fields.englishName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  fullName: [
    { required: true, message: t('identity.user.fields.fullName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidFullName(value)) {
          return Promise.reject(t('identity.user.fields.fullName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  realName: [
    { required: true, message: t('identity.user.fields.realName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidRealName(value)) {
          return Promise.reject(t('identity.user.fields.realName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  userType: [
    { required: true, message: t('identity.user.fields.userType.validation.required') }
  ],
  email: [
    { required: true, message: t('identity.user.fields.email.validation.required') },
    { 
      validator: (_, value) => {
        if (value && !RegexUtils.isValidEmail(value)) {
          return Promise.reject(t('identity.user.fields.email.validation.invalid'))
        }
        return Promise.resolve()
      }
    }
  ],
  phoneNumber: [
    { required: true, message: t('identity.user.fields.phoneNumber.validation.required') },
    {
      validator: (_, value) => {
        console.log('phone校验', value, RegexUtils.PHONE.test(value))
        if (value && !RegexUtils.PHONE.test(value)) {
          return Promise.reject(t('identity.user.fields.phoneNumber.validation.invalid'))
        }
        return Promise.resolve()
      }
    }
  ],
  gender: [
    { required: true, message: t('identity.user.fields.gender.validation.required') }
  ],
  status: [
    { required: true, message: t('identity.user.fields.status.validation.required') }
  ],
  roleIds: [
    { required: true, type: 'array', message: t('identity.user.fields.role.validation.required') }
  ],
  postIds: [
    { required: true, type: 'array', message: t('identity.user.fields.post.validation.required') }
  ],
  deptIds: [
    { required: true, type: 'array', message: t('identity.user.fields.deptName.validation.required') }
  ]
}

// 加载状态
const loading = ref(false)

// 获取用户信息
const getInfo = async (userId: number) => {
  console.log('userId:', userId)
  try {
    const res = await getUser(userId)
    if (res.data.code === 200) {
      Object.assign(form, res.data.data)
    } else {
      message.error(res.data.msg || t('common.failed'))
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
    roleOptions.value = roles.data.data
    postOptions.value = posts.data.data
    deptOptions.value = depts.data.data
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
    try {
      let res
      if (props.userId) {
        // 更新用户时，只发送必要的字段
        const updateData: HbtUserUpdate = {
          userId: props.userId,
          userName: form.userName,
          nickName: form.nickName,
          userType: form.userType,
          password: '', // 更新时不修改密码
          realName: form.realName,
          fullName: form.fullName,
          englishName: form.englishName,
          email: form.email,
          phoneNumber: form.phoneNumber,
          gender: form.gender,
          avatar: form.avatar,
          status: form.status,
          deptId: form.deptIds.length > 0 ? form.deptIds[0] : 0, // 使用第一个部门ID
          roleIds: form.roleIds.map(id => Number(id)),
          postIds: form.postIds.map(id => Number(id)),
          deptIds: form.deptIds.map(id => Number(id)),
          remark: form.remark
        }
        console.log('更新用户数据:', updateData)
        try {
          // 设置 loading 状态
          loading.value = true
          
          res = await updateUser(updateData)
          if (res.data.code === 200) {
            message.success(t('common.success'))
            emit('success')
            handleVisibleChange(false)
          } else {
            // 处理特定的错误码
            switch (res.data.code) {
              case 401:
                // Token过期，不需要处理，request.ts会处理重定向
                break
              case 403:
                message.error(t('common.error.forbidden'))
                break
              case 500:
                message.error(t('common.error.serverError'))
                break
              default:
                message.error(res.data.msg || t('common.failed'))
            }
          }
        } catch (error: any) {
          console.error('更新用户失败:', error)
          // 如果是取消的请求（重定向中），不显示错误信息
          if (error.message !== '页面重定向中，请求已取消') {
            if (error.response?.data?.msg) {
              message.error(error.response.data.msg)
            } else {
              message.error(t('common.failed'))
            }
          }
        } finally {
          loading.value = false
        }
      } else {
        // 创建用户时，直接传明文密码，不加密
        const createData: HbtUserCreate = {
          userId: 0, // 创建时不需要用户ID
          userName: form.userName,
          nickName: form.nickName,
          userType: form.userType,
          password: 'Hbt@123852', // 设置默认密码
          realName: form.realName,
          fullName: form.fullName,
          englishName: form.englishName,
          email: form.email,
          phoneNumber: form.phoneNumber,
          gender: form.gender,
          avatar: form.avatar,
          status: form.status,
          deptId: form.deptIds.length > 0 ? form.deptIds[0] : 0, // 使用第一个部门ID
          roleIds: form.roleIds.map(id => Number(id)),
          postIds: form.postIds.map(id => Number(id)),
          deptIds: form.deptIds.map(id => Number(id)),
          remark: form.remark
        }
        console.log('创建用户数据:', createData)
        try {
          res = await createUser(createData)
          if (res.data.code === 200) {
            message.success(t('common.success'))
            emit('success')
            handleVisibleChange(false)
          } else {
            message.error(res.data.msg || t('common.failed'))
          }
        } catch (error: any) {
          console.error('创建用户失败:', error)
          if (error.response?.data?.msg) {
            message.error(error.response.data.msg)
          } else {
            message.error(t('common.failed'))
          }
        }
      }
    } catch (error) {
      console.error(error)
      message.error(t('common.failed'))
    }
  })
}

// 重置表单
const resetForm = () => {  
  form.userName = ''
  form.nickName = ''
  form.realName = ''
  form.fullName = ''
  form.englishName = ''
  form.userType = 0
  form.password = ''
  form.email = ''
  form.phoneNumber = ''
  form.gender = 0
  form.avatar = ''
  form.status = 0
  form.remark = ''
  form.roleIds = []
  form.postIds = []
  form.deptIds = []
  formRef.value?.resetFields()
}

// 处理弹窗显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    resetForm()
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// 监听userName变化，自动赋值englishName（仅为空时）
watch(
  () => form.userName,
  (val) => {
    if (!props.userId && !form.englishName) {
      form.englishName = val
    }
  }
)
// 监听nickName变化，自动赋值realName、fullName（仅为空时）
watch(
  () => form.nickName,
  (val) => {
    if (!props.userId) {
      if (!form.realName) form.realName = val
      if (!form.fullName) form.fullName = val
    }
  }
)

// 监听用户ID变化
watch(
  () => props.userId,
  (val) => {
    if (val) {
      getInfo(val)
    } else {
      formRef.value?.resetFields()
      // 新建时同步赋值
      if (form.nickName) {
        form.realName = form.nickName
        form.fullName = form.nickName
        form.englishName = form.nickName
      }
    }
  }
)

// 组件挂载时加载数据
onMounted(async () => {
  await fetchOptions()
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