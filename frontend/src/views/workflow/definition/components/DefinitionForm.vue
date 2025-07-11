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
    
    <!-- 步骤条 -->
    <a-steps :current="currentStep" class="form-steps">
      <a-step title="基本信息" description="填写工作流基本信息" />
      <a-step title="选择表单" description="选择关联的表单" />
      <a-step title="流程设计" description="设计工作流程图" />
    </a-steps>

    <!-- 步骤内容 -->
    <div class="step-content">
      <!-- 第一步：基本信息 -->
      <div v-show="currentStep === 0" class="step-panel">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 4 }"
          :wrapper-col="{ span: 20 }"
        >
          <a-form-item label="工作流名称" name="workflowName">
            <a-input v-model:value="formState.workflowName" placeholder="请输入工作流名称" />
          </a-form-item>
          <a-form-item label="工作流类型" name="workflowCategory">
            <hbt-select v-model:value="formState.workflowCategory" dict-type="workflow_category" type="select" 
            :show-all="false" :placeholder="t('generator.config.placeholder.workflowCategory')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="版本" name="workflowVersion">
            <hbt-select v-model:value="formState.workflowVersion" dict-type="workflow_version" type="select" 
            :show-all="false" :placeholder="t('generator.config.placeholder.workflowVersion')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="状态" name="status">
            <hbt-select v-model:value="formState.status" dict-type="workflow_status" type="select" 
            :show-all="false" :placeholder="t('generator.config.placeholder.status')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="备注" name="remark">
            <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="4" />
          </a-form-item>
        </a-form>
      </div>

      <!-- 第二步：选择表单 -->
      <div v-show="currentStep === 1" class="step-panel">
        <div class="form-selection-container">
          <!-- 左侧表单列表 -->
          <div class="form-list-panel">
            <h4>可用表单列表</h4>
            <div class="form-list">
              <div
                v-for="form in formOptions"
                :key="form.value"
                class="form-item"
                :class="{ active: formState.formId === form.value }"
                @click="selectForm(form.value)"
              >
                <div class="form-item-content">
                  <div class="form-name">{{ form.label }}</div>
                  <div class="form-id">ID: {{ form.value }}</div>
                </div>
              </div>
            </div>
          </div>

          <!-- 右侧表单预览 -->
          <div class="form-preview-panel">
            <div class="preview-header">
              <h4>表单预览</h4>
              <a-button v-if="formState.formId" type="primary" size="small" @click="confirmFormSelection">
                确认选择
              </a-button>
            </div>
            <div class="preview-content">
              <div v-if="!formState.formId" class="no-selection">
                <a-empty description="请从左侧选择一个表单进行预览" />
              </div>
              <div v-else-if="loadingForm" class="loading-form">
                <a-spin tip="加载表单配置中..." />
              </div>
              <div v-else class="form-preview-wrapper">
                <hbt-form 
                  :model-value="selectedFormConfig"
                  :readonly="true"
                  :height="'600px'"
                />
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- 第三步：流程设计 -->
      <div v-show="currentStep === 2" class="step-panel">
        <div class="flow-canvas-wrapper">
          <hbt-flow v-model:value="formState.workflowConfig" :width="1600" :height="800" />
        </div>
      </div>
    </div>

    <!-- 步骤操作按钮 -->
    <template #footer>
      <div class="step-actions">
        <a-button v-if="currentStep > 0" @click="prevStep">
          上一步
        </a-button>
        <a-button v-if="currentStep < 2" type="primary" @click="nextStep">
          下一步
        </a-button>
        <a-button v-if="currentStep === 2" type="primary" @click="handleSubmit" :loading="loading">
          保存
        </a-button>
        <a-button @click="handleVisibleChange(false)">
          取消
        </a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { FullscreenOutlined, FullscreenExitOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import { getWorkflowDefinition, createWorkflowDefinition, updateWorkflowDefinition } from '@/api/workflow/definition'
import type { HbtDefinition, HbtDefinitionCreate, HbtDefinitionUpdate } from '@/types/workflow/definition'
import HbtFlow from '@/components/Business/HbtFlow/index.vue'
import HbtForm from '@/components/Business/HbtForm/index.vue'

import { getFormOptions, getForm } from '@/api/workflow/form'

const props = defineProps<{
  open: boolean
  title: string
  definitionId?: number
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
const formOptions = ref<{ label: string; value: number }[]>([])
const currentStep = ref(0)
const isFullscreen = ref(false)
const loadingForm = ref(false)
const selectedFormConfig = ref('')

const formState = reactive<HbtDefinitionCreate>({
  workflowName: '',
  workflowCategory: '',
  workflowVersion: 'A',
  formId: 0,
  workflowConfig: {},
  status: 0,
  remark: ''
})

const rules: Record<string, any[]> = {
  workflowName: [
    { required: true, message: t('workflow.definition.fields.workflowName.required'), trigger: 'blur' }
  ],
  workflowCategory: [
    { required: true, message: t('workflow.definition.fields.workflowCategory.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('workflow.definition.fields.status.required'), trigger: 'change' }
  ]
}

// 下一步
const nextStep = async () => {
  try {
    if (currentStep.value === 0) {
      await formRef.value?.validate()
    } else if (currentStep.value === 1) {
      // 检查是否选择了表单
      if (!formState.formId) {
        message.error('请选择一个表单')
        return
      }
    }
    currentStep.value++
  } catch (error) {
    console.error('表单验证失败:', error)
  }
}

// 上一步
const prevStep = () => {
  currentStep.value--
}

// 获取工作流定义详情
const getDetail = async (id: number) => {
  if (!id) {
    return
  }
  try {
    const res = await getWorkflowDefinition(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.workflowName = data.workflowName || ''
      formState.workflowCategory = data.workflowCategory || ''
      formState.workflowVersion = data.workflowVersion
      formState.formId = data.formId || 0
      formState.workflowConfig = data.workflowConfig || {}
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
    currentStep.value = 0
  }
}, { immediate: true })

// 监听工作流定义ID变化
watch(() => props.definitionId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    // 重置表单状态
    formState.workflowName = ''
    formState.workflowCategory = ''
    formState.workflowVersion = 'A'
    formState.formId = 0
    formState.workflowConfig = {}
    formState.status = 0
    formState.remark = ''
    currentStep.value = 0
  }
}, { immediate: true })

// 监听 workflowConfig 变化
watch(() => formState.workflowConfig, (newVal) => {
  console.log('workflowConfig 发生变化:', newVal)
}, { deep: true })

// 切换全屏
const toggleFullscreen = () => {
  isFullscreen.value = !isFullscreen.value
}

// 提交表单
const handleSubmit = async () => {
  try {
    loading.value = true
    console.log('提交前的 workflowConfig:', formState.workflowConfig)
    
    // 保证 workflowConfig 为字符串
    let workflowConfigStr = formState.workflowConfig
    if (typeof workflowConfigStr !== 'string') {
      workflowConfigStr = JSON.stringify(workflowConfigStr)
    }
    console.log('转换后的 workflowConfig:', workflowConfigStr)
    
    const data: HbtDefinitionCreate = {
      workflowName: formState.workflowName,
      workflowCategory: formState.workflowCategory,
      workflowVersion: formState.workflowVersion,
      formId: formState.formId,
      workflowConfig: workflowConfigStr,
      status: formState.status,
      remark: formState.remark
    }
    console.log('最终提交的数据:', data)
    
    let res
    if (props.definitionId) {
      const updateData: HbtDefinitionUpdate = {
        ...data,
        definitionId: props.definitionId
      }
      res = await updateWorkflowDefinition(updateData)
    } else {
      res = await createWorkflowDefinition(data)
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
    formState.workflowName = ''
    formState.workflowCategory = ''
    formState.workflowVersion = 'A'
    formState.formId = 0
    formState.workflowConfig = {}
    formState.status = 0
    formState.remark = ''
    currentStep.value = 0
  }
}

// 选择表单
const selectForm = async (formId: number) => {
  formState.formId = formId
  await loadFormConfig(formId)
}

// 加载表单配置
const loadFormConfig = async (formId: number) => {
  if (!formId) return
  
  loadingForm.value = true
  try {
    // 调用API获取表单配置
    const response = await getForm(formId)
    if (response.data.code === 200) {
      const formData = response.data.data
      console.log('表单数据:', formData)
      
      // 确保formConfig存在且格式正确
      if (formData.formConfig) {
        try {
          const config = JSON.parse(formData.formConfig)
          console.log('解析后的配置:', config)
          selectedFormConfig.value = formData.formConfig
        } catch (parseError) {
          console.error('解析表单配置失败:', parseError)
          // 如果解析失败，直接使用原始字符串
          selectedFormConfig.value = formData.formConfig
        }
      } else {
        console.warn('表单配置为空')
        selectedFormConfig.value = ''
      }
    } else {
      message.error(response.data.msg || '获取表单配置失败')
    }
  } catch (error) {
    console.error('获取表单配置失败:', error)
    message.error('获取表单配置失败')
  } finally {
    loadingForm.value = false
  }
}

// 确认表单选择
const confirmFormSelection = () => {
  if (formState.formId) {
    message.success('表单选择已确认')
  }
}
</script>

<style lang="less" scoped>
.form-steps {
  margin-bottom: 24px;
}

.step-content {
  min-height: 400px;
}

.step-panel {
  padding: 24px 0;
}

.form-preview {
  margin-top: 24px;
  padding: 16px;
  background: #f5f5f5;
  border-radius: 6px;
  
  h4 {
    margin-bottom: 12px;
    color: #333;
  }
  
  .preview-content {
    color: #666;
  }
}

.step-actions {
  display: flex;
  justify-content: space-between;
  align-items: center;
  
  .ant-btn {
    margin-left: 8px;
  }
}

.flow-canvas-wrapper {
  width: 100%;
  min-width: 1200px;
  height: 80vh;
  min-height: 600px;
  background: #fff;
  box-sizing: border-box;
  border: 1px solid #e1e5e9;
  border-radius: 4px;
  overflow: hidden;
  display: flex;
  align-items: stretch;
  justify-content: flex-start;
}

.form-selection-container {
  display: flex;
  gap: 16px;
  height: 600px;
}

.form-list-panel {
  flex: 0 0 33.33%;
  padding: 12px;
  background: #fff;
  border-radius: 6px;
  box-sizing: border-box;
  border: 1px solid #e1e5e9;
  display: flex;
  flex-direction: column;

  h4 {
    margin-bottom: 8px;
    color: #333;
    font-size: 14px;
  }
}

.form-list {
  display: flex;
  flex-direction: column;
  gap: 4px;
  flex: 1;
  overflow-y: auto;
}

.form-item {
  display: flex;
  align-items: center;
  padding: 8px 10px;
  background: #f5f5f5;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.2s ease;
  border: 1px solid transparent;

  &:hover {
    background: #e6f7ff;
    border-color: #91d5ff;
  }

  &.active {
    background: #e6f7ff;
    border-color: #1890ff;
  }

  .form-item-content {
    flex: 1;

    .form-name {
      font-weight: 500;
      color: #333;
      margin-bottom: 2px;
      font-size: 13px;
      line-height: 1.2;
    }

    .form-id {
      font-size: 11px;
      color: #666;
      line-height: 1.2;
    }
  }
}

.form-preview-panel {
  flex: 0 0 66.67%;
  padding: 12px;
  background: #fff;
  border-radius: 6px;
  box-sizing: border-box;
  border: 1px solid #e1e5e9;
  display: flex;
  flex-direction: column;

  .preview-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 8px;

    h4 {
      color: #333;
      font-size: 14px;
    }
  }
}

.preview-content {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 0;

  .no-selection {
    text-align: center;
  }

  .loading-form {
    text-align: center;
    padding: 40px;
  }

  .form-preview-wrapper {
    width: 100%;
    height: 100%;
    padding: 8px;
    background: #fff;
    border-radius: 4px;
    box-sizing: border-box;
    border: 1px solid #e1e5e9;
    overflow: auto;
  }
}
</style>
