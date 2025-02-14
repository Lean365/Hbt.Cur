<template>
  <a-tooltip :title="isDarkMode ? t('theme.light') : t('theme.dark')">
    <a-button
      type="text"
      class="theme-switch"
      @click="toggleTheme"
    >
      <template #icon>
        <component :is="themeIcon" />
      </template>
    </a-button>
  </a-tooltip>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { BulbOutlined, BulbFilled } from '@ant-design/icons-vue'
import { useThemeStore } from '@/stores/theme'

const { t } = useI18n()
const themeStore = useThemeStore()

const isDarkMode = computed(() => themeStore.isDarkMode)
const themeIcon = computed(() => isDarkMode.value ? BulbOutlined : BulbFilled)

// 切换主题
const toggleTheme = () => {
  themeStore.toggleTheme()
}
</script>

<style scoped>
.theme-switch {
  display: inline-flex;
  align-items: center;
  padding: 0 4px;
  color: inherit;
}

.theme-switch :deep(.anticon) {
  font-size: 16px;
}
</style> 