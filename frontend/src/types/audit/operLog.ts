import type { HbtBaseEntity, HbtPageQuery } from '@/types/common'

/**
 * 操作日志查询参数
 */
export interface OperLogQuery extends HbtPageQuery {
  /** 模块标题 */
  title?: string
  /** 操作人员 */
  operName?: string
  /** 业务类型（0其它 1新增 2修改 3删除） */
  businessType?: number
  /** 状态（0正常 1异常） */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 操作日志对象
 */
export interface OperLog extends HbtBaseEntity {
  /** 日志ID */
  operId: number
  /** 模块标题 */
  title: string
  /** 业务类型（0其它 1新增 2修改 3删除） */
  businessType: number
  /** 方法名称 */
  method: string
  /** 请求方式 */
  requestMethod: string
  /** 操作类别（0其它 1后台用户 2手机端用户） */
  operatorType: number
  /** 操作人员 */
  operName: string
  /** 部门名称 */
  deptName: string
  /** 请求URL */
  operUrl: string
  /** 主机地址 */
  operIp: string
  /** 操作地点 */
  operLocation: string
  /** 请求参数 */
  operParam: string
  /** 返回参数 */
  jsonResult: string
  /** 状态（0正常 1异常） */
  status: number
  /** 错误消息 */
  errorMsg: string
  /** 操作时间 */
  operTime: string
}

/**
 * 操作日志导出参数
 */
export interface OperLogExport extends OperLogQuery {
  /** 排序列 */
  orderByColumn?: string
  /** 排序方向 */
  isAsc?: string
} 