//===================================================================
// 项目名 : Lean.Hbt
// 文件名称: notice.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 通知相关的API
//===================================================================

import request from '@/utils/request'
import type { HbtNoticeQueryParams, HbtNoticePageResult, HbtNoticeDto } from '@/types/routine/notice'

/** 获取通知列表 */
export function getNoticeList(params: HbtNoticeQueryParams) {
  return request<HbtNoticePageResult>({
    url: '/api/HbtNotice/list',
    method: 'get',
    params
  })
}

/** 获取通知详情 */
export function getNotice(id: number | bigint) {
  return request<HbtNoticeDto>({
    url: `/api/HbtNotice/${id}`,
    method: 'get'
  })
}

/** 标记通知已读 */
export function markNoticeAsRead(id: number | bigint) {
  return request({
    url: `/api/HbtNotice/${id}/read`,
    method: 'put'
  })
}

/** 标记所有通知已读 */
export function markAllNoticeAsRead() {
  return request({
    url: '/api/HbtNotice/read-all',
    method: 'put'
  })
} 