<template>
  <ConfigProvider>
    <router-view></router-view>
  </ConfigProvider>
</template>

<script lang="ts" setup>
import 'ant-design-vue/dist/reset.css'
import { ConfigProvider } from 'ant-design-vue'
import { onMounted, computed, watch } from 'vue'
import { theme } from 'ant-design-vue'
import { useThemeStore } from '@/stores/theme'
import { useHolidayStore } from '@/stores/holiday'
import GlobalSettings from '@/components/layout/GlobalSettings.vue'

const themeStore = useThemeStore()
const holidayStore = useHolidayStore()
const isDark = computed(() => themeStore.isDarkMode)
const currentTheme = computed(() => holidayStore.holidayTheme)
const isMemorialMode = computed(() => currentTheme.value?.id === 'memorial')

onMounted(() => {
  themeStore.initTheme()
  holidayStore.initHolidayTheme()
  document.documentElement.style.colorScheme = isDark.value ? 'dark' : 'light'
})

watch(isDark, (newValue) => {
  document.documentElement.style.colorScheme = newValue ? 'dark' : 'light'
})

// 监听纪念模式的变化
watch(isMemorialMode, (newValue) => {
  if (newValue) {
    document.documentElement.style.filter = currentTheme.value?.theme?.filter_css || ''
    document.body.classList.add('memorial-mode')
  } else {
    document.documentElement.style.filter = ''
    document.body.classList.remove('memorial-mode')
  }
})
</script>

<style>
#app {
  width: 100%;
  height: 100vh;
  background-color: var(--ant-color-bg-layout);
  color: var(--ant-color-text);
}

body {
  margin: 0;
  padding: 0;
  background-color: var(--ant-color-bg-layout);
  color: var(--ant-color-text);
}

.theme-dark {
  background-color: var(--ant-color-bg-layout);
  color: var(--ant-color-text);
  min-height: 100vh;
}

:root {
  color-scheme: light dark;
}

/* 添加全局主题变量应用 */
* {
  transition: background-color 0.3s, color 0.3s, filter 0.3s;
}

/* 纪念模式样式 */
body.memorial-mode,
body.memorial-mode #app,
body.memorial-mode .ant-dropdown,
body.memorial-mode .ant-modal-root,
body.memorial-mode .ant-message,
body.memorial-mode .ant-notification,
body.memorial-mode .ant-drawer {
  filter: grayscale(100%) contrast(90%) brightness(90%);
  transition: filter 0.3s ease;
}

/* 确保所有容器都使用主题颜色 */
.ant-layout {
  background: var(--ant-color-bg-layout);
}

.ant-layout-content {
  background: var(--ant-color-bg-container);
}

.ant-card {
  background: var(--ant-color-bg-container);
  border-color: var(--ant-color-border);
}

.ant-menu {
  background: var(--ant-color-bg-container);
  border-color: var(--ant-color-border);
}

.ant-table {
  background: var(--ant-color-bg-container);
  color: var(--ant-color-text);
}

.ant-modal-content {
  background: var(--ant-color-bg-container);
  color: var(--ant-color-text);
}

.ant-drawer-content {
  background: var(--ant-color-bg-container);
  color: var(--ant-color-text);
}

/* 确保所有容器都使用主题颜色 */
.ant-dropdown-menu {
  background: var(--ant-color-bg-container);
  border-color: var(--ant-color-border);
}

.ant-dropdown-menu-item {
  color: var(--ant-color-text);
  
  &:hover {
    background: var(--ant-color-bg-container-hover);
  }
}

.ant-dropdown-menu-item-group-title {
  color: var(--ant-color-text-secondary);
}
</style> 