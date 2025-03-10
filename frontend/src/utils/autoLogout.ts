import { useUserStore } from '@/stores/user'
import { useRouter } from 'vue-router'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'

// 30分钟无操作自动登出
const INACTIVITY_TIMEOUT = 30 * 60 * 1000

let inactivityTimer: NodeJS.Timeout | null = null

// 重置定时器
const resetTimer = () => {
  if (inactivityTimer) {
    clearTimeout(inactivityTimer)
  }

  inactivityTimer = setTimeout(async () => {
    const userStore = useUserStore()
    const router = useRouter()
    const { t } = useI18n()

    // 显示提示消息
    message.warning(t('auth.autoLogout'))

    // 执行登出
    await userStore.logout()

    // 跳转到登录页
    router.push('/login')
  }, INACTIVITY_TIMEOUT)
}

// 设置活动监听器
const setupActivityListeners = () => {
  // 监听用户活动
  const events = ['mousemove', 'mousedown', 'keypress', 'scroll', 'touchstart']
  events.forEach(event => {
    document.addEventListener(event, resetTimer)
  })
}

// 清理活动监听器
const clearActivityListeners = () => {
  const events = ['mousemove', 'mousedown', 'keypress', 'scroll', 'touchstart']
  events.forEach(event => {
    document.removeEventListener(event, resetTimer)
  })

  if (inactivityTimer) {
    clearTimeout(inactivityTimer)
    inactivityTimer = null
  }
}

// 初始化自动登出
export const initAutoLogout = () => {
  resetTimer()
  setupActivityListeners()
}

// 清理自动登出
export const clearAutoLogout = () => {
  clearActivityListeners()
} 