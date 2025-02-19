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
import type { MenuInfo } from 'ant-design-vue/lib/menu/src/interface'
import { useThemeStore } from '@/stores/theme'
import { useMenuStore } from '@/stores/menu'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const themeStore = useThemeStore()
const menuStore = useMenuStore()

// 响应式状态
const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

// 菜单配置
const menuItems = computed<Required<MenuProps>['items']>(() => {
  // 如果正在加载，返回空数组
  if (menuStore.isLoading) {
    console.log('[菜单组件] 菜单正在加载中')
    return []
  }
  
  // 如果有后端返回的菜单数据，使用后端数据
  if (menuStore.menuList && menuStore.menuList.length > 0) {
    console.log('[菜单组件] 渲染菜单数据:', menuStore.menuList)
    return menuStore.menuList
  }

  console.log('[菜单组件] 没有菜单数据')
  return []
})

// 监听路由变化，更新选中状态
watch(() => route.path, (path) => {
  console.log('[菜单组件] 路由变化:', path)
  
  // 确保路径格式一致
  const normalizedPath = path.startsWith('/') ? path : `/${path}`
  selectedKeys.value = [normalizedPath]
  
  // 更新展开的菜单
  const pathParts = normalizedPath.split('/').filter(Boolean)
  if (pathParts.length > 1) {
    // 如果是多级路径，展开所有父级
    const parentKeys = pathParts.reduce((acc: string[], curr: string, index: number) => {
      if (index === 0) {
        acc.push(`/${curr}`)
      } else {
        const parentPath = acc[index - 1]
        acc.push(`${parentPath}/${curr}`)
      }
      return acc
    }, [])
    
    console.log('[菜单组件] 展开的菜单:', parentKeys)
    openKeys.value = parentKeys
  }
}, { immediate: true })

// 监听菜单数据变化
watch(() => menuStore.menuList, (newMenuList) => {
  console.log('[菜单组件] 菜单数据变化:', {
    hasData: !!newMenuList,
    length: newMenuList?.length,
    isLoading: menuStore.isLoading
  })
}, { immediate: true })

// 处理菜单点击
const handleMenuClick = (info: MenuInfo) => {
  console.log('[菜单组件] 菜单点击:', info)
  if (typeof info.key === 'string') {
    const path = info.key.startsWith('/') ? info.key : `/${info.key}`
    // 如果点击的是已选中的菜单项，不进行路由跳转
    if (selectedKeys.value.includes(path)) {
      return
    }
    router.push(path)
  }
}
</script>

<style lang="less" scoped>
.side-menu {
  height: 100%;
  border-right: 0;

  :deep(.ant-menu-item) {
    margin: 4px 0;
    
    &:hover {
      color: var(--ant-color-primary);
      background-color: var(--ant-color-primary-bg);
    }
    
    &.ant-menu-item-selected {
      color: var(--ant-color-primary);
      background-color: var(--ant-color-primary-bg);
      
      &:hover {
        color: var(--ant-color-primary-hover);
        background-color: var(--ant-color-primary-bg-hover);
      }
    }
  }

  :deep(.ant-menu-submenu) {
    &-title {
      &:hover {
        color: var(--ant-color-primary);
        background-color: var(--ant-color-primary-bg);
      }
    }
  }

  :deep(.ant-menu-title-content) {
    transition: color 0.3s;
  }

  :deep(.anticon) {
    min-width: 14px;
    margin-right: 10px;
    font-size: 16px;
  }
}

// 暗色主题
:deep(.ant-menu.ant-menu-dark) {
  .ant-menu-item,
  .ant-menu-submenu-title {
    &:hover {
      background-color: var(--ant-menu-dark-item-active-bg);
    }

    &.ant-menu-item-selected {
      background-color: var(--ant-menu-dark-item-selected-bg);
    }
  }
}
</style> 