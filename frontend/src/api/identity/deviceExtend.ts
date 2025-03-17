import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  DeviceExtendQuery, 
  DeviceExtend, 
  DeviceExtendCreate, 
  DeviceExtendUpdate,
  DeviceExtendOnlinePeriodUpdate
} from '@/types/identity/deviceExtend'

// 获取设备扩展信息列表
export function getDeviceExtendList(params: DeviceExtendQuery) {
  return request<HbtApiResponse<DeviceExtend[]>>({
    url: '/api/device-extend',
    method: 'get',
    params
  })
}

// 获取设备扩展详情
export function getDeviceExtend(userId: number, deviceId: string) {
  return request<HbtApiResponse<DeviceExtend>>({
    url: `/api/device-extend/${userId}/${deviceId}`,
    method: 'get'
  })
}

// 获取用户的所有设备扩展信息
export function getByUserId(userId: number) {
  return request<HbtApiResponse<DeviceExtend[]>>({
    url: `/api/device-extend/user/${userId}`,
    method: 'get'
  })
}

// 更新设备信息
export function updateDeviceInfo(data: DeviceExtendUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/device-extend/device',
    method: 'put',
    data
  })
}

// 更新设备离线信息
export function updateOfflineInfo(userId: number, deviceId: string) {
  return request<HbtApiResponse<any>>({
    url: `/api/device-extend/${userId}/${deviceId}/offline`,
    method: 'put'
  })
}

// 更新设备在线时段
export function updateOnlinePeriod(data: DeviceExtendOnlinePeriodUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/device-extend/online-period',
    method: 'put',
    data
  })
}

// 获取当前设备信息
export function getCurrentDevice() {
  return request<HbtApiResponse<DeviceExtend>>({
    url: '/api/device-extend/current',
    method: 'get'
  })
}