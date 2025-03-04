<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: DictDataForm.vue
创建日期: 2024-03-20
描述: 字典数据表单组件
=================================================================== 
-->

<template>
  <hbt-modal
    :title="title"
    :visible="visible"
    :loading="loading"
    :width="500"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="字典类型" name="dictType">
        <a-input
          v-model:value="formState.dictType"
          :disabled="true"
        />
      </a-form-item>

      <a-form-item label="字典标签" name="dictLabel">
        <a-input
          v-model:value="formState.dictLabel"
          placeholder="请输入字典标签"
          :maxlength="100"
          allow-clear
        />
      </a-form-item>

      <a-form-item label="字典键值" name="dictValue">
        <a-input
          v-model:value="formState.dictValue"
          placeholder="请输入字典键值"
          :maxlength="100"
          allow-clear
        />
      </a-form-item>

      <a-form-item label="字典排序" name="dictSort">
        <a-input-number
          v-model:value="formState.dictSort"
          placeholder="请输入字典排序"
          :min="0"
          :max="999"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item label="状态" name="status">
        <hbt-select
          v-model:value="formState.status"
          :options="statusOptions"
          placeholder="请选择状态"
        />
      </a-form-item>

      <a-form-item label="样式属性" name="cssClass">
        <a-input
          v-model:value="formState.cssClass"
          placeholder="请输入样式属性"
          :maxlength="100"
          allow-clear
        />
      </a-form-item>

      <a-form-item label="回显样式" name="listClass">
        <a-input
          v-model:value="formState.listClass"
          placeholder="请输入回显样式"
          :maxlength="100"
          allow-clear
        />
      </a-form-item>

      <a-form-item label="是否默认" name="isDefault">
        <a-radio-group v-model:value="formState.isDefault">
          <a-radio :value="'Y'">是</a-radio>
          <a-radio :value="'N'">否</a-radio>
        </a-radio-group>
      </a-form-item>

      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          placeholder="请输入备注"
          :maxlength="500"
          :auto-size="{ minRows: 3, maxRows: 5 }"
          allow-clear
        />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { HbtDictData } from '@/types/admin/hbtDictData'

const { t } = useI18n()

// === Props 定义 ===
interface Props {
  visible: boolean
  title: string
  loading?: boolean
  model?: Partial<HbtDictData>
  dictType: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  loading: false,
  model: () => ({})
})

// === Emits 定义 ===
const emit = defineEmits<{
  'update:visible': [value: boolean]
  'ok': [values: HbtDictData]
  'cancel': []
}>()

// === 状态定义 ===
const formRef = ref<FormInstance>()
const formState = ref<Partial<HbtDictData>>({
  dictType: props.dictType,
  dictLabel: '',
  dictValue: '',
  dictSort: 0,
  status: 1,
  cssClass: '',
  listClass: '',
  isDefault: 'N',
  remark: ''
})

// === 状态选项 ===
const statusOptions = [
  { label: t('common.status.normal'), value: 1 },
  { label: t('common.status.disabled'), value: 0 }
]

// === 表单校验规则 ===
const rules = {
  dictLabel: [
    { required: true, message: '请输入字典标签', trigger: 'blur' },
    { min: 2, max: 100, message: '字典标签长度必须在2-100个字符之间', trigger: 'blur' }
  ],
  dictValue: [
    { required: true, message: '请输入字典键值', trigger: 'blur' },
    { min: 1, max: 100, message: '字典键值长度必须在1-100个字符之间', trigger: 'blur' }
  ],
  dictSort: [
    { required: true, message: '请输入字典排序', trigger: 'blur' }
  ],
  status: [
    { required: true, message: '请选择状态', trigger: 'change' }
  ]
}

// === 监听数据变化 ===
watch(
  () => props.model,
  (val) => {
    formState.value = { ...val }
  },
  { deep: true, immediate: true }
)

// === 方法定义 ===
// 确认
const handleOk = async () => {
  try {
    await formRef.value?.validate()
    emit('ok', formState.value as HbtDictData)
  } catch (error) {
    // 表单验证失败
  }
}

// 取消
const handleCancel = () => {
  formRef.value?.resetFields()
  emit('update:visible', false)
  emit('cancel')
}
</script> 