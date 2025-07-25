import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getLanguageOptions } from '@/api/routine/core/language'
import type { HbtLanguage } from '@/types/routine/core/language'
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
      const { data: response } = await getLanguageOptions()

      if (response.code === 200 && Array.isArray(response.data)) {
        languageList.value = response.data
          .map((lang: HbtLanguage) => ({
            langCode: lang.langCode as SupportedLocale,
            langName: lang.langName,
            langIcon: lang.langIcon
          }))
          .filter((lang: Language) => SUPPORTED_LOCALES.includes(lang.langCode))
      } else {
        throw new Error('响应格式不正确')
      }
    } catch (error) {
      console.error('[Language] 获取语言列表失败:', error)
      throw error
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