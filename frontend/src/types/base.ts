/**
 * 通用状态枚举
 */
export enum HbtStatus {
  /** 正常 */
  Normal = 0,
  /** 停用 */
  Disabled = 1
}

/**
 * 是否枚举
 */
export enum HbtYesNo {
  /** 否 */
  No = 0,
  /** 是 */
  Yes = 1
}

/**
 * 菜单类型枚举
 */
export enum HbtMenuType {
  /** 目录 */
  Directory = 0,
  /** 菜单 */
  Menu = 1,
  /** 按钮 */
  Button = 2
}

/**
 * 显示状态枚举
 */
export enum HbtVisible {
  /** 显示 */
  Show = 0,
  /** 隐藏 */
  Hide = 1
}

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
 * 分页结果接口
 */
export interface PageResult<T> {
  /** 当前页码 */
  pageIndex: number
  /** 每页条数 */
  pageSize: number
  /** 数据行 */
  rows: T[]
  /** 总条数 */
  totalNum: number
  /** 总页数 */
  totalPage: number
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