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
      <a-form-item label="实例ID" name="instanceId">
        <a-input-number v-model:value="formState.instanceId" placeholder="请输入实例ID" style="width: 100%" />
      </a-form-item>
      <a-form-item label="节点ID" name="nodeId">
        <a-input v-model:value="formState.nodeId" placeholder="请输入节点ID" />
      </a-form-item>
      <a-form-item label="节点名称" name="nodeName">
        <a-input v-model:value="formState.nodeName" placeholder="请输入节点名称" />
      </a-form-item>
      <a-form-item label="操作类型" name="operType">
        <hbt-select
          v-model:value="formState.operType"
          dict-type="workflow_oper_type"
          placeholder="请选择操作类型"
        />
      </a-form-item>
      <a-form-item label="操作人ID" name="operatorId">
        <a-input-number v-model:value="formState.operatorId" placeholder="请输入操作人ID" style="width: 100%" />
      </a-form-item>
      <a-form-item label="操作人姓名" name="operatorName">
        <a-input v-model:value="formState.operatorName" placeholder="请输入操作人姓名" />
      </a-form-item>
      <a-form-item label="操作意见" name="operOpinion">
        <a-textarea v-model:value="formState.operOpinion" placeholder="请输入操作意见" :rows="3" />
      </a-form-item>
      <a-form-item label="操作数据" name="operData">
        <a-textarea v-model:value="formState.operData" placeholder="请输入操作数据(JSON格式)" :rows="4" />
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
import { getInstanceOperById, createInstanceOper, updateInstanceOper } from '@/api/workflow/oper'
import type { HbtInstanceOper, HbtInstanceOperCreate, HbtInstanceOperUpdate } from '@/types/workflow/oper'

const props = defineProps<{
  open: boolean
  title: string
  instanceOperId?: number
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

// 重置表单
const resetForm = () => {
  formState.instanceId = 0
  formState.nodeId = ''
  formState.nodeName = ''
  formState.operType = 1
  formState.operatorId = 0
  formState.operatorName = ''
  formState.operOpinion = ''
  formState.operData = ''
  formState.remark = ''
  formRef.value?.resetFields()
}

const formState = reactive<HbtInstanceOperCreate>({
  instanceId: 0,
  nodeId: '',
  nodeName: '',
  operType: 1,
  operatorId: 0,
  operatorName: '',
  operOpinion: '',
  operData: '',
  remark: ''
})

const rules: Record<string, any[]> = {
  instanceId: [
    { required: true, message: t('workflow.oper.fields.instanceId.required'), trigger: 'change' }
  ],
  nodeName: [
    { required: true, message: t('workflow.oper.fields.nodeName.required'), trigger: 'blur' }
  ],
  operType: [
    { required: true, message: t('workflow.oper.fields.operType.required'), trigger: 'change' }
  ],
  operatorId: [
    { required: true, message: t('workflow.oper.fields.operatorId.required'), trigger: 'change' }
  ],
  operatorName: [
    { required: true, message: t('workflow.oper.fields.operatorName.required'), trigger: 'blur' }
  ]
}

// 获取操作记录详情
const getDetail = async (id: number) => {
  try {
    const res = await getInstanceOperById(id)
    if (res.data.code === 200) {
      const data = res.data.data
      formState.instanceId = data.instanceId
      formState.nodeId = data.nodeId || ''
      formState.nodeName = data.nodeName || ''
      formState.operType = data.operType
      formState.operatorId = data.operatorId
      formState.operatorName = data.operatorName
      formState.operOpinion = data.operOpinion || ''
      formState.operData = data.operData || ''
      formState.remark = data.remark || ''
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听操作记录ID变化
watch(() => props.instanceOperId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    resetForm()
  }
}, { immediate: true })

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    const data: HbtInstanceOperCreate = {
      instanceId: formState.instanceId,
      nodeId: formState.nodeId,
      nodeName: formState.nodeName,
      operType: formState.operType,
      operatorId: formState.operatorId,
      operatorName: formState.operatorName,
      operOpinion: formState.operOpinion,
      operData: formState.operData,
      remark: formState.remark
    }
    let res
    if (props.instanceOperId) {
      const updateData: HbtInstanceOperUpdate = {
        ...data,
        instanceOperId: props.instanceOperId
      }
      res = await updateInstanceOper(updateData)
    } else {
      res = await createInstanceOper(data)
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
const handleCancel = () => {
  emit('update:open', false)
  resetForm()
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:open', value)
}
</script> 