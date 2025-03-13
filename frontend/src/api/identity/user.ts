import request from '@/utils/request'
import type { UserQuery, User, UserForm, UserStatus, ResetPassword, ChangePassword } from '@/types/identity/user'
import type { HbtPagedResult } from '@/types/common'

// 获取用户分页列表
export function getPagedList(query: UserQuery) {
  return request<HbtPagedResult<User>>({
    url: '/api/hbtuser',
    method: 'get',
    params: query
  })
}

// 获取用户详情
export function getUser(userId: number) {
  return request<User>({
    url: `/api/hbtuser/${userId}`,
    method: 'get'
  })
}

// 创建用户
export function createUser(data: UserForm) {
  return request<number>({
    url: '/api/hbtuser',
    method: 'post',
    data
  })
}

// 更新用户
export function updateUser(data: UserForm) {
  return request<boolean>({
    url: '/api/hbtuser',
    method: 'put',
    data
  })
}

// 删除用户
export function deleteUser(userId: number) {
  return request<boolean>({
    url: `/api/hbtuser/${userId}`,
    method: 'delete'
  })
}

// 批量删除用户
export function batchDeleteUser(userIds: number[]) {
  return request<boolean>({
    url: '/api/hbtuser/batch',
    method: 'delete',
    data: userIds
  })
}

// 导入用户
export function importUser(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/hbtuser/import',
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
    url: '/api/hbtuser/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getTemplate() {
  return request<Blob>({
    url: '/api/hbtuser/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新用户状态
export function updateUserStatus(data: UserStatus) {
  return request<boolean>({
    url: `/api/hbtuser/${data.userId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 重置密码
export function resetPassword(data: ResetPassword) {
  return request<boolean>({
    url: '/api/hbtuser/reset-password',
    method: 'put',
    data
  })
}

// 修改密码
export function changePassword(data: ChangePassword) {
  return request<boolean>({
    url: '/api/hbtuser/change-password',
    method: 'put',
    data
  })
}
