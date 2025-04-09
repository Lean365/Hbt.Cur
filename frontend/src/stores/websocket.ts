import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useWebSocketStore = defineStore('websocket', () => {
  const ws = ref<WebSocket | null>(null)
  const connected = ref(false)
  const error = ref<string | null>(null)
  const reconnectAttempts = ref(0)
  const maxReconnectAttempts = 5
  const reconnectTimeout = ref<number | null>(null)
  const lastErrorTime = ref<number>(0)
  const errorDisplayInterval = 120000 // 错误提示最小间隔时间（2分钟）
  
  // 获取WebSocket配置
  const getWebSocketConfig = () => {
    const protocol = window.location.protocol === 'https:' ? 'wss:' : 'ws:'
    const host = window.location.hostname
    
    // 根据环境设置不同的端口和路径
    let port: string
    let path: string
    
    if (import.meta.env.DEV) {
      // 开发环境 - 使用Vite的WebSocket服务
      const viteClientPort = '5349'  // Vite默认端口
      const hmrPort = new URL(import.meta.env.VITE_DEV_SERVER_URL || '').port || viteClientPort
      port = hmrPort
      path = ''  // Vite的HMR会自动处理路径
      console.log('当前环境: 开发环境')
    } else if (import.meta.env.MODE === 'test') {
      // 测试环境
      port = import.meta.env.VITE_TEST_PORT || '5350'
      path = '/ws'
      console.log('当前环境: 测试环境')
    } else {
      // 生产环境
      port = import.meta.env.VITE_PROD_PORT || '5000'
      path = '/ws'
      console.log('当前环境: 生产环境')
    }
    
    return {
      url: `${protocol}//${host}:${port}${path}`,
      heartbeatInterval: import.meta.env.DEV ? 30000 : 60000 // 开发环境30秒，生产环境60秒
    }
  }
  
  // 连接 WebSocket
  const connect = () => {
    // 如果是开发环境，不需要手动建立WebSocket连接
    if (import.meta.env.DEV) {
      console.log('开发环境下，Vite会自动处理HMR连接')
      connected.value = true
      error.value = null
      return
    }

    try {
      // 清除之前的连接
      disconnect()
      
      // 获取配置
      const config = getWebSocketConfig()
      console.log('WebSocket连接配置:', config)
      
      // 创建新的 WebSocket 连接
      ws.value = new WebSocket(config.url)
      
      // 连接成功
      ws.value.onopen = () => {
        console.log('服务连接成功')
        connected.value = true
        error.value = null
        reconnectAttempts.value = 0
      }
      
      // 连接关闭
      ws.value.onclose = (event) => {
        console.log('服务连接关闭:', event.code, event.reason)
        connected.value = false
        
        // 只有在非正常关闭时才重连和显示错误
        if (event.code !== 1000 && event.code !== 1001) {
          handleReconnect()
        }
      }
      
      // 连接错误
      ws.value.onerror = (event) => {
        console.log('服务连接错误:', event)
        connected.value = false
        
        // 检查是否应该显示错误提示
        const now = Date.now()
        if (now - lastErrorTime.value > errorDisplayInterval) {
          error.value = '服务连接中断，正在重试...'
          lastErrorTime.value = now
        }
        
        handleReconnect()
      }
      
      // 接收消息
      ws.value.onmessage = (event) => {
        try {
          const data = JSON.parse(event.data)
          // 处理服务状态消息
          if (data.type === 'status') {
            connected.value = data.status === 'ok'
          }
        } catch (e) {
          console.log('解析消息失败:', e)
        }
      }
      
      // 定期发送心跳
      const heartbeat = setInterval(() => {
        if (ws.value && ws.value.readyState === WebSocket.OPEN) {
          ws.value.send(JSON.stringify({ type: 'ping' }))
        }
      }, config.heartbeatInterval)
      
      // 在组件卸载时清理心跳
      return () => {
        clearInterval(heartbeat)
      }
      
    } catch (e) {
      console.log('创建连接失败:', e)
      handleReconnect()
    }
  }
  
  // 断开连接
  const disconnect = () => {
    if (ws.value) {
      ws.value.close()
      ws.value = null
    }
    connected.value = false
    error.value = null
    
    if (reconnectTimeout.value) {
      clearTimeout(reconnectTimeout.value)
      reconnectTimeout.value = null
    }
  }
  
  // 处理重连
  const handleReconnect = () => {
    if (reconnectAttempts.value >= maxReconnectAttempts) {
      console.log('重连次数超过最大限制')
      error.value = '服务连接失败，请刷新页面重试'
      return
    }
    
    // 使用递增的重连延迟，最小3秒，最大30秒
    const delay = Math.min(3000 * (reconnectAttempts.value + 1), 30000)
    reconnectTimeout.value = window.setTimeout(() => {
      reconnectAttempts.value++
      connect()
    }, delay)
  }
  
  return {
    connected,
    error,
    connect,
    disconnect
  }
}) 
