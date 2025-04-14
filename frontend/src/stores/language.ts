import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getSupportedLanguages } from '@/api/admin/hbtLanguage'
import type { HbtLanguage } from '@/types/admin/language'
import type { HbtApiResponse } from '@/types/common'
import { SUPPORTED_LOCALES, type SupportedLocale } from './app'

interface Language {
  langCode: SupportedLocale
  langName: string
  langIcon?: string
}

export const useLanguageStore = defineStore('language', () => {
  const loading = ref(false)
  const languageList = ref<Language[]>([])
  const initialized = ref(false)

  // 获取语言列表
  async function fetchLanguages() {
    if (loading.value) return

    loading.value = true
    try {
      const response = await getSupportedLanguages()
      console.log('[Language] 获取语言列表响应:', response)

      if (response.code === 200 && Array.isArray(response.data)) {
        languageList.value = response.data.map(lang => ({
          langCode: lang.langCode as SupportedLocale,
          langName: lang.langName,
          langIcon: lang.langIcon
        }))
        console.log('[Language] 语言列表更新成功:', languageList.value)
      } else {
        console.error('[Language] 获取语言列表失败: 响应格式不正确')
      }
    } catch (error) {
      console.error('[Language] 获取语言列表失败:', error)
    } finally {
      loading.value = false
    }
  }

  // 初始化 store
  async function initializeStore() {
    if (!initialized.value) {
      await fetchLanguages()
      initialized.value = true
    }
  }

  return {
    loading,
    languageList,
    initialized,
    fetchLanguages,
    initializeStore
  }
}) 