import { HubConnection, HubConnectionState } from '@microsoft/signalr'
import { createHubConnection, startHeartbeat, handleConnectionError } from './config'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { message } from 'ant-design-vue'
import i18n from '@/locales'
import { LogLevel, customLogger } from '@/utils/logger'
import axios from 'axios'

const { t } = i18n.global

type EventHandler = (...args: any[]) => void

// 系统配置接口
interface SystemConfig {
  singleSignOn: {
    enabled: boolean;
  };
  // 其他配置项...
}

// 默认配置
const DEFAULT_CONFIG: SystemConfig = {
  singleSignOn: {
    enabled: false // 默认为多点登录
  }
};

// 全局配置对象
let globalConfig: SystemConfig = DEFAULT_CONFIG;

// 初始化配置
export async function initSystemConfig() {
  try {
    // 从后端获取系统配置
    const response = await axios.get('/api/system/config')
    globalConfig = response.data
    console.log('[SignalR] 系统配置已加载:', globalConfig)
  } catch (error) {
    console.error('[SignalR] 获取系统配置失败，使用默认配置:', error)
    globalConfig = DEFAULT_CONFIG
  }
}

export class SignalRService {
  private static instance: SignalRService
  private connection: HubConnection | null = null
  private eventHandlers: Map<string, EventHandler[]> = new Map()
  private heartbeat: any = null
  private connecting: boolean = false
  private isInitialized: boolean = false
  private isStarting: boolean = false
  private isConnected: boolean = false
  private retryCount: number = 0
  private maxRetries: number = 5
  private lastConnectionId: string | null = null
  private connectionCheckInterval: NodeJS.Timeout | null = null

  private constructor() {
    this.start = this.start.bind(this)
    this.stop = this.stop.bind(this)
    this.on = this.on.bind(this)
    this.off = this.off.bind(this)
  }

  public static getInstance(): SignalRService {
    if (!SignalRService.instance) {
      SignalRService.instance = new SignalRService()
    }
    return SignalRService.instance
  }

  resetConnectionState() {
    console.log('[SignalR] 重置连接状态')
    this.connecting = false
    this.isInitialized = false
    this.isStarting = false
    if (this.heartbeat) {
      this.heartbeat.stop()
      this.heartbeat = null
    }
    if (this.connection) {
      this.connection.stop()
      this.connection = null
    }
    this.retryCount = 0
    this.isConnected = false
  }

  public async start(): Promise<void> {
    try {
      if (this.connecting) {
        console.log('[SignalR] 已经在连接中，跳过重复连接')
        return
      }

      this.connecting = true
      console.log('[SignalR] 启动连接前的 Token 状态:', await getToken() ? '已获取' : '未获取')
      
      if (!this.connection) {
        console.log('[SignalR] 开始创建新连接')
        this.connection = await createHubConnection()
      }

      console.log('[SignalR] 开始启动连接')
      await this.setupConnection()
      
    } catch (error) {
      const errorType = handleConnectionError(error as Error)
      if (errorType === 'AUTH_ERROR') {
        removeToken()
        window.location.href = '/login'
        return
      }
      throw error
    } finally {
      this.connecting = false
    }
  }

  async stop() {
    try {
      if (this.heartbeat) {
        this.heartbeat.stop()
        this.heartbeat = null
      }
      
      if (this.connection) {
        await this.connection.stop()
        this.connection = null
      }
      
      this.resetConnectionState()
      console.log('[SignalR] 连接已停止')
    } catch (error) {
      console.error('[SignalR] 停止连接失败:', error)
    }
  }

  on(eventName: string, handler: EventHandler) {
    if (!this.eventHandlers.has(eventName)) {
      this.eventHandlers.set(eventName, [])
    }
    this.eventHandlers.get(eventName)?.push(handler)
    
    if (this.connection) {
      this.connection.on(eventName, handler)
    }
  }

  off(eventName: string, handler?: EventHandler) {
    if (handler) {
      const handlers = this.eventHandlers.get(eventName)
      if (handlers) {
        const index = handlers.indexOf(handler)
        if (index > -1) {
          handlers.splice(index, 1)
        }
      }
      if (this.connection) {
        this.connection.off(eventName, handler)
      }
    } else {
      this.eventHandlers.delete(eventName)
      if (this.connection) {
        this.connection.off(eventName)
      }
    }
  }

  private registerConnectionHandlers() {
    if (!this.connection) return

    this.connection.onclose((error) => {
      console.error('[SignalR] 连接关闭:', error?.message || '未知原因')
      this.isConnected = false
      this.emit('ConnectionClosed', error)
      
      if (!this.isStarting) {
        this.retryConnection()
      }
    })

    this.connection.onreconnecting((error) => {
      console.warn('[SignalR] 正在重连:', error?.message || '未知原因')
      this.emit('Reconnecting', error)
    })

    this.connection.onreconnected((connectionId) => {
      console.log('[SignalR] 重连成功，新连接ID:', connectionId)
      this.isConnected = true
      this.retryCount = 0
      this.emit('Reconnected', connectionId)
    })

    this.registerEventHandlers()
  }

  private registerEventHandlers() {
    if (!this.connection) return

    this.connection.on('ReceiveMessage', (message: string) => {
      console.log('[SignalR] 收到消息:', message)
      this.emit('ReceiveMessage', message)
    })

    this.connection.on('UserStatusChanged', (userId: string, isOnline: boolean) => {
      console.log('[SignalR] 用户状态变化:', userId, isOnline)
      this.emit('UserStatusChanged', userId, isOnline)
    })

    this.connection.on('UserKickedOut', (message: string) => {
      console.log('[SignalR] 用户被踢出:', message)
      this.emit('UserKickedOut', message)
    })

    this.connection.on('ForceOffline', (message: string) => {
      console.log('[SignalR] 收到强制下线通知:', message)
      this.emit('ForceOffline', message)
    })

    this.connection.on('Kickout', (message: string) => {
      console.log('[SignalR] 收到Kickout事件:', message)
      this.emit('Kickout', message)
    })
  }

  private emit(eventName: string, ...args: any[]) {
    const handlers = this.eventHandlers.get(eventName)
    if (handlers) {
      handlers.forEach(handler => handler(...args))
    }
  }

  private async setupConnection(): Promise<void> {
    try {
      if (!this.connection) return

      this.registerConnectionHandlers()
      await this.connection.start()
      
      console.log('[SignalR] 连接已建立，连接ID:', this.connection.connectionId)
      this.isConnected = true
      this.retryCount = 0

      this.startHeartbeat()
      
    } catch (error) {
      console.error('[SignalR] 连接建立失败:', error)
      this.isConnected = false
      this.retryCount++
      
      if (this.retryCount >= this.maxRetries) {
        console.error('[SignalR] 达到最大重试次数，停止重连')
        throw error
      }
      
      const delay = Math.min(1000 * Math.pow(2, this.retryCount), 30000)
      console.log(`[SignalR] ${delay/1000}秒后重试连接...`)
      await new Promise(resolve => setTimeout(resolve, delay))
      return this.setupConnection()
    }
  }

  private startHeartbeat() {
    if (this.heartbeat) {
      this.heartbeat.stop()
    }
    if (this.connection) {
      this.heartbeat = startHeartbeat(this.connection)
    }
  }

  private async retryConnection() {
    if (this.retryCount >= this.maxRetries) {
      console.error('[SignalR] 达到最大重试次数，停止重连')
      return
    }

    this.retryCount++
    const delay = Math.min(1000 * Math.pow(2, this.retryCount), 30000)
    console.log(`[SignalR] ${delay/1000}秒后重试连接...`)
    
    await new Promise(resolve => setTimeout(resolve, delay))
    try {
      await this.start()
    } catch (error) {
      console.error('[SignalR] 重连失败:', error)
    }
  }
}

// 导出单例实例
export const signalRService = SignalRService.getInstance() 