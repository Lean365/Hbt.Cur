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
          <a-form-item label="实例标题" name="instanceTitle">
            <a-input v-model:value="formState.instanceTitle" placeholder="请输入实例标题" />
          </a-form-item>
          <a-form-item label="流程定义" name="schemeId">
            <a-input-number v-model:value="formState.schemeId" placeholder="请输入流程定义ID" style="width: 100%" />
          </a-form-item>
          <a-form-item label="业务键" name="businessKey">
            <a-input v-model:value="formState.businessKey" placeholder="请输入业务键" />
          </a-form-item>
          <a-form-item label="发起人ID" name="initiatorId">
            <a-input-number v-model:value="formState.initiatorId" placeholder="请输入发起人ID" style="width: 100%" />
          </a-form-item>
          <a-form-item label="优先级" name="priority">
            <a-select v-model:value="formState.priority" placeholder="请选择优先级" style="width: 100%">
              <a-select-option :value="1">低</a-select-option>
              <a-select-option :value="2">普通</a-select-option>
              <a-select-option :value="3">高</a-select-option>
              <a-select-option :value="4">紧急</a-select-option>
              <a-select-option :value="5">特急</a-select-option>
            </a-select>
          </a-form-item>
          <a-form-item label="紧急程度" name="urgency">
            <a-select v-model:value="formState.urgency" placeholder="请选择紧急程度" style="width: 100%">
              <a-select-option :value="1">普通</a-select-option>
              <a-select-option :value="2">加急</a-select-option>
              <a-select-option :value="3">特急</a-select-option>
            </a-select>
          </a-form-item>
          <a-form-item label="备注" name="remark">
            <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="4" />
          </a-form-item>
        </a-form>
      </a-tab-pane>
      <a-tab-pane key="2" :tab="t('workflow.instance.tabs.designer')">
        <a-textarea v-model:value="formState.variables" placeholder="请输入流程变量（JSON格式）" :rows="10" />
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
import { getInstanceById, createInstance, updateInstance } from '@/api/workflow/instance'
import { useDictStore } from '@/stores/dict'
import HbtWorkflowConfig from './WorkflowConfig.vue'


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

// 重置表单
const resetForm = () => {
  formState.schemeId = 0
  formState.instanceTitle = ''
  formState.businessKey = ''
  formState.initiatorId = 0
  formState.priority = 2
  formState.urgency = 1
  formState.variables = ''
  formState.remark = ''
  formRef.value?.resetFields()
}

// 表单状态
const formState = reactive<HbtInstanceCreate>({
  schemeId: 0,
  instanceTitle: '',
  businessKey: '',
  initiatorId: 0,
  priority: 2,
  urgency: 1,
  variables: '',
  remark: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  instanceTitle: [
    { required: true, message: t('workflow.instance.fields.instanceTitle.required'), trigger: 'blur' }
  ],
  schemeId: [
    { required: true, message: t('workflow.instance.fields.schemeId.required'), trigger: 'change' }
  ],
  initiatorId: [
    { required: true, message: t('workflow.instance.fields.initiatorId.required'), trigger: 'change' }
  ]
}

// 获取实例详情
const getDetail = async (id: number) => {
  if (!id) {
    return
  }
  try {
    const res = await getInstanceById(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.schemeId = data.schemeId
      formState.instanceTitle = data.instanceTitle || ''
      formState.businessKey = data.businessKey || ''
      formState.initiatorId = data.initiatorId
      formState.priority = data.priority
      formState.urgency = data.urgency
      formState.variables = data.variables || ''
      formState.remark = data.remark || ''
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听表单打开状态
watch(() => props.open, (newVal) => {
  if (newVal) {
    // 表单打开时的初始化逻辑
  }
}, { immediate: true })

// 监听工作流实例ID变化
watch(() => props.instanceId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    resetForm()
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
      schemeId: formState.schemeId,
      instanceTitle: formState.instanceTitle,
      businessKey: formState.businessKey,
      initiatorId: formState.initiatorId,
      priority: formState.priority,
      urgency: formState.urgency,
      variables: formState.variables,
      remark: formState.remark
    }
    let res
    if (props.instanceId) {
      const updateData: HbtInstanceUpdate = {
        ...data,
        instanceId: props.instanceId
      }
      res = await updateInstance(updateData)
    } else {
      res = await createInstance(data)
    }
    if (res.data.code === 200) {
      message.success(t('common.success'))
      resetForm()
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
    resetForm()
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