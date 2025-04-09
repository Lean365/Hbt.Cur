import { defineStore } from 'pinia'
import { ref } from 'vue'
import i18n from '@/locales'
import { nextTick } from 'vue'

// 支持的语言列表
export const SUPPORTED_LOCALES = [
  'zh-CN',  // 简体中文
  'zh-TW',  // 繁体中文
  'en-US',  // 英语
  'ja-JP',  // 日语
  'ko-KR',  // 韩语
  'fr-FR',  // 法语
  'es-ES',  // 西班牙语
  'ru-RU',  // 俄语
  'ar-SA'   // 阿拉伯语
] as const

export type SupportedLocale = typeof SUPPORTED_LOCALES[number]

// 默认语言
export const DEFAULT_LOCALE: SupportedLocale = 'zh-CN'

interface Settings {
  navMode: 'side' | 'top' | 'mix'
  fixedHeader: boolean
  showBreadcrumb: boolean
  showTabs: boolean
  showFooter: boolean
  autoHideHeader: boolean
  sidebarColor: string
  primaryColor: string
  showWatermark: boolean
  showLogo: boolean
  animateTitle: boolean
  keepTabs: boolean
  showTabIcon: boolean
}

export const useAppStore = defineStore('app', () => {
  // 状态
  const language = ref<SupportedLocale>(localStorage.getItem('language') as SupportedLocale || DEFAULT_LOCALE)
  const theme = ref<'light' | 'dark'>(localStorage.getItem('theme') as 'light' | 'dark' || 'light')
  const settings = ref<Settings>({
    navMode: 'side',
    fixedHeader: true,
    showBreadcrumb: true,
    showTabs: true,
    showFooter: true,
    autoHideHeader: false,
    sidebarColor: '#001529',
    primaryColor: '#1890ff',
    showWatermark: false,
    showLogo: true,
    animateTitle: false,
    keepTabs: false,
    showTabIcon: true
  })

  // 初始化函数
  const initialize = async () => {
    const savedLanguage = localStorage.getItem('language')
    if (savedLanguage && SUPPORTED_LOCALES.includes(savedLanguage as SupportedLocale)) {
      await setLocale(savedLanguage as SupportedLocale)
    } else {
      // 如果没有保存的语言或语言无效，使用默认语言
      await setLocale(DEFAULT_LOCALE)
    }
  }

  // 设置语言
  const setLocale = async (locale: SupportedLocale) => {
    try {
      // 验证语言代码
      if (!SUPPORTED_LOCALES.includes(locale)) {
        throw new Error(`不支持的语言: ${locale}`)
      }

      // 更新 store 中的语言
      language.value = locale
      localStorage.setItem('language', locale)
      
      // 更新 i18n 语言
      i18n.global.locale.value = locale
      
      // 等待下一个 tick，确保语言切换生效
      await nextTick()
      
      // 更新文档方向（对于阿拉伯语等从右到左的语言）
      document.dir = locale === 'ar-SA' ? 'rtl' : 'ltr'
      document.documentElement.setAttribute('dir', document.dir)
      document.documentElement.setAttribute('lang', locale)
      
      // 触发自定义事件，通知其他组件语言已更改
      window.dispatchEvent(new Event('languagechange'))

      return true
    } catch (error) {
      console.error('Failed to change language:', error)
      throw error
    }
  }

  // 获取当前语言
  const getCurrentLocale = (): SupportedLocale => {
    return language.value
  }

  // 检查是否支持某个语言
  const isLocaleSupported = (locale: string): boolean => {
    return SUPPORTED_LOCALES.includes(locale as SupportedLocale)
  }

  // 设置主题
  const setTheme = (newTheme: 'light' | 'dark') => {
    theme.value = newTheme
    localStorage.setItem('theme', newTheme)
    document.documentElement.setAttribute('data-theme', newTheme)
  }

  // 获取默认设置
  const getDefaultSettings = (): Settings => ({
    navMode: 'side',
    fixedHeader: true,
    showBreadcrumb: true,
    showTabs: true,
    showFooter: true,
    autoHideHeader: false,
    sidebarColor: '#001529',
    primaryColor: '#1890ff',
    showWatermark: false,
    showLogo: true,
    animateTitle: false,
    keepTabs: false,
    showTabIcon: true
  })

  // 获取当前设置
  const getSettings = (): Settings => {
    const savedSettings = localStorage.getItem('app_settings')
    if (savedSettings) {
      return JSON.parse(savedSettings)
    }
    return settings.value
  }

  // 保存设置
  const saveSettings = (newSettings: Settings) => {
    settings.value = newSettings
    localStorage.setItem('app_settings', JSON.stringify(newSettings))
  }

  // 重置设置
  const resetSettings = () => {
    const defaultSettings = getDefaultSettings()
    settings.value = defaultSettings
    localStorage.setItem('app_settings', JSON.stringify(defaultSettings))
  }

  // 设置导航模式
  const setNavMode = (mode: 'side' | 'top' | 'mix') => {
    settings.value.navMode = mode
    saveSettings(settings.value)
  }

  // 设置固定头部
  const setFixedHeader = (fixed: boolean) => {
    settings.value.fixedHeader = fixed
    saveSettings(settings.value)
  }

  // 设置显示面包屑
  const setShowBreadcrumb = (show: boolean) => {
    settings.value.showBreadcrumb = show
    saveSettings(settings.value)
  }

  // 设置显示标签页
  const setShowTabs = (show: boolean) => {
    settings.value.showTabs = show
    saveSettings(settings.value)
  }

  // 设置显示页脚
  const setShowFooter = (show: boolean) => {
    settings.value.showFooter = show
    saveSettings(settings.value)
  }

  // 设置自动隐藏头部
  const setAutoHideHeader = (auto: boolean) => {
    settings.value.autoHideHeader = auto
    saveSettings(settings.value)
  }

  return {
    // 状态
    language,
    theme,
    settings,
    // 方法
    initialize,
    setLocale,
    getCurrentLocale,
    isLocaleSupported,
    setTheme,
    getDefaultSettings,
    getSettings,
    saveSettings,
    resetSettings,
    setNavMode,
    setFixedHeader,
    setShowBreadcrumb,
    setShowTabs,
    setShowFooter,
    setAutoHideHeader,
    // 常量
    SUPPORTED_LOCALES
  }
}) 