import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'
import { message } from 'ant-design-vue'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useI18n } from 'vue-i18n'
import i18n from '@/locales'
import { getDeviceInfo } from '@/utils/device'
import type { HbtApiResponse } from '@/types/common'

const { t } = i18n.global

interface CustomAxiosRequestConfig extends AxiosRequestConfig {
  retries?: number
  retryDelay?: number
  retryCount?: number
}

// 扩展类型
interface ExtendedAxiosInstance extends AxiosInstance {
  download: (url: string, params?: any) => Promise<void>
  <T = any>(config: CustomAxiosRequestConfig): Promise<T>
  <T = any>(url: string, config?: CustomAxiosRequestConfig): Promise<T>
}

// 创建axios实例
const service = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 100000000000000,
  withCredentials: true  // 允许跨域请求时携带cookie
}) as ExtendedAxiosInstance

// 设置默认重试配置
const defaultRetryConfig = {
  retries: 3,
  retryDelay: 1000
}

// 从cookie中获取指定名称的值
function getCookie(name: string): string | null {
  console.log('[Cookie] 开始获取Cookie:', name)
  console.log('[Cookie] 原始cookie字符串:', document.cookie)
  
  try {
    if (!document.cookie) {
      console.log('[Cookie] cookie字符串为空')
      return null
    }

    const cookies = document.cookie.split(';')
    console.log('[Cookie] 分割后的部分:', JSON.stringify(cookies))

    for (const cookie of cookies) {
      const [key, value] = cookie.trim().split('=')
      if (key === name) {
        console.log('[Cookie] 找到Cookie:', name, '=', value)
        // 解码cookie值
        const decodedValue = decodeURIComponent(value)
        console.log('[Cookie] 解码后的值:', decodedValue)
        return decodedValue
      }
    }
    
    console.log('[Cookie] 未找到Cookie:', name)
    return null
  } catch (error) {
    console.error('[Cookie] 获取Cookie出错:', error)
    return null
  }
}

// 获取CSRF Token的函数
async function getCsrfToken(): Promise<string | null> {
  console.log('[CSRF] 开始获取CSRF Token')
  
  try {
    // 1. 首先从 cookie 获取（优先）
    const cookieToken = getCookie('XSRF-TOKEN')
    if (cookieToken) {
      console.log('[CSRF] 从Cookie获取到Token:', cookieToken)
      return cookieToken
    }
    
    // 2. 如果cookie中没有，尝试从localStorage获取
    const localToken = localStorage.getItem('csrf_token')
    if (localToken) {
      console.log('[CSRF] 从localStorage获取到Token:', localToken)
      return localToken
    }
    
    // 3. 如果都没有，返回null
    console.log('[CSRF] 未找到Token，跳过设置')
    return null
  } catch (error) {
    console.error('[CSRF] 获取Token时出错:', error)
    return null
  }
}

// 请求拦截器
service.interceptors.request.use(
  async (config) => {
    console.log('[请求拦截器] 处理请求:', config.url)
    
    // 获取CSRF token
    const csrfToken = await getCsrfToken()
    if (csrfToken) {
      // 解码CSRF token
      const decodedToken = decodeURIComponent(csrfToken)
      config.headers['X-XSRF-TOKEN'] = decodedToken
      console.log('[CSRF] Token已设置到请求头:', decodedToken)
    }

    // 确保headers对象存在
    config.headers = config.headers || {}
    
    // 设置通用headers
    config.headers['Accept'] = 'application/json'
    config.headers['Content-Type'] = 'application/json'

    // 如果是登录请求，需要特殊处理
    if (config.url?.includes('HbtAuth/login')) {
      const loginData = typeof config.data === 'string' ? JSON.parse(config.data) : config.data
      // 设置租户ID
      if (loginData?.tenantId !== undefined) {
        config.headers['X-Tenant-Id'] = loginData.tenantId.toString()
      }
      // 添加设备信息
      if (!loginData.deviceInfo) {
        loginData.deviceInfo = getDeviceInfo()
        loginData.loginSource = 0 // Web端
        config.data = JSON.stringify(loginData)
      }
    } else {
      // 非登录请求，处理token
      const token = getToken()
      console.log('[请求拦截器] 获取到的token:', token ? `${token.substring(0, 10)}...` : '未获取到token')
      
      // 判断是否为公共接口
      const isPublicApi = config.url?.includes('HbtLanguage/supported') || 
                         config.url?.includes('HbtAuth/captcha') ||
                         config.url?.includes('HbtAuth/csrf')
      
      // 判断是否为登出请求
      const isLogoutRequest = config.url?.includes('HbtAuth/logout')
      
      if (isPublicApi) {
        console.log('[请求拦截器] 当前请求为公共接口，无需token:', config.url)
      }
      
      if (token) {
        // 添加Bearer前缀
        config.headers['Authorization'] = `Bearer ${token}`
        console.log('[请求拦截器] 设置Authorization:', `Bearer ${token.substring(0, 10)}...`)
        
        // 从JWT令牌中获取租户ID
        try {
          const tokenParts = token.split('.')
          if (tokenParts.length === 3) {
            // 记录完整的token结构
            console.log('[请求拦截器] Token结构验证:', {
              header: tokenParts[0].length,
              payload: tokenParts[1].length,
              signature: tokenParts[2].length
            })
            
            const payload = JSON.parse(atob(tokenParts[1]))
            
            // 安全地转换时间戳
            const formatTimestamp = (timestamp: number | undefined) => {
              if (!timestamp) return 'N/A'
              try {
                const date = new Date(timestamp * 1000)
                return date.toISOString()
              } catch (error) {
                return `Invalid timestamp: ${timestamp}`
              }
            }
            
            console.log('[请求拦截器] Token payload:', {
              ...payload,
              exp: formatTimestamp(payload.exp),
              iat: formatTimestamp(payload.iat)
            })
            
            if (payload.tenant_id !== undefined) {
              config.headers['X-Tenant-Id'] = payload.tenant_id.toString()
              console.log('[请求拦截器] 从token中获取到租户ID:', payload.tenant_id)
            }
            
            // 检查token是否过期
            const now = Math.floor(Date.now() / 1000)
            if (payload.exp && typeof payload.exp === 'number') {
              if (payload.exp < now) {
                console.error('[请求拦截器] Token已过期:', {
                  当前时间: new Date(now * 1000).toISOString(),
                  过期时间: new Date(payload.exp * 1000).toISOString()
                })
              } else {
                console.log('[请求拦截器] Token有效期检查通过，将在以下时间过期:', 
                  new Date(payload.exp * 1000).toISOString())
              }
            } else {
              console.warn('[请求拦截器] Token中未找到有效的过期时间')
            }
          } else {
            console.error('[请求拦截器] Token格式错误: 不是标准的JWT格式')
          }
        } catch (error) {
          console.error('[请求拦截器] JWT解析失败:', error)
        }
      } else if (!isPublicApi) {
        console.warn('[请求拦截器] 非公共接口请求未找到token，请求可能会失败')
      }
    }
    
    // 详细记录请求信息
    console.log('[请求拦截器] 最终请求配置:', {
      url: config.url,
      method: config.method,
      params: config.params || '(无参数)',
      data: config.data ? (typeof config.data === 'string' ? '(JSON字符串)' : config.data) : '(无数据)',
      headers: {
        ...config.headers,
        Authorization: config.headers.Authorization ? '(已设置)' : '(未设置)',
        'X-Tenant-Id': config.headers['X-Tenant-Id'] || '(未设置)',
        'X-XSRF-TOKEN': config.headers['X-XSRF-TOKEN'] ? '(已设置)' : '(未设置)'
      }
    })
    
    // 额外记录实际的请求数据
    if (config.data) {
      console.log('[请求拦截器] 实际请求数据:', typeof config.data === 'string' ? JSON.parse(config.data) : config.data)
    }
    
    return config
  },
  (error) => {
    console.error('[请求拦截器] 请求错误:', error)
    return Promise.reject(error)
  }
)

// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse<any>) => {
    // 从响应头中获取新的CSRF token
    const newCsrfToken = response.headers['x-csrf-token']
    if (newCsrfToken) {
      console.log('[响应拦截器] 从响应头获取到新的CSRF Token:', newCsrfToken)
      localStorage.setItem('csrf_token', newCsrfToken)
    }

    // 记录响应信息
    console.log('[响应拦截器] 原始响应:', {
      url: response.config.url,
      status: response.status,
      headers: response.headers,
      data: response.data
    })

    // 首先检查 HTTP 状态码
    if (response.status !== 200) {
      message.error(t('common.error.network'))
      return Promise.reject(new Error('HTTP 状态码错误: ' + response.status))
    }
    
    // 如果是二进制数据，直接返回
    if (response.request.responseType === 'blob' || response.request.responseType === 'arraybuffer') {
      return response
    }

    // 检查响应数据
    if (!response.data) {
      message.error(t('common.error.noData'))
      return Promise.reject(new Error('响应数据为空'))
    }

    // 未设置状态码则默认成功状态
    const { code = 200, msg = '' } = response.data

    // 统一错误处理函数
    const handleError = (errorMsg: string, defaultMsg: string, needLogout = false) => {
      if (needLogout) {
        const userStore = useUserStore()
        userStore.logout()
      }
      message.error(msg || t(errorMsg))
      return Promise.reject(new Error(msg || defaultMsg))
    }

    // 处理业务状态码
    switch (code) {
      // 成功状态
      case 200:
        return response.data
      // 未登录或token过期
      case 401:
        return handleError('common.error.unauthorized', 'Unauthorized', true)
      // 权限不足
      case 403:
        return handleError('common.error.forbidden', 'Forbidden')
      // 资源不存在
      case 404:
        return handleError('common.error.notFound', 'Not Found')
      // 请求参数错误
      case 400:
        return handleError('common.error.badRequest', 'Bad Request')
      // 服务器错误
      case 500:
        return handleError('common.error.serverError', 'Server Error')
      // 服务不可用
      case 503:
        return handleError('common.error.serviceUnavailable', 'Service Unavailable')
      // 网关错误
      case 502:
        return handleError('common.error.badGateway', 'Bad Gateway')
      // 请求超时
      case 504:
        return handleError('common.error.gatewayTimeout', 'Gateway Timeout')
      // 业务错误（非标准HTTP状态码）
      case 1001: // 验证码错误
        return handleError('common.error.captchaError', 'Captcha Error')
      case 1002: // 用户名或密码错误
        return handleError('common.error.loginError', 'Login Error')
      case 1003: // 账号被锁定
        return handleError('common.error.accountLocked', 'Account Locked')
      case 1004: // 账号未激活
        return handleError('common.error.accountInactive', 'Account Inactive')
      case 2001: // 无效会话
        return handleError('common.error.invalidSession', 'Invalid Session', true)
      case 2002: // 会话过期
        return handleError('common.error.sessionExpired', 'Session Expired', true)
      case 2003: // 会话被踢出
        return handleError('common.error.sessionKicked', 'Session Kicked', true)
      case 2004: // 并发登录限制
        return handleError('common.error.concurrentLogin', 'Concurrent Login', true)
      // 其他错误情况
      default:
        if (code !== 200) {
          return handleError('common.error.unknown', 'Unknown Error')
        }
        return response.data
    }
  },
  (error) => {
    console.error('[响应拦截器] 请求错误:', error)
    
    // 处理网络错误
    if (!error.response) {
      message.error(t('common.error.network'))
      return Promise.reject(error)
    }

    // 统一错误处理函数
    const handleError = (errorMsg: string, defaultMsg: string, needLogout = false) => {
      if (needLogout) {
        const userStore = useUserStore()
        userStore.logout()
      }
      message.error(error.response?.data?.msg || t(errorMsg))
      return Promise.reject(new Error(error.response?.data?.msg || defaultMsg))
    }

    // 处理 HTTP 错误
    switch (error.response.status) {
      // 客户端错误 4xx
      case 400: return handleError('common.error.badRequest', 'Bad Request')
      case 401: return handleError('common.error.unauthorized', 'Unauthorized', true)
      case 402: return handleError('common.error.paymentRequired', 'Payment Required')
      case 403: return handleError('common.error.forbidden', 'Forbidden')
      case 404: return handleError('common.error.notFound', 'Not Found')
      case 405: return handleError('common.error.methodNotAllowed', 'Method Not Allowed')
      case 406: return handleError('common.error.notAcceptable', 'Not Acceptable')
      case 407: return handleError('common.error.proxyAuthRequired', 'Proxy Authentication Required')
      case 408: return handleError('common.error.requestTimeout', 'Request Timeout')
      case 409: return handleError('common.error.conflict', 'Conflict')
      case 410: return handleError('common.error.gone', 'Gone')
      case 411: return handleError('common.error.lengthRequired', 'Length Required')
      case 412: return handleError('common.error.preconditionFailed', 'Precondition Failed')
      case 413: return handleError('common.error.requestTooLarge', 'Request Entity Too Large')
      case 414: return handleError('common.error.uriTooLong', 'Request-URI Too Long')
      case 415: return handleError('common.error.unsupportedMedia', 'Unsupported Media Type')
      case 416: return handleError('common.error.rangeNotSatisfiable', 'Requested Range Not Satisfiable')
      case 417: return handleError('common.error.expectationFailed', 'Expectation Failed')
      case 429: return handleError('common.error.tooManyRequests', 'Too Many Requests')
      // 服务器错误 5xx
      case 500: return handleError('common.error.serverError', 'Internal Server Error')
      case 501: return handleError('common.error.notImplemented', 'Not Implemented')
      case 502: return handleError('common.error.badGateway', 'Bad Gateway')
      case 503: return handleError('common.error.serviceUnavailable', 'Service Unavailable')
      case 504: return handleError('common.error.gatewayTimeout', 'Gateway Timeout')
      case 505: return handleError('common.error.httpVersionNotSupported', 'HTTP Version Not Supported')
      // 其他错误
      default:
        if (error.response.status >= 400 && error.response.status < 500) {
          return handleError('common.error.clientError', 'Client Error')
        } else if (error.response.status >= 500 && error.response.status < 600) {
          return handleError('common.error.serverError', 'Server Error')
        }
        return handleError('common.error.unknown', 'Unknown Error')
    }
  }
)

// 扩展下载方法
service.download = (url: string, params?: any) => {
  return (service as AxiosInstance)({
    url,
    method: 'get',
    params,
    responseType: 'blob'
  }).then((response: AxiosResponse) => {
    const blob = new Blob([response.data])
    const filename = params?.filename || 'download.xlsx'
    if ('download' in document.createElement('a')) {
      const elink = document.createElement('a')
      elink.download = filename
      elink.style.display = 'none'
      elink.href = URL.createObjectURL(blob)
      document.body.appendChild(elink)
      elink.click()
      URL.revokeObjectURL(elink.href)
      document.body.removeChild(elink)
    } else {
      const URL = window.URL || window.webkitURL
      const downloadUrl = URL.createObjectURL(blob)
      window.location.href = downloadUrl
    }
  }).catch((error: Error) => {
    console.error(error)
  })
}

export default service 