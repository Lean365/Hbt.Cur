import request from '@/utils/request';
import type { HbtApiResponse } from '@/types/common';
import type { HbtGenConfig, HbtGenConfigCreate, HbtGenConfigUpdate, HbtGenConfigQuery, HbtGenConfigPageResult } from '@/types/generator/genConfig';

/**
 * 获取代码生成配置列表
 */
export function getGenConfigList(params: HbtGenConfigQuery) {
  return request<HbtApiResponse<HbtGenConfigPageResult>>({
    url: '/api/HbtGenConfig/list',
    method: 'get',
    params
  });
}

/**
 * 获取代码生成配置详情
 */
export function getGenConfig(id: number) {
  return request<HbtApiResponse<HbtGenConfig>>({
    url: `/api/HbtGenConfig/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成配置
 */
export function createGenConfig(data: HbtGenConfigCreate) {
  return request<HbtApiResponse<HbtGenConfig>>({
    url: '/api/HbtGenConfig',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成配置
 */
export function updateGenConfig(id: number, data: HbtGenConfigUpdate) {
  return request<HbtApiResponse<HbtGenConfig>>({
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
 * 批量删除代码生成配置
 */
export function batchDeleteGenConfig(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtGenConfig/batch',
    method: 'delete',
    data: ids
  });
}

/**
 * 导出代码生成配置
 */
export function exportGenConfig(params: HbtGenConfigQuery) {
  return request<HbtApiResponse<Blob>>({
    url: '/api/HbtGenConfig/export',
    method: 'get',
    params,
    responseType: 'blob'
  });
}

/**
 * 下载导入模板
 */
export function downloadTemplate() {
  return request<HbtApiResponse<Blob>>({
    url: '/api/HbtGenConfig/template',
    method: 'get',
    responseType: 'blob'
  });
}

/**
 * 导入代码生成配置
 */
export function importGenConfig(file: File) {
  const formData = new FormData();
  formData.append('file', file);
  return request<HbtApiResponse<{ success: number; fail: number }>>({
    url: '/api/HbtGenConfig/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
} 