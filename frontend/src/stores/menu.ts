import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { MenuProps } from 'ant-design-vue'
import type { Menu } from '@/types/identity/menu'
import type { ApiResult } from '@/types/base'
import { getCurrentUserMenus } from '@/api/identity/menu'
import { message } from 'ant-design-vue'
import { transformMenu } from '@/utils/menu'

export const useMenuStore = defineStore('menu', () => {
  // 菜单加载状态
  const isLoading = ref(false)
  // 原始菜单列表
  const rawMenuList = ref<Menu[]>([])
  // 处理后的菜单树
  const menuList = ref<MenuProps['items']>([])
  // 菜单是否已加载
  const isMenuLoaded = ref(false)

  // 加载用户菜单
  const loadUserMenus = async () => {
    // 如果菜单已加载且存在，直接返回true
    if (isMenuLoaded.value && rawMenuList.value?.length > 0) {
      console.log('[菜单加载] 菜单已加载，跳过加载')
      return true
    }

    // 如果正在加载中，等待加载完成
    if (isLoading.value) {
      console.log('[菜单加载] 正在加载中，等待加载完成')
      return false
    }

    console.log('[菜单加载] 开始加载用户菜单')
    isLoading.value = true

    try {
      const response = await getCurrentUserMenus()
      console.log('[菜单加载] API响应:', response)

      // 检查响应数据
      if (!response || !response.data) {
        console.error('[菜单加载] 响应无效:', response)
        message.error('获取菜单数据失败：响应无效')
        return false
      }

      const apiResult = response as unknown as ApiResult<Menu[]>

      // 检查业务状态码
      if (apiResult.code !== 200) {
        console.warn('[菜单加载] 业务状态码错误:', { code: apiResult.code, msg: apiResult.msg })
        message.error(apiResult.msg || `获取菜单数据失败：错误码 ${apiResult.code}`)
        return false
      }

      // 检查菜单数据
      const menus = apiResult.data
      if (!Array.isArray(menus)) {
        console.error('[菜单加载] 菜单数据格式错误:', menus)
        message.error('获取菜单数据失败：数据格式错误')
        return false
      }

      // 更新菜单数据
      rawMenuList.value = menus
      menuList.value = transformMenu(menus)
      isMenuLoaded.value = true

      console.log('[菜单加载] 加载完成，菜单数据:', {
        rawMenuList: menus,
        menuList: menuList.value
      })
      return true
    } catch (error) {
      console.error('[菜单加载] 发生错误:', error)
      message.error('加载菜单失败，请重试')
      return false
    } finally {
      isLoading.value = false
    }
  }

  // 清除菜单数据
  const clearMenus = () => {
    rawMenuList.value = []
    menuList.value = []
    isMenuLoaded.value = false
    isLoading.value = false
  }

  return {
    isLoading,
    rawMenuList,
    menuList,
    isMenuLoaded,
    loadUserMenus,
    clearMenus
  }
}) 