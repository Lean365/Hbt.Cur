import request from '@/utils/request'
import type { HbtApiResult } from '@/types/api'
import type { 
  SysConfigQuery, 
  SysConfig, 
  SysConfigCreate, 
  SysConfigUpdate
} from '@/types/admin/sysConfig'

// 获取系统配置列表
export function getSysConfigList(params: SysConfigQuery) {
  return request<HbtApiResult<SysConfig[]>>({
    url: '/api/HbtSysConfig',
    method: 'get',
    params
  })
}

// 获取系统配置详情
export function getSysConfig(configId: number) {
  return request<HbtApiResult<SysConfig>>({
    url: `/api/HbtSysConfig/${configId}`,
    method: 'get'
  })
}

// 根据键名获取系统配置
export function getSysConfigByKey(configKey: string) {
  return request<HbtApiResult<string>>({
    url: `/api/sys-config/key/${configKey}`,
    method: 'get'
  })
}

// 创建系统配置
export function createSysConfig(data: SysConfigCreate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtSysConfig',
    method: 'post',
    data
  })
}

// 更新系统配置
export function updateSysConfig(data: SysConfigUpdate) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtSysConfig',
    method: 'put',
    data
  })
}

// 删除系统配置
export function deleteSysConfig(configId: number) {
  return request<HbtApiResult<any>>({
    url: `/api/HbtSysConfig/${configId}`,
    method: 'delete'
  })
}

// 批量删除系统配置
export function batchDeleteSysConfig(configIds: number[]) {
  return request<HbtApiResult<any>>({
    url: '/api/HbtSysConfig/batch',
    method: 'delete',
    data: configIds
  })
}

// 导入系统配置
export function importSysConfig(file: File, sheetName: string = '系统配置信息') {
  const formData = new FormData()
  formData.append('file', file)
  return request<HbtApiResult<any>>({
    url: '/api/HbtSysConfig/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出系统配置
export function exportSysConfig(params: SysConfigQuery, sheetName: string = '系统配置信息') {
  return request({
    url: '/api/HbtSysConfig/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

// 获取系统配置导入模板
export function getSysConfigTemplate(sheetName: string = '系统配置信息') {
  return request({
    url: '/api/HbtSysConfig/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 刷新系统配置缓存
export function refreshConfigCache() {
  return request<HbtApiResult<any>>({
    url: '/api/HbtSysConfig/refresh',
    method: 'post'
  })
} 