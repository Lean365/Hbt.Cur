import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

// 角色查询参数
export interface RoleQuery extends HbtPageQuery {
  roleName?: string;
  roleKey?: string;
  status?: number;
  beginTime?: string;
  endTime?: string;
}

// 角色对象
export interface Role extends HbtBaseEntity {
  roleId: number;
  roleName: string;
  roleKey: string;
  orderNum: number;
  dataScope: number;
  status: number;
  tenantId: number;
  remark: string;
  menuIds?: number[];
  deptIds?: number[];
}

// 创建角色参数
export interface RoleCreate {
  roleName: string;
  roleKey: string;
  orderNum: number;
  dataScope: number;
  status: number;
  tenantId: number;
  remark?: string;
  menuIds?: number[];
  deptIds?: number[];
}

// 更新角色参数
export interface RoleUpdate extends RoleCreate {
  roleId: number;
}

// 角色状态更新参数
export interface RoleStatus {
  roleId: number;
  status: number;
} 