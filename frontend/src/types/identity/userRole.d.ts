import type { HbtBaseEntity } from '@/types/common'

/**
 * 用户角色关联
 */
export interface HbtUserRole extends HbtBaseEntity {
  /** 用户ID */
  userId: number
  /** 角色ID */
  roleId: number
}

/**
 * 用户角色分配参数
 */
export interface UserRoleAllocate {
  userId: number
  roleIds: number[]
} 