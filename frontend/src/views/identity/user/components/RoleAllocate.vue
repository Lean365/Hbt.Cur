<template>
  <a-modal
    :open="visible"
    title="分配角色"
    :confirm-loading="confirmLoading"
    @update:open="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-transfer
      v-model:targetKeys="targetKeys"
      :data-source="roleList"
      :titles="['未分配', '已分配']"
      :render="(item) => item.title"
      :disabled="false"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TransferItem } from 'ant-design-vue/es/transfer'
import type { Role } from '@/types/identity/role'
import { getRoleList } from '@/api/identity/role'
import { getUserRoleList, allocateUserRole } from '@/api/identity/userRole'

interface Props {
  visible: boolean
  userId?: number
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  userId: undefined
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 角色列表
const roleList = ref<TransferItem[]>([])
// 已选角色
const targetKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)

// 加载角色列表
const loadRoleList = async () => {
  try {
    const { data } = await getRoleList()
    roleList.value = data.map(role => ({
      key: role.roleId.toString(),
      title: role.roleName,
      description: role.remark
    }))
  } catch (err) {
    console.error('加载角色列表失败:', err)
  }
}

// 加载用户角色
const loadUserRoles = async () => {
  if (!props.userId) return
  try {
    const { data } = await getUserRoleList(props.userId)
    targetKeys.value = data.map(id => id.toString())
  } catch (err) {
    console.error('加载用户角色失败:', err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.userId) return
  try {
    confirmLoading.value = true
    await allocateUserRole({
      userId: props.userId,
      roleIds: targetKeys.value.map(key => parseInt(key))
    })
    message.success('分配成功')
    emit('success')
    emit('update:visible', false)
  } catch (err) {
    console.error('分配角色失败:', err)
  } finally {
    confirmLoading.value = false
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  (val) => {
    if (val) {
      loadRoleList()
      loadUserRoles()
    } else {
      targetKeys.value = []
    }
  }
)
</script> 