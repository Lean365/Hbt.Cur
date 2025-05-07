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
    :open="visible"
    :confirm-loading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
      class="language-form"
    >
      <a-form-item :label="t('admin.language.langCode')" name="langCode">
        <a-input
          v-model:value="formData.langCode"
          :placeholder="t('admin.language.langCodePlaceholder')"
          :disabled="!!languageId"
        />
      </a-form-item>

      <a-form-item :label="t('admin.language.langName')" name="langName">
        <a-input
          v-model:value="formData.langName"
          :placeholder="t('admin.language.langNamePlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('admin.language.langIcon')" name="langIcon">
        <a-input
          v-model:value="formData.langIcon"
          :placeholder="t('admin.language.langIconPlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('admin.language.orderNum')" name="orderNum">
        <a-input-number
          v-model:value="formData.orderNum"
          :min="0"
          :max="999"
          :placeholder="t('admin.language.orderNumPlaceholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item :label="t('admin.language.isDefault')" name="isDefault">
        <a-switch
          v-model:checked="formData.isDefault"
          :checked-value="1"
          :unchecked-value="0"
        />
      </a-form-item>

      <a-form-item :label="t('admin.language.status')" name="status">
        <a-radio-group v-model:value="formData.status">
          <a-radio :value="0">{{ t('common.status.normal') }}</a-radio>
          <a-radio :value="1">{{ t('common.status.disable') }}</a-radio>
        </a-radio-group>
      </a-form-item>

      <a-form-item :label="t('admin.language.remark')" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          :rows="3"
          :placeholder="t('admin.language.remarkPlaceholder')"
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

const props = defineProps<{
  visible: boolean
  title: string
  languageId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const formRef = ref<FormInstance>()

// === 表单数据 ===
const formData = reactive<HbtLanguage>({
  languageId: 0,
  langCode: '',
  langName: '',
  langIcon: '',
  orderNum: 0,
  isDefault: 0,
  status: 0,
  id: 0,
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
    { required: true, type: 'string', message: t('admin.language.langCodeRequired'), trigger: 'blur' },
    { type: 'string', min: 2, max: 10, message: t('admin.language.langCodeLength'), trigger: 'blur' }
  ],
  langName: [
    { required: true, type: 'string', message: t('admin.language.langNameRequired'), trigger: 'blur' },
    { type: 'string', min: 2, max: 50, message: t('admin.language.langNameLength'), trigger: 'blur' }
  ],
  langIcon: [
    { required: true, type: 'string', message: t('admin.language.langIconRequired'), trigger: 'blur' }
  ],
  orderNum: [
    { required: true, type: 'number', message: t('admin.language.orderNumRequired'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('admin.language.statusRequired'), trigger: 'change' }
  ]
}

// === 方法定义 ===
// 获取语言详情
const getLanguageDetail = async (id: number) => {
  try {
    loading.value = true
    const res = await getHbtLanguage(id)
    if (res.code === 200) {
      Object.assign(formData, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
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
    if (res.code === 200) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
    } else {
      message.error(res.msg || t('common.message.saveFailed'))
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
  () => props.visible,
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
        status: 0,
        id: 0,
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
}
</style> 