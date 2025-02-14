import axios from 'axios'
import type { AxiosInstance, InternalAxiosRequestConfig, AxiosResponse } from 'axios'
import { message } from 'ant-design-vue'
import { useUserStore } from '@/stores/user'
import { useRouter } from 'vue-router'
import type { HbtApiResult } from '@/types/api'

// 创建axios实例
const service: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  }
})

// 请求拦截器
service.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    // 从 localStorage 获取 token
    const token = localStorage.getItem('token')
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`
    }
    return config
  },
  (error) => {
    console.error('Request error:', error)
    return Promise.reject(error)
  }
)

// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse<HbtApiResult<any>>) => {
    const res = response.data

    // 如果响应中包含错误信息
    if (res.code !== 200) {
      message.error(res.msg || '请求失败')
      return Promise.reject(new Error(res.msg || '请求失败'))
    }
    // 返回数据部分
    return res.data
  },
  (error) => {
    if (error.response) {
      const { status, data } = error.response
      
      // 对于429状态码，直接返回错误对象
      if (status === 429) {
        return Promise.reject(error)
      }

      switch (status) {
        case 401:
          message.error(data?.msg || '用户名或密码错误')
          break
        case 403:
          message.error(data?.msg || '没有权限访问')
          break
        case 404:
          message.error(data?.msg || '请求的资源不存在')
          break
        case 500:
          message.error(data?.msg || '服务器错误')
          break
        default:
          message.error(data?.msg || `请求失败: ${status}`)
      }
    } else if (error.request) {
      message.error('网络错误，请检查您的网络连接')
    } else {
      message.error('请求配置错误')
    }
    return Promise.reject(error)
  }
)

export default service 