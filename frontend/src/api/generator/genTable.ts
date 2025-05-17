import request from '@/utils/request';
import type { HbtGenTable, HbtGenTableQuery, HbtGenTablePageResult } from '@/types/generator/genTable';
import type { HbtGenColumn } from '@/types/generator/genColumn';
import type { HbtApiResponse, HbtPagedResult } from '@/types/common';

/**
 * 获取代码生成表分页列表
 */
export function getTableList(params: HbtGenTableQuery) {
  return request<HbtApiResponse<HbtPagedResult<HbtGenTable>>>({
    url: '/api/HbtGenTable/list',
    method: 'get',
    params
  });
}

/**
 * 获取代码生成表详情
 */
export function getTable(id: number) {
  return request<HbtApiResponse<HbtGenTable>>({
    url: `/api/HbtGenTable/${id}`,
    method: 'get'
  });
}

/**
 * 获取表字段列表
 */
export function getColumns(tableId: number) {
  return request<HbtApiResponse<HbtGenColumn[]>>({
    url: `/api/HbtGenTable/columns/${tableId}`,
    method: 'get'
  });
}

/**
 * 创建代码生成表
 */
export function createTable(data: HbtGenTable) {
  return request<HbtApiResponse<HbtGenTable>>({
    url: '/api/HbtGenTable',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成表
 */
export function updateTable(data: HbtGenTable) {
  return request<HbtApiResponse<HbtGenTable>>({
    url: '/api/HbtGenTable',
    method: 'put',
    data
  });
}

/**
 * 删除代码生成表
 */
export function deleteTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${id}`,
    method: 'delete'
  });
}

/**
 * 从数据库导入表结构
 */
export function importTable(input: { tableNames: string[] }) {
  return request<HbtApiResponse<HbtGenTable[]>>({
    url: '/api/HbtGenTable/import',
    method: 'post',
    data: input
  });
}

/**
 * 获取数据库列表
 */
export function getDatabasesByDb() {
  return request<HbtApiResponse<string[]>>({
    url: '/api/HbtGenTable/databases',
    method: 'get'
  });
}

/**
 * 获取数据库表列表
 */
export function getTablesByDb(databaseName: string) {
  return request<HbtApiResponse<Array<{ tableName: string; tableComment: string }>>>({
    url: `/api/HbtGenTable/tables/${databaseName}`,
    method: 'get'
  });
}

/**
 * 获取数据库表列信息
 */
export function getTableColumnsByDb(databaseName: string, tableName: string) {
  return request<HbtApiResponse<HbtGenColumn[]>>({
    url: `/api/HbtGenTable/columns/${databaseName}/${tableName}`,
    method: 'get'
  });
}

/**
 * 导入表及其列信息
 */
export function importTableAndColumns(databaseName: string, tableName: string) {
  return request<HbtApiResponse<HbtGenTable>>({
    url: `/api/HbtGenTable/import-table-and-columns/${databaseName}/${tableName}`,
    method: 'post',
    timeout: 60000 // 增加超时时间到60秒
  });
}

/**
 * 同步表结构到数据库
 */
export function syncTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/sync/${id}`,
    method: 'post'
  });
}

/**
 * 预览生成的代码
 */
export function previewCode(id: number) {
  return request<HbtApiResponse<Record<string, string>>>({
    url: `/api/HbtGenTable/preview/${id}`,
    method: 'get'
  });
}

/**
 * 生成代码
 */
export function generateCode(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/generate/${id}`,
    method: 'post'
  });
}

/**
 * 批量生成代码
 */
export function batchGenerateCode(ids: number[]) {
  return request<HbtApiResponse<string>>({
    url: '/api/HbtGenTable/generate/batch',
    method: 'post',
    data: ids
  });
}

/**
 * 下载生成的代码
 */
export function downloadCode(id: number) {
  return request<HbtApiResponse<Blob>>({
    url: `/api/HbtGenTable/download/${id}`,
    method: 'get',
    responseType: 'blob'
  });
}
