import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  DictDataQuery, 
  DictData, 
  DictDataCreate, 
  DictDataUpdate,
  DictDataStatus
} from '@/types/admin/dictData'

// 获取字典数据列表
export function getDictDataList(params: DictDataQuery) {
  return request<HbtApiResult<DictData[]>>({
    url: '/api/HbtDictData',
    method: 'get',
    params
  })
}

// 获取字典数据详情
export function getDictData(dictCode: number) {
  return request<HbtApiResult<DictData>>({
    url: `/api/HbtDictData/${dictCode}`,
    method: 'get'
  })
}

// 创建字典数据
export function createDictData(data: DictDataCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictData',
    method: 'post',
    data
  })
}

// 更新字典数据
export function updateDictData(data: DictDataUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictData',
    method: 'put',
    data
  })
}

// 删除字典数据
export function deleteDictData(dictCode: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtDictData/${dictCode}`,
    method: 'delete'
  })
}

// 批量删除字典数据
export function batchDeleteDictData(dictCodes: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictData/batch',
    method: 'delete',
    data: dictCodes
  })
}

// 更新字典数据状态
export function updateDictDataStatus(data: DictDataStatus) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtDictData/${data.dictCode}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

// 导入字典数据
export function importDictData(file: File, sheetName: string = '字典数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<any>>({
    url: '/api/HbtDictData/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出字典数据
export function exportDictData(params: DictDataQuery, sheetName: string = '字典数据') {
  return request({
    url: '/api/HbtDictData/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取字典数据导入模板
export function getDictDataTemplate(sheetName: string = '字典数据') {
  return request({
    url: '/api/HbtDictData/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}