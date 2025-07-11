import type { HbtBaseEntity } from '@/types/common'

/**
 * 用户部门关联
 */
export interface HbtUserDept extends HbtBaseEntity {
  /** 用户ID */
  userId: number
  /** 部门ID */
  deptId: number
}

/**
 * 用户部门分配参数
 */
export interface UserDeptAllocate {
  userId: number
  deptId: number
} 