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
  /** 文件状态 */
  fileStatus?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 文件数据传输对象 */
export interface HbtFileDto extends HbtBaseEntity {
  /** ID */
  fileId: number
  /** 原始文件名 */
  fileOriginalName: string
  /** 文件扩展名 */
  fileExtension: string
  /** 文件名称 */
  fileName: string
  /** 文件路径 */
  filePath: string
  /** 文件类型 */
  fileType: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件存储类型 */
  fileStorageType: number
  /** 文件存储位置 */
  fileStorageLocation: string
  /** 文件访问URL */
  fileAccessUrl: string
  /** 文件MD5 */
  fileMd5: string | null
  /** 文件状态 */
  fileStatus: number
  /** 文件下载次数 */
  fileDownloadCount: number

}

/** 文件创建DTO */
export interface HbtFileCreateDto {
  /** 原始文件名 */
  fileOriginalName: string
  /** 文件扩展名 */
  fileExtension: string
  /** 文件名称 */
  fileName: string
  /** 文件路径 */
  filePath: string
  /** 文件类型 */
  fileType: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件存储类型 */
  fileStorageType: string
  /** 文件存储位置 */
  fileStorageLocation: string
  /** 文件访问URL */
  fileAccessUrl: string
  /** 文件MD5 */
  fileMd5: string
  /** 文件状态 */
  fileStatus: number
  /** 备注 */
  remark?: string
}

/** 文件更新DTO */
export interface HbtFileUpdateDto extends HbtFileCreateDto {
  /** ID */
  fileId: bigint
}

/** 文件上传DTO（与后端保持一致） */
export interface HbtFileUploadDto {
  /** 文件名称 */
  fileName: string
  /** 原始文件名 */
  fileOriginalName: string
  /** 文件扩展名 */
  fileExtension: string
  /** 文件路径 */
  filePath: string
  /** 文件类型 */
  fileType: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件存储类型 */
  fileStorageType: string | number
  /** 文件存储位置 */
  fileStorageLocation: string
  /** 文件访问URL */
  fileAccessUrl: string
  /** 文件MD5 */
  fileMd5: string
  /** 文件状态 */
  fileStatus: number
  /** 备注 */
  remark?: string
}

/** 文件分页结果 */
export type HbtFilePagedResult = HbtPagedResult<HbtFileDto> 