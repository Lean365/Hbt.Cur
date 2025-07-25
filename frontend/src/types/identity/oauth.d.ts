import type { HbtBaseEntity, HbtPagedQuery, HbtPagedResult } from '@/types/common'

// ==================== CRUD相关类型 ====================

/**
 * OAuth账号
 */
export interface HbtOAuth extends HbtBaseEntity {
  /** OAuth账号ID */
  id: number;
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 绑定时间 */
  bindTime: string;
  /** 是否主要账号 */
  isPrimary: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark?: string;
  /** 用户信息 */
  user?: {
    id: number;
    userName: string;
    nickName: string;
    email: string;
    avatar: string;
  };
}

/**
 * OAuth查询
 */
export interface HbtOAuthQuery extends HbtPagedQuery {
  /** 用户ID */
  userId?: number;
  /** 提供商 */
  provider?: string;
  /** OAuth用户ID */
  oauthUserId?: string;
  /** OAuth用户名 */
  oauthUserName?: string;
  /** 状态 */
  status?: number;
  /** 开始时间 */
  startTime?: string;
  /** 结束时间 */
  endTime?: string;
}

/**
 * OAuth分页结果
 */
export interface HbtOAuthPagedResult extends HbtPagedResult<HbtOAuth> {}

/**
 * OAuth创建
 */
export interface HbtOAuthCreate {
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 是否主要账号 */
  isPrimary?: number;
  /** 状态 */
  status?: number;
  /** 备注 */
  remark?: string;
}

/**
 * OAuth表单
 */
export interface HbtOAuthForm {
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 是否主要账号 */
  isPrimary: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark: string;
}

/**
 * OAuth更新
 */
export interface HbtOAuthUpdate {
  /** OAuth账号ID */
  id: number;
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 是否主要账号 */
  isPrimary?: number;
  /** 状态 */
  status?: number;
  /** 备注 */
  remark?: string;
}

/**
 * OAuth状态
 */
export interface HbtOAuthStatus {
  /** OAuth账号ID */
  id: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark?: string;
}

// ==================== 导入导出类型 ====================

/**
 * OAuth导入
 */
export interface HbtOAuthImport {
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 是否主要账号 */
  isPrimary: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark?: string;
}

/**
 * OAuth导出
 */
export interface HbtOAuthExport {
  /** OAuth账号ID */
  id: number;
  /** 用户ID */
  userId: number;
  /** 用户名 */
  userName: string;
  /** 昵称 */
  nickName: string;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 绑定时间 */
  bindTime: string;
  /** 是否主要账号 */
  isPrimary: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark?: string;
  /** 创建时间 */
  createTime: string;
  /** 更新时间 */
  updateTime: string;
}

/**
 * OAuth模板
 */
export interface HbtOAuthTemplate {
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 是否主要账号 */
  isPrimary: number;
  /** 状态 */
  status: number;
  /** 备注 */
  remark: string;
}

// ==================== 绑定管理类型 ====================

/**
 * OAuth绑定
 */
export interface HbtOAuthBind {
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 是否主要账号 */
  isPrimary?: number;
}

/**
 * OAuth解绑
 */
export interface HbtOAuthUnbind {
  /** 用户ID */
  userId: number;
  /** 提供商 */
  provider: string;
}

/**
 * OAuth账号（用户视角）
 */
export interface HbtOAuthAccount {
  /** OAuth账号ID */
  id: number;
  /** 提供商 */
  provider: string;
  /** OAuth用户ID */
  oauthUserId: string;
  /** OAuth用户名 */
  oauthUserName: string;
  /** 绑定时间 */
  bindTime: string;
  /** 是否主要账号 */
  isPrimary: number;
  /** 状态 */
  status: number;
  /** 提供商信息 */
  providerInfo?: HbtOAuthProvider;
}

// ==================== 登录相关类型 ====================

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
 * 登录结果
 */
export interface HbtLoginResult {
  /** 是否成功 */
  success: boolean;
  /** 访问令牌 */
  accessToken: string;
  /** 刷新令牌 */
  refreshToken: string;
  /** 过期时间（秒） */
  expiresIn: number;
  /** 用户信息 */
  userInfo?: {
    /** 用户ID */
    userId: number;
    /** 用户名 */
    userName: string;
    /** 昵称 */
    nickName: string;
    /** 邮箱 */
    email: string;
    /** 头像 */
    avatar: string;
    /** 角色列表 */
    roles: string[];
    /** 权限列表 */
    permissions: string[];
  };
  /** 错误信息 */
  message?: string;
}

// ==================== 提供商相关类型 ====================

/**
 * OAuth提供商
 */
export interface HbtOAuthProvider {
  /** 提供商名称 */
  name: string;
  /** 显示名称 */
  displayName: string;
  /** 是否启用 */
  enabled: boolean;
  /** 图标URL */
  iconUrl: string;
  /** 授权范围 */
  scope: string;
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