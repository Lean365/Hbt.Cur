import request from '@/utils/request';
import type { HbtGenConfigDto, HbtGenConfigQuery, HbtGenConfigPagedResult } from '@/types/generator/config';
import type { HbtApiResponse } from '@/types/common';

/**
 * 获取代码生成配置列表
 */
export function getPagedList(params: HbtGenConfigQuery) {
  return request<HbtApiResponse<HbtGenConfigPagedResult>>({
    url: '/api/HbtGenConfig/list',
    method: 'get',
    params
  });
}

/**
 * 获取代码生成配置详情
 */
export function getGenConfig(id: number) {
  return request<HbtApiResponse<HbtGenConfigDto>>({
    url: `/api/HbtGenConfig/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成配置
 */
export function createGenConfig(data: HbtGenConfigDto) {
  return request<HbtApiResponse<HbtGenConfigDto>>({
    url: '/api/HbtGenConfig',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成配置
 */
export function updateGenConfig(id: number, data: HbtGenConfigDto) {
  return request<HbtApiResponse<HbtGenConfigDto>>({
    url: `/api/HbtGenConfig/${id}`,
    method: 'put',
    data
  });
}

/**
 * 删除代码生成配置
 */
export function deleteGenConfig(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenConfig/${id}`,
    method: 'delete'
  });
}

/**
 * 生成代码
 */
export function generateGenConfig(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenConfig/${id}/generate`,
    method: 'post'
  });
}

/**
 * 预览代码
 */
export function previewGenConfig(id: number) {
  return request<HbtApiResponse<Record<string, string>>>({
    url: `/api/HbtGenConfig/${id}/preview`,
    method: 'get'
  });
}

/**
 * 下载代码
 */
export function downloadGenConfig(id: number) {
  return request<Blob>({
    url: `/api/HbtGenConfig/${id}/download`,
    method: 'get',
    responseType: 'blob'
  });
}

/** 批量删除生成配置 */
export function batchDeleteGenConfig(ids: number[]) {
  return request({
    url: '/api/HbtGenConfig/batch',
    method: 'delete',
    data: { ids }
  });
} 