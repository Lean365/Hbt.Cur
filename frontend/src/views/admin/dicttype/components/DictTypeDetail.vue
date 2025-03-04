<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: DictTypeDetail.vue
创建日期: 2024-03-20
描述: 字典类型详情对话框组件
=================================================================== 
-->

<template>
  <hbt-modal
    :open="open"
    :title="t('dictType.detail.title')"
    :width="500"
    :footer="null"
    @update:open="(val) => emit('update:open', val)"
    @cancel="handleClose"
  >
    <a-descriptions :column="1" bordered>
      <a-descriptions-item :label="t('dictType.form.name')">
        {{ model?.dictName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('dictType.form.type')">
        {{ model?.dictType }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('dictType.form.category')">
        {{ model?.dictCategory === 0 ? t('dictType.category.system') : t('dictType.category.sql') }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('dictType.form.orderNum')">
        {{ model?.orderNum }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('dictType.form.status')">
        <a-tag :color="model?.status === 0 ? 'success' : 'error'">
          {{ model?.status === 0 ? t('common.status.normal') : t('common.status.disabled') }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item :label="t('dictType.form.remark')">
        {{ model?.remark }}
      </a-descriptions-item>
    </a-descriptions>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import type { HbtDictType } from '@/types/admin/hbtDictType'
import HbtModal from '@/components/Business/Modal/index.vue'

const { t } = useI18n()

// === Props 定义 ===
interface Props {
  open: boolean
  loading?: boolean
  model?: HbtDictType
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  loading: false
})

// === Emits 定义 ===
const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'close'): void
}>()

// === 方法定义 ===
const handleClose = () => {
  emit('update:open', false)
  emit('close')
}
</script> 