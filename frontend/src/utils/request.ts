import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'
import { getToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'

// 创建axios实例
const service: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,  // 使用环境变量中的 baseURL
  timeout: 30000, // 增加超时时间
  withCredentials: true, // 允许跨域请求携带 cookie
  maxContentLength: Infinity, // 允许上传大文件
  maxBodyLength: Infinity,
  headers: {
    'Accept': 'application/json, text/plain, */*'
  }
})

// 从 cookie 中获取 CSRF 令牌
export function getCsrfToken(): string | null {
  const cookies = document.cookie.split(';')
  for (const cookie of cookies) {
    const [name, value] = cookie.trim().split('=')
    if (name === 'XSRF-TOKEN') {
      // 对 Cookie 中的 Token 进行 URL 解码
      const decodedValue = decodeURIComponent(value)
      console.log('[CSRF] 从cookie中获取到完整令牌:', decodedValue)
      return decodedValue
    }
  }
  console.warn('[CSRF] 未在cookie中找到XSRF-TOKEN令牌')
  return null
}

// 请求拦截器
service.interceptors.request.use(
  (config) => {
    console.log('=== 请求拦截器开始 ===')
    console.log('请求配置:', {
      url: config.url,
      method: config.method,
      params: config.params,
      data: config.data,
      headers: config.headers
    })

    // 获取 CSRF Token
    const csrfToken = getCsrfToken()
    if (csrfToken) {
      console.log('[CSRF] 从cookie中获取到完整令牌:', csrfToken)
      config.headers['X-CSRF-TOKEN'] = csrfToken
      console.log('CSRF Token: 存在')
    }

    // 获取认证 Token
    const token = getToken()
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`
      console.log('Authorization Token: 存在')
    }

    // 处理文件上传
    if (config.data instanceof FormData) {
      console.log('[Request] 文件上传请求:', {
        url: config.url,
        method: config.method,
        headers: config.headers,
        data: config.data
      })

      // 验证 FormData 内容
      console.log('[Request] FormData 内容详情:')
      for (const [key, value] of config.data.entries()) {
        console.log(`- ${key}:`, value)
        if (value instanceof File) {
          console.log(`  文件详情:`, {
            name: value.name,
            size: value.size,
            type: value.type,
            lastModified: value.lastModified
          })
        }
      }

      // 设置文件上传超时时间
      config.timeout = 60000

      // 删除 Content-Type 头，让浏览器自动设置正确的 boundary
      delete config.headers['Content-Type']

      // 添加 X-Requested-With 头
      config.headers['X-Requested-With'] = 'XMLHttpRequest'
    }

    console.log('=== 请求拦截器结束 ===')
    return config
  },
  (error) => {
    console.error('请求拦截器错误:', error)
    return Promise.reject(error)
  }
)

/**
 * 将 PascalCase 或 snake_case 转换为 camelCase
 */
function toCamelCase(str: string): string {
  return str.replace(/[-_]([a-z])/g, (_, letter) => letter.toUpperCase())
    .replace(/^[A-Z]/, letter => letter.toLowerCase())
}

/**
 * 转换对象的键名为 camelCase
 */
function normalizeKeys(obj: any): any {
  if (obj === null || typeof obj !== 'object') {
    return obj
  }

  if (Array.isArray(obj)) {
    return obj.map(item => normalizeKeys(item))
  }

  return Object.keys(obj).reduce((acc, key) => {
    const camelKey = toCamelCase(key)
    acc[camelKey] = normalizeKeys(obj[key])
    return acc
  }, {} as any)
}

// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse) => {
    const { data: res } = response
    
    // 记录原始响应
    console.log(`[Response] 原始响应:`, {
      url: response.config.url,
      method: response.config.method,
      status: response.status,
      statusText: response.statusText,
      headers: response.headers,
      data: res
    })
    
    // 如果是文件上传响应，直接返回
    if (response.config.data instanceof FormData) {
      return response
    }
    
    // 如果是Blob类型，检查响应头
    if (response.config.responseType === 'blob') {
      // 检查响应头中的Content-Type
      const contentType = response.headers['content-type']
      if (contentType && contentType.includes('application/json')) {
        // 如果是JSON格式的错误响应，需要解析错误信息
        return new Promise((resolve, reject) => {
          const reader = new FileReader()
          reader.onload = () => {
            try {
              const errorData = JSON.parse(reader.result as string)
              reject(new Error(errorData.msg || '导出失败'))
            } catch (e) {
              reject(new Error('导出失败'))
            }
          }
          reader.onerror = () => reject(new Error('导出失败'))
          reader.readAsText(res)
        })
      }
      // 如果是Excel文件，直接返回整个response对象
      return response
    }
    
    // 转换为统一的小驼峰格式
    const normalizedData = normalizeKeys(res)
    console.log(`[Response] 转换后数据:`, normalizedData)
    
    // 处理业务状态码
    if (normalizedData.code === 200) {
      response.data = normalizedData
      return response
    }
    
    // 处理特定的业务状态码
    switch (normalizedData.code) {
      case 401: // 未授权
      case 403: // 禁止访问
        const userStore = useUserStore()
        userStore.logout()
        break
      case 500: // 服务器错误
        console.error('[Response] 服务器错误:', normalizedData.msg)
        break
    }
    
    return Promise.reject(normalizedData)
  },
  error => {
    // 处理HTTP错误
    if (error.response) {
      const { status, data, headers } = error.response
      console.error('[Response] HTTP错误:', {
        url: error.config.url,
        method: error.config.method,
        status,
        statusText: error.response.statusText,
        headers,
        data,
        config: error.config
      })
      
      // 如果是Blob类型的错误响应，需要解析错误信息
      if (error.config.responseType === 'blob' && data instanceof Blob) {
        return new Promise((resolve, reject) => {
          const reader = new FileReader()
          reader.onload = () => {
            try {
              const errorData = JSON.parse(reader.result as string)
              reject(new Error(errorData.msg || '导出失败'))
            } catch (e) {
              reject(new Error('导出失败'))
            }
          }
          reader.onerror = () => reject(new Error('导出失败'))
          reader.readAsText(data)
        })
      }
      
      switch (status) {
        case 401: // 未授权
          const userStore = useUserStore()
          userStore.logout()
          break
        case 403: // 禁止访问
          console.error('[Response] 禁止访问')
          break
        case 404: // 未找到
          console.error('[Response] 接口不存在')
          break
        case 500: // 服务器错误
          console.error('[Response] 服务器错误')
          break
        default:
          console.error(`[Response] HTTP错误 ${status}:`, data)
      }
    } else if (error.request) {
      // 请求已发出但没有收到响应
      console.error('[Response] 网络错误:', {
        url: error.config.url,
        method: error.config.method,
        message: error.message,
        request: error.request,
        config: error.config
      })
    } else {
      // 请求配置出错
      console.error('[Response] 请求配置错误:', {
        url: error.config.url,
        method: error.config.method,
        message: error.message,
        config: error.config
      })
    }
    
    return Promise.reject(error)
  }
)

export default service