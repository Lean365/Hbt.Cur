/** 工作区项目 */
export interface HbtWorkplaceProject {
  /** 项目ID */
  id: string
  /** 项目名称 */
  name: string
  /** 项目描述 */
  description: string
  /** 项目路径 */
  path: string
  /** 创建时间 */
  createTime: string
  /** 更新时间 */
  updateTime: string
  /** 状态（0-禁用，1-启用） */
  status: number
}

/** 工作区项目查询参数 */
export interface HbtWorkplaceProjectQuery {
  /** 项目名称 */
  name?: string
  /** 状态 */
  status?: number
  /** 页码 */
  pageIndex: number
  /** 每页条数 */
  pageSize: number
}

/** 工作区项目分页结果 */
export interface HbtWorkplaceProjectPageResult {
  /** 总条数 */
  total: number
  /** 数据列表 */
  items: HbtWorkplaceProject[]
} 