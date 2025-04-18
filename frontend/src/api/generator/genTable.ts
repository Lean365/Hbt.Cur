import request from '@/utils/request';
import type { HbtGenTableDto, HbtGenTableQueryDto, HbtGenTablePageResultDto } from '@/types/generator/table';
import type { HbtApiResponse } from '@/types/common';

/**
 * 获取代码生成表列表
 */
export function getPagedList(params: HbtGenTableQueryDto) {
  return request<HbtApiResponse<HbtGenTablePageResultDto>>({
    url: '/api/HbtGenTable/list',
    method: 'get',
    params
  });
}

/**
 * 获取代码生成表详情
 */
export function getGenTable(id: number) {
  return request<HbtApiResponse<HbtGenTableDto>>({
    url: `/api/HbtGenTable/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成表
 */
export function createGenTable(data: HbtGenTableDto) {
  return request<HbtApiResponse<HbtGenTableDto>>({
    url: '/api/HbtGenTable',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成表
 */
export function updateGenTable(id: number, data: HbtGenTableDto) {
  return request<HbtApiResponse<HbtGenTableDto>>({
    url: `/api/HbtGenTable/${id}`,
    method: 'put',
    data
  });
}

/**
 * 删除代码生成表
 */
export function deleteGenTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${id}`,
    method: 'delete'
  });
}

/**
 * 生成代码
 */
export function generateGenTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${id}/generate`,
    method: 'post'
  });
}

/**
 * 预览代码
 */
export function previewGenTable(id: number) {
  return request<HbtApiResponse<Record<string, string>>>({
    url: `/api/HbtGenTable/${id}/preview`,
    method: 'get'
  });
}

/**
 * 下载代码
 */
export function downloadGenTable(id: number) {
  return request<Blob>({
    url: `/api/HbtGenTable/${id}/download`,
    method: 'get',
    responseType: 'blob'
  });
}

/**
 * 同步数据库
 */
export function syncGenTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${id}/sync`,
    method: 'post'
  });
}