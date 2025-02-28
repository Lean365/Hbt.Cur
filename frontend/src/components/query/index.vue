//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 通用查询组件
//===================================================================

<template>
  <div class="hbt-query">
    <a-form
      ref="formRef"
      :model="formState"
      :label-col="{ style: { width: layout.labelWidth + 'px' } }"
      :label-align="layout.labelAlign"
    >
      <a-row :gutter="16">
        <!-- 查询项 -->
        <template v-for="(item, index) in displayItems" :key="item.name">
          <a-col :span="colSpan">
            <a-form-item
              :label="item.label"
              :name="item.name"
              :rules="item.rules"
            >
              <!-- 输入框 -->
              <a-input
                v-if="item.type === 'input'"
                v-model:value="formState[item.name]"
                :placeholder="getPlaceholder(item, 'inputPlaceholder')"
                allow-clear
                @pressEnter="handleSearch"
              />

              <!-- 选择框 -->
              <a-select
                v-else-if="item.type === 'select'"
                v-model:value="formState[item.name]"
                :placeholder="getPlaceholder(item, 'selectPlaceholder')"
                :options="item.options"
                allow-clear
              />

              <!-- 日期选择 -->
              <a-date-picker
                v-else-if="item.type === 'date'"
                v-model:value="formState[item.name]"
                :placeholder="getPlaceholder(item, 'datePlaceholder')"
                :format="dateFormat"
                :value-format="dateFormat"
                allow-clear
                style="width: 100%"
                @change="(date, dateString) => handleDateChange(item.name, dateString)"
              />

              <!-- 日期范围 -->
              <a-range-picker
                v-else-if="item.type === 'dateRange'"
                v-model:value="formState[item.name]"
                :placeholder="getDateRangePlaceholder(item)"
                :format="dateFormat"
                :value-format="dateFormat"
                allow-clear
                style="width: 100%"
                @change="(dates, dateStrings) => handleDateRangeChange(item.name, dateStrings)"
              />

              <!-- 数字输入 -->
              <a-input-number
                v-else-if="item.type === 'number'"
                v-model:value="formState[item.name]"
                :placeholder="getPlaceholder(item, 'numberPlaceholder')"
                style="width: 100%"
              />

              <!-- 自定义插槽 -->
              <slot
                v-else
                :name="item.name"
                v-bind="{ value: formState[item.name], onChange: (val: any) => handleCustomChange(item.name, val) }"
              />
            </a-form-item>
          </a-col>
        </template>

        <!-- 操作按钮 -->
        <a-col :span="colSpan" class="query-buttons">
          <a-space>
            <a-button
              v-if="buttons.search"
              type="primary"
              :loading="loading"
              @click="handleSearch"
            >
              {{ t('query.search') }}
            </a-button>
            <a-button v-if="buttons.reset" @click="handleReset">
              {{ t('query.reset') }}
            </a-button>
            <a-button
              v-if="buttons.collapse && items.length > showCount"
              type="link"
              @click="toggleCollapse"
            >
              {{ collapsed ? t('query.expand') : t('query.collapse') }}
              <template #icon>
                <down-outlined v-if="collapsed" />
                <up-outlined v-else />
              </template>
            </a-button>
          </a-space>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { Form } from 'ant-design-vue'
import { DownOutlined, UpOutlined } from '@ant-design/icons-vue'
import { useI18n } from 'vue-i18n'
import type {
  IHbtQueryProps,
  IHbtQueryInstance,
  IHbtQueryItem
} from '@/types/components/query'
import { 
  formatDateTime,
  parseDate,
  formatDate,
  getDatesBetween
} from '@/utils/datetime'

// 定义属性
const props = withDefaults(defineProps<IHbtQueryProps>(), {
  items: () => [],
  layout: () => ({}),
  buttons: () => ({}),
  showCount: 3,
  loading: false
})

// 定义事件
const emit = defineEmits<{
  (e: 'search', values: Record<string, any>): void
  (e: 'reset'): void
}>()

// 国际化
const { t } = useI18n()

// 表单实例
const formRef = ref()
const formState = ref<Record<string, any>>({})

// 布局配置
const layout = computed(() => ({
  labelWidth: 100,
  span: 6,
  labelAlign: 'right' as const,
  ...props.layout
}))

// 按钮配置
const buttons = computed(() => ({
  search: true,
  reset: true,
  collapse: true,
  ...props.buttons
}))

// 展开/收起状态
const collapsed = ref(true)
const showCount = computed(() => props.showCount)

// 计算列宽
const colSpan = computed(() => layout.value.span)

// 计算显示的查询项
const displayItems = computed(() => {
  if (!collapsed.value || props.items.length <= showCount.value) {
    return props.items
  }
  return props.items.slice(0, showCount.value)
})

// 获取占位符
const getPlaceholder = (item: IHbtQueryItem, key: string) => {
  if (item.placeholder && typeof item.placeholder === 'string') {
    return item.placeholder
  }
  return t(`components.query.${key}`, { field: item.label })
}

// 获取日期范围占位符
const getDateRangePlaceholder = (item: IHbtQueryItem): string[] => {
  if (item.placeholder && Array.isArray(item.placeholder)) {
    return item.placeholder
  }
  const defaultPlaceholder = t('components.query.dateRangePlaceholder', { returnType: 'array' })
  return Array.isArray(defaultPlaceholder) ? defaultPlaceholder : ['开始日期', '结束日期']
}

// 切换展开/收起
const toggleCollapse = () => {
  collapsed.value = !collapsed.value
}

// 日期格式
const dateFormat = 'yyyy-MM-dd'

// 处理日期选择变更
const handleDateChange = (name: string, dateString: string | null) => {
  if (!dateString) {
    formState.value[name] = null
    return
  }
  formState.value[name] = formatDate(dateString)
}

// 处理日期范围变更
const handleDateRangeChange = (name: string, dateStrings: [string, string] | null) => {
  if (!dateStrings || !dateStrings[0] || !dateStrings[1]) {
    formState.value[name] = null
    return
  }

  const [startStr, endStr] = dateStrings
  formState.value[name] = [
    formatDate(startStr),
    formatDate(endStr)
  ]
}

// 处理查询
const handleSearch = async () => {
  try {
    const values = await formRef.value.validateFields()
    
    // 处理日期字段
    const processedValues = { ...values }
    props.items.forEach(item => {
      if (item.type === 'date' && processedValues[item.name]) {
        processedValues[item.name] = formatDate(processedValues[item.name])
      } else if (item.type === 'dateRange' && Array.isArray(processedValues[item.name])) {
        const [start, end] = processedValues[item.name]
        if (start && end) {
          processedValues[item.name] = [
            formatDate(start),
            formatDate(end)
          ]
        }
      }
    })
    
    emit('search', processedValues)
  } catch (error) {
    // 校验失败
  }
}

// 处理重置
const handleReset = () => {
  formRef.value.resetFields()
  emit('reset')
}

// 处理自定义组件值变更
const handleCustomChange = (name: string, value: any) => {
  formState.value[name] = value
}

// 监听items变化,设置默认值
watch(
  () => props.items,
  (items) => {
    const defaultValues: Record<string, any> = {}
    items.forEach((item) => {
      if (item.defaultValue !== undefined) {
        defaultValues[item.name] = item.defaultValue
      }
    })
    formState.value = { ...formState.value, ...defaultValues }
  },
  { immediate: true }
)

// 暴露组件实例方法
defineExpose<IHbtQueryInstance>({
  reset: () => {
    formRef.value?.resetFields()
  },
  setFieldsValue: (values: Record<string, any>) => {
    formRef.value?.setFieldsValue(values)
  },
  getFieldsValue: () => {
    return formRef.value?.getFieldsValue()
  },
  validate: async () => {
    return await formRef.value?.validateFields()
  }
})
</script>

<style lang="less" scoped>

</style> 