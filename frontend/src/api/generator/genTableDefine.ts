//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : genTableDefine.ts
// 创建者 : Lean365
// 创建时间: 2024-04-25
// 版本号 : V0.0.1
// 描述    : 代码生成表定义API
//===================================================================

import request from '@/utils/request';
import type { HbtGenTableDefine, HbtGenTableDefineQuery, HbtGenTableDefinePageResult, HbtGenTableDefineCreate, HbtGenTableDefineUpdate, HbtGenTableDefineImport } from '@/types/generator/genTableDefine';
import type { HbtApiResponse, HbtPagedResult } from '@/types/common';

// API基础路径
const baseUrl = '/api/HbtGenTableDefine';

/**
 * 获取表定义列表
 * @param params 查询参数
 * @returns 分页结果
 */
export function getTableDefineList(params: HbtGenTableDefineQuery) {
  return request<HbtPagedResult<HbtGenTableDefine>>({
    url: `${baseUrl}/list`,
    method: 'get',
    params
  });
}

/**
 * 获取表定义详情
 * @param id 表定义ID
 * @returns 表定义详情
 */
export function getTableDefineInfo(id: number) {
  return request<HbtApiResponse<HbtGenTableDefine>>({
    url: `${baseUrl}/${id}`,
    method: 'get'
  });
}

/**
 * 创建表定义
 * @param data 创建参数
 * @returns 创建结果
 */
export function createTableDefine(data: HbtGenTableDefineCreate) {
  return request<HbtApiResponse<number>>({
    url: baseUrl,
    method: 'post',
    data
  });
}

/**
 * 更新表定义
 * @param data 更新参数
 * @returns 更新结果
 */
export function updateTableDefine(data: HbtGenTableDefineUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: baseUrl,
    method: 'put',
    data
  });
}

/**
 * 删除表定义
 * @param id 表定义ID
 * @returns 删除结果
 */
export function deleteTableDefine(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `${baseUrl}/${id}`,
    method: 'delete'
  });
}

/**
 * 批量删除表定义
 * @param ids 表定义ID数组
 * @returns 删除结果
 */
export function batchDeleteTableDefine(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: `${baseUrl}/batch/${ids.join(',')}`,
    method: 'delete'
  });
}

/**
 * 导入表定义
 * @param data 导入参数
 * @returns 导入结果
 */
export function importTableDefine(data: HbtGenTableDefineImport) {
  return request<HbtApiResponse<boolean>>({
    url: `${baseUrl}/import`,
    method: 'post',
    data
  });
}

/**
 * 导出表定义
 * @returns 导出结果
 */
export function exportTableDefine() {
  return request<HbtApiResponse<Blob>>({
    url: `${baseUrl}/export`,
    method: 'get',
    responseType: 'blob'
  });
}

/**
 * 获取表定义模板
 * @returns 模板结果
 */
export function getTableDefineTemplate() {
  return request<HbtApiResponse<Blob>>({
    url: `${baseUrl}/template`,
    method: 'get',
    responseType: 'blob'
  });
}

/**
 * 初始化表结构
 * @param data 初始化参数
 * @returns 初始化结果
 */
export function initializeTableDefine(data: HbtGenTableDefineCreate) {
  return request<HbtApiResponse<boolean>>({
    url: `${baseUrl}/initialize`,
    method: 'post',
    data
  });
}

/**
 * 同步表结构
 * @param id 表定义ID
 * @returns 同步结果
 */
export function syncTableDefine(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `${baseUrl}/sync/${id}`,
    method: 'post'
  });
}