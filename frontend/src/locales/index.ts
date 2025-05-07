import { createI18n } from 'vue-i18n'

// 支持的语言列表
const locales = [
  'en-US', 'zh-CN', 'ar-SA', 'es-ES', 'fr-FR', 
  'ja-JP', 'ko-KR', 'ru-RU', 'zh-TW'
] as const

type Locale = typeof locales[number]

// 模块列表 - 严格按照目录结构顺序
const modules = [
  // admin
  'admin/configs',
  'admin/dicttype',
  'admin/language',
  'admin/translation',
  
  // components
  'components/footer',
  'components/header',
  'components/memorial',
  'components/icon',
  'components/locale',
  'components/menu',
  'components/pagination',
  'components/query',
  'components/table',
  'components/theme',
  'components/setting',
  'components/notification',
  
  // common
  'common',
  
  // dashboard
  'dashboard',
  
  // error
  'error',
  
  // generator
  'generator',
  
  // home
  'home',
  
  // identity
  'identity/auth',
  'identity/dept',
  'identity/menu',
  'identity/post',
  'identity/role',
  'identity/tenant',
  'identity/user',
  
  // realtime
  'realtime/message',
  'realtime/online',
  'realtime/server',

  'routine/file',
  'routine/mailtmpl',
  'routine/mail',
  'routine/notice',
  'routine/quartz',

  'workflow/definition',
  'workflow/history',
  'workflow/instance',
  'workflow/node',
  'workflow/task',
  'workflow/variable',
  
  // router
  'router'
] as const

type Module = typeof modules[number]

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

// 使用 Vite 的 import.meta.glob 动态导入所有翻译文件
const translationModules = import.meta.glob([
  './admin/**/*.ts',
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

// 处理翻译文件
const messages = locales.reduce<Messages>((acc, locale) => {
  acc[locale] = modules.reduce<TranslationMessages>((moduleAcc, module) => {
    const path = `./${module}/${locale}.ts`
    if (translationModules[path]) {
      return deepMerge(moduleAcc, (translationModules[path] as any).default)
    }
    // 如果找不到翻译文件，使用英文作为后备
    const fallbackPath = `./${module}/en-US.ts`
    if (translationModules[fallbackPath]) {
      console.warn(`Missing translation for ${module}/${locale}, using English as fallback`)
      return deepMerge(moduleAcc, (translationModules[fallbackPath] as any).default)
    }
    console.warn(`Missing translation for ${module}/${locale} and no English fallback available`)
    return moduleAcc
  }, {})
  return acc
}, {})

// 创建 i18n 实例
const i18n = createI18n({
  legacy: false,
  locale: import.meta.env.VITE_LOCALE || 'zh-CN',
  fallbackLocale: 'en-US',
  messages,
  silentTranslationWarn: true,
  missingWarn: false,
  fallbackWarn: false,
  silentFallbackWarn: true
})

export default i18n
