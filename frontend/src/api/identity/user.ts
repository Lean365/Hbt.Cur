import request from '@/utils/request'
import type { HbtUserQuery, HbtUser, HbtUserStatus, HbtUserPasswordReset, HbtUserUpdate, HbtUserCreate, HbtUserRoleDto, HbtUserDeptDto, HbtUserPostDto } from '@/types/identity/user'
import type { HbtPagedResult } from '@/types/common'
import type { HbtApiResponse } from '@/types/common'
import { useUserStore } from '@/stores/user'
import { AxiosResponse } from 'axios'

// 获取用户分页列表
export function getUserList(query: HbtUserQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtUser>>>({
    url: '/api/HbtUser/list',
    method: 'get',
    params: query
  })
}

// 获取用户详情
export function getUser(userId: number) {
  return request<HbtApiResponse<HbtUser>>({
    url: `/api/HbtUser/${userId}`,
    method: 'get'
  })
}

// 创建用户
export function createUser(data: HbtUserCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtUser',
    method: 'post',
    data
  })
}

// 更新用户
export function updateUser(data: HbtUserUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtUser',
    method: 'put',
    data
  })
}

// 删除用户
export function deleteUser(userId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${userId}`,
    method: 'delete'
  })
}

// 批量删除用户
export function batchDeleteUser(userIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtUser/batch',
    method: 'delete',
    data: userIds
  })
}

// 导入用户
export function importUser(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtUser/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出用户
export function exportUser(query: HbtUserQuery) {
  return request<Blob>({
    url: '/api/HbtUser/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getTemplate() {
  return request<Blob>({
    url: '/api/HbtUser/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新用户状态
export function updateUserStatus(data: HbtUserStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${data.userId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 重置密码
export function resetPassword(data: HbtUserPasswordReset) {
  return request<boolean>({
    url: '/api/HbtUser/reset-password',
    method: 'put',
    data
  })
}

// 修改密码
export function changePassword(data: { oldPassword: string; newPassword: string }) {
  return request<boolean>({
    url: '/api/HbtUser/change-password',
    method: 'put',
    data
  })
}

// 更新用户密码
export function updateUserPassword(data: { userId: number; password: string }) {
  return request({
    url: `/api/HbtUser/${data.userId}/password`,
    method: 'put',
    data: {
      password: data.password
    }
  })
}

/**
 * 个人资料更新参数
 */
export interface ProfileUpdate {
  userId: number
  nickName: string
  englishName?: string
  email?: string
  phoneNumber?: string
  gender?: number
  oldPassword?: string
  newPassword?: string
}

/**
 * 更新个人资料
 * @param data 个人资料数据
 */
export function updateProfile(data: ProfileUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtAuth/profile',
    method: 'put',
    data
  })
}

// 搜索用户
export function searchUser(query: { keyword: string }) {
  return request<HbtApiResponse<HbtPagedResult<HbtUser>>>({
    url: '/api/HbtUser',
    method: 'get',
    params: {
      userName: query.keyword,
      pageSize: 10,
      pageIndex: 1
    }
  })
}

// 获取用户角色列表
export function getUserRoles(userId: number) {
  return request<HbtApiResponse<HbtUserRoleDto[]>>({
    url: `/api/HbtUser/${userId}/roles`,
    method: 'get'
  })
}

/**
 * 获取用户部门列表
 * @param userId 用户ID
 */
export function getUserDepts(userId: number) {
  return request<HbtApiResponse<HbtUserDeptDto[]>>({
    url: `/api/HbtUser/${userId}/depts`,
    method: 'get'
  })
}

/**
 * 分配用户部门
 * @param userId 用户ID
 * @param deptIds 部门ID列表
 */
export function allocateUserDepts(userId: number, deptIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${userId}/depts`,
    method: 'put',
    data: deptIds
  })
}

/**
 * 获取用户岗位列表
 * @param userId 用户ID
 */
export function getUserPosts(userId: number) {
  return request<HbtApiResponse<HbtUserPostDto[]>>({
    url: `/api/HbtUser/${userId}/posts`,
    method: 'get'
  })
}

/**
 * 分配用户岗位
 * @param userId 用户ID
 * @param postIds 岗位ID列表
 */
export function allocateUserPosts(userId: number, postIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${userId}/posts`,
    method: 'put',
    data: postIds
  })
}

/**
 * 获取用户选项列表
 */
export function getUserOptions() {
  return request<HbtApiResponse<{ label: string; value: number }[]>>({
    url: '/api/HbtUser/options',
    method: 'get'
  })
}

/**
 * 获取用户租户列表
 * @param userId 用户ID
 */
export function getUserTenants(userId: number) {
  return request<HbtApiResponse<any[]>>({
    url: `/api/HbtUser/${userId}/tenants`,
    method: 'get'
  })
}

/**
 * 分配用户租户
 * @param userId 用户ID
 * @param configIds 租户配置ID列表
 */
export function allocateUserTenants(userId: number, configIds: string[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${userId}/tenants`,
    method: 'put',
    data: configIds
  })
}
