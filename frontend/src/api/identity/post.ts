import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  HbtPostQuery, 
  HbtPost,
  HbtPostCreate,
  HbtPostUpdate,
  HbtPostStatus
} from '@/types/identity/post'
import type { HbtUser } from '@/types/identity/user'

/**
 * 获取岗位分页列表
 */
export function getPostList(query: HbtPostQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtPost>>>({
    url: '/api/HbtPost/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取岗位详情
 */
export function getPost(postId: number) {
  return request<HbtApiResponse<HbtPost>>({
    url: `/api/HbtPost/${postId}`,
    method: 'get'
  })
}

/**
 * 创建岗位
 */
export function createPost(data: HbtPostCreate) {
  return request({
    url: '/api/HbtPost',
    method: 'post',
    data
  })
}

/**
 * 更新岗位
 */
export function updatePost(data: HbtPostUpdate) {
  return request({
    url: '/api/HbtPost',
    method: 'put',
    data
  })
}

/**
 * 删除岗位
 */
export function deletePost(id: number) {
  return request({
    url: `/api/HbtPost/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除岗位
 */
export function batchDeletePost(ids: number[]) {
  return request({
    url: '/api/HbtPost/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新岗位状态
 */
export function updatePostStatus(data: HbtPostStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtPost/${data.postId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 导出岗位数据
 */
export function exportPost(query: HbtPostQuery) {
  return request({
    url: '/api/HbtPost/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 导入岗位数据
 */
export function importPost(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtPost/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 下载岗位导入模板
 */
export function downloadTemplate() {
  return request({
    url: '/api/HbtPost/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 获取岗位选项列表
 */
export function getPostOptions() {
  return request<HbtApiResponse<{ label: string; value: number }[]>>({
    url: '/api/HbtPost/options',
    method: 'get'
  })
}

/**
 * 获取岗位列表
 */
export function getPostAllList() {
  return request<HbtApiResponse<HbtPost[]>>({
    url: '/api/HbtPost/list',
    method: 'get'
  })
}

/**
 * 获取岗位用户列表
 */
export function getPostUsers(postId: number) {
  return request({
    url: `/api/HbtPost/${postId}/users`,
    method: 'get'
  })
}

/**
 * 分配岗位用户
 */
export function assignPostUsers(postId: number, userIds: number[]) {
  return request({
    url: `/api/HbtPost/${postId}/users`,
    method: 'post',
    data: userIds
  })
}

/**
 * 移除岗位用户
 */
export function removePostUsers(postId: number, userIds: number[]) {
  return request({
    url: `/api/HbtPost/${postId}/users`,
    method: 'delete',
    data: userIds
  })
}