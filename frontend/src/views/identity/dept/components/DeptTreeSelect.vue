<template>
  <a-tree-select
    :value="modelValue"
    :tree-data="treeData"
    :field-names="{
      children: 'children',
      label: 'deptName',
      value: 'deptId'
    }"
    :placeholder="t('identity.dept.fields.parentId.placeholder')"
    :loading="loading"
    :disabled="disabled"
    :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }"
    allow-clear
    tree-default-expand-all
    @update:value="(value: number | undefined) => emit('update:modelValue', value)"
  />
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { Dept } from '@/types/identity/dept'
import { getDeptTree } from '@/api/identity/dept'

const { t } = useI18n()

const props = defineProps<{
  modelValue?: number
  exclude?: number
  disabled?: boolean
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value?: number): void
}>()

// 加载状态
const loading = ref(false)

// 树形数据
const treeData = ref<Dept[]>([])

// 过滤树形数据
const filterTree = (tree: Dept[], excludeId?: number): Dept[] => {
  if (!excludeId) return tree

  return tree
    .filter((node) => node.deptId !== excludeId)
    .map((node) => ({
      ...node,
      children: node.children ? filterTree(node.children, excludeId) : undefined
    }))
}

// 加载树形数据
const loadTreeData = async () => {
  try {
    loading.value = true
    const res = await getDeptTree()
    if (res.data.code === 200) {
      treeData.value = filterTree(res.data.data, props.exclude)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('[部门管理] 获取部门树形数据出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  loadTreeData()
})
</script> 