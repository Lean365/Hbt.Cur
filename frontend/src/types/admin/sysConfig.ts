// 系统配置查询参数
export interface SysConfigQuery {
  pageNum?: number;
  pageSize?: number;
  configName?: string;
  configKey?: string;
  configType?: string;
  beginTime?: string;
  endTime?: string;
}

// 系统配置对象
export interface SysConfig {
  configId: number;
  configName: string;
  configKey: string;
  configValue: string;
  configType: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建系统配置参数
export interface SysConfigCreate {
  configName: string;
  configKey: string;
  configValue: string;
  configType: string;
  remark?: string;
}

// 更新系统配置参数
export interface SysConfigUpdate extends SysConfigCreate {
  configId: number;
} 