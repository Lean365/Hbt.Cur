/** 法律法规相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 法律法规查询参数 */
export interface HbtLawQuery extends HbtPagedQuery {
  lawCode?: string
  lawName?: string
  lawTitle?: string
  lawType?: number
  lawLevel?: number
  lawStatus?: number
  importanceLevel?: number
  isMandatory?: number
  isPublic?: number
  needTraining?: number
  needExam?: number
  issuingAuthority?: string
  sourceAuthority?: string
  isTop?: number
  isRecommended?: number
  keywords?: string
  startTime?: Date
  endTime?: Date
}

/** 法律法规数据传输对象 */
export interface HbtLaw extends HbtBaseEntity {
  lawId: number
  parentId?: number
  orderNum: number
  lawCode: string
  lawName: string
  lawTitle: string
  lawType: number
  lawLevel: number
  lawDescription?: string
  lawPdfPath?: string
  lawVersion: string
  revisionVersion?: string
  issuingAuthority?: string
  issueDate?: Date
  effectiveDate?: Date
  expiryDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  sourceAuthority?: string
  downloadDate?: Date
  downloader?: string
  originalFilePath?: string
  originalFileSize?: number
  originalFileFormat?: string
  relatedLaws?: string
  relatedFiles?: string
  keywords?: string
  readCount: number
  downloadCount: number
  lawStatus: number
  isTop: number
  isRecommended: number
  parent?: HbtLaw
  children?: HbtLaw[]
}

/** 法律法规创建参数 */
export interface HbtLawCreate {
  parentId?: number
  orderNum: number
  lawCode: string
  lawName: string
  lawTitle: string
  lawType: number
  lawLevel: number
  lawDescription?: string
  lawPdfPath?: string
  lawVersion: string
  revisionVersion?: string
  issuingAuthority?: string
  issueDate?: Date
  effectiveDate?: Date
  expiryDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  sourceAuthority?: string
  downloadDate?: Date
  downloader?: string
  originalFilePath?: string
  originalFileSize?: number
  originalFileFormat?: string
  relatedLaws?: string
  relatedFiles?: string
  keywords?: string
  lawStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 法律法规更新参数 */
export interface HbtLawUpdate extends HbtLawCreate {
  lawId: number
}

/** 法律法规删除参数 */
export interface HbtLawDelete {
  lawId: number
}

/** 法律法规批量删除参数 */
export interface HbtLawBatchDelete {
  lawIds: number[]
}

/** 法律法规导入参数 */
export interface HbtLawImport {
  lawCode: string
  lawName: string
  lawTitle: string
  lawType: number
  lawLevel: number
  lawDescription?: string
  lawVersion: string
  issuingAuthority?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  sourceAuthority?: string
  downloadDate?: Date
  downloader?: string
  originalFilePath?: string
  originalFileSize?: number
  originalFileFormat?: string
  relatedLaws?: string
  relatedFiles?: string
  keywords?: string
  lawStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 法律法规导出参数 */
export interface HbtLawExport {
  lawCode: string
  lawName: string
  lawTitle: string
  lawType: number
  lawLevel: number
  lawDescription?: string
  lawVersion: string
  issuingAuthority?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  sourceAuthority?: string
  downloadDate?: Date
  downloader?: string
  originalFilePath?: string
  originalFileSize?: number
  originalFileFormat?: string
  relatedLaws?: string
  relatedFiles?: string
  keywords?: string
  lawStatus: number
  isTop: number
  isRecommended: number
  readCount: number
  downloadCount: number
  createTime: Date
}

/** 法律法规导入模板参数 */
export interface HbtLawTemplate {
  lawCode: string
  lawName: string
  lawTitle: string
  lawType: number
  lawLevel: number
  lawDescription?: string
  lawVersion: string
  issuingAuthority?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  sourceAuthority?: string
  downloadDate?: Date
  downloader?: string
  originalFilePath?: string
  originalFileSize?: number
  originalFileFormat?: string
  relatedLaws?: string
  relatedFiles?: string
  keywords?: string
  lawStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 法律法规分页结果 */
export type HbtLawPagedResult = HbtPagedResult<HbtLaw>

/** 法律法规树节点 */
export interface HbtLawTreeNode {
  lawId: number
  label: string
  parentId?: number
  orderNum: number
  lawCode: string
  lawName: string
  lawTitle: string
  lawType: number
  lawLevel: number
  lawStatus: number
  children?: HbtLawTreeNode[]
} 