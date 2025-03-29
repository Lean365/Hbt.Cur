/**
 * 分页结果接口
 */
export interface HbtPagedResult<T> {
  /**
   * 总记录数
   */
  total: number

  /**
   * 当前页码
   */
  pageNum: number

  /**
   * 每页记录数
   */
  pageSize: number

  /**
   * 数据列表
   */
  items: T[]
}

/**
 * 基础查询参数
 */
export interface HbtBaseQueryDto {
  /**
   * 页码
   */
  pageNum: number

  /**
   * 每页记录数
   */
  pageSize: number

  /**
   * 用户ID
   */
  userId?: number
} 