import axios, { AxiosHeaders } from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, AxiosRequestHeaders } from 'axios'
import { message } from 'ant-design-vue'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useI18n } from 'vue-i18n'
import i18n from '@/locales'
import type { HbtApiResponse } from '@/types/common'
import { logout } from '@/api/identity/auth'

// 获取 CSRF Token
const getCsrfToken = () => {
  const match = document.cookie.match(/XSRF-TOKEN=([^;]+)/)
  if (match) {
    console.log('[CSRF] 从Cookie中获取到Token:', match[1])
    return decodeURIComponent(match[1])
  }
  return null
}

// 设置 CSRF Token
const setCsrfToken = (token: string) => {
  // 设置Cookie时确保SameSite为None，这对于跨域请求是必要的
  document.cookie = `XSRF-TOKEN=${token}; path=/; secure; samesite=none`
  console.log('[CSRF] Token已设置到Cookie:', token)
}

// 移除 CSRF Token
const removeCsrfToken = () => {
  document.cookie = 'XSRF-TOKEN=; path=/; expires=Thu, 01 Jan 1970 00:00:01 GMT'
}

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

const { t } = i18n.global

// 创建 axios 实例
const service = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 10000,
  withCredentials: true  // 添加此配置以支持跨域请求时携带Cookie
}) as ExtendedAxiosInstance

// 统一的请求头处理方法
export const createRequestHeaders = (additionalHeaders?: Record<string, string>): AxiosRequestHeaders => {
  const token = getToken()
  const headers: AxiosRequestHeaders = new AxiosHeaders()
  
  if (token) {
    try {
      // 解析token查看内容
      const [header, payload, signature] = token.split('.')
      // 使用 URL 安全的 base64 解码
      const decodedPayload = JSON.parse(decodeURIComponent(atob(payload.replace(/-/g, '+').replace(/_/g, '/')).split('').map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join('')))
      console.log('[Token分析]', {
        header: atob(header.replace(/-/g, '+').replace(/_/g, '/')),
        payloadSize: payload.length,
        payload: decodedPayload,
        signatureSize: signature.length
      })
    } catch (e) {
      console.error('[Token解析失败]', e)
    }
    headers.set('Authorization', `Bearer ${token}`)
  }
  
  // 保留所有必要的请求头
  if (additionalHeaders) {
    Object.entries(additionalHeaders).forEach(([key, value]) => {
      headers.set(key, value)
    })
  }
  
  return headers
}

// 请求拦截器
service.interceptors.request.use(
  async (config) => {
    // 记录请求开始
    console.log('[Request] 开始处理请求:', config.url)

    // 如果是登出接口，不需要CSRF Token
    if (config.url?.includes('/HbtAuth/logout')) {
      console.log('[Request] 登出接口，跳过CSRF Token检查')
      return config
    }

    // 创建新的headers对象
    const headers = new AxiosHeaders()
    
    // 设置基本请求头
    headers.set('Accept', 'application/json, text/plain, */*')
    
    // 根据请求方法设置Content-Type
    if (config.method?.toLowerCase() === 'post' || config.method?.toLowerCase() === 'put') {
      headers.set('Content-Type', 'application/json')
      
      // 确保请求体是JSON字符串
      if (config.data && typeof config.data === 'object') {
        config.data = JSON.stringify(config.data)
      }
    }

    // 获取并设置认证Token
    const token = getToken()
    if (token) {
      headers.set('Authorization', `Bearer ${token}`)
      console.log('[Request] 已设置认证Token到请求头')
    }

    // 获取CSRF Token
    const csrfToken = getCsrfToken()
    if (csrfToken) {
      // 不再对Token进行格式转换，保持原始格式
      headers.set('X-CSRF-Token', csrfToken)
      console.log('[Request] 已设置CSRF Token到请求头:', csrfToken)
    } else {
      console.warn('[Request] 未能获取CSRF Token')
    }

    // 保留原始请求头中的其他字段
    if (config.headers) {
      Object.entries(config.headers).forEach(([key, value]) => {
        if (!headers.has(key)) {
          headers.set(key, value)
        }
      })
    }

    config.headers = headers
    console.log('[Request] 最终请求头:', config.headers)
    return config
  },
  (error) => {
    console.error('[Request] 请求拦截器错误:', error)
    return Promise.reject(error)
  }
)

// 响应拦截器
service.interceptors.response.use(
  async (response: AxiosResponse) => {
    // 从响应头中获取 CSRF Token
    const csrfToken = response.headers['x-csrf-token']
    if (csrfToken) {
      console.log('[Response] 收到新的CSRF Token:', csrfToken)
      setCsrfToken(csrfToken)
    }

    return response.data
  },
  async (error) => {
    console.error('响应错误:', error)
    
    if (error.response) {
      // 处理CSRF Token验证失败的情况
      if (error.response.status === 403 && error.response.data?.message?.includes('CSRF')) {
        console.log('[Response] CSRF Token验证失败，尝试重新获取Token')
        
        // 获取新的CSRF Token
        const csrfToken = error.response.headers['x-csrf-token']
        if (csrfToken) {
          console.log('[Response] 获取到新的CSRF Token:', csrfToken)
          setCsrfToken(csrfToken)
          
          // 重试原始请求
          const config = error.config
          if (config && !config.retryCount) {
            config.retryCount = 1
            return service(config)
          }
        }
        
        // 如果重试失败或无法获取新Token，才执行登出
        console.log('[Response] CSRF Token重试失败，执行登出')
        const userStore = useUserStore()
        removeToken()
        removeCsrfToken()
        userStore.logout()
        message.error('登录已过期，请重新登录')
        return Promise.reject(error)
      }

      switch (error.response.status) {
        case 401:
          // 未授权，清除token并跳转到登录页
          const userStore = useUserStore()
          removeToken()
          removeCsrfToken()
          userStore.logout()
          message.error('登录已过期，请重新登录')
          break
        case 403:
          message.error('权限不足，请联系管理员')
          break
        case 404:
          message.error('请求的资源不存在')
          break
        case 500:
          message.error('服务器错误，请稍后重试')
          break
        default:
          message.error(error.response.data?.message || '请求失败')
      }
    } else if (error.request) {
      message.error('网络错误，请检查网络连接')
    } else {
      message.error(error.message || '请求配置错误')
    }
    
    return Promise.reject(error)
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

// 心跳请求
export const sendHeartbeat = async () => {
  try {
    await service.post('/api/HbtOnlineUser/heartbeat')
    return true
  } catch (error) {
    console.error('[心跳] 发送失败:', error)
    return false
  }
}

// SignalR 访问令牌工厂
export const createSignalRAccessToken = () => {
  const token = getToken()
  if (!token) {
    throw new Error('未找到有效的访问令牌')
  }
  return token
}

export default service 