import type { HbtPageQuery, HbtPagedResult } from '@/types/common'

/**
 * 代码生成配置DTO
 */
export interface HbtGenConfigDto {
  /** 主键ID */
  id: number;
  /** 表名 */
  tableName: string;
  /** 作者 */
  author: string;
  /** 模块名 */
  moduleName: string;
  /** 包名 */
  packageName: string;
  /** 业务名 */
  businessName: string;
  /** 功能名 */
  functionName: string;
  /** 生成类型 */
  genType: number;
  /** 生成模板 */
  genTemplate: string;
  /** 生成路径 */
  genPath: string;
  /** 选项配置 */
  options?: string;
  /** 租户ID */
  tenantId: number;
  /** 创建时间 */
  createTime?: string;
  /** 更新时间 */
  updateTime?: string;
}

/**
 * 代码生成配置查询参数
 */
export interface HbtGenConfigQuery extends HbtPageQuery {
  /** 表名 */
  tableName?: string;
  /** 作者 */
  author?: string;
  /** 模块名 */
  moduleName?: string;
  /** 业务名 */
  businessName?: string;
  /** 功能名 */
  functionName?: string;
  /** 生成类型 */
  genType?: number;
  /** 日期范围 */
  dateRange?: [string, string];
}

/**
 * 代码生成配置分页结果
 */
export type HbtGenConfigPagedResult = HbtPagedResult<HbtGenConfigDto> 