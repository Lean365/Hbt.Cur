<template>
  <a-form
    ref="formRef"
    :model="form"
    :rules="rules"
    :label-col="{ span: 4 }"
    :wrapper-col="{ span: 19 }"
  >
    <a-form-item :label="t('admin.translation.langCode.label')" name="langCode">
      <a-select
        v-model:value="form.langCode"
        :placeholder="t('admin.translation.langCode.placeholder')"
        :loading="languageLoading"
      >
        <a-select-option v-for="lang in languageList" :key="lang.langCode" :value="lang.langCode">
          {{ lang.langName }}
        </a-select-option>
      </a-select>
    </a-form-item>
    <a-form-item :label="t('admin.translation.module.label')" name="module">
      <a-input v-model:value="form.module" :placeholder="t('admin.translation.module.placeholder')" />
    </a-form-item>
    <a-form-item :label="t('admin.translation.key.label')" name="key">
      <a-input v-model:value="form.key" :placeholder="t('admin.translation.key.placeholder')" />
    </a-form-item>
    <a-form-item :label="t('admin.translation.value.label')" name="value">
      <a-textarea v-model:value="form.value" :placeholder="t('admin.translation.value.placeholder')" :rows="4" />
    </a-form-item>
    <a-form-item :label="t('admin.translation.remark.label')" name="remark">
      <a-textarea v-model:value="form.remark" :placeholder="t('admin.translation.remark.placeholder')" />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { TranslationCreate, TranslationUpdate } from '@/types/admin/translation'
import type { Language } from '@/types/admin/language'
import { getSupportedLanguages } from '@/api/admin/language'

const { t } = useI18n()

const props = defineProps<{
  formData?: TranslationCreate | TranslationUpdate
}>()

const emit = defineEmits<{
  (e: 'update:formData', value: TranslationCreate | TranslationUpdate): void
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<TranslationCreate | TranslationUpdate>({
  langCode: '',
  module: '',
  key: '',
  value: '',
  remark: '',
  ...props.formData
})

// 表单校验规则
const rules = {
  langCode: [
    { required: true, message: t('admin.translation.langCode.validation.required') }
  ],
  module: [
    { required: true, message: t('admin.translation.module.validation.required') }
  ],
  key: [
    { required: true, message: t('admin.translation.key.validation.required') }
  ],
  value: [
    { required: true, message: t('admin.translation.value.validation.required') }
  ]
}

// 语言列表
const languageList = ref<Language[]>([])
const languageLoading = ref(false)

// 获取语言列表
const getLanguageList = async () => {
  languageLoading.value = true
  try {
    const response = await getSupportedLanguages()
    languageList.value = response.data.data
  } catch (error) {
    console.error(error)
  }
  languageLoading.value = false
}

// 表单验证方法
const validate = async () => {
  return await formRef.value?.validate()
}

// 重置表单
const resetFields = () => {
  formRef.value?.resetFields()
}

// 监听表单数据变化
watch(form, (newValue) => {
  emit('update:formData', newValue)
}, { deep: true })

// 组件挂载时获取语言列表
onMounted(() => {
  getLanguageList()
})

// 暴露方法给父组件
defineExpose({
  form,
  validate,
  resetFields
})
</script> 