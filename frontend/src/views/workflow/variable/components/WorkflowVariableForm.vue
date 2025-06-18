<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="600"
    :loading="loading"
    @update:open="handleVisibleChange"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item label="变量名称" name="variableName">
        <a-input v-model:value="formState.variableName" placeholder="请输入变量名称" />
      </a-form-item>
      <a-form-item label="变量类型" name="variableType">
        <a-select v-model:value="formState.variableType" placeholder="请选择变量类型">
          <a-select-option value="string">字符串</a-select-option>
          <a-select-option value="number">数字</a-select-option>
          <a-select-option value="boolean">布尔值</a-select-option>
          <a-select-option value="date">日期</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="变量值" name="variableValue">
        <a-input v-model:value="formState.variableValue" placeholder="请输入变量值" />
      </a-form-item>
      <a-form-item label="作用域" name="scope">
        <a-radio-group v-model:value="formState.scope">
          <a-radio :value="1">全局</a-radio>
          <a-radio :value="2">节点</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-input v-model:value="formState.remark" placeholder="请输入备注" />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { getWorkflowVariable, createWorkflowVariable, updateWorkflowVariable } from '@/api/workflow/variable'
import type { HbtWorkflowVariableCreate, HbtWorkflowVariableUpdate } from '@/types/workflow/variable'
import type { Rule } from 'ant-design-vue/es/form'

const props = defineProps<{
  visible: boolean
  title: string
  variableId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)

const formState = reactive<HbtWorkflowVariableCreate>({
  workflowInstanceId: 0,
  variableName: '',
  variableType: 'string',
  variableValue: '',
  scope: 1,
  nodeId: undefined,
  remark: ''
})

const rules: Record<string, Rule[]> = {
  variableName: [
    { required: true, message: '请输入变量名称', trigger: 'blur', type: 'string' }
  ],
  variableType: [
    { required: true, message: '请选择变量类型', trigger: 'change', type: 'string' }
  ],
  variableValue: [
    { required: true, message: '请输入变量值', trigger: 'blur', type: 'string' }
  ],
  scope: [
    { required: true, message: '请选择作用域', trigger: 'change', type: 'number' }
  ]
}

// 获取工作流变量详情
const getDetail = async (id: number) => {
  try {
    const res = await getWorkflowVariable(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.workflowInstanceId = data.workflowInstanceId
      formState.variableName = data.variableName
      formState.variableType = data.variableType
      formState.variableValue = data.variableValue
      formState.scope = data.scope
      formState.nodeId = data.nodeId
      formState.remark = data.remark || ''
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听ID变化
watch(() => props.variableId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    formState.variableName = ''
    formState.variableType = 'string'
    formState.variableValue = ''
    formState.scope = 1
    formState.nodeId = undefined
    formState.remark = ''
  }
}, { immediate: true })

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    let res
    if (props.variableId) {
      res = await updateWorkflowVariable({
        ...formState,
        id: String(props.variableId)
      })
    } else {
      res = await createWorkflowVariable(formState)
    }

    if (res.data.code === 200) {
      message.success(t('common.success'))
      emit('success')
      handleCancel()
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
const handleCancel = () => {
  emit('update:visible', false)
  formRef.value?.resetFields()
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:visible', value)
}
</script> 