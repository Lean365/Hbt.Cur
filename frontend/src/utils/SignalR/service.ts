import { HubConnection } from '@microsoft/signalr'
import { createHubConnection, signalRConfig } from './config'
import { message } from 'ant-design-vue'
import router from '@/router'
import { useUserStore } from '@/stores/user'

class SignalRService {
  private connection: HubConnection | null = null
  private static instance: SignalRService
  private eventHandlers: Map<string, Function[]> = new Map()
  private heartbeatTimer: NodeJS.Timeout | null = null
  private reconnectCount = 0
  private isForceOffline = false

  private constructor() {}

  public static getInstance(): SignalRService {
    if (!SignalRService.instance) {
      SignalRService.instance = new SignalRService()
    }
    return SignalRService.instance
  }

  // 设置强制下线状态
  public setForceOffline(status: boolean): void {
    this.isForceOffline = status
  }

  // 获取强制下线状态
  public getForceOffline(): boolean {
    return this.isForceOffline
  }

  // 启动连接
  public async start(): Promise<void> {
    if (this.connection) return
    if (this.isForceOffline) {
      console.log('[SignalR] 用户被强制下线,禁止重新连接')
      return
    }

    this.connection = createHubConnection()
    
    // 注册重连事件
    this.connection.onreconnecting(() => {
      console.log('[SignalR] 正在重新连接...')
      // 只在非登录页面显示重连消息
      if (!window.location.pathname.includes('/login')) {
        message.warning('[SignalR] 正在尝试重新连接到服务器...')
      }
    })

    this.connection.onreconnected(() => {
      console.log('[SignalR] 重新连接成功')
      // 只在非登录页面显示重连成功消息
      if (!window.location.pathname.includes('/login')) {
        message.success('[SignalR] 已重新连接到服务器')
      }
      this.startHeartbeat()
    })

    this.connection.onclose(() => {
      console.log('[SignalR] 连接已关闭')
      // 只在非登录页面显示断开连接消息
      if (!window.location.pathname.includes('/login')) {
        message.error('[SignalR] 与服务器的连接已断开')
      }
      this.stopHeartbeat()
      // 只有在非强制下线状态下才尝试重连
      if (!this.isForceOffline) {
        this.retryConnection()
      }
    })

    // 注册事件处理
    this.registerHandlers()

    try {
      await this.connection.start()
      console.log('[SignalR] 连接成功')
      // 只在非登录页面显示连接成功消息
      if (!window.location.pathname.includes('/login')) {
        message.success('[SignalR] 已连接到服务器')
      }
      this.reconnectCount = 0
      this.startHeartbeat()
    } catch (error) {
      console.error('[SignalR] 连接失败:', error)
      // 只在非登录页面显示连接失败消息
      if (!window.location.pathname.includes('/login')) {
        message.error('[SignalR] 连接服务器失败')
      }
      this.retryConnection()
    }
  }

  // 停止连接
  public async stop(): Promise<void> {
    this.stopHeartbeat()
    if (!this.connection) return

    try {
      await this.connection.stop()
      this.connection = null
      console.log('SignalR连接已停止')
    } catch (error) {
      console.error('停止SignalR连接失败:', error)
      throw error
    }
  }

  // 重试连接
  private async retryConnection(): Promise<void> {
    if (this.reconnectCount >= signalRConfig.maxRetries) {
      console.error('[SignalR] 达到最大重试次数')
      message.error('[SignalR] 无法连接到服务器，请刷新页面重试')
      return
    }

    this.reconnectCount++
    console.log(`[SignalR] 尝试重新连接 (${this.reconnectCount}/${signalRConfig.maxRetries})...`)
    
    setTimeout(() => {
      this.start()
    }, signalRConfig.reconnectInterval)
  }

  // 开始心跳检测
  private startHeartbeat(): void {
    this.stopHeartbeat()
    
    this.heartbeatTimer = setInterval(async () => {
      try {
        if (this.connection?.state === 'Connected') {
          await this.connection.invoke('SendHeartbeat')
        }
      } catch (error) {
        console.error('发送心跳包失败:', error)
        this.retryConnection()
      }
    }, signalRConfig.heartbeatInterval)
  }

  // 停止心跳检测
  private stopHeartbeat(): void {
    if (this.heartbeatTimer) {
      clearInterval(this.heartbeatTimer)
      this.heartbeatTimer = null
    }
  }

  // 注册事件处理
  private registerHandlers(): void {
    if (!this.connection) {
      console.log('[SignalR] 连接未初始化，无法注册事件处理器')
      return
    }

    console.log('[SignalR] 开始注册事件处理器')

    // 接收消息
    this.connection.on('ReceiveMessage', (message: string) => {
      console.log('[SignalR] 收到消息:', message)
      this.emit('ReceiveMessage', message)
    })

    // 用户上线通知
    this.connection.on('UserOnline', (userId: string) => {
      console.log('[SignalR] 用户上线:', userId)
      this.emit('UserOnline', userId)
    })

    // 用户下线通知
    this.connection.on('UserOffline', (userId: string) => {
      console.log('[SignalR] 用户下线:', userId)
      this.emit('UserOffline', userId)
    })

    // 心跳响应
    this.connection.on('ReceiveHeartbeat', (timestamp: string) => {
      console.debug('[SignalR] 收到心跳响应:', timestamp)
    })

    // 单点登录踢出处理
    if (signalRConfig.sso.enabled) {
      console.log('[SignalR] 注册单点登录踢出事件处理器')
      this.connection.on(signalRConfig.sso.kickoutEvent, async (msg: string) => {
        console.log('[SignalR] 收到踢出通知:', msg)
        await this.handleKickout(msg)
      })
    }

    // 强制下线处理
    console.log('[SignalR] 注册强制下线事件处理器')
    this.connection.on('ForceOffline', async (msg: string) => {
      console.log('[SignalR] 收到强制下线通知:', msg)
      try {
        console.log('[SignalR] 开始处理强制下线')
        await this.handleKickout(msg)
        console.log('[SignalR] 强制下线处理完成')
      } catch (error) {
        console.error('[SignalR] 处理强制下线时发生错误:', error)
      }
    })

    console.log('[SignalR] 事件处理器注册完成')
  }

  // 处理踢出
  private async handleKickout(msg: string): Promise<void> {
    console.log('[SignalR] 开始处理踢出:', msg)
    try {
      // 设置强制下线状态
      this.setForceOffline(true)
      
      // 显示提示
      message.warning(msg || '您已被强制下线')
      console.log('[SignalR] 已显示踢出提示')

      // 清理登录状态
      await this.cleanupLoginState()
      console.log('[SignalR] 已清理登录状态')

      // 跳转到登录页
      await router.replace('/login')
      console.log('[SignalR] 已跳转到登录页')

      // 刷新页面
      window.location.reload()
      console.log('[SignalR] 已触发页面刷新')
    } catch (error) {
      console.error('[SignalR] 处理踢出时发生错误:', error)
      throw error
    }
  }

  // 清理登录状态
  private async cleanupLoginState(): Promise<void> {
    console.log('[SignalR] 开始清理登录状态')
    try {
      // 清除 token
      localStorage.removeItem('token')
      console.log('[SignalR] 已清除 token')

      // 清除用户信息
      localStorage.removeItem('user')
      console.log('[SignalR] 已清除用户信息')

      // 断开 SignalR 连接
      if (this.connection) {
        console.log('[SignalR] 准备断开连接')
        await this.connection.stop()
        console.log('[SignalR] 已断开连接')
      }
    } catch (error) {
      console.error('[SignalR] 清理登录状态时发生错误:', error)
      throw error
    }
  }

  // 发送消息
  public async sendMessage(message: string): Promise<void> {
    if (!this.connection) {
      throw new Error('SignalR未连接')
    }

    try {
      await this.connection.invoke('SendMessage', message)
    } catch (error) {
      console.error('发送消息失败:', error)
      throw error
    }
  }

  // 获取连接状态
  public getConnectionState(): string {
    return this.connection?.state || 'Disconnected'
  }

  // 检查是否已连接
  public isConnected(): boolean {
    return this.connection?.state === 'Connected'
  }

  // 注册事件监听
  public on(event: string, handler: Function): void {
    if (!this.eventHandlers.has(event)) {
      this.eventHandlers.set(event, [])
    }
    this.eventHandlers.get(event)?.push(handler)
  }

  // 移除事件监听
  public off(event: string, handler: Function): void {
    const handlers = this.eventHandlers.get(event)
    if (handlers) {
      const index = handlers.indexOf(handler)
      if (index !== -1) {
        handlers.splice(index, 1)
      }
    }
  }

  // 触发事件
  private emit(event: string, ...args: any[]): void {
    const handlers = this.eventHandlers.get(event)
    if (handlers) {
      handlers.forEach(handler => handler(...args))
    }
  }
}

export const signalRService = SignalRService.getInstance() 