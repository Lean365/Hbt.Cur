//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : TranslationDetail.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译详情组件
//===================================================================

<template>
  <hbt-modal
    :title="t('core.translation.actions.detail')"
    :open="open"
    :footer="null"
    width="600px"
    @cancel="handleCancel"
  >
    <a-descriptions
      :column="2"
      bordered
      class="translation-detail"
    >
      <a-descriptions-item :label="t('core.translation.fields.langCode.label')">
        {{ translation.langCode }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.translation.fields.transKey.label')">
        {{ translation.transKey }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.translation.fields.transValue.label')">
        {{ translation.transValue }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.translation.fields.remark.label')">
        {{ translation.remark }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.createBy')">
        {{ translation.createBy }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.createTime')">
        {{ translation.createTime }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.updateBy')">
        {{ translation.updateBy }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.updateTime')">
        {{ translation.updateTime }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.isDeleted')">
        <hbt-dict-tag dict-type="sys_yes_no" :value="translation.isDeleted" />
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.deleteBy')">
        {{ translation.deleteBy }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.deleteTime')">
        {{ translation.deleteTime }}
      </a-descriptions-item>
    </a-descriptions>
    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel">{{ t('common.button.cancel') }}</a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtTranslation } from '@/types/core/translation'
import { getHbtTranslation } from '@/api/core/translation'

const { t } = useI18n()

const props = defineProps({
  open: {
    type: Boolean,
    default: false
  },
  translationId: {
    type: Number,
    required: false
  }
})

const emit = defineEmits(['update:open'])

const translation = ref<HbtTranslation>({
  translationId: 0,
  langCode: '',
  moduleName: '',
  transKey: '',
  transValue: '',
  orderNum: 0,
  status: 0,
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  isDeleted: 0,
  deleteBy: '',
  deleteTime: ''
})

const loadTranslationDetail = async () => {
  if (!props.translationId) {
    message.error(t('common.message.paramError'))
    return
  }
  
  try {
    const res = await getHbtTranslation(props.translationId)
    if (res.data.code === 200) {
      translation.value = res.data.data
    } else {
      message.error(res.data.msg || t('common.message.queryFailed'))
    }
  } catch (error) {
    console.error('加载翻译详情失败:', error)
    message.error(t('common.message.queryFailed'))
  }
}

const handleCancel = () => {
  emit('update:open', false)
}

// 监听visible和translationId的变化
watch(
  () => [props.open, props.translationId],
  ([newOpen, newTranslationId]) => {
    if (newOpen && newTranslationId) {
      loadTranslationDetail()
    }
  },
  { immediate: true }
)

onMounted(() => {
  if (props.open && props.translationId) {
    loadTranslationDetail()
  }
})
</script>

<style lang="less" scoped>
.translation-detail {
  padding: 24px;
}
</style> 