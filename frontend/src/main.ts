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

// 导入 dayjs 及其插件
import dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime'
import customParseFormat from 'dayjs/plugin/customParseFormat'
import weekOfYear from 'dayjs/plugin/weekOfYear'
import isSameOrBefore from 'dayjs/plugin/isSameOrBefore'
import isSameOrAfter from 'dayjs/plugin/isSameOrAfter'

// 配置 dayjs 插件
dayjs.extend(relativeTime)
dayjs.extend(customParseFormat)
dayjs.extend(weekOfYear)
dayjs.extend(isSameOrBefore)
dayjs.extend(isSameOrAfter)

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

  // 6. 监听语言变化，动态加载 dayjs 语言包
  watch(
    () => i18n.global.locale.value,
    async (newLang) => {
      try {
        await import(`dayjs/locale/${newLang}.js`)
        dayjs.locale(newLang)
      } catch (err) {
        console.warn(`[dayjs] 语言包 ${newLang} 加载失败，将使用默认语言 en`)
        dayjs.locale('en')
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