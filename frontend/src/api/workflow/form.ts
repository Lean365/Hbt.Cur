import request from '@/utils/request'
import type { HbtApiResponse } from '@/types/common'
import type {
  HbtFormQuery,
  HbtForm,
  HbtFormCreate,
  HbtFormUpdate,
  HbtFormStatus,
  HbtFormPagedResult
} from '@/types/workflow/form'

// 获取表单列表
export function getFormList(params: HbtFormQuery) {
  return request<HbtApiResponse<HbtFormPagedResult>>({
    url: '/api/HbtForm/list',
    method: 'get',
    params
  })
}

// 获取表单详情
export function getForm(id: number) {
  return request<HbtApiResponse<HbtForm>>({
    url: `/api/HbtForm/${id}`,
    method: 'get'
  })
}

// 创建表单
export function createForm(data: HbtFormCreate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtForm',
    method: 'post',
    data
  })
}

// 更新表单
export function updateForm(data: HbtFormUpdate) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtForm',
    method: 'put',
    data
  })
}

// 删除表单
export function deleteForm(id: number) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtForm/${id}`,
    method: 'delete'
  })
}

// 批量删除表单
export function batchDeleteForm(ids: number[]) {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtForm/batch',
    method: 'delete',
    data: ids
  })
}

// 导入表单
export function importForm(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<any>>({
    url: '/api/HbtForm/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出表单
export function exportForm(params: HbtFormQuery, sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtForm/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取表单导入模板
export function getFormTemplate(sheetName: string = 'Sheet1') {
  return request({
    url: '/api/HbtForm/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 更新表单状态
export function updateFormStatus(id: number, data: HbtFormStatus) {
  return request<HbtApiResponse<any>>({
    url: `/api/HbtForm/${id}/status`,
    method: 'put',
    data
  })
}

// 获取表单选项
export function getFormOptions() {
  return request<HbtApiResponse<any>>({
    url: '/api/HbtForm/options',
    method: 'get'
  })
} 