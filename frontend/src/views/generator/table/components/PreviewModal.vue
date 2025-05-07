<template>
  <a-modal
    :open="open"
    title="代码预览"
    width="80%"
    :footer="null"
    @cancel="handleCancel"
  >
    <a-spin :spinning="loading">
      <a-tabs v-model:activeKey="activeKey">
        <a-tab-pane key="controllers" tab="Controllers">
          <controllers-preview :code="previewData.controllers" />
        </a-tab-pane>
        <a-tab-pane key="services" tab="Services">
          <services-preview :code="previewData.services" />
        </a-tab-pane>
        <a-tab-pane key="entities" tab="Entities">
          <entities-preview :code="previewData.entities" />
        </a-tab-pane>
        <a-tab-pane key="dtos" tab="Dtos">
          <dtos-preview :code="previewData.dtos" />
        </a-tab-pane>
        <a-tab-pane key="views" tab="Views">
          <views-preview :code="previewData.views" />
        </a-tab-pane>
        <a-tab-pane key="types" tab="Types">
          <types-preview :code="previewData.types" />
        </a-tab-pane>
        <a-tab-pane key="locales" tab="Locales">
          <locales-preview :code="previewData.locales" />
        </a-tab-pane>
      </a-tabs>
    </a-spin>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import ControllersPreview from './preview/ControllersPreview.vue'
import ServicesPreview from './preview/ServicesPreview.vue'
import EntitiesPreview from './preview/EntitiesPreview.vue'
import DtosPreview from './preview/DtosPreview.vue'
import ViewsPreview from './preview/ViewsPreview.vue'
import TypesPreview from './preview/TypesPreview.vue'
import LocalesPreview from './preview/LocalesPreview.vue'

defineProps<{
  open: boolean
  loading: boolean
  previewData: Record<string, string>
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
}>()

// 当前激活的标签页
const activeKey = ref('controllers')

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:open', false)
}
</script>

<style lang="less" scoped>
:deep(.ant-tabs-content) {
  height: 600px;
  overflow-y: auto;
}

:deep(.ant-tabs-tabpane) {
  padding: 16px 0;
}
</style> 