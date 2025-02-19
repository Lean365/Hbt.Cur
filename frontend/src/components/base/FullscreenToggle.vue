<template>
  <a-tooltip :title="isFullscreen ? t('header.fullscreen.exit') : t('header.fullscreen.enter')">
    <a-button type="text" @click="toggleFullscreen">
      <template #icon>
        <fullscreen-exit-outlined v-if="isFullscreen" />
        <fullscreen-outlined v-else />
      </template>
    </a-button>
  </a-tooltip>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { FullscreenOutlined, FullscreenExitOutlined } from '@ant-design/icons-vue'

const { t } = useI18n()
const isFullscreen = ref(false)

const toggleFullscreen = () => {
  if (!document.fullscreenElement) {
    document.documentElement.requestFullscreen()
    isFullscreen.value = true
  } else {
    document.exitFullscreen()
    isFullscreen.value = false
  }
}

const handleFullscreenChange = () => {
  isFullscreen.value = !!document.fullscreenElement
}

onMounted(() => {
  document.addEventListener('fullscreenchange', handleFullscreenChange)
})

onUnmounted(() => {
  document.removeEventListener('fullscreenchange', handleFullscreenChange)
})
</script>

<style scoped>
:deep(.anticon) {
  font-size: 16px;
}
</style> 