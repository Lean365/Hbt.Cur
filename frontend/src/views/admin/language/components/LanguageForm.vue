<template>
  <a-modal
    :title="formData ? t('common.edit') : t('common.add')"
    :visible="visible"
    :confirm-loading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item :label="t('admin.language.code.label')" name="langCode">
        <a-input
          v-model:value="form.langCode"
          :placeholder="t('admin.language.code.placeholder')"
          :disabled="!!formData"
        />
      </a-form-item>
      <a-form-item :label="t('admin.language.name.label')" name="langName">
        <a-input
          v-model:value="form.langName"
          :placeholder="t('admin.language.name.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('admin.language.icon.label')" name="langIcon">
        <a-upload
          name="file"
          list-type="picture-card"
          :show-upload-list="false"
          :action="uploadUrl"
          :headers="headers"
          :before-upload="beforeIconUpload"
          @change="handleIconChange"
        >
          <img v-if="form.langIcon" :src="form.langIcon" style="width: 100%" />
          <div v-else>
            <plus-outlined />
            <div class="ant-upload-text">{{ t('common.upload.icon') }}</div>
          </div>
        </a-upload>
      </a-form-item>
      <a-form-item :label="t('common.orderNum')" name="orderNum">
        <a-input-number
          v-model:value="form.orderNum"
          :min="0"
          :max="999"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('common.status')" name="status">
        <a-radio-group v-model:value="form.status">
          <a-radio value="0">{{ t('common.status.normal') }}</a-radio>
          <a-radio value="1">{{ t('common.status.disabled') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :label="t('common.remark')" name="remark">
        <a-textarea
          v-model:value="form.remark"
          :placeholder="t('common.remark.placeholder')"
          :rows="4"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import type { LanguageCreate, LanguageUpdate } from '@/types/admin/language'
import { createLanguage, updateLanguage } from '@/api/admin/language'

const props = defineProps<{
  visible: boolean
  formData?: LanguageCreate | LanguageUpdate
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单数据
const form = reactive<LanguageCreate>({
  langCode: '',
  langName: '',
  langIcon: '',
  orderNum: 0,
  status: '0',
  remark: ''
})

// 表单校验规则
const rules = {
  langCode: [
    { required: true, message: t('admin.language.code.validation.required') },
    { min: 2, max: 20, message: t('admin.language.code.validation.length') }
  ],
  langName: [
    { required: true, message: t('admin.language.name.validation.required') },
    { min: 2, max: 30, message: t('admin.language.name.validation.length') }
  ],
  orderNum: [
    { required: true, message: t('common.orderNum.required') }
  ]
}

// 上传相关配置
const uploadUrl = import.meta.env.VITE_UPLOAD_URL
const headers = {
  Authorization: 'Bearer ' + localStorage.getItem('token')
}

// 上传前校验
const beforeIconUpload = (file: File) => {
  const isImage = file.type.startsWith('image/')
  if (!isImage) {
    message.error(t('common.upload.image.type'))
    return false
  }
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isLt2M) {
    message.error(t('common.upload.image.size'))
    return false
  }
  return true
}

// 图标上传变化
const handleIconChange = (info: any) => {
  if (info.file.status === 'done') {
    form.langIcon = info.file.response.data
  }
}

// 监听表单数据变化
watch(
  () => props.formData,
  (val) => {
    if (val) {
      Object.assign(form, val)
    } else {
      formRef.value?.resetFields()
      Object.assign(form, {
        langCode: '',
        langName: '',
        langIcon: '',
        orderNum: 0,
        status: '0',
        remark: ''
      })
    }
  },
  { immediate: true }
)

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    if (props.formData && 'langId' in props.formData) {
      await updateLanguage({
        ...form,
        langId: props.formData.langId
      })
      message.success(t('common.success.update'))
    } else {
      await createLanguage(form)
      message.success(t('common.success.create'))
    }
    
    emit('success')
    emit('update:visible', false)
  } catch (error) {
    console.error(error)
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  formRef.value?.resetFields()
  emit('update:visible', false)
}
</script>

<style lang="less" scoped>
.ant-upload-text {
  margin-top: 8px;
  color: #666;
}
</style> 