import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { Role, RoleQuery } from '@/types/identity/role'

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
 * 获取角色列表
 */
export function getRoleList() {
  return request<HbtApiResponse<Role[]>>({
    url: '/api/HbtRole/list',
    method: 'get'
  })
}

/**
 * 获取角色详情
 * @param roleId 角色ID
 */
export function getRole(roleId: number) {
  return request<HbtApiResponse<Role>>({
    url: `/api/HbtRole/${roleId}`,
    method: 'get'
  })
}

/**
 * 创建角色
 * @param data 角色信息
 */
export function createRole(data: Omit<Role, 'roleId'>) {
  return request<HbtApiResponse<void>>({
    url: '/api/HbtRole',
    method: 'post',
    data
  })
}

/**
 * 更新角色
 * @param data 角色信息
 */
export function updateRole(data: Role) {
  return request<HbtApiResponse<void>>({
    url: '/api/HbtRole',
    method: 'put',
    data
  })
}

/**
 * 删除角色
 * @param roleId 角色ID
 */
export function deleteRole(roleId: number) {
  return request<HbtApiResponse<void>>({
    url: `/api/HbtRole/${roleId}`,
    method: 'delete'
  })
}

/**
 * 更新角色状态
 */
export function updateRoleStatus(data: { roleId: number; status: number }) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${data.roleId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 获取角色分页列表
 * @param query 查询参数
 */
export function getPagedList(query: RoleQuery) {
  return request<HbtApiResponse<HbtPagedResult<Role>>>({
    url: '/api/HbtRole',
    method: 'get',
    params: query
  })
}

/**
 * 导出角色数据
 */
export function exportRole(params?: RoleQuery) {
  return request({
    url: '/api/HbtRole/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入角色数据
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
 * 下载角色导入模板
 */
export function downloadTemplate() {
  return request({
    url: '/api/HbtRole/template',
    method: 'get',
    responseType: 'blob'
  })
}
