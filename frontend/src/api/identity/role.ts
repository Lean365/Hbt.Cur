import request from '@/utils/request'
import type { Role, RoleQuery } from '@/types/identity/role'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { HbtRoleDeptDto, HbtRoleMenuDto } from '@/types/identity/role'

/**
 * 获取角色分页列表
 * @param query 查询参数
 */
export function getPagedList(query: RoleQuery) {
  return request<HbtApiResponse<HbtPagedResult<Role>>>(
    {
      url: '/api/HbtRole/list',
      method: 'get',
      params: query
    }
  )
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
 * 批量删除角色（如有需求可补充实现）
 * @param roleIds 角色ID数组
 */
// export function batchDeleteRole(roleIds: number[]) {
//   return request<HbtApiResponse<boolean>>({
//     url: '/api/HbtRole/batch',
//     method: 'delete',
//     data: roleIds
//   })
// }

/**
 * 导入角色
 * @param file 角色数据文件
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
 * @param query 查询参数
 */
export function exportRole(query?: RoleQuery) {
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
export function downloadTemplate() {
  return request<Blob>({
    url: '/api/HbtRole/template',
    method: 'get',
    responseType: 'blob'
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
 * 获取角色选项列表
 */
export function getRoleOptions() {
  return request<HbtApiResponse<{ label: string; value: number }[]>>({
    url: '/api/HbtRole/options',
    method: 'get'
  })
}

/**
 * 获取全部角色列表
 */
export function getRoleList() {
  return request<HbtApiResponse<Role[]>>({
    url: '/api/HbtRole/list',
    method: 'get'
  })
}

/**
 * 获取角色部门列表
 * @param roleId 角色ID
 */
export function getRoleDepts(roleId: number) {
  return request<HbtApiResponse<HbtRoleDeptDto[]>>({
    url: `/api/HbtRole/${roleId}/depts`,
    method: 'get'
  })
}

/**
 * 获取角色菜单列表
 * @param roleId 角色ID
 */
export function getRoleMenus(roleId: number) {
  return request<HbtApiResponse<HbtRoleMenuDto[]>>({
    url: `/api/HbtRole/${roleId}/menus`,
    method: 'get'
  })
}

/**
 * 分配角色菜单
 * @param roleId 角色ID
 * @param menuIds 菜单ID数组
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
 * @param roleId 角色ID
 * @param userIds 用户ID数组
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
 * @param roleId 角色ID
 * @param deptIds 部门ID数组
 */
export function allocateRoleDept(roleId: number, deptIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${roleId}/depts`,
    method: 'put',
    data: deptIds
  })
}
