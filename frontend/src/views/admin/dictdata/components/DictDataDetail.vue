<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: DictDataDetail.vue
创建日期: 2024-03-20
描述: 字典数据详情组件
=================================================================== 
-->

<template>
  <hbt-modal
    title="字典数据详情"
    :visible="visible"
    :loading="loading"
    :width="500"
    :footer="null"
    @cancel="handleClose"
  >
    <a-descriptions :column="1" bordered>
      <a-descriptions-item label="字典类型">
        {{ model?.dictType }}
      </a-descriptions-item>
      <a-descriptions-item label="字典标签">
        {{ model?.dictLabel }}
      </a-descriptions-item>
      <a-descriptions-item label="字典键值">
        {{ model?.dictValue }}
      </a-descriptions-item>
      <a-descriptions-item label="字典排序">
        {{ model?.dictSort }}
      </a-descriptions-item>
      <a-descriptions-item label="状态">
        <a-tag :color="model?.status ? 'success' : 'error'">
          {{ model?.status ? t('common.status.normal') : t('common.status.disabled') }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="样式属性">
        {{ model?.cssClass }}
      </a-descriptions-item>
      <a-descriptions-item label="回显样式">
        {{ model?.listClass }}
      </a-descriptions-item>
      <a-descriptions-item label="是否默认">
        {{ model?.isDefault === 'Y' ? '是' : '否' }}
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
      <a-descriptions-item label="创建者">
        {{ model?.createBy }}
      </a-descriptions-item>
      <a-descriptions-item label="更新者">
        {{ model?.updateBy }}
      </a-descriptions-item>
    </a-descriptions>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import type { HbtDictData } from '@/types/admin/dictData'

const { t } = useI18n()

// === Props 定义 ===
interface Props {
  visible: boolean
  loading?: boolean
  model?: HbtDictData
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  loading: false
})

// === Emits 定义 ===
const emit = defineEmits<{
  'update:visible': [value: boolean]
  'close': []
}>()

// === 方法定义 ===
const handleClose = () => {
  emit('update:visible', false)
  emit('close')
}
</script> 