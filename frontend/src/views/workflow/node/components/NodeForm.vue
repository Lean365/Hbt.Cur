<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="600"
    :loading="loading"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item label="节点名称" name="nodeName">
        <a-input v-model:value="formState.nodeName" placeholder="请输入节点名称" />
      </a-form-item>
      <a-form-item label="节点类型" name="nodeType">
        <hbt-select
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
      <a-form-item label="状态" name="status">
        <hbt-select
          v-model:value="formState.status"
          dict-type="workflow_node_status"
          placeholder="请选择状态"
        />
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="2" />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { getWorkflowNode, createWorkflowNode, updateWorkflowNode } from '@/api/workflow/node'
import type { HbtNode, HbtNodeCreate, HbtNodeUpdate } from '@/types/workflow/node'

const props = defineProps<{
  open: boolean
  title: string
  nodeId?: number
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

const formState = reactive<HbtNodeCreate>({
  nodeName: '',
  nodeType: 0,
  definitionId: 0,
  parentNodeId: undefined,
  nodeConfig: '',
  orderNum: 0,
  remark: '',
  instanceId: 0,
  status: 0,
  startTime: '',
  endTime: ''
})

const rules: Record<string, any[]> = {
  nodeName: [
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
      formState.nodeName = data.nodeName || ''
      formState.nodeType = data.nodeType
      formState.definitionId = data.definitionId
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
    formState.nodeName = ''
    formState.nodeType = 0
    formState.definitionId = 0
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
    const data: HbtNodeCreate = {
      nodeName: formState.nodeName,
      nodeType: formState.nodeType,
      definitionId: formState.definitionId,
      parentNodeId: formState.parentNodeId,
      nodeConfig: formState.nodeConfig,
      orderNum: formState.orderNum,
      remark: formState.remark,
      instanceId: formState.instanceId,
      status: formState.status,
      startTime: formState.startTime,
      endTime: formState.endTime
    }
    let res
    if (props.nodeId) {
      const updateData: HbtNodeUpdate = {
        ...data,
        nodeId: props.nodeId
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
  emit('update:open', false)
  formRef.value?.resetFields()
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:open', value)
}
</script> 