// 系统状态枚举
export enum HbtStatus {
  Normal = 0, // 正常
  Disabled = 1 // 停用
}

// 是否枚举
export enum HbtYesNo {
  No = 0, // 否
  Yes = 1 // 是
}

// 菜单类型枚举
export enum HbtMenuType {
  Directory = 0, // 目录
  Menu = 1, // 菜单
  Button = 2 // 按钮
}

// 显示状态枚举
export enum HbtVisible {
  Show = 0, // 显示
  Hide = 1 // 隐藏
}

// 用户类型枚举
export enum HbtUserType {
  System = 0, // 系统用户
  User = 1 // 普通用户
}

// 性别枚举
export enum HbtGender {
  Unknown = 0, // 未知
  Male = 1, // 男
  Female = 2 // 女
}

// 数据范围枚举
export enum HbtDataScope {
  All = 1, // 全部数据权限
  Custom = 2, // 自定数据权限
  Dept = 3, // 本部门数据权限
  DeptAndChild = 4, // 本部门及以下数据权限
  Self = 5 // 仅本人数据权限
}

// 登录类型枚举
export enum HbtLoginType {
  Normal = 0, // 普通登录
  OAuth = 1, // OAuth登录
  SSO = 2 // SSO登录
}

// 登录来源枚举
export enum HbtLoginSource {
  Web = 0, // Web端
  App = 1, // App端
  WeChat = 2 // 微信端
}

// 登录状态枚举
export enum HbtLoginStatus {
  Offline = 0, // 离线
  Online = 1 // 在线
} 