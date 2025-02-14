// 登录参数接口
export interface LoginParams {
  TenantId: number;
  UserName: string;
  Password: string;
  CaptchaToken?: string;
  CaptchaOffset: number; // 验证码偏移量
}

// 用户信息接口
export interface UserInfo {
  userId: number;
  userName: string;
  nickName: string;
  tenantId: number;
  tenantName: string;
  roles: string[];
  permissions: string[];
}

// 登录响应接口
export interface LoginResult {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
  userInfo: UserInfo;
}

// 盐值响应接口
export interface SaltResponse {
  salt: string;
  iterations: number;
}

// 验证码响应接口
export interface CaptchaResponse {
  backgroundImage: string;
  sliderImage: string;
  token: string;
}

// 验证码验证结果接口
export interface CaptchaResult {
  success: boolean;
  message?: string;
} 