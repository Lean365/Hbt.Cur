import { useUserStore } from '@/stores/user'
import { message, Modal } from 'ant-design-vue'
import { getToken } from '@/utils/auth'

// 与后端JWT令牌过期时间保持一致（30分钟）
const INACTIVITY_TIMEOUT = 30 * 60 * 1000
// Token刷新阈值（5分钟内过期时刷新，与后端保持一致）
const TOKEN_REFRESH_THRESHOLD = 5 * 60 * 1000
// 用户活动检测间隔（1分钟）
const ACTIVITY_CHECK_INTERVAL = 60 * 1000
// 活动防抖间隔（避免频繁重置定时器）
const ACTIVITY_DEBOUNCE_INTERVAL = 5000 // 5秒内只重置一次定时器
// 超时前提醒时间（如1分钟）
const WARNING_TIME = 60 * 1000

let inactivityTimer: NodeJS.Timeout | null = null
let activityCheckTimer: NodeJS.Timeout | null = null
let warningTimer: NodeJS.Timeout | null = null
let userStore: ReturnType<typeof useUserStore> | null = null
let isAutoLogoutEnabled = false
let isInitialized = false // 添加初始化状态标志
let lastActivityTime = Date.now()
let lastTokenCheckTime = Date.now()
let lastResetTime = 0 // 记录上次重置定时器的时间

// 多标签页同步登出
let logoutChannel: BroadcastChannel | null = null
function setupLogoutSync() {
  if (typeof BroadcastChannel !== 'undefined') {
    if (!logoutChannel) {
      logoutChannel = new BroadcastChannel('logout')
      logoutChannel.onmessage = (event) => {
        if (event.data === 'logout') {
          if (userStore) userStore.logout(false)
        }
      }
    }
  } else {
    window.addEventListener('storage', (e) => {
      if (e.key === 'logout') {
        if (userStore) userStore.logout(false)
      }
    })
  }
}

// 获取当前用户是否已登录
const isUserLoggedIn = (): boolean => {
  // 检查用户信息和Token都存在
  const hasUserInfo = userStore?.userInfo !== undefined && userStore?.userInfo !== null
  const hasToken = getToken() !== null
  
  // 如果用户信息存在或Token存在，认为用户已登录
  // 这样可以处理Token刷新失败但用户信息被清除的情况
  return hasUserInfo || hasToken
}

// 检查是否在登录页面
const isOnLoginPage = (): boolean => {
  return window.location.pathname === '/login' || 
         window.location.pathname === '/auth/login' ||
         window.location.pathname === '/'
}

// 检查Token是否即将过期
const isTokenExpiringSoon = (): boolean => {
  const token = getToken()
  if (!token) {
    return false
  }

  try {
    // 安全地解析JWT token
    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    const tokenData = JSON.parse(decodeURIComponent(escape(atob(base64))))
    const expireTime = tokenData.exp * 1000 // 转换为毫秒
    const now = Date.now()
    
    // 如果token将在5分钟内过期，需要刷新
    return expireTime - now < TOKEN_REFRESH_THRESHOLD
  } catch (e) {
    console.error('[自动登出] Token解析失败:', e)
    return false
  }
}

// 自动刷新Token
const autoRefreshToken = async () => {
  if (!userStore || !isUserLoggedIn()) {
    return
  }

  try {
    console.log('[自动登出] 检测到Token即将过期，开始自动刷新')
    
    // 刷新Token
    const success = await userStore.refreshToken()
    if (success) {
      console.log('[自动登出] Token自动刷新成功')
      // 静默刷新，不显示消息
    } else {
      console.warn('[自动登出] Token自动刷新失败，但不立即登出')
      // 刷新失败时不立即登出，让用户继续使用当前token
      // 只有在token真正过期时才登出
    }
  } catch (error) {
    console.warn('[自动登出] Token自动刷新异常，但不立即登出:', error)
    // 刷新异常时不立即登出，让用户继续使用当前token
    // 只有在token真正过期时才登出
  }
}

// 重置定时器
const resetTimer = async (force = false) => {
  // 如果用户未登录或在登录页面，不启动定时器
  if (!isUserLoggedIn() || isOnLoginPage()) {
    return
  }

  // 防抖：如果不是强制重置，且距离上次重置时间不足5秒，则不重置
  const now = Date.now()
  if (!force && now - lastResetTime < ACTIVITY_DEBOUNCE_INTERVAL) {
    return
  }

  // 更新最后活动时间和重置时间
  lastActivityTime = now
  lastResetTime = now

  // 清除现有定时器
  if (inactivityTimer) {
    clearTimeout(inactivityTimer)
  }
  if (activityCheckTimer) {
    clearInterval(activityCheckTimer)
  }
  if (warningTimer) {
    clearTimeout(warningTimer)
    warningTimer = null
  }

  // 每次有操作都主动刷新token，确保token滑动延长
  if (userStore && isUserLoggedIn()) {
    try {
      await userStore.refreshToken()
    } catch (e) {
      console.warn('[自动登出] Token刷新失败，但不立即登出:', e)
      // 刷新失败时不立即登出，让用户继续使用当前token
      // 只有在token真正过期时才登出
      return
    }
  }

  // 获取Token过期时间
  const token = getToken()
  if (!token) {
    return
  }

  try {
    // 解析JWT token获取过期时间
    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    const tokenData = JSON.parse(decodeURIComponent(escape(atob(base64))))
    const expireTime = tokenData.exp * 1000 // 转换为毫秒
    const currentTime = Date.now()
    // 计算到Token过期的剩余时间
    const timeUntilExpiry = expireTime - currentTime
    if (timeUntilExpiry <= 0) {
      handleAutoLogout()
      return
    }
    // 设置定时器在Token过期时登出
    inactivityTimer = setTimeout(() => {
      handleAutoLogout()
    }, timeUntilExpiry)
    // 设置定期检查Token状态的定时器（每分钟检查一次）
    activityCheckTimer = setInterval(() => {
      checkAndRefreshTokenIfNeeded()
    }, ACTIVITY_CHECK_INTERVAL)
    // 设置提醒定时器（超时前弹窗）
    let timeUntilWarning = timeUntilExpiry - WARNING_TIME
    if (timeUntilWarning < 0) timeUntilWarning = 0
    warningTimer = setTimeout(() => {
      showTimeoutWarning()
    }, timeUntilWarning)
  } catch (e) {
    // 如果Token解析失败，使用默认的30分钟定时器
    inactivityTimer = setTimeout(() => {
      handleAutoLogout()
    }, INACTIVITY_TIMEOUT)
  }
}

function showTimeoutWarning() {
  Modal.confirm({
    title: '会话即将超时',
    content: '您已长时间未操作，是否继续保持登录？',
    okText: '继续操作',
    cancelText: '退出登录',
    onOk: () => resetTimer(true),
    onCancel: () => handleAutoLogout()
  })
}

// 执行自动登出
const handleAutoLogout = async () => {
  if (!userStore || !isUserLoggedIn()) {
    return
  }
  // 多标签页同步广播
  if (typeof BroadcastChannel !== 'undefined') {
    if (!logoutChannel) logoutChannel = new BroadcastChannel('logout')
    logoutChannel.postMessage('logout')
  } else {
    localStorage.setItem('logout', Date.now().toString())
  }

  console.log('[自动登出] 开始自动登出')
  
  // 显示提示消息
  message.warning('登录已过期，请重新登录')

  // 执行登出
  await userStore.logout()

  // 跳转到登录页
  window.location.href = '/login'
}

// 检查并刷新Token（如果需要）
const checkAndRefreshTokenIfNeeded = async () => {
  if (!isUserLoggedIn() || isOnLoginPage()) {
    return
  }

  // 检查Token是否即将过期
  if (isTokenExpiringSoon()) {
    console.log('[自动登出] 检测到Token即将过期，开始自动刷新')
    await autoRefreshToken()
  }
}

// 处理用户活动
const handleUserActivity = async () => {
  // 如果自动登出未启用，不处理活动
  if (!isAutoLogoutEnabled) {
    return
  }

  // 如果用户未登录或在登录页面，不重置定时器
  if (!isUserLoggedIn() || isOnLoginPage()) {
    return
  }

  // 只在Token即将过期时刷新（与后端阈值保持一致）
  if (isTokenExpiringSoon()) {
    try {
      const success = await userStore?.refreshToken?.()
      if (!success) {
        console.warn('[自动登出] Token刷新失败，但不立即登出')
        // 刷新失败时不立即登出，让用户继续使用当前token
        return
      }
      // Token刷新成功后，强制重置定时器
      resetTimer(true)
      return
    } catch (error) {
      console.warn('[自动登出] Token刷新异常，但不立即登出:', error)
      // 刷新异常时不立即登出，让用户继续使用当前token
      return
    }
  }

  // 重置定时器（基于Token过期时间，使用防抖）
  resetTimer(false)
}

// 设置活动监听器
const setupActivityListeners = () => {
  // 监听用户活动事件（增加移动端触摸事件）
  const events = [
    'click',          // 点击
    'keydown',        // 按键按下
    'scroll',         // 滚动
    'mousedown',      // 鼠标按下
    'input',          // 输入
    'change',         // 值改变
    'submit',         // 表单提交
    'touchstart',     // 触摸开始（移动端）
    'touchmove',      // 触摸移动（移动端）
    'touchend'        // 触摸结束（移动端）
  ]

  events.forEach(event => {
    document.addEventListener(event, handleUserActivity, { passive: true })
  })

  // 监听页面可见性变化
  document.addEventListener('visibilitychange', () => {
    if (document.visibilityState === 'visible') {
      // 页面变为可见时，检查是否需要重置定时器和刷新Token
      handleUserActivity()
    }
  })

  // 监听窗口焦点变化
  window.addEventListener('focus', handleUserActivity)

  console.log('[自动登出] 活动监听器已设置')
}

// 清理活动监听器
const clearActivityListeners = () => {
  const events = [
    'click', 'keydown', 'scroll', 'mousedown', 'input', 'change', 'submit',
    'touchstart', 'touchmove', 'touchend'
  ]

  events.forEach(event => {
    document.removeEventListener(event, handleUserActivity)
  })

  // 移除页面可见性和窗口焦点监听器
  document.removeEventListener('visibilitychange', handleUserActivity)
  window.removeEventListener('focus', handleUserActivity)

  // 清除定时器
  if (inactivityTimer) {
    clearTimeout(inactivityTimer)
    inactivityTimer = null
  }
  if (activityCheckTimer) {
    clearInterval(activityCheckTimer)
    activityCheckTimer = null
  }
  if (warningTimer) {
    clearTimeout(warningTimer)
    warningTimer = null
  }

  console.log('[自动登出] 活动监听器已清理')
}

// 获取剩余时间（用于调试）
const getRemainingTime = (): number => {
  if (!inactivityTimer) {
    return 0
  }
  
  const elapsed = Date.now() - lastActivityTime
  return Math.max(0, INACTIVITY_TIMEOUT - elapsed)
}

// 初始化自动登出
export const initAutoLogout = (store: ReturnType<typeof useUserStore>) => {
  // 如果已经初始化过，直接返回
  if (isInitialized) {
    console.log('[自动登出] 已经初始化过，跳过重复初始化')
    return
  }
  
  userStore = store
  isAutoLogoutEnabled = true
  isInitialized = true
  
  console.log('[自动登出] 开始初始化自动登出功能')
  
  // 立即重置定时器
  resetTimer(true)
  
  // 设置活动监听器
  setupActivityListeners()
  setupLogoutSync()
  
  console.log('[自动登出] 初始化完成，30分钟无操作将自动登出，有操作时将自动刷新Token')
}

// 清理自动登出
export const clearAutoLogout = () => {
  isAutoLogoutEnabled = false
  isInitialized = false // 重置初始化状态
  clearActivityListeners()
  userStore = null
  
  console.log('[自动登出] 自动登出功能已清理')
}

// 手动重置定时器（供外部调用）
export const resetAutoLogoutTimer = () => {
  if (isAutoLogoutEnabled) {
    resetTimer(true) // 外部调用时强制重置
  }
}

// 手动检查并刷新Token（供外部调用）
export const checkAndRefreshToken = async () => {
  if (isAutoLogoutEnabled && isUserLoggedIn() && !isOnLoginPage()) {
    if (isTokenExpiringSoon()) {
      await autoRefreshToken()
    }
  }
}

// 获取自动登出状态（用于调试）
export const getAutoLogoutStatus = () => {
  return {
    isEnabled: isAutoLogoutEnabled,
    isLoggedIn: isUserLoggedIn(),
    isOnLoginPage: isOnLoginPage(),
    isTokenExpiringSoon: isTokenExpiringSoon(),
    remainingTime: getRemainingTime(),
    lastActivityTime,
    hasTimer: !!inactivityTimer
  }
}

// 添加全局调试函数（仅在开发环境）
if (import.meta.env.DEV) {
  // 将调试函数添加到全局对象
  ;(window as any).HbtAutoLogout = {
    // 获取状态
    getStatus: getAutoLogoutStatus,
    // 手动重置定时器
    resetTimer: resetAutoLogoutTimer,
    // 手动检查并刷新Token
    checkAndRefreshToken,
    // 手动触发用户活动
    triggerActivity: () => handleUserActivity(),
    // 获取Token信息
    getTokenInfo: () => {
      const token = getToken()
      if (!token) {
        return null
      }
      
      try {
        const base64Url = token.split('.')[1]
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
        const tokenData = JSON.parse(decodeURIComponent(escape(atob(base64))))
        
        const now = Date.now()
        
        // 安全地处理 iat 和 exp 字段
        const issuedAt = tokenData.iat ? new Date(tokenData.iat * 1000) : null
        const expireTime = tokenData.exp ? tokenData.exp * 1000 : null
        const expiresAt = expireTime ? new Date(expireTime) : null
        
        return {
          issuedAt,
          expiresAt,
          remainingTime: expireTime ? expireTime - now : 0,
          remainingMinutes: expireTime ? Math.floor((expireTime - now) / 1000 / 60) : 0,
          isExpiringSoon: expireTime ? expireTime - now < TOKEN_REFRESH_THRESHOLD : false,
          isExpired: expireTime ? expireTime - now <= 0 : true,
          tokenData // 添加原始数据用于调试
        }
      } catch (e) {
        console.error('[getTokenInfo] Token解析失败:', e)
        return { error: 'Token解析失败', details: e }
      }
    },
    // 测试自动刷新Token
    testAutoRefresh: async () => {
      console.log('[调试] 开始测试自动刷新Token')
      await checkAndRefreshToken()
    },
    // 模拟Token即将过期（用于测试）
    simulateTokenExpiring: () => {
      console.log('[调试] 模拟Token即将过期')
      // 这里可以临时修改TOKEN_REFRESH_THRESHOLD来测试
      console.log('当前Token信息:', (window as any).HbtAutoLogout.getTokenInfo())
    }
  }
  
  console.log('[自动登出] 调试工具已加载，使用 window.HbtAutoLogout 访问调试函数')
} 