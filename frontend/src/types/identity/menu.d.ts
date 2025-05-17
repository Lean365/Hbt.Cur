import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
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
 * 菜单基础类型
 */
export interface Menu extends HbtBaseEntity {
  menuName: string;
  transKey?: string;
  parentId?: number;
  orderNum: number;
  path: string;
  component: string;
  queryParams?: string;
  isExternal: number;
  isCache: number;
  menuType: number;
  visible: number;
  status: number;
  perms?: string;
  icon?: string;
  tenantId?: number;
  menuId: number;
  children?: Menu[];
}

/**
 * 菜单查询参数
 */
export interface MenuQuery extends HbtPagedQuery {
  menuName?: string;
  status?: number;
  menuType?: number;
  parentId?: number;
}

/**
 * 菜单创建参数
 */
export interface MenuCreate {
  menuName: string;
  transKey?: string;
  parentId?: number;
  orderNum: number;
  path?: string;
  component?: string;
  queryParams?: string;
  isExternal: number;
  isCache: number;
  menuType: number;
  visible: number;
  status: number;
  perms?: string;
  icon?: string;
  tenantId?: number;
}

/**
 * 菜单更新参数
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
 * 菜单表单类型
 */
export interface MenuForm {
  menuName: string;
  transKey?: string;
  parentId?: number;
  orderNum: number;
  path?: string;
  component?: string;
  queryParams?: string;
  isExternal: number;
  isCache: number;
  menuType: number;
  visible: number;
  status: number;
  perms?: string;
  icon?: string;
  tenantId?: number;
  menuId?: number;
}

/**
 * 菜单分页结果
 */
export type MenuPageResult = HbtPagedResult<Menu>

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
  perms?: string;
  /** 子菜单 */
  children?: MenuNode[];
}; 