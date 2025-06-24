import { useUserStore } from '@/stores/user'
import { message } from 'ant-design-vue'
import { getToken } from '@/utils/auth'

// 与后端JWT令牌过期时间保持一致（30分钟）
const INACTIVITY_TIMEOUT = 30 * 60 * 1000
// Token刷新阈值（5分钟内过期时刷新）
const TOKEN_REFRESH_THRESHOLD = 5 * 60 * 1000
// 刷新令牌过期时间（7天）
const REFRESH_TOKEN_EXPIRE_TIME = 7 * 24 * 60 * 60 * 1000

let inactivityTimer: NodeJS.Timeout | null = null
let userStore: ReturnType<typeof useUserStore> | null = null
let isAutoLogoutEnabled = false
let lastActivityTime = Date.now()

// 获取当前用户是否已登录
const isUserLoggedIn = (): boolean => {
  return userStore?.userInfo !== undefined && userStore?.userInfo !== null
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

// 检查刷新令牌是否已过期
const isRefreshTokenExpired = (): boolean => {
  const token = getToken()
  if (!token) {
    return true
  }

  try {
    // 安全地解析JWT token
    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    const tokenData = JSON.parse(decodeURIComponent(escape(atob(base64))))
    const now = Date.now()
    
    // 检查是否在刷新令牌的有效期内
    return now - tokenData.iat * 1000 >= REFRESH_TOKEN_EXPIRE_TIME
  } catch (e) {
    console.error('[自动登出] 刷新令牌检查失败:', e)
    return true
  }
}

// 自动刷新Token
const autoRefreshToken = async () => {
  if (!userStore || !isUserLoggedIn()) {
    return
  }

  try {
    console.log('[自动登出] 检测到Token即将过期，开始自动刷新')
    
    // 检查刷新令牌是否已过期
    if (isRefreshTokenExpired()) {
      console.log('[自动登出] 刷新令牌已过期，执行登出')
      message.warning('登录已过期，请重新登录')
      await userStore.logout()
      window.location.href = '/login'
      return
    }

    // 刷新Token
    const success = await userStore.refreshToken()
    if (success) {
      console.log('[自动登出] Token自动刷新成功')
      // 静默刷新，不显示消息
    } else {
      console.error('[自动登出] Token自动刷新失败')
      message.error('Token刷新失败，请重新登录')
      await userStore.logout()
      window.location.href = '/login'
    }
  } catch (error) {
    console.error('[自动登出] Token自动刷新异常:', error)
    message.error('Token刷新失败，请重新登录')
    await userStore.logout()
    window.location.href = '/login'
  }
}

// 重置定时器
const resetTimer = () => {
  // 如果用户未登录或在登录页面，不启动定时器
  if (!isUserLoggedIn() || isOnLoginPage()) {
    return
  }

  // 更新最后活动时间
  lastActivityTime = Date.now()
  
  // 清除现有定时器
  if (inactivityTimer) {
    clearTimeout(inactivityTimer)
  }

  // 设置新的定时器
  inactivityTimer = setTimeout(async () => {
    if (!userStore || !isUserLoggedIn()) {
      return
    }

    console.log('[自动登出] 30分钟无操作，开始自动登出')
    
    // 显示提示消息
    message.warning('由于长时间未操作，您已被自动登出')

    // 执行登出
    await userStore.logout()

    // 跳转到登录页
    window.location.href = '/login'
  }, INACTIVITY_TIMEOUT)

  console.log('[自动登出] 定时器已重置，将在30分钟后自动登出')
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

  // 检查Token是否即将过期，如果是则自动刷新
  if (isTokenExpiringSoon()) {
    await autoRefreshToken()
  }

  // 重置定时器
  resetTimer()
}

// 设置活动监听器
const setupActivityListeners = () => {
  // 监听用户活动事件
  const events = [
    'mousemove',      // 鼠标移动
    'mousedown',      // 鼠标按下
    'mouseup',        // 鼠标释放
    'click',          // 点击
    'keypress',       // 按键
    'keydown',        // 按键按下
    'keyup',          // 按键释放
    'scroll',         // 滚动
    'touchstart',     // 触摸开始
    'touchmove',      // 触摸移动
    'touchend',       // 触摸结束
    'focus',          // 获得焦点
    'blur',           // 失去焦点
    'input',          // 输入
    'change',         // 值改变
    'submit',         // 表单提交
    'dragstart',      // 拖拽开始
    'drop'            // 拖拽放下
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
  window.addEventListener('blur', handleUserActivity)

  console.log('[自动登出] 活动监听器已设置')
}

// 清理活动监听器
const clearActivityListeners = () => {
  const events = [
    'mousemove', 'mousedown', 'mouseup', 'click', 'keypress', 'keydown', 'keyup',
    'scroll', 'touchstart', 'touchmove', 'touchend', 'focus', 'blur', 'input',
    'change', 'submit', 'dragstart', 'drop'
  ]

  events.forEach(event => {
    document.removeEventListener(event, handleUserActivity)
  })

  // 移除页面可见性和窗口焦点监听器
  document.removeEventListener('visibilitychange', handleUserActivity)
  window.removeEventListener('focus', handleUserActivity)
  window.removeEventListener('blur', handleUserActivity)

  // 清除定时器
  if (inactivityTimer) {
    clearTimeout(inactivityTimer)
    inactivityTimer = null
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
  userStore = store
  isAutoLogoutEnabled = true
  
  console.log('[自动登出] 开始初始化自动登出功能')
  
  // 立即重置定时器
  resetTimer()
  
  // 设置活动监听器
  setupActivityListeners()
  
  console.log('[自动登出] 初始化完成，30分钟无操作将自动登出，有操作时将自动刷新Token')
}

// 清理自动登出
export const clearAutoLogout = () => {
  isAutoLogoutEnabled = false
  clearActivityListeners()
  userStore = null
  
  console.log('[自动登出] 自动登出功能已清理')
}

// 手动重置定时器（供外部调用）
export const resetAutoLogoutTimer = () => {
  if (isAutoLogoutEnabled) {
    resetTimer()
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
    isRefreshTokenExpired: isRefreshTokenExpired(),
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
        console.log('[getTokenInfo] 没有找到Token')
        return null
      }
      
      try {
        const base64Url = token.split('.')[1]
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
        const tokenData = JSON.parse(decodeURIComponent(escape(atob(base64))))
        
        console.log('[getTokenInfo] Token数据:', tokenData)
        console.log('[getTokenInfo] iat:', tokenData.iat, 'exp:', tokenData.exp)
        
        const now = Date.now()
        
        // 安全地处理 iat 和 exp 字段
        const issuedAt = tokenData.iat ? new Date(tokenData.iat * 1000) : null
        const expireTime = tokenData.exp ? tokenData.exp * 1000 : null
        const expiresAt = expireTime ? new Date(expireTime) : null
        
        console.log('[getTokenInfo] 解析后的时间:', {
          issuedAt: issuedAt?.toISOString(),
          expiresAt: expiresAt?.toISOString(),
          expireTime,
          now: new Date(now).toISOString()
        })
        
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
  console.log('[自动登出] 可用函数:')
  console.log('  - getStatus(): 获取自动登出状态')
  console.log('  - resetTimer(): 手动重置定时器')
  console.log('  - checkAndRefreshToken(): 检查并刷新Token')
  console.log('  - triggerActivity(): 触发用户活动')
  console.log('  - getTokenInfo(): 获取Token详细信息')
  console.log('  - testAutoRefresh(): 测试自动刷新Token')
} 