import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'

export const useAppStore = defineStore('app', () => {
  // 状态
  const language = ref(localStorage.getItem('language') || 'zh-CN')
  const theme = ref<'light' | 'dark'>(localStorage.getItem('theme') as 'light' | 'dark' || 'light')

  // 设置语言
  const setLocale = async (locale: string) => {
    language.value = locale
    localStorage.setItem('language', locale)
    const { locale: i18nLocale } = useI18n()
    i18nLocale.value = locale
  }

  // 设置主题
  const setTheme = (newTheme: 'light' | 'dark') => {
    theme.value = newTheme
    localStorage.setItem('theme', newTheme)
    document.documentElement.setAttribute('data-theme', newTheme)
  }

  return {
    // 状态
    language,
    theme,
    // 方法
    setLocale,
    setTheme
  }
}) 