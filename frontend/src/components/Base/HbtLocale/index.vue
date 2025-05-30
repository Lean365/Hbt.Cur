<template>
  <a-dropdown :trigger="['hover']" placement="bottom" class="locale-dropdown">
    <a-button type="text" :loading="loading">
      <template #icon>
        <translation-outlined v-if="!loading" />
      </template>
    </a-button>
    <template #overlay>
      <a-menu @click="({ key }) => handleLocaleChange(String(key))">
        <a-menu-item v-for="lang in languageList" :key="lang.langCode">
          <template #icon>
            <check-outlined v-if="currentLocale === lang.langCode" />
          </template>
          <span :class="{ 'current-lang': currentLocale === lang.langCode }">
            {{ lang.langIcon }} {{ lang.langName }}
          </span>
        </a-menu-item>
        <a-menu-divider v-if="languageList.length === 0" />
        <a-menu-item v-if="languageList.length === 0" disabled>
          {{ loading ? t('common.loading') : t('common.noData') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { MenuProps } from 'ant-design-vue'
import { TranslationOutlined, CheckOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import { useAppStore } from '@/stores/app'
import { useLanguageStore } from '@/stores/language'

const { t } = useI18n()
const appStore = useAppStore()
const languageStore = useLanguageStore()
const currentLocale = ref(appStore.language)
const loading = ref(false)
const languageList = ref(languageStore.languageList)

// 处理语言切换
const handleLocaleChange = async (langCode: string) => {
  if (loading.value) return
  
  loading.value = true
  try {
    await appStore.setLocale(langCode as 'zh-CN' | 'en-US' | 'ar-SA' | 'es-ES' | 'fr-FR' | 'ja-JP' | 'ko-KR' | 'ru-RU' | 'zh-TW')
    message.success(t('common.message.operationSuccess'))
  } catch (error) {
    message.error(t('common.message.operationFailure'))
  } finally {
    loading.value = false
  }
}

// 监听语言变化
watch(() => appStore.language, (newLocale) => {
  currentLocale.value = newLocale
})

// 监听语言列表变化
watch(() => languageStore.languageList, (newList) => {
  languageList.value = newList
})

// 组件挂载时初始化语言列表
onMounted(async () => {
  await languageStore.initializeStore()
})
</script>

<style lang="less" scoped>
.locale-dropdown {
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
  font-size: 14px;
  line-height: 1;
}

.current-lang {
  font-weight: 500;
  color: #1890ff;
}
</style> 