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

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const themeStore = useThemeStore()
const menuStore = useMenuStore()

// 响应式状态
const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

// 图标映射
const iconMap = Icons

interface MenuItemType {
  key: string;
  icon?: () => VNode;
  label: string;
  children?: MenuItemType[];
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
      const childPath = subChild.path.startsWith('/') ? subChild.path : `${parentPath}/${subChild.path}`
      return {
        key: childPath,
        icon: subMeta?.icon ? () => h(iconMap[subMeta.icon as keyof typeof iconMap]) : undefined,
        label: t(subMeta?.title as string || '')
      }
    })
  }
}

// 处理动态菜单项
const processMenuItem = (menu: Menu, parentPath: string = ''): MenuItemType => {
  // 调试日志
  console.log('[菜单组件] 处理菜单项:', {
    菜单对象: menu,
    父路径: parentPath,
    菜单名称: menu?.menuName,
    翻译键: menu?.transKey,
    菜单路径: menu?.path,
    菜单类型: menu?.menuType,
    图标: menu?.icon,
    子菜单数量: menu?.children?.length
  })

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
    // 目录类型：确保以/开头
    fullPath = menuPath.startsWith('/') ? menuPath : `/${menuPath}`
  } else {
    // 菜单类型：组合父路径
    if (parentPath) {
      // 确保父路径以/开头，并且不以/结尾
      const normalizedParentPath = parentPath.startsWith('/') ? parentPath : `/${parentPath}`
      const cleanParentPath = normalizedParentPath.endsWith('/') ? normalizedParentPath.slice(0, -1) : normalizedParentPath
      // 确保子路径不以/开头
      const cleanMenuPath = menuPath.startsWith('/') ? menuPath.slice(1) : menuPath
      fullPath = `${cleanParentPath}/${cleanMenuPath}`
    } else {
      fullPath = menuPath.startsWith('/') ? menuPath : `/${menuPath}`
    }
  }

  // 处理图标
  let icon
  if (menu.icon) {
    // 确保图标名称以 Outlined 结尾
    const iconName = menu.icon.endsWith('Outlined') ? menu.icon : `${menu.icon}Outlined`
    if (iconMap[iconName as keyof typeof iconMap]) {
      icon = () => h(iconMap[iconName as keyof typeof iconMap])
    } else {
      console.warn(`[菜单组件] 未找到图标: ${iconName}，原始图标名: ${menu.icon}`)
    }
  }

  // 构建菜单项
  const result: MenuItemType = {
    key: menu.menuType === HbtMenuType.Directory ? `dir_${menu.menuId}` : fullPath,
    icon,
    label: menu.transKey ? t(menu.transKey) : menu.menuName
  }

  // 处理子菜单
  if (menu.children?.length) {
    result.children = menu.children
      .map(child => processMenuItem(child, fullPath))
      .filter(Boolean)
  }

  console.log('[菜单组件] 生成菜单项:', {
    原始路径: menuPath,
    父路径: parentPath,
    完整路径: fullPath,
    菜单项: result
  })

  return result
}

// 菜单配置
const menuItems = computed<MenuProps['items']>(() => {
  if (menuStore.isLoading) {
    console.log('[菜单组件] 菜单加载中')
    return []
  }

  // 调试日志：检查菜单数据
  console.log('[菜单组件] 菜单数据:', {
    原始菜单列表: menuStore.rawMenuList,
    处理后菜单列表: menuStore.menuList,
    加载状态: menuStore.isLoading
  })

  // 获取根路由
  const rootRoute = router.getRoutes().find(route => route.path === '/')
  if (!rootRoute?.children) return []

  // 主页和仪表盘菜单
  const baseMenus = rootRoute.children
    .filter(child => ['home', 'dashboard'].includes(child.path))
    .map(child => child.children ? processSubMenus(child) : processBaseMenu(child))
    .filter(Boolean)

  // 动态菜单（从后端获取）
  const dynamicMenus = (menuStore.rawMenuList || []).map(menu => processMenuItem(menu))

  // 关于菜单
  const aboutMenu = rootRoute.children
    .filter(child => child.path === '/about')
    .map(child => processSubMenus(child))
    .filter(Boolean)

  // 合并所有菜单：主页/仪表盘 + 动态菜单 + About菜单
  const allMenus = [
    ...baseMenus,
    ...dynamicMenus,
    ...aboutMenu
  ]

  console.log('[菜单组件] 构建菜单项:', {
    基础菜单: baseMenus,
    动态菜单: dynamicMenus,
    关于菜单: aboutMenu,
    所有菜单: allMenus
  })

  return allMenus
})

// 监听路由变化，更新选中状态
watch(() => route.path, (path) => {
  const normalizedPath = path.startsWith('/') ? path : `/${path}`
  console.log('[菜单组件] 路由变化:', {
    path: normalizedPath,
    selectedKeys: selectedKeys.value,
    openKeys: openKeys.value
  })
  
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
}, { immediate: true })

// 监听菜单数据变化
watch(() => menuStore.menuList, (newMenuList) => {
  console.log('[菜单组件] 菜单数据变化:', {
    hasData: !!newMenuList,
    length: newMenuList?.length
  })
}, { immediate: true })

// 处理菜单点击
const handleMenuClick = (info: MenuInfo) => {
  console.log('[菜单点击] 点击信息:', {
    key: info.key,
    keyPath: info.keyPath,
    item: info.item,
    domEvent: info.domEvent
  })

  if (typeof info.key === 'string') {
    // 如果是目录菜单（以 dir_ 开头），查找对应的菜单项并导航到其路径
    if (info.key.startsWith('dir_')) {
      const menuId = info.key.replace('dir_', '')
      const findMenuById = (menus: Menu[] | undefined): Menu | undefined => {
        if (!menus) return undefined
        for (const menu of menus) {
          if (menu.menuId === menuId) return menu
          if (menu.children?.length) {
            const found = findMenuById(menu.children)
            if (found) return found
          }
        }
        return undefined
      }
      
      const menuItem = findMenuById(menuStore.rawMenuList)
      if (menuItem?.path) {
        const path = menuItem.path.startsWith('/') ? menuItem.path : `/${menuItem.path}`
        if (path !== route.path) {
          router.push(path)
        }
      }
      return
    }

    // 如果点击的是已选中的菜单项，不进行导航
    if (selectedKeys.value.includes(info.key)) {
      return
    }

    // 如果是普通菜单项，进行导航
    router.push(info.key)
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