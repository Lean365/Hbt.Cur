import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getSupportedLanguages } from '@/api/admin/hbtLanguage'
import type { HbtLanguage } from '@/types/admin/hbtLanguage'
import type { HbtApiResponse } from '@/types/common'

interface Language {
  langCode: string
  langName: string
  langIcon?: string
}

export const useLanguageStore = defineStore('language', () => {
  const loading = ref(false)
  const languageList = ref<Language[]>([])
  const initialized = ref(false)

  async function fetchLanguages() {
    loading.value = true
    try {
      const response: HbtApiResponse<HbtLanguage[]> = await getSupportedLanguages()

      // 验证响应结构
      if (!response || response.code !== 200 || !response.data) {
        console.error('API响应格式错误:', response)
        return
      }

      // 使用response.data作为语言数组
      const languages = response.data
      //console.log('语言列表:', languages)

      if (Array.isArray(languages)) {
        // 验证数组中的每个对象
        const validLanguages = languages.filter(lang => {
          const isValid = lang && 
            typeof lang === 'object' && 
            typeof lang.langCode === 'string' && 
            typeof lang.langName === 'string'
          
          if (!isValid) {
            //console.warn('无效的语言对象:', lang)
          }
          return isValid
        })

        //console.log('有效的语言对象:', validLanguages)

        // 转换为Language对象
        languageList.value = validLanguages.map(lang => ({
          langCode: lang.langCode,
          langName: lang.langName,
          langIcon: lang.langIcon
        }))

        //console.log('最终语言列表:', languageList.value)
      } else {
        //console.error('返回的数据不是数组:', languages)
      }
    } catch (error) {
      //console.error('获取语言列表失败:', error)
    } finally {
      loading.value = false
    }
  }

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