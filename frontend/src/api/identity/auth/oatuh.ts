import request from '@/utils/request'
import type { HbtApiResponse, HbtPagedResult } from '@/types/common'
import type { 
  HbtOAuth,
  HbtOAuthQuery,
  HbtOAuthCreate,
  HbtOAuthUpdate,
  HbtOAuthStatus,
  HbtOAuthBind,
  HbtOAuthUnbind,
  HbtOAuthProvider,
  HbtOAuthAccount,
  HbtLoginResult,
  HbtOAuthPagedResult
} from '@/types/identity/oauth'

// ==================== OAuth账号CRUD操作 ====================

/**
 * 获取OAuth账号分页列表
 */
export function getOAuthList(params: HbtOAuthQuery) {
  return request<HbtApiResponse<HbtOAuthPagedResult>>({
    url: '/api/HbtOAuth/list',
    method: 'get',
    params
  })
}

/**
 * 获取OAuth账号详情
 */
export function getOAuthById(id: number) {
  return request<HbtApiResponse<HbtOAuth>>({
    url: `/api/HbtOAuth/${id}`,
    method: 'get'
  })
}

/**
 * 获取OAuth账号详情（别名）
 */
export function getOAuth(id: number) {
  return getOAuthById(id)
}

/**
 * 创建OAuth账号
 */
export function createOAuth(data: HbtOAuthCreate) {
  return request<HbtApiResponse<number>>({
    url: '/api/HbtOAuth',
    method: 'post',
    data
  })
}

/**
 * 更新OAuth账号
 */
export function updateOAuth(data: HbtOAuthUpdate) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOAuth',
    method: 'put',
    data
  })
}

/**
 * 删除OAuth账号
 */
export function deleteOAuth(id: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOAuth/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除OAuth账号
 */
export function batchDeleteOAuth(ids: number[]) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOAuth/batch',
    method: 'delete',
    data: ids
  })
}

// ==================== 导入导出操作 ====================

/**
 * 导入OAuth账号数据
 */
export function importOAuthAccounts(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  
  return request<HbtApiResponse<{
    success: number;
    failed: number;
    errors: string[];
  }>>({
    url: '/api/HbtOAuth/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出OAuth账号数据
 */
export function exportOAuthAccounts(params: HbtOAuthQuery) {
  return request<Blob>({
    url: '/api/HbtOAuth/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取OAuth账号导入模板
 */
export function getOAuthImportTemplate() {
  return request<Blob>({
    url: '/api/HbtOAuth/template',
    method: 'get',
    responseType: 'blob'
  })
}

// ==================== OAuth账号状态管理 ====================

/**
 * 更新OAuth账号状态
 */
export function updateOAuthStatus(id: number, status: number) {
  return request<HbtApiResponse<boolean>>({
    url: `/api/HbtOAuth/${id}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 批量更新OAuth账号状态
 */
export function batchUpdateOAuthStatus(ids: number[], status: number, remark?: string) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOAuth/batch/status',
    method: 'put',
    data: ids,
    params: { status, remark }
  })
}

// ==================== OAuth账号绑定管理 ====================

/**
 * 绑定OAuth账号
 */
export function bindOAuthAccount(data: HbtOAuthBind) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOAuth/bind',
    method: 'post',
    data
  })
}

/**
 * 解绑OAuth账号
 */
export function unbindOAuthAccount(data: HbtOAuthUnbind) {
  return request<HbtApiResponse<boolean>>({
    url: '/api/HbtOAuth/unbind',
    method: 'post',
    data
  })
}

/**
 * 获取用户的OAuth账号列表
 */
export function getUserOAuthAccounts(userId: number) {
  return request<HbtApiResponse<HbtOAuthAccount[]>>({
    url: `/api/HbtOAuth/user/${userId}/accounts`,
    method: 'get'
  })
}

// ==================== OAuth登录相关 ====================

/**
 * 获取OAuth授权地址
 */
export function getOAuthUrl(provider: string) {
  return request<HbtApiResponse<string>>({
    url: `/api/HbtOAuth/authorize/${provider}`,
    method: 'get'
  })
}

/**
 * OAuth回调处理
 */
export function oauthCallback(provider: string, code: string, state: string) {
  return request<HbtApiResponse<HbtLoginResult>>({
    url: `/api/HbtOAuth/callback/${provider}`,
    method: 'get',
    params: { code, state }
  })
}

/**
 * 获取支持的OAuth提供商列表
 */
export function getOAuthProviders() {
  return request<HbtApiResponse<HbtOAuthProvider[]>>({
    url: '/api/HbtOAuth/providers',
    method: 'get'
  })
}

// ==================== 具体第三方登录方法 ====================

/**
 * 微信登录
 */
export function wechatLogin() {
  return request<HbtApiResponse<{ success: boolean; authUrl: string }>>({
    url: '/api/HbtOAuth/wechat',
    method: 'post'
  })
}

/**
 * QQ登录
 */
export function qqLogin() {
  return request<HbtApiResponse<{ success: boolean; authUrl: string }>>({
    url: '/api/HbtOAuth/qq',
    method: 'post'
  })
}

/**
 * 微博登录
 */
export function weiboLogin() {
  return request<HbtApiResponse<{ success: boolean; authUrl: string }>>({
    url: '/api/HbtOAuth/weibo',
    method: 'post'
  })
}

/**
 * GitHub登录
 */
export function githubLogin() {
  return request<HbtApiResponse<{ success: boolean; authUrl: string }>>({
    url: '/api/HbtOAuth/github',
    method: 'post'
  })
}

/**
 * Google登录
 */
export function googleLogin() {
  return request<HbtApiResponse<{ success: boolean; authUrl: string }>>({
    url: '/api/HbtOAuth/google',
    method: 'post'
  })
}

/**
 * 钉钉登录
 */
export function dingTalkLogin() {
  return request<HbtApiResponse<{ success: boolean; authUrl: string }>>({
    url: '/api/HbtOAuth/dingtalk',
    method: 'post'
  })
}