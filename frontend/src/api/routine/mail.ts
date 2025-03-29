//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: mail.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 邮件相关的API
//===================================================================

import request from '@/utils/request'
import type { HbtMailQueryParams, HbtMailPageResult, HbtMailDto, HbtMailCreateDto, HbtMailUpdateDto, HbtMailSendDto } from '@/types/routine/mail'

/** 获取邮件列表 */
export function getMailList(params: HbtMailQueryParams) {
  return request<HbtMailPageResult>({
    url: '/api/HbtMail/list',
    method: 'get',
    params
  })
}

/** 获取邮件详情 */
export function getMail(id: number | bigint) {
  return request<HbtMailDto>({
    url: `/api/HbtMail/${id}`,
    method: 'get'
  })
}

/** 创建邮件 */
export function createMail(data: HbtMailCreateDto) {
  return request<number>({
    url: '/api/HbtMail',
    method: 'post',
    data
  })
}

/** 更新邮件 */
export function updateMail(data: HbtMailUpdateDto) {
  return request<boolean>({
    url: '/api/HbtMail',
    method: 'put',
    data
  })
}

/** 删除邮件 */
export function deleteMail(id: number | bigint) {
  return request<boolean>({
    url: `/api/HbtMail/${id}`,
    method: 'delete'
  })
}

/** 批量删除邮件 */
export function batchDeleteMail(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/HbtMail/batch',
    method: 'delete',
    data: ids
  })
}

/** 发送邮件 */
export function sendMail(data: HbtMailSendDto) {
  return request<boolean>({
    url: '/api/HbtMail/send',
    method: 'post',
    data
  })
}

/** 批量发送邮件 */
export function batchSendMail(data: HbtMailSendDto[]) {
  return request<[number, number]>({
    url: '/api/HbtMail/batch-send',
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

/** 导出邮件数据 */
export function exportMail(params: HbtMailQueryParams) {
  return request<Blob>({
    url: '/api/HbtMail/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
} 