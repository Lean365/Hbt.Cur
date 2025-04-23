import request from '@/utils/request'
import type { Menu } from '@/types/identity/menu'
import type { HbtApiResponse } from '@/types/common'

export const getCurrentUserMenus = () => {
  return request<HbtApiResponse<Menu[]>>({
    url: '/api/HbtMenu/current',
    method: 'get'
  })
} 