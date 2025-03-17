import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  ExceptionLogQuery, 
  ExceptionLog,
  ExceptionLogExport
} from '@/types/audit/exceptionLog'

// 获取异常日志列表
export function getExceptionLogList(params: ExceptionLogQuery) {
  return request<HbtApiResponse<ExceptionLog[]>>({
    url: '/api/HbtExceptionLog',
    method: 'get',
    params
  })
}

// 获取异常日志详情
export function getExceptionLog(id: number) {
  return request<HbtApiResponse<ExceptionLog>>({
    url: `/api/HbtExceptionLog/${id}`,
    method: 'get'
  })
}

// 清空异常日志
export function clearExceptionLog() {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtExceptionLog/clear',
    method: 'delete'
  })
}

// 导出异常日志
export function exportExceptionLog(params: ExceptionLogExport, sheetName: string = '异常日志数据') {
  return request({
    url: '/api/HbtExceptionLog/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
} 