<template>
  <a-layout-header :class="['header', { collapsed }]">
    <div class="header-content">
      <!-- 左侧区域：收缩按钮和刷新按钮 -->
      <div class="header-left">
        <menu-fold-outlined
          v-if="!collapsed"
          class="trigger"
          @click="toggleCollapsed(true)"
        />
        <menu-unfold-outlined
          v-else
          class="trigger"
          @click="toggleCollapsed(false)"
        />
        <reload-outlined 
          class="trigger" 
          :class="{ 'loading': isRefreshing }"
          @click="handleRefresh" 
        />
      </div>
      
      <!-- 右侧工具栏 -->
      <div class="header-right">
        <a-space :size="4">
          <font-size />
          <full-screen />
          <notification-center />
          <locale-select />
          <memorial-theme />
          <theme-select />
          <a-dropdown :trigger="['click']" placement="bottom" class="user-dropdown">
            <div class="user-info">
              <a-avatar :size="32" :src="userStore.user?.avatar">
                <template #icon>
                  <user-outlined />
                </template>
              </a-avatar>
              <span class="username">{{ userStore.user?.nickName || userStore.user?.englishName || userStore.user?.userName }}</span>
            </div>
            <template #overlay>
              <a-menu class="user-menu">
                <a-menu-item key="profile" @click="handleProfile">
                  <template #icon><user-outlined /></template>
                  <span>{{ t('header.user.profile') }}</span>
                </a-menu-item>
                <a-menu-item key="change-password" @click="handleChangePassword">
                  <template #icon><key-outlined /></template>
                  <span>{{ t('header.user.changePassword') }}</span>
                </a-menu-item>
                <a-menu-item key="clear-cache" @click="handleClearCache">
                  <template #icon><clear-outlined /></template>
                  <span>{{ t('header.user.clearCache') }}</span>
                </a-menu-item>
                <a-menu-divider />
                <a-menu-item key="logout" @click="handleLogout">
                  <template #icon><logout-outlined /></template>
                  <span>{{ t('header.user.logout') }}</span>
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
          <system-settings />
        </a-space>
      </div>
    </div>
  </a-layout-header>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user'
import { Modal } from 'ant-design-vue'

import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UserOutlined,
  LogoutOutlined,
  ReloadOutlined,
  ClearOutlined,
  KeyOutlined
} from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()
const isRefreshing = ref(false)

// Props
interface Props {
  collapsed: boolean
}
const props = withDefaults(defineProps<Props>(), {
  collapsed: false
})

// Emits
const emit = defineEmits<{
  (e: 'update:collapsed', value: boolean): void
}>()

// Methods
const toggleCollapsed = (value: boolean) => {
  emit('update:collapsed', value)
}

const handleRefresh = async () => {
  if (isRefreshing.value) return
  isRefreshing.value = true

  try {
    // 获取当前路由信息
    const currentRoute = router.currentRoute.value
    if (!currentRoute) {
      throw new Error('当前路由信息不存在')
    }
    
    const { fullPath, meta } = currentRoute
    
    // 如果是 PageTabs 组件的页面，需要保持 tab 状态
    if (meta?.keepAlive) {
      // 清除组件缓存
      const el = document.querySelector(`[data-key="${fullPath}"]`)
      if (el) {
        // 使用类型断言来访问Vue内部属性
        const vueEl = el as unknown as { __vueParentComponent: any }
        if (vueEl?.__vueParentComponent?.ctx) {
          const vnode = vueEl.__vueParentComponent
          vnode.ctx?.deactivated?.()
          vnode.ctx?.activated?.()
        }
      }
    }
    
    // 通过重定向刷新页面
    await router.replace({
      path: '/redirect' + fullPath
    })
    
    // 显示刷新成功提示
    message.success(t('header.refresh.success'))
  } catch (error) {
    console.error('刷新失败:', error)
    message.error(t('header.refresh.failed'))
  } finally {
    // 延迟关闭加载状态，让动画效果更流畅
    setTimeout(() => {
      isRefreshing.value = false
    }, 1000)
  }
}

// 处理个人信息
const handleProfile = () => {
  router.push('/identity/user/profile')
}

// 处理登出
const handleLogout = () => {
  Modal.confirm({
    title: t('header.logout.title'),
    content: t('header.logout.confirm'),
    okText: t('common.button.confirm'),
    cancelText: t('common.actions.cancel'),
    centered: true,
    async onOk() {
      try {
        // 1. 调用登出接口
        await userStore.logout()
        
        // 2. 清除本地存储的 token
        localStorage.removeItem('token')
        sessionStorage.removeItem('token')
        
        // 3. 清除路由缓存
        const routes = router.getRoutes()
        routes.forEach(route => {
          if (route.meta?.keepAlive) {
            const el = document.querySelector(`[data-key="${route.path}"]`)
            if (el) {
              const vueEl = el as unknown as { __vueParentComponent: any }
              if (vueEl?.__vueParentComponent?.ctx) {
                const vnode = vueEl.__vueParentComponent
                vnode.ctx?.deactivated?.()
              }
            }
          }
        })
        
        // 4. 清除用户信息
        userStore.$reset()
        
        // 5. 显示成功消息
        message.success(t('header.logout.success'))
        
        // 6. 跳转到登录页
        router.push('/login')
      } catch (error) {
        console.error('登出失败:', error)
        message.error(t('header.logout.failed'))
      }
    }
  })
}

// 添加清除缓存方法
const handleClearCache = async () => {
  try {
    // 清除组件缓存
    const el = document.querySelector('.ant-layout-content')
    if (el) {
      const vueEl = el as unknown as { __vueParentComponent: any }
      if (vueEl?.__vueParentComponent?.ctx) {
        const vnode = vueEl.__vueParentComponent
        vnode.ctx?.deactivated?.()
        vnode.ctx?.activated?.()
      }
    }
    // 清除路由缓存
    router.replace({
      path: '/redirect' + router.currentRoute.value.fullPath
    })
    message.success(t('header.clearCache.success'))
  } catch (error) {
    console.error('清除缓存失败:', error)
    message.error(t('header.clearCache.failed'))
  }
}

// 添加修改密码方法
const handleChangePassword = () => {
  router.push('/identity/user/change-password')
}
</script>

<style lang="less" scoped>
.header {
  position: fixed;
  top: 0;
  right: 0;
  left: 240px;
  height: 64px;
  padding: 0;
  background: var(--ant-component-background);
  border-bottom: 1px solid var(--ant-border-color-split);
  z-index: 9;
  transition: all 0.3s;

  &.collapsed {
    left: 48px;
  }

  .header-content {
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;

    .header-left {
      display: flex;
      align-items: center;
      
      .trigger {
        font-size: 18px;
        cursor: pointer;
        transition: all 0.3s;
        color: var(--ant-text-color);
        padding: 0 12px;

        &:hover {
          color: var(--ant-primary-color);
        }

        &.loading {
          animation: loading-rotate 1s linear infinite;
        }
      }
    }

    .header-right {
      margin-left: auto;
      padding-right: 24px;

      :deep(.ant-space) {
        gap: 8px !important;
      }

      :deep(.ant-space-item) {
        display: flex !important;
        align-items: center !important;
        justify-content: center !important;
      }
    }
  }
}

.user-dropdown {
  display: flex;
  align-items: center;
  cursor: pointer;
  transition: all 0.3s;

  &:hover {
    background: var(--ant-primary-1);
  }

  .user-info {
    display: flex;
    align-items: center;
    gap: 8px;
    padding: 0 8px;
    
    .username {
      max-width: 100px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
      color: var(--ant-text-color);
      font-size: 14px;
    }
  }
}

:deep(.user-menu) {
  min-width: 160px;
  
  .ant-dropdown-menu-item {
    display: flex;
    align-items: center;
    padding: 8px 16px;
    
    .anticon {
      font-size: 16px;
      margin-right: 8px;
    }
    
    &:hover {
      background: var(--ant-primary-1);
    }
  }
  
  .ant-dropdown-menu-item-divider {
    margin: 4px 0;
  }
}

@keyframes loading-rotate {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .header {
    left: 0;
    
    .header-content {
      .header-left {
        left: 0;
        
        .trigger {
          padding: 0 16px;
        }
      }
      
      .header-right {
        padding-right: 16px;
      }
    }
  }
}
</style> 