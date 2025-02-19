<template>
  <a-dropdown :trigger="['click']" placement="bottom" class="locale-dropdown">
    <a-button type="text" :loading="loading">
      <template #icon>
        <translation-outlined v-if="!loading" />
      </template>
    </a-button>
    <template #overlay>
      <a-menu @click="({ key }) => handleLocaleChange(key)">
        <a-menu-item v-for="lang in languageList" :key="lang.langCode">
          <template #icon>
            <check-outlined v-if="currentLocale === lang.langCode" />
          </template>
          <span :class="{ 'current-lang': currentLocale === lang.langCode }">
            {{ lang.langName }}
            <img v-if="lang.icon" :src="lang.icon" :alt="lang.langName" class="lang-icon" />
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
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { MenuProps } from 'ant-design-vue'
import { TranslationOutlined, CheckOutlined } from '@ant-design/icons-vue'
import { getSupportedLanguages } from '@/api/admin/language'
import type { Language } from '@/types/admin/language'
import { message } from 'ant-design-vue'
import { useAppStore } from '@/stores/app'

const { t } = useI18n()
const appStore = useAppStore()
const currentLocale = ref(appStore.language)
const loading = ref(false)
const languageList = ref<Language[]>([])

// 处理语言切换
const handleLocaleChange = async (langCode: string) => {
  if (loading.value) return
  
  loading.value = true
  try {
    await appStore.setLocale(langCode)
    currentLocale.value = langCode
    message.success(t('common.message.operationSuccess'))
  } catch (error) {
    console.error('Failed to change language:', error)
    message.error(t('common.message.operationFailure'))
  } finally {
    loading.value = false
  }
}

// 获取支持的语言列表
const fetchLanguages = async () => {
  loading.value = true
  try {
    const { code, msg, data } = await getSupportedLanguages()
    if (code === 200 && data) {
      languageList.value = data.sort((a, b) => {
        // 将当前语言置顶
        if (a.langCode === currentLocale.value) return -1
        if (b.langCode === currentLocale.value) return 1
        // 按照 orderNum 排序
        return (a.orderNum || 0) - (b.orderNum || 0)
      })
    } else {
      console.error('Failed to fetch languages:', msg)
      message.error(msg || t('common.message.fetchFailure'))
      // 如果 API 调用失败,使用默认语言列表作为备选
      languageList.value = [
        { id: 1, langCode: 'zh-CN', langName: '简体中文', status: 0, orderNum: 1 },
        { id: 2, langCode: 'en-US', langName: 'English', status: 0, orderNum: 2 }
      ]
    }
  } catch (error: any) {
    console.error('Error fetching languages:', error)
    message.error(error.message || t('common.message.fetchFailure'))
    // 使用默认语言列表
    languageList.value = [
      { id: 1, langCode: 'zh-CN', langName: '简体中文', status: 0, orderNum: 1 },
      { id: 2, langCode: 'en-US', langName: 'English', status: 0, orderNum: 2 }
    ]
  } finally {
    loading.value = false
  }
}

// 组件挂载时获取语言列表
onMounted(() => {
  fetchLanguages()
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

.lang-icon {
  width: 16px;
  height: 16px;
  margin-left: 8px;
  vertical-align: middle;
}

:deep(.ant-menu-item) {
  min-width: 160px;
}
</style> 