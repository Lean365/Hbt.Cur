<template>
    <hbt-modal
      v-model:open="visible"
      :title="title"
      :width="1800"
      :loading="loading"
      :fullscreen="isFullscreen"
      @update:open="handleVisibleChange"
      @ok="handleSubmit"
    >
      <template #extra>
        <a-button type="text" @click="toggleFullscreen">
          <template #icon>
            <component :is="isFullscreen ? 'FullscreenExitOutlined' : 'FullscreenOutlined'" />
          </template>
        </a-button>
      </template>
      <a-tabs v-model:activeKey="activeTab" class="form-tabs">
        <a-tab-pane key="1" :tab="t('workflow.form.tabs.basic')">
          <a-form
            ref="formRef"
            :model="formState"
            :rules="rules"
            :label-col="{ span: 4 }"
            :wrapper-col="{ span: 20 }"
          >
            <a-form-item :label="t('workflow.form.fields.formName')" name="formName">
              <a-input v-model:value="formState.formName" :placeholder="t('workflow.form.placeholder.formName')" />
            </a-form-item>
            <a-form-item :label="t('workflow.form.fields.formDesc')" name="formDesc">
              <a-input v-model:value="formState.formDesc" :placeholder="t('workflow.form.placeholder.formDesc')" />
            </a-form-item>
            <a-form-item :label="t('workflow.form.fields.formCategory')" name="formCategory">
              <hbt-select
                v-model:value="formState.formCategory"
                dict-type="workflow_form_category"
                :placeholder="t('workflow.form.placeholder.formCategory')"
              />
            </a-form-item>
            <a-form-item :label="t('workflow.form.fields.formVersion')" name="formVersion">
              <hbt-select
                v-model:value="formState.formVersion"
                dict-type="workflow_form_version"
                :placeholder="t('workflow.form.placeholder.formVersion')"
              />
            </a-form-item>
            <a-form-item :label="t('workflow.form.fields.status')" name="status">
              <hbt-select
                v-model:value="formState.status"
                dict-type="workflow_form_status"
                :placeholder="t('workflow.form.placeholder.status')"
              />
            </a-form-item>
            <a-form-item :label="t('workflow.form.fields.formConfig')" name="formConfig">
              <a-textarea 
                v-model:value="formState.formConfig" 
                :placeholder="t('workflow.form.placeholder.formConfig')" 
                :rows="10"
                :auto-size="{ minRows: 10, maxRows: 20 }"
                disabled
              />
            </a-form-item>
            <a-form-item :label="t('workflow.form.fields.remark')" name="remark">
              <a-input v-model:value="formState.remark" :placeholder="t('workflow.form.placeholder.remark')" />
            </a-form-item>
          </a-form>
        </a-tab-pane>
        <a-tab-pane key="2" :tab="t('workflow.form.tabs.designer')">
          <form-config v-model:value="formState.formConfig" />
        </a-tab-pane>
      </a-tabs>
    </hbt-modal>
  </template>
  
  <script lang="ts" setup>
  import { ref, reactive, watch, computed } from 'vue'
  import { useI18n } from 'vue-i18n'
  import { message } from 'ant-design-vue'
  import { FullscreenOutlined, FullscreenExitOutlined } from '@ant-design/icons-vue'
  import type { FormInstance } from 'ant-design-vue'
  import { getForm, createForm, updateForm } from '@/api/workflow/form'
  import type { HbtForm, HbtFormCreate, HbtFormUpdate } from '@/types/workflow/form'
  import FormConfig from './FormConfig.vue'
  
  const props = defineProps<{
    open: boolean
    title: string
    formId?: number
  }>()
  
  const emit = defineEmits<{
    (e: 'update:open', value: boolean): void
    (e: 'success'): void
  }>()
  const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})
  const { t } = useI18n()
  const formRef = ref<FormInstance>()
  const loading = ref(false)
  const activeTab = ref('1')
  
  const formState = reactive<HbtFormCreate>({
    formName: '',
    formDesc: '',
    formConfig: '',
    formVersion: 'v1.0.0-draft',
    formCategory: 0,
    status: 0,
    remark: ''
  })
  
  const rules: Record<string, any[]> = {
    formName: [
      { required: true, message: t('workflow.form.placeholder.validation.formName.required'), trigger: 'blur' },
      { max: 50, message: t('workflow.form.placeholder.validation.formName.length'), trigger: 'blur' },
      { pattern: /^[\u4e00-\u9fa5a-zA-Z0-9_-]+$/, message: t('workflow.form.placeholder.validation.formName.pattern'), trigger: 'blur' }
    ],
    formDesc: [
      { required: true, message: t('workflow.form.placeholder.validation.formDesc.required'), trigger: 'blur' },
      { max: 200, message: t('workflow.form.placeholder.validation.formDesc.length'), trigger: 'blur' }
    ],
    formConfig: [
      { required: true, message: t('workflow.form.placeholder.validation.formConfig.required'), trigger: 'change' }
    ],
    status: [
      { required: true, message: t('workflow.form.placeholder.validation.status.required'), trigger: 'change' }
    ]
  }
  
  const isFullscreen = ref(false)
  
  const toggleFullscreen = () => {
    isFullscreen.value = !isFullscreen.value
  }
  
  // 获取表单详情
  const getDetail = async (id: number) => {
    try {
      const res = await getForm(id)
      if (res.data.code === 200) {
        const data = res.data.data
        formState.formName = data.formName || ''
        formState.formDesc = data.formDesc || ''
        formState.formConfig = data.formConfig || ''
        formState.status = data.status
        formState.remark = data.remark
      } else {
        message.error(res.data.msg || t('common.failed'))
      }
    } catch (error) {
      console.error(error)
      message.error(t('common.failed'))
    }
  }
  
  // 监听表单ID变化
  watch(() => props.formId, (newVal) => {
    if (newVal) {
      getDetail(newVal)
    } else {
      formState.formName = ''
      formState.formDesc = ''
      formState.formConfig = ''
      formState.formVersion = 'v1.0.0-draft'
      formState.formCategory = 0
      formState.status = 0
      formState.remark = ''
    }
  }, { immediate: true })
  
  // 处理可见性变化
  const handleVisibleChange = (value: boolean) => {
    if (!value) {
      formRef.value?.resetFields()
    }
  }
  
  // 提交表单
  const handleSubmit = async () => {
    try {
      await formRef.value?.validate()
      loading.value = true
      const data = { ...formState }
      let res
      if (props.formId) {
        res = await updateForm({ ...data, formId: props.formId })
      } else {
        res = await createForm(data)
      }
      if (res.data.code === 200) {
        message.success(t('common.success'))
        // 重置表单
        formRef.value?.resetFields()
        // 重置表单状态
        formState.formName = ''
        formState.formDesc = ''
        formState.formConfig = ''
        formState.formVersion = 'v1.0.0-draft'
        formState.formCategory = 0
        formState.status = 0
        formState.remark = ''
        // 触发成功事件
        emit('success')
      } else {
        message.error(res.data.msg || t('common.failed'))
      }
    } catch (error) {
      console.error(error)
      message.error(t('common.failed'))
    } finally {
      loading.value = false
    }
  }
  </script>
  
  <style lang="less" scoped>
  .form-tabs {
    :deep(.ant-tabs-content) {
      height: calc(100vh - 200px);
      overflow-y: auto;
    }
  }
  </style>
  