import type { HbtBaseQueryDto } from '../base'

/**
 * 任务日志查询参数
 */
export interface HbtTaskLogQueryDto extends HbtBaseQueryDto {
  /**
   * 开始时间范围-开始
   */
  startTime?: string

  /**
   * 开始时间范围-结束
   */
  endTime?: string

  /**
   * 任务状态
   */
  status?: number

  /**
   * 任务名称
   */
  taskName?: string
}

/**
 * 任务日志数据传输对象
 */
export interface HbtTaskLogDto {
  /**
   * 日志ID
   */
  taskLogId: number

  /**
   * 用户ID
   */
  userId: number

  /**
   * 用户名
   */
  userName: string

  /**
   * 任务名称
   */
  taskName: string

  /**
   * 开始时间
   */
  startTime: string

  /**
   * 结束时间
   */
  endTime: string

  /**
   * 执行耗时(毫秒)
   */
  duration: number

  /**
   * 任务状态
   */
  status: number

  /**
   * 执行消息
   */
  message: string
} 