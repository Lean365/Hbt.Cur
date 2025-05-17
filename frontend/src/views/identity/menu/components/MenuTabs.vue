<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :width="800"
    @update:open="handleVisibleChange"
    @ok="handleOk"
    
  >
    <a-tabs v-model:activeKey="activeKey">
      <a-tab-pane key="directory" tab="目录">
        <directory-form
          ref="directoryFormRef"
          :visible="true"
          :title="title"
          :menu-id="menuId"
        />
      </a-tab-pane>
      <a-tab-pane key="menu" tab="菜单">
        <menu-form
          ref="menuFormRef"
          :visible="true"
          :title="title"
          :menu-id="menuId"
        />
      </a-tab-pane>
      <a-tab-pane key="button" tab="按钮">
        <button-form
          ref="buttonFormRef"
          :visible="true"
          :title="title"
          :menu-id="menuId"
        />
      </a-tab-pane>
    </a-tabs>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import DirectoryForm from './DirectoryForm.vue'
import MenuForm from './MenuForm.vue'
import ButtonForm from './ButtonForm.vue'
import { message } from 'ant-design-vue'

const props = defineProps<{
  visible: boolean
  title: string
  menuId?: number
}>()

const emit = defineEmits(['update:visible', 'success'])

// 当前激活的标签页
const activeKey = ref('directory')

const directoryFormRef = ref()
const menuFormRef = ref()
const buttonFormRef = ref()

function handleVisibleChange(val: boolean) {
  emit('update:visible', val)
}

async function handleOk() {
  try {
    if (activeKey.value === 'directory') {
      await directoryFormRef.value?.submitForm?.()
    } else if (activeKey.value === 'menu') {
      await menuFormRef.value?.submitForm?.()
    } else if (activeKey.value === 'button') {
      await buttonFormRef.value?.submitForm?.()
    }
    emit('success')
    emit('update:visible', false)
  } catch (e: any) {
    message.error(e?.message || '保存失败，请检查表单内容')
  }
}


</script>

<style scoped>
.menu-tabs {
  background: #fff;
  padding: 16px;
}

.menu-tabs :deep(.ant-tabs-nav) {
  margin-bottom: 16px;
}

.menu-tabs :deep(.ant-tabs-tab) {
  padding: 12px 16px;
  margin: 0 32px 0 0;
}

.menu-tabs :deep(.ant-tabs-tab-btn) {
  font-size: 14px;
  color: rgba(0, 0, 0, 0.85);
}

.menu-tabs :deep(.ant-tabs-tab-active .ant-tabs-tab-btn) {
  color: #1890ff;
}

.menu-tabs :deep(.ant-tabs-ink-bar) {
  background: #1890ff;
}
</style> 