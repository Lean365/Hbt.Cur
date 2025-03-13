import request from '@/utils/request'
import type { HbtApiResult, HbtPagedResult } from '@/types/common'
import type { 
  MenuQuery, 
  Menu, 
  MenuCreate, 
  MenuUpdate,
  MenuStatus,
  MenuOrder
} from '@/types/identity/menu'

// 获取菜单分页列表
export function getMenuList(params: MenuQuery) {
  return request<HbtApiResult<HbtPagedResult<Menu>>>({
    url: '/api/HbtMenu',
    method: 'get',
    params
  })
}

// 获取菜单树形结构
export function getMenuTree() {
  return request<HbtApiResult<Menu[]>>({
    url: '/api/HbtMenu/tree',
    method: 'get'
  })
}

// 获取菜单详情
export function getMenu(menuId: number) {
  return request<HbtApiResult<Menu>>({
    url: `/api/HbtMenu/${menuId}`,
    method: 'get'
  })
}

// 创建菜单
export function createMenu(data: MenuCreate) {
  return request<HbtApiResult<number>>({
    url: '/api/HbtMenu',
    method: 'post',
    data
  })
}

// 更新菜单
export function updateMenu(data: MenuUpdate) {
  return request<HbtApiResult<boolean>>({
    url: '/api/HbtMenu',
    method: 'put',
    data
  })
}

// 删除菜单
export function deleteMenu(menuId: number) {
  return request<HbtApiResult<boolean>>({
    url: `/api/HbtMenu/${menuId}`,
    method: 'delete'
  })
}

// 批量删除菜单
export function batchDeleteMenu(menuIds: number[]) {
  return request<HbtApiResult<boolean>>({
    url: '/api/HbtMenu/batch',
    method: 'delete',
    data: menuIds
  })
}

// 导入菜单数据
export function importMenu(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<any>>({
    url: `/api/HbtMenu/import?sheetName=${sheetName}`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出菜单数据
export function exportMenu(params: MenuQuery, sheetName: string = '菜单数据') {
  return request<Blob>({
    url: `/api/HbtMenu/export?sheetName=${sheetName}`,
    method: 'get',
    params,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getImportTemplate(sheetName: string = '菜单导入模板') {
  return request<Blob>({
    url: `/api/HbtMenu/template?sheetName=${sheetName}`,
    method: 'get',
    responseType: 'blob'
  })
}

// 更新菜单状态
export function updateMenuStatus(data: MenuStatus) {
  return request<HbtApiResult<boolean>>({
    url: `/api/HbtMenu/${data.menuId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 更新菜单排序
export function updateMenuOrder(data: MenuOrder) {
  return request<HbtApiResult<boolean>>({
    url: `/api/HbtMenu/${data.menuId}/order`,
    method: 'put',
    params: { orderNum: data.orderNum }
  })
}

// 获取当前用户菜单权限
export function getCurrentUserMenus() {
  return request<HbtApiResult<Menu[]>>({
    url: '/api/HbtMenu/current',
    method: 'get',
    headers: {
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache'
    }
  })
} 