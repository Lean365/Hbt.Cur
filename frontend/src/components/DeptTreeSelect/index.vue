<template>
  <a-tree-select
    v-model:value="value"
    :tree-data="treeData"
    :placeholder="t('identity.dept.parentDept.placeholder')"
    :loading="loading"
    :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }"
    :field-names="{
      children: 'children',
      label: 'label',
      value: 'id'
    }"
    allow-clear
    show-search
    tree-default-expand-all
  />
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { TreeSelectProps } from 'ant-design-vue'
import type { DeptTreeNode } from '@/types/identity/dept'
import { listDeptTree } from '@/api/identity/dept'

const props = defineProps<{
  value?: number
}>()

const emit = defineEmits<{
  (e: 'update:value', value: number | undefined): void
}>()

const { t } = useI18n()

// 加载状态
const loading = ref(false)

// 树形数据
const treeData = ref<DeptTreeNode[]>([])

// 加载部门树形数据
const loadTreeData = async () => {
  loading.value = true
  try {
    const res = await listDeptTree()
    treeData.value = res.data
  } catch (error) {
    console.error(error)
  }
  loading.value = false
}

// 组件挂载时加载数据
onMounted(() => {
  loadTreeData()
})

// 处理值变化
const value = ref<number | undefined>(props.value)
watch(
  () => props.value,
  (val) => {
    value.value = val
  }
)
watch(
  () => value.value,
  (val) => {
    emit('update:value', val)
  }
)
</script> 