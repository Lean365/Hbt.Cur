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
    :title="t('core.language.detail.title')"
    :open="open"
    width="800px"
    @cancel="handleCancel"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('core.language.fields.langCode.label')">
        {{ detail?.langCode }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.langName.label')">
        {{ detail?.langName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.langIcon.label')">
        {{ detail?.langIcon }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.orderNum.label')">
        {{ detail?.orderNum }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.isBuiltin.label')">
        <hbt-dict-tag dict-type="sys_yes_no" :value="detail?.isBuiltin ?? 0" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.isDefault.label')">
        <hbt-dict-tag dict-type="sys_yes_no" :value="detail?.isDefault ?? 0" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.status.label')">
        <hbt-dict-tag dict-type="sys_normal_disable" :value="detail?.status ?? 0" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.remark.label')">
        {{ detail?.remark }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.createBy.label')">
        {{ detail?.createBy }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.createTime.label')">
        {{ detail?.createTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.updateBy.label')">
        {{ detail?.updateBy }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.language.fields.updateTime.label')">
        {{ detail?.updateTime }}
      </a-descriptions-item>
    </a-descriptions>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtLanguage } from '@/types/core/language'
import { getHbtLanguage } from '@/api/core/language'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  languageId: number
}>()

const emit = defineEmits(['update:open'])

const detail = ref<HbtLanguage>()

const fetchDetail = async () => {
  try {
    const res = await getHbtLanguage(props.languageId)
    if (res.data.code === 200) {
      detail.value = res.data.data
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取语言详情失败:', error)
    message.error(t('common.failed'))
  }
}

const handleCancel = () => {
  emit('update:open', false)
}

watch(
  () => props.open,
  (val) => {
    if (val && props.languageId) {
      fetchDetail()
    }
  }
)

watch(
  () => props.languageId,
  (val) => {
    if (props.open && val) {
      fetchDetail()
    }
  }
)
</script>

<style lang="less" scoped>
:deep(.ant-descriptions-item-label) {
  width: 120px;
  text-align: right;
}
</style> 