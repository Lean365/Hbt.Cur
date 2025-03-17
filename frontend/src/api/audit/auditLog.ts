import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  AuditLogQuery, 
  AuditLog,
  AuditLogExport
} from '@/types/audit/auditLog'

// 获取审计日志列表
export function getAuditLogList(params: AuditLogQuery) {
  return request<HbtApiResponse<AuditLog[]>>({
    url: '/api/HbtAuditLog',
    method: 'get',
    params
  })
}

// 获取审计日志详情
export function getAuditLog(id: number) {
  return request<HbtApiResponse<AuditLog>>({
    url: `/api/HbtAuditLog/${id}`,
    method: 'get'
  })
}

// 清空审计日志
export function clearAuditLog() {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtAuditLog/clear',
    method: 'delete'
  })
}

// 导出审计日志
export function exportAuditLog(params: AuditLogExport, sheetName: string = '审计日志数据') {
  return request({
    url: '/api/HbtAuditLog/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
} 