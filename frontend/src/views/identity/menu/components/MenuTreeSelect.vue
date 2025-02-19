<template>
  <a-tree-select
    v-model:value="value"
    :tree-data="treeData"
    :placeholder="t('identity.menu.parentMenu.placeholder')"
    :loading="loading"
    :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }"
    :field-names="{
      children: 'children',
      label: 'menuName',
      value: 'menuId'
    }"
    allow-clear
    show-search
    tree-default-expand-all
  />
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { Menu } from '@/types/identity/menu'
import { getMenuTree } from '@/api/identity/menu'

const props = defineProps<{
  value?: number
}>()

const emit = defineEmits(['update:value'])

const { t } = useI18n()

// 加载状态
const loading = ref(false)

// 树形数据
const treeData = ref<Menu[]>([])

// 加载菜单树形数据
const loadTreeData = async () => {
  loading.value = true
  try {
    const res = await getMenuTree()
    if (res.code === 200 && res.data) {
      // 添加根节点
      treeData.value = [{
        menuId: 0,
        menuName: t('identity.menu.form.base.parentMenu.root'),
        children: res.data
      }]
    } else {
      console.error('加载菜单树失败:', res.msg)
      message.error(res.msg || t('common.message.loadFailed'))
    }
  } catch (error) {
    console.error('加载菜单树发生错误:', error)
    message.error(t('common.message.loadFailed'))
  } finally {
    loading.value = false
  }
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