<template>
  <hbt-modal
    :title="translationId ? t('common.title.edit') : t('common.title.create')"
   :open="open"
    width="600px"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <a-form-item :label="t('core.translation.fields.moduleName.label')" name="moduleName">
        <a-input v-model:value="form.moduleName" :placeholder="t('core.translation.fields.moduleName.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('core.translation.fields.transKey.label')" name="transKey">
        <a-input v-model:value="form.transKey" :placeholder="t('core.translation.fields.transKey.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('core.translation.fields.transValue.label')" name="transValue">
        <a-textarea
          v-model:value="form.transValue"
          :rows="3"
          :placeholder="t('core.translation.fields.transValue.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('core.translation.fields.orderNum.label')" name="orderNum">
        <a-input-number v-model:value="form.orderNum" :min="0" :max="999" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('core.translation.fields.status.label')" name="status">
        <a-radio-group v-model:value="form.status">
          <a-radio :value="0">{{ t('common.status.enabled') }}</a-radio>
          <a-radio :value="1">{{ t('common.status.disabled') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :label="t('core.translation.fields.remark.label')" name="remark">
        <a-textarea
          v-model:value="form.remark"
          :rows="2"
          :placeholder="t('core.translation.fields.remark.placeholder')"
        />
      </a-form-item>
    </a-form>
    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" :loading="loading" @click="handleSubmit">
          {{ t('common.button.confirm') }}
        </a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtTranslation } from '@/types/core/translation'
import { getHbtTranslation, createHbtTranslation, updateHbtTranslation } from '@/api/core/translation'

const props = defineProps<{
  open: boolean
  translationId?: number
  langCode: string
}>()

const emit = defineEmits(['update:open', 'success'])

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)
const dialogVisible = ref(true)

const form = reactive<HbtTranslation>({
  translationId: 0,
  langCode: props.langCode,
  moduleName: '',
  transKey: '',
  transValue: '',
  orderNum: 0,
  status: 0,
  remark: '',
  createTime: '',
  updateTime: '',
  createBy: '',
  updateBy: '',
  isDeleted: 0
})

const rules: Partial<Record<keyof HbtTranslation, Rule[]>> = {
  moduleName: [
    { required: true, type: 'string', message: t('core.translation.fields.moduleName.validation.required'), trigger: 'blur' },
    { type: 'string', min: 2, max: 50, message: t('core.translation.fields.moduleName.validation.length'), trigger: 'blur' }
  ],
  transKey: [
    { required: true, type: 'string', message: t('core.translation.fields.transKey.validation.required'), trigger: 'blur' },
    { type: 'string', min: 2, max: 100, message: t('core.translation.fields.transKey.validation.length'), trigger: 'blur' }
  ],
  transValue: [
    { required: true, type: 'string', message: t('corion.fields.trafields.nsValue.va.validation.ridation.required'), trigger: 'blur' }
  ],
  orderNum: [
    { required: true, type: 'number', message: t('corion.fields.ordfields.erNum.va.validation.ridation.required'), trigger: 'blur' }
  ],
  status: [
    { required: true, type: 'number', message: t('core.translation.fields.status.validation.required'), trigger: 'change' }
  ]
}

// 获取翻译详情
const getDetail = async () => {
  if (!props.translationId) return
  
  try {
    loading.value = true
    const res = await getHbtTranslation(props.translationId)
    if (res.data.code === 200) {
      Object.assign(form, res.data.data)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取翻译详情失败:', error)
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
    
    const data = {
      ...form,
      langCode: props.langCode,
      id: form.translationId
    }
    
    const res = props.translationId
      ? await updateHbtTranslation(data)
      : await createHbtTranslation(data)
      
    if (res.data.code === 200) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
      handleCancel()
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
  emit('update:open', false)
  //emit('update:open', false)
}

// 监听对话框可见性变化
watch(() => props.open, (val) => {
  if (val && props.translationId) {
    getDetail()
  }
})

// 初始化
if (props.translationId) {
  getDetail()
}
</script>

<style lang="less" scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 