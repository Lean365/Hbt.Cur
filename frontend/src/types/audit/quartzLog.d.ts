import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 定时任务日志查询参数 */
export interface HbtQuartzLogQueryDto extends HbtPagedQuery {
  /** 任务名称 */
  logTaskName?: string
  /** 任务组名 */
  logGroupName?: string
  /** 执行状态(0=失败,1=成功) */
  logStatus?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/** 定时任务日志记录 */
export interface HbtQuartzLogDto {
  /** 日志ID */
  logId: number
  /** 任务ID */
  logTaskId: number
  /** 任务名称 */
  logTaskName: string
  /** 任务组名 */
  logGroupName: string
  /** 执行时间 */
  logExecuteTime: string
  /** 执行耗时(毫秒) */
  logExecuteDuration: number
  /** 执行参数 */
  logExecuteParams: string
  /** 执行状态(0=失败,1=成功) */
  logStatus: number
  /** 错误信息 */
  logErrorInfo: string
  /** 执行机器IP */
  logExecuteIp: string
  /** 执行机器名 */
  logExecuteHost: string
  /** 创建时间 */
  createTime: string
}

/** 定时任务日志分页结果 */
export type HbtQuartzLogPageResult = HbtPagedResult<HbtQuartzLogDto> 