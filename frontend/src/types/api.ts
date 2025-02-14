// API响应包装
export interface HbtApiResult<T> {
  code: number;
  msg: string;
  data: T;
}

// 验证码结果
export interface CaptchaResult {
  token: string;
  offset: number;
  success: boolean;
  message?: string;
}

// 登录参数
export interface LoginParams {
  TenantId: number;
  UserName: string;
  Password: string;
  CaptchaToken?: string;
  CaptchaOffset?: number;
}

// 盐值响应
export interface SaltResponse {
  salt: string;
  iterations: number;
} 