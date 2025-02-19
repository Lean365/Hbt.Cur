// 岗位查询参数
export interface PostQuery {
  pageNum?: number;
  pageSize?: number;
  postCode?: string;
  postName?: string;
  status?: string;
  beginTime?: string;
  endTime?: string;
}

// 岗位对象
export interface Post {
  postId: number;
  postCode: string;
  postName: string;
  postSort: number;
  status: string;
  createBy: string;
  createTime: string;
  updateBy: string;
  updateTime: string;
  remark: string;
}

// 创建岗位参数
export interface PostCreate {
  postCode: string;
  postName: string;
  postSort: number;
  status: string;
  remark?: string;
}

// 更新岗位参数
export interface PostUpdate extends PostCreate {
  postId: number;
}

// 岗位状态更新参数
export interface PostStatus {
  postId: number;
  status: string;
} 