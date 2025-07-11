import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { Dayjs } from 'dayjs'

/**
 * 租户实体
 */
export interface HbtTenant extends HbtBaseEntity {
  /** 租户ID */
  tenantId: number
  /** 租户名称 */
  tenantName: string
  /** 租户编码 */
  tenantCode: string
  /** 联系人 */
  contactUser?: string
  /** 联系电话 */
  contactPhone?: string
  /** 联系邮箱 */
  contactEmail: string
  /** 联系地址 */
  address?: string
  /** 许可证类型 */
  licenseType?: string
  /** 许可注册码 */
  licenseKey?: string
  /** 最大用户数 */
  maxUserCount: number
  /** 过期时间 */
  expireTime?: string
  /** 许可证开始时间 */
  licenseStartTime?: string
  /** 许可证结束时间 */
  licenseEndTime?: string
  /** 租户域名 */
  domain: string
  /** 租户Logo */
  logoUrl?: string
  /** 租户主题 */
  theme?: string
  /** 租户配置ID */
  configId: string
  /** 租户状态（0正常 1停用） */
  status: number
}

/**
 * 租户查询参数
 */
export interface HbtTenantQuery extends HbtPagedQuery {
  /** 租户配置ID */
  configId?: string
  /** 租户编码 */
  tenantCode?: string
  /** 租户名称 */
  tenantName?: string
  /** 联系人 */
  contactUser?: string
  /** 联系电话 */
  contactPhone?: string
  /** 状态 */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 租户创建参数
 */
export interface HbtTenantCreate {
    /** 租户ID */
  tenantId: number
  /** 租户配置ID */
  configId: string
  /** 租户名称 */
  tenantName: string
  /** 租户编码 */
  tenantCode: string
  /** 联系人 */
  contactUser: string
  /** 联系电话 */
  contactPhone: string
  /** 联系邮箱 */
  contactEmail: string
  /** 联系地址 */
  address?: string
  /** 许可证类型 */
  licenseType?: string
  /** 许可注册码 */
  licenseKey?: string
  /** 过期时间 */
  expireTime?: string
  /** 租户域名 */
  domain: string
  /** 租户Logo */
  logoUrl?: string
  /** 租户主题 */
  theme?: string
  /** 许可证开始时间 */
  licenseStartTime?: string
  /** 许可证结束时间 */
  licenseEndTime?: string
  /** 最大用户数 */
  maxUserCount: number
  /** 状态 */
  status: number
}

/**
 * 租户更新参数
 */
export interface HbtTenantUpdate extends HbtTenantCreate {
  /** 租户ID */
  tenantId: number
}

/**
 * 租户状态更新参数
 */
export interface HbtTenantStatus {
  /** 租户配置ID */
  configId: string
  /** 租户ID */
  tenantId: number
  /** 状态 */
  status: number
  /** 状态名称 */
  statusName: string
}

/**
 * 租户导入模板
 */
export interface HbtTenantTemplate {
  /** 租户配置ID */
  configId: string
  /** 租户编码 */
  tenantCode: string
  /** 租户名称 */
  tenantName: string
  /** 联系人 */
  contactUser: string
  /** 联系电话 */
  contactPhone: string
  /** 联系邮箱 */
  contactEmail: string
  /** 联系地址 */
  address: string
  /** 许可证类型 */
  licenseType: string
  /** 许可注册码 */
  licenseKey: string
  /** 租户域名 */
  domain: string
  /** 租户主题 */
  theme: string
  /** 许可证开始时间 */
  licenseStartTime: string
  /** 许可证结束时间 */
  licenseEndTime: string
  /** 最大用户数 */
  maxUserCount: string
}

/**
 * 租户导入参数
 */
export interface HbtTenantImport {
  /** 租户配置ID */
  configId: string
  /** 租户名称 */
  tenantName: string
  /** 租户编码 */
  tenantCode: string
  /** 联系人 */
  contactUser?: string
  /** 联系电话 */
  contactPhone?: string
  /** 联系邮箱 */
  contactEmail?: string
  /** 联系地址 */
  address?: string
  /** 许可证类型 */
  licenseType?: string
  /** 许可注册码 */
  licenseKey?: string
  /** 租户域名 */
  domain?: string
}

/**
 * 租户导出参数
 */
export interface HbtTenantExport {
  /** 租户配置ID */
  configId: string
  /** 租户名称 */
  tenantName: string
  /** 租户编码 */
  tenantCode: string
  /** 联系人 */
  contactUser: string
  /** 联系电话 */
  contactPhone: string
  /** 联系邮箱 */
  contactEmail: string
  /** 联系地址 */
  address: string
  /** 许可证类型 */
  licenseType: string
  /** 许可注册码 */
  licenseKey: string
  /** 租户域名 */
  domain: string
  /** 创建时间 */
  createTime: string
}

/**
 * 租户分页结果
 */
export interface HbtTenantPagedResult extends HbtPagedResult<HbtTenant> {}

/**
 * 租户DTO
 */
export interface HbtTenantDto {
  /** 租户ID */
  tenantId: number
  /** 租户配置ID */
  configId: string
  /** 租户名称 */
  tenantName: string
  /** 租户编码 */
  tenantCode: string
  /** 联系人 */
  contactUser: string
  /** 联系电话 */
  contactPhone: string
  /** 联系邮箱 */
  contactEmail: string
  /** 联系地址 */
  address?: string
  /** 许可证类型 */
  licenseType?: string
  /** 许可注册码 */
  licenseKey?: string
  /** 过期时间 */
  expireTime?: string
  /** 租户域名 */
  domain: string
  /** 租户Logo */
  logoUrl?: string
  /** 租户主题 */
  theme?: string
  /** 许可证开始时间 */
  licenseStartTime?: string
  /** 许可证结束时间 */
  licenseEndTime?: string
  /** 最大用户数 */
  maxUserCount: number
  /** 状态 */
  status: number
  /** 备注 */
  remark?: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
  /** 更新时间 */
  updateTime: string
  /** 更新者 */
  updateBy: string
  /** 是否删除 */
  isDeleted: number
  /** 删除时间 */
  deleteTime?: string
  /** 删除者 */
  deleteBy?: string
}

/**
 * 租户选项
 */
export interface HbtTenantOption {
  /** 标签 */
  label: string
  /** 值 */
  value: string
}

