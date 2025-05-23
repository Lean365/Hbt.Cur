import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 角色对象
 */
export interface HbtRole extends HbtBaseEntity {
  /** 角色ID */
  roleId: number
  /** 租户ID */
  tenantId: number
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 排序号 */
  orderNum: number
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number;
  /** 用户数 */
  userCount: number;
  /** 状态（0正常 1停用） */
  status: number;

  /** 菜单ID列表 */
  menuIds?: number[];
  /** 部门ID列表 */
  deptIds?: number[];
}

/**
 * 角色查询参数
 */
export interface HbtRoleQuery extends HbtPagedQuery {
  /** 角色名称 */
  roleName?: string;
  /** 角色标识 */
  roleKey?: string;
  /** 状态（0正常 1停用） */
  status?: number;
}

/**
 * 创建角色参数
 */
export interface HbtRoleCreate {
  /** 角色名称 */
  roleName: string;
  /** 角色标识 */
  roleKey: string;
  /** 排序号 */
  orderNum: number;
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number;
  /** 用户数 */
  userCount: number;
  /** 状态（0正常 1停用） */
  status: number;
  /** 租户ID */
  tenantId: number;
  /** 备注 */
  remark?: string;
  /** 菜单ID列表 */
  menuIds?: number[];
  /** 部门ID列表 */
  deptIds?: number[];
}

/**
 * 更新角色参数
 */
export interface HbtRoleUpdate extends HbtRoleCreate {
  /** 角色ID */
  roleId: number;
}

/**
 * 角色状态更新参数
 */
export interface HbtRoleStatus {
  /** 角色ID */
  roleId: number;
  /** 状态（0正常 1停用） */
  status: number;
}

/**
 * 角色导入模板
 */
export interface HbtRoleTemplate {
  /** 角色名称 */
  roleName: string;
  /** 角色标识 */
  roleKey: string;
  /** 排序号 */
  orderNum: string;
  /** 数据范围 */
  dataScope: string;
  /** 状态 */
  status: string;
  /** 备注 */
  remark: string;
  /** 菜单名称列表 */
  menuNames: string;
  /** 部门名称列表 */
  deptNames: string;
}

/**
 * 角色导入参数
 */
export interface HbtRoleImport {
  /** 角色名称 */
  roleName: string;
  /** 角色标识 */
  roleKey: string;
  /** 排序号 */
  orderNum: number;
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number;
  /** 状态（0正常 1停用） */
  status: number;
  /** 备注 */
  remark?: string;
  /** 菜单名称列表 */
  menuNames?: string;
  /** 部门名称列表 */
  deptNames?: string;
}

/**
 * 角色导出参数
 */
export interface HbtRoleExport {
  /** 角色名称 */
  roleName: string;
  /** 角色标识 */
  roleKey: string;
  /** 排序号 */
  orderNum: number;
  /** 数据范围 */
  dataScope: number;
  /** 用户数 */
  userCount: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark: string;
  /** 创建时间 */
  createTime: string;
}

/**
 * 角色分页结果
 */
export type HbtRolePageResult = HbtPagedResult<HbtRole>

/**
 * 角色DTO
 */
export interface HbtRoleDto {
  /** 角色ID */
  roleId: number;
  /** 角色名称 */
  roleName: string;
  /** 角色标识 */
  roleKey: string;
  /** 排序号 */
  orderNum: number;
  /** 数据范围 */
  dataScope: number;
  /** 用户数 */
  userCount: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark: string;
  /** 创建时间 */
  createTime: string;
  /** 创建者 */
  createBy: string;
  /** 更新时间 */
  updateTime: string;
  /** 更新者 */
  updateBy: string;
  /** 菜单ID列表 */
  menuIds: number[];
  /** 部门ID列表 */
  deptIds: number[];
}

/**
 * 角色选项
 */
export interface HbtRoleOption {
  /** 标签 */
  label: string;
  /** 值 */
  value: number;
}

/**
 * 角色部门DTO
 */
export interface HbtRoleDeptDto {
  id: number;
  roleId: number;
  deptId: number;
  deptName: string;
  deptCode: string;
  createTime: string;
  createBy: string;
}

/**
 * 角色菜单DTO
 */
export interface HbtRoleMenuDto {
  id: number;
  roleId: number;
  menuId: number;
  menuName: string;
  menuCode: string;
  createTime: string;
  createBy: string;
} 