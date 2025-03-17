import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { UserRoleAllocate } from '@/types/identity/userRole'

/**
 * 获取用户角色列表
 */
export function getUserRoleList(userId: number) {
  return request<HbtApiResponse<number[]>>({
    url: `/api/HbtUser/${userId}/roles`,
    method: 'get'
  })
}

/**
 * 分配用户角色
 */
export function allocateUserRole(data: UserRoleAllocate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${data.userId}/roles`,
    method: 'put',
    data: data.roleIds
  })
} 