import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  UserQuery, 
  User, 
  UserCreate, 
  UserUpdate, 
  UserStatus,
  ResetPassword,
  ChangePassword,
  ImportResult,
  UserForm,
  ResetPwdForm
} from '@/types/identity/user'
import { HbtStatus } from '@/types/enums'
import type { ApiResult, PageResult } from '@/types/base'

/**
 * 查询用户列表
 * @param query 查询参数
 */
export function getUserList(query: Record<string, any>) {
  return request.get<ApiResult<PageResult<User>>>('/identity/user/list', { params: query })
}

/**
 * 查询用户详细
 * @param userId 用户ID
 */
export function getUser(userId: number) {
  return request.get<ApiResult<User>>(`/identity/user/${userId}`)
}

/**
 * 新增用户
 * @param data 用户信息
 */
export function createUser(data: UserForm) {
  return request.post<ApiResult<void>>('/identity/user', data)
}

/**
 * 修改用户
 * @param data 用户信息
 */
export function updateUser(data: UserForm & { userId: number }) {
  return request.put<ApiResult<void>>('/identity/user', data)
}

/**
 * 删除用户
 * @param userId 用户ID
 */
export function deleteUser(userId: number) {
  return request.delete<ApiResult<void>>(`/identity/user/${userId}`)
}

// 批量删除用户
export function batchDeleteUser(userIds: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/user/batch',
    method: 'delete',
    data: userIds
  })
}

// 更新用户状态
export function updateUserStatus(userId: number, status: HbtStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/user/${userId}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 重置密码
 * @param data 重置密码信息
 */
export function resetUserPassword(data: ResetPwdForm) {
  return request.put<ApiResult<void>>('/identity/user/resetPwd', data)
}

// 修改用户密码
export function changeUserPassword(data: ChangePassword) {
  return request<HbtApiResult<any>>({
    url: '/api/user/change-password',
    method: 'put',
    data
  })
}

// 导入用户
export function importUser(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<ImportResult>>({
    url: '/api/user/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出用户
 * @param query 查询参数
 */
export function exportUser(query?: Record<string, any>) {
  return request.download('/identity/user/export', query)
}

// 获取用户导入模板
export function getUserTemplate() {
  return request({
    url: '/api/user/template',
    method: 'get',
    responseType: 'blob'
  })
} 