import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'
import { getToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'

// 创建axios实例
const service: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  timeout: 10000
})

// 请求拦截器
service.interceptors.request.use(
  (config) => {
    // 在请求发送之前做一些处理
    const token = getToken()
    if (token && config.headers) {
      // 让每个请求携带token
      config.headers['Authorization'] = `Bearer ${token}`
    }
    return config
  },
  error => {
    // 发送失败
    console.error('[Request] 请求错误:', error)
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
    console.log(`[Response] 原始数据:`, res)
    
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
    
    return Promise.reject(new Error(normalizedData.msg || '请求失败'))
  },
  error => {
    // 处理HTTP错误
    if (error.response) {
      const { status, data } = error.response
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
      console.error('[Response] 网络错误:', error.message)
    } else {
      // 请求配置出错
      console.error('[Response] 请求配置错误:', error.message)
    }
    
    return Promise.reject(error)
  }
)

export default service