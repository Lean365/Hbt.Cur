import request from '@/utils/request';
import type { HbtGenTableDefineDto } from '@/types/generator/tableDefine';
import type { HbtApiResponse } from '@/types/common';

/**
 * 获取代码生成表定义列表
 */
export function getPagedList(params: HbtGenTableDefineDto) {
  return request<HbtApiResponse<HbtGenTableDefineDto[]>>({
    url: '/api/HbtGenTableDefine',
    method: 'get',
    params
  });
}

/**
 * 获取代码生成表定义详情
 */
export function getGenTableDefine(id: number) {
  return request<HbtApiResponse<HbtGenTableDefineDto>>({
    url: `/api/HbtGenTableDefine/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成表定义
 */
export function createGenTableDefine(data: HbtGenTableDefineDto) {
  return request<HbtApiResponse<HbtGenTableDefineDto>>({
    url: '/api/HbtGenTableDefine',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成表定义
 */
export function updateGenTableDefine(id: number, data: HbtGenTableDefineDto) {
  return request<HbtApiResponse<HbtGenTableDefineDto>>({
    url: `/api/HbtGenTableDefine/${id}`,
    method: 'put',
    data
  });
}

/**
 * 删除代码生成表定义
 */
export function deleteGenTableDefine(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTableDefine/${id}`,
    method: 'delete'
  });
}

/**
 * 生成代码
 */
export function generateGenTableDefine(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTableDefine/${id}/generate`,
    method: 'post'
  });
}

/**
 * 预览代码
 */
export function previewGenTableDefine(id: number) {
  return request<HbtApiResponse<Record<string, string>>>({
    url: `/api/HbtGenTableDefine/${id}/preview`,
    method: 'get'
  });
}

/**
 * 下载代码
 */
export function downloadGenTableDefine(id: number) {
  return request<Blob>({
    url: `/api/HbtGenTableDefine/${id}/download`,
    method: 'get',
    responseType: 'blob'
  });
}