import { defineStore } from 'pinia'

interface SettingsState {
  navMode: 'side' | 'top' | 'mix'
  fixedHeader: boolean
  showBreadcrumb: boolean
  showTabs: boolean
  showFooter: boolean
  weakMode: boolean
  autoHideHeader: boolean
}

export const useSettingStore = defineStore('settings', {
  state: (): SettingsState => ({
    navMode: 'side',
    fixedHeader: true,
    showBreadcrumb: true,
    showTabs: true,
    showFooter: true,
    weakMode: false,
    autoHideHeader: false
  }),

  actions: {
    // 更新设置
    updateSettings(settings: Partial<SettingsState>) {
      Object.assign(this, settings)
      this.saveToStorage()
    },

    // 重置设置
    resetSettings() {
      this.navMode = 'side'
      this.fixedHeader = true
      this.showBreadcrumb = true
      this.showTabs = true
      this.showFooter = true
      this.weakMode = false
      this.autoHideHeader = false
      this.saveToStorage()
    },

    // 从本地存储加载设置
    loadFromStorage() {
      const settings = localStorage.getItem('app-settings')
      if (settings) {
        try {
          const parsed = JSON.parse(settings)
          this.updateSettings(parsed)
        } catch (e) {
          console.error('Failed to parse settings from storage:', e)
        }
      }
    },

    // 保存到本地存储
    saveToStorage() {
      localStorage.setItem('app-settings', JSON.stringify({
        navMode: this.navMode,
        fixedHeader: this.fixedHeader,
        showBreadcrumb: this.showBreadcrumb,
        showTabs: this.showTabs,
        showFooter: this.showFooter,
        weakMode: this.weakMode,
        autoHideHeader: this.autoHideHeader
      }))
    }
  }
}) 