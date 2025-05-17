import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  PostQuery, 
  Post,
  PostCreate,
  PostUpdate,
  PostStatus
} from '@/types/identity/post'

/**
 * 获取岗位分页列表
 */
export function getPagedList(params: PostQuery) {
  return request<HbtApiResponse<HbtPagedResult<Post>>>({
    url: '/api/HbtPost/list',
    method: 'get',
    params
  })
}

/**
 * 获取岗位详情
 */
export function getPost(postId: number) {
  return request<HbtApiResponse<Post>>({
    url: `/api/HbtPost/${postId}`,
    method: 'get'
  })
}

/**
 * 创建岗位
 */
export function createPost(data: PostCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtPost',
    method: 'post',
    data
  })
}

/**
 * 更新岗位
 */
export function updatePost(data: PostUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtPost',
    method: 'put',
    data
  })
}

/**
 * 删除岗位
 */
export function deletePost(postId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtPost/${postId}`,
    method: 'delete'
  })
}

/**
 * 批量删除岗位
 */
export function batchDeletePost(postIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtPost/batch',
    method: 'delete',
    data: postIds
  })
}

/**
 * 更新岗位状态
 */
export function updatePostStatus(data: PostStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtPost/${data.postId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 导出岗位数据
 */
export function exportPost(params?: PostQuery) {
  return request({
    url: '/api/HbtPost/export',
    method: 'get',
    params,
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
export function getPostList() {
  return request<HbtApiResponse<HbtPostDto[]>>({
    url: '/api/HbtPost/list',
    method: 'get'
  })
}