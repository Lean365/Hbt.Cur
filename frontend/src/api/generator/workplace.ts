import request from '@/utils/request'
import type { HbtWorkplaceProject, HbtWorkplaceProjectQuery, HbtWorkplaceProjectPageResult } from '@/types/generator/workplace'

/** 获取工作区项目列表 */
export function getWorkplaceProjectList(query: HbtWorkplaceProjectQuery) {
  return request<HbtWorkplaceProjectPageResult>({
    url: '/api/generator/workplace/projects',
    method: 'get',
    params: query
  })
}

/** 获取工作区项目详情 */
export function getWorkplaceProject(id: string) {
  return request<HbtWorkplaceProject>({
    url: `/api/generator/workplace/projects/${id}`,
    method: 'get'
  })
}

/** 创建工作区项目 */
export function createWorkplaceProject(data: HbtWorkplaceProject) {
  return request<void>({
    url: '/api/generator/workplace/projects',
    method: 'post',
    data
  })
}

/** 更新工作区项目 */
export function updateWorkplaceProject(data: HbtWorkplaceProject) {
  return request<void>({
    url: `/api/generator/workplace/projects/${data.id}`,
    method: 'put',
    data
  })
}

/** 删除工作区项目 */
export function deleteWorkplaceProject(id: string) {
  return request<void>({
    url: `/api/generator/workplace/projects/${id}`,
    method: 'delete'
  })
}

/** 批量删除工作区项目 */
export function batchDeleteWorkplaceProject(ids: string[]) {
  return request<void>({
    url: '/api/generator/workplace/projects/batch',
    method: 'delete',
    data: ids
  })
}

/** 导出工作区项目 */
export function exportWorkplaceProject(query: HbtWorkplaceProjectQuery) {
  return request<Blob>({
    url: '/api/generator/workplace/projects/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/** 下载工作区项目模板 */
export function downloadWorkplaceProjectTemplate() {
  return request<Blob>({
    url: '/api/generator/workplace/projects/template',
    method: 'get',
    responseType: 'blob'
  })
}

/** 导入工作区项目 */
export function importWorkplaceProject(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<void>({
    url: '/api/generator/workplace/projects/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
} 