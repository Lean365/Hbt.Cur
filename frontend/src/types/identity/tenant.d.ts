import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { Dayjs } from 'dayjs'

/**
 * 租户对象
 */
export interface HbtTenant extends HbtBaseEntity {
  tenantId: number;
  tenantName: string;
  tenantCode: string;
  contactUser?: string;
  contactPhone?: string;
  contactEmail: string;
  address?: string;
  license?: string;
  expireTime?: string | Dayjs;
  status: number;
  isDefault: number;
  dbConnection: string;
  domain: string;
  logoUrl?: string;
  theme?: string;
  licenseStartTime?: string | Dayjs;
  licenseEndTime?: string | Dayjs;
  maxUserCount: number;
}

/**
 * 租户查询参数
 */
export interface HbtTenantQuery extends HbtPagedQuery {
  tenantName?: string;
  tenantCode?: string;
  contactUser?: string;
  contactPhone?: string;
  status?: number;
  beginTime?: string;
  endTime?: string;
}

/**
 * 创建租户参数
 */
export interface HbtTenantCreate {
  tenantId?: number;
  tenantName: string;
  tenantCode: string;
  contactUser: string;
  contactPhone: string;
  contactEmail: string;
  address?: string;
  license?: string;
  dbConnection: string;
  domain: string;
  logoUrl?: string;
  theme?: string;
  licenseStartTime?: string | Dayjs;
  licenseEndTime?: string | Dayjs;
  maxUserCount: number;
  expireTime: string | Dayjs;
  status: number;
  isDefault: number;
}

/**
 * 更新租户参数
 */
export interface HbtTenantUpdate extends HbtTenantCreate {
  tenantId: number;
}

/**
 * 租户导入模板
 */
export interface HbtTenantTemplate {
  tenantCode: string;
  tenantName: string;
  contactPerson: string;
  contactPhone: string;
  contactEmail: string;
  address: string;
  domain: string;
  theme: string;
  licenseStartTime: string;
  licenseEndTime: string;
  maxUserCount: string;
}

/**
 * 租户导入参数
 */
export interface HbtTenantImport {
  tenantName: string;
  tenantCode: string;
  contactPerson?: string;
  contactPhone?: string;
  contactEmail?: string;
  address?: string;
  domain?: string;
}

/**
 * 租户导出参数
 */
export interface HbtTenantExport {
  tenantName: string;
  tenantCode: string;
  contactPerson: string;
  contactPhone: string;
  contactEmail: string;
  address: string;
  domain: string;
  createTime: string;
} 

/**
 * 租户状态更新参数
 */
export interface HbtTenantStatus {
  tenantId: number;
  status: number;
}

/**
 * 租户分页结果
 */
export type HbtTenantPageResult = HbtPagedResult<HbtTenant>

/**
 * 租户DTO
 */
export interface HbtTenantDto {
  tenantId: number;
  tenantName: string;
  tenantCode: string;
  contactUser: string;
  contactPhone: string;
  contactEmail: string;
  address?: string;
  license?: string;
  dbConnection: string;
  domain: string;
  logoUrl?: string;
  theme?: string;
  licenseStartTime?: string | Date;
  licenseEndTime?: string | Date;
  maxUserCount: number;
  expireTime: string | Date;
  status: number;
  isDefault: number;
  createTime: string;
  updateTime: string;
}

/**
 * 租户选项
 */
export interface HbtTenantOption {
  label: string;
  value: number;
}

