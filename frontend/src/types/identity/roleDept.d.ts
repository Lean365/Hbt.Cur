import type { HbtBaseEntity } from '@/types/common'

/**
 * 角色部门关联对象
 */
export interface RoleDept extends HbtBaseEntity {
  roleId: number
  deptId: number
  tenantId: number
}

/**
 * 角色部门分配参数
 */
export interface RoleDeptAllocate {
  roleId: number
  deptIds: number[]
} 