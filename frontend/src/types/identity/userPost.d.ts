import type { HbtBaseEntity } from '@/types/common'

/**
 * 用户岗位关联
 */
export interface HbtUserPost extends HbtBaseEntity {
  /** 用户ID */
  userId: number
  /** 岗位ID */
  postId: number
}

/**
 * 用户岗位分配参数
 */
export interface UserPostAllocate {
  userId: number
  postIds: number[]
} 