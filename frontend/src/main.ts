import { createApp } from 'vue'
import { createPinia } from 'pinia'
import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/reset.css'
import App from './App.vue'
import router from './router'
import i18n from './locales'
import { useSettingStore } from '@/stores/settings'

import './assets/styles/index.less'

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

  // 6. 挂载应用
  app.mount('#app')
}

// 启动应用
bootstrap().catch(err => {
  console.error('[启动] 应用初始化失败:', err)
}) 