import request from '@/utils/request'

/**
 * 用户名密码登录
 */
export const passwordLogin = (data: {
  userName: string
  password: string
  captcha?: number
  captchaToken?: string
  rememberMe?: boolean
}) => {
  return request({
    url: '/api/identity/login',
    method: 'POST',
    data
  })
}

/**
 * 登出
 */
export const logout = () => {
  return request({
    url: '/api/identity/logout',
    method: 'POST'
  })
}

/**
 * 刷新Token
 */
export const refreshToken = () => {
  return request({
    url: '/api/identity/refresh-token',
    method: 'POST'
  })
} 