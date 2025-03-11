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
  timeout: 100000000000000
}) as AxiosInstance & { download?: Function }

// 设置默认重试配置
const defaultRetryConfig = {
  retries: 3,
  retryDelay: 1000
}

// 请求拦截器
service.interceptors.request.use(
  (config) => {
    // 每次请求都重新获取token
    const token = getToken()
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
    if (config.url?.includes('/auth/login')) {
      const loginData = typeof config.data === 'string' ? JSON.parse(config.data) : config.data;
      //console.log('[请求拦截器] 登录数据:', {
      //  ...loginData,
      //  password: loginData.password ? '******' : undefined
      //})
      if (loginData?.tenantId !== undefined) {
        config.headers['X-Tenant-Id'] = loginData.tenantId.toString();
        //console.log('[请求拦截器] 从登录数据设置租户ID:', loginData.tenantId)
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
    //console.log('[请求拦截器] 最终请求配置:', {
    //  url: config.url,
    //  method: config.method,
    //  params: config.params,
    //  data: typeof config.data === 'string' ? '(字符串数据)' : config.data,
    //  headers: {
    //    ...config.headers,
    //    Authorization: config.headers.Authorization ? '(已设置)' : '(未设置)',
    //    'X-Tenant-Id': config.headers['X-Tenant-Id'] || '(未设置)'
    //  }
    //})
    
    return config
  },
  (error) => {
    //console.error('[请求拦截器] 请求错误:', error)
    return Promise.reject(error)
  }
)

// 响应拦截器
service.interceptors.response.use(
  (response) => {
    const res = response.data
    console.log('[Response Interceptor] 收到响应:', {
      url: response.config.url,
      status: response.status,
      data: res
    })

    // 如果是二进制数据，直接返回
    if (response.request.responseType === 'blob' || response.request.responseType === 'arraybuffer') {
      return response.data
    }

    // 直接返回响应数据
    return res
  },
  (error) => {
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