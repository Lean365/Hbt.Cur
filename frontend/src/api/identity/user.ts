import request from '@/utils/request'
import type { UserQuery, User, UserStatus, ResetPassword, ChangePassword, UserUpdate,UserCreate } from '@/types/identity/user'
import type { HbtPagedResult } from '@/types/common'
import type { HbtApiResponse } from '@/types/common'
import { useUserStore } from '@/stores/user'

// 获取用户分页列表
export function getPagedList(query: UserQuery) {
  return request<HbtApiResponse<HbtPagedResult<User>>>({
    url: '/api/HbtUser',
    method: 'get',
    params: query
  })
}

// 获取用户详情
export function getUser(userId: number) {
  return request<HbtApiResponse<User>>({
    url: `/api/HbtUser/${userId}`,
    method: 'get'
  })
}

// 创建用户
export function createUser(data: UserCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtUser',
    method: 'post',
    data
  })
}

// 更新用户
export function updateUser(data: UserUpdate) {
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
  return request<{ success: number; fail: number }>({
    url: '/api/HbtUser/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出用户
export function exportUser(query: UserQuery) {
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
export function updateUserStatus(data: UserStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${data.userId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 重置密码
export function resetPassword(data: ResetPassword) {
  return request<boolean>({
    url: '/api/HbtUser/reset-password',
    method: 'put',
    data
  })
}

// 修改密码
export function changePassword(data: ChangePassword) {
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
  return request<HbtApiResponse<HbtPagedResult<User>>>({
    url: '/api/HbtUser',
    method: 'get',
    params: {
      userName: query.keyword,
      pageSize: 10,
      pageIndex: 1
    }
  })
}
