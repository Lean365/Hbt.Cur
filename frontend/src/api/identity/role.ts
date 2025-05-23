import request from '@/utils/request'
import type { HbtRole, HbtRoleQuery, HbtRoleUpdate, HbtRoleCreate, HbtRoleStatus } from '@/types/identity/role'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { HbtRoleDeptDto, HbtRoleMenuDto } from '@/types/identity/role'

/**
 * 获取角色分页列表
 */
export function getRoleList(query: HbtRoleQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtRole>>>({
    url: '/api/HbtRole/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取角色详情
 */
export function getRole(roleId: number) {
  return request<HbtApiResponse<HbtRole>>({
    url: `/api/HbtRole/${roleId}`,
    method: 'get'
  })
}

/**
 * 创建角色
 */
export function createRole(data: HbtRoleCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtRole',
    method: 'post',
    data
  })
}

/**
 * 更新角色
 */
export function updateRole(data: HbtRoleUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtRole',
    method: 'put',
    data
  })
}

/**
 * 删除角色
 */
export function deleteRole(roleId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${roleId}`,
    method: 'delete'
  })
}

/**
 * 批量删除角色
 */
export function batchDeleteRole(roleIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtRole/batch',
    method: 'delete',
    data: roleIds
  })
}

/**
 * 导入角色
 */
export function importRole(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtRole/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出角色
 */
export function exportRole(query: HbtRoleQuery) {
  return request<Blob>({
    url: '/api/HbtRole/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getTemplate() {
  return request<Blob>({
    url: '/api/HbtRole/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 更新角色状态
 */
export function updateRoleStatus(data: HbtRoleStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${data.roleId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 获取角色选项列表
 */
export function getRoleOptions() {
  return request<HbtApiResponse<{ label: string; value: number }[]>>({
    url: '/api/HbtRole/options',
    method: 'get'
  })
}

/**
 * 获取角色部门列表
 */
export function getRoleDepts(roleId: number) {
  return request<HbtApiResponse<HbtRoleDeptDto[]>>({
    url: `/api/HbtRole/${roleId}/depts`,
    method: 'get'
  })
}

/**
 * 获取角色菜单列表
 */
export function getRoleMenus(roleId: number) {
  return request<HbtApiResponse<HbtRoleMenuDto[]>>({
    url: `/api/HbtRole/${roleId}/menus`,
    method: 'get'
  })
}

/**
 * 分配角色菜单
 */
export function allocateRoleMenu(roleId: number, menuIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${roleId}/menus`,
    method: 'put',
    data: menuIds
  })
}

/**
 * 分配角色用户
 */
export function allocateRoleUser(roleId: number, userIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${roleId}/users`,
    method: 'put',
    data: userIds
  })
}

/**
 * 分配角色部门
 */
export function allocateRoleDept(roleId: number, deptIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${roleId}/depts`,
    method: 'put',
    data: deptIds
  })
}
