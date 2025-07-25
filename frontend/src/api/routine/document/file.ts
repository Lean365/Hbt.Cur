import request from '@/utils/request'
import type { HbtFile, HbtFileQuery, HbtFileCreate, HbtFileUpdate } from '@/types/routine/document/file'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取文件列表
 * @param params 查询参数
 * @returns 文件列表
 */
export function getFileList(params: HbtFileQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtFile>>>({
    url: '/api/HbtFile/list',
    method: 'get',
    params
  })
}

/**
 * 获取文件详情
 * @param id 文件ID
 * @returns 文件详情
 */
export function getFileDetail(id: number | bigint) {
  return request<HbtApiResponse<HbtFile>>({
    url: `/api/HbtFile/${id}`,
    method: 'get'
  })
}

/**
 * 创建文件
 * @param data 文件数据
 * @returns 创建结果
 */
export function createFile(data: HbtFileCreate) {
  return request<HbtApiResponse<HbtFile>>({
    url: '/api/HbtFile',
    method: 'post',
    data
  })
}

/**
 * 更新文件
 * @param id 文件ID
 * @param data 文件数据
 * @returns 更新结果
 */
export function updateFile(id: number | bigint, data: HbtFileUpdate) {
  return request<HbtApiResponse<HbtFile>>({
    url: `/api/HbtFile/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除文件
 * @param id 文件ID
 * @returns 删除结果
 */
export function deleteFile(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtFile/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除文件
 * @param ids 文件ID列表
 * @returns 删除结果
 */
export function batchDeleteFile(ids: number[] | bigint[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtFile/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出文件列表
 * @param params 查询参数
 * @returns 导出结果
 */
export function exportFileList(params: HbtFileQuery) {
  return request<Blob>({
    url: '/api/HbtFile/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入文件列表
 * @param file 文件
 * @returns 导入结果
 */
export function importFileList(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtFile/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 下载文件
 * @param id 文件ID
 * @returns 下载结果
 */
export function downloadFile(id: number | bigint) {
  return request<Blob>({
    url: `/api/HbtFile/download/${id}`,
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 上传文件
 * @param file 文件
 * @returns 上传结果
 */
export function uploadFile(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<HbtFile>>({
    url: '/api/HbtFile/upload',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
} 