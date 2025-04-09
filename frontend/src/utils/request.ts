import axios, { AxiosHeaders } from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, AxiosRequestHeaders } from 'axios'
import { message } from 'ant-design-vue'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useI18n } from 'vue-i18n'
import i18n from '@/locales'
import type { HbtApiResponse } from '@/types/common'
import { logout } from '@/api/identity/auth'

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
  timeout: 10000
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
  config => {
    const originalHeaders = config.headers
    console.log('[原始请求头]', originalHeaders)
    
    config.headers = createRequestHeaders(config.headers as Record<string, string>)
    
    // 计算请求头大小
    const headersStr = JSON.stringify(config.headers)
    const headersSize = new Blob([headersStr]).size
    console.log('[请求头大小]', headersSize, 'bytes')
    
    // 如果请求头过大，清除token并重新登录
    if (headersSize > 8000) {  // 8KB 限制
      console.error('[错误] 请求头过大，需要重新登录')
      message.error('登录信息过期，请重新登录')
      removeToken()
      window.location.href = '/login'
      return Promise.reject(new Error('请求头过大'))
    }
    
    console.log('[最终请求头]', config.headers)
    return config
  },
  error => {
    console.error('[请求拦截器] 请求错误:', error)
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

// 响应拦截器
service.interceptors.response.use(
  response => {
    return response.data
  },
  error => {
    console.error('[响应拦截器] 请求错误:', error)
    
    if (error.response) {
      switch (error.response.status) {
        case 401:
          // 未授权，清除token并跳转到登录页
          removeToken()
          window.location.href = '/login'
          break
        case 403:
          // 权限不足
          message.error('权限不足，请联系管理员')
          break
        case 404:
          // 资源不存在
          message.error('请求的资源不存在')
          break
        case 500:
          // 服务器错误
          message.error('服务器错误，请稍后重试')
          break
        default:
          message.error(error.response.data?.msg || '请求失败')
      }
    } else if (error.request) {
      // 请求已发出但没有收到响应
      message.error('网络错误，请检查网络连接')
    } else {
      // 请求配置出错
      message.error('请求配置错误')
    }
    
    return Promise.reject(error)
  }
)

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