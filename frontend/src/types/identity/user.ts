import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'
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
  remark: string;
}

/**
 * 用户查询参数
 */
export interface UserQuery extends HbtPageQuery {
  userName?: string;
  nickName?: string;
  phoneNumber?: string;
  email?: string;
  gender?: number;
  status?: number;
  userType?: number;
  deptId?: number;
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
 * 用户表单数据
 */
export interface UserForm {
  userId?: number
  tenantId: number
  userName: string
  nickName: string
  englishName?: string
  userType: number
  password?: string
  email?: string
  phoneNumber?: string
  gender: number
  avatar?: string
  status: number
  remark?: string
  roleIds?: number[]
  postIds?: number[]
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