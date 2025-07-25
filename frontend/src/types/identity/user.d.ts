import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { HbtRole } from '@/types/identity/role'
import type { HbtPost } from '@/types/identity/post'
import type { HbtDept } from '@/types/identity/dept'

/**
 * 用户实体
 */
export interface HbtUser extends HbtBaseEntity {
  /** 用户ID */
  userId: number
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
  /** 是否锁定（0否 1是） */
  isLock: number
  /** 错误次数限制 */
  errorLimit: number
  /** 登录次数 */
  loginCount: number
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
 * 用户创建参数
 */
export interface HbtUserCreate {
  /** 用户ID */
  userId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 密码 */
  password: string
  /** 真实姓名 */
  realName: string
  /** 全名 */
  fullName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户） */
  userType: number
  /** 邮箱 */
  email?: string
  /** 手机号码 */
  phoneNumber?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  status: number
  /** 部门ID */
  deptId: number
  /** 角色ID列表 */
  roleIds: number[]
  /** 岗位ID列表 */
  postIds: number[]
  /** 部门ID列表 */
  deptIds: number[]
  /** 租户ID列表 */
  tenantIds?: number[]
  /** 备注 */
  remark?: string
}

/**
 * 用户更新参数
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
 * 用户密码重置参数
 */
export interface HbtUserPasswordReset {
  /** 用户ID */
  userId: number
  /** 新密码 */
  newPassword: string
}

/**
 * 用户个人信息更新参数
 */
export interface HbtUserProfileUpdate {
  /** 昵称 */
  nickName: string
  /** 邮箱 */
  email: string
  /** 手机号 */
  phoneNumber: string
  /** 性别 */
  sex: number
  /** 头像 */
  avatar?: string
}

/**
 * 用户密码修改参数
 */
export interface HbtUserPasswordChange {
  /** 旧密码 */
  oldPassword: string
  /** 新密码 */
  newPassword: string
}

/**
 * 用户登录信息
 */
export interface HbtUserLoginInfo {
  /** 用户ID */
  userId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 邮箱 */
  email: string
  /** 手机号 */
  phoneNumber: string
  /** 头像 */
  avatar: string
  /** 性别 */
  sex: number
  /** 状态 */
  status: number
  /** 登录IP */
  loginIp: string
  /** 登录时间 */
  loginDate: string
  /** 角色列表 */
  roles: string[]
  /** 权限列表 */
  permissions: string[]
}

/**
 * 用户统计信息
 */
export interface HbtUserStatistics {
  /** 总用户数 */
  totalUsers: number
  /** 在线用户数 */
  onlineUsers: number
  /** 今日新增用户数 */
  todayNewUsers: number
  /** 本月新增用户数 */
  monthNewUsers: number
  /** 用户状态分布 */
  statusDistribution: {
    /** 正常用户数 */
    normal: number
    /** 停用用户数 */
    disabled: number
  }
  /** 性别分布 */
  sexDistribution: {
    /** 男性用户数 */
    male: number
    /** 女性用户数 */
    female: number
    /** 未知性别用户数 */
    unknown: number
  }
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
  nickName: string
  /** 真实姓名 */
  realName: string
  /** 全名 */
  fullName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型(0=系统用户,1=普通用户) */
  userType: number
  /** 手机号码 */
  phoneNumber: string
  /** 邮箱 */
  email: string
  /** 性别(0=未知,1=男,2=女) */
  gender: number
  /** 头像 */
  avatar: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 用户导出参数
 */
export interface HbtUserExport {
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 真实姓名 */
  realName: string
  /** 全名 */
  fullName: string
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
  /** 部门名称 */
  deptName: string
  /** 角色名称列表 */
  roleNames: string
  /** 岗位名称列表 */
  postNames: string
  /** 状态 */
  status: number
  /** 创建时间 */
  createTime: string
}

/**
 * 用户分页结果
 */
export interface HbtUserPagedResult extends HbtPagedResult<HbtUser> {}

/**
 * 用户DTO
 */
export interface HbtUserDto {
  /** 用户ID */
  userId: number
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 真实姓名 */
  realName: string
  /** 全名 */
  fullName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户） */
  userType: number
  /** 邮箱 */
  email?: string
  /** 手机号码 */
  phoneNumber?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  status: number
  /** 租户ID */
  tenantId: number
  /** 租户名称 */
  tenantName: string
  /** 角色列表 */
  roles: string[]
  /** 权限列表 */
  permissions: string[]
  /** 锁定状态（0正常 1临时锁定30分钟 2永久锁定需要人工干预） */
  isLock: number
  /** 错误次数限制（0是3次 1是5次） */
  errorLimit: number
  /** 登录次数 */
  loginCount: number
  /** 角色ID列表 */
  roleIds: number[]
  /** 岗位ID列表 */
  postIds: number[]
  /** 部门ID列表 */
  deptIds: number[]
  /** 备注 */
  remark?: string
  /** 创建者 */
  createBy?: string
  /** 创建时间 */
  createTime: string
  /** 更新者 */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
  /** 是否删除（0未删除 1已删除） */
  isDeleted: number
  /** 删除者 */
  deleteBy?: string
  /** 删除时间 */
  deleteTime?: string
}

/**
 * 用户角色DTO
 */
export interface HbtUserRoleDto {
  /** ID */
  id: number
  /** 用户ID */
  userId: number
  /** 角色ID */
  roleId: number
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
}

/**
 * 用户部门DTO
 */
export interface HbtUserDeptDto {
  /** ID */
  id: number
  /** 用户ID */
  userId: number
  /** 部门ID */
  deptId: number
  /** 部门名称 */
  deptName: string
  /** 部门编码 */
  deptCode: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
}

/**
 * 用户岗位DTO
 */
export interface HbtUserPostDto {
  /** ID */
  id: number
  /** 用户ID */
  userId: number
  /** 岗位ID */
  postId: number
  /** 岗位名称 */
  postName: string
  /** 岗位编码 */
  postCode: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
}

/**
 * 用户表单类型
 */
export interface HbtUserForm {
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 真实姓名 */
  realName: string
  /** 全名 */
  fullName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户） */
  userType: number
  /** 密码 */
  password?: string
  /** 邮箱 */
  email?: string
  /** 手机号码 */
  phoneNumber?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
  /** 角色ID列表 */
  roleIds: number[]
  /** 岗位ID列表 */
  postIds: number[]
  /** 部门ID列表 */
  deptIds: number[]
}