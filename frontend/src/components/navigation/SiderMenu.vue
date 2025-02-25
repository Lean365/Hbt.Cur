<template>
  <a-menu
    v-model:selectedKeys="selectedKeys"
    v-model:openKeys="openKeys"
    mode="inline"
    :theme="theme"
    :items="menuItems"
    class="side-menu"
    @click="handleMenuClick"
  />
</template>

<script lang="ts" setup>
import { ref, computed, watch, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter, type RouteRecordRaw } from 'vue-router'
import type { MenuProps } from 'ant-design-vue'
import type { MenuInfo } from 'ant-design-vue/lib/menu/src/interface'
import type { ItemType } from 'ant-design-vue/es/menu/src/hooks/useItems'
import { useThemeStore } from '@/stores/theme'
import { useMenuStore } from '@/stores/menu'
import type { Menu } from '@/types/identity/menu'
import { HbtMenuType } from '@/types/base'
import * as Icons from '@ant-design/icons-vue'
import { findRouteByPath, handleRouteNavigation } from '@/router'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const themeStore = useThemeStore()
const menuStore = useMenuStore()

// 响应式状态
const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])
const theme = computed(() => (themeStore.isDarkMode ? 'dark' : 'light'))

// 图标映射
const iconMap = Icons

interface MenuItemType {
  key: string
  icon?: () => VNode
  label: string
  children?: MenuItemType[]
}

// 查找指定路径的菜单项
const findMenuByPath = (menus: Menu[] | undefined, path: string): Menu | undefined => {
  if (!menus) return undefined

  for (const menu of menus) {
    if (menu.path === path) {
      return menu
    }
    if (menu.children?.length) {
      const found = findMenuByPath(menu.children, path)
      if (found) return found
    }
  }
  return undefined
}

// 处理基础菜单项
const processBaseMenu = (child: RouteRecordRaw) => {
  const meta = child.meta
  if (!meta?.title || meta.requiresAuth === false) return null

  return {
    key: child.path.startsWith('/') ? child.path : `/${child.path}`,
    icon: meta.icon ? () => h(iconMap[meta.icon as keyof typeof iconMap]) : undefined,
    label: t(meta.title as string)
  }
}

// 处理带子菜单的菜单项
const processSubMenus = (child: RouteRecordRaw) => {
  const meta = child.meta
  if (!meta?.title || meta.requiresAuth === false || !child.children) return null

  const parentPath = child.path.startsWith('/') ? child.path : `/${child.path}`

  return {
    key: parentPath,
    icon: meta.icon ? () => h(iconMap[meta.icon as keyof typeof iconMap]) : undefined,
    label: t(meta.title as string),
    children: child.children.map((subChild: RouteRecordRaw) => {
      const subMeta = subChild.meta
      const childPath = subChild.path.startsWith('/')
        ? subChild.path
        : `${parentPath}/${subChild.path}`
      return {
        key: childPath,
        icon: subMeta?.icon ? () => h(iconMap[subMeta.icon as keyof typeof iconMap]) : undefined,
        label: t((subMeta?.title as string) || '')
      }
    })
  }
}

// 处理动态菜单项
const processMenuItem = (menu: Menu, parentPath: string = ''): MenuItemType => {
  // 确保menu对象存在
  if (!menu) {
    console.warn('[菜单组件] 无效的菜单项')
    return {
      key: parentPath || '/',
      label: '未命名菜单'
    }
  }

  // 处理路径
  const menuPath = menu.path || ''
  let fullPath = ''

  if (menu.menuType === HbtMenuType.Directory) {
    // 目录类型使用原始路径
    fullPath = menuPath.startsWith('/') ? menuPath : `/${menuPath}`
  } else if (parentPath) {
    // 子菜单使用相对路径，需要和父路径组合
    const relativePath = menuPath.startsWith('/') ? menuPath.slice(1) : menuPath
    fullPath = `${parentPath}/${relativePath}`.replace(/\/+/g, '/')
  } else {
    // 顶层菜单使用完整路径
    fullPath = menuPath.startsWith('/') ? menuPath : `/${menuPath}`
  }

  // console.log('[菜单组件] 处理菜单项:', {
  //   菜单名称: menu.menuName,
  //   菜单类型: menu.menuType,
  //   原始路径: menuPath,
  //   父级路径: parentPath,
  //   完整路径: fullPath
  // })

  // 构建菜单项
  const result: MenuItemType = {
    key: fullPath,
    icon: menu.icon ? () => h(iconMap[menu.icon as keyof typeof iconMap]) : undefined,
    label: menu.transKey ? t(menu.transKey) : menu.menuName
  }

  // 处理子菜单
  if (menu.children?.length) {
    result.children = menu.children.map(child => processMenuItem(child, fullPath)).filter(Boolean)
  }

  return result
}

// 菜单配置
const menuItems = computed<MenuProps['items']>(() => {
  if (menuStore.isLoading) {
    console.log('[菜单组件] 菜单加载中')
    return []
  }

  // 调试日志：检查菜单数据
  // console.log('[菜单组件] 菜单数据:', {
  //   原始菜单列表: menuStore.rawMenuList,
  //   处理后菜单列表: menuStore.menuList,
  //   加载状态: menuStore.isLoading
  // })

  // 获取根路由
  const rootRoute = router.getRoutes().find(route => route.path === '/')
  if (!rootRoute?.children) return []

  // 主页和仪表盘菜单
  const baseMenus = rootRoute.children
    .filter(child => ['home', 'dashboard'].includes(child.path))
    .map(child => (child.children ? processSubMenus(child) : processBaseMenu(child)))
    .filter(Boolean)

  // 动态菜单（从后端获取）
  const dynamicMenus = (menuStore.rawMenuList || []).map(menu => processMenuItem(menu))

  // 关于菜单
  const aboutMenu = rootRoute.children
    .filter(child => child.path === '/about')
    .map(child => processSubMenus(child))
    .filter(Boolean)

  // 合并所有菜单：主页/仪表盘 + 动态菜单 + About菜单
  const allMenus = [...baseMenus, ...dynamicMenus, ...aboutMenu]

  // console.log('[菜单组件] 构建菜单项:', {
  //   基础菜单: baseMenus,
  //   动态菜单: dynamicMenus,
  //   关于菜单: aboutMenu,
  //   所有菜单: allMenus
  // })

  return allMenus
})

// 监听路由变化，更新选中状态
watch(
  () => route.path,
  path => {
    const normalizedPath = path.startsWith('/') ? path : `/${path}`
    // console.log('[菜单组件] 路由变化:', {
    //   path: normalizedPath,
    //   selectedKeys: selectedKeys.value,
    //   openKeys: openKeys.value
    // })

    // 设置选中的菜单项
    selectedKeys.value = [normalizedPath]

    // 展开当前路径的父级菜单
    const pathParts = normalizedPath.split('/').filter(Boolean)
    if (pathParts.length > 1) {
      const parentKeys = []
      let currentPath = ''
      for (const part of pathParts.slice(0, -1)) {
        currentPath += `/${part}`
        // 查找对应的目录菜单
        const menuItem = findMenuByPath(menuStore.rawMenuList, currentPath)
        if (menuItem && menuItem.menuType === HbtMenuType.Directory) {
          parentKeys.push(`dir_${menuItem.menuId}`)
        }
      }
      openKeys.value = parentKeys
    }
  },
  { immediate: true }
)

// 监听菜单数据变化
watch(
  () => menuStore.menuList,
  newMenuList => {
    console.log('[菜单组件] 菜单数据变化:', {
      hasData: !!newMenuList,
      length: newMenuList?.length
    })
  },
  { immediate: true }
)

// 查找菜单项
const findMenuItem = (menus: Menu[] | undefined, key: string): Menu | undefined => {
  if (!menus) {
    console.log('[菜单查找] 菜单列表为空')
    return undefined
  }

  // 标准化查找的key
  const normalizedKey = key.replace(/\/+/g, '/')

  // console.log('[菜单查找] 开始查找菜单项:', {
  //   查找的Key: normalizedKey,
  //   菜单列表长度: menus.length,
  //   菜单列表: menus.map(m => ({
  //     ID: m.menuId,
  //     名称: m.menuName,
  //     路径: m.path,
  //     类型: m.menuType
  //   }))
  // })

  // 递归查找菜单项
  const findMenuWithKey = (menu: Menu, parentPath: string = ''): Menu | undefined => {
    // 处理路径
    const menuPath = menu.path || ''
    let fullPath = ''

    if (menu.menuType === HbtMenuType.Directory) {
      // 目录类型使用完整路径
      fullPath = menuPath.startsWith('/') ? menuPath : `/${menuPath}`
    } else {
      // 子菜单使用相对路径,需要考虑父路径
      const relativePath = menuPath.startsWith('/') ? menuPath.slice(1) : menuPath
      fullPath = parentPath
        ? `${parentPath}/${relativePath}`.replace(/\/+/g, '/')
        : `/${relativePath}`
    }

    // 规范化路径
    fullPath = fullPath.replace(/\/+/g, '/')

    // console.log('[菜单查找] 检查菜单项:', {
    //   菜单ID: menu.menuId,
    //   菜单名称: menu.menuName,
    //   菜单类型: menu.menuType,
    //   原始路径: menuPath,
    //   父级路径: parentPath,
    //   完整路径: fullPath,
    //   查找的Key: normalizedKey,
    //   匹配结果: fullPath === normalizedKey
    // })

    if (fullPath === normalizedKey) {
      console.log('[菜单查找] 找到匹配的菜单项:', menu)
      return menu
    }

    // 递归查找子菜单
    if (menu.children?.length) {
      for (const child of menu.children) {
        const found = findMenuWithKey(child, fullPath)
        if (found) {
          console.log('[菜单查找] 在子菜单中找到匹配项:', {
            父菜单: menu.menuName,
            子菜单: found.menuName,
            菜单路径: fullPath
          })
          return found
        }
      }
    }

    return undefined
  }

  // 遍历顶层菜单
  for (const menu of menus) {
    const found = findMenuWithKey(menu, '')
    if (found) return found
  }

  console.log('[菜单查找] 未找到匹配的菜单项:', normalizedKey)
  return undefined
}

// 处理菜单点击
const handleMenuClick = async (info: MenuInfo) => {
  if (!info.key || typeof info.key !== 'string') return

  console.log('[菜单点击] 原始信息:', {
    点击的Key: info.key,
    菜单项: info,
    父级路径: info.keyPath?.slice(1) || []
  })

  // 确保路径以斜杠开头
  const routePath = info.key.startsWith('/') ? info.key : `/${info.key}`

  console.log('[菜单点击] 处理后的路径:', {
    原始路径: info.key,
    处理后路径: routePath
  })

  // 使用路由工具函数处理导航
  const success = await handleRouteNavigation(routePath)
  if (!success) {
    // 如果导航失败，重置选中状态
    selectedKeys.value = [route.path]
  }
}
</script>

<style lang="less" scoped>
.side-menu {
  height: 100%;
  border-right: 0;

  :deep(.anticon) {
    min-width: 14px;
    margin-right: 10px;
    font-size: 16px;
  }
}
</style>
