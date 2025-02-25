//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 通用字典组件
//===================================================================

<template>
  <div class="hbt-dict">
    <!-- 下拉选择 -->
    <template v-if="type === 'select'">
      <a-select
        v-model:value="selectedValue"
        :placeholder="placeholder"
        :allow-clear="allowClear"
        :disabled="disabled"
        :loading="loading"
        :mode="multiple ? 'multiple' : undefined"
        :options="options"
        :filter-option="false"
        :show-search="showSearch"
        :virtual-scroll="true"
        :not-found-content="loading ? undefined : '无匹配数据'"
        :autoClearSearchValue="true"
        @search="handleSearch"
        @change="handleSelectChange"
        @popupScroll="handlePopupScroll"
        @dropdownVisibleChange="handleDropdownVisibleChange"
      >
        <template #option="{ label, value }">
          <a-tooltip :title="label">
            {{ label }}
          </a-tooltip>
        </template>
      </a-select>
    </template>

    <!-- 单选框组 -->
    <template v-else-if="type === 'radio'">
      <div class="radio-container">
        <a-input-search
          v-if="showSearch"
          v-model:value="searchText"
          :loading="loading"
          placeholder="请输入关键字"
          @search="handleSearch"
          style="margin-bottom: 8px;"
        />
        <div v-if="loading" class="loading-container">
          <a-spin />
        </div>
        <div v-else-if="!filteredOptions.length" class="empty-container">
          无匹配数据
        </div>
        <div v-else class="radio-list" v-virtual-scroll="{ items: filteredOptions }">
          <a-radio-group
            v-model:value="selectedValue"
            :disabled="disabled"
            @change="handleRadioChange"
          >
            <a-radio
              v-for="item in filteredOptions"
              :key="item.value"
              :value="item.value"
              :disabled="item.disabled"
            >
              <a-tooltip :title="item.label">
                {{ item.label }}
              </a-tooltip>
            </a-radio>
          </a-radio-group>
        </div>
      </div>
    </template>

    <!-- 复选框组 -->
    <template v-else-if="type === 'checkbox'">
      <div class="checkbox-container">
        <a-input-search
          v-if="showSearch"
          v-model:value="searchText"
          :loading="loading"
          placeholder="请输入关键字"
          @search="handleSearch"
          style="margin-bottom: 8px;"
        />
        <div v-if="loading" class="loading-container">
          <a-spin />
        </div>
        <div v-else-if="!filteredOptions.length" class="empty-container">
          无匹配数据
        </div>
        <div v-else class="checkbox-list" v-virtual-scroll="{ items: filteredOptions }">
          <a-checkbox-group
            :value="selectedValueArray"
            :disabled="disabled"
            @change="handleCheckboxChange"
          >
            <a-checkbox
              v-for="item in filteredOptions"
              :key="item.value"
              :value="item.value"
              :disabled="item.disabled"
            >
              <a-tooltip :title="item.label">
                {{ item.label }}
              </a-tooltip>
            </a-checkbox>
          </a-checkbox-group>
        </div>
      </div>
    </template>

    <!-- 标签展示 -->
    <template v-else-if="type === 'tag'">
      <a-tag
        v-if="!multiple"
        :color="getTagColor(selectedValue as string | number)"
      >
        {{ getLabel(selectedValue as string | number) }}
      </a-tag>
      <template v-else>
        <a-tag
          v-for="value in selectedValue as (string | number)[]"
          :key="value"
          :color="getTagColor(value)"
        >
          {{ getLabel(value) }}
        </a-tag>
      </template>
    </template>

    <!-- 文本展示 -->
    <template v-else>
      <span :class="getTextClass(selectedValue as string | number)">
        {{ multiple ? getLabels(selectedValue as (string | number)[]).join(',') : getLabel(selectedValue as string | number) }}
      </span>
    </template>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue'
import type { RadioChangeEvent } from 'ant-design-vue'
import type { SelectValue } from 'ant-design-vue/es/select'
import { Checkbox } from 'ant-design-vue'
import { getHbtDictDataList } from '@/api/admin/hbtDictData'
import type { HbtDictData, HbtDictDataQuery } from '@/types/admin/hbtDictData'
import { debounce } from 'lodash-es'

type CheckboxValueType = string | number | boolean

// 组件属性定义
const props = withDefaults(defineProps<{
  // 字典类型
  dictType: string
  // 展示类型：select/radio/checkbox/tag/text
  type?: string
  // 占位符
  placeholder?: string
  // 是否允许清除
  allowClear?: boolean
  // 是否禁用
  disabled?: boolean
  // 是否多选
  multiple?: boolean
  // 默认值
  value?: string | number | (string | number)[]
  // 标签颜色映射
  tagColors?: Record<string | number, string>
  // 文本样式映射
  textClasses?: Record<string | number, string>
  // 是否显示搜索框
  showSearch?: boolean
  // 每页加载数量
  pageSize?: number
  // 最小搜索字符数
  minSearchLength?: number
  // 搜索防抖延迟(ms)
  searchDebounce?: number
}>(), {
  type: 'select',
  placeholder: '请选择',
  allowClear: true,
  disabled: false,
  multiple: false,
  tagColors: () => ({}),
  textClasses: () => ({}),
  showSearch: true,
  pageSize: 20,
  minSearchLength: 1,
  searchDebounce: 300
})

// 组件事件
const emit = defineEmits<{
  (e: 'update:value', value: string | number | (string | number)[]): void
  (e: 'change', value: string | number | (string | number)[]): void
}>()

// 组件状态
const loading = ref(false)
const options = ref<{ label: string; value: string | number; disabled?: boolean }[]>([])
const selectedValue = ref<string | number | (string | number)[] | undefined>(props.value)
const searchText = ref('')
const pageNum = ref(1)
const hasMore = ref(true)
const dropdownVisible = ref(false)

// 复选框值
const selectedValueArray = computed(() => {
  return Array.isArray(selectedValue.value) ? selectedValue.value : []
})

// 过滤后的选项
const filteredOptions = computed(() => {
  if (!searchText.value) return options.value
  const keyword = searchText.value.toLowerCase()
  return options.value.filter(item => 
    item.label.toLowerCase().includes(keyword) || 
    String(item.value).toLowerCase().includes(keyword)
  )
})

// 监听值变化
watch(() => props.value, (newValue) => {
  selectedValue.value = newValue
})

// 获取选项标签
const getLabel = (value: string | number) => {
  const option = options.value.find(item => item.value === value)
  return option?.label ?? value
}

// 获取多个选项标签
const getLabels = (values: (string | number)[]) => {
  return values.map(value => getLabel(value))
}

// 获取标签颜色
const getTagColor = (value: string | number) => {
  return props.tagColors[value] ?? 'default'
}

// 获取文本样式
const getTextClass = (value: string | number) => {
  return props.textClasses[value] ?? ''
}

// Select值变化处理
const handleSelectChange = (value: SelectValue, option: any) => {
  const newValue = value as string | number | (string | number)[]
  selectedValue.value = newValue
  emit('update:value', newValue)
  emit('change', newValue)
}

// Radio值变化处理
const handleRadioChange = (e: RadioChangeEvent) => {
  const newValue = e.target.value as string | number
  selectedValue.value = newValue
  emit('update:value', newValue)
  emit('change', newValue)
}

// Checkbox值变化处理
const handleCheckboxChange = (checkedValue: CheckboxValueType[]) => {
  const newValue = checkedValue.map(v => Number(v))
  selectedValue.value = newValue
  emit('update:value', newValue)
  emit('change', newValue)
}

// 加载字典数据
const loadDictData = async (isAppend = false) => {
  if (!hasMore.value && isAppend) return
  
  loading.value = true
  try {
    const { data } = await getHbtDictDataList({
      pageNum: pageNum.value,
      pageSize: props.pageSize,
      dictTypeId: Number(props.dictType),
      status: 0, // 只获取正常状态的字典数据
      keyword: searchText.value
    })
    if (data?.code === 200) {
      const newOptions = data.data.rows.map((item: HbtDictData) => ({
        label: item.dictLabel,
        value: item.dictValue,
        disabled: item.status === 1
      }))
      
      if (isAppend) {
        options.value = [...options.value, ...newOptions]
      } else {
        options.value = newOptions
      }
      
      hasMore.value = data.data.rows.length === props.pageSize
      if (isAppend) {
        pageNum.value++
      }
    }
  } catch (error) {
    console.error('加载字典数据失败:', error)
  } finally {
    loading.value = false
  }
}

// 搜索处理
const handleSearch = debounce((value: string) => {
  if (value.length < props.minSearchLength) {
    if (props.type === 'select' && !dropdownVisible.value) return
    searchText.value = ''
    pageNum.value = 1
    hasMore.value = true
    loadDictData()
    return
  }
  
  searchText.value = value
  pageNum.value = 1
  hasMore.value = true
  loadDictData()
}, props.searchDebounce)

// 滚动加载处理
const handlePopupScroll = (e: Event) => {
  const target = e.target as HTMLElement
  if (target.scrollTop + target.clientHeight >= target.scrollHeight - 20) {
    loadDictData(true)
  }
}

// 下拉框显示状态变化处理
const handleDropdownVisibleChange = (visible: boolean) => {
  dropdownVisible.value = visible
  if (visible && !searchText.value && !options.value.length) {
    loadDictData()
  }
}

// 组件挂载时加载数据
onMounted(() => {
  if (props.type !== 'select') {
    loadDictData()
  }
})
</script>

<style lang="less" scoped>
.hbt-dict {
  display: inline-block;

  .ant-select {
    min-width: 120px;
  }

  .radio-container,
  .checkbox-container {
    max-height: 300px;
    overflow: auto;

    .loading-container,
    .empty-container {
      padding: 32px;
      text-align: center;
      color: rgba(0, 0, 0, 0.25);
    }

    .radio-list,
    .checkbox-list {
      .ant-radio-wrapper,
      .ant-checkbox-wrapper {
        display: block;
        margin: 8px 0;
      }
    }
  }

  .ant-radio-group,
  .ant-checkbox-group {
    display: inline-block;
  }
}
</style> 