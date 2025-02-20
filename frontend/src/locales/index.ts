import { createI18n } from 'vue-i18n';

import zhCNHome from './home/zh-CN';
import enUSHome from './home/en-US';
import zhCNDashboard from './dashboard/zh-CN';
import enUSDashboard from './dashboard/en-US';
import zhCNTheme from './theme/zh-CN';
import enUSTheme from './theme/en-US';
import zhCNLogin from './login/zh-CN';
import enUSLogin from './login/en-US';
import zhCNLocale from './locale/zh-CN';
import enUSLocale from './locale/en-US';
import zhCNCaptcha from './captcha/zh-CN';
import enUSCaptcha from './captcha/en-US';
import zhCNHeader from './header/zh-CN';
import enUSHeader from './header/en-US';
import zhCNIdentityMenu from './identity/menu/zh-CN';
import enUSIdentityMenu from './identity/menu/en-US';
import zhCNIdentityUser from './identity/user/zh-CN';
import enUSIdentityUser from './identity/user/en-US';
import zhCNCommon from './common/zh-CN';
import enUSCommon from './common/en-US';
import zhCNAdminLang from './admin/language/zh-CN';
import enUSAdminLang from './admin/language/en-US';
import zhCNAdminTrans from './admin/translation/zh-CN';
import enUSAdminTrans from './admin/translation/en-US';
import zhCNFooter from './footer/zh-CN';
import enUSFooter from './footer/en-US';
import zhCNMenu from './menu/zh-CN';
import enUSMenu from './menu/en-US';

// 检查原始翻译文件
console.log('Menu translation file:', zhCNIdentityMenu);

// 合并所有翻译
const messages = {
  'zh-CN': {
    ...zhCNCommon,
    ...zhCNTheme,
    ...zhCNLocale,
    ...zhCNHeader,
    ...zhCNLogin,
    ...zhCNCaptcha,
    ...zhCNHome,
    ...zhCNDashboard,
    ...zhCNAdminLang,
    ...zhCNAdminTrans,
    ...zhCNFooter,
    ...zhCNMenu,
    identity: {
      ...zhCNIdentityMenu.identity,
      ...zhCNIdentityUser.identity
    }
  },
  'en-US': {
    ...enUSCommon,
    ...enUSTheme,
    ...enUSLocale,
    ...enUSHeader,
    ...enUSLogin,
    ...enUSCaptcha,
    ...enUSHome,
    ...enUSDashboard,
    ...enUSAdminLang,
    ...enUSAdminTrans,
    ...enUSFooter,
    ...enUSMenu,
    identity: {
      ...enUSIdentityMenu.identity,
      ...enUSIdentityUser.identity
    }
  }
};

// 检查最终的翻译对象
//console.log('Menu translation file content:', JSON.stringify(zhCNIdentityMenu, null, 2));
//console.log('Final messages content:', JSON.stringify(messages['zh-CN'].identity, null, 2));

const i18n = createI18n({
  legacy: false,
  locale: localStorage.getItem('language') || 'zh-CN',
  fallbackLocale: 'zh-CN',
  messages,
  missing: (locale, key) => {
    console.warn(`Missing translation: locale=${locale}, key=${key}`);
    return key;
  }
});

export default i18n; 