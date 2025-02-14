import { createI18n } from 'vue-i18n';

import zhCNTheme from './theme/zh-CN';
import enUSTheme from './theme/en-US';
import zhCNLogin from './login/zh-CN';
import enUSLogin from './login/en-US';
import zhCNMenu from './menu/zh-CN';
import enUSMenu from './menu/en-US';
import zhCNLocale from './locale/zh-CN';
import enUSLocale from './locale/en-US';
import zhCNCaptcha from './captcha/zh-CN';
import enUSCaptcha from './captcha/en-US';

// 合并所有翻译
export const messages = {
  'zh-CN': {
    ...zhCNTheme,
    ...zhCNLogin,
    ...zhCNMenu,
    ...zhCNLocale,
    ...zhCNCaptcha
  },
  'en-US': {
    ...enUSTheme,
    ...enUSLogin,
    ...enUSMenu,
    ...enUSLocale,
    ...enUSCaptcha
  }
};

const i18n = createI18n({
  legacy: false,
  locale: localStorage.getItem('locale') || 'zh-CN', // 从本地存储读取语言设置，默认中文
  fallbackLocale: 'en-US',
  messages
});

export default i18n; 