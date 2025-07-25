import request from '@/utils/request'
import type { HbtFormQuery, HbtForm, HbtFormStatus, HbtFormUpdate, HbtFormCreate } from '@/types/workflow/form'
import type { HbtPagedResult } from '@/types/common'
import type { HbtApiResponse } from '@/types/common'

// 获取表单分页列表
export function getFormList(query: HbtFormQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtForm>>>({
    url: '/api/HbtForm/list',
    method: 'get',
    params: query
  })
}

// 获取表单详情
export function getFormById(formId: number) {
  return request<HbtApiResponse<HbtForm>>({
    url: `/api/HbtForm/${formId}`,
    method: 'get'
  })
}

// 根据键获取表单定义
export function getFormByKey(formKey: string) {
  return request<HbtApiResponse<HbtForm>>({
    url: `/api/HbtForm/key/${formKey}`,
    method: 'get'
  })
}

// 创建表单
export function createForm(data: HbtFormCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtForm',
    method: 'post',
    data
  })
}

// 更新表单
export function updateForm(data: HbtFormUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtForm',
    method: 'put',
    data
  })
}

// 删除表单
export function deleteForm(formId: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtForm/${formId}`,
    method: 'delete'
  })
}

// 批量删除表单
export function batchDeleteForm(formIds: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtForm/batch',
    method: 'delete',
    data: formIds
  })
}

// 导入表单
export function importForm(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtForm/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出表单
export function exportForm(query: HbtFormQuery) {
  return request<Blob>({
    url: '/api/HbtForm/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getFormTemplate() {
  return request<Blob>({
    url: '/api/HbtForm/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新表单状态
export function updateFormStatus(data: HbtFormStatus) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtForm/${data.formId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 获取我的表单列表
export function getMyForms(query: HbtFormQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtForm>>>({
    url: '/api/HbtForm/my',
    method: 'get',
    params: query
  })
}
