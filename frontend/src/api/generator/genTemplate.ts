import request from '@/utils/request';
import type { HbtGenTemplate } from '@/types/generator/genTemplate';
import type { HbtApiResponse } from '@/types/common';

/**
 * 获取代码生成模板列表
 */
export function getPagedList(params: HbtGenTemplate) {
  return request<HbtApiResponse<HbtGenTemplate[]>>({
    url: '/api/HbtGenTemplate/list',
    method: 'get',
    params
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
export function createGenTemplate(data: HbtGenTemplate) {
  return request<HbtApiResponse<HbtGenTemplate>>({
    url: '/api/HbtGenTemplate',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成模板
 */
export function updateGenTemplate(id: number, data: HbtGenTemplate) {
  return request<HbtApiResponse<HbtGenTemplate>>({
    url: `/api/HbtGenTemplate/${id}`,
    method: 'put',
    data
  });
}

/**
 * 删除代码生成模板
 */
export function deleteGenTemplate(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTemplate/${id}`,
    method: 'delete'
  });
}

/**
 * 生成代码
 */
export function generateGenTemplate(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTemplate/${id}/generate`,
    method: 'post'
  });
}

/**
 * 预览代码
 */
export function previewGenTemplate(id: number) {
  return request<HbtApiResponse<Record<string, string>>>({
    url: `/api/HbtGenTemplate/${id}/preview`,
    method: 'get'
  });
}

/**
 * 下载代码
 */
export function downloadGenTemplate(id: number) {
  return request<Blob>({
    url: `/api/HbtGenTemplate/${id}/download`,
    method: 'get',
    responseType: 'blob'
  });
}