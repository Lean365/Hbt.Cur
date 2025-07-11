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
 * 菜单对象
 */
export interface HbtMenu extends HbtBaseEntity {
  /** 菜单ID */
  menuId: number
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey?: string
  /** 菜单图标 */
  icon?: string
  /** 父菜单ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component?: string
  /** 路由参数 */
  queryParams?: string
  /** 是否为外链（0否 1是） */
  isExternal: number
  /** 是否缓存（0否 1是） */
  isCache: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType: number
  /** 菜单状态（0显示 1隐藏） */
  visible: number
  /** 权限标识 */
  perms?: string  
  /** 菜单状态（0正常 1停用） */
  status: number
  /** 子菜单 */
  children?: HbtMenu[]
}

/**
 * 菜单查询参数
 */
export interface HbtMenuQuery extends HbtPagedQuery {
  /** 菜单名称 */
  menuName?: string
  /** 菜单状态（0正常 1停用） */
  status?: number
  /** 菜单状态（0显示 1隐藏） */
  visible?: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType?: number
  /** 是否为外链（0否 1是） */
  isExternal?: number
  /** 是否缓存（0否 1是） */
  isCache?: number
}

/**
 * 菜单树形查询参数
 */
export interface HbtMenuTreeQuery {
  /** 菜单名称 */
  menuName?: string
  /** 菜单状态（0正常 1停用） */
  status?: number
  /** 菜单状态（0显示 1隐藏） */
  visible?: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType?: number
  /** 是否为外链（0否 1是） */
  isExternal?: number
  /** 是否缓存（0否 1是） */
  isCache?: number
  /** 父菜单ID */
  parentId?: number
}

/**
 * 创建菜单参数
 */
export interface HbtMenuCreate {
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey?: string
  /** 父菜单ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component?: string
  /** 路由参数 */
  queryParams?: string
  /** 是否为外链（0否 1是） */
  isExternal: number
  /** 是否缓存（0否 1是） */
  isCache: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType: number
  /** 菜单状态（0显示 1隐藏） */
  visible: number
  /** 菜单状态（0正常 1停用） */
  status: number
  /** 权限标识 */
  perms?: string
  /** 菜单图标 */
  icon?: string
  /** 备注 */
  remark?: string
}

/**
 * 更新菜单参数
 */
export interface HbtMenuUpdate extends HbtMenuCreate {
  /** 菜单ID */
  menuId: number
}

/**
 * 菜单状态更新参数
 */
export interface HbtMenuStatus {
  /** 菜单ID */
  menuId: number
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 菜单排序更新参数
 */
export interface HbtMenuOrder {
  /** 菜单ID */
  menuId: number
  /** 显示顺序 */
  orderNum: number
}

/**
 * 菜单导入模板
 */
export interface HbtMenuTemplate {
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey: string
  /** 父菜单名称 */
  parentMenuName: string
  /** 显示顺序 */
  orderNum: string
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component: string
  /** 路由参数 */
  queryParams: string
  /** 是否为外链 */
  isExternal: string
  /** 是否缓存 */
  isCache: string
  /** 菜单类型 */
  menuType: string
  /** 菜单状态 */
  visible: string
  /** 状态 */
  status: string
  /** 权限标识 */
  perms: string
  /** 菜单图标 */
  icon: string
  /** 备注 */
  remark: string
}

/**
 * 菜单导入参数
 */
export interface HbtMenuImport {
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey?: string
  /** 父菜单名称 */
  parentMenuName?: string
  /** 显示顺序 */
  orderNum: number
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component?: string
  /** 路由参数 */
  queryParams?: string
  /** 是否为外链（0否 1是） */
  isExternal: number
  /** 是否缓存（0否 1是） */
  isCache: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType: number
  /** 菜单状态（0显示 1隐藏） */
  visible: number
  /** 菜单状态（0正常 1停用） */
  status: number
  /** 权限标识 */
  perms?: string
  /** 菜单图标 */
  icon?: string
  /** 备注 */
  remark?: string
}

/**
 * 菜单导出参数
 */
export interface HbtMenuExport {
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey: string
  /** 父菜单ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component: string
  /** 路由参数 */
  queryParams: string
  /** 是否为外链 */
  isExternal: number
  /** 是否缓存 */
  isCache: number
  /** 菜单类型 */
  menuType: number
  /** 菜单状态 */
  visible: number
  /** 状态 */
  status: number
  /** 权限标识 */
  perms: string
  /** 菜单图标 */
  icon: string
  /** 备注 */
  remark: string
  /** 创建时间 */
  createTime: string
}

/**
 * 菜单分页结果
 */
export type HbtMenuPageResult = HbtPagedResult<HbtMenu>

/**
 * 菜单DTO
 */
export interface HbtMenuDto {
  /** 菜单ID */
  menuId: number
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey: string
  /** 父菜单ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component: string
  /** 路由参数 */
  queryParams: string
  /** 是否为外链 */
  isExternal: number
  /** 是否缓存 */
  isCache: number
  /** 菜单类型 */
  menuType: number
  /** 菜单状态 */
  visible: number
  /** 状态 */
  status: number
  /** 权限标识 */
  perms: string
  /** 菜单图标 */
  icon: string
  /** 备注 */
  remark: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
  /** 更新时间 */
  updateTime: string
  /** 更新者 */
  updateBy: string
  /** 子菜单 */
  children: HbtMenuDto[]
}

/**
 * 菜单选项
 */
export interface HbtMenuOption {
  /** 标签 */
  label: string
  /** 值 */
  value: number
  /** 子菜单 */
  children?: HbtMenuOption[]
}

/**
 * 用户菜单DTO
 */
export interface HbtUserMenuDto {
  id: number;
  userId: number;
  menuId: number;
  menuName: string;
  menuCode: string;
  createTime: string;
  createBy: string;
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
  perms?: string;
  /** 子菜单 */
  children?: MenuNode[];
}; 