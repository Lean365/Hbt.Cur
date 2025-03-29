import type { HbtBaseQueryDto } from '../base'

/**
 * 异常日志查询参数
 */
export interface HbtErrorLogQueryDto extends HbtBaseQueryDto {
  /**
   * 异常时间范围-开始
   */
  startTime?: string

  /**
   * 异常时间范围-结束
   */
  endTime?: string

  /**
   * 异常等级
   */
  level?: string

  /**
   * 异常来源
   */
  source?: string
}

/**
 * 异常日志数据传输对象
 */
export interface HbtErrorLogDto {
  /**
   * 日志ID
   */
  errorLogId: number

  /**
   * 用户ID
   */
  userId: number

  /**
   * 用户名
   */
  userName: string

  /**
   * 异常等级
   */
  level: string

  /**
   * 异常来源
   */
  source: string

  /**
   * 异常消息
   */
  message: string

  /**
   * 堆栈跟踪
   */
  stackTrace: string

  /**
   * 异常时间
   */
  time: string
} 