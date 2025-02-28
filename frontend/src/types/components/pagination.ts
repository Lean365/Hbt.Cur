//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : pagination.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 分页组件类型定义
//===================================================================

import type { VNode } from 'vue'

/**
 * 分页组件属性接口
 */
export interface IHbtPaginationProps {
  /** 当前页码 */
  Current?: number
  /** 每页条数 */
  PageSize?: number
  /** 数据总数 */
  Total: number
  /** 是否显示快速跳转 */
  ShowQuickJumper?: boolean
  /** 是否显示每页条数选择器 */
  ShowSizeChanger?: boolean
  /** 是否显示总数 */
  ShowTotal?: (total: number, range: [number, number]) => VNode
  /** 是否禁用 */
  Disabled?: boolean
  /** 每页条数选项 */
  PageSizeOptions?: string[]
  /** 组件大小 */
  Size?: 'small' | 'default'
  /** 是否使用简单模式 */
  Simple?: boolean
  /** 对齐方式 */
  Align?: 'left' | 'center' | 'right'
  /** 是否显示较少页面内容 */
  Small?: boolean
  /** 用于自定义页码的结构 */
  ItemRender?: (opt: { page: number; type: 'page' | 'prev' | 'next' | 'jump-prev' | 'jump-next'; originalElement: any }) => any
  /** 当添加该属性时，显示为简单分页 */
  Responsive?: boolean
  /** 指定每页可以显示多少条 */
  DefaultPageSize?: number
  /** 指定默认的当前页数 */
  DefaultCurrent?: number
  /** 只有一页时是否隐藏分页器 */
  HideOnSinglePage?: boolean
  /** 当为「small」时，是否显示较少页面内容 */
  ShowLessItems?: boolean
  /** 主题模式 */
  Theme?: 'light' | 'dark'
}

/**
 * 分页组件事件接口
 */
export interface IHbtPaginationEvents {
  /**
   * 更新当前页码事件
   * @param page 新的页码
   */
  (e: 'update:Current', page: number): void
  /**
   * 更新每页条数事件
   * @param size 新的每页条数
   */
  (e: 'update:PageSize', size: number): void
  /**
   * 分页变更事件
   * @param page 新的页码
   * @param pageSize 新的每页条数
   */
  (e: 'change', page: number, pageSize: number): void
  /**
   * 页码改变的回调
   * @param page 改变后的页码
   * @param pageSize 每页条数
   */
  (e: 'PageChange', page: number, pageSize: number): void
  /**
   * 每页条数改变的回调
   * @param size 改变后的每页条数
   * @param current 当前页码
   */
  (e: 'SizeChange', size: number, current: number): void
  /**
   * 快速跳转时的回调
   * @param page 跳转的页码
   */
  (e: 'JumpTo', page: number): void
} 