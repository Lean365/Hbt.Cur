//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : main.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 应用程序入口文件
//===================================================================

import { createApp, watch } from 'vue'
import { createPinia } from 'pinia'
import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/reset.css'
import App from './App.vue'
import router from './router'
import i18n from './locales'
import { useSettingStore } from '@/stores/settings'
import { setupPermission } from './directives/permission'

// 导入 date-fns 及其语言包
import { format, formatDistance, formatRelative, isDate } from 'date-fns'
import { zhCN, zhTW, ja, ko, enUS, fr, es, ru, arSA } from 'date-fns/locale'

// 语言包映射
const LOCALE_MAP: Record<string, any> = {
  // 东亚语言
  'zh-cn': zhCN,     // 简体中文
  'zh-tw': zhTW,     // 繁体中文
  'ja': ja,          // 日语
  'ko': ko,          // 韩语
  
  // 联合国官方语言
  'en': enUS,        // 英语
  'fr': fr,          // 法语
  'es': es,          // 西班牙语
  'ru': ru,          // 俄语
  'ar': arSA,        // 阿拉伯语
}

async function bootstrap() {
  const app = createApp(App)

  // 1. 首先注册 Pinia
  const pinia = createPinia()
  app.use(pinia)

  // 2. 注册 Antd
  app.use(Antd)

  // 3. 注册 i18n
  app.use(i18n)

  // 4. 注册路由 - 只注册基础路由
  app.use(router)

  // 5. 初始化设置
  const settingStore = useSettingStore()
  await settingStore.loadFromStorage()

  // 6. 监听语言变化，设置 date-fns 语言包
  watch(
    () => i18n.global.locale.value,
    (newLang) => {
      try {
        const lang = newLang.toLowerCase()
        const locale = LOCALE_MAP[lang] || enUS
        
        // 将语言设置到全局配置中
        app.config.globalProperties.$dateLocale = locale
        console.log('[date-fns] 设置语言成功:', newLang)
      } catch (err) {
        console.warn('[date-fns] 设置语言失败，将使用默认语言 en-US:', err)
        app.config.globalProperties.$dateLocale = enUS
      }
    },
    { immediate: true }
  )

  // 7. 注册权限指令
  setupPermission(app)

  // 8. 挂载应用
  app.mount('#app')
}

// 启动应用
bootstrap().catch(err => {
  console.error('[启动] 应用初始化失败:', err)
}) 