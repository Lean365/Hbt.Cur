import { createI18n } from 'vue-i18n'

// 导入管理模块翻译
import adminTranslationZhCN from './admin/translation/zh-CN'
import adminTranslationEnUS from './admin/translation/en-US'
import adminLanguageZhCN from './admin/language/zh-CN'
import adminLanguageEnUS from './admin/language/en-US'

// 导入公共翻译
import commonZhCN from './common/zh-CN'
import commonEnUS from './common/en-US'

// 导入仪表盘翻译
import dashboardZhCN from './dashboard/zh-CN'
import dashboardEnUS from './dashboard/en-US'

// 导入错误页面翻译
import errorZhCN from './error/zh-CN'
import errorEnUS from './error/en-US'

// 导入页脚翻译
import footerZhCN from './components/footer/zh-CN'
import footerEnUS from './components/footer/en-US'

// 导入页头翻译
import headerZhCN from './components/header/zh-CN'
import headerEnUS from './components/header/en-US'

// 导入节日翻译
import holidayZhCN from './components/holiday/zh-CN'
import holidayEnUS from './components/holiday/en-US'

// 导入首页翻译
import homeZhCN from './home/zh-CN'
import homeEnUS from './home/en-US'

// 导入身份认证翻译
import identityAuthZhCN from './identity/auth/zh-CN'
import identityAuthEnUS from './identity/auth/en-US'

// 导入身份菜单翻译
import identityMenuZhCN from './identity/menu/zh-CN'
import identityMenuEnUS from './identity/menu/en-US'

// 导入本地化翻译
import localeZhCN from './components/locale/zh-CN'
import localeEnUS from './components/locale/en-US'

// 导入菜单翻译
import menuZhCN from './components/menu/zh-CN'
import menuEnUS from './components/menu/en-US'

// 导入分页翻译
import paginationZhCN from './components/pagination/zh-CN'
import paginationEnUS from './components/pagination/en-US'

// 导入表格翻译
import tableZhCN from './components/table/zh-CN'
import tableEnUS from './components/table/en-US'

// 导入查询翻译
import queryZhCN from './components/query/zh-CN'
import queryEnUS from './components/query/en-US'

// 导入路由翻译
import routerZhCN from './router/zh-CN'
import routerEnUS from './router/en-US'

// 导入主题翻译
import themeZhCN from './components/theme/zh-CN'
import themeEnUS from './components/theme/en-US'

// 合并所有翻译
const messages = {
  'zh-CN': {
    admin: {
      ...adminTranslationZhCN.admin,
      ...adminLanguageZhCN.admin
    },
    ...commonZhCN,
    ...dashboardZhCN,
    ...errorZhCN,
    ...footerZhCN,
    ...headerZhCN,
    ...holidayZhCN,
    ...homeZhCN,
    identity: {
      ...identityAuthZhCN.identity,
      ...identityMenuZhCN.identity
    },
    ...localeZhCN,
    ...menuZhCN,
    ...paginationZhCN,
    ...tableZhCN,
    ...queryZhCN,
    ...routerZhCN,
    ...themeZhCN
  },
  'en-US': {
    admin: {
      ...adminTranslationEnUS.admin,
      ...adminLanguageEnUS.admin
    },
    ...commonEnUS,
    ...dashboardEnUS,
    ...errorEnUS,
    ...footerEnUS,
    ...headerEnUS,
    ...holidayEnUS,
    ...homeEnUS,
    identity: {
      ...identityAuthEnUS.identity,
      ...identityMenuEnUS.identity
    },
    ...localeEnUS,
    ...menuEnUS,
    ...paginationEnUS,
    ...tableEnUS,
    ...queryEnUS,
    ...routerEnUS,
    ...themeEnUS
  }
}

// 创建i18n实例
const i18n = createI18n({
  legacy: false, // 使用Composition API模式
  locale: localStorage.getItem('language') || 'zh-CN',
  fallbackLocale: 'zh-CN',
  messages,
  globalInjection: true, // 全局注入 $t 函数
  silentTranslationWarn: true, // 关闭翻译警告
  missingWarn: false, // 关闭缺少翻译警告
  silentFallbackWarn: true, // 关闭回退翻译警告
  // 添加语言环境映射
  availableLocales: ['zh-CN', 'en-US'],
  fallbackLocaleChain: {
    'zh': ['zh-CN', 'en-US'],
    'zh-CN': ['zh-CN', 'en-US'],
    'en': ['en-US', 'zh-CN'],
    'en-US': ['en-US', 'zh-CN']
  }
})

export default i18n
