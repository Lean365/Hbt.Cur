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
    :title="t('admin.language.detail')"
    :open="dialogVisible"
    @update:open="handleClose"
  >
    <a-descriptions
      :column="2"
      bordered
      class="language-detail"
    >
      <a-descriptions-item :label="t('admin.language.langCode')">
        {{ language.langCode }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('admin.language.langName')">
        {{ language.langName }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('admin.language.langIcon')">
        {{ language.langIcon }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('admin.language.orderNum')">
        {{ language.orderNum }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('admin.language.isDefault')">
        <a-tag :color="language.isDefault === 1 ? 'success' : 'default'">
          {{ language.isDefault === 1 ? t('common.yes') : t('common.no') }}
        </a-tag>
      </a-descriptions-item>

      <a-descriptions-item :label="t('admin.language.status')">
        <a-tag :color="language.status === 0 ? 'success' : 'error'">
          {{ language.status === 0 ? t('common.normal') : t('common.disabled') }}
        </a-tag>
      </a-descriptions-item>

      <a-descriptions-item :label="t('admin.language.remark')" :span="2">
        {{ language.remark }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('common.createTime')">
        {{ language.createTime }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('common.updateTime')">
        {{ language.updateTime }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('common.createBy')">
        {{ language.createBy }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('common.updateBy')">
        {{ language.updateBy }}
      </a-descriptions-item>
    </a-descriptions>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { HbtLanguage } from '@/types/admin/language'
import { getHbtLanguage } from '@/api/admin/language'

const { t } = useI18n()

const props = defineProps<{
  languageId: number
  dialogVisible: boolean
}>()

const emit = defineEmits<{
  (e: 'update:dialogVisible', value: boolean): void
}>()

const language = ref<HbtLanguage>({
    languageId: 0,
  langCode: '',
  langName: '',
  langIcon: '',
  orderNum: 0,
  isDefault: 0,
  status: 0,
  id: 0,
  createBy: '',
  createTime: '',
  isDeleted: 0,
  remark: '',
  updateTime: '',
  updateBy: ''
})

const loadLanguageDetail = async () => {
  if (props.languageId) {
    const res = await getHbtLanguage(props.languageId)
    if (res.code === 200) {
      language.value = res.data
    }
  }
}

const handleClose = () => {
  emit('update:dialogVisible', false)
}

onMounted(() => {
  if (props.dialogVisible) {
    loadLanguageDetail()
  }
})
</script>

<style lang="less" scoped>
.language-detail {
  padding: 24px;
}
</style> 