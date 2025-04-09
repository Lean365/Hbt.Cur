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

  // 获取支持的语言列表
  async function fetchLanguages() {
    if (loading.value) return

    loading.value = true
    try {
      const response: HbtApiResponse<HbtLanguage[]> = await getSupportedLanguages()

      if (!response || response.code !== 200 || !response.data) {
        console.error('获取语言列表失败:', response)
        return
      }

      const languages = response.data
      if (Array.isArray(languages)) {
        // 只保留支持的语言
        const validLanguages = languages.filter(lang => 
          lang && 
          typeof lang === 'object' && 
          typeof lang.langCode === 'string' && 
          typeof lang.langName === 'string' &&
          SUPPORTED_LOCALES.includes(lang.langCode as SupportedLocale)
        )

        languageList.value = validLanguages.map(lang => ({
          langCode: lang.langCode as SupportedLocale,
          langName: lang.langName,
          langIcon: lang.langIcon
        }))
      }
    } catch (error) {
      console.error('获取语言列表失败:', error)
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