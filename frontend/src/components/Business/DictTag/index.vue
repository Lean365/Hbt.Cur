<template>
  <a-tag v-if="finalLabel" :class="['hbt-dict-tag', tagClass]">{{ finalLabel }}</a-tag>
  <a-tag v-else color="default" class="hbt-dict-tag">{{ t('common.unknown') }}</a-tag>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useDictData } from '@/hooks/useDictData'
import '@/assets/styles/dict-tag.less'

const { t } = useI18n()

interface Props {
  // 字典类型
  dictType: string
  // 字典值
  value: number | string
  // 是否使用国际化
  useI18n?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  useI18n: false
})

// 使用字典Hook
const { getDictLabel, getDictClass, getDictTransKey } = useDictData([props.dictType])

// 计算最终显示的标签
const finalLabel = computed(() => {
  const label = getDictLabel(props.dictType, Number(props.value))
  if (props.useI18n) {
    const transKey = getDictTransKey(props.dictType, Number(props.value))
    return transKey ? t(transKey) : label
  }
  return label
})

// 计算标签样式类
const tagClass = computed(() => {
  const baseClass = getDictClass(props.dictType, Number(props.value))
  return baseClass ? `hbt-dict-tag-${baseClass}` : ''
})
</script> 