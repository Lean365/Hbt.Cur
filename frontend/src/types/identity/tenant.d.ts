import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 租户查询参数
 */
export interface TenantQuery extends HbtPagedQuery {
  /** 租户编码 */
  tenantCode?: string
  /** 租户名称 */
  tenantName?: string
  /** 联系人 */
  contactPerson?: string
  /** 联系电话 */
  contactPhone?: string
  /** 状态（0=正常 1=停用） */
  status?: number
}

/**
 * 租户对象
 */
export interface Tenant extends HbtBaseEntity {
  /** 租户ID */
  tenantId: number
  /** 租户编码 */
  tenantCode: string
  /** 租户名称 */
  tenantName: string
  /** 联系人 */
  contactPerson: string
  /** 联系电话 */
  contactPhone: string
  /** 联系邮箱 */
  contactEmail: string
  /** 租户地址 */
  address: string
  /** 租户域名 */
  domain: string
  /** 租户Logo */
  logoUrl: string
  /** 租户主题 */
  theme: string
  /** 授权开始时间 */
  licenseStartTime?: string
  /** 授权结束时间 */
  licenseEndTime?: string
  /** 最大用户数 */
  maxUserCount: number
  /** 状态（0=正常 1=停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 租户创建参数
 */
export interface TenantCreate {
  /** 租户编码 */
  tenantCode: string
  /** 租户名称 */
  tenantName: string
  /** 联系人 */
  contactPerson: string
  /** 联系电话 */
  contactPhone: string
  /** 联系邮箱 */
  contactEmail: string
  /** 租户地址 */
  address: string
  /** 租户域名 */
  domain: string
  /** 租户Logo */
  logoUrl: string
  /** 租户主题 */
  theme: string
  /** 授权开始时间 */
  licenseStartTime?: string
  /** 授权结束时间 */
  licenseEndTime?: string
  /** 最大用户数 */
  maxUserCount: number
  /** 状态（0=正常 1=停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 租户更新参数
 */
export interface TenantUpdate extends TenantCreate {
  /** 租户ID */
  tenantId: number
}

/**
 * 租户状态参数
 */
export interface TenantStatus {
  /** 租户ID */
  tenantId: number
  /** 状态（0=正常 1=停用） */
  status: number
}

/**
 * 租户分页结果
 */
export type TenantPageResult = HbtPagedResult<Tenant> 