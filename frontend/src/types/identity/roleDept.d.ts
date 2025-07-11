import type { HbtBaseEntity } from '@/types/common'

/**
 * 角色部门关联
 */
export interface HbtRoleDept extends HbtBaseEntity {
  /** 角色ID */
  roleId: number
  /** 部门ID */
  deptId: number
}

/**
 * 角色部门分配参数
 */
export interface RoleDeptAllocate {
  roleId: number
  deptIds: number[]
} 