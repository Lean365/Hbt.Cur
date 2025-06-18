import { useUserStore } from '@/stores/user'
import { message } from 'ant-design-vue'

// 与后端JWT令牌过期时间保持一致（30分钟）
const INACTIVITY_TIMEOUT = 30 * 60 * 1000

let inactivityTimer: NodeJS.Timeout | null = null
let userStore: ReturnType<typeof useUserStore> | null = null

// 重置定时器
const resetTimer = () => {
  if (inactivityTimer) {
    clearTimeout(inactivityTimer)
  }

  inactivityTimer = setTimeout(async () => {
    if (!userStore) return

    // 显示提示消息
    message.warning('由于长时间未操作，您已被自动登出')

    // 执行登出
    await userStore.logout()

    // 跳转到登录页
    window.location.href = '/login'
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
export const initAutoLogout = (store: ReturnType<typeof useUserStore>) => {
  userStore = store
  resetTimer()
  setupActivityListeners()
}

// 清理自动登出
export const clearAutoLogout = () => {
  clearActivityListeners()
  userStore = null
} 