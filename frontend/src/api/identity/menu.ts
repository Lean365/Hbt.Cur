import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  HbtMenuQuery, 
  HbtMenu, 
  HbtMenuCreate, 
  HbtMenuUpdate,
  HbtMenuStatus,
  HbtMenuOrder,
  HbtMenuTemplate,
  HbtMenuImport,
  HbtMenuExport,
  HbtMenuTreeQuery
} from '@/types/identity/menu'
import { useUserStore } from '@/stores/user'
import { getToken } from '@/utils/auth'

// 获取菜单分页列表
export function getMenuList(params: HbtMenuQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtMenu>>>({
    url: '/api/HbtMenu/list',
    method: 'get',
    params
  })
}

// 获取菜单树形结构
export function getMenuTree(params?: HbtMenuTreeQuery) {
  return request<HbtApiResponse<HbtMenu[]>>({
    url: '/api/HbtMenu/tree',
    method: 'get',
    params
  })
}

// 获取菜单详情
export function getMenu(menuId: number) {
  return request<HbtApiResponse<HbtMenu>>({
    url: `/api/HbtMenu/${menuId}`,
    method: 'get'
  })
}

// 创建菜单
export function createMenu(data: HbtMenuCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtMenu',
    method: 'post',
    data
  })
}

// 更新菜单
export function updateMenu(data: HbtMenuUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMenu',
    method: 'put',
    data
  })
}

// 删除菜单
export function deleteMenu(menuId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMenu/${menuId}`,
    method: 'delete'
  })
}

// 批量删除菜单
export function batchDeleteMenu(menuIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMenu/batch',
    method: 'delete',
    data: menuIds
  })
}

// 导入菜单数据
export function importMenu(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtMenu/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出菜单数据
export function exportMenu(params: HbtMenuTreeQuery) {
  return request<Blob>({
    url: '/api/HbtMenu/export',
    method: 'get',
    params,
    responseType: 'blob',
  })
}

// 获取导入模板
export function getTemplate() {
  return request<Blob>({
    url: '/api/HbtMenu/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新菜单状态
export function updateMenuStatus(data: HbtMenuStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMenu/${data.menuId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 更新菜单排序
export function updateMenuOrder(data: HbtMenuOrder) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMenu/${data.menuId}/order`,
    method: 'put',
    params: { orderNum: data.orderNum }
  })
}

// 获取当前用户菜单权限
export function getCurrentUserMenus(): Promise<HbtApiResponse<HbtMenu[]>> {
  console.log('[Menu API] 开始获取当前用户菜单')
  const userStore = useUserStore()
  const tenantId = userStore.getCurrentTenantId()
  const token = getToken()
  
  console.log('[Menu API] 当前状态:', {
    租户ID: tenantId,
    是否有Token: !!token,
    Token长度: token?.length,
    用户信息: userStore.userInfo
  })
  
  if (!token) {
    console.error('[Menu API] 未找到Token，无法获取菜单')
    return Promise.reject(new Error('未找到Token'))
  }
  
  if (!tenantId || tenantId <= 0) {
    console.error('[Menu API] 未找到有效的租户ID，尝试重新获取用户信息')
    return userStore.getUserInfo().then(() => {
      const newTenantId = userStore.getCurrentTenantId()
      if (!newTenantId || newTenantId <= 0) {
        throw new Error('未找到有效的租户ID')
      }
      return getCurrentUserMenus()
    })
  }
  
  return request<HbtApiResponse<HbtMenu[]>>({
    url: '/api/HbtMenu/current',
    method: 'get',
    headers: {
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache',
      'X-Tenant-Id': tenantId.toString(),
      'Authorization': `Bearer ${token}`
    },
    validateStatus: function (status): boolean {
      console.log('[Menu API] 响应状态码:', status)
      return status >= 200 && status < 300
    }
  }).then(response => {
    console.log('[Menu API] 获取菜单响应:', {
      状态码: response.status,
      状态文本: response.statusText,
      响应头: response.headers,
      响应数据: response.data
    })
    return response.data
  }).catch(error => {
    console.error('[Menu API] 获取菜单失败:', {
      错误信息: error.message,
      错误代码: error.code,
      请求配置: error.config,
      响应数据: error.response?.data,
      响应状态: error.response?.status,
      响应头: error.response?.headers,
      错误详情: error.response?.data?.msg || error.message
    })
    throw error
  })
} 