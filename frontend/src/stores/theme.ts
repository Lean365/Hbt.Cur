import { defineStore } from 'pinia'

export const useThemeStore = defineStore('theme', {
  state: () => ({
    isDarkMode: false
  }),

  actions: {
    initTheme() {
      const darkMode = localStorage.getItem('darkMode')
      this.isDarkMode = darkMode === 'true'
    },

    toggleTheme() {
      this.isDarkMode = !this.isDarkMode
      localStorage.setItem('darkMode', String(this.isDarkMode))
    }
  }
}) 