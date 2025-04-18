//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: notice.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 通知相关的API
//===================================================================

import request from '@/utils/request'
import type { HbtNoticeDto, HbtNoticeQueryDto, HbtNoticeCreateDto, HbtNoticeUpdateDto } from '@/types/routine/notice'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取通知列表
 * @param params 查询参数
 * @returns 通知列表
 */
export function getNoticeList(params: HbtNoticeQueryDto) {
  return request<HbtApiResponse<HbtPagedResult<HbtNoticeDto>>>({
    url: '/api/HbtNotice/list',
    method: 'get',
    params
  })
}

/**
 * 获取通知详情
 * @param id 通知ID
 * @returns 通知详情
 */
export function getNoticeDetail(id: number | bigint) {
  return request<HbtApiResponse<HbtNoticeDto>>({
    url: `/api/HbtNotice/${id}`,
    method: 'get'
  })
}

/**
 * 创建通知
 * @param data 通知数据
 * @returns 创建结果
 */
export function createNotice(data: HbtNoticeCreateDto) {
  return request<HbtApiResponse<HbtNoticeDto>>({
    url: '/api/HbtNotice',
    method: 'post',
    data
  })
}

/**
 * 更新通知
 * @param id 通知ID
 * @param data 通知数据
 * @returns 更新结果
 */
export function updateNotice(id: number | bigint, data: HbtNoticeUpdateDto) {
  return request<HbtApiResponse<HbtNoticeDto>>({
    url: `/api/HbtNotice/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除通知
 * @param id 通知ID
 * @returns 删除结果
 */
export function deleteNotice(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNotice/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除通知
 * @param ids 通知ID列表
 * @returns 删除结果
 */
export function batchDeleteNotice(ids: number[] | bigint[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNotice/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出通知列表
 * @param params 查询参数
 * @returns 导出结果
 */
export function exportNoticeList(params: HbtNoticeQueryDto) {
  return request<Blob>({
    url: '/api/HbtNotice/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入通知列表
 * @param file 文件
 * @returns 导入结果
 */
export function importNoticeList(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNotice/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 标记通知为已读
 * @param id 通知ID
 * @returns 标记结果
 */
export function markNoticeAsRead(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtNotice/read/${id}`,
    method: 'put'
  })
}

/**
 * 批量标记通知为已读
 * @param ids 通知ID列表
 * @returns 标记结果
 */
export function batchMarkNoticeAsRead(ids: number[] | bigint[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtNotice/read/batch',
    method: 'put',
    data: ids
  })
} 