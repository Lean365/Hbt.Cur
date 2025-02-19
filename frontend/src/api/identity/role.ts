import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  RoleQuery, 
  Role, 
  RoleCreate, 
  RoleUpdate,
  RoleStatus
} from '@/types/identity/role'
import { HbtStatus } from '@/types/enums'
import type { ApiResult } from '@/types/base'

// 获取角色列表
export function getRoleList(params: RoleQuery) {
  return request<HbtApiResult<Role[]>>({
    url: '/api/role',
    method: 'get',
    params
  })
}

// 获取角色详情
export function getRole(roleId: number) {
  return request<HbtApiResult<Role>>({
    url: `/api/role/${roleId}`,
    method: 'get'
  })
}

// 创建角色
export function createRole(data: RoleCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/role',
    method: 'post',
    data
  })
}

// 更新角色
export function updateRole(data: RoleUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/role',
    method: 'put',
    data
  })
}

// 删除角色
export function deleteRole(roleId: number) {
  return request<HbtApiResult<any>>({
    url: `/api/role/${roleId}`,
    method: 'delete'
  })
}

// 批量删除角色
export function batchDeleteRole(roleIds: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/role/batch',
    method: 'delete',
    data: roleIds
  })
}

// 更新角色状态
export function updateRoleStatus(roleId: number, status: HbtStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/role/${roleId}/status`,
    method: 'put',
    params: { status }
  })
}

// 获取角色权限
export function getRolePermissions(roleId: number) {
  return request<HbtApiResult<string[]>>({
    url: `/api/role/${roleId}/permissions`,
    method: 'get'
  })
}

// 更新角色权限
export function updateRolePermissions(roleId: number, permissions: string[]) {
  return request<HbtApiResult<any>>({
    url: `/api/role/${roleId}/permissions`,
    method: 'put',
    data: { permissions }
  })
}

// 导入角色数据
export function importRole(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<any>>({
    url: '/api/role/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出角色数据
export function exportRole(params: RoleQuery) {
  return request({
    url: '/api/role/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

// 获取角色导入模板
export function getRoleTemplate() {
  return request({
    url: '/api/role/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 查询角色列表
 */
export function listRole() {
  return request.get<ApiResult<Role[]>>('/identity/role/list')
}

/**
 * 查询角色详细
 * @param roleId 角色ID
 */
export function getRole(roleId: number) {
  return request.get<ApiResult<Role>>(`/identity/role/${roleId}`)
}

/**
 * 新增角色
 * @param data 角色信息
 */
export function createRole(data: Omit<Role, 'roleId'>) {
  return request.post<ApiResult<void>>('/identity/role', data)
}

/**
 * 修改角色
 * @param data 角色信息
 */
export function updateRole(data: Role) {
  return request.put<ApiResult<void>>('/identity/role', data)
}

/**
 * 删除角色
 * @param roleId 角色ID
 */
export function deleteRole(roleId: number) {
  return request.delete<ApiResult<void>>(`/identity/role/${roleId}`)
}

/**
 * 导出角色
 * @param query 查询参数
 */
export function exportRole(query?: Record<string, any>) {
  return request.download('/identity/role/export', query)
} 