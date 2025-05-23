//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : LanguageDetail.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言详情组件
//===================================================================

<template>
  <hbt-modal
    :title="t('core.language.detail') "
    :open="open"
    @update:open="handleCancel"
  >
    <a-descriptions
      :column="2"
      bordered
      class="language-detail"
    >
      <a-descriptions-item :label="t('core.language.table.columns.langCode')">
        {{ language.langCode }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.language.table.columns.langName')">
        {{ language.langName }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.language.table.columns.langIcon')">
        {{ language.langIcon }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.language.table.columns.orderNum')">
        {{ language.orderNum }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.language.table.columns.status')">
        <hbt-dict-tag dict-type="sys_normal_disable" :value="language.status" />
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.remark')">
        {{ language.remark }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.createBy')">
        {{ language.createBy }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.createTime')">
        {{ language.createTime }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.updateBy')">
        {{ language.updateBy }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.updateTime')">
        {{ language.updateTime }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.isDeleted')">
        <hbt-dict-tag dict-type="sys_yes_no" :value="language.isDeleted" />
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.deleteBy')">
        {{ language.deleteBy }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('table.columns.deleteTime')">
        {{ language.deleteTime }}
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
import type { HbtLanguage } from '@/types/core/language'
import { getHbtLanguage } from '@/api/core/language'

const { t } = useI18n()

const props = defineProps({
  open: {
    type: Boolean,
    default: false
  },
  languageId: {
    type: Number,
    required: true
  }
})

const emit = defineEmits(['update:open'])

const language = ref<HbtLanguage>({
  languageId: 0,
  langCode: '',
  langName: '',
  langIcon: '',
  orderNum: 0,
  isDefault: 0,
  isBuiltin: 0,
  status: 0,
  tenantId: 0,
  createBy: '',
  createTime: '',
  isDeleted: 0,
  remark: '',
  updateTime: '',
  updateBy: ''
})

const loadLanguageDetail = async () => {
  if (!props.languageId) {
    message.error(t('common.message.paramError'))
    return
  }
  
  try {
    const res = await getHbtLanguage(props.languageId)
    if (res.data.code === 200) {
      language.value = res.data.data
    } else {
      message.error(res.data.msg || t('common.message.queryFailed'))
    }
  } catch (error) {
    console.error('加载语言详情失败:', error)
    message.error(t('common.message.queryFailed'))
  }
}

const handleCancel = () => {
  emit('update:open', false)
}

// 监听visible和languageId的变化
watch(
  () => [props.open, props.languageId],
  ([newOpen, newLanguageId]) => {
    if (newOpen && newLanguageId) {
      loadLanguageDetail()
    }
  },
  { immediate: true }
)

onMounted(() => {
  if (props.open && props.languageId) {
    loadLanguageDetail()
  }
})
</script>

<style lang="less" scoped>
.language-detail {
  padding: 24px;
}
</style> 