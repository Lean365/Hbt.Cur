import type { HbtBaseEntity } from '@/types/common'

/**
 * OAuth认证参数
 */
export interface OAuthParams {
  /** 提供商 */
  provider: string;
  /** 授权码 */
  code: string;
  /** 状态码 */
  state: string;
  /** 重定向URI */
  redirectUri: string;
}

/**
 * OAuth认证结果
 */
export interface OAuthResult {
  /** 访问令牌 */
  token: string;
  /** 刷新令牌 */
  refreshToken: string;
  /** 过期时间 */
  expireTime: string;
  /** 权限列表 */
  permissions: string[];
  /** 角色列表 */
  roles: string[];
  /** 用户信息 */
  user: OAuthUser;
}

/**
 * OAuth用户信息
 */
export interface OAuthUser extends HbtBaseEntity {
  /** 用户ID */
  userId: number;
  /** 用户名 */
  userName: string;
  /** 昵称 */
  nickName: string;
  /** 头像 */
  avatar: string;
}

/**
 * OAuth提供商配置
 */
export interface OAuthProvider {
  /** 提供商名称 */
  name: string;
  /** 客户端ID */
  clientId: string;
  /** 客户端密钥 */
  clientSecret: string;
  /** 授权URL */
  authorizeUrl: string;
  /** 令牌URL */
  tokenUrl: string;
  /** 用户信息URL */
  userInfoUrl: string;
  /** 重定向URI */
  redirectUri: string;
  /** 授权范围 */
  scope: string;
  /** 是否启用 */
  enabled: boolean;
}

/**
 * OAuth用户信息
 */
export interface OAuthUserInfo {
  /** 用户ID */
  id: string;
  /** 用户名 */
  name: string;
  /** 邮箱 */
  email: string;
  /** 头像 */
  avatar: string;
  /** 提供商 */
  provider: string;
  /** 原始数据 */
  rawData: Record<string, unknown>;
} 