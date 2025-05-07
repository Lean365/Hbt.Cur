//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : hbtConfig.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置类型定义
//===================================================================

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 系统配置实体
 */
export interface HbtConfig extends HbtBaseEntity {
  /** 配置ID */
  configId: number
  /** 配置名称 */
  configName: string
  /** 配置键名 */
  configKey: string
  /** 配置键值 */
  configValue: string
  /** 系统内置（0否 1是） */
  isBuiltin: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark: string
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 系统配置查询参数
 */
export interface HbtConfigQuery extends HbtPagedQuery {
  /** 配置名称 */
  configName?: string
  /** 配置键名 */
  configKey?: string
  /** 配置键值 */
  configValue?: string
  /** 系统内置（0否 1是） */
  isBuiltin: number
  /** 状态（0正常 1停用） */
  status: number
  /** 是否加密（0否 1是） */
  isEncrypted: number
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  orderType?: 'asc' | 'desc'
}

/**
 * 系统配置创建参数
 */
export interface HbtConfigCreate {
  /** 配置名称 */
  configName: string
  /** 配置键名 */
  configKey: string
  /** 配置键值 */
  configValue: string
  /** 系统内置 */
  isBuiltin: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark?: string
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 系统配置更新参数
 */
export interface HbtConfigUpdate extends HbtConfigCreate {
  /** 配置ID */
  configId: number
}

/**
 * 批量删除系统配置参数
 */
export interface HbtConfigBatchDelete {
  /** 配置ID数组 */
  configIds: number[]
}

/**
 * 更新系统配置状态参数
 */
export interface HbtConfigStatusUpdate {
  /** 配置ID */
  configId: number
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 导入系统配置参数
 */
export interface HbtConfigImport {
  /** 文件对象 */
  file: File
  /** 工作表名称 */
  sheetName?: string
}

/**
 * 导出系统配置参数
 */
export interface HbtConfigExport extends HbtConfigQuery {
  /** 工作表名称 */
  sheetName?: string
}

/**
 * 系统配置项
 */
export interface HbtConfigItem {
  /** 配置ID */
  id: number
  /** 配置名称 */
  configName: string
  /** 配置键 */
  configKey: string
  /** 配置值 */
  configValue: string
  /** 是否系统配置 */
  isSystem: boolean
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
  /** 创建时间 */
  createTime?: string
  /** 更新时间 */
  updateTime?: string
}

/**
 * 系统配置分页结果
 */
export type HbtConfigPageResult = HbtPagedResult<HbtConfig>

/**
 * 导入系统配置结果
 */
export interface HbtConfigImportResult {
  /** 是否成功 */
  success: boolean
  /** 消息 */
  message: string
  /** 成功数量 */
  successCount?: number
  /** 失败数量 */
  failureCount?: number
} 