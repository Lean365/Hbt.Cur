import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 租户查询参数
 */
export interface TenantQuery extends HbtPageQuery {
  /** 租户名称 */
  tenantName?: string;
  /** 状态（0=正常 1=停用） */
  status?: number;
  /** 开始时间 */
  beginTime?: string;
  /** 结束时间 */
  endTime?: string;
}

/**
 * 租户对象
 */
export interface Tenant extends HbtBaseEntity {
  /** 租户ID */
  tenantId: number;
  /** 租户名称 */
  tenantName: string;
  /** 域名 */
  domain: string;
  /** 联系人 */
  linkman: string;
  /** 联系电话 */
  contactNumber: string;
  /** 地址 */
  address: string;
  /** 营业执照号 */
  licenseNumber: string;
  /** 营业执照日期 */
  licenseDate: string;
  /** 状态（0=正常 1=停用） */
  status: number;
  /** 备注 */
  remark?: string;
}

/**
 * 创建租户参数
 */
export interface TenantCreate {
  /** 租户名称 */
  tenantName: string;
  /** 域名 */
  domain: string;
  /** 联系人 */
  linkman: string;
  /** 联系电话 */
  contactNumber: string;
  /** 地址 */
  address?: string;
  /** 营业执照号 */
  licenseNumber?: string;
  /** 营业执照日期 */
  licenseDate?: string;
  /** 状态（0=正常 1=停用） */
  status: number;
  /** 备注 */
  remark?: string;
}

/**
 * 更新租户参数
 */
export interface TenantUpdate extends TenantCreate {
  /** 租户ID */
  tenantId: number;
}

/**
 * 租户状态更新参数
 */
export interface TenantStatus {
  /** 租户ID */
  tenantId: number;
  /** 状态（0=正常 1=停用） */
  status: number;
} 