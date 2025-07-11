import type { HbtBaseEntity } from '@/types/common'

/**
 * 角色菜单关联
 */
export interface HbtRoleMenu extends HbtBaseEntity {
  /** 角色ID */
  roleId: number
  /** 菜单ID */
  menuId: number
}

/**
 * 角色菜单分配参数
 */
export interface RoleMenuAllocate {
  roleId: number
  menuIds: number[]
} 