import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  DictTypeQuery, 
  DictType, 
  DictTypeCreate, 
  DictTypeUpdate,
  DictTypeStatus,
  DictTypeImport
} from '@/types/admin/dictType'

// 获取字典类型列表
export function getDictTypeList(params: DictTypeQuery) {
  return request<HbtApiResult<DictType[]>>({
    url: '/api/HbtDictType',
    method: 'get',
    params
  })
}

// 获取字典类型详情
export function getDictType(dictId: number) {
  return request<HbtApiResult<DictType>>({
    url: `/api/HbtDictType/${dictId}`,
    method: 'get'
  })
}

// 创建字典类型
export function createDictType(data: DictTypeCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictType',
    method: 'post',
    data
  })
}

// 更新字典类型
export function updateDictType(data: DictTypeUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictType',
    method: 'put',
    data
  })
}

// 删除字典类型
export function deleteDictType(dictId: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtDictType/${dictId}`,
    method: 'delete'
  })
}

// 批量删除字典类型
export function batchDeleteDictType(dictIds: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictType/batch',
    method: 'delete',
    data: dictIds
  })
}

// 更新字典类型状态
export function updateDictTypeStatus(data: DictTypeStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtDictType/${data.dictId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 导入字典类型数据
export function importDictType(data: DictTypeImport[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictType/import',
    method: 'post',
    data
  })
}

// 导出字典类型数据
export function exportDictType(params: DictTypeQuery) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictType/export',
    method: 'get',
    params
  })
}

// 获取字典类型导入模板
export function getDictTypeTemplate() {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictType/template',
    method: 'get'
  })
}

// 刷新字典缓存
export function refreshDictCache() {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictType/refresh',
    method: 'post'
  })
}