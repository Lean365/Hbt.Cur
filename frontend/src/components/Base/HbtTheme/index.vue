<template>
  <a-dropdown>
    <a-button type="text" class="theme-button">
      <template #icon>
        <bulb-outlined v-if="!isDarkMode" />
        <bulb-filled v-else />
      </template>
    </a-button>
    <template #overlay>
      <a-menu>
        <a-menu-item key="light" @click="handleThemeChange('light')" :disabled="isThemeDisabled">
          <template #icon>
            <bulb-outlined />
          </template>
          {{ t('theme.mode.light') }}
        </a-menu-item>
        <a-menu-item key="dark" @click="handleThemeChange('dark')" :disabled="isThemeDisabled">
          <template #icon>
            <bulb-filled />
          </template>
          {{ t('theme.mode.dark') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useThemeStore } from '@/stores/theme'
import { useMemorialStore } from '@/stores/memorial'
import { BulbOutlined, BulbFilled } from '@ant-design/icons-vue'

const { t } = useI18n()
const themeStore = useThemeStore()
const memorialStore = useMemorialStore()

// 计算属性
const isDarkMode = computed(() => themeStore.isDarkMode)
const isThemeDisabled = computed(() => memorialStore.isMemorialMode || memorialStore.currentHoliday !== null)

// 方法
const handleThemeChange = (theme: 'light' | 'dark') => {
  themeStore.updateTheme({ isDarkMode: theme === 'dark' })
}
</script>

<style lang="less" scoped>
.theme-button {
  .anticon {
    font-size: 16px;
    transition: all 0.3s;
  }
}
</style> 