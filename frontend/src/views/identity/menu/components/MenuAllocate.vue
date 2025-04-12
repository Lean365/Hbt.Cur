<template>
  <a-modal
    :visible="visible"
    :title="title"
    :width="800"
    @ok="handleOk"
    @cancel="handleCancel"
    @update:visible="(val) => emit('update:visible', val)"
  >
    <a-spin :spinning="loading">
      <a-tree
        v-model:checkedKeys="checkedKeys"
        v-model:selectedKeys="selectedKeys"
        :tree-data="treeData"
        checkable
        :defaultExpandAll="true"
      />
    </a-spin>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TreeProps } from 'ant-design-vue'
import type { Menu } from '@/types/identity/menu'

const props = defineProps<{
  visible: boolean
  title?: string
  menuIds?: number[]
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const loading = ref(false)
const treeData = ref<TreeProps['treeData']>([])
const checkedKeys = ref<number[]>([])
const selectedKeys = ref<number[]>([])

watch(
  () => props.visible,
  (val) => {
    if (val && props.menuIds) {
      checkedKeys.value = props.menuIds
    }
  }
)

const handleOk = async () => {
  emit('success')
  emit('update:visible', false)
}

const handleCancel = () => {
  emit('update:visible', false)
}
</script> 