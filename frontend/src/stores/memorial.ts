import { defineStore } from 'pinia'

export const useMemorialStore = defineStore('memorial', {
  state: () => ({
    isMemorialMode: false
  }),

  actions: {
    initMemorialMode() {
      const memorialMode = localStorage.getItem('memorialMode')
      this.isMemorialMode = memorialMode === 'true'
      this.updateMemorialStyle()
    },

    toggleMemorialMode() {
      this.isMemorialMode = !this.isMemorialMode
      localStorage.setItem('memorialMode', String(this.isMemorialMode))
      this.updateMemorialStyle()
    },

    updateMemorialStyle() {
      if (this.isMemorialMode) {
        document.documentElement.style.filter = 'grayscale(100%)'
      } else {
        document.documentElement.style.filter = ''
      }
    }
  }
}) 