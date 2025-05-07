import request from '@/utils/request';
import type { HbtGenTable, HbtGenTableCreate, HbtGenTableUpdate, HbtGenTableQuery, HbtGenTablePageResult, TableInfoDto, TableInfoTuple } from '@/types/generator/genTable';
import type { HbtGenColumn } from '@/types/generator/genColumn';
import type { HbtApiResponse, HbtPagedResult } from '@/types/common';

/**
 * 获取代码生成表分页列表
 */
export function getTableList(query: HbtGenTable.Query) {
  return request<HbtApiResponse<HbtPagedResult<HbtGenTable.Table>>>({
    url: '/api/HbtGenTable/list',
    method: 'get',
    params: query
  });
}

/**
 * 获取代码生成表详情（包含字段信息）
 */
export function getTable(id: number) {
  return request<HbtApiResponse<HbtGenTable.Table>>({
    url: `/api/HbtGenTable/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成表（包含字段信息）
 */
export function createTable(data: HbtGenTable.Create) {
  return request<HbtApiResponse<HbtGenTable.Table>>({
    url: '/api/HbtGenTable',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成表（包含字段信息）
 */
export function updateTable(data: HbtGenTable.Update) {
  return request<HbtApiResponse<HbtGenTable.Table>>({
    url: '/api/HbtGenTable',
    method: 'put',
    data
  });
}

/**
 * 删除代码生成表（同时删除关联的字段信息）
 */
export function deleteTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${id}`,
    method: 'delete'
  });
}

/**
 * 获取表字段列表
 */
export function getTableColumns(tableId: number) {
  return request<HbtApiResponse<HbtGenColumn.Column[]>>({
    url: `/api/HbtGenTable/${tableId}/columns`,
    method: 'get'
  });
}

/**
 * 更新表字段信息
 */
export function updateTableColumns(tableId: number, columns: HbtGenColumn.Column[]) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${tableId}/columns`,
    method: 'put',
    data: columns
  });
}

/**
 * 从数据库导入表结构
 */
export function importTableFromDb(input: { tableNames: string[] }) {
  return request<HbtApiResponse<HbtGenTable.Table[]>>({
    url: '/api/HbtGenTable/import',
    method: 'post',
    data: input
  });
}

/**
 * 获取数据库列表
 */
export function getDatabaseList() {
  return request<HbtApiResponse<string[]>>({
    url: '/api/HbtGenTable/databases',
    method: 'get'
  });
}

/**
 * 获取数据库表列表
 */
export function getTableListByDb(databaseName: string): Promise<HbtApiResponse<{ code: number; msg: string; data: Array<{ name: string; description: string }> }>> {
  return request({
    url: `/api/HbtGenTable/tables/${databaseName}`,
    method: 'get'
  })
}

/**
 * 同步表结构到数据库
 */
export function syncTable(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${id}/sync`,
    method: 'post'
  });
}

/**
 * 预览生成的代码
 */
export function previewCode(id: number) {
  return request<HbtApiResponse<Record<string, string>>>({
    url: `/api/HbtGenTable/${id}/preview`,
    method: 'get'
  });
}

/**
 * 生成代码
 */
export function generateCode(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtGenTable/${id}/generate`,
    method: 'post'
  });
}

/**
 * 批量生成代码
 */
export function batchGenerateCode(ids: number[]) {
  return request<HbtApiResponse<string>>({
    url: '/api/HbtGenTable/batch-generate',
    method: 'post',
    data: ids
  });
}

/**
 * 下载生成的代码
 */
export function downloadCode(id: number) {
  return request<Blob>({
    url: `/api/HbtGenTable/${id}/download`,
    method: 'get',
    responseType: 'blob'
  });
}

/**
 * 获取数据库表列信息
 */
export function getTableColumnList(databaseName: string, tableName: string) {
  return request<HbtApiResponse<HbtGenColumn.Column[]>>({
    url: `/api/HbtGenTable/columns/${databaseName}/${tableName}`,
    method: 'get'
  });
}

/**
 * 导入表及其列信息
 */
export function importTableAndColumns(databaseName: string, tableName: string) {
  return request<HbtApiResponse<HbtGenTable.Table>>({
    url: `/api/HbtGenTable/import-table-and-columns/${databaseName}/${tableName}`,
    method: 'post',
    timeout: 60000 // 增加超时时间到60秒
  });
}
