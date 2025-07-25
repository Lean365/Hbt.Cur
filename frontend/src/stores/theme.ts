import { defineStore } from 'pinia'
import { ref } from 'vue'
import { theme } from 'ant-design-vue'
import { useConfigStore } from './config'

interface ThemeState {
  isDarkMode: boolean
  primaryColor: string
}

export const useThemeStore = defineStore('theme', () => {
  const isDarkMode = ref(false)
  const primaryColor = ref(theme.defaultConfig.token?.colorPrimary)

  const initTheme = () => {
    const savedTheme = localStorage.getItem('theme')
    if (savedTheme === 'dark') {
      isDarkMode.value = true
    }
    
    // 优先从configStore读取主色调设置
    try {
      const configStore = useConfigStore()
      if (configStore.currentPrimaryColor) {
        primaryColor.value = configStore.currentPrimaryColor
      } else {
        // 如果configStore中没有，则从localStorage读取
        const savedColor = localStorage.getItem('primaryColor')
        if (savedColor) {
          primaryColor.value = savedColor
        }
      }
    } catch (error) {
      // 如果configStore还未初始化，则从localStorage读取
      const savedColor = localStorage.getItem('primaryColor')
      if (savedColor) {
        primaryColor.value = savedColor
      }
    }
  }

  const toggleTheme = () => {
    isDarkMode.value = !isDarkMode.value
    localStorage.setItem('theme', isDarkMode.value ? 'dark' : 'light')
  }

  const updateTheme = (options: Partial<ThemeState>) => {
    if (options.isDarkMode !== undefined) {
      isDarkMode.value = options.isDarkMode
      localStorage.setItem('theme', options.isDarkMode ? 'dark' : 'light')
    }
    if (options.primaryColor) {
      primaryColor.value = options.primaryColor
      localStorage.setItem('primaryColor', options.primaryColor)
    }
  }

  // 同步configStore中的主色调设置
  const syncPrimaryColorFromConfig = () => {
    try {
      const configStore = useConfigStore()
      if (configStore.currentPrimaryColor && configStore.currentPrimaryColor !== primaryColor.value) {
        primaryColor.value = configStore.currentPrimaryColor
        localStorage.setItem('primaryColor', configStore.currentPrimaryColor)
      }
    } catch (error) {
      console.warn('无法同步configStore中的主色调设置:', error)
    }
  }

  return {
    isDarkMode,
    primaryColor,
    initTheme,
    toggleTheme,
    updateTheme,
    syncPrimaryColorFromConfig
  }
})