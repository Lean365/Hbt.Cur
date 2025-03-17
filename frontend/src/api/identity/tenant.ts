import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  TenantQuery, 
  Tenant,
  TenantCreate,
  TenantUpdate,
  TenantStatus
} from '@/types/identity/tenant'

/**
 * 获取租户分页列表
 */
export function getPagedList(params: TenantQuery) {
  return request<HbtApiResponse<HbtPagedResult<Tenant>>>({
    url: '/api/HbtTenant',
    method: 'get',
    params
  })
}

/**
 * 获取租户详情
 */
export function getTenant(tenantId: number) {
  return request<HbtApiResponse<Tenant>>({
    url: `/api/HbtTenant/${tenantId}`,
    method: 'get'
  })
}

/**
 * 创建租户
 */
export function createTenant(data: TenantCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtTenant',
    method: 'post',
    data
  })
}

/**
 * 更新租户
 */
export function updateTenant(data: TenantUpdate) {
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
export function updateTenantStatus(data: TenantStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtTenant/${data.tenantId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 导出租户数据
 */
export function exportTenant(params?: TenantQuery) {
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
  return request<HbtApiResponse<Tenant>>({
    url: '/api/HbtTenant/current',
    method: 'get'
  })
}