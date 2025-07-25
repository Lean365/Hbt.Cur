import request from '@/utils/request'
import type { HbtSchedule, HbtScheduleQuery, HbtScheduleCreate, HbtScheduleUpdate } from '@/types/routine/schedule/schedule'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取日程分页列表
 */
export function getScheduleList(params: HbtScheduleQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtSchedule>>>({
    url: '/api/HbtSchedule/list',
    method: 'get',
    params
  })
}

/**
 * 获取日程详情
 */
export function getScheduleById(scheduleId: number | bigint) {
  return request<HbtApiResponse<HbtSchedule>>({
    url: `/api/HbtSchedule/${scheduleId}`,
    method: 'get'
  })
}

/**
 * 创建日程
 */
export function createSchedule(data: HbtScheduleCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtSchedule',
    method: 'post',
    data
  })
}

/**
 * 更新日程
 */
export function updateSchedule(data: HbtScheduleUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtSchedule',
    method: 'put',
    data
  })
}

/**
 * 删除日程
 */
export function deleteSchedule(scheduleId: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtSchedule/${scheduleId}`,
    method: 'delete'
  })
}

/**
 * 批量删除日程
 */
export function batchDeleteSchedule(scheduleIds: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtSchedule/batch',
    method: 'delete',
    data: scheduleIds
  })
}

/**
 * 导入日程数据
 */
export function importSchedule(file: File, sheetName = '日程信息') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtSchedule/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出日程数据
 */
export function exportSchedule(params: HbtScheduleQuery, sheetName = '日程信息') {
  return request<Blob>({
    url: '/api/HbtSchedule/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取日程导入模板
 */
export function getScheduleTemplate(sheetName = '日程信息') {
  return request<Blob>({
    url: '/api/HbtSchedule/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取我的日程列表
 */
export function getMyScheduleList(params: HbtScheduleQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtSchedule>>>({
    url: '/api/HbtSchedule/my',
    method: 'get',
    params
  })
}

/**
 * 获取团队日程列表
 */
export function getTeamScheduleList(params: HbtScheduleQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtSchedule>>>({
    url: '/api/HbtSchedule/team',
    method: 'get',
    params
  })
}

/**
 * 获取我的日程统计
 */
export function getMyScheduleStats() {
  return request<HbtApiResponse<{
    total: number
    today: number
    thisWeek: number
    thisMonth: number
    pending: number
    completed: number
  }>>({
    url: '/api/HbtSchedule/my/stats',
    method: 'get'
  })
}

/**
 * 获取团队日程统计
 */
export function getTeamScheduleStats() {
  return request<HbtApiResponse<{
    total: number
    today: number
    thisWeek: number
    thisMonth: number
    pending: number
    completed: number
  }>>({
    url: '/api/HbtSchedule/team/stats',
    method: 'get'
  })
}

/**
 * 更新日程状态
 */
export function updateScheduleStatus(scheduleId: number, status: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtSchedule/${scheduleId}/status`,
    method: 'put',
    data: { status }
  })
}

/**
 * 批量更新日程状态
 */
export function batchUpdateScheduleStatus(scheduleIds: number[], status: number) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtSchedule/batch/status',
    method: 'put',
    data: { scheduleIds, status }
  })
} 