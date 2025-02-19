// 字典类型查询参数
export interface DictTypeQuery {
  pageNum?: number;
  pageSize?: number;
  dictName?: string;
  dictType?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 字典类型对象
export interface DictType {
  dictId: number;
  dictName: string;
  dictType: string;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建字典类型参数
export interface DictTypeCreate {
  dictName: string;
  dictType: string;
  status: string;
  remark?: string;
}

// 更新字典类型参数
export interface DictTypeUpdate extends DictTypeCreate {
  dictId: number;
}

// 字典类型状态更新参数
export interface DictTypeStatus {
  dictId: number;
  status: string;
}

// 字典类型导入参数
export interface DictTypeImport {
  dictName: string;
  dictType: string;
  status: string;
  remark?: string;
} 