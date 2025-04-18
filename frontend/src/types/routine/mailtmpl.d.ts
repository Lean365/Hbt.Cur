/** 邮件模板相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 邮件模板查询参数 */
export interface HbtMailTemplateQueryDto extends HbtPagedQuery {
  /** 模板名称 */
  templateName?: string
  /** 模板类型（1系统模板 2自定义模板） */
  templateType?: number
  /** 模板状态（0禁用 1启用） */
  templateStatus?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 邮件模板数据传输对象 */
export interface HbtMailTemplateDto extends HbtBaseEntity {
  /** ID */
  templateId: number | bigint
  /** 模板名称 */
  templateName: string
  /** 模板类型（1系统模板 2自定义模板） */
  templateType: number
  /** 模板状态（0禁用 1启用） */
  templateStatus: number
  /** 模板标题 */
  templateSubject: string
  /** 模板内容 */
  templateContent: string
  /** 模板参数 */
  templateParams?: string
  /** 创建时间 */
  templateCreateTime?: Date
  /** 更新时间 */
  templateUpdateTime?: Date
  /** 创建人ID */
  templateCreatorId: number | bigint
  /** 创建人名称 */
  templateCreatorName: string
}

/** 邮件模板创建DTO */
export interface HbtMailTemplateCreateDto {
  /** 模板名称 */
  templateName: string
  /** 模板类型（1系统模板 2自定义模板） */
  templateType: number
  /** 模板状态（0禁用 1启用） */
  templateStatus: number
  /** 模板标题 */
  templateSubject: string
  /** 模板内容 */
  templateContent: string
  /** 模板参数 */
  templateParams?: string
  /** 创建人ID */
  templateCreatorId: number | bigint
  /** 创建人名称 */
  templateCreatorName: string
}

/** 邮件模板更新DTO */
export interface HbtMailTemplateUpdateDto extends HbtMailTemplateCreateDto {
  /** ID */
  templateId: bigint
}

/** 邮件模板分页结果 */
export type HbtMailTemplatePagedResult = HbtPagedResult<HbtMailTemplateDto> 