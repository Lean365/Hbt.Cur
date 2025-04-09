import { defineStore } from 'pinia'
import { ref } from 'vue'
import { theme } from 'ant-design-vue'

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
    const savedColor = localStorage.getItem('primaryColor')
    if (savedColor) {
      primaryColor.value = savedColor
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

  return {
    isDarkMode,
    primaryColor,
    initTheme,
    toggleTheme,
    updateTheme
  }
})