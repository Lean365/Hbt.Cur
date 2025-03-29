//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : {{ pascal_case table.table_name }}Form.vue
// 创建者 : CodeGenerator
// 创建时间: {{ datetime }}
// 版本号 : v1.0.0
// 描述    : {{ table.comment }}表单组件
//===================================================================

<template>
  <hbt-modal
    v-model:visible="visible"
    :title="title"
    :loading="loading"
    :width="800"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-row :gutter="24">
        {{~ for column in table.columns ~}}
        {{~ if not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
        <a-col :span="12">
          <a-form-item
            :label="t('{{ table.module_name }}.{{ table.name }}.fields.{{ column.column_name }}.label')"
            name="{{ column.column_name }}"
          >
            {{~ if column.data_type == "string" and column.length > 100 ~}}
            <a-textarea
              v-model:value="formState.{{ column.column_name }}"
              :placeholder="t('{{ table.module_name }}.{{ table.name }}.fields.{{ column.column_name }}.placeholder')"
              :rows="4"
              :maxlength="{{ column.length }}"
              show-count
              allow-clear
            />
            {{~ else if column.data_type == "string" ~}}
            <a-input
              v-model:value="formState.{{ column.column_name }}"
              :placeholder="t('{{ table.module_name }}.{{ table.name }}.fields.{{ column.column_name }}.placeholder')"
              :maxlength="{{ column.length }}"
              allow-clear
            />
            {{~ else if column.data_type == "int" or column.data_type == "long" ~}}
            <a-input-number
              v-model:value="formState.{{ column.column_name }}"
              :placeholder="t('{{ table.module_name }}.{{ table.name }}.fields.{{ column.column_name }}.placeholder')"
              style="width: 100%"
            />
            {{~ else if column.data_type == "datetime" ~}}
            <a-date-picker
              v-model:value="formState.{{ column.column_name }}"
              :placeholder="t('{{ table.module_name }}.{{ table.name }}.fields.{{ column.column_name }}.placeholder')"
              show-time
              style="width: 100%"
            />
            {{~ else if column.column_name == "status" ~}}
            <hbt-select
              v-model:value="formState.status"
              dict-type="sys_normal_disable"
              :placeholder="t('{{ table.module_name }}.{{ table.name }}.fields.status.placeholder')"
            />
            {{~ end ~}}
          </a-form-item>
        </a-col>
        {{~ end ~}}
        {{~ end ~}}
      </a-row>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { {{ pascal_case table.table_name }}, {{ pascal_case table.table_name }}Create, {{ pascal_case table.table_name }}Update } from '@/types/{{ table.module_name }}/{{ table.name }}'
import { create, update } from '@/api/{{ table.module_name }}/{{ table.name }}'
import { message } from 'ant-design-vue'

const { t } = useI18n()

// === Props 定义 ===
interface Props {
  visible: boolean
  title: string
  loading?: boolean
  model?: Partial<{{ pascal_case table.table_name }}>
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  loading: false,
  model: () => ({})
})

// === Emits 定义 ===
const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'ok', values: {{ pascal_case table.table_name }}Create | {{ pascal_case table.table_name }}Update): void
  (e: 'cancel'): void
}>()

// === 状态定义 ===
const formRef = ref<FormInstance>()
const formState = reactive<Partial<{{ pascal_case table.table_name }}>>({
  {{~ for column in table.columns ~}}
  {{~ if not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
  {{ column.column_name }}: undefined,
  {{~ end ~}}
  {{~ end ~}}
})

// === 表单校验规则 ===
const rules: Record<string, Rule[]> = {
  {{~ for column in table.columns ~}}
  {{~ if not column.is_nullable and not column.is_pk and column.column_name != "create_time" and column.column_name != "update_time" ~}}
  {{ column.column_name }}: [
    { required: true, message: t('{{ table.module_name }}.{{ table.name }}.rules.{{ column.column_name }}.required') },
    {{~ if column.data_type == "string" ~}}
    { max: {{ column.length }}, message: t('{{ table.module_name }}.{{ table.name }}.rules.{{ column.column_name }}.max') },
    {{~ end ~}}
  ],
  {{~ end ~}}
  {{~ end ~}}
}

// === 监听数据变化 ===
watch(
  () => props.model,
  (val) => {
    if (val) {
      Object.assign(formState, val)
    }
  },
  { deep: true, immediate: true }
)

// === 方法定义 ===
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    const values = { ...formState }
    if (props.model?.{{ get_pk_name table }}) {
      await update(values as {{ pascal_case table.table_name }}Update)
      message.success(t('{{ table.module_name }}.{{ table.name }}.message.success.update'))
    } else {
      await create(values as {{ pascal_case table.table_name }}Create)
      message.success(t('{{ table.module_name }}.{{ table.name }}.message.success.create'))
    }
    emit('ok', values)
    emit('update:visible', false)
  } catch (error) {
    // 表单验证失败
    console.error('表单验证失败:', error)
  }
}
</script>

<style lang="less" scoped>
.ant-form {
  padding: 24px 0;
}

.ant-form-item {
  margin-bottom: 24px;
}

.ant-input-number {
  width: 100%;
}

:deep(.ant-form-item-label) {
  min-width: 90px;
  text-align: right;
  padding-right: 8px;
}

:deep(.ant-form-item-control) {
  flex: 1;
}

:deep(.ant-input-textarea) {
  width: 100%;
}
</style> 