<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: Select/index.vue
创建日期: 2024-03-20
描述: 统一封装的选择组件
功能特点:
1. 支持多种选择类型（Select、Radio、Radio.Button、Checkbox、Switch、Rate等）
2. 处理数值类型的自动转换
3. 支持字典数据的自动加载和使用
4. 支持大数据虚拟滚动和远程搜索
5. 保持与ant-design-vue的API一致性
=================================================================== 
-->

<template>
  <component
    :is="controlComponent"
    v-model:value="innerValue"
    v-bind="controlProps"
    @change="handleChange"
    @search="handleSearch"
  >
    <template v-if="type === 'radio-button'">
      <a-radio-button 
        v-for="option in displayOptions" 
        :key="String(option.value ?? '')" 
        :value="option.value"
        :disabled="option.disabled"
      >
        {{ option.label }}
      </a-radio-button>
    </template>
    <template v-else-if="type === 'checkbox'">
      <a-checkbox 
        v-for="option in displayOptions" 
        :key="String(option.value ?? '')" 
        :value="option.value"
        :disabled="option.disabled"
      >
        {{ option.label }}
      </a-checkbox>
    </template>
  </component>
</template>

<script lang="ts" setup>
import type { SelectProps } from 'ant-design-vue'
import { SelectValue, DefaultOptionType } from 'ant-design-vue/es/select'
import { useDictData } from '@/hooks/useDictData'
import type { HbtDictData } from '@/types/admin/hbtDictData'
import type { DictOption } from '@/hooks/useDictData'
import { debounce } from 'lodash-es'
import { useI18n } from 'vue-i18n'
const { t } = useI18n()
const attrs = useAttrs()
const loading = ref(false)
const keyword = ref('')
const page = ref(1)
type ControlType = 'select' | 'radio' | 'radio-button' | 'checkbox' | 'checkbox-group' | 'switch' | 'rate'

interface Props {
  value?: SelectValue
  options?: DefaultOptionType[]
  dictType?: string
  type?: ControlType
  checkedValue?: number | string
  uncheckedValue?: number | string
  count?: number
  remote?: boolean
  showSearch?: boolean
  maxTagCount?: number
  pageSize?: number
  mode?: 'multiple' | 'tags'
  placeholder?: string
  allowClear?: boolean
  loading?: boolean
  filterOption?: boolean | ((input: string, option: DefaultOptionType) => boolean)
}

const props = withDefaults(defineProps<Props>(), {
  options: () => [],
  type: 'select',
  checkedValue: 1,
  uncheckedValue: 0,
  count: 5,
  remote: false,
  showSearch: true,
  maxTagCount: 3,
  pageSize: 100,
  placeholder: 'common.form.placeholder.select',
  allowClear: true,
  loading: false,
  filterOption: (input: string, option: DefaultOptionType) => {
    return (option?.label as string)?.toLowerCase().includes(input.toLowerCase())
  }
})

const emit = defineEmits<{
  'update:value': [value: SelectValue]
  'change': [value: SelectValue, option: DefaultOptionType | DefaultOptionType[]]
}>()

// 加载字典数据
const { dictDataMap } = props.dictType 
  ? useDictData([props.dictType])
  : { dictDataMap: ref<Record<string, DictOption[]>>({}) }

// 根据type确定使用哪个组件
const controlComponent = computed(() => {
  switch (props.type) {
    case 'radio':
      return 'a-radio-group'
    case 'radio-button':
      return 'a-radio-group'
    case 'checkbox':
      return 'a-checkbox-group'
    case 'checkbox-group':
      return 'a-checkbox-group'
    case 'switch':
      return 'a-switch'
    case 'rate':
      return 'a-rate'
    default:
      return 'a-select'
  }
})

// 获取本地化的占位符文本
const localizedPlaceholder = computed(() => {
  return props.placeholder ? t(props.placeholder) : ''
})

// 根据控件类型设置不同的属性
const controlProps = computed(() => {
  const baseProps = { ...attrs }
  
  switch (props.type) {
    case 'select':
      return {
        ...baseProps,
        options: displayOptions.value,
        loading: loading.value,
        showSearch: props.showSearch,
        filterOption: props.remote ? false : props.filterOption,
        maxTagCount: props.maxTagCount,
        mode: props.mode,
        placeholder: localizedPlaceholder.value,
        allowClear: props.allowClear,
        virtual: true,
        onPopupScroll: props.remote ? handlePopupScroll : undefined,
        onSearch: props.remote ? handleSearch : undefined
      }
    case 'radio':
    case 'radio-button':
    case 'checkbox':
    case 'checkbox-group':
      return {
        ...baseProps,
        options: mergedOptions.value,
        placeholder: localizedPlaceholder.value
      }
    case 'switch':
      return {
        ...baseProps,
        checkedValue: props.checkedValue,
        unCheckedValue: props.uncheckedValue,
        checkedChildren: t('common.yesNo.yes'),
        unCheckedChildren: t('common.yesNo.no')
      }
    case 'rate':
      return {
        ...baseProps,
        count: props.count
      }
    default:
      return baseProps
  }
})

// 将数值类型转换为字符串用于内部显示
const innerValue = computed({
  get: () => {
    if (props.value === undefined || props.value === null) return props.value
    
    switch (props.type) {
      case 'switch':
        return props.value === props.checkedValue
      case 'checkbox':
      case 'checkbox-group':
        return Array.isArray(props.value) 
          ? props.value.map(String)
          : [String(props.value)]
      default:
        return Array.isArray(props.value)
          ? props.value.map(String)
          : String(props.value)
    }
  },
  set: (val) => {
    if (val === undefined || val === null) {
      emit('update:value', val)
      return
    }
    
    let newVal
    switch (props.type) {
      case 'switch':
        newVal = val ? props.checkedValue : props.uncheckedValue
        break
      case 'checkbox':
      case 'checkbox-group':
        newVal = Array.isArray(val)
          ? val.map(v => Number(v))
          : [Number(val)]
        break
      default:
        newVal = Array.isArray(val)
          ? val.map(v => Number(v))
          : Number(val)
    }
    emit('update:value', newVal)
  }
})

// 合并选项
const mergedOptions = computed(() => {
  if (props.dictType && dictDataMap.value) {
    return dictDataMap.value[props.dictType] || []
  }
  return props.options || []
})

// 显示的选项
const displayOptions = computed(() => {
  const options = mergedOptions.value
  if (!options.length) return []
  
  if (props.remote && keyword.value) {
    return options.filter((option: DefaultOptionType) => 
      String(option.label)
        .toLowerCase()
        .includes(keyword.value.toLowerCase())
    )
  }
  
  return options
})

// 处理change事件
const handleChange = (value: SelectValue, option: DefaultOptionType | DefaultOptionType[]) => {
  if (value === undefined || value === null) {
    emit('change', value, option)
    return
  }
  
  let convertedValue
  switch (props.type) {
    case 'switch':
      convertedValue = value ? props.checkedValue : props.uncheckedValue
      break
    case 'checkbox':
    case 'checkbox-group':
      convertedValue = Array.isArray(value)
        ? value.map(v => Number(v))
        : [Number(value)]
      break
    default:
      convertedValue = Array.isArray(value)
        ? value.map(v => Number(v))
        : Number(value)
  }
  emit('change', convertedValue, option)
}

// 处理远程搜索
const handleSearch = props.type === 'select' ? debounce(async (value: string) => {
  if (!props.remote) return
  
  keyword.value = value
  page.value = 1
}, 300) : undefined

// 处理滚动加载
const handlePopupScroll = props.type === 'select' ? async (e: Event) => {
  if (!props.remote) return
  
  const target = e.target as HTMLElement
  if (
    !loading.value &&
    target.scrollTop + target.clientHeight >= target.scrollHeight - 20
  ) {
    page.value++
  }
} : undefined

// 监听dictType变化，自动加载字典数据
watch(() => props.dictType, async (newType) => {
  if (newType) {
    page.value = 1
    keyword.value = ''
  }
}, { immediate: true })

// 组件挂载时加载字典数据
onMounted(async () => {
  if (props.dictType) {
  }
})
</script>
