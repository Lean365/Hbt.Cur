import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { Role } from '@/types/identity/role'
import type { Post } from '@/types/identity/post'

/**
 * 用户基础类型
 */
export interface User extends HbtBaseEntity {
  userId: number;
  tenantId: number;
  userName: string;
  nickName: string;
  englishName: string;
  userType: number;
  email: string;
  phoneNumber: string;
  gender: number;
  avatar: string;
  status: number;
  lastPasswordChangeTime: string;  
  lockEndTime?: string;
  lockReason?: string;
  isLock?: number;
  errorLimit?: number;
  loginCount?: number;
}

/**
 * 用户查询参数
 */
export interface UserQuery extends HbtPagedQuery {
  userName?: string;
  nickName?: string;
  phoneNumber?: string;
  email?: string;
  gender?: number;
  status?: number;
  userType?: number;
  deptId?: number;
  sheetName?: string;
}

/**
 * 用户创建参数
 */
export interface UserCreate {
  userName: string;
  nickName: string;
  englishName?: string;
  password: string;
  email?: string;
  phoneNumber?: string;
  gender: number;
  avatar?: string;
  status: number;
  roleIds: number[];
  postIds: number[];
  remark?: string;
}

/**
 * 用户更新参数
 */
export interface UserUpdate {
  userId: number;
  nickName: string;
  englishName?: string;
  email?: string;
  phoneNumber?: string;
  gender: number;
  avatar?: string;
  status: number;
  roleIds: number[];
  postIds: number[];
  deptIds: number[];
  remark?: string;
}

/**
 * 用户导入参数
 */
export interface UserImport {
  userName: string;
  nickName: string;
  englishName: string;
  userType: string;
  password: string;
  email: string;
  phoneNumber: string;
  gender: string;
  avatar: string;
  deptName: string;
  roleNames: string;
  postNames: string;
  remark: string;
}

/**
 * 用户导出参数
 */
export interface UserExport {
  userName: string;
  nickName: string;
  englishName: string;
  userType: string;
  phoneNumber: string;
  email: string;
  gender: string;
  avatar: string;
  deptName: string;
  roleNames: string;
  postNames: string;
  status: number;
  createTime: string;
}

/**
 * 重置密码参数
 */
export interface ResetPassword {
  userId: number
  password: string
}

/**
 * 修改密码参数
 */
export interface ChangePassword {
  userId: number
  oldPassword: string
  newPassword: string
}

/**
 * 用户状态更新参数
 */
export interface UserStatus {
  userId: number
  status: number
}

/**
 * 用户表单类型
 */
export interface UserForm {
  userId?: number;
  tenantId: number;
  userName: string;
  nickName: string;
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

/**
 * 用户分页结果
 */
export type UserPageResult = HbtPagedResult<User>

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