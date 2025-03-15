import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'
import { message } from 'ant-design-vue'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { useI18n } from 'vue-i18n'
import i18n from '@/locales'
import { getDeviceInfo } from '@/utils/device'

const { t } = i18n.global

interface CustomAxiosRequestConfig extends AxiosRequestConfig {
  retries?: number
  retryDelay?: number
  retryCount?: number
}

// 创建axios实例
const service: AxiosInstance & { download?: Function } = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 100000000000000,
  withCredentials: true  // 允许跨域请求时携带cookie
}) as AxiosInstance & { download?: Function }

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
    
    // 2. 如果cookie中没有，尝试通过GET请求获取新token
    console.log('[CSRF] 未找到Token，尝试获取新Token')
    try {
      await axios.get('/api/csrf-token', { withCredentials: true })
      // 再次尝试从cookie获取
      const newToken = getCookie('XSRF-TOKEN')
      if (newToken) {
        console.log('[CSRF] 成功获取新Token:', newToken)
        return newToken
      }
    } catch (error) {
      console.error('[CSRF] 获取新Token失败:', error)
    }
    
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
      config.headers['X-CSRF-Token'] = decodedToken
      console.log('[CSRF] Token已设置到请求头:', decodedToken)
    } else {
      console.log('[CSRF] 未找到Token，跳过设置')
      
      // 如果是GET请求，可能需要先获取Token
      if (config.method?.toLowerCase() === 'get') {
        console.log('[CSRF] GET请求，尝试从服务器获取新Token')
      }
    }

    // 每次请求都重新获取token
    const token = getToken();
    //console.log('[请求拦截器] 开始处理请求:', config.url)
    
    // 过滤请求参数中的空值
    if (config.params) {
      config.params = Object.fromEntries(
        Object.entries(config.params).filter(([_, value]) => {
          return value !== '' && value !== null && value !== undefined
        })
      )
    }
    
    if (config.data && typeof config.data === 'object' && !(config.data instanceof FormData)) {
      config.data = Object.fromEntries(
        Object.entries(config.data).filter(([_, value]) => {
          return value !== '' && value !== null && value !== undefined
        })
      )
    }
    
    // 如果是登录请求，从请求体中获取租户ID
    if (config.url?.toLowerCase().includes('/auth/login')) {
      const loginData = typeof config.data === 'string' ? JSON.parse(config.data) : config.data;
      if (loginData?.tenantId !== undefined) {
        config.headers['X-Tenant-Id'] = loginData.tenantId.toString();
      }

      // 如果没有设备信息，添加默认的设备信息
      if (!loginData.deviceInfo) {
        loginData.deviceInfo = getDeviceInfo();
        loginData.loginSource = 0; // Web端
        config.data = JSON.stringify(loginData);
      }
    } else if (token) {
      // 添加Bearer前缀
      config.headers['Authorization'] = `Bearer ${token}`
      //console.log('[请求拦截器] Token:', typeof token === 'string' ? token.substring(0, 10) + '...' : token)
      
      // 从JWT令牌中获取租户ID
      try {
        const tokenParts = token.split('.');
        if (tokenParts.length === 3) {
          const payload = JSON.parse(atob(tokenParts[1]));
          //console.log('[请求拦截器] JWT Payload:', {
          //  ...payload,
          //  sub: payload.sub ? '******' : undefined,
          //  jti: payload.jti ? '******' : undefined
          //})
          if (payload.tenant_id !== undefined) {
            config.headers['X-Tenant-Id'] = payload.tenant_id.toString();
            //console.log('[请求拦截器] 从JWT设置租户ID:', payload.tenant_id)
          } else {
            //console.warn('[请求拦截器] JWT中未找到租户ID')
          }
        }
      } catch (error) {
        //console.error('[请求拦截器] JWT解析失败:', error);
      }
    }
    
    // 确保headers对象存在
    config.headers = config.headers || {}
    
    // 设置通用headers
    config.headers['Accept'] = 'application/json'
    config.headers['Content-Type'] = 'application/json'
    
    // 详细记录请求信息
    console.log('[请求拦截器] 最终请求配置:', {
      url: config.url,
      method: config.method,
      params: config.params,
      data: typeof config.data === 'string' ? '(字符串数据)' : config.data,
      headers: {
        ...config.headers,
        Authorization: config.headers.Authorization ? '(已设置)' : '(未设置)',
        'X-Tenant-Id': config.headers['X-Tenant-Id'] || '(未设置)'
      }
    })
    
    return config
  },
  (error) => {
    console.error('[请求拦截器] 请求错误:', error)
    return Promise.reject(error)
  }
)

// 响应拦截器
service.interceptors.response.use(
  (response) => {
    // 从响应头中获取新的CSRF token
    const newCsrfToken = response.headers['x-csrf-token']
    if (newCsrfToken) {
      console.log('从响应头获取到新的CSRF Token:', newCsrfToken)
      localStorage.setItem('csrf_token', newCsrfToken)
    }
    
    const res = response.data
    const { code, msg } = response.data
    console.log('[Response Interceptor] 收到响应:', {
      url: response.config.url,
      status: response.status,
      res: res,
      code: code,
      message: msg
    })

    // 如果是二进制数据，直接返回
    if (response.request.responseType === 'blob' || response.request.responseType === 'arraybuffer') {
      return response.data
    }

    // 直接返回响应数据
    return res
  },
  (error) => {
    // 如果是CSRF错误，清除本地存储的token
    if (error.response?.status === 403 && error.response?.data?.message?.includes('CSRF')) {
      console.log('CSRF验证失败，清除本地存储的token')
      localStorage.removeItem('csrf_token')
    }
    
    console.error('[Response Interceptor] 响应错误:', error.message)
    message.error(error.message || '请求失败')
    return Promise.reject(error)
  }
)

// 扩展下载方法
service.download = (url: string, params?: any) => {
  return service({
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
  }).catch((r) => {
    console.error(r)
  })
}

// 扩展请求方法
interface ExtendedAxiosInstance extends AxiosInstance {
  download: (url: string, params?: any) => Promise<void>
  get<T = any>(url: string, config?: AxiosRequestConfig): Promise<T>
  delete<T = any>(url: string, config?: AxiosRequestConfig): Promise<T>
  head<T = any>(url: string, config?: AxiosRequestConfig): Promise<T>
  options<T = any>(url: string, config?: AxiosRequestConfig): Promise<T>
  post<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T>
  put<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T>
  patch<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T>
}

export default service as ExtendedAxiosInstance 