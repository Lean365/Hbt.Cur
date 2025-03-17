import type { HbtBaseEntity } from '@/types/common'

/**
 * 用户岗位关联对象
 */
export interface UserPost extends HbtBaseEntity {
  userId: number
  postId: number
  tenantId: number
}

/**
 * 用户岗位分配参数
 */
export interface UserPostAllocate {
  userId: number
  postIds: number[]
} 