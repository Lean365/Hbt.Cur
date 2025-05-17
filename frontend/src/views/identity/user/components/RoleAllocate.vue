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
      :default-expand-all="true"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TransferItem } from 'ant-design-vue/es/transfer'
import type { Role } from '@/types/identity/role'
import { getRoleOptions } from '@/api/identity/role'
import { getUserRoles } from '@/api/identity/user'
import { allocateUserRole } from '@/api/identity/userRole'
import { useI18n } from 'vue-i18n'

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

const i18n = useI18n()

// 角色列表
const roleList = ref<TransferItem[]>([])
// 已选角色
const targetKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)

// 加载角色列表
const loadRoleList = async () => {
  try {
    const res = await getRoleOptions()
    roleList.value = res.data.data.map(role => ({
      key: role.value.toString(),
      title: role.label,
      description: ''
    }))
  } catch (err) {
    console.error('加载角色列表失败:', err)
  }
}

// 加载用户角色
const loadUserRoles = async () => {
  if (!props.userId) return
  try {
    const { data } = await getUserRoles(props.userId)
    targetKeys.value = data.data.map(role => role.roleId.toString())
  } catch (err) {
    console.error('加载用户角色失败:', err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.userId) return
  try {
    const res = await allocateUserRole({
      userId: props.userId,
      roleIds: targetKeys.value.map(key => parseInt(key))
    })
    if (res.data.code === 200) {
      message.success(i18n.t('common.success'))
      emit('success')
      targetKeys.value = []
      emit('update:visible', false)
    } else {
      message.error(res.data.msg || i18n.t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(i18n.t('common.failed'))
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