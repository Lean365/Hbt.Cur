import { createI18n } from 'vue-i18n';

// 导入管理模块翻译
import adminTranslationZhCN from './admin/translation/zh-CN';
import adminTranslationEnUS from './admin/translation/en-US';
import adminLanguageZhCN from './admin/language/zh-CN';
import adminLanguageEnUS from './admin/language/en-US';

// 导入公共翻译
import commonZhCN from './common/zh-CN';
import commonEnUS from './common/en-US';

// 导入仪表盘翻译
import dashboardZhCN from './dashboard/zh-CN';
import dashboardEnUS from './dashboard/en-US';

// 导入页脚翻译
import footerZhCN from './footer/zh-CN';
import footerEnUS from './footer/en-US';

// 导入页头翻译
import headerZhCN from './header/zh-CN';
import headerEnUS from './header/en-US';

// 导入节日翻译
import holidayZhCN from './holiday/zh-CN';
import holidayEnUS from './holiday/en-US';

// 导入首页翻译
import homeZhCN from './home/zh-CN';
import homeEnUS from './home/en-US';

// 导入身份认证翻译
import identityAuthZhCN from './identity/auth/zh-CN';
import identityAuthEnUS from './identity/auth/en-US';

// 导入身份菜单翻译
import identityMenuZhCN from './identity/menu/zh-CN';
import identityMenuEnUS from './identity/menu/en-US';

// 导入本地化翻译
import localeZhCN from './locale/zh-CN';
import localeEnUS from './locale/en-US';

// 导入菜单翻译
import menuZhCN from './menu/zh-CN';
import menuEnUS from './menu/en-US';

// 导入分页翻译
import paginationZhCN from './pagination/zh-CN';
import paginationEnUS from './pagination/en-US';

// 导入路由翻译
import routerZhCN from './router/zh-CN';
import routerEnUS from './router/en-US';

// 导入主题翻译
import themeZhCN from './theme/zh-CN';
import themeEnUS from './theme/en-US';

// 合并所有翻译
const messages = {
  'zh-CN': {
    admin: {
      ...adminTranslationZhCN.admin,
      ...adminLanguageZhCN.admin
    },
    ...commonZhCN,
    ...dashboardZhCN,
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
    ...routerEnUS,
    ...themeEnUS
  }
};

// 创建i18n实例
const i18n = createI18n({
  legacy: false, // 使用Composition API模式
  locale: localStorage.getItem('language') || 'zh-CN',
  fallbackLocale: 'zh-CN',
  messages,
  globalInjection: true, // 全局注入 $t 函数
  silentTranslationWarn: true, // 关闭翻译警告
  missingWarn: false, // 关闭缺少翻译警告
  silentFallbackWarn: true // 关闭回退翻译警告
});

export default i18n; 