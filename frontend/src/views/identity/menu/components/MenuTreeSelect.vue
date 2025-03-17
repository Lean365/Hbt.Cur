<template>
  <a-tree-select
    :value="modelValue"
    :tree-data="treeData"
    :field-names="{
      children: 'children',
      label: 'menuName',
      value: 'menuId'
    }"
    :placeholder="t('identity.menu.fields.parentMenu.placeholder')"
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
import type { Menu } from '@/types/identity/menu'
import type { HbtApiResponse } from '@/types/common'
import { getMenuTree } from '@/api/identity/menu'

const props = defineProps<{
  modelValue?: number
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value?: number): void
}>()

const { t } = useI18n()

// 树形数据
const treeData = ref<Menu[]>([])

// 加载菜单树数据
const loadTreeData = async () => {
  try {
    console.log('[菜单树选择] 开始加载菜单树数据')
    const res = await getMenuTree()
    console.log('[菜单树选择] API返回数据:', res)
    
    if (res.code === 200) {
      treeData.value = res.data
      console.log('[菜单树选择] 设置树形数据:', treeData.value)
    } else {
      console.error('[菜单树选择] 加载失败:', res.msg)
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('[菜单树选择] 加载出错:', error)
    message.error(t('common.failed'))
  }
}

// 初始化
onMounted(() => {
  loadTreeData()
})
</script>