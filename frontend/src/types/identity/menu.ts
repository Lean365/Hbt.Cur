import type { BaseEntity, BaseQuery } from '../base'
import type { HbtStatus, HbtYesNo, HbtMenuType, HbtVisible } from '../enums'

// 菜单查询参数
export interface MenuQuery extends BaseQuery {
  menuName?: string
  status?: HbtStatus
}

// 菜单对象
export interface Menu extends BaseEntity {
  menuName: string
  transKey?: string
  parentId?: number
  orderNum: number
  path?: string
  component?: string
  queryParams?: string
  isFrame: HbtYesNo
  isCache: HbtYesNo
  menuType: HbtMenuType
  visible: HbtVisible
  status: HbtStatus
  perms?: string
  icon?: string
  tenantId: number
  children?: Menu[]
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