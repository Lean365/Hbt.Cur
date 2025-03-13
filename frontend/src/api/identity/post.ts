import request from '@/utils/request'
import type { HbtApiResult } from '@/types/common'
import type { 
  PostQuery, 
  Post,
  PostCreate,
  PostUpdate,
  PostStatus
} from '@/types/identity/post'
import type { ApiResult } from '@/types/base'

// 获取岗位列表
export function getPostList(params: PostQuery) {
  return request<HbtApiResult<Post[]>>({
    url: '/api/post',
    method: 'get',
    params
  })
}

// 获取岗位详情
export function getPost(postId: number) {
  return request<HbtApiResult<Post>>({
    url: `/api/post/${postId}`,
    method: 'get'
  })
}

// 创建岗位
export function createPost(data: PostCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/post',
    method: 'post',
    data
  })
}

// 更新岗位
export function updatePost(data: PostUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/post',
    method: 'put',
    data
  })
}

// 删除岗位
export function deletePost(postId: number) {
  return request<HbtApiResult<any>>({
    url: `/api/post/${postId}`,
    method: 'delete'
  })
}

// 批量删除岗位
export function batchDeletePost(postIds: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/post/batch',
    method: 'delete',
    data: postIds
  })
}

// 更新岗位状态
export function updatePostStatus(data: PostStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/post/${data.postId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 导入岗位数据
export function importPost(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<any>>({
    url: '/api/post/import',
    method: 'post',
    data: formData,
    params: {
      sheetName: 'Sheet1'
    },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出岗位数据
export function exportPost(params: PostQuery) {
  return request({
    url: '/api/post/export',
    method: 'get',
    params: {
      ...params,
      sheetName: '岗位数据'
    },
    responseType: 'blob'
  })
}

// 获取岗位导入模板
export function getPostTemplate() {
  return request({
    url: '/api/post/template',
    method: 'get',
    params: {
      sheetName: '岗位导入模板'
    },
    responseType: 'blob'
  })
}

/**
 * 查询岗位列表
 */
export function listPost() {
  return request.get<ApiResult<Post[]>>('/identity/post/list')
}

/**
 * 查询岗位详细
 * @param postId 岗位ID
 */
export function getPost(postId: number) {
  return request.get<ApiResult<Post>>(`/identity/post/${postId}`)
}

/**
 * 新增岗位
 * @param data 岗位信息
 */
export function createPost(data: Omit<Post, 'postId'>) {
  return request.post<ApiResult<void>>('/identity/post', data)
}

/**
 * 修改岗位
 * @param data 岗位信息
 */
export function updatePost(data: Post) {
  return request.put<ApiResult<void>>('/identity/post', data)
}

/**
 * 删除岗位
 * @param postId 岗位ID
 */
export function deletePost(postId: number) {
  return request.delete<ApiResult<void>>(`/identity/post/${postId}`)
}

/**
 * 导出岗位
 * @param query 查询参数
 */
export function exportPost(query?: Record<string, any>) {
  return request.download('/identity/post/export', query)
} 