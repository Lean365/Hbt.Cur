import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  HbtTenantQuery, 
  HbtTenant,
  HbtTenantCreate,
  HbtTenantUpdate,
  HbtTenantStatus,
  HbtTenantOption
} from '@/types/identity/tenant'

/**
 * 获取租户分页列表
 */
export function getTenantList(params: HbtTenantQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtTenant>>>({
    url: '/api/HbtTenant/list',
    method: 'get',
    params
  })
}

/**
 * 获取租户详情
 */
export function getTenant(tenantId: number) {
  return request<HbtApiResponse<HbtTenant>>({
    url: `/api/HbtTenant/${tenantId}`,
    method: 'get'
  })
}

/**
 * 创建租户
 */
export function createTenant(data: HbtTenantCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtTenant',
    method: 'post',
    data
  })
}

/**
 * 更新租户
 */
export function updateTenant(data: HbtTenantUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtTenant',
    method: 'put',
    data
  })
}

/**
 * 删除租户
 */
export function deleteTenant(tenantId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtTenant/${tenantId}`,
    method: 'delete'
  })
}

/**
 * 批量删除租户
 */
export function batchDeleteTenant(tenantIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtTenant/batch',
    method: 'delete',
    data: tenantIds
  })
}

/**
 * 更新租户状态
 */
export function updateTenantStatus(data: HbtTenantStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtTenant/${data.tenantId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 导出租户数据
 */
export function exportTenant(params?: HbtTenantQuery) {
  return request({
    url: '/api/HbtTenant/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入租户数据
 */
export function importTenant(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtTenant/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 下载租户导入模板
 */
export function downloadTemplate() {
  return request({
    url: '/api/HbtTenant/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 获取当前租户信息
 */
export function getCurrentTenant() {
  return request<HbtApiResponse<HbtTenant>>({
    url: '/api/HbtTenant/current',
    method: 'get'
  })
}

/**
 * 获取租户选项列表
 */
export function getTenantOptions() {
  return request<HbtApiResponse<HbtTenantOption[]>>({
    url: '/api/HbtTenant/options',
    method: 'get'
  })
}

/**
 * 测试数据库连接
 * @param data 数据库连接信息
 */
export function testDbConnection(data: any) {
  return request({
    url: '/api/identity/tenant/test-connection',
    method: 'post',
    data
  })
}

