<template>
  <a-config-provider :theme="themeConfig" :locale="antdLocale">
    <div class="app-container">
      <HbtErrorAlert
        v-model:visible="showError"
        :type="errorType"
        :message="errorMessage"
        :description="errorDescription"
        :show-retry="true"
        @retry="handleRetry"
        @close="handleErrorClose"
      />
      <router-view></router-view>
    </div>
  </a-config-provider>
</template>

<script lang="ts" setup>
import 'ant-design-vue/dist/reset.css'
import { ConfigProvider } from 'ant-design-vue'
import { onMounted, computed, watch, onUnmounted, ref, nextTick } from 'vue'
import { theme } from 'ant-design-vue'
import { useThemeStore } from '@/stores/theme'
import { useMemorialStore } from '@/stores/memorial'
import { useAppStore } from '@/stores/app'
import zhCN from 'ant-design-vue/es/locale/zh_CN'
import enUS from 'ant-design-vue/es/locale/en_US'
import arEG from 'ant-design-vue/es/locale/ar_EG'
import esES from 'ant-design-vue/es/locale/es_ES'
import frFR from 'ant-design-vue/es/locale/fr_FR'
import jaJP from 'ant-design-vue/es/locale/ja_JP'
import koKR from 'ant-design-vue/es/locale/ko_KR'
import ruRU from 'ant-design-vue/es/locale/ru_RU'
import zhTW from 'ant-design-vue/es/locale/zh_TW'
import { initAutoLogout, clearAutoLogout } from '@/utils/autoLogout'
import { useDictStore } from '@/stores/dict'
import { useWebSocketStore } from '@/stores/websocket'
import HbtErrorAlert from '@/components/Business/HbtErrorAlert/index.vue'
import { useUserStore } from '@/stores/user'

const themeStore = useThemeStore()
const memorialStore = useMemorialStore()
const appStore = useAppStore()
const wsStore = useWebSocketStore()
const userStore = useUserStore()
const isDark = computed(() => themeStore.isDarkMode)
const currentTheme = computed(() => memorialStore.currentTheme)
const isMemorialMode = computed(() => memorialStore.isMemorialMode)

// 语言包映射
const localeMap = {
  'en-US': enUS,
  'zh-CN': zhCN,
  'ar-SA': arEG,
  'es-ES': esES,
  'fr-FR': frFR,
  'ja-JP': jaJP,
  'ko-KR': koKR,
  'ru-RU': ruRU,
  'zh-TW': zhTW
}

// 当前 Ant Design Vue 的语言包
const currentAntdLocale = ref(localeMap[appStore.language as keyof typeof localeMap] || zhCN)

// 根据当前语言获取 Ant Design Vue 的语言包
const antdLocale = computed(() => currentAntdLocale.value)

// 监听语言变化，更新 Ant Design Vue 的语言包
watch(() => appStore.language, (newLocale) => {
  currentAntdLocale.value = localeMap[newLocale as keyof typeof localeMap] || zhCN
})

// 计算主题配置
const themeConfig = computed(() => {
  const memorialTheme = memorialStore.currentTheme?.token || {}
  const isDarkMode = themeStore.isDarkMode

  return {
    algorithm: isDarkMode ? theme.darkAlgorithm : theme.defaultAlgorithm,
    token: {
      ...memorialTheme,
      colorPrimary: memorialTheme.colorPrimary || themeStore.primaryColor,
      borderRadius: 6,
      // 添加更多全局 token
      wireframe: false, // 线框模式
      colorBgContainer: isDarkMode ? '#141414' : '#ffffff',
      colorBgLayout: isDarkMode ? '#000000' : '#f5f5f5',
      colorBgElevated: isDarkMode ? '#1f1f1f' : '#ffffff',
      // 文字颜色
      colorText: isDarkMode ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.88)',
      colorTextSecondary: isDarkMode ? 'rgba(255, 255, 255, 0.65)' : 'rgba(0, 0, 0, 0.65)',
      colorTextTertiary: isDarkMode ? 'rgba(255, 255, 255, 0.45)' : 'rgba(0, 0, 0, 0.45)',
      colorTextQuaternary: isDarkMode ? 'rgba(255, 255, 255, 0.25)' : 'rgba(0, 0, 0, 0.25)',
      // 输入框
      colorBgContainer_input: isDarkMode ? '#000000' : '#ffffff',
      colorBorder: isDarkMode ? '#424242' : '#d9d9d9',
      colorBorderSecondary: isDarkMode ? '#303030' : '#f0f0f0'
    },
    components: {
      Layout: {
        colorBgHeader: 'var(--ant-color-bg-container)',
        colorBgBody: 'var(--ant-color-bg-layout)',
        colorBgTrigger: 'var(--ant-color-bg-container)'
      },
      Menu: {
        colorItemBg: 'var(--ant-color-bg-container)',
        colorSubItemBg: 'var(--ant-color-bg-container)',
        colorItemBgSelected: 'var(--ant-color-primary-1)',
        colorItemBgActive: 'var(--ant-color-primary-1)'
      },
      Card: {
        colorBgContainer: 'var(--ant-color-bg-container)'
      },
      Input: {
        colorBgContainer: 'var(--ant-color-bg-container_input)',
        colorBorder: 'var(--ant-color-border)',
        colorText: 'var(--ant-color-text)'
      },
      Form: {
        labelColor: 'var(--ant-color-text)',
        colorText: 'var(--ant-color-text)'
      }
    }
  }
})

// 错误提示相关
const showError = ref(false)
const errorType = ref<'warning' | 'error'>('warning')
const errorMessage = ref('')
const errorDescription = ref('')

// 监听 WebSocket 错误
const handleWebSocketError = () => {
  if (wsStore.error) {
    errorType.value = 'error'
    errorMessage.value = '连接错误'
    errorDescription.value = wsStore.error
    showError.value = true
  }
}

// 监听 WebSocket 连接状态
const handleWebSocketConnection = () => {
  if (!wsStore.connected) {
    errorType.value = 'warning'
    errorMessage.value = '连接断开'
    errorDescription.value = '正在尝试重新连接...'
    showError.value = true
  } else {
    showError.value = false
  }
}

// 处理重试
const handleRetry = () => {
  wsStore.connect()
}

// 处理关闭错误提示
const handleErrorClose = () => {
  showError.value = false
}

// 组件挂载时连接 WebSocket
onMounted(async () => {
  console.log('🚀🚀🚀 [App] onMounted 开始执行 🚀🚀🚀')
  
  const dictStore = useDictStore()
  dictStore.clearCache()
  themeStore.initTheme()
  memorialStore.initMemorialMode()
  document.documentElement.style.colorScheme = isDark.value ? 'dark' : 'light'
  initAutoLogout(userStore)
  wsStore.connect()
  
  console.log('[App] onMounted 执行完成')
})

onUnmounted(() => {
  clearAutoLogout()
  wsStore.disconnect()
})

watch(isDark, (newValue) => {
  document.documentElement.style.colorScheme = newValue ? 'dark' : 'light'
})

// 监听纪念模式的变化
watch(isMemorialMode, (newValue) => {
  if (newValue) {
    document.body.classList.add('memorial-mode')
  } else {
    document.body.classList.remove('memorial-mode')
    // 当纪念模式关闭时，确保自动模式开启
    nextTick(() => {
      memorialStore.checkHolidays()
    })
  }
})

// 监听 WebSocket 状态变化
watch(() => wsStore.error, handleWebSocketError)
watch(() => wsStore.connected, handleWebSocketConnection)
</script>

<style lang="less">
.app-container {
  min-height: 100vh;
  background-color: var(--ant-color-bg-layout);
}

// 亮色主题下的纪念模式
body:not(.dark-mode).memorial-mode,
body:not(.dark-mode).memorial-mode .ant-dropdown,
body:not(.dark-mode).memorial-mode .ant-modal-root,
body:not(.dark-mode).memorial-mode .ant-message,
body:not(.dark-mode).memorial-mode .ant-notification,
body:not(.dark-mode).memorial-mode .ant-drawer {
  filter: grayscale(100%) contrast(90%) brightness(90%);
  transition: filter 0.3s ease;
}

// 暗黑主题下的纪念模式
body.dark-mode.memorial-mode,
body.dark-mode.memorial-mode .ant-dropdown,
body.dark-mode.memorial-mode .ant-modal-root,
body.dark-mode.memorial-mode .ant-message,
body.dark-mode.memorial-mode .ant-notification,
body.dark-mode.memorial-mode .ant-drawer {
  filter: grayscale(100%) contrast(70%) brightness(70%);
  transition: filter 0.3s ease;
}
</style>

