import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 设备扩展查询参数
 */
export interface DeviceExtendQuery extends HbtPageQuery {
  /** 用户ID */
  userId?: number;
  /** 设备ID */
  deviceId?: string;
  /** 设备类型（0=PC 1=移动设备 2=平板 3=其他） */
  deviceType?: number;
  /** 状态（0=正常 1=停用） */
  status?: number;
  /** 开始时间 */
  beginTime?: string;
  /** 结束时间 */
  endTime?: string;
}

/**
 * 设备扩展对象
 */
export interface DeviceExtend extends HbtBaseEntity {
  /** ID */
  id: number;
  /** 用户ID */
  userId: number;
  /** 设备ID */
  deviceId: string;
  /** 设备类型（0=PC 1=移动设备 2=平板 3=其他） */
  deviceType: number;
  /** 设备名称 */
  deviceName: string;
  /** 设备型号 */
  deviceModel: string;
  /** 操作系统版本 */
  osVersion: string;
  /** 浏览器类型（0=Chrome 1=Firefox 2=Safari 3=Edge 4=IE 5=Opera 6=其他） */
  browserType: number;
  /** 浏览器版本 */
  browserVersion: string;
  /** IP地址 */
  ipAddress: string;
  /** 地理位置 */
  location: string;
  /** 最后登录时间 */
  lastLoginTime: string;
  /** 状态（0=正常 1=停用） */
  status: number;
  /** 备注 */
  remark?: string;
}

/**
 * 创建设备扩展参数
 */
export interface DeviceExtendCreate {
  /** 用户ID */
  userId: number;
  /** 设备ID */
  deviceId: string;
  /** 设备类型（0=PC 1=移动设备 2=平板 3=其他） */
  deviceType: number;
  /** 设备名称 */
  deviceName: string;
  /** 设备型号 */
  deviceModel: string;
  /** 操作系统版本 */
  osVersion: string;
  /** 浏览器类型（0=Chrome 1=Firefox 2=Safari 3=Edge 4=IE 5=Opera 6=其他） */
  browserType: number;
  /** 浏览器版本 */
  browserVersion: string;
  /** IP地址 */
  ipAddress: string;
  /** 地理位置 */
  location: string;
  /** 状态（0=正常 1=停用） */
  status: number;
  /** 备注 */
  remark?: string;
}

/**
 * 更新设备扩展参数
 */
export interface DeviceExtendUpdate extends DeviceExtendCreate {
  /** ID */
  id: number;
}

/**
 * 设备扩展状态更新参数
 */
export interface DeviceExtendStatus {
  /** ID */
  id: number;
  /** 状态（0=正常 1=停用） */
  status: number;
}

/**
 * 设备扩展在线时段更新参数
 */
export interface DeviceExtendOnlinePeriodUpdate {
  /** 用户ID */
  userId: number;
  /** 设备ID */
  deviceId: string;
  /** 开始时间 */
  startTime: string;
  /** 结束时间 */
  endTime: string;
}

/**
 * 设备类型枚举
 */
export enum HbtDeviceType {
  /** PC */
  PC = 0,
  /** 移动设备 */
  Mobile = 1,
  /** 平板 */
  Tablet = 2,
  /** 其他 */
  Other = 3
}

/**
 * 操作系统类型枚举
 */
export enum HbtOsType {
  /** Windows */
  Windows = 0,
  /** Linux */
  Linux = 1,
  /** MacOS */
  MacOS = 2,
  /** iOS */
  iOS = 3,
  /** Android */
  Android = 4,
  /** 其他 */
  Other = 5
}

/**
 * 浏览器类型枚举
 */
export enum HbtBrowserType {
  /** Chrome */
  Chrome = 0,
  /** Firefox */
  Firefox = 1,
  /** Safari */
  Safari = 2,
  /** Edge */
  Edge = 3,
  /** IE */
  IE = 4,
  /** Opera */
  Opera = 5,
  /** 其他 */
  Other = 6
}

/**
 * 设备信息接口
 */
export interface DeviceInfo {
  /** 设备ID */
  deviceId: string
  /** 设备类型 */
  deviceType: HbtDeviceType
  /** 设备名称 */
  deviceName: string
  /** 设备型号 */
  deviceModel: string
  /** 操作系统类型 */
  osType: HbtOsType
  /** 操作系统版本 */
  osVersion: string
  /** 浏览器类型 */
  browserType: HbtBrowserType
  /** 浏览器版本 */
  browserVersion: string
  /** 分辨率 */
  resolution: string
  /** IP地址 */
  ipAddress: string
  /** 地理位置 */
  location: string
}

/**
 * 设备信息字段长度限制
 */
export const DEVICE_INFO_LENGTH = {
  /** 设备ID最大长度 */
  DEVICE_ID: 100,
  /** 设备名称最大长度 */
  DEVICE_NAME: 100,
  /** 设备型号最大长度 */
  DEVICE_MODEL: 100,
  /** 操作系统版本最大长度 */
  OS_VERSION: 50,
  /** 浏览器版本最大长度 */
  BROWSER_VERSION: 50,
  /** 分辨率最大长度 */
  RESOLUTION: 50,
  /** IP地址最大长度 */
  IP_ADDRESS: 50,
  /** 地理位置最大长度 */
  LOCATION: 100
} as const 