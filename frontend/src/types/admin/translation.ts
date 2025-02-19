// 翻译查询参数
export interface TranslationQuery {
  pageNum?: number;
  pageSize?: number;
  langCode?: string;
  module?: string;
  key?: string;
  value?: string;
  beginTime?: string;
  endTime?: string;
}

// 翻译对象
export interface Translation {
  id: number;
  langCode: string;
  module: string;
  key: string;
  value: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建翻译参数
export interface TranslationCreate {
  langCode: string;
  module: string;
  key: string;
  value: string;
  remark?: string;
}

// 更新翻译参数
export interface TranslationUpdate extends TranslationCreate {
  id: number;
}

// 翻译导入参数
export interface TranslationImport {
  file: File;
  updateSupport?: boolean;
}

// 翻译导出参数
export interface TranslationExport extends TranslationQuery {
  orderByColumn?: string;
  isAsc?: string;
}

// 翻译状态更新参数
export interface TranslationStatus {
  translationId: number;
  status: string;
} 