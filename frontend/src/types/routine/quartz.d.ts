/** 定时任务相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 定时任务查询参数 */
export interface HbtQuartzJobQueryDto extends HbtPagedQuery {
  /** 任务名称 */
  jobName?: string
  /** 任务组 */
  jobGroup?: string
  /** 任务状态（0暂停 1运行） */
  jobStatus?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 定时任务数据传输对象 */
export interface HbtQuartzJobDto extends HbtBaseEntity {
  /** ID */
  jobId: number | bigint
  /** 任务名称 */
  jobName: string
  /** 任务组 */
  jobGroup: string
  /** 任务状态（0暂停 1运行） */
  jobStatus: number
  /** 任务类名 */
  jobClassName: string
  /** 任务方法名 */
  jobMethodName: string
  /** 任务参数 */
  jobParams?: string
  /** 任务描述 */
  jobDescription?: string
  /** 任务表达式 */
  jobCronExpression: string
  /** 任务执行时间 */
  jobExecuteTime?: Date
  /** 任务执行结果 */
  jobExecuteResult?: string
  /** 任务执行错误 */
  jobExecuteError?: string
  /** 创建时间 */
  jobCreateTime?: Date
  /** 更新时间 */
  jobUpdateTime?: Date
  /** 创建人ID */
  jobCreatorId: number | bigint
  /** 创建人名称 */
  jobCreatorName: string
}

/** 定时任务创建DTO */
export interface HbtQuartzJobCreateDto {
  /** 任务名称 */
  jobName: string
  /** 任务组 */
  jobGroup: string
  /** 任务状态（0暂停 1运行） */
  jobStatus: number
  /** 任务类名 */
  jobClassName: string
  /** 任务方法名 */
  jobMethodName: string
  /** 任务参数 */
  jobParams?: string
  /** 任务描述 */
  jobDescription?: string
  /** 任务表达式 */
  jobCronExpression: string
  /** 创建人ID */
  jobCreatorId: number | bigint
  /** 创建人名称 */
  jobCreatorName: string
}

/** 定时任务更新DTO */
export interface HbtQuartzJobUpdateDto extends HbtQuartzJobCreateDto {
  /** ID */
  jobId: bigint
}

/** 定时任务分页结果 */
export type HbtQuartzJobPagedResult = HbtPagedResult<HbtQuartzJobDto> 