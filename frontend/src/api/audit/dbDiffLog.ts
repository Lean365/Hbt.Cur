import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  DbDiffLogQuery, 
  DbDiffLog,
  DbDiffLogExport
} from '@/types/audit/dbDiffLog'

// 获取数据库差异日志列表
export function getDbDiffLogList(params: DbDiffLogQuery) {
  return request<HbtApiResponse<DbDiffLog[]>>({
    url: '/api/HbtDbDiffLog',
    method: 'get',
    params
  })
}

// 获取数据库差异日志详情
export function getDbDiffLog(id: number) {
  return request<HbtApiResponse<DbDiffLog>>({
    url: `/api/HbtDbDiffLog/${id}`,
    method: 'get'
  })
}

// 清空数据库差异日志
export function clearDbDiffLog() {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtDbDiffLog/clear',
    method: 'delete'
  })
}

// 导出数据库差异日志
export function exportDbDiffLog(params: DbDiffLogExport, sheetName: string = '数据库差异日志数据') {
  return request({
    url: '/api/HbtDbDiffLog/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}