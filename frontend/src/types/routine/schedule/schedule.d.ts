/** 日程相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 日程查询参数 */
export interface HbtScheduleQuery extends HbtPagedQuery {
  title?: string
  status?: number
  startTime?: string
  endTime?: string
  [key: string]: any
}

/** 日程实体 */
export interface HbtSchedule extends HbtBaseEntity {
  scheduleId: number
  title: string
  content?: string
  location?: string
  startTime: string
  endTime: string
  status: number
  [key: string]: any
}

/** 日程创建参数 */
export interface HbtScheduleCreate {
  title: string
  content?: string
  location?: string
  startTime: string
  endTime: string
  status: number
  [key: string]: any
}

/** 日程更新参数 */
export interface HbtScheduleUpdate extends HbtScheduleCreate {
  scheduleId: number
}

/** 日程批量删除参数 */
export interface HbtScheduleBatchDelete {
  scheduleIds: number[]
}

/** 日程分页结果 */
export type HbtSchedulePagedResult = HbtPagedResult<HbtSchedule> 