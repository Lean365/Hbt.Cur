import request from '@/utils/request';
import type { HbtApiResponse, HbtPagedResult } from '@/types/common';
import type { 
  HbtGenTemplateQuery, 
  HbtGenTemplate,
  HbtGenTemplateCreate,
  HbtGenTemplateUpdate
} from '@/types/generator/genTemplate';

/**
 * 获取代码生成模板分页列表
 */
export function getGenTemplateList(query: HbtGenTemplateQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtGenTemplate>>>({
    url: '/api/HbtGenTemplate/list',
    method: 'get',
    params: query
  });
}

/**
 * 获取代码生成模板详情
 */
export function getGenTemplate(id: number) {
  return request<HbtApiResponse<HbtGenTemplate>>({
    url: `/api/HbtGenTemplate/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成模板
 */
export function createGenTemplate(data: HbtGenTemplateCreate) {
  return request({
    url: '/api/HbtGenTemplate',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成模板
 */
export function updateGenTemplate(data: HbtGenTemplateUpdate) {
  return request({
    url: '/api/HbtGenTemplate',
    method: 'put',
    data
  });
}

/**
 * 删除代码生成模板
 */
export function deleteGenTemplate(id: number) {
  return request({
    url: `/api/HbtGenTemplate/${id}`,
    method: 'delete'
  });
}

/**
 * 批量删除代码生成模板
 */
export function batchDeleteGenTemplate(ids: number[]) {
  return request({
    url: '/api/HbtGenTemplate/batch',
    method: 'delete',
    data: ids
  });
}

/**
 * 导出代码生成模板数据
 */
export function exportGenTemplate(query: HbtGenTemplateQuery) {
  return request({
    url: '/api/HbtGenTemplate/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  });
}

/**
 * 导入代码生成模板数据
 */
export function importGenTemplate(file: File) {
  const formData = new FormData();
  formData.append('file', file);
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtGenTemplate/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}

/**
 * 下载代码生成模板导入模板
 */
export function downloadTemplate() {
  return request({
    url: '/api/HbtGenTemplate/template',
    method: 'get',
    responseType: 'blob'
  });
}