/**
 * 应用通用配置类型定义
 */

/**
 * 水印配置
 */
export interface HbtWatermarkConfig {
  /** 是否启用水印 */
  enabled: boolean
  /** 水印内容（支持单行字符串或多行字符串数组） */
  content: string | string[]
  /** 水印字体大小 */
  fontSize: number
  /** 水印颜色（可选，不设置则使用默认颜色） */
  color?: string
  /** 水印透明度 */
  opacity: number
  /** 水印旋转角度 */
  rotate: number
  /** 水印间距 */
  gap: [number, number]
}

/**
 * 用户注册配置
 */
export interface HbtRegisterConfig {
  /** 是否显示注册入口 */
  showRegister: boolean
  /** 是否允许用户注册 */
  allowRegister: boolean
  /** 注册是否需要邮箱验证 */
  requireEmailVerification: boolean
  /** 注册是否需要手机验证 */
  requirePhoneVerification: boolean
}

/**
 * 系统功能配置
 */
export interface HbtFeatureConfig {
  /** 是否显示帮助文档 */
  showHelp: boolean
  /** 是否显示反馈功能 */
  showFeedback: boolean
  /** 是否显示在线用户 */
  showOnlineUsers: boolean
  /** 是否显示系统公告 */
  showAnnouncement: boolean
}

/**
 * 安全配置
 */
export interface HbtSecurityConfig {
  /** 是否启用验证码 */
  enableCaptcha: boolean
  /** 密码最小长度 */
  passwordMinLength: number
  /** 密码复杂度要求 */
  passwordComplexity: {
    requireUppercase: boolean
    requireLowercase: boolean
    requireNumbers: boolean
    requireSpecialChars: boolean
  }
  /** 登录失败锁定次数 */
  loginFailLockCount: number
  /** 登录失败锁定时间（分钟） */
  loginFailLockTime: number
}

/**
 * 主题配置
 */
export interface HbtThemeConfig {
  /** 主题模式 */
  mode: 'light' | 'dark' | 'auto'
  /** 主色调 */
  primaryColor: string
  /** 是否启用动画 */
  enableAnimation: boolean
  /** 是否启用紧凑模式 */
  compact: boolean
}

/**
 * Logo配置
 */
export interface HbtLogoConfig {
  /** Logo图片路径 */
  src: string
  /** Logo图片alt属性 */
  alt: string
  /** Logo宽度 */
  width: number
  /** Logo高度 */
  height: number
  /** 是否显示文字 */
  showText: boolean
  /** Logo文字 */
  text: string
  /** 文字大小 */
  textSize: number
  /** 文字粗细 */
  textWeight: number
}

/**
 * 应用全局配置
 */
export interface HbtAppConfig {
  /** 水印配置 */
  watermark: HbtWatermarkConfig
  /** 注册配置 */
  register: HbtRegisterConfig
  /** 功能配置 */
  features: HbtFeatureConfig
  /** 安全配置 */
  security: HbtSecurityConfig
  /** 主题配置 */
  theme: HbtThemeConfig
  /** 系统标题 */
  title: string
  /** 系统副标题 */
  subtitle: string
  /** 系统Logo配置 */
  logo: HbtLogoConfig
  /** 版权信息（可选，使用国际化文件） */
  copyright?: string
  /** 版本号（可选，从后端获取） */
  version?: string
}

/**
 * 配置更新请求
 */
export interface HbtConfigUpdateRequest {
  /** 配置键 */
  key: string
  /** 配置值 */
  value: any
}

/**
 * 配置响应
 */
export interface HbtConfigResponse {
  /** 响应码 */
  code: number
  /** 响应消息 */
  message: string
  /** 配置数据 */
  data: HbtAppConfig
} 