<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: query/index.vue
创建日期: 2024-03-20
描述: 查询组件，提供表单查询、重置、展开收起等功能
=================================================================== 
-->

<template>
  <div class="query-container">
    <!-- 查询表单 -->
    <a-form
      ref="formRef"
      :model="formState"
      :label-col="labelCol"
      :wrapper-col="wrapperCol"
      :layout="layout"
      @finish="handleFinish"
    >
      <a-row :gutter="24">
        <!-- 查询字段 -->
        <template v-for="(field, index) in queryFields" :key="field.name">
          <a-col
            :span="field.span || defaultSpan"
            :xs="24"
            :sm="12"
            :md="field.span || defaultSpan"
            v-show="!collapsed || index < visibleFields"
          >
            <a-form-item
              :label="field.label"
              :name="field.name"
              :rules="field.rules"
              :colon="field.colon"
            >
              <!-- 默认插槽 -->
              <slot
                :name="field.name"
                v-bind="{ field, value: formState[field.name] }"
              >
                <!-- 输入框 -->
                <a-input
                  v-if="field.type === 'input'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder || t('common.form.placeholder.input')"
                  :allowClear="true"
                  :maxLength="field.maxLength"
                  :disabled="field.disabled"
                  :size="size"
                />

                <!-- 选择框 -->
                <a-select
                  v-else-if="field.type === 'select'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder || t('common.form.placeholder.select')"
                  :options="field.options"
                  :allowClear="true"
                  :mode="field.mode"
                  :disabled="field.disabled"
                  :size="size"
                />

                <!-- 日期选择器 -->
                <a-date-picker
                  v-else-if="field.type === 'date'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder || t('common.form.placeholder.date')"
                  :format="field.format || 'YYYY-MM-DD'"
                  :allowClear="true"
                  :disabled="field.disabled"
                  :size="size"
                />

                <!-- 日期范围选择器 -->
                <a-range-picker
                  v-else-if="field.type === 'dateRange'"
                  v-model:value="formState[field.name]"
                  :placeholder="[
                    field.placeholder?.[0] || t('common.datetime.startDate'),
                    field.placeholder?.[1] || t('common.datetime.endDate')
                  ]"
                  :format="field.format || 'YYYY-MM-DD'"
                  :allowClear="true"
                  :disabled="field.disabled"
                  :size="size"
                />

                <!-- 数字输入框 -->
                <a-input-number
                  v-else-if="field.type === 'number'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder"
                  :min="field.min"
                  :max="field.max"
                  :step="field.step"
                  :precision="field.precision"
                  :disabled="field.disabled"
                  :size="size"
                />

                <!-- 单选框组 -->
                <a-radio-group
                  v-else-if="field.type === 'radio'"
                  v-model:value="formState[field.name]"
                  :options="field.options"
                  :disabled="field.disabled"
                  :size="size"
                />

                <!-- 复选框组 -->
                <a-checkbox-group
                  v-else-if="field.type === 'checkbox'"
                  v-model:value="formState[field.name]"
                  :options="field.options"
                  :disabled="field.disabled"
                />

                <!-- 级联选择 -->
                <a-cascader
                  v-else-if="field.type === 'cascader'"
                  v-model:value="formState[field.name]"
                  :options="field.options"
                  :placeholder="field.placeholder || t('common.form.placeholder.select')"
                  :allowClear="true"
                  :disabled="field.disabled"
                  :size="size"
                />
              </slot>
            </a-form-item>
          </a-col>
        </template>

        <!-- 操作按钮 -->
        <a-col
          :span="defaultSpan"
          :xs="24"
          :sm="12"
          :md="defaultSpan"
          class="query-actions"
        >
          <a-space :size="buttonSpace">
            <!-- 搜索按钮 -->
            <a-button
              type="primary"
              html-type="submit"
              :loading="loading"
              :size="size"
            >
              <template #icon><search-outlined /></template>
              {{ t('query.search') }}
            </a-button>
            
            <!-- 重置按钮 -->
            <a-button @click="handleReset" :size="size">
              <template #icon><redo-outlined /></template>
              {{ t('query.reset') }}
            </a-button>
            
            <!-- 展开/收起按钮 -->
            <a-button
              v-if="showCollapse && queryFields.length > visibleFields"
              type="link"
              @click="toggleCollapse"
              :size="size"
            >
              {{ collapsed ? t('query.expand') : t('query.collapse') }}
              <template #icon>
                <down-outlined :class="{ 'expanded': !collapsed }" />
              </template>
            </a-button>

            <!-- 额外的操作按钮 -->
            <slot name="actions"></slot>
          </a-space>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import {
  SearchOutlined,
  RedoOutlined,
  DownOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()

// === 类型定义 ===
type FieldType = 'input' | 'select' | 'date' | 'dateRange' | 'number' | 'radio' | 'checkbox' | 'cascader'

interface QueryField {
  name: string // 字段名
  label: string // 字段标签
  type: FieldType // 字段类型
  span?: number // 栅格宽度
  placeholder?: string | string[] // 占位文本
  rules?: any[] // 验证规则
  options?: { label: string; value: any }[] // 选择项
  format?: string // 日期格式
  min?: number // 最小值
  max?: number // 最大值
  step?: number // 步长
  precision?: number // 精度
  maxLength?: number // 最大长度
  disabled?: boolean // 是否禁用
  colon?: boolean // 是否显示冒号
  mode?: 'multiple' | 'tags' // 选择模式
}

interface Props {
  queryFields: QueryField[] // 查询字段配置
  defaultSpan?: number // 默认栅格宽度
  visibleFields?: number // 默认显示字段数
  showCollapse?: boolean // 是否显示展开/收起按钮
  loading?: boolean // 加载状态
  size?: 'small' | 'middle' | 'large' // 组件大小
  layout?: 'horizontal' | 'vertical' | 'inline' // 表单布局
  labelCol?: object // 标签布局
  wrapperCol?: object // 输入框布局
  buttonSpace?: number // 按钮间距
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  queryFields: () => [],
  defaultSpan: 6,
  visibleFields: 3,
  showCollapse: true,
  loading: false,
  size: 'middle',
  layout: 'horizontal',
  labelCol: () => ({ span: 8 }),
  wrapperCol: () => ({ span: 16 }),
  buttonSpace: 8
})

// === 事件定义 ===
const emit = defineEmits(['search', 'reset'])

// === 表单相关 ===
const formRef = ref<FormInstance>()
const formState = reactive<Record<string, any>>({})
const collapsed = ref(true)

// === 监听器 ===
watch(
  () => props.queryFields,
  (fields) => {
    fields.forEach((field) => {
      if (!(field.name in formState)) {
        formState[field.name] = undefined
      }
    })
  },
  { immediate: true }
)

// === 事件处理 ===
const handleFinish = (values: any) => {
  emit('search', values)
}

const handleReset = () => {
  formRef.value?.resetFields()
  emit('reset')
}

const toggleCollapse = () => {
  collapsed.value = !collapsed.value
}
</script>

<style lang="less" scoped>
</style> 