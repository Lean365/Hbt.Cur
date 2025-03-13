import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 岗位查询参数
 */
export interface PostQuery extends HbtPageQuery {
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