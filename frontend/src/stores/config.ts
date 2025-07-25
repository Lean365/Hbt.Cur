import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import type { HbtAppConfig, HbtConfigUpdateRequest } from '@/types/common/config'
import { useThemeStore } from './theme'
import { 
  DEFAULT_APP_CONFIG, 
  getDefaultAppConfig,
  DEFAULT_UI_SETTINGS, 
  getDefaultUISettings as getDefaultUISettingsFromSetting,
  type UISettings,
  isValidUISettings,
  isValidAppConfig
} from '@/setting'

// 使用从defaults.ts导入的UISettings接口

/**
 * 应用配置管理 Store
 */
export const useConfigStore = defineStore('config', () => {
  // 配置状态
  const config = ref<HbtAppConfig>({ ...DEFAULT_APP_CONFIG })

  // UI设置状态
  const uiSettings = ref<UISettings>({ ...DEFAULT_UI_SETTINGS })

  // 是否已初始化
  const initialized = ref(false)

  // 获取当前主题模式
  const getCurrentThemeMode = (): boolean => {
    try {
      const themeStore = useThemeStore()
      const isDarkMode = themeStore.isDarkMode
      console.log('[配置Store] getCurrentThemeMode - 从themeStore获取:', isDarkMode ? 'dark' : 'light')
      return isDarkMode
    } catch (error) {
      console.warn('[配置Store] getCurrentThemeMode - 无法获取themeStore，使用默认值:', error)
      // 尝试从localStorage获取
      const savedTheme = localStorage.getItem('theme')
      const isDarkMode = savedTheme === 'dark'
      console.log('[配置Store] getCurrentThemeMode - 从localStorage获取:', isDarkMode ? 'dark' : 'light')
      return isDarkMode
    }
  }

  // 计算属性
  const watermarkConfig = computed(() => config.value.watermark)
  const registerConfig = computed(() => config.value.register)
  const featuresConfig = computed(() => config.value.features)
  const securityConfig = computed(() => config.value.security)
  const themeConfig = computed(() => config.value.theme)

  // 是否显示注册入口
  const showRegister = computed(() => config.value.register.showRegister)
  
  // 是否启用水印
  const watermarkEnabled = computed(() => config.value.watermark.enabled)



  // UI设置相关的计算属性
  const currentThemeMode = computed(() => uiSettings.value.themeMode)
  const currentNavMode = computed(() => uiSettings.value.navMode)
  const currentSidebarColor = computed(() => {
    const color = uiSettings.value.sidebarColor
    console.log('[配置Store] currentSidebarColor - 当前值:', color)
    return color
  })
  const currentPrimaryColor = computed(() => uiSettings.value.primaryColor)
  const isShowBreadcrumb = computed(() => uiSettings.value.showBreadcrumb)
  const isShowTabs = computed(() => uiSettings.value.showTabs)
  const isShowFooter = computed(() => uiSettings.value.showFooter)
  const isShowWatermark = computed(() => uiSettings.value.showWatermark)
  const isShowLogo = computed(() => uiSettings.value.showLogo)
  const isShowTitle = computed(() => uiSettings.value.showTitle)
  const isAnimateTitle = computed(() => uiSettings.value.animateTitle)
  const isKeepTabs = computed(() => uiSettings.value.keepTabs)
  const isShowTabIcon = computed(() => uiSettings.value.showTabIcon)

  /**
   * 设置配置
   */
  const setConfig = (newConfig: Partial<HbtAppConfig>) => {
    Object.assign(config.value, newConfig)
  }

  /**
   * 更新单个配置项
   */
  const updateConfig = (key: string, value: any) => {
    const keys = key.split('.')
    let current: any = config.value
    
    // 遍历到最后一个键的父级
    for (let i = 0; i < keys.length - 1; i++) {
      if (current[keys[i]] === undefined) {
        current[keys[i]] = {}
      }
      current = current[keys[i]]
    }
    
    // 设置最后一个键的值
    current[keys[keys.length - 1]] = value
  }

  /**
   * 批量更新配置
   */
  const batchUpdateConfig = (updates: HbtConfigUpdateRequest[]) => {
    updates.forEach(update => {
      updateConfig(update.key, update.value)
    })
  }

  /**
   * 重置配置为默认值
   */
  const resetConfig = () => {
    console.log('[配置Store] 重置应用配置到默认值')
    // 根据当前主题模式获取动态配置
    const isDarkMode = getCurrentThemeMode()
    config.value = { ...getDefaultAppConfig(isDarkMode) }
    // 保存重置的配置到本地存储
    saveToStorage()
    console.log('[配置Store] 应用配置已重置（主题模式:', isDarkMode ? 'dark' : 'light', '）')
  }

  /**
   * 从本地存储加载配置
   */
  const loadFromStorage = () => {
    try {
      const stored = localStorage.getItem('hbt-app-config')
      if (stored) {
        const parsed = JSON.parse(stored)
        setConfig(parsed)
      }
    } catch (error) {
      console.error('加载配置失败:', error)
    }
  }

  /**
   * 保存配置到本地存储
   */
  const saveToStorage = () => {
    try {
      localStorage.setItem('hbt-app-config', JSON.stringify(config.value))
    } catch (error) {
      console.error('保存配置失败:', error)
    }
  }

  /**
   * 获取默认UI设置
   */
  const getDefaultUISettings = (): UISettings => {
    // 根据当前主题模式获取动态UI设置
    const isDarkMode = getCurrentThemeMode()
    console.log('[配置Store] getDefaultUISettings - 当前主题模式:', isDarkMode ? 'dark' : 'light')
    return { ...getDefaultUISettingsFromSetting(isDarkMode) }
  }

  /**
   * 获取当前UI设置
   */
  const getUISettings = (): UISettings => {
    const savedSettings = localStorage.getItem('app_settings')
    if (savedSettings) {
      return JSON.parse(savedSettings)
    }
    return uiSettings.value
  }

  /**
   * 保存UI设置
   */
  const saveUISettings = (newSettings: UISettings) => {
    uiSettings.value = newSettings
    localStorage.setItem('app_settings', JSON.stringify(newSettings))
  }

  /**
   * 重置UI设置
   */
  const resetUISettings = () => {
    console.log('[配置Store] 重置UI设置到默认值')
    // 根据当前主题模式获取动态UI设置
    const isDarkMode = getCurrentThemeMode()
    console.log('[配置Store] resetUISettings - 当前主题模式:', isDarkMode ? 'dark' : 'light')
    const defaultSettings = getDefaultUISettingsFromSetting(isDarkMode)
    uiSettings.value = { ...defaultSettings }
    localStorage.setItem('app_settings', JSON.stringify(defaultSettings))
    console.log('[配置Store] UI设置已重置（主题模式:', isDarkMode ? 'dark' : 'light', '）:', defaultSettings)
  }

  /**
   * 设置导航模式
   */
  const setNavMode = (mode: 'side' | 'top' | 'mix') => {
    uiSettings.value.navMode = mode
    saveUISettings(uiSettings.value)
  }



  /**
   * 设置显示面包屑
   */
  const setShowBreadcrumb = (show: boolean) => {
    uiSettings.value.showBreadcrumb = show
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置显示标签页
   */
  const setShowTabs = (show: boolean) => {
    uiSettings.value.showTabs = show
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置显示页脚
   */
  const setShowFooter = (show: boolean) => {
    uiSettings.value.showFooter = show
    saveUISettings(uiSettings.value)
  }



  /**
   * 设置主题模式
   */
  const setThemeMode = (mode: 'light' | 'dark' | 'auto') => {
    uiSettings.value.themeMode = mode
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置侧边栏颜色
   */
  const setSidebarColor = (color: string) => {
    uiSettings.value.sidebarColor = color
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置主色调
   */
  const setPrimaryColor = (color: string) => {
    uiSettings.value.primaryColor = color
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置水印显示
   */
  const setShowWatermark = (show: boolean) => {
    uiSettings.value.showWatermark = show
    // 同步到应用配置中的水印开关
    config.value.watermark.enabled = show
    saveUISettings(uiSettings.value)
    saveToStorage()
  }

  /**
   * 设置Logo显示
   */
  const setShowLogo = (show: boolean) => {
    uiSettings.value.showLogo = show
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置标题显示
   */
  const setShowTitle = (show: boolean) => {
    uiSettings.value.showTitle = show
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置动画标题
   */
  const setAnimateTitle = (animate: boolean) => {
    uiSettings.value.animateTitle = animate
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置保持标签页
   */
  const setKeepTabs = (keep: boolean) => {
    uiSettings.value.keepTabs = keep
    saveUISettings(uiSettings.value)
  }

  /**
   * 设置标签页图标显示
   */
  const setShowTabIcon = (show: boolean) => {
    uiSettings.value.showTabIcon = show
    saveUISettings(uiSettings.value)
  }

  /**
   * 获取所有UI设置
   */
  const getAllUISettings = (): UISettings => {
    return { ...uiSettings.value }
  }

  /**
   * 批量更新UI设置
   */
  const batchUpdateUISettings = (updates: Partial<UISettings>) => {
    console.log('[配置Store] batchUpdateUISettings - 更新内容:', updates)
    Object.assign(uiSettings.value, updates)
    console.log('[配置Store] batchUpdateUISettings - 更新后的uiSettings:', uiSettings.value)
    
    // 如果更新了水印设置，同步到应用配置
    if ('showWatermark' in updates) {
      config.value.watermark.enabled = updates.showWatermark!
      saveToStorage()
    }
    
    saveUISettings(uiSettings.value)
  }

  /**
   * 同步UI设置到组件状态
   */
  const syncUISettingsToComponent = (componentSettings: any) => {
    Object.assign(componentSettings, uiSettings.value)
  }

  /**
   * 从组件状态同步UI设置
   */
  const syncComponentSettingsToUI = (componentSettings: UISettings) => {
    Object.assign(uiSettings.value, componentSettings)
    saveUISettings(uiSettings.value)
  }

// 使用从defaults.ts导入的isValidUISettings函数

  /**
   * 完全重置所有配置（应用配置 + UI设置）
   */
  const resetAllConfig = () => {
    console.log('[配置Store] 开始完全重置所有配置...')
    
    // 重置应用配置
    resetConfig()
    
    // 重置UI设置
    resetUISettings()
    
    // 清除所有相关的本地存储
    try {
      localStorage.removeItem('hbt-app-config')
      localStorage.removeItem('app_settings')
      console.log('[配置Store] 本地存储已清除')
    } catch (error) {
      console.error('[配置Store] 清除本地存储失败:', error)
    }
    
    console.log('[配置Store] 所有配置已完全重置到默认值')
  }

  /**
   * 初始化配置
   */
  const initialize = async () => {
    if (initialized.value) return

    console.log('[配置Store] 开始初始化...')

    try {
      // 使用新的默认配置
      const isDarkMode = getCurrentThemeMode()
      config.value = { ...getDefaultAppConfig(isDarkMode) }
      uiSettings.value = { ...getDefaultUISettingsFromSetting(isDarkMode) }
      
      // 尝试加载保存的应用配置
      const savedAppConfig = localStorage.getItem('hbt-app-config')
      if (savedAppConfig) {
        try {
          const parsedConfig = JSON.parse(savedAppConfig)
          if (isValidAppConfig(parsedConfig)) {
            config.value = parsedConfig
            console.log('[配置Store] 应用配置加载成功:', parsedConfig)
          } else {
            console.warn('[配置Store] 应用配置数据不完整，使用默认配置')
            saveToStorage()
          }
        } catch (error) {
          console.error('[配置Store] 应用配置解析失败，使用默认配置:', error)
          saveToStorage()
        }
      } else {
        console.log('[配置Store] 未找到保存的应用配置，使用默认配置')
        saveToStorage()
      }
      
      // 加载UI设置
      const savedUISettings = localStorage.getItem('app_settings')
      if (savedUISettings) {
        try {
          const parsedSettings = JSON.parse(savedUISettings)
          // 验证设置数据的完整性
          if (isValidUISettings(parsedSettings)) {
            uiSettings.value = parsedSettings
            console.log('[配置Store] UI设置加载成功:', parsedSettings)
          } else {
            console.warn('[配置Store] UI设置数据不完整，使用默认设置')
            resetUISettings()
          }
        } catch (error) {
          console.error('[配置Store] UI设置解析失败，使用默认设置:', error)
          resetUISettings()
        }
      } else {
        console.log('[配置Store] 未找到保存的UI设置，使用默认设置')
        resetUISettings()
      }
      
      // 这里可以从后端API加载配置
      // try {
      //   const response = await getAppConfig()
      //   if (response.data) {
      //     setConfig(response.data)
      //   }
      // } catch (error) {
      //   console.error('从后端加载配置失败:', error)
      // }

      initialized.value = true
      console.log('[配置Store] 初始化完成')
    } catch (error) {
      console.error('[配置Store] 初始化失败:', error)
      // 初始化失败时使用默认配置
      resetConfig()
      resetUISettings()
      initialized.value = true
    }
  }

  /**
   * 根据主题模式更新配置
   */
  const updateConfigForTheme = (isDarkMode: boolean) => {
    console.log('[配置Store] 根据主题模式更新配置:', isDarkMode ? 'dark' : 'light')
    
    // 获取当前配置
    const currentConfig = { ...config.value }
    
    // 根据主题模式更新主色调
    const newConfig = getDefaultAppConfig(isDarkMode)
    currentConfig.theme.primaryColor = newConfig.theme.primaryColor
    
    // 更新配置
    config.value = currentConfig
    
    // 只更新UI设置中的主色调，不覆盖用户设置的侧边栏颜色
    const newUISettings = getDefaultUISettingsFromSetting(isDarkMode)
    uiSettings.value.primaryColor = newUISettings.primaryColor
    // 注意：不更新侧边栏颜色，保持用户的选择
    
    // 保存到本地存储
    saveToStorage()
    saveUISettings(uiSettings.value)
    
    console.log('[配置Store] 配置已根据主题模式更新（保持用户侧边栏颜色）')
  }

  /**
   * 监听配置变化并自动保存
   */
  const watchAndSave = () => {
    // 这里可以使用 watch 监听配置变化
    // 暂时在每次更新时手动保存
  }

  return {
    // 状态
    config,
    uiSettings,
    initialized,
    
    // 计算属性
    watermarkConfig,
    registerConfig,
    featuresConfig,
    securityConfig,
    themeConfig,
    showRegister,
    watermarkEnabled,
    
    // UI设置计算属性
    currentThemeMode,
    currentNavMode,
    currentSidebarColor,
    currentPrimaryColor,
    isShowBreadcrumb,
    isShowTabs,
    isShowFooter,
    isShowWatermark,
    isShowLogo,
    isShowTitle,
    isAnimateTitle,
    isKeepTabs,
    isShowTabIcon,
    
    // 方法
    setConfig,
    updateConfig,
    batchUpdateConfig,
    resetConfig,
    loadFromStorage,
    saveToStorage,
    getDefaultUISettings,
    getUISettings,
    saveUISettings,
    resetUISettings,
    setNavMode,
    setShowBreadcrumb,
    setShowTabs,
    setShowFooter,
    setThemeMode,
    setSidebarColor,
    setPrimaryColor,
    setShowWatermark,
    setShowLogo,
    setShowTitle,
    setAnimateTitle,
    setKeepTabs,
    setShowTabIcon,
    getAllUISettings,
    batchUpdateUISettings,
    syncUISettingsToComponent,
    syncComponentSettingsToUI,
    isValidUISettings,
    resetAllConfig,
    initialize,
    updateConfigForTheme,
    watchAndSave
  }
}) 