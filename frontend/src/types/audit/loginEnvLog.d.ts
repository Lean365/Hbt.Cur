import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 登录环境日志对象
 */
export interface HbtLoginEnvLog extends HbtBaseEntity {
  loginEnvLogId: number;
  userId: number;
  tenantId: number;
  deviceId: string;
  environmentId: string;
  loginType: number;
  loginSource: number;
  loginStatus: number;
  loginProvider: number;
  providerKey: string;
  providerDisplayName: string;
  firstLoginTime?: string;
  firstLoginIp?: string;
  firstLoginLocation?: string;
  firstLoginDeviceId?: string;
  firstLoginDeviceType?: number;
  firstLoginBrowser?: number;
  firstLoginOs?: number;
  lastLoginTime?: string;
  lastLoginIp?: string;
  lastLoginLocation?: string;
  lastLoginDeviceId?: string;
  lastLoginDeviceType?: number;
  lastLoginBrowser?: number;
  lastLoginOs?: number;
  lastOfflineTime?: string;
  todayOnlinePeriods?: string;
  loginCount: number;
  continuousLoginDays: number;
}

/**
 * 登录环境日志查询参数
 */
export interface HbtLoginEnvLogQuery extends HbtPagedQuery {
  userId?: number;
  tenantId?: number;
  deviceId?: string;
  environmentId?: string;
  loginType?: number;
  loginSource?: number;
  loginStatus?: number;
  loginProvider?: number;
  beginTime?: string;
  endTime?: string;
}

/**
 * 登录环境日志创建参数
 */
export interface HbtLoginEnvLogCreate {
  userId: number;
  tenantId: number;
  deviceId: string;
  environmentId: string;
  loginType: number;
  loginSource: number;
  loginStatus: number;
  loginProvider: number;
  providerKey: string;
  providerDisplayName: string;
  firstLoginTime?: string;
  firstLoginIp?: string;
  firstLoginLocation?: string;
  firstLoginDeviceId?: string;
  firstLoginDeviceType?: number;
  firstLoginBrowser?: number;
  firstLoginOs?: number;
  lastLoginTime?: string;
  lastLoginIp?: string;
  lastLoginLocation?: string;
  lastLoginDeviceId?: string;
  lastLoginDeviceType?: number;
  lastLoginBrowser?: number;
  lastLoginOs?: number;
  lastOfflineTime?: string;
  todayOnlinePeriods?: string;
  loginCount: number;
  continuousLoginDays: number;
}

/**
 * 登录环境日志更新参数
 */
export interface HbtLoginEnvLogUpdate extends HbtLoginEnvLogCreate {
  id: number;
}

/**
 * 登录环境日志模板
 */
export interface HbtLoginEnvLogTemplate {
  userId: string;
  tenantId: string;
  deviceId: string;
  environmentId: string;
  loginType: string;
  loginSource: string;
  loginStatus: string;
  loginProvider: string;
  providerKey: string;
  providerDisplayName: string;
  firstLoginTime: string;
  firstLoginIp: string;
  firstLoginLocation: string;
  firstLoginDeviceId: string;
  firstLoginDeviceType: string;
  firstLoginBrowser: string;
  firstLoginOs: string;
  lastLoginTime: string;
  lastLoginIp: string;
  lastLoginLocation: string;
  lastLoginDeviceId: string;
  lastLoginDeviceType: string;
  lastLoginBrowser: string;
  lastLoginOs: string;
  lastOfflineTime: string;
  todayOnlinePeriods: string;
  loginCount: string;
  continuousLoginDays: string;
}

/**
 * 登录环境日志导入参数
 */
export interface HbtLoginEnvLogImport {
  userId: number;
  tenantId: number;
  deviceId: string;
  environmentId: string;
  loginType: number;
  loginSource: number;
  loginStatus: number;
  loginProvider: number;
  providerKey: string;
  providerDisplayName: string;
  firstLoginTime?: string;
  firstLoginIp?: string;
  firstLoginLocation?: string;
  firstLoginDeviceId?: string;
  firstLoginDeviceType?: number;
  firstLoginBrowser?: number;
  firstLoginOs?: number;
  lastLoginTime?: string;
  lastLoginIp?: string;
  lastLoginLocation?: string;
  lastLoginDeviceId?: string;
  lastLoginDeviceType?: number;
  lastLoginBrowser?: number;
  lastLoginOs?: number;
  lastOfflineTime?: string;
  todayOnlinePeriods?: string;
  loginCount: number;
  continuousLoginDays: number;
}

/**
 * 登录环境日志导出参数
 */
export interface HbtLoginEnvLogExport {
  userId: number;
  tenantId: number;
  deviceId: string;
  environmentId: string;
  loginType: number;
  loginSource: number;
  loginStatus: number;
  loginProvider: number;
  providerKey: string;
  providerDisplayName: string;
  firstLoginTime: string;
  firstLoginIp: string;
  firstLoginLocation: string;
  firstLoginDeviceId: string;
  firstLoginDeviceType: number;
  firstLoginBrowser: number;
  firstLoginOs: number;
  lastLoginTime: string;
  lastLoginIp: string;
  lastLoginLocation: string;
  lastLoginDeviceId: string;
  lastLoginDeviceType: number;
  lastLoginBrowser: number;
  lastLoginOs: number;
  lastOfflineTime: string;
  todayOnlinePeriods: string;
  loginCount: number;
  continuousLoginDays: number;
  createTime: string;
}

/**
 * 登录环境日志分页结果
 */
export type HbtLoginEnvLogPageResult = HbtPagedResult<HbtLoginEnvLog>

/**
 * 登录环境日志DTO
 */
export interface HbtLoginEnvLogDto {
  loginEnvLogId: number;
  userId: number;
  tenantId: number;
  deviceId: string;
  environmentId: string;
  loginType: number;
  loginSource: number;
  loginStatus: number;
  loginProvider: number;
  providerKey: string;
  providerDisplayName: string;
  firstLoginTime?: string;
  firstLoginIp?: string;
  firstLoginLocation?: string;
  firstLoginDeviceId?: string;
  firstLoginDeviceType?: number;
  firstLoginBrowser?: number;
  firstLoginOs?: number;
  lastLoginTime?: string;
  lastLoginIp?: string;
  lastLoginLocation?: string;
  lastLoginDeviceId?: string;
  lastLoginDeviceType?: number;
  lastLoginBrowser?: number;
  lastLoginOs?: number;
  lastOfflineTime?: string;
  todayOnlinePeriods?: string;
  loginCount: number;
  continuousLoginDays: number;
  createTime: string;
  createBy: string;
  updateTime: string;
  updateBy: string;
}