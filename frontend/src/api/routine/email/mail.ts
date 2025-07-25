//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: mail.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 邮件相关的API
//===================================================================

import request from '@/utils/request'
import type { HbtMail, HbtMailQuery, HbtMailCreate, HbtMailUpdate } from '@/types/routine/email/mail'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取邮件列表
 * @param params 查询参数
 * @returns 邮件列表
 */
export function getMailList(params: HbtMailQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtMail>>>({
    url: '/api/HbtMail/list',
    method: 'get',
    params
  })
}

/**
 * 获取邮件详情
 * @param id 邮件ID
 * @returns 邮件详情
 */
export function getMailDetail(id: number | bigint) {
  return request<HbtApiResponse<HbtMail>>({
    url: `/api/HbtMail/${id}`,
    method: 'get'
  })
}

/**
 * 创建邮件
 * @param data 邮件数据
 * @returns 创建结果
 */
export function createMail(data: HbtMailCreate) {
  return request<HbtApiResponse<HbtMail>>({
    url: '/api/HbtMail',
    method: 'post',
    data
  })
}

/**
 * 更新邮件
 * @param id 邮件ID
 * @param data 邮件数据
 * @returns 更新结果
 */
export function updateMail(id: number | bigint, data: HbtMailUpdate) {
  return request<HbtApiResponse<HbtMail>>({
    url: `/api/HbtMail/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除邮件
 * @param id 邮件ID
 * @returns 删除结果
 */
export function deleteMail(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMail/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除邮件
 * @param ids 邮件ID列表
 * @returns 删除结果
 */
export function batchDeleteMail(ids: number[] | bigint[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMail/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出邮件列表
 * @param params 查询参数
 * @returns 导出结果
 */
export function exportMailList(params: HbtMailQuery) {
  return request<Blob>({
    url: '/api/HbtMail/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入邮件列表
 * @param file 文件
 * @returns 导入结果
 */
export function importMailList(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMail/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 发送邮件
 * @param data 邮件数据
 * @returns 发送结果
 */
export function sendMail(data: HbtMailCreate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMail/send',
    method: 'post',
    data
  })
}

/** 标记邮件已读 */
export function markMailAsRead(id: number | bigint) {
  return request<boolean>({
    url: `/api/HbtMail/${id}/read`,
    method: 'post'
  })
}

/** 标记所有邮件已读 */
export function markAllMailAsRead() {
  return request<number>({
    url: '/api/HbtMail/read-all',
    method: 'post'
  })
}

/** 标记邮件未读 */
export function markMailAsUnread(id: number | bigint) {
  return request<boolean>({
    url: `/api/HbtMail/${id}/unread`,
    method: 'post'
  })
}

/** 标记所有邮件未读 */
export function markAllMailAsUnread() {
  return request<number>({
    url: '/api/HbtMail/unread-all',
    method: 'post'
  })
} 