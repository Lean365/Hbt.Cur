import type { BaseEntity, BaseQuery, HbtStatus, HbtYesNo, HbtMenuType, HbtVisible } from '../base'
import type { MenuProps } from 'ant-design-vue'

// 菜单查询参数
export interface MenuQuery extends BaseQuery {
  menuName?: string
  status?: HbtStatus
}

/**
 * 后端返回的菜单项类型
 */
export interface Menu {
  /** 菜单ID */
  menuId: string
  /** 父菜单ID */
  parentId: string | null
  /** 菜单名称 */
  name: string
  /** 翻译键 */
  transKey?: string
  /** 菜单图标 */
  icon?: string
  /** 菜单路由路径 */
  path: string
  /** 菜单组件路径 */
  component?: string
  /** 菜单类型 */
  type: HbtMenuType
  /** 菜单排序 */
  sort: number
  /** 是否禁用 */
  disabled?: boolean
  /** 是否隐藏 */
  hidden?: boolean
  /** 权限标识 */
  permission?: string
  /** 子菜单 */
  children?: Menu[]
}

/**
 * 前端菜单项类型
 */
export type MenuNode = Required<NonNullable<MenuProps['items']>>[number] & {
  /** 菜单ID */
  menuId: string
  /** 父菜单ID */
  parentId: string | null
  /** 菜单路由路径 */
  path: string
  /** 菜单组件路径 */
  component?: string
  /** 权限标识 */
  permission?: string
  /** 子菜单 */
  children?: MenuNode[]
}

// 创建菜单参数
export interface MenuCreate {
  menuName: string
  transKey?: string
  parentId?: number
  orderNum: number
  path?: string
  component?: string
  queryParams?: string
  isFrame?: HbtYesNo
  isCache?: HbtYesNo
  menuType: HbtMenuType
  visible?: HbtVisible
  status: HbtStatus
  perms?: string
  icon?: string
}

// 更新菜单参数
export interface MenuUpdate extends MenuCreate {
  menuId: number
}

// 菜单状态更新参数
export interface MenuStatus {
  menuId: number
  status: HbtStatus
}

// 菜单排序更新参数
export interface MenuOrder {
  menuId: number
  orderNum: number
}

/**
 * API响应结果类型
 */
export interface ApiResult<T> {
  /** 状态码 */
  code: number
  /** 消息 */
  msg: string
  /** 数据 */
  data: T
} 