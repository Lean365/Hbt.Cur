import type { Role } from './role';
import type { BaseEntity } from '../base';

/**
 * 用户查询参数
 */
export interface UserQuery {
  pageNum: number;
  pageSize: number;
  userName?: string;
  nickName?: string;
  phoneNumber?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

/**
 * 用户信息
 */
export interface User extends BaseEntity {
  userId: number;
  deptId: number;
  userName: string;
  nickName: string;
  userType: string;
  email: string;
  phoneNumber: string;
  sex: string;
  avatar: string;
  password: string;
  status: string;
  delFlag: string;
  loginIp: string;
  loginDate: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
  roles?: Role[];
  posts?: Post[];
  roleIds?: number[];
  postIds?: number[];
  deptName?: string;
}

/**
 * 角色信息
 */
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

/**
 * 岗位信息
 */
export interface Post {
  postId: number;
  postCode: string;
  postName: string;
  postSort: number;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

/**
 * 用户表单数据
 */
export interface UserForm {
  userId?: number;
  deptId?: number;
  userName: string;
  nickName: string;
  password?: string;
  phoneNumber?: string;
  email?: string;
  sex?: string;
  status?: string;
  remark?: string;
  postIds?: number[];
  roleIds?: number[];
}

/**
 * 重置密码表单
 */
export interface ResetPwdForm {
  userId: number;
  password: string;
}

// 创建用户参数
export interface UserCreate {
  deptId: number;
  userName: string;
  nickName: string;
  password: string;
  phoneNumber?: string;
  email?: string;
  sex?: string;
  status: string;
  remark?: string;
  roleIds?: number[];
  postIds?: number[];
}

// 更新用户参数
export interface UserUpdate extends Omit<UserCreate, 'password'> {
  userId: number;
}

// 重置密码参数
export interface ResetPassword {
  userId: number;
  password: string;
}

// 修改密码参数
export interface ChangePassword {
  oldPassword: string;
  newPassword: string;
}

// 用户状态更新参数
export interface UserStatus {
  userId: number;
  status: string;
}

// 导入用户结果
export interface ImportResult {
  total: number;
  success: number;
  failed: number;
  message: string;
} 