//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : loginMethod.ts
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 登录方法API
//===================================================================

import request from '@/utils/request'

/**
 * 登录方式配置接口
 */
export interface LoginMethod {
  key: string
  name: string
  type: string
  options?: LoginMethod[]
  providers?: OAuthProvider[]
}

/**
 * OAuth提供商接口
 */
export interface OAuthProvider {
  key: string
  name: string
  icon: string
}

/**
 * 登录配置选项接口
 */
export interface LoginOptions {
  passwordLogin: {
    enabled: boolean
    name: string
    key: string
  }
  smsLogin: {
    enabled: boolean
    name: string
    key: string
  }
  qrCodeLogin: {
    enabled: boolean
    name: string
    key: string
    options: {
      weChatLogin: {
        enabled: boolean
        name: string
        key: string
      }
      alipayLogin: {
        enabled: boolean
        name: string
        key: string
      }
    }
  }
  oauthLogin: {
    enabled: boolean
    name: string
    key: string
    providers: {
      github: {
        enabled: boolean
        name: string
        key: string
        icon: string
      }
      google: {
        enabled: boolean
        name: string
        key: string
        icon: string
      }
      facebook: {
        enabled: boolean
        name: string
        key: string
        icon: string
      }
      twitter: {
        enabled: boolean
        name: string
        key: string
        icon: string
      }
      qq: {
        enabled: boolean
        name: string
        key: string
        icon: string
      }
      gitee: {
        enabled: boolean
        name: string
        key: string
        icon: string
      }
      weibo: {
        enabled: boolean
        name: string
        key: string
        icon: string
      }
    }
  }
}

/**
 * 获取登录配置信息
 */
export function getLoginOptions() {
  return request<LoginOptions>({
    url: '/api/HbtLoginMethod/login-options',
    method: 'GET'
  })
}

/**
 * 获取启用的登录方式列表
 */
export function getEnabledLoginMethods() {
  return request<LoginMethod[]>({
    url: '/api/HbtLoginMethod/enabled-login-methods',
    method: 'GET'
  })
} 