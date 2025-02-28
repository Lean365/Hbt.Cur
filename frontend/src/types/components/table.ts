//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : table.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 表格组件类型定义
//===================================================================

import type { TableProps, TableColumnType } from 'ant-design-vue'

/**
 * 表格操作按钮配置
 */
export interface HbtTableAction {
  /** 操作键名 */
  key: string
  /** 操作名称 */
  label: string
  /** 操作类型 */
  type?: 'link' | 'primary' | 'danger'
  /** 图标组件 */
  icon?: any
  /** 权限标识 */
  permission?: string
  /** 确认提示 */
  confirm?: string
  /** 是否禁用 */
  disabled?: (record: any) => boolean
  /** 是否显示 */
  visible?: (record: any) => boolean
  /** 悬停提示 */
  tooltip?: string
}

/**
 * 操作列配置
 */
export interface HbtTableActionColumn {
  /** 列宽度 */
  width?: number
  /** 固定位置 */
  fixed?: 'left' | 'right'
  /** 操作按钮配置 */
  actions: HbtTableAction[]
}

/**
 * 表格列配置
 */
export interface HbtTableColumn extends Omit<TableColumnType, 'customRender'> {
  /** 列标题 */
  title: string
  /** 列数据字段 */
  dataIndex: string
  /** 列键名 */
  key?: string
  /** 列宽度 */
  width?: number
  /** 对齐方式 */
  align?: 'left' | 'center' | 'right'
  /** 固定位置 */
  fixed?: 'left' | 'right'
  /** 是否可排序 */
  sorter?: boolean | ((a: any, b: any) => number)
  /** 是否可筛选 */
  filters?: { text: string; value: string | number | boolean }[]
  /** 自定义渲染 */
  customRender?: (opt: { text: any; record: any; index: number }) => any
  /** 列提示 */
  tooltip?: string
  /** 是否可拖动调整宽度 */
  resizable?: boolean
  /** 是否可见 */
  visible?: boolean
  /** 导出时的列标题 */
  exportTitle?: string
  /** 是否允许导出 */
  exportable?: boolean
  /** 插槽名称 */
  slots?: {
    customRender?: string
  }
  /** 数据类型 */
  valueType?: 'text' | 'date' | 'datetime' | 'number' | 'percent' | 'money'
  /** 日期格式化模式 */
  dateFormat?: string
  /** 数字格式化选项 */
  numberFormat?: {
    /** 小数位数 */
    precision?: number
    /** 千分位分隔符 */
    separator?: string
    /** 前缀 */
    prefix?: string
    /** 后缀 */
    suffix?: string
  }
}

/**
 * 分页配置
 */
export interface HbtTablePagination {
  /** 当前页码 */
  pageNum: number
  /** 每页条数 */
  pageSize: number
  /** 总条数 */
  totalNum: number
  /** 是否显示每页条数选择器 */
  showSizeChanger?: boolean
  /** 是否显示快速跳转 */
  showQuickJumper?: boolean
  /** 总条数显示方法 */
  showTotal?: (total: number) => string
}

/**
 * 工具栏按钮配置
 */
export interface HbtTableToolbarButton {
  /** 按钮键名 */
  key: string
  /** 按钮文本 */
  label: string
  /** 按钮类型 */
  type?: 'link' | 'primary' | 'default' | 'dashed' | 'text'
  /** 是否危险按钮 */
  danger?: boolean
  /** 图标组件 */
  icon?: any
  /** 权限标识 */
  permission?: string
  /** 是否禁用 */
  disabled?: boolean
  /** 加载状态 */
  loading?: boolean
  /** 是否显示 */
  visible?: boolean
  /** 悬停提示 */
  tooltip?: string
}

/**
 * 表格属性
 */
export interface HbtTableProps extends Omit<TableProps, 'columns' | 'pagination'> {
  /** 表格列配置 */
  columns: HbtTableColumn[]
  /** 数据源 */
  dataSource: any[]
  /** 加载状态 */
  loading?: boolean
  /** 是否显示序号列 */
  showIndex?: boolean
  /** 序号列配置 */
  indexColumn?: Partial<HbtTableColumn>
  /** 操作列配置 */
  actionColumn?: HbtTableActionColumn
  /** 是否显示选择列 */
  showSelection?: boolean
  /** 选择列配置 */
  rowSelection?: TableProps['rowSelection']
  /** 分页配置 */
  pagination?: HbtTablePagination | false
  /** 表格大小 */
  size?: 'small' | 'middle' | 'large'
  /** 是否显示边框 */
  bordered?: boolean
  /** 滚动配置 */
  scroll?: {
    x?: number | string
    y?: number | string
  }
  /** 是否显示工具栏 */
  showToolbar?: boolean
  /** 工具栏按钮配置 */
  toolbarButtons?: HbtTableToolbarButton[]
  /** 是否开启虚拟滚动 */
  virtual?: boolean
  /** 虚拟滚动配置 */
  virtualConfig?: {
    /** 每页加载的数据量 */
    pageSize?: number
    /** 缓冲区域大小 */
    buffer?: number
    /** 行高 */
    rowHeight?: number
    /** 是否开启动态高度 */
    dynamicHeight?: boolean
  }
  /** 是否开启懒加载 */
  lazyLoad?: boolean
  /** 懒加载配置 */
  lazyLoadConfig?: {
    /** 触发加载的距离 */
    threshold?: number
    /** 是否显示加载更多按钮 */
    showLoadMore?: boolean
    /** 加载更多文本 */
    loadMoreText?: string
  }
}

/**
 * 表格事件
 */
export interface HbtTableEvents {
  /** 分页、排序、筛选变化时的回调 */
  onChange?: (pagination: any, filters: any, sorter: any) => void
  /** 选择项变化时的回调 */
  onSelect?: (selectedRowKeys: any[], selectedRows: any[]) => void
  /** 操作按钮点击时的回调 */
  onAction?: (action: string, record: any) => void
}

/**
 * 表格实例方法
 */
export interface HbtTableInstance {
  /** 刷新表格数据 */
  refresh: () => void
  /** 重置表格状态 */
  reset: () => void
  /** 获取选中行 */
  getSelectedRows: () => any[]
  /** 清空选中行 */
  clearSelection: () => void
} 