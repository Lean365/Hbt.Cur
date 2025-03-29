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
import setupIcons from '@/utils/icons'
import { signalRService } from '@/utils/SignalR/service'

// 导入 date-fns 及其语言包
import { format, formatDistance, formatRelative, isDate } from 'date-fns'
import { zhCN, zhTW, ja, ko, enUS, fr, es, ru, arSA } from 'date-fns/locale'

// 语言包映射
const LOCALE_MAP: Record<string, any> = {
  // 东亚语言
  'zh-cn': zhCN, // 简体中文
  'zh-tw': zhTW, // 繁体中文
  ja: ja, // 日语
  ko: ko, // 韩语

  // 联合国官方语言
  en: enUS, // 英语
  fr: fr, // 法语
  es: es, // 西班牙语
  ru: ru, // 俄语
  ar: arSA // 阿拉伯语
}

// 导入基础组件
import HbtFontSize from '@/components/Base/FontSize.vue'
import HbtFullScreen from '@/components/Base/FullScreen.vue'
import HbtHolidayTheme from '@/components/Base/HolidayTheme.vue'
import HbtLocaleSelect from '@/components/Base/LocaleSelect.vue'
import HbtMemorialTheme from '@/components/Base/MemorialTheme.vue'
import HbtNotificationCenter from '@/components/Base/NotificationCenter.vue'
import HbtNotificationItem from '@/components/Base/NotificationItem.vue'
import HbtSliderCaptcha from '@/components/Base/SliderCaptcha.vue'
import HbtSystemSettings from '@/components/Base/SystemSettings.vue'
import HbtThemeSelect from '@/components/Base/ThemeSelect.vue'

// 导入SignalR组件
import HbtConnectionStatus from '@/components/SignalR/ConnectionStatus.vue'
import HbtChatRoom from '@/components/SignalR/ChatRoom.vue'
import HbtOnlineUsers from '@/components/SignalR/OnlineUsers.vue'

// 导入通用业务组件
import HbtDictTag from '@/components/Business/DictTag/index.vue'
import HbtErrorAlert from '@/components/Business/ErrorAlert/index.vue'
import HbtImportDialog from '@/components/Business/ImportDialog/index.vue'
import HbtModal from '@/components/Business/Modal/index.vue'
import HbtOperation from '@/components/Business/Operation/index.vue'
import HbtPagination from '@/components/Business/Pagination/index.vue'
import HbtQuery from '@/components/Business/Query/index.vue'
import HbtSelect from '@/components/Business/Select/index.vue'
import HbtTable from '@/components/Business/Table/index.vue'
import HbtToolbar from '@/components/Business/Toolbar/index.vue'
import HbtTreeSelect from '@/components/Business/TreeSelect/index.vue'
import HbtImageUpload from '@/components/Business/Upload/imageUpload.vue'
import HbtFileUpload from '@/components/Business/Upload/fileUpload.vue'

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

  // 5. 注册全局组件
  // 注册基础组件
  app.component('HbtFontSize', HbtFontSize)
  app.component('HbtFullScreen', HbtFullScreen)
  app.component('HbtHolidayTheme', HbtHolidayTheme)
  app.component('HbtLocaleSelect', HbtLocaleSelect)
  app.component('HbtMemorialTheme', HbtMemorialTheme)
  app.component('HbtNotificationCenter', HbtNotificationCenter)
  app.component('HbtNotificationItem', HbtNotificationItem)
  app.component('HbtSliderCaptcha', HbtSliderCaptcha)
  app.component('HbtSystemSettings', HbtSystemSettings)
  app.component('HbtThemeSelect', HbtThemeSelect)

  // 注册SignalR组件
  app.component('HbtConnectionStatus', HbtConnectionStatus)
  app.component('HbtChatRoom', HbtChatRoom)
  app.component('HbtOnlineUsers', HbtOnlineUsers)

  // 注册业务组件
  app.component('HbtDictTag', HbtDictTag)
  app.component('HbtErrorAlert', HbtErrorAlert)
  app.component('HbtImportDialog', HbtImportDialog)
  app.component('HbtModal', HbtModal)
  app.component('HbtOperation', HbtOperation)
  app.component('HbtPagination', HbtPagination)
  app.component('HbtQuery', HbtQuery)
  app.component('HbtSelect', HbtSelect)
  app.component('HbtTable', HbtTable)
  app.component('HbtToolbar', HbtToolbar)
  app.component('HbtTreeSelect', HbtTreeSelect)
  app.component('HbtImageUpload', HbtImageUpload)
  app.component('HbtFileUpload', HbtFileUpload)

  // 6. 注册图标组件
  setupIcons(app)

  // 7. 初始化设置
  const settingStore = useSettingStore()
  await settingStore.loadFromStorage()

  // 8. 监听语言变化，设置 date-fns 语言包
  watch(
    () => i18n.global.locale.value,
    newLang => {
      try {
        const lang = newLang.toLowerCase()
        const locale = LOCALE_MAP[lang] || enUS

        // 将语言设置到全局配置中
        app.config.globalProperties.$dateLocale = locale
        //console.log('[date-fns] 设置语言成功:', newLang)
      } catch (err) {
        console.warn('[date-fns] 设置语言失败，将使用默认语言 en-US:', err)
        app.config.globalProperties.$dateLocale = enUS
      }
    },
    { immediate: true }
  )

  // 9. 注册权限指令
  setupPermission(app)

  // 10. 初始化SignalR连接
  signalRService.start().catch(error => {
    console.error('初始化SignalR失败:', error)
  })

  // 11. 挂载应用
  app.mount('#app')
}

// 启动应用
bootstrap().catch(err => {
  console.error('[启动] 应用初始化失败:', err)
})
