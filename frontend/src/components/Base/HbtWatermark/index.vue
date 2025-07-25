<template>
  <a-watermark 
    v-if="shouldShowWatermark"
    :content="watermarkConfig.content"
    :gap="watermarkConfig.gap"
    :rotate="watermarkConfig.rotate"
    :opacity="watermarkConfig.opacity"
    :font="watermarkFont"
    :zIndex="9999"
  >
    <div style="width: 100%; height: 100%;">
      <slot />
    </div>
  </a-watermark>
  
  <div v-else style="width: 100%; height: 100%;">
    <slot />
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useConfigStore } from '@/stores/config'

const configStore = useConfigStore()

const shouldShowWatermark = computed(() => {
  return configStore.isShowWatermark && configStore.watermarkEnabled
})

const watermarkConfig = computed(() => configStore.watermarkConfig)

const watermarkFont = computed(() => ({
  fontSize: watermarkConfig.value.fontSize,
  fontFamily: 'Arial, sans-serif'
}))
</script>