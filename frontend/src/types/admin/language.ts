import type { BaseQuery } from '../base'
import type { HbtStatus } from '../enums'

// 语言查询参数
export interface LanguageQuery extends BaseQuery {
  langCode?: string;
  langName?: string;
  status?: HbtStatus;
}

// 语言对象
export interface Language {
  id: number;
  langCode: string;
  langName: string;
  icon?: string;
  status: HbtStatus;
  orderNum: number;
  remark?: string;
  createTime: string;
}

// 创建语言参数
export interface LanguageCreate {
  langCode: string;
  langName: string;
  icon?: string;
  status: HbtStatus;
  orderNum: number;
  remark?: string;
}

// 更新语言参数
export interface LanguageUpdate extends LanguageCreate {
  id: number;
}

// 语言状态更新参数
export interface LanguageStatus {
  langId: number;
  status: HbtStatus;
} 