import type { BaseEntity } from '../base'

/**
 * 部门查询参数
 */
export interface DeptQuery {
  deptName?: string
  status?: string
}

/**
 * 部门信息
 */
export interface Dept {
  id: number
  deptName: string
  parentId?: number
  orderNum: number
  leader?: string
  phone?: string
  email?: string
  status: string
  children?: Dept[]
}

/**
 * 创建部门参数
 */
export interface DeptCreate {
  deptName: string
  parentId?: number
  orderNum: number
  leader?: string
  phone?: string
  email?: string
  status: string
}

/**
 * 更新部门参数
 */
export interface DeptUpdate extends DeptCreate {
  id: number
}

/**
 * 部门树形节点
 */
export interface DeptTreeNode {
  id: number
  label: string
  children?: DeptTreeNode[]
}

// 部门状态更新参数
export interface DeptStatus {
  deptId: number;
  status: string;
}

// 部门导出参数
export interface DeptExport {
  deptId: number;
  parentId: number;
  ancestors: string;
  deptName: string;
  orderNum: number;
  leader: string;
  phone: string;
  email: string;
  status: string;
} 