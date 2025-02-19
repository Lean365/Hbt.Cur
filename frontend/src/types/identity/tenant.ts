// 租户查询参数
export interface TenantQuery {
  pageNum?: number;
  pageSize?: number;
  tenantName?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 租户对象
export interface Tenant {
  tenantId: number;
  tenantName: string;
  domain: string;
  linkman: string;
  contactNumber: string;
  address: string;
  licenseNumber: string;
  licenseDate: string;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建租户参数
export interface TenantCreate {
  tenantName: string;
  domain: string;
  linkman: string;
  contactNumber: string;
  address?: string;
  licenseNumber?: string;
  licenseDate?: string;
  status: string;
  remark?: string;
}

// 更新租户参数
export interface TenantUpdate extends TenantCreate {
  tenantId: number;
}

// 租户状态更新参数
export interface TenantStatus {
  tenantId: number;
  status: string;
} 