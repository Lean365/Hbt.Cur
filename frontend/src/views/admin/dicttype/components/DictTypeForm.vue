//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : DictTypeForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型表单组件
//===================================================================

<template>
  <a-modal
    :visible="visible"
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
      <a-form-item label="字典名称" name="dictName">
        <a-input v-model:value="formData.dictName" placeholder="请输入字典名称" />
      </a-form-item>
      <a-form-item label="字典类型" name="dictType">
        <a-input v-model:value="formData.dictType" placeholder="请输入字典类型" />
      </a-form-item>
      <a-form-item label="字典类别" name="dictCategory">
        <a-select v-model:value="formData.dictCategory" placeholder="请选择">
          <a-select-option :value="0">系统</a-select-option>
          <a-select-option :value="1">SQL</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="SQL脚本" name="sqlScript" v-if="formData.dictCategory === 1">
        <a-textarea v-model:value="formData.sqlScript" placeholder="请输入SQL脚本" :rows="4" />
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
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { HbtDictType, HbtDictTypeCreate, HbtDictTypeUpdate } from '@/types/admin/hbtDictType'

const props = defineProps<{
  visible: boolean
  title: string
  record?: HbtDictType
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'submit', data: HbtDictTypeCreate | HbtDictTypeUpdate): void
}>()

// 表单状态
const loading = ref(false)
const formRef = ref<FormInstance>()
const formData = reactive<HbtDictTypeCreate>({
  dictName: '',
  dictType: '',
  dictCategory: 0,
  sqlScript: '',
  orderNum: 0,
  status: 0
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  dictName: [
    { required: true, message: '请输入字典名称', trigger: 'blur', type: 'string' },
    { max: 100, message: '字典名称长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  dictType: [
    { required: true, message: '请输入字典类型', trigger: 'blur', type: 'string' },
    { max: 100, message: '字典类型长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  dictCategory: [
    { required: true, message: '请选择字典类别', trigger: 'change', type: 'number' }
  ],
  sqlScript: [
    { max: 500, message: 'SQL脚本长度不能超过500个字符', trigger: 'blur', type: 'string' }
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
        dictName: '',
        dictType: '',
        dictCategory: 0,
        sqlScript: '',
        orderNum: 0,
        status: 0
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
      ? { ...formData, dictTypeId: props.record.dictTypeId }
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