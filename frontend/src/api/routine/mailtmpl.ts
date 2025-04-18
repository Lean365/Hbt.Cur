import request from '@/utils/request'
import type { HbtMailTemplateDto, HbtMailTemplateQueryDto, HbtMailTemplateCreateDto, HbtMailTemplateUpdateDto } from '@/types/routine/mailtmpl'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取邮件模板列表
 * @param params 查询参数
 * @returns 邮件模板列表
 */
export function getMailTemplateList(params: HbtMailTemplateQueryDto) {
  return request<HbtApiResponse<HbtPagedResult<HbtMailTemplateDto>>>({
    url: '/api/HbtMailTemplate/list',
    method: 'get',
    params
  })
}

/**
 * 获取邮件模板详情
 * @param id 邮件模板ID
 * @returns 邮件模板详情
 */
export function getMailTemplateDetail(id: number | bigint) {
  return request<HbtApiResponse<HbtMailTemplateDto>>({
    url: `/api/HbtMailTemplate/${id}`,
    method: 'get'
  })
}

/**
 * 创建邮件模板
 * @param data 邮件模板数据
 * @returns 创建结果
 */
export function createMailTemplate(data: HbtMailTemplateCreateDto) {
  return request<HbtApiResponse<HbtMailTemplateDto>>({
    url: '/api/HbtMailTemplate',
    method: 'post',
    data
  })
}

/**
 * 更新邮件模板
 * @param id 邮件模板ID
 * @param data 邮件模板数据
 * @returns 更新结果
 */
export function updateMailTemplate(id: number | bigint, data: HbtMailTemplateUpdateDto) {
  return request<HbtApiResponse<HbtMailTemplateDto>>({
    url: `/api/HbtMailTemplate/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除邮件模板
 * @param id 邮件模板ID
 * @returns 删除结果
 */
export function deleteMailTemplate(id: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMailTemplate/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除邮件模板
 * @param ids 邮件模板ID列表
 * @returns 删除结果
 */
export function batchDeleteMailTemplate(ids: number[] | bigint[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMailTemplate/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出邮件模板列表
 * @param params 查询参数
 * @returns 导出结果
 */
export function exportMailTemplateList(params: HbtMailTemplateQueryDto) {
  return request<Blob>({
    url: '/api/HbtMailTemplate/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入邮件模板列表
 * @param file 文件
 * @returns 导入结果
 */
export function importMailTemplateList(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMailTemplate/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
} 