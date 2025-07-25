import request from '@/utils/request'

/**
 * 获取二维码
 */
export const getQrCode = (qrCodeType?: string) => {
  return request({
    url: '/api/HbtQrCodeAuth/generate',
    method: 'POST',
    data: {
      qrCodeType: qrCodeType || 'Login'
    }
  })
}

/**
 * 检查二维码状态
 */
export const checkQrCodeStatus = (qrCodeId: string) => {
  return request({
    url: '/api/HbtQrCodeAuth/check-status',
    method: 'POST',
    data: { qrCodeId }
  })
}

/**
 * 扫描二维码
 */
export const scanQrCode = (qrCodeId: string) => {
  return request({
    url: `/api/HbtQrCodeAuth/scan/${qrCodeId}`,
    method: 'POST'
  })
}

/**
 * 确认二维码登录
 */
export const confirmQrCodeLogin = (data: { qrCodeId: string; userId: number; confirm: boolean }) => {
  return request({
    url: '/api/HbtQrCodeAuth/confirm',
    method: 'POST',
    data
  })
}

/**
 * 拒绝二维码登录
 */
export const rejectQrCodeLogin = (qrCodeId: string) => {
  return request({
    url: `/api/HbtQrCodeAuth/reject/${qrCodeId}`,
    method: 'POST'
  })
}

/**
 * 取消二维码
 */
export const cancelQrCode = (qrCodeId: string) => {
  return request({
    url: `/api/HbtQrCodeAuth/cancel/${qrCodeId}`,
    method: 'POST'
  })
} 