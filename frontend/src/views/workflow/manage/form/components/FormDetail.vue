<template>
  <hbt-form-dialog
    v-model:open="dialogVisible"
    title="表单详情"
    :loading="loading"
    :show-footer="false"
    @cancel="handleCancel"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item label="表单ID">
        {{ formData.formId }}
      </a-descriptions-item>
      <a-descriptions-item label="表单名称">
        {{ formData.formName }}
      </a-descriptions-item>
      <a-descriptions-item label="表单键">
        {{ formData.formKey }}
      </a-descriptions-item>
      <a-descriptions-item label="表单分类">
        <hbt-dict-tag dict-type="workflow_form_category" :value="formData.formCategory" />
      </a-descriptions-item>
      <a-descriptions-item label="表单类型">
        <hbt-dict-tag dict-type="workflow_form_type" :value="formData.formType" />
      </a-descriptions-item>
      <a-descriptions-item label="版本">
        {{ formData.version }}
      </a-descriptions-item>
      <a-descriptions-item label="状态">
        <hbt-dict-tag dict-type="workflow_form_status" :value="formData.status" />
      </a-descriptions-item>
      <a-descriptions-item label="创建时间">
        {{ formData.createTime }}
      </a-descriptions-item>
      <a-descriptions-item label="表单描述" :span="2">
        {{ formData.description || '暂无描述' }}
      </a-descriptions-item>
      <a-descriptions-item label="表单配置" :span="2">
        <div class="form-config-preview">
          <hbt-form
            v-if="formData.formConfig"
            :model-value="formData.formConfig"
            :readonly="true"
            :height="'300px'"
          />
          <a-empty v-else description="暂无表单配置" />
        </div>
      </a-descriptions-item>
    </a-descriptions>
  </hbt-form-dialog>
</template>

<script setup lang="ts">
import { ref, reactive, computed, watch } from 'vue'
import { message } from 'ant-design-vue'
import { getFormById } from '@/api/workflow/form'
import type { HbtForm } from '@/types/workflow/form'

// Props
interface Props {
  open: boolean
  formId?: number
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  formId: undefined
})

// Emits
const emit = defineEmits<{
  'update:open': [value: boolean]
}>()

// 响应式数据
const loading = ref(false)

// 表单数据
const formData = reactive<HbtForm>({
  formId: 0,
  formKey: '',
  formName: '',
  formCategory: 1,
  formType: 1,
  version: '1.0',
  status: 0,
  formConfig: '',
  description: '',
  createTime: '',
  updateTime: '',
  createBy: '',
  updateBy: '',
  isDeleted: 0
})

// 计算属性
const dialogVisible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

// 方法
const loadFormData = async () => {
  if (!props.formId) return
  
  loading.value = true
  try {
    const result = await getFormById(props.formId)
    if (result.data.code === 200) {
      Object.assign(formData, result.data.data)
    }
  } catch (error) {
    console.error('加载表单数据失败:', error)
    message.error('加载表单数据失败')
  } finally {
    loading.value = false
  }
}

const resetForm = () => {
  Object.assign(formData, {
    formId: 0,
    formKey: '',
    formName: '',
    formCategory: 1,
    formType: 1,
    version: '1.0',
    status: 0,
    formConfig: '',
    description: '',
    createTime: '',
    updateTime: '',
    createBy: '',
    updateBy: '',
    isDeleted: 0
  })
}

const handleCancel = () => {
  dialogVisible.value = false
  resetForm()
}

// 监听器
watch(() => props.open, (newVal) => {
  if (newVal && props.formId) {
    loadFormData()
  } else if (!newVal) {
    resetForm()
  }
})
</script>

<style lang="less" scoped>
.form-config-preview {
  .hbt-form {
    border: 1px solid #d9d9d9;
    border-radius: 4px;
  }
}
</style> 