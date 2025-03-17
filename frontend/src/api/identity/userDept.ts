import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { UserDeptAllocate } from '@/types/identity/userDept'

/**
 * 获取用户部门
 */
export function getUserDept(userId: number) {
  return request<HbtApiResponse<number>>({
    url: `/api/HbtUser/${userId}/dept`,
    method: 'get'
  })
}

/**
 * 分配用户部门
 */
export function allocateUserDept(data: UserDeptAllocate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtUser/${data.userId}/dept`,
    method: 'put',
    data: { deptId: data.deptId }
  })
} 