import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { RoleDeptAllocate } from '@/types/identity/roleDept'

/**
 * 获取角色部门列表
 */
export function getRoleDeptList(roleId: number) {
  return request<HbtApiResponse<number[]>>({
    url: `/api/HbtRole/${roleId}/depts`,
    method: 'get'
  })
}

/**
 * 分配角色部门
 */
export function allocateRoleDept(data: RoleDeptAllocate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtRole/${data.roleId}/depts`,
    method: 'put',
    data: data.deptIds
  })
} 