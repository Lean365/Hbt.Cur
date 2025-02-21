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
import { 
  HomeOutlined, 
  DashboardOutlined, 
  DesktopOutlined, 
  BarChartOutlined, 
  MonitorOutlined,
  InfoCircleOutlined,
  FileTextOutlined,
  SafetyOutlined
} from '@ant-design/icons-vue'

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
const iconMap = {
  HomeOutlined,
  DashboardOutlined,
  DesktopOutlined,
  BarChartOutlined,
  MonitorOutlined,
  InfoCircleOutlined,
  FileTextOutlined,
  SafetyOutlined
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

// 菜单配置
const menuItems = computed<MenuProps['items']>(() => {
  if (menuStore.isLoading) {
    console.log('[菜单组件] 菜单加载中')
    return []
  }

  // 获取根路由
  const rootRoute = router.getRoutes().find(route => route.path === '/')
  if (!rootRoute?.children) return []

  // 主页和仪表盘菜单
  const baseMenus = rootRoute.children
    .filter(child => ['home', 'dashboard'].includes(child.path))
    .map(child => child.children ? processSubMenus(child) : processBaseMenu(child))
    .filter(Boolean)

  // 动态菜单（从后端获取）
  const dynamicMenus = menuStore.menuList || []

  // 关于菜单
  const aboutMenu = rootRoute.children
    .filter(child => child.path.startsWith('/about'))
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
      if (menuItem && menuItem.type === HbtMenuType.Directory) {
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
    // 如果是目录菜单（以 dir_ 开头），只处理展开/折叠
    if (info.key.startsWith('dir_')) {
      const isOpen = openKeys.value.includes(info.key)
      if (isOpen) {
        // 如果已展开，则折叠
        openKeys.value = openKeys.value.filter(key => key !== info.key)
      } else {
        // 如果已折叠，则展开
        openKeys.value = [...openKeys.value, info.key]
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