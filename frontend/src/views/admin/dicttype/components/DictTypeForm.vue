<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: DictTypeForm.vue
创建日期: 2024-03-20
描述: 字典类型表单对话框组件
=================================================================== 
-->

<template>
  <hbt-modal
    :open="open"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="(val) => emit('update:open', val)"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 8 }"
      :wrapper-col="{ span: 14 }"
    >
      <a-tabs v-model:activeKey="activeTab">
        <a-tab-pane key="basic" :tab="t('common.tab.basicInfo')">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('dictType.form.name')" name="dictName">
                <a-input
                  v-model:value="formState.dictName"
                  :placeholder="t('dictType.form.namePlaceholder')"
                  :maxlength="100"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('dictType.form.type')" name="dictType">
                <a-input
                  v-model:value="formState.dictType"
                  :placeholder="t('dictType.form.typePlaceholder')"
                  :maxlength="100"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('dictType.form.category')" name="dictCategory">
                <hbt-select
                  v-model:value="formState.dictCategory"
                  :options="categoryOptions"
                  :placeholder="t('dictType.form.categoryPlaceholder')"
                  show-search
                  :filter-option="(input: string, option: any) => {
                    return option.label.toLowerCase().indexOf(input.toLowerCase()) >= 0
                  }"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('dictType.form.status')" name="status">
                <hbt-select
                  v-model:value="formState.status"
                  :options="statusOptions"
                  :placeholder="t('dictType.form.statusPlaceholder')"
                  show-search
                  :filter-option="(input: string, option: any) => {
                    return option.label.toLowerCase().indexOf(input.toLowerCase()) >= 0
                  }"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>
        <a-tab-pane key="other" :tab="t('common.tab.otherInfo')">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('dictType.form.orderNum')" name="orderNum">
                <a-input-number
                  v-model:value="formState.orderNum"
                  :min="0"
                  :max="9999"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="24">
              <a-form-item :label="t('dictType.form.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 19 }">
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('dictType.form.remarkPlaceholder')"
                  :maxlength="500"
                  :auto-size="{ minRows: 3, maxRows: 5 }"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>
      </a-tabs>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { HbtDictType } from '@/types/admin/hbtDictType'
import type { HbtStatus } from '@/types/base'
import HbtModal from '@/components/Business/Modal/index.vue'
import HbtSelect from '@/components/Business/Select/index.vue'

const { t } = useI18n()

// === Props 定义 ===
interface Props {
  open: boolean
  title?: string
  loading?: boolean
  model?: Partial<HbtDictType>
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  title: '',
  loading: false,
  model: () => ({})
})

// === Emits 定义 ===
const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'ok', values: Partial<HbtDictType>): void
  (e: 'cancel'): void
}>()

// === 状态定义 ===
const formRef = ref<FormInstance>()
const activeTab = ref('basic')
const formState = ref<Partial<HbtDictType>>({
  dictName: '',
  dictType: '',
  dictCategory: 0,
  dictBuiltin: 0,
  orderNum: 0,
  status: 0 as HbtStatus,
  remark: ''
})

// === 选项定义 ===
const statusOptions = [
  { label: t('common.status.normal'), value: 0 },
  { label: t('common.status.disabled'), value: 1 }
]

const categoryOptions = [
  { label: t('dictType.category.system'), value: 0 },
  { label: t('dictType.category.sql'), value: 1 }
]

// === 表单校验规则 ===
const rules: Record<string, RuleObject[]> = {
  dictName: [{ required: true, message: t('dictType.rules.nameRequired'), trigger: 'blur' }],
  dictType: [{ required: true, message: t('dictType.rules.typeRequired'), trigger: 'blur' }],
  dictCategory: [{ required: true, message: t('dictType.rules.categoryRequired'), trigger: 'change' }],
  orderNum: [{ required: true, message: t('dictType.rules.orderNumRequired'), trigger: 'blur' }],
  status: [{ required: true, message: t('dictType.rules.statusRequired'), trigger: 'change' }]
}

// === 监听数据变化 ===
watch(
  () => props.model,
  (val) => {
    if (val) {
      formState.value = {
        dictName: val.dictName || '',
        dictType: val.dictType || '',
        dictCategory: val.dictCategory ?? 0,
        dictBuiltin: val.dictBuiltin ?? 0,
        orderNum: val.orderNum ?? 0,
        status: val.status ?? 0,
        remark: val.remark || ''
      }
    }
  },
  { immediate: true }
)

// === 方法定义 ===
const handleOk = async () => {
  try {
    await formRef.value?.validate()
    emit('ok', formState.value)
  } catch (error: any) {
    console.error('表单验证失败:', error)
    if (error.response?.status === 403) {
      message.error('您没有操作权限')
    } else if (error.message) {
      message.error(error.message)
    } else {
      message.error('表单提交失败，请稍后重试')
    }
  }
}

const handleCancel = () => {
  formRef.value?.resetFields()
  emit('update:open', false)
  emit('cancel')
}
</script>

<style lang="less" scoped>
.ant-form-item {
  margin-bottom: 24px;
}
</style> 