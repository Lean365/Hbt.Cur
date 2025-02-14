<template>
  <a-tooltip :title="isMemorialMode ? t('memorial.disable') : t('memorial.enable')">
    <a-button
      type="text"
      class="memorial-theme"
      @click="toggleMemorialMode"
    >
      <template #icon>
        <component :is="memorialIcon" />
      </template>
    </a-button>
  </a-tooltip>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { HeartOutlined, HeartFilled } from '@ant-design/icons-vue'
import { useMemorialStore } from '@/stores/memorial'

const { t } = useI18n()
const memorialStore = useMemorialStore()

const isMemorialMode = computed(() => memorialStore.isMemorialMode)
const memorialIcon = computed(() => isMemorialMode.value ? HeartFilled : HeartOutlined)

// 切换纪念模式
const toggleMemorialMode = () => {
  memorialStore.toggleMemorialMode()
}
</script>

<style scoped>
.memorial-theme {
  display: inline-flex;
  align-items: center;
  padding: 0 4px;
  color: inherit;
}

.memorial-theme :deep(.anticon) {
  font-size: 16px;
}
</style> 