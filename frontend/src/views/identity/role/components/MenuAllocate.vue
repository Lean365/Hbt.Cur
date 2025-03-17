<template>
  <a-modal
    :visible="visible"
    title="分配菜单权限"
    :confirm-loading="confirmLoading"
    @update:visible="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-tree
      v-model:checkedKeys="checkedKeys"
      :tree-data="treeData"
      :field-names="{
        children: 'children',
        title: 'menuName',
        key: 'menuId'
      }"
      checkable
      :default-expand-all="true"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TreeDataItem } from 'ant-design-vue/es/tree'
import type { Menu } from '@/types/identity/menu'
import { getMenuTree } from '@/api/identity/menu'
import { getRoleMenuList, allocateRoleMenu } from '@/api/identity/roleMenu'

interface Props {
  visible: boolean
  roleId?: number
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  roleId: undefined
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 菜单树数据
const menuTree = ref<Menu[]>([])
// 转换后的树形数据
const treeData = ref<TreeDataItem[]>([])
// 选中的菜单ID
const checkedKeys = ref<number[]>([])
// 确认加载
const confirmLoading = ref(false)

// 转换树形数据
const transformTreeData = (menus: Menu[]): TreeDataItem[] => {
  return menus.map(menu => ({
    key: menu.menuId,
    title: menu.menuName,
    children: menu.children ? transformTreeData(menu.children) : undefined
  }))
}

// 加载菜单树数据
const loadMenuTree = async () => {
  try {
    const { data } = await getMenuTree()
    menuTree.value = data
    treeData.value = transformTreeData(data)
  } catch (err) {
    console.error('加载菜单树失败:', err)
  }
}

// 加载角色菜单
const loadRoleMenus = async () => {
  if (!props.roleId) return
  try {
    const { data } = await getRoleMenuList(props.roleId)
    checkedKeys.value = data
  } catch (err) {
    console.error('加载角色菜单失败:', err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.roleId) return
  try {
    confirmLoading.value = true
    await allocateRoleMenu({
      roleId: props.roleId,
      menuIds: checkedKeys.value
    })
    message.success('分配成功')
    emit('success')
    emit('update:visible', false)
  } catch (err) {
    console.error('分配菜单失败:', err)
  } finally {
    confirmLoading.value = false
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  (val) => {
    if (val) {
      loadMenuTree()
      loadRoleMenus()
    } else {
      checkedKeys.value = []
    }
  }
)
</script> 