import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

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
 * 登录设备日志对象
 */
export interface HbtLoginDevLog extends HbtBaseEntity {
  loginDevLogId: number;
  userId: number;
  deviceId: string;
  deviceType: number;
  deviceName?: string;
  deviceModel?: string;
  deviceToken?: string;
  osType?: number;
  osVersion?: string;
  browserType?: number;
  browserVersion?: string;
}

/**
 * 登录设备日志查询参数
 */
export interface HbtLoginDevLogQuery extends HbtPagedQuery {
  userId?: number;
  deviceId?: string;
  deviceType?: number;
  deviceName?: string;
  osType?: number;
  browserType?: number;
  beginTime?: string;
  endTime?: string;
}

/**
 * 登录设备日志创建参数
 */
export interface HbtLoginDevLogCreate {
  userId: number;
  deviceId: string;
  deviceType: number;
  deviceName?: string;
  deviceModel?: string;
  deviceToken?: string;
  osType?: number;
  osVersion?: string;
  browserType?: number;
  browserVersion?: string;
}

/**
 * 登录设备日志更新参数
 */
export interface HbtLoginDevLogUpdate extends HbtLoginDevLogCreate {
  id: number;
}

/**
 * 登录设备日志模板
 */
export interface HbtLoginDevLogTemplate {
  userId: string;
  deviceId: string;
  deviceType: string;
  deviceName: string;
  deviceModel: string;
  deviceToken: string;
  osType: string;
  osVersion: string;
  browserType: string;
  browserVersion: string;
}

/**
 * 登录设备日志导入参数
 */
export interface HbtLoginDevLogImport {
  userId: number;
  deviceId: string;
  deviceType: number;
  deviceName?: string;
  deviceModel?: string;
  deviceToken?: string;
  osType?: number;
  osVersion?: string;
  browserType?: number;
  browserVersion?: string;
}

/**
 * 登录设备日志导出参数
 */
export interface HbtLoginDevLogExport {
  userId: number;
  deviceId: string;
  deviceType: number;
  deviceName: string;
  deviceModel: string;
  deviceToken: string;
  osType: number;
  osVersion: string;
  browserType: number;
  browserVersion: string;
  createTime: string;
}

/**
 * 登录设备日志分页结果
 */
export type HbtLoginDevLogPageResult = HbtPagedResult<HbtLoginDevLog>

/**
 * 登录设备日志DTO
 */
export interface HbtLoginDevLogDto {
  loginDevLogId: number;
  userId: number;
  deviceType: number;
  deviceId: string;
  deviceToken?: string;
  deviceName?: string;
  deviceModel?: string;
  osType?: number;
  osVersion?: string;
  browserType?: number;
  browserVersion?: string;
  createTime: string;
  createBy: string;
  updateTime: string;
  updateBy: string;
}
