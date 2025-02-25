//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : query.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 查询组件类型定义
//===================================================================

import type { Rule } from 'ant-design-vue/es/form'

/**
 * 查询项配置
 */
export interface IHbtQueryItem {
  /** 标签名称 */
  label: string
  /** 字段名 */
  name: string
  /** 控件类型 */
  type: 'input' | 'select' | 'date' | 'dateRange' | 'number'
  /** 下拉选项 */
  options?: Array<{ label: string; value: any }>
  /** 占位提示 */
  placeholder?: string | string[]
  /** 默认值 */
  defaultValue?: any
  /** 校验规则 */
  rules?: Rule[]
  /** 日期格式 */
  format?: string
}

/**
 * 布局配置
 */
export interface IHbtQueryLayout {
  /** 标签宽度 */
  labelWidth?: number
  /** 每行显示几个查询项 */
  span?: number
  /** 标签对齐方式 */
  labelAlign?: 'left' | 'right'
}

/**
 * 按钮配置
 */
export interface IHbtQueryButtons {
  /** 是否显示搜索按钮 */
  search?: boolean
  /** 是否显示重置按钮 */
  reset?: boolean
  /** 是否显示展开/收起按钮 */
  collapse?: boolean
}

/**
 * 组件属性
 */
export interface IHbtQueryProps {
  /** 查询项配置 */
  items: IHbtQueryItem[]
  /** 布局配置 */
  layout?: Partial<IHbtQueryLayout>
  /** 按钮配置 */
  buttons?: Partial<IHbtQueryButtons>
  /** 默认显示几个查询项 */
  showCount?: number
  /** 加载状态 */
  loading?: boolean
}

/**
 * 组件实例方法
 */
export interface IHbtQueryInstance {
  /** 重置表单 */
  reset: () => void
  /** 设置表单值 */
  setFieldsValue: (values: Record<string, any>) => void
  /** 获取表单值 */
  getFieldsValue: () => Record<string, any>
  /** 校验表单 */
  validate: () => Promise<Record<string, any>>
} 