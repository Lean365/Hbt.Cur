import request from '@/utils/request'
import type { HbtMailTpl, HbtMailTplQuery, HbtMailTplCreate, HbtMailTplUpdate } from '@/types/routine/email/mailtpl'
import type { HbtPagedResult, HbtApiResponse } from '@/types/common'

/**
 * 获取邮件模板列表
 * @param params 查询参数
 * @returns 邮件模板列表
 */
export function getMailTplList(params: HbtMailTplQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtMailTpl>>>({
    url: '/api/HbtMailTpl/list',
    method: 'get',
    params
  })
}

/**
 * 获取邮件模板详情
 * @param mailTplId 模板ID
 * @returns 邮件模板详情
 */
export function getMailTplById(mailTplId: number | bigint) {
  return request<HbtApiResponse<HbtMailTpl>>({
    url: `/api/HbtMailTpl/${mailTplId}`,
    method: 'get'
  })
}

/**
 * 创建邮件模板
 * @param data 创建数据
 * @returns 创建结果
 */
export function createMailTpl(data: HbtMailTplCreate) {
  return request<HbtApiResponse<number | bigint>>({
    url: '/api/HbtMailTpl',
    method: 'post',
    data
  })
}

/**
 * 更新邮件模板
 * @param mailTplId 模板ID
 * @param data 更新数据
 * @returns 更新结果
 */
export function updateMailTpl(mailTplId: number | bigint, data: HbtMailTplUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMailTpl/${mailTplId}`,
    method: 'put',
    data
  })
}

/**
 * 删除邮件模板
 * @param mailTplId 模板ID
 * @returns 删除结果
 */
export function deleteMailTpl(mailTplId: number | bigint) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtMailTpl/${mailTplId}`,
    method: 'delete'
  })
}

/**
 * 批量删除邮件模板
 * @param mailTplIds 模板ID数组
 * @returns 批量删除结果
 */
export function batchDeleteMailTpl(mailTplIds: (number | bigint)[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtMailTpl/batch',
    method: 'delete',
    data: mailTplIds
  })
}

/**
 * 导出邮件模板数据
 * @param params 查询参数
 * @param sheetName 工作表名称
 * @returns 导出文件
 */
export function exportMailTpl(params: HbtMailTplQuery, sheetName = '邮件模板数据') {
  return request<Blob>({
    url: '/api/HbtMailTpl/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入邮件模板数据
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 导入结果
 */
export function importMailTpl(file: File, sheetName = '邮件模板数据') {
  const formData = new FormData()
  formData.append('file', file)
  
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtMailTpl/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
     'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取邮件模板导入模板
 * @param sheetName 工作表名称
 * @returns 模板文件
 */
export function getMailTplTemplate(sheetName = '邮件模板数据') {
  return request<Blob>({
    url: '/api/HbtMailTpl/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 