import { defineStore } from 'pinia'
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Holiday } from '@/types/holiday'

type HolidayThemes = {
  [key: string]: Holiday;
  springFestival: Holiday;
  nationalDay: Holiday;
  lanternFestival: Holiday;
  dragonBoat: Holiday;
  midAutumn: Holiday;
  chuseok: Holiday;
  shogatsu: Holiday;
  christmas: Holiday;
  halloween: Holiday;
  thanksgiving: Holiday;
  easter: Holiday;
  valentines: Holiday;
  memorial: Holiday;
}

export const useHolidayStore = defineStore('holiday', () => {
  const { t } = useI18n()
  
  // 状态定义
  const currentHoliday = ref<string | null>(null)
  const isAutoMode = ref(true)
  const customTheme = ref<Holiday | null>(null)

  // 节日主题配置
  const holidayThemes = computed<HolidayThemes>(() => ({
    // 中国传统节日
    springFestival: {
      id: 'springFestival',
      name: t('holiday.springFestival.name'),
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
      name: t('holiday.nationalDay.name'),
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
      name: t('holiday.lanternFestival.name'),
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
      name: t('holiday.dragonBoat.name'),
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
      name: t('holiday.midAutumn.name'),
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
      name: t('holiday.chuseok.name'),
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
      name: t('holiday.shogatsu.name'),
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
      name: t('holiday.christmas.name'),
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
      name: t('holiday.halloween.name'),
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
      name: t('holiday.thanksgiving.name'),
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
      name: t('holiday.easter.name'),
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
      name: t('holiday.valentines.name'),
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
      name: t('holiday.memorial.name'),
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
  }))

  // 计算属性
  const holidayTheme = computed(() => {
    if (customTheme.value) return customTheme.value
    return currentHoliday.value ? holidayThemes.value[currentHoliday.value] : null
  })

  const allHolidays = computed(() => holidayThemes.value)

  // 方法
  function initHolidayTheme() {
    const savedHoliday = localStorage.getItem('holidayTheme')
    const autoMode = localStorage.getItem('holidayAutoMode')
    const savedCustomTheme = localStorage.getItem('customTheme')
    
    isAutoMode.value = autoMode !== 'false'
    if (savedCustomTheme) {
      customTheme.value = JSON.parse(savedCustomTheme)
    } else if (isAutoMode.value) {
      checkCurrentHoliday()
    } else if (savedHoliday && savedHoliday in holidayThemes.value) {
      setHolidayTheme(savedHoliday)
    }
  }

  function setHolidayTheme(holidayId: string | null) {
    if (holidayId && !(holidayId in holidayThemes.value)) return
    
    currentHoliday.value = holidayId
    customTheme.value = null
    localStorage.setItem('holidayTheme', holidayId || '')
    localStorage.removeItem('customTheme')
  }

  function setCustomTheme(theme: Partial<Holiday['theme']>) {
    customTheme.value = {
      id: 'custom',
      name: t('holiday.custom.name'),
      icon: '🎨',
      theme
    }
    localStorage.setItem('customTheme', JSON.stringify(customTheme.value))
    currentHoliday.value = null
    localStorage.removeItem('holidayTheme')
  }

  function setAutoMode(enabled: boolean) {
    isAutoMode.value = enabled
    localStorage.setItem('holidayAutoMode', enabled.toString())
    
    if (enabled) {
      checkCurrentHoliday()
    }
  }

  function checkCurrentHoliday() {
    const now = new Date()
    const month = now.getMonth() + 1
    const day = now.getDate()
    
    // 检查当前日期是否为特定节日
    if (month === 1 && day >= 1 && day <= 3) {
      setHolidayTheme('shogatsu') // 日本新年
    } else if (month === 1 && day >= 20 && day <= 27) {
      setHolidayTheme('springFestival') // 春节
    } else if (month === 2 && day === 14) {
      setHolidayTheme('valentines') // 情人节
    } else if (month === 2 && day === 15) {
      setHolidayTheme('lanternFestival') // 元宵节
    } else if (month === 4 && day >= 1 && day <= 15) {
      setHolidayTheme('easter') // 复活节（日期不固定）
    } else if (month === 6 && day >= 20 && day <= 25) {
      setHolidayTheme('dragonBoat') // 端午节
    } else if (month === 9 && day >= 28 && day <= 30) {
      setHolidayTheme('chuseok') // 韩国中秋
    } else if (month === 9 && day >= 15 && day <= 17) {
      setHolidayTheme('midAutumn') // 中秋节
    } else if (month === 10 && day >= 1 && day <= 7) {
      setHolidayTheme('nationalDay') // 国庆节
    } else if (month === 10 && day === 31) {
      setHolidayTheme('halloween') // 万圣节
    } else if (month === 11 && day >= 20 && day <= 26) {
      setHolidayTheme('thanksgiving') // 感恩节
    } else if (month === 12 && (day >= 24 && day <= 26)) {
      setHolidayTheme('christmas') // 圣诞节
    } else {
      setHolidayTheme(null)
    }
  }

  // 返回store的公共接口
  return {
    // 状态
    currentHoliday,
    isAutoMode,
    customTheme,
    
    // 计算属性
    holidayTheme,
    allHolidays,
    
    // 方法
    initHolidayTheme,
    setHolidayTheme,
    setCustomTheme,
    setAutoMode,
    checkCurrentHoliday
  }
}) 