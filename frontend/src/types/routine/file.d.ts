/** 文件相关类型定义 */

import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/** 文件查询参数 */
export interface HbtFileQueryDto extends HbtPagedQuery {
  /** 文件名称 */
  fileName?: string
  /** 文件类型 */
  fileType?: string
  /** 文件大小（字节） */
  fileSize?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 文件数据传输对象 */
export interface HbtFileDto extends HbtBaseEntity {
  /** ID */
  fileId: number | bigint
  /** 文件名称 */
  fileName: string
  /** 文件类型 */
  fileType: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件路径 */
  filePath: string
  /** 文件URL */
  fileUrl: string
  /** 文件MD5 */
  fileMd5: string
  /** 上传时间 */
  fileUploadTime?: Date
  /** 上传人ID */
  fileUploaderId: number | bigint
  /** 上传人名称 */
  fileUploaderName: string
}

/** 文件创建DTO */
export interface HbtFileCreateDto {
  /** 文件名称 */
  fileName: string
  /** 文件类型 */
  fileType: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件路径 */
  filePath: string
  /** 文件URL */
  fileUrl: string
  /** 文件MD5 */
  fileMd5: string
  /** 上传人ID */
  fileUploaderId: number | bigint
  /** 上传人名称 */
  fileUploaderName: string
}

/** 文件更新DTO */
export interface HbtFileUpdateDto extends HbtFileCreateDto {
  /** ID */
  fileId: bigint
}

/** 文件分页结果 */
export type HbtFilePagedResult = HbtPagedResult<HbtFileDto> 