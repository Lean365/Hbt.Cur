import type { HbtBaseEntity } from '@/types/common'

/**
 * 用户部门关联对象
 */
export interface UserDept extends HbtBaseEntity {
  userId: number
  deptId: number
  tenantId: number
}

/**
 * 用户部门分配参数
 */
export interface UserDeptAllocate {
  userId: number
  deptId: number
} 