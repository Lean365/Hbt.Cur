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
      <a-tab-pane key="1" :tab="t('workflow.instance.tabs.basic')">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 4 }"
          :wrapper-col="{ span: 20 }"
        >
          <a-form-item label="实例名称" name="instanceName">
            <a-input v-model:value="formState.instanceName" placeholder="请输入实例名称" />
          </a-form-item>
          <a-form-item label="工作流类型" name="workflowCategory">
            <hbt-select v-model:value="formState.workflowCategory" dict-type="workflow_category" type="select" 
            :show-all="false" :placeholder="t('generator.config.placeholder.workflowCategory')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="版本" name="workflowVersion">
            <hbt-select v-model:value="formState.workflowVersion" dict-type="workflow_version" type="select" 
            :show-all="false" :placeholder="t('generator.config.placeholder.workflowVersion')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="表单" name="formId">
            <hbt-select v-model:value="formState.formId" :options="formOptions" placeholder="请选择表单" />
          </a-form-item>
          <a-form-item label="状态" name="status">
            <hbt-select v-model:value="formState.status" dict-type="workflow_instance_status" type="select" 
            :show-all="false" :placeholder="t('generator.config.placeholder.status')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="备注" name="remark">
            <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="4" />
          </a-form-item>
        </a-form>
      </a-tab-pane>
      <a-tab-pane key="2" :tab="t('workflow.instance.tabs.designer')">
        <hbt-flow  v-model:value="formState.workflowConfig" />
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
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtInstance, HbtInstanceCreate, HbtInstanceUpdate } from '@/types/workflow/instance'
import { getWorkflowInstance, createWorkflowInstance, updateWorkflowInstance } from '@/api/workflow/instance'
import { useDictStore } from '@/stores/dict'
import HbtWorkflowConfig from './WorkflowConfig.vue'
import { getFormOptions } from '@/api/workflow/form'

const props = defineProps<{
  open: boolean
  title: string
  instanceId?: number
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
const dictStore = useDictStore()
const formRef = ref<FormInstance>()
const loading = ref(false)
const formOptions = ref([])
const activeTab = ref('1')
const isFullscreen = ref(false)

// 表单状态
const formState = reactive<HbtInstanceCreate>({
  instanceName: '',
  businessKey: '',
  definitionId: 0,
  currentNodeId: 0,
  initiatorId: 0,
  formData: '',
  startTime: new Date().toISOString(),
  workflowCategory: '',
  workflowVersion: 'A',
  formId: 0,
  workflowConfig: '',
  status: 0,
  remark: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  instanceName: [
    { required: true, message: t('workflow.instance.fields.instanceName.required'), trigger: 'blur' }
  ],
  workflowCategory: [
    { required: true, message: t('workflow.instance.fields.workflowCategory.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('workflow.instance.fields.status.required'), trigger: 'change' }
  ]
}

// 获取实例详情
const getDetail = async (id: number) => {
  if (!id) {
    return
  }
  try {
    const res = await getWorkflowInstance(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.instanceName = data.instanceName || ''
      formState.workflowCategory = data.workflowCategory || ''
      formState.workflowVersion = data.workflowVersion
      formState.formId = data.formId || 0
      formState.workflowConfig = data.workflowConfig || ''
      formState.status = data.status
      formState.remark = data.remark || ''
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 获取表单选项
const fetchFormOptions = async () => {
  try {
    const res = await getFormOptions()
    if (res.data.code === 200) {
      formOptions.value = res.data.data.map((item: any) => ({ label: item.label, value: item.value }))
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取表单选项失败:', error)
    message.error(t('common.failed'))
  }
}

// 监听表单打开状态
watch(() => props.open, (newVal) => {
  if (newVal) {
    fetchFormOptions()
  }
}, { immediate: true })

// 监听工作流实例ID变化
watch(() => props.instanceId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    // 重置表单状态
    formState.instanceName = ''
    formState.workflowCategory = ''
    formState.workflowVersion = 'A'
    formState.formId = 0
    formState.workflowConfig = ''
    formState.status = 0
    formState.remark = ''
  }
}, { immediate: true })

// 切换全屏
const toggleFullscreen = () => {
  isFullscreen.value = !isFullscreen.value
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    const data: HbtInstanceCreate = {
      instanceName: formState.instanceName,
      businessKey: formState.businessKey,
      definitionId: formState.definitionId,
      currentNodeId: formState.currentNodeId,
      initiatorId: formState.initiatorId,
      formData: formState.formData,
      startTime: formState.startTime,
      workflowCategory: formState.workflowCategory,
      workflowVersion: formState.workflowVersion,
      formId: formState.formId,
      workflowConfig: formState.workflowConfig,
      status: formState.status,
      remark: formState.remark
    }
    let res
    if (props.instanceId) {
      const updateData: HbtInstanceUpdate = {
        ...data,
        instanceId: props.instanceId
      }
      res = await updateWorkflowInstance(updateData)
    } else {
      res = await createWorkflowInstance(data)
    }
    if (res.data.code === 200) {
      message.success(t('common.success'))
      emit('success')
      handleVisibleChange(false)
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

// 取消
const handleVisibleChange = (value: boolean) => {
  emit('update:open', value)
  if (!value) {
    formRef.value?.resetFields()
    // 重置表单状态
    formState.instanceName = ''
    formState.workflowCategory = ''
    formState.workflowVersion = 'A'
    formState.formId = 0
    formState.workflowConfig = ''
    formState.status = 0
    formState.remark = ''
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