import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

/**
 * 部门对象
 */
export interface HbtDept extends HbtBaseEntity {
  /** 部门ID */
  deptId: number
  /** 部门名称 */
  deptName: string
  /** 父部门ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 负责人 */
  leader?: string
  /** 联系电话 */
  phone?: string
  /** 邮箱 */
  email?: string
  /** 用户数 */
  userCount: number
  /** 部门状态（0正常 1停用） */
  status: number
  /** 子部门列表 */
  children?: HbtDept[]
}

/**
 * 部门查询参数
 */
export interface HbtDeptQuery {
  pageIndex: number
  pageSize: number
  deptName?: string
  status?: number
  parentId?: number
}

/**
 * 创建部门参数
 */
export interface HbtDeptCreate {
  /** 部门名称 */
  deptName: string
  /** 父部门ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 负责人 */
  leader?: string
  /** 联系电话 */
  phone?: string
  /** 邮箱 */
  email?: string
  /** 用户数 */
  userCount: number
  /** 部门状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 更新部门参数
 */
export interface HbtDeptUpdate extends HbtDeptCreate {
  /** 部门ID */
  deptId: number
}

/**
 * 部门状态更新参数
 */
export interface HbtDeptStatus {
  /** 部门ID */
  deptId: number
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 部门导入模板
 */
export interface HbtDeptTemplate {
  /** 部门名称 */
  deptName: string
  /** 父部门名称 */
  parentDeptName: string
  /** 显示顺序 */
  orderNum: string
  /** 负责人 */
  leader: string
  /** 联系电话 */
  phone: string
  /** 邮箱 */
  email: string
  /** 部门状态 */
  status: string
  /** 备注 */
  remark: string
}

/**
 * 部门导入参数
 */
export interface HbtDeptImport {
  /** 部门名称 */
  deptName: string
  /** 父部门名称 */
  parentDeptName?: string
  /** 显示顺序 */
  orderNum: number
  /** 负责人 */
  leader?: string
  /** 联系电话 */
  phone?: string
  /** 邮箱 */
  email?: string
  /** 部门状态（0正常 1停用） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 部门导出参数
 */
export interface HbtDeptExport {
  /** 部门名称 */
  deptName: string
  /** 父部门ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 负责人 */
  leader: string
  /** 联系电话 */
  phone: string
  /** 邮箱 */
  email: string
  /** 用户数 */
  userCount: number
  /** 部门状态 */
  status: number
  /** 备注 */
  remark: string
  /** 创建时间 */
  createTime: string
}

/**
 * 部门分页结果
 */
export type HbtDeptPageResult = HbtPagedResult<HbtDept>

/**
 * 部门DTO
 */
export interface HbtDeptDto {
  /** 部门ID */
  deptId: number
  /** 部门名称 */
  deptName: string
  /** 父部门ID */
  parentId: number
  /** 显示顺序 */
  orderNum: number
  /** 负责人 */
  leader: string
  /** 联系电话 */
  phone: string
  /** 邮箱 */
  email: string
  /** 用户数 */
  userCount: number
  /** 部门状态 */
  status: number
  /** 备注 */
  remark: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
  /** 更新时间 */
  updateTime: string
  /** 更新者 */
  updateBy: string
  /** 子部门列表 */
  children: HbtDeptDto[]
}

/**
 * 部门选项
 */
export interface HbtDeptOption {
  label: string;
  value: number;
  children?: HbtDeptOption[];
}

/**
 * 用户部门DTO
 */
export interface HbtUserDeptDto {
  id: number;
  userId: number;
  deptId: number;
  deptName: string;
  deptCode: string;
  createTime: string;
  createBy: string;
}

/**
 * 部门信息
 */
export interface DeptInfo extends HbtBaseEntity {
  deptName: string;
  parentId: number;
  orderNum: number;
  leader: string;
  phone: string;
  email: string;
  userCount: number;
  status: number;
  children?: DeptInfo[];
}

/**
 * 部门树形节点
 */
export interface DeptTreeNode {
  id: number;
  label: string;
  children?: DeptTreeNode[];
}

/**
 * 部门导出参数
 */
export interface DeptExport {
  deptId: number;
  parentId: number;
  ancestors: string;
  deptName: string;
  orderNum: number;
  leader: string;
  phone: string;
  email: string;
  userCount: number;
  status: number;
} 