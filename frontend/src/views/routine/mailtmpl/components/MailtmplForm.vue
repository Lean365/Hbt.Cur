<template>
  <a-modal
    :visible="visible"
    :title="title"
    width="700px"
    @cancel="handleCancel"
    @ok="handleOk"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item :label="t('mailtmpl.templateName')" name="templateName">
        <a-input
          v-model:value="formState.templateName"
          :placeholder="t('mailtmpl.placeholder.templateName')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateType')" name="templateType">
        <hbt-select
          v-model:value="formState.templateType"
          dict-type="email_template_type"
          type="radio"
          :placeholder="t('mailtmpl.placeholder.templateType')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateSubject')" name="templateSubject">
        <a-input
          v-model:value="formState.templateSubject"
          :placeholder="t('mailtmpl.placeholder.templateSubject')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateContent')" name="templateContent">
        <a-textarea
          v-model:value="formState.templateContent"
          :placeholder="t('mailtmpl.placeholder.templateContent')"
          :rows="10"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateStatus')" name="templateStatus">
        <hbt-select
          v-model:value="formState.templateStatus"
          dict-type="email_template_status"
          type="radio"
          :placeholder="t('mailtmpl.placeholder.templateStatus')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.remark')" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('mailtmpl.placeholder.remark')"
          :rows="4"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, watch } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import { getMailTemplateDetail, createMailTemplate, updateMailTemplate } from '@/api/routine/mailtmpl'
import type { RuleObject } from 'ant-design-vue/es/form/interface'
import type { HbtMailTemplateDto, HbtMailTemplateCreateDto, HbtMailTemplateUpdateDto } from '@/types/routine/mailtmpl'
import type { HbtApiResponse } from '@/types/common'
import type { AxiosResponse } from 'axios'

const props = defineProps<{
  visible: boolean
  title: string
  templateId?: string
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const formState = ref<Partial<HbtMailTemplateDto>>({
  templateId: undefined,
  templateName: '',
  templateType: 1,
  templateSubject: '',
  templateContent: '',
  templateStatus: 0,
  templateParams: '',
  templateCreateTime: undefined,
  templateUpdateTime: undefined,
  templateCreatorId: 0,
  templateCreatorName: ''
})

// 表单校验规则
const rules: Record<string, RuleObject[]> = {
  templateName: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateName.required'), trigger: 'blur' },
    { type: 'string', max: 100, message: t('mailtmpl.validation.templateName.maxLength'), trigger: 'blur' }
  ],
  templateType: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateType.required'), trigger: 'change' }
  ],
  templateSubject: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateSubject.required'), trigger: 'blur' },
    { type: 'string', max: 200, message: t('mailtmpl.validation.templateSubject.maxLength'), trigger: 'blur' }
  ],
  templateContent: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateContent.required'), trigger: 'blur' },
    { type: 'string', max: 4000, message: t('mailtmpl.validation.templateContent.maxLength'), trigger: 'blur' }
  ],
  templateStatus: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateStatus.required'), trigger: 'change' }
  ]
}

// 监听模板ID变化
watch(() => props.templateId, async (newVal) => {
  if (newVal) {
    try {
      const res: AxiosResponse<HbtApiResponse<HbtMailTemplateDto>> = await getMailTemplateDetail(Number(newVal))
      if (res.data && res.data.data) {
        formState.value = { ...res.data.data }
      }
    } catch (error) {
      console.error('获取邮件模板详情失败:', error)
    }
  } else {
    formState.value = {
      templateId: undefined,
      templateName: '',
      templateType: 1,
      templateSubject: '',
      templateContent: '',
      templateStatus: 0,
      templateParams: '',
      templateCreateTime: undefined,
      templateUpdateTime: undefined,
      templateCreatorId: 0,
      templateCreatorName: ''
    }
  }
}, { immediate: true })

// 取消按钮
const handleCancel = () => {
  emit('update:visible', false)
}

// 确定按钮
const handleOk = async () => {
  try {
    // 使用validateFields()进行表单验证
    const values = await formRef.value?.validateFields()
    if (!values) return
    
    if (formState.value.templateId) {
      // 更新操作
      const updateData: HbtMailTemplateUpdateDto = {
        templateId: BigInt(formState.value.templateId),
        templateName: values.templateName,
        templateType: values.templateType,
        templateSubject: values.templateSubject,
        templateContent: values.templateContent,
        templateStatus: values.templateStatus,
        templateParams: values.templateParams || '',
        templateCreatorId: BigInt(formState.value.templateCreatorId!),
        templateCreatorName: formState.value.templateCreatorName!
      }
      await updateMailTemplate(Number(formState.value.templateId), updateData)
      message.success(t('mailtmpl.operation.success.edit'))
    } else {
      // 创建操作
      const createData: HbtMailTemplateCreateDto = {
        templateName: values.templateName,
        templateType: values.templateType,
        templateSubject: values.templateSubject,
        templateContent: values.templateContent,
        templateStatus: values.templateStatus,
        templateParams: values.templateParams || '',
        templateCreatorId: BigInt(formState.value.templateCreatorId!),
        templateCreatorName: formState.value.templateCreatorName!
      }
      await createMailTemplate(createData)
      message.success(t('mailtmpl.operation.success.add'))
    }
    emit('update:visible', false)
    emit('success')
  } catch (error) {
    // 表单验证失败或API调用失败
    if (error instanceof Error) {
      message.error(error.message)
    }
    console.error('保存邮件模板失败:', error)
  }
}
</script> 