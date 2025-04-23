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
import { useDictStore } from '@/stores/dict'
import type { DictOption } from '@/stores/dict'
import type { HbtSelectOption } from '@/types/common'
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
  showAll?: boolean
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
  showAll: true,
  filterOption: (input: string, option: DefaultOptionType) => {
    return (option?.label as string)?.toLowerCase().includes(input.toLowerCase())
  }
})

const emit = defineEmits<{
  'update:value': [value: SelectValue]
  'change': [value: SelectValue, option: DefaultOptionType | DefaultOptionType[]]
}>()

// 加载字典数据
const dictStore = useDictStore()
const options = computed(() => props.dictType ? dictStore.getDictOptions(props.dictType) : [])

// 统一处理值的类型转换
const normalizeValue = (value: any): any => {
  if (value === undefined || value === null) {
    return -1;
  }
  // 如果是字符串且可以转换为数字，则转换为数字
  if (typeof value === 'string' && !isNaN(Number(value))) {
    return Number(value);
  }
  return value;
}

// 根据值查找对应的选项
const findOptionByValue = (value: any, options: DefaultOptionType[]): DefaultOptionType | undefined => {
  const normalizedSearchValue = normalizeValue(value);
  return options.find(opt => normalizeValue(opt.value) === normalizedSearchValue);
}

// 显示选项
const displayOptions = computed(() => {
  let opts: DefaultOptionType[] = []
  
  // 添加其他选项
  if (props.options && props.options.length > 0) {
    opts = [...opts, ...props.options.map(opt => ({
      ...opt,
      value: normalizeValue(opt.value)
    }))]
  } else if (props.dictType) {
    opts = [...opts, ...options.value.map(opt => ({
      ...opt,
      value: normalizeValue(opt.value)
    }))]
  }
  
  // 如果是 radio 类型且 showAll 为 true，添加"全部"选项
  if (props.type === 'radio' && props.showAll) {
    opts.unshift({
      label: t('select.all'),
      value: undefined,
      disabled: false,
      cssClass: '',
      listClass: '',
      extLabel: '',
      extValue: '',
      transKey: ''
    })
  }
  
  return opts
})

// 根据type确定使用哪个组件
const controlComponent = computed(() => {
  //console.log('[HbtSelect] type:', props.type)
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
  //console.log('[HbtSelect] controlProps type:', props.type)
  
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
      return {
        ...baseProps,
        optionType: props.type === 'radio-button' ? 'button' : 'default',
        options: displayOptions.value,
        placeholder: localizedPlaceholder.value,
        buttonStyle: props.type === 'radio-button' ? 'solid' : undefined,
        direction: 'horizontal'
      }
    case 'checkbox':
    case 'checkbox-group':
      return {
        ...baseProps,
        options: displayOptions.value,
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

// 将数值类型统一处理用于内部显示
const innerValue = computed({
  get: () => {
    const opts = displayOptions.value;
    const normalizedValue = normalizeValue(props.value);
    
    // 如果选项还没有加载完成，先返回原始值
    if (opts.length === 0 && (props.options || props.dictType)) {
      return normalizedValue;
    }
    
    // 对于所有类型，都尝试查找匹配的选项
    const matchingOption = findOptionByValue(normalizedValue, opts);
    
    // 如果找到匹配的选项，返回其值（保持原始类型）
    if (matchingOption) {
      return matchingOption.value;
    }
    
    // 如果没有找到匹配的选项，返回原始值
    return normalizedValue;
  },
  set: (val: SelectValue) => {
    // 发出更新事件时，确保值的类型正确
    emit('update:value', normalizeValue(val));
  }
})

// 处理change事件
const handleChange = (value: SelectValue, option: DefaultOptionType | DefaultOptionType[]) => {
  if (value === undefined || value === null) {
    emit('change', value, option);
    return;
  }
  
  let convertedValue;
  switch (props.type) {
    case 'switch':
      convertedValue = value ? props.checkedValue : props.uncheckedValue;
      break;
    case 'checkbox':
    case 'checkbox-group':
      convertedValue = Array.isArray(value)
        ? value.map(normalizeValue)
        : [normalizeValue(value)];
      break;
    default:
      convertedValue = Array.isArray(value)
        ? value.map(normalizeValue)
        : normalizeValue(value);
  }
  
  emit('change', convertedValue, option);
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
onMounted(() => {
  if (props.dictType) {
    dictStore.loadDict(props.dictType)
  }
})
</script>
