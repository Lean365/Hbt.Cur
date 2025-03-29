import type { HbtBaseEntity } from '@/types/common'

/**
 * 用户角色关联对象
 */
export interface UserRole extends HbtBaseEntity {
  userId: number
  roleId: number
  tenantId: number
}

/**
 * 用户角色分配参数
 */
export interface UserRoleAllocate {
  userId: number
  roleIds: number[]
} 