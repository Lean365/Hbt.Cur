import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from './user'

export const useWebSocketStore = defineStore('websocket', () => {
  const router = useRouter()
  const userStore = useUserStore()
  
  const ws = ref<WebSocket | null>(null)
  const connected = ref(false)
  const error = ref<string | null>(null)
  const reconnectAttempts = ref(0)
  const maxReconnectAttempts = 5
  const reconnectTimeout = ref<number | null>(null)
  
  // 连接 WebSocket
  const connect = () => {
    try {
      // 清除之前的连接
      disconnect()
      
      // 创建新的 WebSocket 连接
      const protocol = window.location.protocol === 'https:' ? 'wss:' : 'ws:'
      const host = window.location.hostname
      const port = '5349' // 固定使用前端服务端口
      ws.value = new WebSocket(`${protocol}//${host}:${port}/__vite_hmr`)
      
      // 连接成功
      ws.value.onopen = () => {
        console.log('WebSocket 连接成功')
        connected.value = true
        error.value = null
        reconnectAttempts.value = 0
        
        // 发送初始化消息
        if (ws.value) {
          ws.value.send(JSON.stringify({
            type: 'connection-check',
            timestamp: new Date().toISOString()
          }))
        }
      }
      
      // 连接关闭
      ws.value.onclose = (event) => {
        console.log('WebSocket 连接关闭:', event.code, event.reason)
        connected.value = false
        // 只有在非正常关闭时才重连
        if (event.code !== 1000 && event.code !== 1001) {
          handleReconnect()
        }
      }
      
      // 连接错误
      ws.value.onerror = (event) => {
        console.error('WebSocket 连接错误:', event)
        connected.value = false
        error.value = '前端服务连接失败，正在重试...'
        // 不立即重连，等待 onclose 事件处理
      }
      
      // 接收消息
      ws.value.onmessage = (event) => {
        try {
          const data = JSON.parse(event.data)
          // 如果是 Vite HMR 消息，忽略它
          if (data.type && data.type.startsWith('vite')) {
            return
          }
          handleMessage(data)
        } catch (e) {
          console.error('解析 WebSocket 消息失败:', e)
        }
      }
    } catch (e) {
      console.error('创建 WebSocket 连接失败:', e)
      error.value = '前端服务连接失败，正在重试...'
      handleReconnect()
    }
  }
  
  // 断开连接
  const disconnect = () => {
    if (ws.value) {
      try {
        ws.value.close(1000, '正常关闭')
      } catch (e) {
        console.error('关闭 WebSocket 连接失败:', e)
      }
      ws.value = null
    }
    connected.value = false
    if (reconnectTimeout.value) {
      clearTimeout(reconnectTimeout.value)
      reconnectTimeout.value = null
    }
  }
  
  // 处理重连
  const handleReconnect = () => {
    if (reconnectAttempts.value >= maxReconnectAttempts) {
      console.log('WebSocket 重连次数超过最大限制，退出登录')
      handleMaxReconnectAttempts()
      return
    }
    
    // 使用指数退避算法计算重连延迟
    const delay = Math.min(1000 * Math.pow(2, reconnectAttempts.value), 30000)
    reconnectTimeout.value = window.setTimeout(() => {
      reconnectAttempts.value++
      connect()
    }, delay)
  }
  
  // 处理超过最大重连次数
  const handleMaxReconnectAttempts = () => {
    // 清除用户登录状态
    userStore.logout()
    // 重定向到登录页
    router.push('/login')
  }
  
  // 处理接收到的消息
  const handleMessage = (data: any) => {
    // TODO: 根据消息类型处理不同的业务逻辑
    console.log('收到 WebSocket 消息:', data)
  }
  
  // 发送消息
  const send = (data: any) => {
    if (ws.value && connected.value) {
      ws.value.send(JSON.stringify(data))
    } else {
      console.warn('WebSocket 未连接，无法发送消息')
    }
  }
  
  return {
    connected,
    error,
    connect,
    disconnect,
    send
  }
}) 