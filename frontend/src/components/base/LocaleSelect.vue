<template>
  <a-dropdown :trigger="['click']">
    <a-button type="text" class="locale-select">
      <template #icon>
        <translation-outlined />
      </template>
    </a-button>
    <template #overlay>
      <a-menu
        :selectedKeys="[currentLocale]"
        @click="handleLocaleChange"
      >
        <a-menu-item key="zh-CN">
          <template #icon>
            <span class="anticon">🇨🇳</span>
          </template>
          {{ t('select.zhCN') }}
        </a-menu-item>
        <a-menu-item key="en-US">
          <template #icon>
            <span class="anticon">🇺🇸</span>
          </template>
          {{ t('select.enUS') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
import type { MenuProps } from 'ant-design-vue'
import { TranslationOutlined } from '@ant-design/icons-vue'

const { locale, t } = useI18n()
const currentLocale = ref(locale.value)

// 切换语言
const handleLocaleChange: MenuProps['onClick'] = ({ key }) => {
  if (typeof key === 'string') {
    locale.value = key
    currentLocale.value = key
    localStorage.setItem('locale', key)
  }
}
</script>

<style scoped>
.locale-select {
  display: inline-flex;
  align-items: center;
  height: 32px;
  padding: 4px;
  color: inherit;
}

.locale-select :deep(.anticon) {
  font-size: 16px;
}
</style> 