// OAuth认证参数
export interface OAuthParams {
  provider: string;
  code: string;
  state: string;
  redirectUri: string;
}

// OAuth认证结果
export interface OAuthResult {
  token: string;
  refreshToken: string;
  expireTime: string;
  permissions: string[];
  roles: string[];
  user: {
    userId: number;
    userName: string;
    nickName: string;
    avatar: string;
  };
}

// OAuth提供商配置
export interface OAuthProvider {
  name: string;
  clientId: string;
  clientSecret: string;
  authorizeUrl: string;
  tokenUrl: string;
  userInfoUrl: string;
  redirectUri: string;
  scope: string;
  enabled: boolean;
}

// OAuth用户信息
export interface OAuthUserInfo {
  id: string;
  name: string;
  email: string;
  avatar: string;
  provider: string;
  rawData: any;
} 