import request from '@/utils/request'
import type { ApiResult } from '@/types/base'
import type { Dept, DeptQuery, DeptCreate, DeptUpdate, DeptTreeNode } from '@/types/identity/dept'

/**
 * 查询部门列表
 * @param query 查询参数
 */
export function listDept(query?: DeptQuery) {
  return request.get<ApiResult<Dept[]>>('/identity/dept/list', { params: query })
}

/**
 * 查询部门树结构
 */
export function listDeptTree() {
  return request.get<ApiResult<DeptTreeNode[]>>('/identity/dept/tree')
}

/**
 * 查询部门详细
 * @param deptId 部门ID
 */
export function getDept(deptId: number) {
  return request.get<ApiResult<Dept>>(`/identity/dept/${deptId}`)
}

/**
 * 新增部门
 * @param data 部门信息
 */
export function createDept(data: DeptCreate) {
  return request.post<ApiResult<void>>('/identity/dept', data)
}

/**
 * 修改部门
 * @param data 部门信息
 */
export function updateDept(data: DeptUpdate) {
  return request.put<ApiResult<void>>('/identity/dept', data)
}

/**
 * 删除部门
 * @param deptId 部门ID
 */
export function deleteDept(deptId: number) {
  return request.delete<ApiResult<void>>(`/identity/dept/${deptId}`)
}

/**
 * 导出部门
 * @param query 查询参数
 */
export function exportDept(query?: DeptQuery) {
  return request.download('/identity/dept/export', query)
}

// 获取部门树形数据
export function getDeptTree() {
  return request<Dept[]>({
    url: '/api/identity/dept/tree',
    method: 'get'
  }).then(res => res.data)
}

// 获取部门列表
export function getDeptList() {
  return request<Dept[]>({
    url: '/api/identity/dept/list',
    method: 'get'
  }).then(res => res.data)
}

// 创建部门
export function createDept(data: DeptCreate) {
  return request<void>({
    url: '/api/identity/dept',
    method: 'post',
    data
  })
}

// 更新部门
export function updateDept(data: DeptUpdate) {
  return request<void>({
    url: `/api/identity/dept/${data.id}`,
    method: 'put',
    data
  })
}

// 删除部门
export function deleteDept(id: number) {
  return request<void>({
    url: `/api/identity/dept/${id}`,
    method: 'delete'
  })
} 