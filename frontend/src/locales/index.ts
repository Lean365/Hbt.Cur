import { createI18n } from 'vue-i18n'

// 支持的语言列表
const supportedLocales = [
  'en-US', 'zh-CN', 'ar-SA', 'es-ES', 'fr-FR', 
  'ja-JP', 'ko-KR', 'ru-RU', 'zh-TW'
] as const

type Locale = typeof supportedLocales[number]

// 定义消息类型
interface TranslationMessages {
  [key: string]: any
}

interface Messages {
  [locale: string]: TranslationMessages
}

// 深度合并对象
function deepMerge(target: any, source: any): any {
  if (!source) return target
  if (!target) return source

  const result = { ...target }
  for (const key in source) {
    if (source[key] && typeof source[key] === 'object') {
      result[key] = deepMerge(result[key], source[key])
    } else {
      result[key] = source[key]
    }
  }
  return result
}

// 从文件路径中提取模块路径
function getModulePath(filePath: string): string {
  const path = filePath.replace('./', '').replace('.ts', '')
  return path.split('/').slice(0, -1).join('/')
}

// 从文件路径中提取语言代码
function getLocale(filePath: string): string {
  const locale = filePath.split('/').pop()?.replace('.ts', '') || ''
  // 强制使用连字符格式
  return locale.replace('_', '-')
}

// 将标准格式转换为文件名格式
function getLocaleFileName(locale: string): string {
  // 强制使用连字符格式
  return locale.replace('_', '-')
}

// 检查文件名格式
function validateLocaleFileName(filePath: string): boolean {
  const fileName = filePath.split('/').pop() || ''
  if (fileName.includes('_')) {
    console.error(`Invalid locale file name format: ${fileName}. Use hyphen (-) instead of underscore (_).`)
    return false
  }
  return true
}

// 获取所有可用的语言和模块
const availableLocales = new Set<string>()
const availableModules = new Set<string>()

// 使用 Vite 的 import.meta.glob 动态导入所有翻译文件
const modules = import.meta.glob([
  './audit/**/*.ts',
  './core/**/*.ts',
  './components/**/*.ts',
  './common/**/*.ts',
  './dashboard/**/*.ts',
  './error/**/*.ts',
  './generator/**/*.ts',
  './home/**/*.ts',
  './identity/**/*.ts',
  './realtime/**/*.ts',
  './routine/**/*.ts',
  './router/**/*.ts',
  './workflow/**/*.ts'
], { eager: true })

Object.keys(modules).forEach(path => {
  // 检查文件名格式
  if (!validateLocaleFileName(path)) {
    return
  }
  const locale = getLocale(path)
  const modulePath = getModulePath(path)
  // 只添加支持的语言
  if (supportedLocales.includes(locale as Locale)) {
    availableLocales.add(locale)
  }
  availableModules.add(modulePath)
})

// 将 Set 转换为数组并排序
const locales = Array.from(availableLocales).sort()
const modulePaths = Array.from(availableModules).sort()

// 创建 i18n 实例
const i18n = createI18n({
  legacy: false,
  locale: import.meta.env.VITE_LOCALE || 'zh-CN',
  fallbackLocale: 'zh-CN',
  messages: {},
  silentTranslationWarn: true,
  missingWarn: false,
  fallbackWarn: false,
  silentFallbackWarn: true
})

// 处理翻译文件
const messages = locales.reduce<Messages>((acc, locale) => {
  acc[locale] = modulePaths.reduce<TranslationMessages>((moduleAcc, module) => {
    const path = `./${module}/${getLocaleFileName(locale)}.ts`
    if (modules[path]) {
      return deepMerge(moduleAcc, (modules[path] as any).default)
    }
    // 如果找不到翻译文件，使用后备语言
    const fallbackPath = `./${module}/${getLocaleFileName(i18n.global.fallbackLocale.value as string)}.ts`
    if (modules[fallbackPath]) {
      console.warn(`Missing translation for ${module}/${locale}, using fallback locale`)
      return deepMerge(moduleAcc, (modules[fallbackPath] as any).default)
    }
    console.warn(`Missing translation for ${module}/${locale} and no fallback available`)
    return moduleAcc
  }, {})
  return acc
}, {})

// 更新 i18n 实例的消息 - 为所有语言设置消息
locales.forEach(locale => {
  if (messages[locale]) {
    i18n.global.setLocaleMessage(locale, messages[locale])
  } else {
    console.warn(`No messages found for locale: ${locale}`)
  }
})

export default i18n
