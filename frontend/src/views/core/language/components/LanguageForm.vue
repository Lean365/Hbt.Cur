//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : LanguageForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言表单组件
//===================================================================

<template>
  <hbt-modal
    :title="title"
    :open="open"
    :confirm-loading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
      class="language-form"
    >
      <a-form-item :label="t('core.language.fields.langCode.label')" name="langCode">
        <hbt-flag-icon
          v-model="formData.langCode"
          :placeholder="t('core.language.fields.langCode.placeholder')"
          :disabled="!!languageId"
          @change="handleLangCodeChange"
        />
      </a-form-item>

      <a-form-item :label="t('core.language.fields.langName.label')" name="langName">
        <a-input
          v-model:value="formData.langName"
          :placeholder="t('core.language.fields.langName.placeholder')"
          disabled
        />
      </a-form-item>

      <a-form-item :label="t('core.language.fields.langIcon.label')" name="langIcon">
        <a-input
          v-model:value="formData.langIcon"
          :placeholder="t('core.language.fields.langIcon.placeholder')"
          disabled
        />
      </a-form-item>

      <a-form-item :label="t('core.language.fields.orderNum.label')" name="orderNum">
        <a-input-number
          v-model:value="formData.orderNum"
          :min="0"
          :max="999"
          :placeholder="t('core.language.fields.orderNum.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item :label="t('core.language.fields.isDefault.label')" name="isDefault">
        <a-switch
          v-model:checked="formData.isDefault"
          :checked-value="1"
          :unchecked-value="0"
        />
      </a-form-item>

      <a-form-item :label="t('core.language.fields.isBuiltin.label')" name="isBuiltin">
        <a-switch
          v-model:checked="formData.isBuiltin"
          :checked-value="1"
          :unchecked-value="0"
        />
      </a-form-item>

      <a-form-item :label="t('core.language.fields.status.label')" name="status">
        <a-switch
          v-model:checked="formData.status"
          :checked-value="0"
          :unchecked-value="1"
        />
      </a-form-item>

      <a-form-item :label="t('core.language.fields.remark.label')" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          :rows="3"
          :placeholder="t('core.language.fields.remark.placeholder')"
        />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtLanguage } from '@/types/core/language'
import { getHbtLanguage, createHbtLanguage, updateHbtLanguage } from '@/api/core/language'
import countries from 'i18n-iso-countries'
import zh from 'i18n-iso-countries/langs/zh.json'
import en from 'i18n-iso-countries/langs/en.json'
import type { SelectValue, DefaultOptionType } from 'ant-design-vue/es/select'
import HbtFlagIcon from '@/components/Base/HbtFlagIcon/index.vue'

// 注册语言
countries.registerLocale(zh)
countries.registerLocale(en)

const props = defineProps<{
  open: boolean
  title: string
  languageId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t, locale } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const formRef = ref<FormInstance>()

// 获取所有国家的ISO代码
const countryCodes = countries.getAlpha2Codes()

// === 表单数据 ===
const formData = reactive<HbtLanguage>({
  languageId: 0,
  langCode: '',
  langName: '',
  langIcon: '',
  orderNum: 0,
  isDefault: 0,
  isBuiltin: 0,
  status: 0,
  tenantId: 0,
  createBy: '',
  createTime: '',
  isDeleted: 0,
  remark: '',
  updateTime: '',
  updateBy: ''
})

// === 表单校验规则 ===
const rules: Record<string, Rule[]> = {
  langCode: [
    { required: true, type: 'string', message: t('core.language.fields.langCode.validation.required'), trigger: 'blur' },
    { type: 'string', min: 2, max: 10, message: t('core.language.fields.langCode.validation.length'), trigger: 'blur' }
  ],
  langName: [
    { required: true, type: 'string', message: t('core.language.fields.langName.validation.required'), trigger: 'blur' },
    { type: 'string', min: 2, max: 50, message: t('core.language.fields.langName.validation.length'), trigger: 'blur' }
  ],
  langIcon: [
    { required: true, type: 'string', message: t('core.language.fields.langIcon.validation.required'), trigger: 'blur' }
  ],
  orderNum: [
    { required: true, type: 'number', message: t('core.language.fields.orderNum.validation.required'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('core.language.fields.status.validation.required'), trigger: 'change' }
  ]
}

// 处理语言代码变化
const handleLangCodeChange = (value: { code: string; name: string }) => {
  if (value) {
    formData.langName = value.name
    formData.langIcon = `fi-${value.code.toLowerCase()}`
  } else {
    formData.langName = ''
    formData.langIcon = ''
  }
}

// === 方法定义 ===
// 获取语言详情
const getLanguageDetail = async (id: number) => {
  try {
    loading.value = true
    const res = await getHbtLanguage(id)
    if (res.data.code === 200) {
      Object.assign(formData, res.data.data)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取语言详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 提交表单
const handleSubmit = async () => {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    loading.value = true
    const api = props.languageId ? updateHbtLanguage : createHbtLanguage
    const res = await api(formData)
    if (res.data.code === 200) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
    } else {
      message.error(res.data.msg || t('common.message.saveFailed'))
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error(t('common.message.saveFailed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// === 监听器 ===
watch(
  () => props.open,
  (val) => {
    if (val && props.languageId) {
      getLanguageDetail(props.languageId)
    } else {
      formRef.value?.resetFields()
      Object.assign(formData, {
        languageId: 0,
        langCode: '',
        langName: '',
        langIcon: '',
        orderNum: 0,
        isDefault: 0,
        isBuiltin: 0,
        status: 0,
        tenantId: 0,
        createBy: '',
        createTime: '',
        isDeleted: 0,
        remark: '',
        updateTime: '',
        updateBy: ''
      })
    }
  }
)
</script>

<style lang="less" scoped>
.language-form {
  padding: 24px;

  :deep(.ant-form-item) {
    margin-bottom: 24px;

    .ant-form-item-label {
      text-align: right;
      padding-right: 8px;
    }

    .ant-form-item-control {
      .ant-input,
      .ant-input-number,
      .ant-select,
      .ant-textarea {
        width: 100%;
      }
    }
  }
}
</style> 