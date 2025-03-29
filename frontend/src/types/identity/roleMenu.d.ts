import type { HbtBaseEntity } from '@/types/common'

/**
 * 角色菜单关联对象
 */
export interface RoleMenu extends HbtBaseEntity {
  roleId: number
  menuId: number
  tenantId: number
}

/**
 * 角色菜单分配参数
 */
export interface RoleMenuAllocate {
  roleId: number
  menuIds: number[]
} 