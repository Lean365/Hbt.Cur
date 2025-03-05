<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: Select/index.vue
创建日期: 2024-03-20
描述: 统一封装的选择组件
功能特点:
1. 处理数值类型的自动转换
2. 保持与ant-design-vue的API一致性
=================================================================== 
-->

<template>
  <a-select
    v-model:value="innerValue"
    v-bind="$attrs"
    :options="convertedOptions"
    @change="handleChange"
  />
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import type { SelectProps } from 'ant-design-vue'
import { SelectValue, DefaultOptionType } from 'ant-design-vue/es/select'

interface Props {
  value?: SelectValue
  options?: DefaultOptionType[]
}

const props = withDefaults(defineProps<Props>(), {
  options: () => []
})

const emit = defineEmits<{
  'update:value': [value: SelectValue]
  'change': [value: SelectValue, option: DefaultOptionType | DefaultOptionType[]]
}>()

// 将数值类型转换为字符串用于内部显示
const innerValue = computed({
  get: () => {
    if (props.value === undefined || props.value === null) return props.value
    return Array.isArray(props.value)
      ? props.value.map(String)
      : String(props.value)
  },
  set: (val) => {
    if (val === undefined || val === null) {
      emit('update:value', val)
      return
    }
    const newVal = Array.isArray(val)
      ? val.map(v => Number(v))
      : Number(val)
    emit('update:value', newVal)
  }
})

// 转换选项值为字符串
const convertedOptions = computed(() => {
  return props.options.map(opt => ({
    ...opt,
    value: opt.value != null ? String(opt.value) : opt.value
  }))
})

// 处理change事件
const handleChange = (value: SelectValue, option: DefaultOptionType | DefaultOptionType[]) => {
  if (value === undefined || value === null) {
    emit('change', value, option)
    return
  }
  const convertedValue = Array.isArray(value)
    ? value.map(v => Number(v))
    : Number(value)
  emit('change', convertedValue, option)
}
</script>
