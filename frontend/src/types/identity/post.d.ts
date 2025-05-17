import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 岗位查询参数
 */
export interface PostQuery extends HbtPagedQuery {
  postCode?: string;
  postName?: string;
  status?: number;
  beginTime?: string;
  endTime?: string;
}

/**
 * 岗位对象
 */
export interface Post extends HbtBaseEntity {
  postId: number;
  postCode: string;
  postName: string;
  postSort: number;
  status: number;
  tenantId: number;
  remark?: string;
}

/**
 * 创建岗位参数
 */
export interface PostCreate {
  postCode: string;
  postName: string;
  postSort: number;
  status: number;
  tenantId: number;
  remark?: string;
}

/**
 * 更新岗位参数
 */
export interface PostUpdate extends PostCreate {
  postId: number;
}

/**
 * 岗位状态更新参数
 */
export interface PostStatus {
  postId: number;
  status: number;
}

/**
 * 岗位分页结果
 */
export type PostPageResult = HbtPagedResult<Post>

/**
 * 岗位DTO
 */
export interface HbtPostDto {
  postId: number
  postName: string
  postCode: string
  postSort: number
  status: number
  remark: string
  createTime: string
  createBy: string
  updateTime: string
  updateBy: string
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