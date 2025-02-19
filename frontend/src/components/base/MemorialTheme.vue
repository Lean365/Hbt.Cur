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

<style lang="less" scoped>
.memorial-dropdown {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-dropdown-trigger) {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-btn) {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 32px;
  width: 32px;
  padding: 0;
}

:deep(.anticon) {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
  line-height: 1;
}
</style> 