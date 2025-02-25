//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : DictDataForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据表单组件
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
      <a-form-item label="字典标签" name="dictLabel">
        <a-input v-model:value="formData.dictLabel" placeholder="请输入字典标签" />
      </a-form-item>
      <a-form-item label="字典键值" name="dictValue">
        <a-input v-model:value="formData.dictValue" placeholder="请输入字典键值" />
      </a-form-item>
      <a-form-item label="字典排序" name="orderNum">
        <a-input-number v-model:value="formData.orderNum" :min="0" style="width: 100%" />
      </a-form-item>
      <a-form-item label="样式属性" name="cssClass">
        <a-input v-model:value="formData.cssClass" placeholder="请输入样式属性" />
      </a-form-item>
      <a-form-item label="表格样式" name="listClass">
        <a-input v-model:value="formData.listClass" placeholder="请输入表格回显样式" />
      </a-form-item>
      <a-form-item label="是否默认" name="isDefault">
        <a-select v-model:value="formData.isDefault" placeholder="请选择">
          <a-select-option :value="0">否</a-select-option>
          <a-select-option :value="1">是</a-select-option>
        </a-select>
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
import type { HbtDictData, HbtDictDataCreate, HbtDictDataUpdate } from '@/types/admin/hbtDictData'

const props = defineProps<{
  visible: boolean
  title: string
  dictTypeId: number
  record?: HbtDictData
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'submit', data: HbtDictDataCreate | HbtDictDataUpdate): void
}>()

// 表单状态
const loading = ref(false)
const formRef = ref<FormInstance>()
const formData = reactive<HbtDictDataCreate>({
  dictTypeId: props.dictTypeId,
  dictLabel: '',
  dictValue: '',
  orderNum: 0,
  cssClass: '',
  listClass: '',
  isDefault: 0,
  status: 0,
  remark: ''
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  dictLabel: [
    { required: true, message: '请输入字典标签', trigger: 'blur', type: 'string' },
    { max: 100, message: '字典标签长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  dictValue: [
    { required: true, message: '请输入字典键值', trigger: 'blur', type: 'string' },
    { max: 100, message: '字典键值长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  orderNum: [
    { required: true, message: '请输入字典排序', trigger: 'blur', type: 'number' }
  ],
  cssClass: [
    { max: 100, message: '样式属性长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  listClass: [
    { max: 100, message: '表格回显样式长度不能超过100个字符', trigger: 'blur', type: 'string' }
  ],
  isDefault: [
    { required: true, message: '请选择是否默认', trigger: 'change', type: 'number' }
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
        dictTypeId: props.dictTypeId,
        dictLabel: '',
        dictValue: '',
        orderNum: 0,
        cssClass: '',
        listClass: '',
        isDefault: 0,
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
      ? { ...formData, dictDataId: props.record.dictDataId }
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