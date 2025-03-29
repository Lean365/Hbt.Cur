import { HubConnectionBuilder, IHttpConnectionOptions, LogLevel } from '@microsoft/signalr'
import { getDeviceInfo } from '../device'
import request from '@/utils/request'
import { getToken } from '@/utils/auth'

// SignalR配置
export const signalRConfig = {
  // Hub URL（使用环境变量中的API基础URL）
  hubUrl: `${import.meta.env.VITE_API_BASE_URL}/signalr/hbthub`,
  // 日志级别
  logLevel: LogLevel.Debug,
  // 重连间隔(毫秒)
  reconnectInterval: 5000,
  // 最大重连次数
  maxRetries: 5,
  // 心跳间隔(毫秒)
  heartbeatInterval: 15000,
  // 心跳超时时间(毫秒)
  heartbeatTimeout: 3000,
  // 单点登录配置
  sso: {
    // 是否启用单点登录
    enabled: true,
    // 踢出消息事件名
    kickoutEvent: 'UserKickedOut',
    // 踢出后的重定向路径
    redirectPath: '/login',
    // 默认踢出消息
    defaultKickoutMessage: '您的账号已在其他设备上登录'
  }
}

// 创建Hub连接
export const createHubConnection = () => {
  // 获取设备信息
  const deviceInfo = getDeviceInfo()
  // 获取认证token
  const token = getToken()

  const options: IHttpConnectionOptions = {
    headers: {
      ...(deviceInfo ? { 'X-Device-Info': JSON.stringify(deviceInfo) } : {}),
      ...(token ? { 'Authorization': `Bearer ${token}` } : {})
    }
  }

  console.log('[SignalR] 创建连接，URL:', signalRConfig.hubUrl)
  console.log('[SignalR] 连接选项:', options)

  return new HubConnectionBuilder()
    .withUrl(signalRConfig.hubUrl, options)
    .withAutomaticReconnect([0, 2000, 5000, 10000, 30000])
    .configureLogging(signalRConfig.logLevel)
    .build()
}

export const startHeartbeat = () => {
    // 每30秒发送一次心跳
    const heartbeatInterval = setInterval(async () => {
        try {
            await request.post('/api/HbtOnlineUser/heartbeat');
            console.log('[心跳] 发送成功');
        } catch (error) {
            console.error('[心跳] 发送失败:', error);
        }
    }, 30000);

    // 返回清理函数
    return () => clearInterval(heartbeatInterval);
}; 