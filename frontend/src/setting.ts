//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : setting.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 项目全局默认配置
//===================================================================

import type { HbtAppConfig } from '@/types/common/config'
import logoUrl from '@/assets/images/logo.svg'

// UI设置接口
export interface UISettings {
  themeMode: 'light' | 'dark' | 'auto'
  navMode: 'side' | 'top' | 'mix'
  showBreadcrumb: boolean
  showTabs: boolean
  showFooter: boolean
  sidebarColor: string
  primaryColor: string
  showWatermark: boolean
  showLogo: boolean
  showTitle: boolean
  animateTitle: boolean
  keepTabs: boolean
  showTabIcon: boolean
}

/**
 * 获取默认应用配置（根据主题模式动态调整）
 */
export const getDefaultAppConfig = (isDarkMode: boolean = false): HbtAppConfig => ({
  watermark: {
    enabled: false, // 默认关闭水印
    content: ['Hbt365', '黑冰台版权所有'],
    fontSize: 16,
    opacity: 0.1,
    rotate: -15,
    gap: [50, 50]
  },
  register: {
    showRegister: true,
    allowRegister: true,
    requireEmailVerification: true,
    requirePhoneVerification: false
  },
  features: {
    showHelp: true,
    showFeedback: true,
    showOnlineUsers: true,
    showAnnouncement: true
  },
  security: {
    enableCaptcha: true,
    passwordMinLength: 6,
    passwordComplexity: {
      requireUppercase: true,
      requireLowercase: true,
      requireNumbers: true,
      requireSpecialChars: false
    },
    loginFailLockCount: 5,
    loginFailLockTime: 30
  },
  theme: {
    mode: 'light',
    primaryColor: isDarkMode ? '#177ddc' : '#1890ff', // 根据主题模式动态调整主色调
    enableAnimation: true,
    compact: false
  },
  title: 'Lean.Hbt',
  subtitle: '企业级管理系统',
  logo: {
    src: logoUrl,
    alt: 'Lean.Hbt Logo',
    width: 32,
    height: 32,
    showText: true,
    text: 'Lean.Hbt',
    textSize: 18,
    textWeight: 600
  },
  copyright: '© 2024 Lean.Hbt. 保留所有权利。',
  version: '1.0.0'
})

/**
 * 默认应用配置（保持向后兼容）
 */
export const DEFAULT_APP_CONFIG: HbtAppConfig = getDefaultAppConfig(false)

/**
 * 获取默认UI设置（根据主题模式动态调整）
 */
export const getDefaultUISettings = (isDarkMode: boolean = false): UISettings => ({
  themeMode: 'light',
  navMode: 'side',
  showBreadcrumb: false, // 默认禁用面包屑
  showTabs: true, // 默认启用标签页
  showFooter: true,
  sidebarColor: isDarkMode ? '#000000' : '#ffffff', // 深色主题使用全黑，浅色主题使用白色
  primaryColor: isDarkMode ? '#177ddc' : '#1890ff', // 根据主题模式动态调整主色调
  showWatermark: false, // 默认关闭水印，与应用配置保持一致
  showLogo: true,
  showTitle: true,
  animateTitle: false,
  keepTabs: false,
  showTabIcon: true
})

/**
 * 默认UI设置（保持向后兼容）
 */
export const DEFAULT_UI_SETTINGS: UISettings = getDefaultUISettings(false)

/**
 * 主题颜色预设
 */
export const THEME_COLORS = [
  // 中国传统色（15个）
  '#E60012',  // 中国红
  '#8E453F',  // 枣红
  '#FFB61E',  // 藤黄
  '#9D2933',  // 胭脂
  '#DC3023',  // 赤
  '#FF4C00',  // 朱红
  '#F05654',  // 粉红
  '#C3272B',  // 茜色
  '#45B787',  // 松花绿
  '#21A675',  // 青碧
  '#2B4490',  // 宝蓝
  '#003152',  // 藏蓝
  '#126E82',  // 靛青
  '#789262',  // 竹青
  '#A3E2C5',  // 青白

  // 日本传统色（15个）
  '#EF4136',  // 红丹
  '#F75C2F',  // 朱红
  '#F6C555',  // 玉子色
  '#7BA23F',  // 若竹色
  '#4C6CB3',  // 瑠璃色
  '#74325C',  // 古代紫
  '#66327C',  // 江戸紫
  '#2EA9DF',  // 空色
  '#0089A7',  // 浅葱色
  '#6A4C9C',  // 菫色
  '#B28FCE',  // 藤色
  '#DB4D6D',  // 桜色
  '#2D6D4B',  // 常盤色
  '#86C166',  // 若草色
  '#5B622E',  // 松叶色
]

/**
 * 侧边栏颜色预设（浅色主题友好）
 */
export const SIDEBAR_COLORS = [
  // 浅色系（适合浅色主题）
  '#ffffff',  // 纯白
  '#f5f5f5',  // 浅灰
  '#fafafa',  // 更浅灰
  '#f0f2f5',  // 蓝灰
  '#e6f7ff',  // 浅蓝
  '#f6ffed',  // 浅绿
  '#fff7e6',  // 浅橙
  '#fff2e8',  // 浅粉
  '#f9f0ff',  // 浅紫
  '#e6fffb',  // 浅青
  
  // 中等深度（通用）
  '#001529',  // 深蓝（经典）
  '#1890ff',  // 蓝色
  '#52c41a',  // 绿色
  '#faad14',  // 橙色
  '#f5222d',  // 红色
  '#722ed1',  // 紫色
  '#13c2c2',  // 青色
  '#eb2f96',  // 粉色
  '#fa8c16',  // 深橙
  '#a0d911',  // 青绿
  
  // 深色系（适合深色主题）
  '#000000',  // 全黑（推荐）
  '#1f1f1f',  // 很深灰
  '#262626',  // 深灰
  '#303030',  // 中等深灰
  '#404040',  // 浅深灰
]

/**
 * 配置版本信息
 */
export const CONFIG_VERSION = {
  version: '1.0.0',
  lastUpdated: '2024-03-20',
  description: 'Lean.Hbt 默认配置'
}

/**
 * 验证UI设置数据的完整性
 */
export const isValidUISettings = (settings: any): settings is UISettings => {
  if (!settings || typeof settings !== 'object') return false
  
  const requiredFields = [
    'themeMode', 'navMode', 'fixedHeader', 'showBreadcrumb', 
    'showTabs', 'showFooter', 'autoHideHeader', 'sidebarColor', 
    'primaryColor', 'showWatermark', 'showLogo', 'animateTitle', 
    'keepTabs', 'showTabIcon'
  ]
  
  return requiredFields.every(field => field in settings)
}

/**
 * 验证应用配置数据的完整性
 */
export const isValidAppConfig = (config: any): config is HbtAppConfig => {
  if (!config || typeof config !== 'object') return false
  
  const requiredFields = [
    'watermark', 'register', 'features', 'security', 'theme', 
    'title', 'subtitle', 'logo', 'copyright', 'version'
  ]
  
  return requiredFields.every(field => field in config)
} 