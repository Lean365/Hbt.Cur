<template>
  <a-tag v-if="finalLabel" :class="['hbt-dict-tag', tagClass]">{{ finalLabel }}</a-tag>
  <a-tag v-else color="default" class="hbt-dict-tag">{{ t('common.unknown') }}</a-tag>
</template>

<script lang="ts" setup>
import { computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { useDictStore } from '@/stores/dict'
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
const dictStore = useDictStore()

// 计算最终显示的标签
const finalLabel = computed(() => {
  const label = dictStore.getDictLabel(props.dictType, Number(props.value))
  if (props.useI18n) {
    const transKey = dictStore.getDictTransKey(props.dictType, Number(props.value))
    return transKey ? t(transKey) : label
  }
  return label
})

// 计算标签样式类
const tagClass = computed(() => {
  const baseClass = dictStore.getDictClass(props.dictType, Number(props.value))
  return baseClass ? `hbt-dict-tag-${baseClass}` : ''
})

onMounted(() => {
  if (props.dictType) {
    dictStore.loadDict(props.dictType)
  }
})

const dictClass = computed(() => props.dictType ? dictStore.getDictClass(props.dictType, props.value) : '')
const dictLabel = computed(() => props.dictType ? dictStore.getDictLabel(props.dictType, props.value) : props.value)
</script> 