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
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 8 }"
      :wrapper-col="{ span: 14 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="用户ID" name="userId">
            <a-input-number
              v-model:value="form.userId"
              placeholder="请输入用户ID"
              style="width: 100%"
              :min="1"
              :disabled="!!props.oauthId"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="提供商" name="provider">
            <a-select
              v-model:value="form.provider"
              placeholder="请选择提供商"
              style="width: 100%"
            >
              <a-select-option value="github">GitHub</a-select-option>
              <a-select-option value="google">Google</a-select-option>
              <a-select-option value="facebook">Facebook</a-select-option>
              <a-select-option value="twitter">Twitter</a-select-option>
              <a-select-option value="qq">QQ</a-select-option>
              <a-select-option value="wechat">微信</a-select-option>
              <a-select-option value="alipay">支付宝</a-select-option>
              <a-select-option value="gitee">Gitee</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="OAuth用户ID" name="oauthUserId">
            <a-input
              v-model:value="form.oauthUserId"
              placeholder="请输入OAuth用户ID"
              :disabled="!!props.oauthId"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="OAuth用户名" name="oauthUserName">
            <a-input
              v-model:value="form.oauthUserName"
              placeholder="请输入OAuth用户名"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="是否主要账号" name="isPrimary">
            <a-radio-group v-model:value="form.isPrimary">
              <a-radio :value="1">主要账号</a-radio>
              <a-radio :value="0">次要账号</a-radio>
            </a-radio-group>
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="状态" name="status">
            <a-radio-group v-model:value="form.status">
              <a-radio :value="0">正常</a-radio>
              <a-radio :value="1">禁用</a-radio>
            </a-radio-group>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item label="备注" name="remark">
            <a-textarea
              v-model:value="form.remark"
              placeholder="请输入备注"
              :rows="4"
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { HbtOAuth, HbtOAuthForm, HbtOAuthCreate, HbtOAuthUpdate } from '@/types/identity/oauth'
import { getOAuth, createOAuth, updateOAuth } from '@/api/identity/auth/oatuh'

const props = defineProps<{
  visible: boolean
  title: string
  oauthId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<HbtOAuthForm>({
  userId: 0,
  provider: '',
  oauthUserId: '',
  oauthUserName: '',
  isPrimary: 0,
  status: 0,
  remark: ''
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  userId: [
    { required: true, message: '请输入用户ID' },
    { type: 'number', min: 1, message: '用户ID必须大于0' }
  ],
  provider: [
    { required: true, message: '请选择提供商' }
  ],
  oauthUserId: [
    { required: true, message: '请输入OAuth用户ID' }
  ],
  oauthUserName: [
    { required: true, message: '请输入OAuth用户名' }
  ],
  isPrimary: [
    { required: true, message: '请选择是否主要账号' }
  ],
  status: [
    { required: true, message: '请选择状态' }
  ]
}

// 加载状态
const loading = ref(false)

// 获取OAuth信息
const getInfo = async (oauthId: number) => {
  console.log('oauthId:', oauthId)
  try {
    const res = await getOAuth(oauthId)
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

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    let res
    if (props.oauthId) {
      // 更新OAuth账号
      const updateData: HbtOAuthUpdate = {
        id: props.oauthId,
        userId: form.userId,
        provider: form.provider,
        oauthUserId: form.oauthUserId,
        oauthUserName: form.oauthUserName,
        isPrimary: form.isPrimary,
        status: form.status,
        remark: form.remark
      }
      console.log('更新OAuth账号数据:', updateData)
      try {
        res = await updateOAuth(updateData)
        if (res.data.code === 200) {
          message.success(t('common.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          message.error(res.data.msg || t('common.failed'))
        }
      } catch (error: any) {
        console.error('更新OAuth账号失败:', error)
        if (error.response?.data?.msg) {
          message.error(error.response.data.msg)
        } else {
          message.error(t('common.failed'))
        }
      }
    } else {
      // 创建OAuth账号
      const createData: HbtOAuthCreate = {
        userId: form.userId,
        provider: form.provider,
        oauthUserId: form.oauthUserId,
        oauthUserName: form.oauthUserName,
        isPrimary: form.isPrimary,
        status: form.status,
        remark: form.remark
      }
      console.log('创建OAuth账号数据:', createData)
      try {
        res = await createOAuth(createData)
        if (res.data.code === 200) {
          message.success(t('common.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          message.error(res.data.msg || t('common.failed'))
        }
      } catch (error: any) {
        console.error('创建OAuth账号失败:', error)
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
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  form.userId = 0
  form.provider = ''
  form.oauthUserId = ''
  form.oauthUserName = ''
  form.isPrimary = 0
  form.status = 0
  form.remark = ''
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

// 监听OAuth ID变化
watch(
  () => props.oauthId,
  (val) => {
    if (val) {
      getInfo(val)
    } else {
      formRef.value?.resetFields()
    }
  }
)

// 组件挂载时加载数据
onMounted(async () => {
  // 可以在这里加载一些基础数据
})
</script>

<style lang="less" scoped>
// 样式可以根据需要添加
</style> 