import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** SQL差异日志记录 */
export interface HbtSqlDiffLogDto extends HbtBaseEntity {
  sqlDiffLogId: number
  diffType: string
  tableName: string
  businessName?: string
  primaryKey?: string
  beforeData?: string
  afterData?: string
  diffFields?: string
  executeSql?: string
  sqlParameters?: string
}

/** SQL差异日志查询参数 */
export interface HbtSqlDiffLogQueryDto extends HbtPagedQuery {
  diffType?: string
  tableName?: string
  businessName?: string
  primaryKey?: string
  startTime?: string
  endTime?: string
}

/** SQL差异日志导出参数 */
export interface HbtSqlDiffLogExportDto {
  sqlDiffLogId: number
  diffType: string
  tableName: string
  businessName?: string
  primaryKey?: string
  beforeData?: string
  afterData?: string
  diffFields?: string
  executeSql?: string
  sqlParameters?: string
  createTime: string
  createBy: string
  updateTime: string
  updateBy: string
}

/** SQL差异日志分页结果 */
export type HbtSqlDiffLogPageResult = HbtPagedResult<HbtSqlDiffLogDto> 