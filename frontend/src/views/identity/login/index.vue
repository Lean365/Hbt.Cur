// 构建登录参数
const loginParams: LoginParams = {
  tenantId: formState.tenantId,
  userName: formState.userName,
  password: encryptedPassword,
  captchaToken: formState.captchaToken,
  captchaOffset: formState.captchaOffset,
  ipAddress: window.location.hostname,
  userAgent: navigator.userAgent,
  loginType: 0, // 密码登录
  loginSource: 0, // Web登录
  deviceInfo: deviceInfo.value
}; 