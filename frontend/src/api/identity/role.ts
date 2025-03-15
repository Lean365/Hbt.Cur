import request from '@/utils/request'
import type { HbtApiResult, HbtPagedResult } from '@/types/common'
import type { Role, RoleQuery } from '@/types/identity/role'

/**
 * 获取角色选项列表
 */
export function getRoleOptions() {
  return request<HbtApiResult<{ label: string; value: number }[]>>({
    url: '/api/role/options',
    method: 'get'
  })
}

/**
 * 获取角色列表
 */
export function getRoleList() {
  return request<HbtApiResult<Role[]>>({
    url: '/api/role/list',
    method: 'get'
  })
}

/**
 * 获取角色详情
 * @param roleId 角色ID
 */
export function getRole(roleId: number) {
  return request<HbtApiResult<Role>>({
    url: `/api/role/${roleId}`,
    method: 'get'
  })
}

/**
 * 创建角色
 * @param data 角色信息
 */
export function createRole(data: Omit<Role, 'roleId'>) {
  return request<HbtApiResult<void>>({
    url: '/api/role',
    method: 'post',
    data
  })
}

/**
 * 更新角色
 * @param data 角色信息
 */
export function updateRole(data: Role) {
  return request<HbtApiResult<void>>({
    url: '/api/role',
    method: 'put',
    data
  })
}

/**
 * 删除角色
 * @param roleId 角色ID
 */
export function deleteRole(roleId: number) {
  return request<HbtApiResult<void>>({
    url: `/api/role/${roleId}`,
    method: 'delete'
  })
}

/**
 * 更新角色状态
 * @param roleId 角色ID
 * @param status 状态（0正常 1停用）
 */
export function updateRoleStatus(roleId: number, status: number) {
  return request<HbtApiResult<void>>({
    url: `/api/role/${roleId}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 获取角色分页列表
 * @param query 查询参数
 */
export function getPagedList(query: RoleQuery) {
  return request<HbtPagedResult<Role>>({
    url: '/api/role',
    method: 'get',
    params: query
  })
}
