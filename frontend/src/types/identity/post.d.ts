import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'
import type { User } from '@/types/identity/user'


/**
 * 岗位实体
 */
export interface HbtPost extends HbtBaseEntity {
  /** 岗位ID */
  postId: number
  /** 岗位编码 */
  postCode: string
  /** 岗位名称 */
  postName: string
  /** 显示顺序 */
  postSort: number
  /** 状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark: string
}

/**
 * 岗位查询参数
 */
export interface HbtPostQuery extends HbtPagedQuery {
  /** 岗位编码 */
  postCode?: string
  /** 岗位名称 */
  postName?: string
  /** 状态 */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 岗位创建参数
 */
export interface HbtPostCreate extends Omit<HbtPost, 'postId' | 'createTime' | 'updateTime'> {}

/**
 * 岗位更新参数
 */
export interface HbtPostUpdate extends Partial<Omit<HbtPost, 'postId' | 'createTime' | 'updateTime'>> {
  /** 岗位ID */
  postId: number
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
  /** 岗位编码 */
  postCode: string
  /** 岗位名称 */
  postName: string
  /** 显示顺序 */
  postSort: number
  /** 状态 */
  status: number
  /** 备注 */
  remark: string
}

/**
 * 岗位导出参数
 */
export interface HbtPostExport extends HbtPostQuery {
  /** 导出类型（1-Excel，2-CSV） */
  exportType: number
}

/**
 * 岗位状态更新参数
 */
export interface HbtPostStatusUpdate {
  /** 岗位ID */
  postId: number
  /** 状态 */
  status: number
}

/**
 * 岗位分页结果
 */
export interface HbtPostPagedResult extends HbtPagedResult<HbtPost> {}

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
  createTime: string;
  createBy: string;
  updateTime: string;
  updateBy: string;
}

/**
 * 岗位选项
 */
export interface HbtPostOption {
  /** 岗位ID */
  value: number
  /** 岗位名称 */
  label: string
  /** 岗位编码 */
  postCode: string
  /** 显示顺序 */
  postSort: number
  /** 状态 */
  status: number
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

/**
 * 岗位树形结构
 */
export interface HbtPostTree {
  /** 岗位ID */
  postId: number
  /** 岗位名称 */
  postName: string
  /** 岗位编码 */
  postCode: string
  /** 显示顺序 */
  postSort: number
  /** 状态 */
  status: number
  /** 子岗位 */
  children?: HbtPostTree[]
}

/**
 * 岗位分配用户参数
 */
export interface HbtPostAssignUser {
  /** 岗位ID */
  postId: number
  /** 用户ID列表 */
  userIds: number[]
}

/**
 * 岗位取消分配用户参数
 */
export interface HbtPostUnassignUser {
  /** 岗位ID */
  postId: number
  /** 用户ID列表 */
  userIds: number[]
}

/**
 * 岗位统计信息
 */
export interface HbtPostStatistics {
  /** 总岗位数 */
  totalPosts: number
  /** 启用岗位数 */
  enabledPosts: number
  /** 停用岗位数 */
  disabledPosts: number
  /** 今日新增岗位数 */
  todayNewPosts: number
  /** 本月新增岗位数 */
  monthNewPosts: number
} 