//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : LanguageForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言表单组件
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
      <a-form-item label="语言代码" name="langCode">
        <a-input v-model:value="formData.langCode" placeholder="请输入语言代码" />
      </a-form-item>
      <a-form-item label="语言名称" name="langName">
        <a-input v-model:value="formData.langName" placeholder="请输入语言名称" />
      </a-form-item>
      <a-form-item label="排序号" name="orderNum">
        <a-input-number v-model:value="formData.orderNum" :min="0" style="width: 100%" />
      </a-form-item>
      <a-form-item label="是否默认" name="isDefault">
        <a-switch v-model:checked="formData.isDefault" />
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-select v-model:value="formData.status" placeholder="请选择">
          <a-select-option :value="0">正常</a-select-option>
          <a-select-option :value="1">停用</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formData.remark" placeholder="请输入备注" :rows="4" />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { HbtLanguage, HbtLanguageCreate, HbtLanguageUpdate } from '@/types/admin/language'

const props = defineProps<{
  visible: boolean
  title: string
  record?: HbtLanguage
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'submit', data: HbtLanguageCreate | HbtLanguageUpdate): void
}>()

// 表单状态
const loading = ref(false)
const formRef = ref<FormInstance>()
const formData = reactive<HbtLanguageCreate>({
  langCode: '',
  langName: '',
  orderNum: 0,
  isDefault: false,
  status: 0,
  remark: ''
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  langCode: [
    { required: true, message: '请输入语言代码', trigger: 'blur', type: 'string' },
    { max: 10, message: '语言代码长度不能超过10个字符', trigger: 'blur', type: 'string' }
  ],
  langName: [
    { required: true, message: '请输入语言名称', trigger: 'blur', type: 'string' },
    { max: 50, message: '语言名称长度不能超过50个字符', trigger: 'blur', type: 'string' }
  ],
  orderNum: [
    { required: true, message: '请输入排序号', trigger: 'blur', type: 'number' }
  ],
  isDefault: [
    { required: true, message: '请选择是否默认', trigger: 'change', type: 'boolean' }
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
        langCode: '',
        langName: '',
        orderNum: 0,
        isDefault: false,
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
      ? { ...formData, id: props.record.id }
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