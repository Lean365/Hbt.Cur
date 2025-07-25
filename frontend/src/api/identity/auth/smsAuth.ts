import request from '@/utils/request'

/**
 * 发送短信验证码
 */
export const sendSmsCode = (data: { phone: string }) => {
  return request({
    url: '/api/HbtSmsAuth/send-code',
    method: 'POST',
    data
  })
}

/**
 * 短信验证码登录
 */
export const smsLogin = (data: { phone: string; verificationCode: string }) => {
  return request({
    url: '/api/HbtSmsAuth/login',
    method: 'POST',
    data
  })
} 