//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ConfigForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置表单组件
//===================================================================

<template>
  <a-modal
    :open="visible"
    :title="title"
    :confirm-loading="loading"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="配置名称" name="configName">
        <a-input v-model:value="formData.configName" placeholder="请输入配置名称" />
      </a-form-item>
      <a-form-item label="配置键名" name="configKey">
        <a-input v-model:value="formData.configKey" placeholder="请输入配置键名" />
      </a-form-item>
      <a-form-item label="配置键值" name="configValue">
        <a-input v-model:value="formData.configValue" placeholder="请输入配置键值" />
      </a-form-item>
      <a-form-item label="系统内置" name="configBuiltin">
        <a-select v-model:value="formData.configBuiltin" placeholder="请选择">
          <a-select-option :value="0">否</a-select-option>
          <a-select-option :value="1">是</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="排序号" name="orderNum">
        <a-input-number v-model:value="formData.orderNum" :min="0" style="width: 100%" />
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-select v-model:value="formData.status" placeholder="请选择">
          <a-select-option :value="0">正常</a-select-option>
          <a-select-option :value="1">停用</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formData.remark" placeholder="请输入备注" />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { HbtConfig, HbtConfigCreate, HbtConfigUpdate } from '@/types/admin/hbtConfig'

const props = defineProps<{
  visible: boolean
  title: string
  record?: HbtConfig
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'submit', data: HbtConfigCreate | HbtConfigUpdate): void
}>()

// 表单状态
const loading = ref(false)
const formRef = ref<FormInstance>()
const formData = reactive<HbtConfigCreate>({
  configName: '',
  configKey: '',
  configValue: '',
  configBuiltin: 0,
  orderNum: 0,
  status: 0,
  remark: ''
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  configName: [
    { required: true, message: '请输入配置名称', trigger: 'blur', type: 'string' },
    { max: 100, message: '配置名称长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  configKey: [
    { required: true, message: '请输入配置键名', trigger: 'blur', type: 'string' },
    { max: 100, message: '配置键名长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  configValue: [
    { required: true, message: '请输入配置键值', trigger: 'blur', type: 'string' },
    { max: 500, message: '配置键值长度不能超过500个字符', trigger: 'blur', type: 'string' }
  ],
  configBuiltin: [
    { required: true, message: '请选择是否系统内置', trigger: 'change', type: 'number' }
  ],
  orderNum: [
    { required: true, message: '请输入排序号', trigger: 'blur', type: 'number' }
  ],
  status: [
    { required: true, message: '请选择状态', trigger: 'change', type: 'number' }
  ]
}

// 监听record变化
watch(
  () => props.record,
  (record) => {
    if (record) {
      Object.assign(formData, record)
    } else {
      Object.assign(formData, {
        configName: '',
        configKey: '',
        configValue: '',
        configBuiltin: 0,
        orderNum: 0,
        status: 0,
        remark: ''
      })
    }
  }
)

// 确定处理
const handleOk = async () => {
  try {
    loading.value = true
    await formRef.value?.validate()
    
    const submitData = props.record
      ? { ...formData, configId: props.record.configId }
      : formData
      
    emit('submit', submitData)
  } catch (error) {
    // 校验失败
    console.error('表单验证失败:', error)
  } finally {
    loading.value = false
  }
}

// 取消处理
const handleCancel = () => {
  formRef.value?.resetFields()
  emit('update:visible', false)
}
</script>

<style scoped>
.ant-form {
  padding: 24px 0;
}

.ant-form-item {
  margin-bottom: 24px;
}

.ant-input-number {
  width: 100%;
}
</style> 