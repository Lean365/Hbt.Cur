// 角色查询参数
export interface RoleQuery {
  pageNum?: number;
  pageSize?: number;
  roleName?: string;
  roleKey?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 角色对象
export interface Role {
  roleId: number;
  roleName: string;
  roleKey: string;
  roleSort: number;
  dataScope: string;
  menuCheckStrictly: boolean;
  deptCheckStrictly: boolean;
  status: string;
  delFlag: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建角色参数
export interface RoleCreate {
  roleName: string;
  roleKey: string;
  roleSort: number;
  dataScope: string;
  menuCheckStrictly: boolean;
  deptCheckStrictly: boolean;
  status: string;
  remark?: string;
}

// 更新角色参数
export interface RoleUpdate extends RoleCreate {
  roleId: number;
}

// 角色状态更新参数
export interface RoleStatus {
  roleId: number;
  status: string;
} 