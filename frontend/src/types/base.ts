/**
 * 基础实体类型
 */
export interface BaseEntity {
  id: number
  createBy: string
  createTime: string
  updateBy?: string
  updateTime?: string
  deleteBy?: string
  deleteTime?: string
  isDeleted: number
  remark?: string
}

/**
 * 分页查询参数
 */
export interface PageQuery {
  pageNum: number
  pageSize: number
  beginTime?: string
  endTime?: string
}

/**
 * 分页响应结果
 */
export interface PageResult<T> {
  total: number
  rows: T[]
  pageNum: number
  pageSize: number
}

/**
 * API响应结果
 */
export interface ApiResult<T> {
  code: number
  msg: string
  data: T
}

/**
 * 树形节点
 */
export interface TreeNode {
  id: number
  label: string
  children?: TreeNode[]
  [key: string]: any
}

/**
 * 选择框选项
 */
export interface SelectOption {
  value: string | number
  label: string
  disabled?: boolean
  [key: string]: any
}

/**
 * 验证码结果
 */
export interface CaptchaResult {
  token: string
  offset: number
  success: boolean
  message?: string
}

/**
 * 基础查询参数
 */
export interface BaseQuery extends PageQuery {
  orderByColumn?: string
  isAsc?: 'asc' | 'desc'
  [key: string]: any
} 