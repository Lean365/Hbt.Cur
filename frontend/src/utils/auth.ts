const TokenKey = 'Admin-Token'

export function getToken(): string | null {
  const token = localStorage.getItem(TokenKey)
  console.log('[Token] 获取Token:', token ? '已存在' : '不存在')
  return token
}

export function setToken(token: string) {
  if (!token) {
    console.error('[Token] 试图设置空token')
    return
  }
  console.log('[Token] 设置Token:', token.substring(0, 10) + '...')
  return localStorage.setItem(TokenKey, token)
}

export function removeToken() {
  console.log('[Token] 移除Token')
  return localStorage.removeItem(TokenKey)
} 