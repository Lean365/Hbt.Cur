<template>
  <a-modal
    :visible="visible"
    title="分配数据权限"
    :confirm-loading="confirmLoading"
    @update:visible="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-tree
      v-model:checkedKeys="checkedKeys"
      :tree-data="treeData"
      :field-names="{
        children: 'children',
        title: 'deptName',
        key: 'deptId'
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
import type { Dept } from '@/types/identity/dept'
import { getDeptTree } from '@/api/identity/dept'
import { getRoleDeptList, allocateRoleDept } from '@/api/identity/roleDept'

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

// 部门树数据
const deptTree = ref<Dept[]>([])
// 转换后的树形数据
const treeData = ref<TreeDataItem[]>([])
// 选中的部门ID
const checkedKeys = ref<number[]>([])
// 确认加载
const confirmLoading = ref(false)

// 转换树形数据
const transformTreeData = (depts: Dept[]): TreeDataItem[] => {
  return depts.map(dept => ({
    key: dept.deptId || 0,
    title: dept.deptName,
    children: dept.children ? transformTreeData(dept.children) : undefined
  }))
}

// 加载部门树数据
const loadDeptTree = async () => {
  try {
    const { data } = await getDeptTree()
    deptTree.value = data
    treeData.value = transformTreeData(data)
  } catch (err) {
    console.error('加载部门树失败:', err)
  }
}

// 加载角色部门
const loadRoleDepts = async () => {
  if (!props.roleId) return
  try {
    const { data } = await getRoleDeptList(props.roleId)
    checkedKeys.value = data
  } catch (err) {
    console.error('加载角色部门失败:', err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.roleId) return
  try {
    confirmLoading.value = true
    await allocateRoleDept({
      roleId: props.roleId,
      deptIds: checkedKeys.value
    })
    message.success('分配成功')
    emit('success')
    emit('update:visible', false)
  } catch (err) {
    console.error('分配部门失败:', err)
  } finally {
    confirmLoading.value = false
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  (val) => {
    if (val) {
      loadDeptTree()
      loadRoleDepts()
    } else {
      checkedKeys.value = []
    }
  }
)
</script> 