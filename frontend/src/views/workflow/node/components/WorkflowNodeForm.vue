<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="600"
    :loading="loading"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item label="节点名称" name="NodeName">
        <a-input v-model:value="formState.NodeName" placeholder="请输入节点名称" />
      </a-form-item>
      <a-form-item label="节点类型" name="nodeType">
        <hbt-dict-select
          v-model:value="formState.nodeType"
          dict-type="workflow_node_type"
          placeholder="请选择节点类型"
        />
      </a-form-item>
      <a-form-item label="节点配置" name="nodeConfig">
        <a-textarea v-model:value="formState.nodeConfig" placeholder="请输入节点配置" :rows="4" />
      </a-form-item>
      <a-form-item label="排序号" name="orderNum">
        <a-input-number v-model:value="formState.orderNum" placeholder="请输入排序号" />
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="2" />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { getWorkflowNode, createWorkflowNode, updateWorkflowNode } from '@/api/workflow/workflowNode'
import type { HbtWorkflowNode, HbtWorkflowNodeCreate, HbtWorkflowNodeUpdate } from '@/types/workflow/workflowNode'

const props = defineProps<{
  visible: boolean
  title: string
  nodeId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)

const formState = reactive<HbtWorkflowNodeCreate>({
  NodeName: '',
  nodeType: 0,
  workflowDefinitionId: 0,
  parentNodeId: undefined,
  nodeConfig: '',
  orderNum: 0,
  remark: ''
})

const rules: Record<string, any[]> = {
  NodeName: [
    { required: true, message: t('workflow.node.fields.nodeName.required'), trigger: 'blur' }
  ],
  nodeType: [
    { required: true, message: t('workflow.node.fields.nodeType.required'), trigger: 'change' }
  ],
  nodeConfig: [
    { required: true, message: t('workflow.node.fields.nodeConfig.required'), trigger: 'blur' }
  ],
  orderNum: [
    { required: true, message: t('workflow.node.fields.orderNum.required'), trigger: 'change' }
  ],
  remark: [
    { required: false, message: '', trigger: 'blur' }
  ]
}

// 获取节点详情
const getDetail = async (id: number) => {
  try {
    const res = await getWorkflowNode(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.NodeName = data.NodeName || ''
      formState.nodeType = data.nodeType
      formState.workflowDefinitionId = data.workflowDefinitionId
      formState.parentNodeId = data.parentNodeId
      formState.nodeConfig = data.nodeConfig || ''
      formState.orderNum = data.orderNum
      formState.remark = data.remark || ''
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听节点ID变化
watch(() => props.nodeId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    formState.NodeName = ''
    formState.nodeType = 0
    formState.workflowDefinitionId = 0
    formState.parentNodeId = undefined
    formState.nodeConfig = ''
    formState.orderNum = 0
    formState.remark = ''
  }
}, { immediate: true })

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    const data: HbtWorkflowNodeCreate = {
      NodeName: formState.NodeName,
      nodeType: formState.nodeType,
      workflowDefinitionId: formState.workflowDefinitionId,
      parentNodeId: formState.parentNodeId,
      nodeConfig: formState.nodeConfig,
      orderNum: formState.orderNum,
      remark: formState.remark
    }
    let res
    if (props.nodeId) {
      const updateData: HbtWorkflowNodeUpdate = {
        ...data,
        id: String(props.nodeId)
      }
      res = await updateWorkflowNode(updateData)
    } else {
      res = await createWorkflowNode(data)
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
const handleCancel = () => {
  emit('update:visible', false)
  formRef.value?.resetFields()
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:visible', value)
}
</script> 