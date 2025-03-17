import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { RoleMenuAllocate } from '@/types/identity/roleMenu'

/**
 * 获取角色菜单列表
 */
export function getRoleMenuList(roleId: number) {
  return request<HbtApiResponse<number[]>>({
    url: `/api/HbtRole/${roleId}/menus`,
    method: 'get'
  })
}

/**
 * 分配角色菜单
 */
export function allocateRoleMenu(data: RoleMenuAllocate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${data.roleId}/menus`,
    method: 'put',
    data: data.menuIds
  })
} 