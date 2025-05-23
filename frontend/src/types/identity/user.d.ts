import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtRole } from '@/types/identity/role'
import type { HbtPost } from '@/types/identity/post'
import type { HbtDept } from '@/types/identity/dept'

/**
 * 用户对象
 */
export interface HbtUser extends HbtBaseEntity {
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName?: string
  /** 全名 */
  fullName?: string
  /** 真实姓名 */
  realName?: string
  /** 英文名称 */
  englishName?: string
  /** 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户） */
  userType: number
  /** 密码 */
  password: string
  /** 盐值 */
  salt: string
  /** 密码迭代次数 */
  iterations: number
  /** 邮箱 */
  email?: string
  /** 手机号 */
  phoneNumber?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  status: number
  /** 最后修改密码时间 */
  lastPasswordChangeTime?: string
  /** 锁定结束时间 */
  lockEndTime?: string
  /** 锁定原因 */
  lockReason?: string
  /** 部门ID列表 */
  deptIds?: number[]
  /** 岗位ID列表 */
  postIds?: number[]
  /** 角色ID列表 */
  roleIds?: number[]
}

/**
 * 用户查询参数
 */
export interface HbtUserQuery extends HbtPagedQuery {
  /** 用户名 */
  userName?: string
  /** 昵称 */
  nickName?: string
  /** 手机号码 */
  phoneNumber?: string
  /** 邮箱 */
  email?: string
  /** 状态（0正常 1停用） */
  status?: number
  /** 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户） */
  userType?: number
  /** 性别（0未知 1男 2女） */
  gender?: number
  /** 部门ID */
  deptId?: number
}

/**
 * 创建用户参数
 */
export interface HbtUserCreate {
  /** 租户ID */
  tenantId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName?: string
  /** 全名 */
  fullName?: string
  /** 真实姓名 */
  realName?: string
  /** 英文名称 */
  englishName?: string
  /** 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户） */
  userType: number
  /** 密码 */
  password: string
  /** 邮箱 */
  email?: string
  /** 手机号 */
  phoneNumber?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
  /** 部门ID列表 */
  deptIds?: number[]
  /** 岗位ID列表 */
  postIds?: number[]
  /** 角色ID列表 */
  roleIds?: number[]
}

/**
 * 更新用户参数
 */
export interface HbtUserUpdate extends HbtUserCreate {
  /** 用户ID */
  userId: number
}

/**
 * 用户状态更新参数
 */
export interface HbtUserStatus {
  /** 用户ID */
  userId: number
  /** 状态（0正常 1停用） */
  status: number
}

/** 
 * 用户密码更新参数
 */
export interface HbtUserPassword {
  /** 用户ID */
  userId: number  
  /** 旧密码 */
  oldPassword: string
  /** 新密码 */
  newPassword: string
  /** 确认密码 */
  confirmPassword: string
}

/**
 * 重置密码参数
 */
export interface HbtUserResetPwd {
  /** 用户ID */
  userId: number
  /** 新密码 */
  password: string
}

/**
 * 用户导入模板
 */
export interface HbtUserTemplate {
  /** 租户ID */
  tenantId: string
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 全名 */
  fullName: string
  /** 真实姓名 */
  realName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型 */
  userType: string
  /** 手机号码 */
  phoneNumber: string
  /** 邮箱 */
  email: string
  /** 性别 */
  gender: string
  /** 头像 */
  avatar: string
  /** 状态 */
  status: string
  /** 备注 */
  remark: string
}

/**
 * 用户导入参数
 */
export interface HbtUserImport {
  /** 租户ID */
  tenantId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName?: string
  /** 全名 */
  fullName?: string
  /** 真实姓名 */
  realName?: string
  /** 英文名称 */
  englishName?: string
  /** 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户） */
  userType: number
  /** 手机号码 */
  phoneNumber?: string
  /** 邮箱 */
  email?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 用户导出参数
 */
export interface HbtUserExport {
  /** 租户ID */
  tenantId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 全名 */
  fullName: string
  /** 真实姓名 */
  realName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型 */
  userType: number
  /** 手机号码 */
  phoneNumber: string
  /** 邮箱 */
  email: string
  /** 性别 */
  gender: number
  /** 头像 */
  avatar: string
  /** 状态 */
  status: number
  /** 备注 */
  remark: string
  /** 创建时间 */
  createTime: string
}

/**
 * 用户分页结果
 */
export type HbtUserPageResult = HbtPagedResult<HbtUser>

/**
 * 用户DTO
 */
export interface HbtUserDto {
  /** 用户ID */
  userId: number
  /** 租户ID */
  tenantId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 全名 */
  fullName: string
  /** 真实姓名 */
  realName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型 */
  userType: number
  /** 手机号码 */
  phoneNumber: string
  /** 邮箱 */
  email: string
  /** 性别 */
  gender: number
  /** 头像 */
  avatar: string
  /** 状态 */
  status: number
  /** 备注 */
  remark: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
  /** 更新时间 */
  updateTime: string
  /** 更新者 */
  updateBy: string
  /** 角色列表 */
  roles: string[]
  /** 权限列表 */
  permissions: string[]
  /** 角色ID列表 */
  roleIds: number[]
  /** 岗位ID列表 */
  postIds: number[]
  /** 部门ID列表 */
  deptIds: number[]
}

/**
 * 用户角色DTO
 */
export interface HbtUserRoleDto {
  id: number;
  userId: number;
  roleId: number;
  roleName: string;
  roleKey: string;
  createTime: string;
  createBy: string;
}

/**
 * 用户部门DTO
 */
export interface HbtUserDeptDto {
  id: number;
  userId: number;
  deptId: number;
  deptName: string;
  deptCode: string;
  createTime: string;
  createBy: string;
}

/**
 * 用户岗位DTO
 */
export interface HbtUserPostDto {
  id: number;
  userId: number;
  postId: number;
  postName: string;
  postCode: string;
  createTime: string;
  createBy: string;
}

/**
 * 用户表单类型
 */
export interface HbtUserForm {
  tenantId: number;
  userName: string;
  nickName: string;
  fullName: string;
  realName: string;
  englishName: string;
  userType: number;
  password?: string;
  email: string;
  phoneNumber: string;
  gender: number;
  avatar: string;
  status: number;
  remark: string;
  roleIds: number[];
  postIds: number[];
  deptIds: number[];
}