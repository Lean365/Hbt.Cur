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
    :title="t('common.title.detail')"
    :width="500"
    :footer="null"
    @update:open="(val) => emit('update:open', val)"
    @cancel="handleClose"
  >
    <a-descriptions :column="1" bordered>
      <a-descriptions-item :label="t('admin.dicttype.form.name')">
        {{ model?.dictName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.type')">
        {{ model?.dictType }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.category')">
        <hbt-dict-tag dict-type="sys_dict_category" :value="model?.dictCategory ?? ''" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.builtin')">
        <hbt-dict-tag dict-type="sys_yes_no" :value="model?.dictBuiltin ?? ''" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.sqlScript')" v-if="model?.dictCategory === 1">
        {{ model?.sqlScript || '-' }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.orderNum')">
        {{ model?.orderNum }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.status')">
        <hbt-dict-tag dict-type="sys_normal_disable" :value="model?.status ?? ''" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.tenantId')">
        {{ model?.tenantId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('common.datetime.createTime')">
        {{ model?.createTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.dicttype.form.remark')">
        {{ model?.remark || '-' }}
      </a-descriptions-item>
    </a-descriptions>
    <div class="detail-footer">
      <a-button @click="handleClose">{{ t('common.actions.close') }}</a-button>
    </div>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import type { HbtDictType } from '@/types/admin/dictType'
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

<style lang="less" scoped>
.detail-footer {
  margin-top: 24px;
  text-align: right;
}
</style> 