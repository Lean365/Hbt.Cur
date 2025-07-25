/** ISO标准文档相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** ISO标准文档查询参数 */
export interface HbtIsoDocumentQuery extends HbtPagedQuery {
  isoCode?: string
  isoTitle?: string
  isoType?: number
  isoLevel?: number
  isoStatus?: number
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

/** ISO标准文档数据传输对象 */
export interface HbtIsoDocument extends HbtBaseEntity {
  isoDocumentId: number
  parentId?: number
  orderNum: number
  isoCode: string
  isoTitle: string
  isoType: number
  isoLevel: number
  isoDescription?: string
  isoContent?: string
  isoPdfPath?: string
  isoVersion: string
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
  relatedDocuments?: string
  relatedFiles?: string
  keywords?: string
  readCount: number
  downloadCount: number
  isoStatus: number
  isTop: number
  isRecommended: number
  parent?: HbtIsoDocument
  children?: HbtIsoDocument[]
}

/** ISO标准文档创建参数 */
export interface HbtIsoDocumentCreate {
  parentId?: number
  orderNum: number
  isoCode: string
  isoTitle: string
  isoType: number
  isoLevel: number
  isoDescription?: string
  isoContent?: string
  isoPdfPath?: string
  isoVersion: string
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
  relatedDocuments?: string
  relatedFiles?: string
  keywords?: string
  isoStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** ISO标准文档更新参数 */
export interface HbtIsoDocumentUpdate extends HbtIsoDocumentCreate {
  isoDocumentId: number
}

/** ISO标准文档删除参数 */
export interface HbtIsoDocumentDelete {
  isoDocumentId: number
}

/** ISO标准文档批量删除参数 */
export interface HbtIsoDocumentBatchDelete {
  isoDocumentIds: number[]
}

/** ISO标准文档导入参数 */
export interface HbtIsoDocumentImport {
  isoCode: string
  isoTitle: string
  isoType: number
  isoLevel: number
  isoDescription?: string
  isoVersion: string
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
  isoStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** ISO标准文档导出参数 */
export interface HbtIsoDocumentExport {
  isoCode: string
  isoTitle: string
  isoType: number
  isoLevel: number
  isoDescription?: string
  isoVersion: string
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
  isoStatus: number
  isTop: number
  isRecommended: number
  readCount: number
  downloadCount: number
  createTime: Date
}

/** ISO标准文档导入模板参数 */
export interface HbtIsoDocumentTemplate {
  isoCode: string
  isoTitle: string
  isoType: number
  isoLevel: number
  isoDescription?: string
  isoVersion: string
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
  isoStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** ISO标准文档分页结果 */
export type HbtIsoDocumentPagedResult = HbtPagedResult<HbtIsoDocument>

/** ISO标准文档树节点 */
export interface HbtIsoDocumentTreeNode {
  isoDocumentId: number
  label: string
  parentId?: number
  orderNum: number
  isoCode: string
  isoTitle: string
  isoType: number
  isoLevel: number
  isoStatus: number
  children?: HbtIsoDocumentTreeNode[]
} 