<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: ConfigDetail.vue
创建日期: 2024-03-20
描述: 系统配置详情组件
=================================================================== 
-->

<template>
  <a-drawer
    :visible="visible"
    :title="t('common.title.detail')"
    placement="right"
    width="600"
    @close="handleClose"
  >
    <a-spin :spinning="loading">
      <a-descriptions :column="1" bordered>
        <a-descriptions-item label="配置名称">
          {{ model?.configName }}
        </a-descriptions-item>
        <a-descriptions-item label="配置键名">
          {{ model?.configKey }}
        </a-descriptions-item>
        <a-descriptions-item label="配置值">
          {{ model?.configValue }}
        </a-descriptions-item>
        <a-descriptions-item label="配置类型">
          <a-tag :color="model?.configType === 0 ? 'blue' : 'green'">
            {{ model?.configType === 0 ? '系统配置' : '业务配置' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="状态">
          <a-tag :color="model?.status ? 'success' : 'error'">
            {{ model?.status ? t('common.status.normal') : t('common.status.disabled') }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="备注">
          {{ model?.remark }}
        </a-descriptions-item>
        <a-descriptions-item label="创建时间">
          {{ model?.createTime }}
        </a-descriptions-item>
        <a-descriptions-item label="更新时间">
          {{ model?.updateTime }}
        </a-descriptions-item>
      </a-descriptions>
    </a-spin>
  </a-drawer>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import type { HbtConfig } from '@/types/admin/config'

const { t } = useI18n()

// === 属性定义 ===
interface Props {
  visible: boolean
  loading?: boolean
  model?: HbtConfig
}

const props = withDefaults(defineProps<Props>(), {
  loading: false
})

// === 事件定义 ===
const emit = defineEmits(['update:visible', 'close'])

// === 方法定义 ===
const handleClose = () => {
  emit('close')
}
</script> 