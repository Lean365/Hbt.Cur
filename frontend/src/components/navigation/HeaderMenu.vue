<template>
  <a-menu
    v-model:selectedKeys="selectedKeys"
    :theme="theme"
    mode="horizontal"
    :items="menuItems"
    class="header-menu"
    @select="handleSelect"
  />
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import type { MenuProps } from 'ant-design-vue'
import { useThemeStore } from '@/stores/theme'
import {
  DashboardOutlined,
  TeamOutlined,
  SettingOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()
const router = useRouter()
const themeStore = useThemeStore()

const selectedKeys = ref<string[]>(['dashboard'])
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

// 菜单配置
const menuItems = computed<MenuProps['items']>(() => [
  {
    key: 'dashboard',
    icon: () => h(DashboardOutlined),
    label: t('dashboard.title'),
    children: [
      { 
        key: 'analysis',
        label: t('dashboard.analysis')
      },
      { 
        key: 'monitor',
        label: t('dashboard.monitor')
      },
      { 
        key: 'workplace',
        label: t('dashboard.workplace')
      }
    ]
  },
  {
    key: 'identity',
    icon: () => h(TeamOutlined),
    label: t('identity.title'),
    children: [
      { 
        key: 'user',
        label: t('identity.user.title')
      },
      { 
        key: 'role',
        label: t('identity.role.title')
      },
      { 
        key: 'menu',
        label: t('identity.menu.title')
      }
    ]
  },
  {
    key: 'settings',
    icon: () => h(SettingOutlined),
    label: t('settings.title')
  }
])

// 处理菜单选择
const handleSelect = ({ key }: { key: string }) => {
  router.push(`/${key}`)
}
</script>

<style lang="less" scoped>
.header-menu {
  flex: 1;
  min-width: 0;
  height: 48px;
  line-height: 48px;
  border: none;
  background: transparent;

  :deep(.ant-menu-item),
  :deep(.ant-menu-submenu) {
    height: 48px;
    line-height: 48px;
    padding: 0 12px;
    
    .anticon {
      margin-right: 8px;
    }
    
    &::after {
      display: none;
    }
    
    &:hover {
      background: rgba(0, 0, 0, 0.025);
    }
  }

  :deep(.ant-menu-submenu-title) {
    height: 48px;
    line-height: 48px;
  }

  &.ant-menu-dark {
    :deep(.ant-menu-item),
    :deep(.ant-menu-submenu) {
      &:hover {
        background: rgba(255, 255, 255, 0.1);
      }
    }
  }
}
</style>