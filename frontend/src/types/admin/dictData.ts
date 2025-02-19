// 字典数据查询参数
export interface DictDataQuery {
  pageNum?: number;
  pageSize?: number;
  dictType?: string;
  dictLabel?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 字典数据对象
export interface DictData {
  dictCode: number;
  dictSort: number;
  dictLabel: string;
  dictValue: string;
  dictType: string;
  cssClass?: string;
  listClass?: string;
  isDefault?: string;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建字典数据参数
export interface DictDataCreate {
  dictSort: number;
  dictLabel: string;
  dictValue: string;
  dictType: string;
  cssClass?: string;
  listClass?: string;
  isDefault?: string;
  status: string;
  remark?: string;
}

// 更新字典数据参数
export interface DictDataUpdate extends DictDataCreate {
  dictCode: number;
}

// 字典数据状态更新参数
export interface DictDataStatus {
  dictCode: number;
  status: string;
} 