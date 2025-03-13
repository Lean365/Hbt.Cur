import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'
import type { Role } from '@/types/identity/role'
import type { Post } from '@/types/identity/post'

/**
 * 用户查询参数
 */
export interface UserQuery extends HbtPageQuery {
  userName?: string;
  phoneNumber?: string;
  status?: number;
  deptId?: number;
  userType?: number;
}

/**
 * 用户信息
 */
export interface User extends HbtBaseEntity {
  userId: number;
  deptId: number;
  userName: string;
  nickName: string;
  englishName?: string;
  userType: number;
  password?: string;
  phoneNumber?: string;
  email?: string;
  sex: number;
  avatar?: string;
  status: number;
  tenantId: number;
  tenantName?: string;
  remark?: string;
  roleIds?: number[];
  postIds?: number[];
}

/**
 * 用户表单数据
 */
export interface UserForm {
  userId?: number;
  deptId?: number;
  userName: string;
  nickName: string;
  englishName?: string;
  password?: string;
  phoneNumber?: string;
  email?: string;
  sex: number;
  avatar?: string;
  status: number;
  remark?: string;
  roleIds?: number[];
  postIds?: number[];
}

/**
 * 重置密码表单
 */
export interface ResetPwdForm {
  userId: number;
  password: string;
}

/**
 * 创建用户参数
 */
export interface UserCreate {
  deptId: number;
  userName: string;
  nickName: string;
  password: string;
  phoneNumber?: string;
  email?: string;
  sex?: number;
  status: number;
  remark?: string;
  roleIds?: number[];
  postIds?: number[];
}

/**
 * 更新用户参数
 */
export interface UserUpdate extends Omit<UserCreate, 'password'> {
  userId: number;
}

/**
 * 重置密码参数
 */
export interface ResetPassword {
  userId: number;
  password: string;
}

/**
 * 修改密码参数
 */
export interface ChangePassword {
  oldPassword: string;
  newPassword: string;
}

/**
 * 用户状态更新参数
 */
export interface UserStatus {
  userId: number;
  status: number;
}

/**
 * 导入用户结果
 */
export interface ImportResult {
  total: number;
  success: number;
  failed: number;
  message: string;
} 