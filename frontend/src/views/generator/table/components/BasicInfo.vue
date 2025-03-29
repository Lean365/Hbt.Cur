<template>
  <div class="basic-info">
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="表名" name="tableName">
        <a-input v-model:value="formData.tableName" placeholder="请输入表名" />
      </a-form-item>
      <a-form-item label="表注释" name="tableComment">
        <a-input v-model:value="formData.tableComment" placeholder="请输入表注释" />
      </a-form-item>
      <a-form-item label="实体类名" name="className">
        <a-input v-model:value="formData.className" placeholder="请输入实体类名" />
      </a-form-item>
      <a-form-item label="作者" name="functionAuthor">
        <a-input v-model:value="formData.functionAuthor" placeholder="请输入作者" />
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formData.remark" placeholder="请输入备注" :rows="4" />
      </a-form-item>
    </a-form>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtGenTableDto } from '@/types/generator/table'

const props = defineProps<{
  modelValue: HbtGenTableDto
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: HbtGenTableDto): void
}>()

const formRef = ref<FormInstance>()

// 表单数据
const formData = reactive<HbtGenTableDto>({
  ...props.modelValue
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  tableName: [{ required: true, message: '请输入表名', trigger: 'blur' }],
  tableComment: [{ required: true, message: '请输入表注释', trigger: 'blur' }],
  className: [{ required: true, message: '请输入实体类名', trigger: 'blur' }],
  functionAuthor: [{ required: true, message: '请输入作者', trigger: 'blur' }]
}

// 监听表单数据变化
watch(
  () => formData,
  (newVal) => {
    emit('update:modelValue', newVal)
  },
  { deep: true }
)
</script>

<style lang="less" scoped>
.basic-info {
  padding: 24px;
}
</style> 