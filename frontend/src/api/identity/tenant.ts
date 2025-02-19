import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  TenantQuery, 
  Tenant, 
  TenantCreate, 
  TenantUpdate,
  TenantStatus
} from '@/types/identity/tenant'

// 获取租户列表
export function getTenantList(params: TenantQuery) {
  return request<HbtApiResult<Tenant[]>>({
    url: '/api/identity/tenant/list',
    method: 'get',
    params
  })
}

// 获取租户详情
export function getTenant(tenantId: number) {
  return request<HbtApiResult<Tenant>>({
    url: `/api/identity/tenant/${tenantId}`,
    method: 'get'
  })
}

// 创建租户
export function createTenant(data: TenantCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/identity/tenant',
    method: 'post',
    data
  })
}

// 更新租户
export function updateTenant(data: TenantUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/identity/tenant',
    method: 'put',
    data
  })
}

// 删除租户
export function deleteTenant(tenantId: number) {
  return request<HbtApiResult<any>>({
    url: `/api/identity/tenant/${tenantId}`,
    method: 'delete'
  })
}

// 批量删除租户
export function batchDeleteTenant(tenantIds: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/identity/tenant/batch',
    method: 'delete',
    data: tenantIds
  })
}

// 更新租户状态
export function updateTenantStatus(data: TenantStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/identity/tenant/${data.tenantId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 导入租户
export function importTenant(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<any>>({
    url: '/api/identity/tenant/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出租户
export function exportTenant(params: TenantQuery) {
  return request({
    url: '/api/identity/tenant/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

// 获取租户导入模板
export function getTenantTemplate() {
  return request({
    url: '/api/identity/tenant/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 获取当前租户信息
export function getCurrentTenant() {
  return request<HbtApiResult<Tenant>>({
    url: '/api/identity/tenant/current',
    method: 'get'
  })
} 