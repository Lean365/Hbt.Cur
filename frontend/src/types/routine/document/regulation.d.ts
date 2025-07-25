/** 内部规章制度相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 内部规章制度查询参数 */
export interface HbtRegulationQuery extends HbtPagedQuery {
  regulationCode?: string
  regulationTitle?: string
  regulationType?: number
  regulationLevel?: number
  regulationStatus?: number
  importanceLevel?: number
  isMandatory?: number
  isPublic?: number
  needTraining?: number
  needExam?: number
  issuingDepartment?: string
  draftPerson?: string
  publisher?: string
  publishMethod?: number
  isTop?: number
  isRecommended?: number
  keywords?: string
  startTime?: Date
  endTime?: Date
}

/** 内部规章制度数据传输对象 */
export interface HbtRegulation extends HbtBaseEntity {
  regulationId: number
  parentId?: number
  orderNum: number
  regulationCode: string
  regulationTitle: string
  regulationType: number
  regulationLevel: number
  regulationDescription?: string
  regulationContent?: string
  regulationPdfPath?: string
  regulationVersion: string
  revisionVersion?: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  expiryDate?: Date
  publishDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  draftDate?: Date
  publisher?: string
  publishMethod: number
  publishScope?: string
  relatedRegulations?: string
  relatedFiles?: string
  keywords?: string
  readCount: number
  downloadCount: number
  regulationStatus: number
  isTop: number
  isRecommended: number
  parent?: HbtRegulation
  children?: HbtRegulation[]
}

/** 内部规章制度创建参数 */
export interface HbtRegulationCreate {
  parentId?: number
  orderNum: number
  regulationCode: string
  regulationTitle: string
  regulationType: number
  regulationLevel: number
  regulationDescription?: string
  regulationContent?: string
  regulationPdfPath?: string
  regulationVersion: string
  revisionVersion?: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  expiryDate?: Date
  publishDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  draftDate?: Date
  publisher?: string
  publishMethod: number
  publishScope?: string
  relatedRegulations?: string
  relatedFiles?: string
  keywords?: string
  regulationStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 内部规章制度更新参数 */
export interface HbtRegulationUpdate extends HbtRegulationCreate {
  regulationId: number
}

/** 内部规章制度删除参数 */
export interface HbtRegulationDelete {
  regulationId: number
}

/** 内部规章制度批量删除参数 */
export interface HbtRegulationBatchDelete {
  regulationIds: number[]
}

/** 内部规章制度导入参数 */
export interface HbtRegulationImport {
  regulationCode: string
  regulationTitle: string
  regulationType: number
  regulationLevel: number
  regulationDescription?: string
  regulationVersion: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  publisher?: string
  publishMethod: number
  keywords?: string
  regulationStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 内部规章制度导出参数 */
export interface HbtRegulationExport {
  regulationCode: string
  regulationTitle: string
  regulationType: number
  regulationLevel: number
  regulationDescription?: string
  regulationVersion: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  publisher?: string
  publishMethod: number
  keywords?: string
  regulationStatus: number
  isTop: number
  isRecommended: number
  readCount: number
  downloadCount: number
  createTime: Date
}

/** 内部规章制度导入模板参数 */
export interface HbtRegulationTemplate {
  regulationCode: string
  regulationTitle: string
  regulationType: number
  regulationLevel: number
  regulationDescription?: string
  regulationVersion: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  publisher?: string
  publishMethod: number
  keywords?: string
  regulationStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 内部规章制度分页结果 */
export type HbtRegulationPagedResult = HbtPagedResult<HbtRegulation>

/** 内部规章制度树节点 */
export interface HbtRegulationTreeNode {
  regulationId: number
  label: string
  parentId?: number
  orderNum: number
  regulationCode: string
  regulationTitle: string
  regulationType: number
  regulationLevel: number
  regulationStatus: number
  children?: HbtRegulationTreeNode[]
} 