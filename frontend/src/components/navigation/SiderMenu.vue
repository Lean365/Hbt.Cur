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
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import type { MenuProps } from 'ant-design-vue'
import type { MenuInfo, ItemType } from 'ant-design-vue/lib/menu/src/interface'
import { useThemeStore } from '@/stores/theme'
import { useMenuStore } from '@/stores/menu'
import type { Menu } from '@/types/identity/menu'
import { HbtMenuType } from '@/types/base'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const themeStore = useThemeStore()
const menuStore = useMenuStore()

// 响应式状态
const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

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

// 菜单配置
const menuItems = computed<Required<MenuProps>['items']>(() => {
  if (menuStore.isLoading) {
    console.log('[菜单组件] 菜单加载中')
    return []
  }
  
  if (menuStore.menuList && menuStore.menuList.length > 0) {
    console.log('[菜单组件] 构建菜单项:', JSON.stringify(menuStore.menuList, null, 2))
    return menuStore.menuList
  }

  console.log('[菜单组件] 无菜单数据')
  return []
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