<template>
  <a-modal
    :visible="visible"
    :title="title"
    :confirm-loading="loading"
    :destroy-on-close="true"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item
        name="tenantCode"
        :label="t('identity.tenant.fields.tenantCode.label')"
      >
        <a-input
          v-model:value="formData.tenantCode"
          :placeholder="t('identity.tenant.fields.tenantCode.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="tenantName"
        :label="t('identity.tenant.fields.tenantName.label')"
      >
        <a-input
          v-model:value="formData.tenantName"
          :placeholder="t('identity.tenant.fields.tenantName.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="contactPerson"
        :label="t('identity.tenant.fields.contactPerson.label')"
      >
        <a-input
          v-model:value="formData.contactPerson"
          :placeholder="t('identity.tenant.fields.contactPerson.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="contactPhone"
        :label="t('identity.tenant.fields.contactPhone.label')"
      >
        <a-input
          v-model:value="formData.contactPhone"
          :placeholder="t('identity.tenant.fields.contactPhone.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="contactEmail"
        :label="t('identity.tenant.fields.contactEmail.label')"
      >
        <a-input
          v-model:value="formData.contactEmail"
          :placeholder="t('identity.tenant.fields.contactEmail.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="address"
        :label="t('identity.tenant.fields.address.label')"
      >
        <a-input
          v-model:value="formData.address"
          :placeholder="t('identity.tenant.fields.address.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="domain"
        :label="t('identity.tenant.fields.domain.label')"
      >
        <a-input
          v-model:value="formData.domain"
          :placeholder="t('identity.tenant.fields.domain.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="logoUrl"
        :label="t('identity.tenant.fields.logoUrl.label')"
      >
        <a-input
          v-model:value="formData.logoUrl"
          :placeholder="t('identity.tenant.fields.logoUrl.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="theme"
        :label="t('identity.tenant.fields.theme.label')"
      >
        <a-input
          v-model:value="formData.theme"
          :placeholder="t('identity.tenant.fields.theme.placeholder')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        name="licenseStartTime"
        :label="t('identity.tenant.fields.licenseStartTime.label')"
      >
        <a-date-picker
          v-model:value="formData.licenseStartTime"
          :placeholder="t('identity.tenant.fields.licenseStartTime.placeholder')"
          style="width: 100%"
          show-time
        />
      </a-form-item>
      <a-form-item
        name="licenseEndTime"
        :label="t('identity.tenant.fields.licenseEndTime.label')"
      >
        <a-date-picker
          v-model:value="formData.licenseEndTime"
          :placeholder="t('identity.tenant.fields.licenseEndTime.placeholder')"
          style="width: 100%"
          show-time
        />
      </a-form-item>
      <a-form-item
        name="maxUserCount"
        :label="t('identity.tenant.fields.maxUserCount.label')"
      >
        <a-input-number
          v-model:value="formData.maxUserCount"
          :placeholder="t('identity.tenant.fields.maxUserCount.placeholder')"
          :min="1"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item
        name="status"
        :label="t('identity.tenant.fields.status.label')"
      >
        <hbt-select
          v-model:value="formData.status"
          dict-type="sys_normal_disable"
          :placeholder="t('identity.tenant.fields.status.placeholder')"
        />
      </a-form-item>
      <a-form-item
        name="remark"
        :label="t('common.remark')"
      >
        <a-textarea
          v-model:value="formData.remark"
          :placeholder="t('common.remark.placeholder')"
          :rows="4"
          allow-clear
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Tenant, TenantCreate, TenantUpdate } from '@/types/identity/tenant'
import { getTenant, createTenant, updateTenant } from '@/api/identity/tenant'

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
const formData = ref<Partial<Tenant>>({
  tenantCode: undefined,
  tenantName: undefined,
  contactPerson: undefined,
  contactPhone: undefined,
  contactEmail: undefined,
  address: undefined,
  domain: undefined,
  logoUrl: undefined,
  theme: undefined,
  licenseStartTime: undefined,
  licenseEndTime: undefined,
  maxUserCount: 1,
  status: 0,
  remark: undefined
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  tenantCode: [
    { required: true, message: t('identity.tenant.rules.tenantCode.required') },
    { max: 50, message: t('identity.tenant.rules.tenantCode.maxLength') }
  ],
  tenantName: [
    { required: true, message: t('identity.tenant.rules.tenantName.required') },
    { max: 50, message: t('identity.tenant.rules.tenantName.maxLength') }
  ],
  contactPerson: [
    { required: true, message: t('identity.tenant.rules.contactPerson.required') },
    { max: 20, message: t('identity.tenant.rules.contactPerson.maxLength') }
  ],
  contactPhone: [
    { required: true, message: t('identity.tenant.rules.contactPhone.required') },
    { max: 20, message: t('identity.tenant.rules.contactPhone.maxLength') }
  ],
  contactEmail: [
    { required: true, message: t('identity.tenant.rules.contactEmail.required') },
    { max: 50, message: t('identity.tenant.rules.contactEmail.maxLength') },
    { type: 'email', message: t('identity.tenant.rules.contactEmail.format') }
  ],
  address: [
    { max: 200, message: t('identity.tenant.rules.address.maxLength') }
  ],
  domain: [
    { max: 100, message: t('identity.tenant.rules.domain.maxLength') }
  ],
  logoUrl: [
    { max: 200, message: t('identity.tenant.rules.logoUrl.maxLength') }
  ],
  theme: [
    { max: 50, message: t('identity.tenant.rules.theme.maxLength') }
  ],
  maxUserCount: [
    { required: true, message: t('identity.tenant.rules.maxUserCount.required') },
    { type: 'number', min: 1, message: t('identity.tenant.rules.maxUserCount.min') }
  ],
  status: [
    { required: true, message: t('identity.tenant.rules.status.required') }
  ]
}

// 监听tenantId变化
watch(
  () => props.tenantId,
  async (newVal) => {
    if (newVal) {
      try {
        loading.value = true
        const res = await getTenant(newVal)
        if (res.data.code === 200) {
          formData.value = res.data.data
        } else {
          message.error(res.data.msg || t('common.failed'))
        }
      } catch (error) {
        console.error(error)
        message.error(t('common.failed'))
      } finally {
        loading.value = false
      }
    } else {
      formData.value = {
        tenantCode: undefined,
        tenantName: undefined,
        contactPerson: undefined,
        contactPhone: undefined,
        contactEmail: undefined,
        address: undefined,
        domain: undefined,
        logoUrl: undefined,
        theme: undefined,
        licenseStartTime: undefined,
        licenseEndTime: undefined,
        maxUserCount: 1,
        status: 0,
        remark: undefined
      }
    }
  },
  { immediate: true }
)

// 处理确认
const handleOk = () => {
  formRef.value?.validate().then(async () => {
    try {
      loading.value = true
      const data = formData.value as (TenantCreate | TenantUpdate)
      const res = props.tenantId
        ? await updateTenant({ ...data, tenantId: props.tenantId })
        : await createTenant(data)
      if (res.data.code === 200) {
        message.success(t(props.tenantId ? 'common.update.success' : 'common.create.success'))
        emit('success')
        emit('update:visible', false)
      } else {
        message.error(res.data.msg || t(props.tenantId ? 'common.update.failed' : 'common.create.failed'))
      }
    } catch (error) {
      console.error(error)
      message.error(t(props.tenantId ? 'common.update.failed' : 'common.create.failed'))
    } finally {
      loading.value = false
    }
  })
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
}
</script> 