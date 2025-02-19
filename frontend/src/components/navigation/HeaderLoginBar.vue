<template>
  <div class="header-login-bar">
    <a-space>
      <locale-select />
      <theme-switch />
      <holiday-theme />
    </a-space>
  </div>
</template>

<script lang="ts" setup>
import LocaleSelect from '@/components/base/LocaleSelect.vue'
import ThemeSwitch from '@/components/base/ThemeSwitch.vue'
import HolidayTheme from '@/components/base/HolidayTheme.vue'
</script>

<style lang="less" scoped>
.header-login-bar {
  position: fixed;
  top: 24px;
  right: 24px;
  z-index: 1000;
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 8px 12px;
  border-radius: 6px;
  background: rgba(0, 0, 0, 0.25);
  backdrop-filter: blur(8px);

  :deep(.ant-btn) {
    color: rgba(255, 255, 255, 0.85);

    &:hover {
      color: #177ddc;
      background: rgba(255, 255, 255, 0.08);
    }
  }

  :deep(.anticon) {
    font-size: 16px;
  }
}
</style> 