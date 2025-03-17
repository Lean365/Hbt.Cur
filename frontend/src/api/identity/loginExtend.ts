import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type { 
  LoginExtendQuery, 
  LoginExtend,
  LoginExtendExport,
  LoginExtendUpdate,
  LoginExtendOnlinePeriodUpdate
} from '@/types/identity/loginExtend'

// 获取登录扩展列表
export function getLoginExtendList(params: LoginExtendQuery) {
  return request<HbtApiResponse<LoginExtend[]>>({
    url: '/api/login-extend',
    method: 'get',
    params
  })
}

// 获取登录扩展详情
export function getLoginExtend(userId: number) {
  return request<HbtApiResponse<LoginExtend>>({
    url: `/api/login-extend/${userId}`,
    method: 'get'
  })
}

// 删除登录扩展记录
export function deleteLoginExtend(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/login-extend/${id}`,
    method: 'delete'
  })
}

// 批量删除登录扩展记录
export function batchDeleteLoginExtend(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/login-extend/batch',
    method: 'delete',
    data: ids
  })
}

// 清空登录扩展记录
export function clearLoginExtend() {
  return request<HbtApiResponse<any>>({
    url: '/api/login-extend/clear',
    method: 'delete'
  })
}

// 导出登录扩展记录
export function exportLoginExtend(data: LoginExtendExport[]) {
  return request({
    url: '/api/login-extend/export',
    method: 'post',
    data,
    params: {
      sheetName: '登录扩展信息'
    },
    responseType: 'blob'
  })
}

// 更新用户登录信息
export function updateLoginInfo(data: LoginExtendUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/login-extend/login',
    method: 'put',
    data
  })
}

// 更新用户离线信息
export function updateOfflineInfo(userId: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/login-extend/${userId}/offline`,
    method: 'put'
  })
}

// 更新用户在线时段
export function updateOnlinePeriod(data: LoginExtendOnlinePeriodUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/login-extend/online-period',
    method: 'put',
    data
  })
} 