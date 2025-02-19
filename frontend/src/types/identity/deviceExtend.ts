// 设备扩展查询参数
export interface DeviceExtendQuery {
  pageNum?: number;
  pageSize?: number;
  userId?: number;
  deviceId?: string;
  deviceType?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 设备扩展对象
export interface DeviceExtend {
  id: number;
  userId: number;
  deviceId: string;
  deviceType: string;
  deviceName: string;
  deviceModel: string;
  osVersion: string;
  browserType: string;
  browserVersion: string;
  ipAddress: string;
  location: string;
  lastLoginTime: string;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建设备扩展参数
export interface DeviceExtendCreate {
  userId: number;
  deviceId: string;
  deviceType: string;
  deviceName: string;
  deviceModel: string;
  osVersion: string;
  browserType: string;
  browserVersion: string;
  ipAddress: string;
  location: string;
  status: string;
  remark?: string;
}

// 更新设备扩展参数
export interface DeviceExtendUpdate extends DeviceExtendCreate {
  id: number;
}

// 设备扩展状态更新参数
export interface DeviceExtendStatus {
  id: number;
  status: string;
}

// 设备扩展在线时段更新参数
export interface DeviceExtendOnlinePeriodUpdate {
  userId: number;
  deviceId: string;
  startTime: string;
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