import { HubConnection, HubConnectionState } from '@microsoft/signalr'
import { createHubConnection, startHeartbeat, handleConnectionError } from './config'
import { getToken, removeToken } from '@/utils/auth'
import { useUserStore } from '@/stores/user'
import { message } from 'ant-design-vue'
import i18n from '@/locales'
import { LogLevel, customLogger } from '@/utils/logger'
import axios from 'axios'
import { nextTick } from 'vue'
import { getDeviceInfo } from '@/utils/device'

const { t } = i18n.global

// 事件处理器类型定义
type EventHandler = (...args: any[]) => void

// 系统配置接口定义
interface SystemConfig {
  singleSignOn: {
    enabled: boolean;  // 是否启用单点登录
  };
  // 其他配置项...
}

// 默认系统配置
const DEFAULT_CONFIG: SystemConfig = {
  singleSignOn: {
    enabled: false // 默认为多点登录
  }
};

// 全局配置对象
let globalConfig: SystemConfig = DEFAULT_CONFIG;

// 初始化系统配置
export async function initSystemConfig() {
  try {
    // 从后端获取系统配置
    const response = await axios.get('/api/admin/config')
    globalConfig = response.data
    console.log('[SignalR] 系统配置已加载:', globalConfig)
  } catch (error) {
    console.error('[SignalR] 获取系统配置失败，使用默认配置:', error)
    globalConfig = DEFAULT_CONFIG
  }
}

// SignalR服务类
export class SignalRService {
  // 单例实例
  private static instance: SignalRService
  // SignalR连接实例
  private connection: HubConnection | null = null
  // 事件处理器映射表
  private eventHandlers: Map<string, EventHandler[]> = new Map()
  // 心跳检测实例
  private heartbeat: any = null
  // 是否正在连接中
  private connecting: boolean = false
  // 是否已初始化
  private isInitialized: boolean = false
  // 是否正在启动
  private isStarting: boolean = false
  // 是否已连接
  private isConnected: boolean = false
  // 重试次数
  private retryCount: number = 0
  // 最大重试次数
  private maxRetries: number = 5
  // 上次连接ID
  private lastConnectionId: string | null = null
  // 连接检查定时器
  private connectionCheckInterval: NodeJS.Timeout | null = null
  // 用户状态存储
  private userStore: any

  // 私有构造函数，确保单例模式
  private constructor() {
    this.start = this.start.bind(this)
    this.stop = this.stop.bind(this)
    this.on = this.on.bind(this)
    this.off = this.off.bind(this)
  }

  // 获取连接状态
  public getConnectionState(): boolean {
    return this.isConnected
  }

  // 获取单例实例
  public static getInstance(): SignalRService {
    if (!SignalRService.instance) {
      SignalRService.instance = new SignalRService()
    }
    return SignalRService.instance
  }

  // 重置连接状态
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

  // 启动SignalR连接
  public async start(): Promise<void> {
    try {
      // 如果已经连接，直接返回
      if (this.isConnected) {
        console.log('[SignalR] 已经连接，无需重复连接')
        return
      }

      // 如果正在连接中，等待连接完成
      if (this.connecting) {
        console.log('[SignalR] 正在连接中，等待连接完成')
        return
      }

      this.connecting = true
      console.log('[SignalR] 启动连接前的 Token 状态:', await getToken() ? '已获取' : '未获取')
      
      // 确保现有连接已完全停止
      if (this.connection) {
        console.log('[SignalR] 停止现有连接')
        await this.stop()
        // 等待连接完全关闭
        await new Promise(resolve => setTimeout(resolve, 1000))
      }

      console.log('[SignalR] 开始创建新连接')
      this.connection = await createHubConnection()

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

  // 停止SignalR连接
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

  // 注册事件处理器
  on(eventName: string, handler: EventHandler) {
    if (!this.eventHandlers.has(eventName)) {
      this.eventHandlers.set(eventName, [])
    }
    this.eventHandlers.get(eventName)?.push(handler)
    
    if (this.connection) {
      this.connection.on(eventName, handler)
    }
  }

  // 移除事件处理器
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

  // 注册连接事件处理器
  private registerConnectionHandlers() {
    if (!this.connection) return

    // 连接关闭事件
    this.connection.onclose((error) => {
      const reason = error ? error.message : '正常关闭'
      console.log('[SignalR] 连接关闭:', reason)
      this.isConnected = false
      this.emit('ConnectionClosed', error)
    })

    // 重连中事件
    this.connection.onreconnecting((error) => {
      console.warn('[SignalR] 正在重连:', error?.message || '未知原因')
      this.emit('Reconnecting', error)
    })

    // 重连成功事件
    this.connection.onreconnected((connectionId) => {
      console.log('[SignalR] 重连成功，新连接ID:', connectionId)
      this.isConnected = true
      this.emit('Reconnected', connectionId)
    })

    this.registerEventHandlers()
  }

  // 注册业务事件处理器
  private registerEventHandlers() {
    if (!this.connection) return

    // 接收消息事件
    this.connection.on('ReceiveMessage', (message: any) => {
      this.handleNewMessage(message)
    })

    // 接收邮件状态更新事件
    this.connection.on('ReceiveMailStatus', (notification: any) => {
      console.log('[SignalR] 收到邮件状态更新:', {
        raw: notification,
        type: notification?.Type,
        title: notification?.Title,
        content: notification?.Content,
        timestamp: notification?.Timestamp,
        data: notification?.Data
      });
      this.emit('ReceiveMailStatus', notification);
    })

    // 接收通知状态更新事件
    this.connection.on('ReceiveNoticeStatus', (notification: any) => {
      console.log('[SignalR] 收到通知状态更新:', {
        raw: notification,
        type: notification?.Type,
        title: notification?.Title,
        content: notification?.Content,
        timestamp: notification?.Timestamp,
        data: notification?.Data
      });
      this.emit('ReceiveNoticeStatus', notification);
    })

    // 接收任务状态更新事件
    this.connection.on('ReceiveTaskStatus', (notification: any) => {
      console.log('[SignalR] 收到任务状态更新:', {
        raw: notification,
        type: notification?.Type,
        title: notification?.Title,
        content: notification?.Content,
        timestamp: notification?.Timestamp
      });
      this.emit('ReceiveTaskStatus', notification);
    })

    // 接收个人通知事件
    this.connection.on('ReceivePersonalNotice', (notification: any) => {
      console.log('[SignalR] 收到个人通知:', {
        raw: notification,
        type: notification?.Type,
        title: notification?.Title,
        content: notification?.Content,
        timestamp: notification?.Timestamp
      });
      this.emit('ReceivePersonalNotice', notification);
    })

    // 接收系统广播事件
    this.connection.on('ReceiveBroadcast', (notification: any) => {
      console.log('[SignalR] 收到系统广播:', {
        raw: notification,
        type: notification?.Type,
        title: notification?.Title,
        content: notification?.Content,
        timestamp: notification?.Timestamp
      });
      this.emit('ReceiveBroadcast', notification);
    })

    // 接收心跳响应事件
    this.connection.on('ReceiveHeartbeat', (timestamp: Date) => {
      console.log('[SignalR] 收到心跳响应:', {
        raw: timestamp,
        timestamp: timestamp,
        dateType: typeof timestamp
      });
      this.emit('ReceiveHeartbeat', timestamp);
    })

    // 接收强制下线事件
    this.connection.on('ForceOffline', (message: string) => {
      console.log('[SignalR] 收到强制下线通知:', {
        raw: message,
        message: message,
        messageType: typeof message
      });
      this.emit('ForceOffline', message);
    })
  }

  // 设置连接
  private async setupConnection() {
    if (!this.connection) {
      throw new Error('连接未创建')
    }

    try {
      // 注册连接事件处理器
      this.registerConnectionHandlers()

      // 启动连接
      console.log('[SignalR] 开始启动连接，当前连接ID:', this.connection.connectionId)
      
      // 添加重试机制
      let retryCount = 0
      const maxRetries = 3
      const retryDelay = 1000 // 1秒

      while (retryCount < maxRetries) {
        try {
          // 验证设备信息
          const deviceInfo = await getDeviceInfo()
          if (!deviceInfo.deviceId) {
            throw new Error('设备信息不完整，无法建立连接')
          }

          await this.connection.start()
          
          // 等待连接状态稳定
          let stateCheckCount = 0
          const maxStateChecks = 10
          const stateCheckDelay = 500 // 0.5秒

          while (stateCheckCount < maxStateChecks) {
            if (this.connection.state === 'Connected') {
              const connectionId = this.connection.connectionId
              if (connectionId) {
                console.log('[SignalR] 连接状态稳定，当前连接ID:', connectionId)
                this.isConnected = true
                this.isInitialized = true
                this.lastConnectionId = connectionId
                
                // 启动心跳检测
                this.heartbeat = startHeartbeat(this.connection)
                
                console.log('[SignalR] 连接建立成功，连接ID:', connectionId)
                this.emit('Connected', connectionId)
                return
              }
            }
            
            stateCheckCount++
            console.log(`[SignalR] 等待连接状态稳定，当前状态: ${this.connection.state}, 第 ${stateCheckCount} 次检查`)
            
            if (stateCheckCount >= maxStateChecks) {
              throw new Error('连接状态检查超时')
            }
            
            await new Promise(resolve => setTimeout(resolve, stateCheckDelay))
          }
          
          break
        } catch (error) {
          retryCount++
          console.error(`[SignalR] 第 ${retryCount} 次连接尝试失败:`, error)
          
          if (retryCount >= maxRetries) {
            throw error
          }
          
          console.log(`[SignalR] 等待 ${retryDelay}ms 后重试...`)
          await new Promise(resolve => setTimeout(resolve, retryDelay))
        }
      }
    } catch (error) {
      console.error('[SignalR] 连接建立失败:', error)
      this.isConnected = false
      throw error
    }
  }

  // 发送事件
  private emit(eventName: string, ...args: any[]) {
    const handlers = this.eventHandlers.get(eventName)
    if (handlers) {
      handlers.forEach(handler => handler(...args))
    }
  }

  // 发送消息
  public async sendMessage(data: { userId: string; content: string }): Promise<void> {
    if (!this.connection) {
      console.error('发送消息失败: SignalR 连接未建立');
      throw new Error('SignalR connection is not established');
    }

    if (!this.connection.connectionId) {
      console.error('发送消息失败: SignalR 连接ID未设置');
      throw new Error('SignalR connection ID is not set');
    }

    try {
      console.log('准备发送消息:', {
        userId: data.userId,
        content: data.content,
        connectionId: this.connection.connectionId,
        connectionState: this.connection.state,
        currentUser: useUserStore().userInfo
      });
      
      await this.connection.invoke('SendMessageAsync', data.userId, data.content);
      
      console.log('消息发送成功');
    } catch (error) {
      console.error('发送消息失败:', error);
      throw error;
    }
  }

  // 发送通知
  async sendNotification(notification: any) {
    if (!this.connection) {
      throw new Error('SignalR connection not established')
    }
    
    try {
      await this.connection.invoke('SendNotification', notification)
      console.log('[SignalR] 通知发送成功:', notification)
    } catch (error) {
      console.error('[SignalR] 通知发送失败:', error)
      throw error
    }
  }

  // 调用服务器方法
  async invoke(methodName: string, ...args: any[]): Promise<any> {
    if (!this.connection) {
      throw new Error('SignalR connection not established')
    }
    
    try {
      return await this.connection.invoke(methodName, ...args)
    } catch (error) {
      console.error(`[SignalR] 调用方法 ${methodName} 失败:`, error)
      throw error
    }
  }

  // 处理新消息
  private handleNewMessage(message: any) {
    console.log('[SignalR] 收到原始消息:', message)
    
    try {
      // 如果是字符串类型的消息，尝试解析为JSON
      if (typeof message === 'string') {
        // 检查是否是连接成功消息
        if (message === '连接成功') {
          console.log('[SignalR] 收到连接成功消息，当前连接ID:', this.connection?.connectionId)
          return
        }
        
        // 尝试解析为JSON
        try {
          message = JSON.parse(message)
        } catch (e) {
          console.warn('[SignalR] 消息不是JSON格式，使用原始内容:', message)
        }
      }
      
      // 处理不同类型的消息
      if (message && typeof message === 'object') {
        // 处理连接成功消息
        if (message.type === 'connection' && message.status === 'success') {
          console.log('[SignalR] 收到连接成功消息，当前连接ID:', message.connectionId)
          if (this.connection) {
            console.log('[SignalR] 连接ID已更新:', message.connectionId)
          }
          return
        }
        
        // 处理其他类型的消息
        this.handleMessage(message)
      }
    } catch (error) {
      console.error('[SignalR] 处理消息时发生错误:', error)
    }
  }

  // 处理具体消息内容
  private handleMessage(message: any) {
    // 根据消息类型进行不同的处理
    switch (message.type) {
      case 'notification':
        this.handleNotification(message)
        break
      case 'system':
        this.handleSystemMessage(message)
        break
      default:
        console.log('[SignalR] 收到未知类型的消息:', message)
    }
  }

  // 处理通知消息
  private handleNotification(message: any) {
    // 触发通知事件
    this.emit('notification', message)
  }

  // 处理系统消息
  private handleSystemMessage(message: any) {
    // 触发系统消息事件
    this.emit('system', message)
  }
}

// 导出单例实例
export const signalRService = SignalRService.getInstance() 