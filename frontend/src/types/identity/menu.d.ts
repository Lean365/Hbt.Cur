import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult, HbtApiResponse } from '@/types/common'
import type { MenuProps } from 'ant-design-vue'

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
 * 菜单查询参数
 */
export interface MenuQuery extends HbtPagedQuery {
  menuName?: string;
  status?: number;
}

/**
 * 后端返回的菜单项类型
 */
export interface Menu {
  menuId: number
  menuName: string
  menuType: number
  parentId?: number
  path: string
  component: string
  query?: string
  icon?: string
  title?: string
  isLink?: boolean
  isHide?: boolean
  isFull?: boolean
  isAffix?: boolean
  isKeepAlive?: boolean
  status?: number
  permission?: string
  orderNum?: number
  createTime?: string
  children?: Menu[]
  hidden?: boolean
  keepAlive?: boolean
  redirect?: string
  remark?: string
}

/**
 * 前端菜单项类型
 */
export type MenuNode = Required<NonNullable<MenuProps['items']>>[number] & {
  /** 菜单ID */
  menuId: string;
  /** 父菜单ID */
  parentId: string | null;
  /** 菜单路由路径 */
  path: string;
  /** 菜单组件路径 */
  component?: string;
  /** 权限标识 */
  permission?: string;
  /** 子菜单 */
  children?: MenuNode[];
};

/**
 * 创建菜单参数
 */
export interface MenuCreate {
  menuName: string;
  transKey?: string;
  parentId?: number;
  orderNum: number;
  path?: string;
  component?: string;
  queryParams?: string;
  /** 是否外链（0=否 1=是） */
  isFrame?: number;
  /** 是否缓存（0=不缓存 1=缓存） */
  isCache?: number;
  /** 菜单类型（1=目录 2=菜单 3=按钮） */
  menuType: number;
  /** 是否可见（0=显示 1=隐藏） */
  visible?: number;
  /** 状态（0=正常 1=停用） */
  status: number;
  perms?: string;
  icon?: string;
}

/**
 * 更新菜单参数
 */
export interface MenuUpdate extends MenuCreate {
  menuId: number;
}

/**
 * 菜单状态更新参数
 */
export interface MenuStatus {
  menuId: number;
  status: number;
}

/**
 * 菜单排序更新参数
 */
export interface MenuOrder {
  menuId: number;
  orderNum: number;
}

/**
 * 菜单分页结果
 */
export type MenuPageResult = HbtPagedResult<Menu> 