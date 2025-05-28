<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :confirm-loading="loading"
    :width="1000"
    @update:visible="handleVisibleChange"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item name="tenantName" :label="t('identity.tenant.fields.tenantName.label')">
            <a-input v-model:value="formData.tenantName" :placeholder="t('identity.tenant.fields.tenantName.placeholder')" allow-clear show-count :maxlength="50" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="tenantCode" :label="t('identity.tenant.fields.tenantCode.label')">
            <a-input v-model:value="formData.tenantCode" :placeholder="t('identity.tenant.fields.tenantCode.placeholder')" allow-clear show-count :maxlength="50" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="contactUser" :label="t('identity.tenant.fields.contactUser.label')">
            <a-input v-model:value="formData.contactUser" :placeholder="t('identity.tenant.fields.contactUser.placeholder')" allow-clear show-count :maxlength="20" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="contactPhone" :label="t('identity.tenant.fields.contactPhone.label')">
            <a-input v-model:value="formData.contactPhone" :placeholder="t('identity.tenant.fields.contactPhone.placeholder')" allow-clear show-count :maxlength="20" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="contactEmail" :label="t('identity.tenant.fields.contactEmail.label')">
            <a-input v-model:value="formData.contactEmail" :placeholder="t('identity.tenant.fields.contactEmail.placeholder')" allow-clear show-count :maxlength="50" />
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item name="address" :label="t('identity.tenant.fields.address.label')">
            <a-input v-model:value="formData.address" :placeholder="t('identity.tenant.fields.address.placeholder')" allow-clear />
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item name="dbConnection" :label="t('identity.tenant.fields.dbConnection.label')">
            <db-connection v-model="formData.dbConnection" />
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item name="domain" :label="t('identity.tenant.fields.domain.label')">
            <hbt-select v-model:value="formData.domain" dict-type="sys_domain" :placeholder="t('identity.tenant.fields.domain.placeholder')" style="width: 100%" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="logoUrl" :label="t('identity.tenant.fields.logoUrl.label')">
            <hbt-select v-model:value="formData.logoUrl" dict-type="sys_logo_url" :placeholder="t('identity.tenant.fields.logoUrl.placeholder')" style="width: 100%" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="theme" :label="t('identity.tenant.fields.theme.label')">
            <hbt-select v-model:value="formData.theme" dict-type="sys_theme" :placeholder="t('identity.tenant.fields.theme.placeholder')" style="width: 100%" />
          </a-form-item>
        </a-col>

        <a-col :span="24">
          <a-form-item name="license" :label="t('identity.tenant.fields.license.label')">
            <hbt-select v-model:value="formData.license" dict-type="sys_license" :placeholder="t('identity.tenant.fields.license.placeholder')" style="width: 100%" />
          </a-form-item>
        </a-col>

        <a-col :span="12">
          <a-form-item name="licenseStartTime" :label="t('identity.tenant.fields.licenseStartTime.label')">
            <a-date-picker v-model:value="formData.licenseStartTime" :placeholder="t('identity.tenant.fields.licenseStartTime.placeholder')" style="width: 100%" show-time />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="licenseEndTime" :label="t('identity.tenant.fields.licenseEndTime.label')">
            <a-date-picker v-model:value="formData.licenseEndTime" :placeholder="t('identity.tenant.fields.licenseEndTime.placeholder')" style="width: 100%" show-time />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="maxUserCount" :label="t('identity.tenant.fields.maxUserCount.label')">
            <a-input-number v-model:value="formData.maxUserCount" :min="1" :placeholder="t('identity.tenant.fields.maxUserCount.placeholder')" style="width: 100%" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="expireTime" :label="t('identity.tenant.fields.expireTime.label')">
            <a-date-picker v-model:value="formData.expireTime" :placeholder="t('identity.tenant.fields.expireTime.placeholder')" style="width: 100%" show-time />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="status" :label="t('identity.tenant.fields.status.label')">
            <hbt-select v-model:value="formData.status" dict-type="sys_normal_disable" type="radio" :show-all="false" :placeholder="t('identity.tenant.fields.status.placeholder')" style="width: 100%" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="isDefault" :label="t('identity.tenant.fields.isDefault.label')">
            <hbt-select v-model:value="formData.isDefault" dict-type="sys_yes_no" type="radio" :show-all="false" :placeholder="t('identity.tenant.fields.isDefault.placeholder')" style="width: 100%" />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtTenantCreate } from '@/types/identity/tenant'
import { getTenant, createTenant, updateTenant } from '@/api/identity/tenant'
import dayjs from 'dayjs'
import DbConnection from './DbConnection.vue'

const { t } = useI18n()

const props = defineProps<{
  visible: boolean
  title: string
  tenantId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const formData = ref<Partial<HbtTenantCreate>>({
  tenantName: '',
  tenantCode: '',
  contactUser: '',
  contactPhone: '',
  contactEmail: '',
  address: '',
  license: '',
  dbConnection: '',
  domain: '',
  logoUrl: '',
  theme: '',
  licenseStartTime: dayjs(),
  licenseEndTime: dayjs().add(1, 'year').subtract(1, 'day'),
  maxUserCount: 9,
  expireTime: dayjs().add(1, 'year').subtract(1, 'day'),
  status:1,
  isDefault:0
})

// 校验规则
const rules: Record<string, Rule[]> = {
  tenantName: [
    { required: true, message: t('identity.tenant.fields.tenantName.validation.required'), trigger: 'blur' },
    { max: 50, message: t('identity.tenant.fields.tenantName.validation.maxLength') }
  ],
  tenantCode: [
    { required: true, message: t('identity.tenant.fields.tenantCode.validation.required'), trigger: 'blur' },
    { max: 50, message: t('identity.tenant.fields.tenantCode.validation.maxLength') }
  ],
  contactUser: [
    { required: true, message: t('identity.tenant.fields.contactUser.validation.required'), trigger: 'blur' },
    { max: 20, message: t('identity.tenant.fields.contactUser.validation.maxLength') }
  ],
  contactPhone: [
    { required: true, message: t('identity.tenant.fields.contactPhone.validation.required'), trigger: 'blur' },
    { max: 20, message: t('identity.tenant.fields.contactPhone.validation.maxLength') }
  ],
  contactEmail: [
    { required: true, message: t('identity.tenant.fields.contactEmail.validation.required'), trigger: 'blur' },
    { max: 50, message: t('identity.tenant.fields.contactEmail.validation.maxLength') },
    { type: 'email', message: t('identity.tenant.fields.contactEmail.validation.format') }
  ],
  dbConnection: [
    { required: true, message: t('identity.tenant.fields.dbConnection.validation.required'), trigger: 'blur' },
    { max: 100, message: t('identity.tenant.fields.dbConnection.validation.maxLength') }
  ],
  domain: [
    { required: true, message: t('identity.tenant.fields.domain.validation.required'), trigger: 'blur' },
    { max: 100, message: t('identity.tenant.fields.domain.validation.maxLength') }
  ]
}

// 重置表单
const resetForm = () => {
  formData.value = {
    tenantName: '',
    tenantCode: '',
    contactUser: '',
    contactPhone: '',
    contactEmail: '',
    address: '',
    dbConnection: '',
    domain: '',
    logoUrl: '',
    theme: '',
    license: '',
    licenseStartTime: dayjs(),
    licenseEndTime: dayjs().add(1, 'year').subtract(1, 'day'),
    maxUserCount: 9,
    expireTime: dayjs().add(1, 'year').subtract(1, 'day'),
    status: 1,
    isDefault: 1
  }
  formRef.value?.resetFields()
}

// 监听租户ID变化，加载或重置表单
watch(
  () => props.tenantId,
  async (newVal: number | undefined) => {
    if (newVal) {
      try {
        const res = await getTenant(newVal)
        if (res.data.code === 200) {
          const data = res.data.data
          if (data.licenseStartTime) {
            data.licenseStartTime = dayjs(data.licenseStartTime)
          }
          if (data.licenseEndTime) {
            data.licenseEndTime = dayjs(data.licenseEndTime)
          }
          formData.value = data
        } else {
          message.error(res.data.msg || t('common.failed'))
        }
      } catch (error) {
        console.error('[租户管理] 获取租户详情出错:', error)
        message.error(t('common.failed'))
      }
    } else {
      resetForm()
      // 新增租户时获取最大编号
    }
  },
  { immediate: true }
)

// 弹窗显示/关闭
const handleVisibleChange = (val: boolean) => {
  console.log('[租户管理] 弹窗显示状态变化:', val)
  if (!val) {
    console.log('[租户管理] 关闭弹窗，重置表单')
    resetForm()
  }
  emit('update:visible', val)
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// 提交
const handleOk = async () => {
  try {
    console.log('[租户管理] 提交表单，当前数据:', formData.value)
    await formRef.value?.validate()
    loading.value = true
    const data = { ...formData.value } as HbtTenantCreate
    console.log('[租户管理] 准备提交的数据:', data)
    if (data.licenseStartTime) {
      data.licenseStartTime = dayjs(data.licenseStartTime).format('YYYY-MM-DD HH:mm:ss')
    }
    if (data.licenseEndTime) {
      data.licenseEndTime = dayjs(data.licenseEndTime).format('YYYY-MM-DD HH:mm:ss')
    }
    let res
    if (!props.tenantId) {
      console.log('[租户管理] 创建新租户')
      res = await createTenant(data)
      console.log('[租户管理] 创建租户响应:', res)
      if (res.data.code === 200) {
        message.success(t('common.create.success'))
        emit('success')
        emit('update:visible', false)
      } else {
        message.error(res.data.msg || t('common.create.failed'))
      }
    } else {
      console.log('[租户管理] 更新租户')
      res = await updateTenant({ ...data, tenantId: props.tenantId })
      console.log('[租户管理] 更新租户响应:', res)
      if (res.data.code === 200) {
        message.success(t('common.update.success'))
        emit('success')
        emit('update:visible', false)
      } else {
        message.error(res.data.msg || t('common.update.failed'))
      }
    }
  } catch (error) {
    console.error('[租户管理] 提交表单出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}
</script>
