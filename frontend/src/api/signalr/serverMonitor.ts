import request from '@/utils/request'
import type { HbtServerMonitorDto, HbtNetworkDto } from '@/types/realtime/serverMonitor'

/**
 * 获取服务器基本信息
 */
export function getServerInfo() {
  return request<{ data: HbtServerMonitorDto }>({
    url: '/api/HbtServerMonitor/info',
    method: 'get'
  })
}

/**
 * 获取网络信息
 */
export function getNetworkInfo() {
  return request<{ data: HbtNetworkDto[] }>({
    url: '/api/HbtServerMonitor/network',
    method: 'get'
  })
} 