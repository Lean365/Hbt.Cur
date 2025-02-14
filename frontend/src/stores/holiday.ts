import { defineStore } from 'pinia'
import type { Holiday } from '@/types/holiday'

// 定义节日主题配置
const HOLIDAY_THEMES: Record<string, Holiday> = {
  // 中国传统节日
  springFestival: {
    id: 'springFestival',
    name: '春节',
    icon: '🧧',
    theme: {
      colorPrimary: '#DE2910',  // 中国红
      colorBgContainer: '#fff1f0',
      colorBgLayout: '#ffedeb',
      colorText: '#BE1205',     // 深中国红
      colorTextSecondary: '#E54335',
      colorBorder: '#ffccc7',
      colorSplit: '#ffd8bf'
    }
  },
  nationalDay: {
    id: 'nationalDay',
    name: '国庆节',
    icon: '🇨🇳',
    theme: {
      colorPrimary: '#DE2910',  // 中国红
      colorBgContainer: '#fff1f0',
      colorBgLayout: '#ffedeb',
      colorText: '#BE1205',     // 深中国红
      colorTextSecondary: '#E54335',
      colorBorder: '#ffccc7',
      colorSplit: '#ffd8bf'
    }
  },
  lanternFestival: {
    id: 'lanternFestival',
    name: '元宵节',
    icon: '🏮',
    theme: {
      colorPrimary: '#ff4d4f',
      colorBgContainer: '#fff2e8',
      colorBgLayout: '#fff7e6',
      colorText: '#d4380d',
      colorTextSecondary: '#ff7a45',
      colorBorder: '#ffbb96',
      colorSplit: '#ffd8bf'
    }
  },
  dragonBoat: {
    id: 'dragonBoat',
    name: '端午节',
    icon: '🛶',
    theme: {
      colorPrimary: '#52c41a',
      colorBgContainer: '#f6ffed',
      colorBgLayout: '#f6ffed',
      colorText: '#389e0d',
      colorTextSecondary: '#52c41a',
      colorBorder: '#b7eb8f',
      colorSplit: '#d9f7be'
    }
  },
  midAutumn: {
    id: 'midAutumn',
    name: '中秋节',
    icon: '🌕',
    theme: {
      colorPrimary: '#faad14',
      colorBgContainer: '#fffbe6',
      colorBgLayout: '#fff7e6',
      colorText: '#d48806',
      colorTextSecondary: '#faad14',
      colorBorder: '#ffe58f',
      colorSplit: '#fff1b8'
    }
  },
  chuseok: {
    id: 'chuseok',
    name: '韩国中秋',
    icon: '🍁',
    theme: {
      colorPrimary: '#eb2f96',
      colorBgContainer: '#fff0f6',
      colorBgLayout: '#fff0f6',
      colorText: '#c41d7f',
      colorTextSecondary: '#eb2f96',
      colorBorder: '#ffadd2',
      colorSplit: '#ffd6e7'
    }
  },
  shogatsu: {
    id: 'shogatsu',
    name: '日本新年',
    icon: '🎍',
    theme: {
      colorPrimary: '#722ed1',
      colorBgContainer: '#f9f0ff',
      colorBgLayout: '#f9f0ff',
      colorText: '#531dab',
      colorTextSecondary: '#722ed1',
      colorBorder: '#d3adf7',
      colorSplit: '#efdbff'
    }
  },

  // 欧美节日
  christmas: {
    id: 'christmas',
    name: '圣诞节',
    icon: '🎄',
    theme: {
      colorPrimary: '#389e0d',
      colorBgContainer: '#f6ffed',
      colorBgLayout: '#fff7e6',
      colorText: '#237804',
      colorTextSecondary: '#52c41a',
      colorBorder: '#b7eb8f',
      colorSplit: '#d9f7be'
    }
  },
  halloween: {
    id: 'halloween',
    name: '万圣节',
    icon: '🎃',
    theme: {
      colorPrimary: '#fa8c16',
      colorBgContainer: '#fff7e6',
      colorBgLayout: '#fff1f0',
      colorText: '#d46b08',
      colorTextSecondary: '#ffa940',
      colorBorder: '#ffd591',
      colorSplit: '#ffe7ba'
    }
  },
  thanksgiving: {
    id: 'thanksgiving',
    name: '感恩节',
    icon: '🦃',
    theme: {
      colorPrimary: '#d48806',
      colorBgContainer: '#fffbe6',
      colorBgLayout: '#fffbe6',
      colorText: '#ad6800',
      colorTextSecondary: '#d48806',
      colorBorder: '#ffe58f',
      colorSplit: '#fff1b8'
    }
  },
  easter: {
    id: 'easter',
    name: '复活节',
    icon: '🥚',
    theme: {
      colorPrimary: '#13c2c2',
      colorBgContainer: '#e6fffb',
      colorBgLayout: '#e6fffb',
      colorText: '#08979c',
      colorTextSecondary: '#13c2c2',
      colorBorder: '#87e8de',
      colorSplit: '#b5f5ec'
    }
  },
  valentines: {
    id: 'valentines',
    name: '情人节',
    icon: '💝',
    theme: {
      colorPrimary: '#eb2f96',
      colorBgContainer: '#fff0f6',
      colorBgLayout: '#fff0f6',
      colorText: '#c41d7f',
      colorTextSecondary: '#eb2f96',
      colorBorder: '#ffadd2',
      colorSplit: '#ffd6e7'
    }
  },

  // 纪念模式
  memorial: {
    id: 'memorial',
    name: '纪念模式',
    icon: '🎗️',
    theme: {
      colorPrimary: '#595959',
      colorBgContainer: '#262626',
      colorBgLayout: '#1f1f1f',
      colorText: '#d9d9d9',
      colorTextSecondary: '#8c8c8c',
      colorBorder: '#434343',
      colorSplit: '#434343',
      filter: 'grayscale(100%)',
      filter_css: 'grayscale(100%) contrast(90%) brightness(90%)'
    }
  }
}

export const useHolidayStore = defineStore('holiday', {
  state: () => ({
    currentHoliday: null as string | null,
    isAutoMode: true,
    customTheme: null as Holiday | null
  }),

  getters: {
    holidayTheme: (state) => {
      if (state.customTheme) return state.customTheme
      return state.currentHoliday ? HOLIDAY_THEMES[state.currentHoliday] : null
    },
    allHolidays: () => HOLIDAY_THEMES
  },

  actions: {
    initHolidayTheme() {
      const savedHoliday = localStorage.getItem('holidayTheme')
      const autoMode = localStorage.getItem('holidayAutoMode')
      const customTheme = localStorage.getItem('customTheme')
      
      this.isAutoMode = autoMode !== 'false'
      if (customTheme) {
        this.customTheme = JSON.parse(customTheme)
      } else if (this.isAutoMode) {
        this.checkCurrentHoliday()
      } else if (savedHoliday && HOLIDAY_THEMES[savedHoliday]) {
        this.setHolidayTheme(savedHoliday)
      }
    },

    setHolidayTheme(holidayId: string | null) {
      if (holidayId && !HOLIDAY_THEMES[holidayId]) return
      
      this.currentHoliday = holidayId
      this.customTheme = null
      localStorage.setItem('holidayTheme', holidayId || '')
      localStorage.removeItem('customTheme')
    },

    setCustomTheme(theme: Partial<Holiday['theme']>) {
      this.customTheme = {
        id: 'custom',
        name: '自定义主题',
        icon: '🎨',
        theme
      }
      localStorage.setItem('customTheme', JSON.stringify(this.customTheme))
      this.currentHoliday = null
      localStorage.removeItem('holidayTheme')
    },

    toggleAutoMode() {
      this.isAutoMode = !this.isAutoMode
      localStorage.setItem('holidayAutoMode', String(this.isAutoMode))
      
      if (this.isAutoMode) {
        this.checkCurrentHoliday()
      }
    },

    checkCurrentHoliday() {
      const now = new Date()
      const month = now.getMonth() + 1
      const day = now.getDate()
      
      // 检查当前日期是否为特定节日
      if (month === 1 && day >= 1 && day <= 3) {
        this.setHolidayTheme('shogatsu') // 日本新年
      } else if (month === 1 && day >= 20 && day <= 27) {
        this.setHolidayTheme('springFestival') // 春节
      } else if (month === 2 && day === 14) {
        this.setHolidayTheme('valentines') // 情人节
      } else if (month === 2 && day === 15) {
        this.setHolidayTheme('lanternFestival') // 元宵节
      } else if (month === 4 && day >= 1 && day <= 15) {
        this.setHolidayTheme('easter') // 复活节（日期不固定）
      } else if (month === 6 && day >= 20 && day <= 25) {
        this.setHolidayTheme('dragonBoat') // 端午节
      } else if (month === 9 && day >= 28 && day <= 30) {
        this.setHolidayTheme('chuseok') // 韩国中秋
      } else if (month === 9 && day >= 15 && day <= 17) {
        this.setHolidayTheme('midAutumn') // 中秋节
      } else if (month === 10 && day >= 1 && day <= 7) {
        this.setHolidayTheme('nationalDay') // 国庆节
      } else if (month === 10 && day === 31) {
        this.setHolidayTheme('halloween') // 万圣节
      } else if (month === 11 && day >= 20 && day <= 26) {
        this.setHolidayTheme('thanksgiving') // 感恩节
      } else if (month === 12 && (day >= 24 && day <= 26)) {
        this.setHolidayTheme('christmas') // 圣诞节
      } else {
        this.setHolidayTheme(null)
      }
    }
  }
}) 