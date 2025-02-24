//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue  
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 通用分页组件
//===================================================================

<template>
  <div class="hbt-pagination" :class="align">
    <a-pagination
      v-model:current="currentPage"
      v-model:pageSize="pageSize"
      :total="total"
      :show-total="showTotal ? (total) => t('pagination.total', { total }) : undefined"
      :show-size-changer="showSizeChanger"
      :show-quick-jumper="showQuickJumper"
      :disabled="disabled"
      :page-size-options="pageSizeOptions"
      :size="size"
      :simple="simple"
      @change="handleChange"
      @showSizeChange="handleSizeChange"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'

interface Props {
  /** 当前页码 */
  current?: number
  /** 每页条数 */
  pageSize?: number
  /** 数据总数 */
  total: number
  /** 是否显示快速跳转 */
  showQuickJumper?: boolean
  /** 是否显示每页条数选择器 */
  showSizeChanger?: boolean
  /** 是否显示总数 */
  showTotal?: boolean
  /** 是否禁用 */
  disabled?: boolean
  /** 每页条数选项 */
  pageSizeOptions?: string[]
  /** 组件大小 */
  size?: 'small' | 'default'
  /** 是否使用简单模式 */
  simple?: boolean
  /** 对齐方式 */
  align?: 'left' | 'center' | 'right'
}

const props = withDefaults(defineProps<Props>(), {
  current: 1,
  pageSize: 10,
  showQuickJumper: true,
  showSizeChanger: true,
  showTotal: true,
  disabled: false,
  pageSizeOptions: () => ['10', '20', '50', '100'],
  size: 'default',
  simple: false,
  align: 'right'
})

const emit = defineEmits<{
  (e: 'update:current', page: number): void
  (e: 'update:pageSize', size: number): void
  (e: 'change', page: number, pageSize: number): void
}>()

// 使用vue-i18n
const { t } = useI18n()

// 当前页码
const currentPage = ref(props.current)
const pageSize = ref(props.pageSize)

// 监听props变化
watch(() => props.current, (val) => {
  currentPage.value = val
})

watch(() => props.pageSize, (val) => {
  pageSize.value = val
})

// 页码改变事件
const handleChange = (page: number, size: number) => {
  emit('update:current', page)
  emit('update:pageSize', size)
  emit('change', page, size)
}

// 每页条数改变事件
const handleSizeChange = (current: number, size: number) => {
  emit('update:pageSize', size)
  emit('change', current, size)
}
</script>

<style scoped>
.hbt-pagination {
  margin: 16px 0;
}

.hbt-pagination.left {
  text-align: left;
}

.hbt-pagination.center {
  text-align: center;
}

.hbt-pagination.right {
  text-align: right;
}
</style> 