import request from '@/utils/request'
import type { HbtApiResult } from '@/types/common'
import type { 
  PostQuery, 
  Post,
  PostCreate,
  PostUpdate,
  PostStatus
} from '@/types/identity/post'

/**
 * 获取岗位选项列表
 */
export function getPostOptions() {
  return request<HbtApiResult<{ label: string; value: number }[]>>({
    url: '/api/post/options',
    method: 'get'
  })
}

/**
 * 获取岗位列表
 */
export function getPostList(params: PostQuery) {
  return request<HbtApiResult<Post[]>>({
    url: '/api/post/list',
    method: 'get',
    params
  })
}

/**
 * 获取岗位详情
 * @param postId 岗位ID
 */
export function getPost(postId: number) {
  return request<HbtApiResult<Post>>({
    url: `/api/post/${postId}`,
    method: 'get'
  })
}

/**
 * 创建岗位
 * @param data 岗位信息
 */
export function createPost(data: PostCreate) {
  return request<HbtApiResult<void>>({
    url: '/api/post',
    method: 'post',
    data
  })
}

/**
 * 更新岗位
 * @param data 岗位信息
 */
export function updatePost(data: PostUpdate) {
  return request<HbtApiResult<void>>({
    url: '/api/post',
    method: 'put',
    data
  })
}

/**
 * 删除岗位
 * @param postId 岗位ID
 */
export function deletePost(postId: number) {
  return request<HbtApiResult<void>>({
    url: `/api/post/${postId}`,
    method: 'delete'
  })
}

/**
 * 批量删除岗位
 * @param postIds 岗位ID列表
 */
export function batchDeletePost(postIds: number[]) {
  return request<HbtApiResult<void>>({
    url: '/api/post/batch',
    method: 'delete',
    data: postIds
  })
}

/**
 * 更新岗位状态
 * @param data 岗位状态信息
 */
export function updatePostStatus(data: PostStatus) {
  return request<HbtApiResult<void>>({
    url: `/api/post/${data.postId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 导入岗位数据
 * @param file 文件对象
 */
export function importPost(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<void>>({
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

/**
 * 导出岗位数据
 * @param params 查询参数
 */
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

/**
 * 获取岗位导入模板
 */
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