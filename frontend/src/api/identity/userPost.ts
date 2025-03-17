import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { UserPostAllocate } from '@/types/identity/userPost'

/**
 * 获取用户岗位列表
 */
export function getUserPostList(userId: number) {
  return request<HbtApiResponse<number[]>>({
    url: `/api/HbtUser/${userId}/posts`,
    method: 'get'
  })
}

/**
 * 分配用户岗位
 */
export function allocateUserPost(data: UserPostAllocate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${data.userId}/posts`,
    method: 'put',
    data: data.postIds
  })
} 