import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { User } from '@/types/identity/user'


/**
 * 岗位对象
 */
export interface HbtPost extends HbtBaseEntity {
  /** 岗位ID */
  postId: number
  /** 租户ID */
  tenantId: number
  /** 岗位编码 */
  postCode: string
  /** 岗位名称 */
  postName: string
  /** 岗位排序 */
  rank: number
  /** 排序 */
  orderNum: number
  /** 用户数 */
  userCount: number
  /** 状态 */
  status: number

}

/**
 * 岗位查询参数
 */
export interface HbtPostQuery extends HbtPagedQuery {
  postCode?: string;
  postName?: string;
  status?: number;
  beginTime?: string;
  endTime?: string;
}

/**
 * 创建岗位参数
 */
export interface HbtPostCreate {
  postCode: string;
  postName: string;
  rank: number;
  orderNum: number;
  userCount: number;
  status: number;
  remark?: string;
  tenantId: number;
}

/**
 * 更新岗位参数
 */
export interface HbtPostUpdate extends HbtPostCreate {
  postId: number;
}

/**
 * 岗位模板
 */
export interface HbtPostTemplate {
  postCode: string;
  postName: string;
  rank: string;
  orderNum: string;
  userCount: string;
  status: string;
  remark: string;
}

/**
 * 岗位导入参数
 */
export interface HbtPostImport {
  postCode: string;
  postName: string;
  rank: number;
  orderNum: number;
  userCount: number;
  status: number;
  remark?: string;
}

/**
 * 岗位导出参数
 */
export interface HbtPostExport {
  postCode: string;
  postName: string;
  rank: number;
  orderNum: number;
  userCount: number;
  status: number;
  remark?: string;
  createTime: string;
}

/**
 * 岗位状态更新参数
 */
export interface HbtPostStatus {
  postId: number;
  status: number;
}

/**
 * 岗位分页结果
 */
export type HbtPostPageResult = HbtPagedResult<HbtPost>

/**
 * 岗位DTO
 */
export interface HbtPostDto {
  postId: number;
  postCode: string;
  postName: string;
  rank: number;
  orderNum: number;
  userCount: number;
  status: number;
  remark?: string;
  tenantId: number;
  createTime: string;
  createBy: string;
  updateTime: string;
  updateBy: string;
}

/**
 * 岗位选项
 */
export interface HbtPostOption {
  label: string;
  value: number;
}

/**
 * 用户岗位DTO
 */
export interface HbtUserPostDto {
  id: number
  userId: number
  postId: number
  postName: string
  postCode: string
  createTime: string
  createBy: string
} 