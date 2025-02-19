import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'
import { message } from 'ant-design-vue'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'

// 创建axios实例
const service: AxiosInstance & { download?: Function } = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 10000
})

// 请求拦截器
service.interceptors.request.use(
  (config) => {
    // 每次请求都重新获取token
    const token = getToken()
    console.log('[Request Interceptor] Current Token:', token)
    
    // 确保headers对象存在
    config.headers = config.headers || {}
    
    // 设置通用headers
    config.headers['Accept'] = 'application/json'
    config.headers['Content-Type'] = 'application/json'
    
    // 如果是登录请求，从请求体中获取租户ID
    if (config.url?.includes('/auth/login')) {
      const loginData = typeof config.data === 'string' ? JSON.parse(config.data) : config.data;
      if (loginData?.tenantId !== undefined) {
        config.headers['X-Tenant-Id'] = loginData.tenantId.toString();
        console.log('[Request Interceptor] 设置租户ID:', loginData.tenantId);
      }

      // 如果没有设备信息，添加默认的设备信息
      if (!loginData.deviceInfo) {
        loginData.deviceInfo = {
          deviceId: crypto.randomUUID(),
          deviceType: 0, // PC
          deviceName: navigator.platform,
          deviceModel: navigator.userAgent,
          osType: 0, // Windows
          osVersion: navigator.platform,
          browserType: 0, // Chrome
          browserVersion: navigator.appVersion,
          resolution: `${window.screen.width}x${window.screen.height}`,
          ipAddress: '',
          location: ''
        }
        loginData.loginSource = 0; // Web端
        config.data = JSON.stringify(loginData);
      }
    } else if (token) {
      // 从JWT令牌中获取租户ID
      try {
        const tokenParts = token.split('.');
        const payload = JSON.parse(atob(tokenParts[1]));
        if (payload.tenant_id) {
          config.headers['X-Tenant-Id'] = payload.tenant_id;
          console.log('[Request Interceptor] 从JWT设置租户ID:', payload.tenant_id);
        }
      } catch (error) {
        console.error('[Request Interceptor] JWT解析失败:', error);
      }
    }
    
    if (token) {
      // 添加Bearer前缀
      config.headers['Authorization'] = `Bearer ${token}`
      // 详细记录请求信息
      console.log('[Request Interceptor] 详细请求信息:', {
        url: config.url,
        method: config.method,
        headers: {
          ...config.headers,
          Authorization: config.headers.Authorization
        },
        baseURL: config.baseURL,
        data: config.data
      })

      // 解析JWT令牌
      try {
        const tokenParts = token.split('.');
        const payload = JSON.parse(atob(tokenParts[1]));
        console.log('[Request Interceptor] JWT解析结果:', {
          exp: new Date(payload.exp * 1000).toLocaleString(),
          nbf: new Date(payload.nbf * 1000).toLocaleString(),
          sub: payload.sub,
          name: payload.name,
          tenant_id: payload.tenant_id
        });
      } catch (error) {
        console.error('[Request Interceptor] JWT解析失败:', error);
      }
    } else {
      console.log('[Request Interceptor] No token found for request:', {
        url: config.url,
        method: config.method
      })
    }
    
    return config
  },
  (error) => {
    if (error.code === 'ERR_NETWORK' || error.code === 'ECONNABORTED') {
      console.warn('net::ERR_CONNECTION_REFUSED')
    }
    console.warn('[Request Interceptor] Error:', error)
    return Promise.reject(error)
  }
)

// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse) => {
    const res = response.data
    console.log('[Response Interceptor] Response:', {
      url: response.config.url,
      method: response.config.method,
      status: response.status,
      headers: response.headers,
      data: res
    })

    // 二进制数据直接返回
    if (response.request.responseType === 'blob' || response.request.responseType === 'arraybuffer') {
      return response.data
    }

    // 如果没有code字段，说明是非标准响应，直接返回
    if (typeof res.code === 'undefined') {
      return res
    }

    // 业务成功
    if (res.code === 200) {
      return res
    }

    // 业务失败
    message.error(res.msg || '系统错误')
    return Promise.reject(res)
  },
  (error) => {
    if (error.code === 'ERR_NETWORK' || error.code === 'ECONNABORTED') {
      // 在控制台显示错误
      console.log('\n\x1b[31m%s\x1b[0m', '  ❌ 后端服务未启动，请先启动后端服务\n');
    }
    
    message.error('服务器连接失败，请检查网络连接')
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