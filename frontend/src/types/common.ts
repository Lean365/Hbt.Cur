/**
 * 基础实体类型
 */
export interface HbtBaseEntity {
  /** 主键 */
  id: number;
  /** 创建者 */
  createBy: string;
  /** 创建时间 */
  createTime: string;
  /** 更新者 */
  updateBy?: string;
  /** 更新时间 */
  updateTime?: string;
  /** 删除者 */
  deleteBy?: string;
  /** 删除时间 */
  deleteTime?: string;
  /** 是否删除(0=未删除,1=已删除) */
  isDeleted: number;
  /** 备注 */
  remark?: string;
}

/**
 * 分页查询参数
 */
export interface HbtPageQuery {
  /** 当前页码 */
  pageIndex: number;
  /** 每页大小 */
  pageSize: number;
}

/**
 * 分页查询结果
 */
export interface HbtPagedResult<T> {
  /** 总记录数 */
  totalNum: number;
  /** 当前页码 */
  pageIndex: number;
  /** 每页大小 */
  pageSize: number;
  /** 数据列表 */
  rows: T[];
}

/**
 * API响应结果
 */
export interface HbtApiResult<T> {
  /** 状态码 */
  code: number;
  /** 消息 */
  msg: string;
  /** 数据 */
  data: T;
}

/**
 * 树形节点
 */
export interface HbtTreeNode {
  /** 节点ID */
  id: number;
  /** 节点标签 */
  label: string;
  /** 子节点 */
  children?: HbtTreeNode[];
  /** 其他属性 */
  [key: string]: any;
}

/**
 * 选择框选项
 */
export interface HbtSelectOption {
  /** 选项值 */
  value: string | number;
  /** 选项标签 */
  label: string;
  /** 值类型(string=字符串,number=数字) */
  valueType?: 'string' | 'number';
  /** 是否禁用 */
  disabled?: boolean;
  /** 其他属性 */
  [key: string]: any;
}

/**
 * 字典数据
 */
export interface HbtDictData {
  /** 字典标签 */
  dictLabel: string;
  /** 字典值 */
  dictValue: string;
  /** CSS类名 */
  cssClass?: string;
  /** 列表类名 */
  listClass?: string;
  /** 扩展标签 */
  extLabel?: string;
  /** 扩展值 */
  extValue?: string;
  /** 翻译键 */
  transKey?: string;
  /** 是否禁用 */
  disabled?: boolean;
}

/**
 * 字典类型
 */
export interface HbtDictType {
  /** 字典ID */
  dictId: number;
  /** 字典名称 */
  dictName: string;
  /** 字典类型 */
  dictType: string;
  /** 字典分类 */
  dictCategory: string;
  /** 状态 */
  status: number;
  /** 备注 */
  remark?: string;
  /** 创建时间 */
  createTime: string;
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