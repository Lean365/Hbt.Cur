import request from '@/utils/request'
import type { HbtApiResult } from '@/types/common'
import type { 
  OAuthParams,
  OAuthResult
} from '@/types/identity/oauth'

// 获取OAuth授权地址
export function getOAuthUrl(provider: string) {
  return request<HbtApiResult<string>>({
    url: `/api/oauth/authorize/${provider}`,
    method: 'get'
  })
}

// OAuth回调
export function oauthCallback(provider: string, code: string, state: string) {
  return request<HbtApiResult<OAuthResult>>({
    url: `/api/oauth/callback/${provider}`,
    method: 'get',
    params: { code, state }
  })
}