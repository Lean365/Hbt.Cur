import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { HbtMenu   } from '@/types/identity/menu'
import { getCurrentUserMenus } from '@/api/identity/menu'
import { registerDynamicRoutes } from '@/utils/route'
import type { HbtApiResponse } from '@/types/common'
import { message } from 'ant-design-vue'
import { transformMenu } from '@/utils/menu'
import i18n from '@/locales'
import { getMenuList } from '@/api/identity/menu'
import type { RouteRecordRaw, Router } from 'vue-router'
import { formatMenuToRoutes } from '@/utils/route'

const { t } = i18n.global

export const useMenuStore = defineStore('menu', () => {
  const rawMenuList = ref<HbtMenu[]>([])
  const menuList = ref<HbtMenu[]>([])
  const isLoading = ref(false)
  const isMenuLoaded = ref(false)

  const loadUserMenus = async () => {
    if (isMenuLoaded.value && rawMenuList.value?.length > 0) {
      return true
    }

    if (isLoading.value) {
      return false
    }

    isLoading.value = true

    try {
      const response = await getCurrentUserMenus()

      if (!response || !response.data) {
        message.error(t('menu.error.loadFailed.invalidResponse'))
        return false
      }

      const apiResult = response as unknown as HbtApiResponse<HbtMenu[]>

      if (apiResult.code !== 200) {
        message.error(
          apiResult.msg || t('menu.error.loadFailed.businessError', { code: apiResult.code })
        )
        return false
      }

      const menus = apiResult.data
      if (!Array.isArray(menus)) {
        message.error(t('menu.error.loadFailed.invalidFormat'))
        return false
      }

      console.log('[菜单] 原始菜单数据:', {
        总数: menus.length,
        菜单项: menus.map(m => ({
          菜单ID: m.menuId,
          名称: m.menuName,
          路径: m.path,
          组件: m.component,
          类型: m.menuType,
          状态: m.status,
          权限: m.perms
        }))
      })

      rawMenuList.value = menus
      menuList.value = menus
      isMenuLoaded.value = true

      return true
    } catch (error) {
      message.error(t('menu.error.loadFailed.retry'))
      return false
    } finally {
      isLoading.value = false
    }
  }

  const clearMenus = () => {
    rawMenuList.value = []
    menuList.value = []
    isMenuLoaded.value = false
    isLoading.value = false
  }

  const reloadMenus = async (router: Router) => {
    try {
      if (!router) {
        return []
      }
      isLoading.value = true
      const response = await getCurrentUserMenus()
      if (response.status === 200) {
        const data = response.data as HbtApiResponse<HbtMenu[]>
        if (data.code === 200) {
          const processedMenus = data.data.map(menu => {
            const processedMenu = {
              ...menu,
              menuId: menu.menuId,
              menuName: menu.menuName || `Menu_${menu.menuId}`,
              children: menu.children?.map(child => ({
                ...child,
                menuId: child.menuId,
                menuName: child.menuName || `Menu_${child.menuId}`,
                parentId: menu.menuId,
                component: child.component || '',
                path: child.path || '',
                menuType: child.menuType || 0,
                status: child.status || 0,
                perms: child.perms || ''
              }))
            }
            return processedMenu
          })
          rawMenuList.value = processedMenus
          menuList.value = processedMenus
          await registerDynamicRoutes(processedMenus, router)
          // 动态路由注册完成后再 resolve
          return processedMenus
        } else {
          console.error('[菜单] 加载失败:', data.msg)
          return []
        }
      } else {
        console.error('[菜单] 加载失败:', response.statusText)
        return []
      }
    } catch (error) {
      console.error('[菜单] 加载出错:', error)
      message.error(t('menu.loadError'))
      return []
    } finally {
      isLoading.value = false
    }
  }

  return {
    rawMenuList,
    menuList,
    isLoading,
    isMenuLoaded,
    loadUserMenus,
    clearMenus,
    reloadMenus
  }
})
