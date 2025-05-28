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
          <hbt-font-size />
          <hbt-full-screen />
          <hbt-notification-center />
          <hbt-online-users :open="false" />
          <hbt-locale />
          <hbt-memorial />
          <hbt-theme />
          <!-- 租户切换 -->
          <a-dropdown v-model:open="tenantOpen" :trigger="['click']">
            <div class="tenant-switch" @click="handleTenantClick">
              <span class="tenant-name">{{ currentTenant?.label || t('common.tenant.select') }}</span>
              <down-outlined />
            </div>
            <template #overlay>
              <a-menu v-model:selectedKeys="selectedTenantKeys" @click="handleTenantSelect">
                <a-menu-item v-for="tenant in tenantList" :key="tenant.value">
                  <template #icon>
                    <check-outlined v-if="tenant.value === currentTenant?.value" />
                  </template>
                  {{ tenant.label }}
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
          <a-dropdown :trigger="['click']" placement="bottom" class="user-dropdown">
            <div class="user-info">
              <a-avatar :size="32" :src="avatarUrl">
                <template #icon>
                  <user-outlined />
                </template>
              </a-avatar>
              <!-- <span class="username">{{ userStore.userInfo?.nickName || userStore.userInfo?.englishName || userStore.userInfo?.userName }}</span> -->
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
          <hbt-setting />
        </a-space>
      </div>
    </div>
  </a-layout-header>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
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
  KeyOutlined,
  DownOutlined,
  CheckOutlined
} from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import UserDropdown from './UserDropdown.vue'
import { getTenantOptions } from '@/api/identity/tenant'
import type { HbtTenantOption } from '@/types/identity/tenant'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'

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

// 处理修改密码
const handleChangePassword = () => {
  router.push('/identity/user/change-password')
}

// 处理清除缓存
const handleClearCache = () => {
  Modal.confirm({
    title: t('header.user.clearCache'),
    content: t('header.user.clearCacheConfirm'),
    okText: t('common.button.confirm'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        // 清除路由缓存
        const router = useRouter()
        router.getRoutes().forEach(route => {
          if (route.meta?.keepAlive) {
            const el = document.querySelector(`[data-key="${route.path}"]`)
            if (el) {
              const vueEl = el as unknown as { __vueParentComponent: any }
              if (vueEl?.__vueParentComponent?.ctx) {
                const vnode = vueEl.__vueParentComponent
                vnode.ctx?.deactivated?.()
                vnode.ctx?.activated?.()
              }
            }
          }
        })
        
        // 清除本地存储
        localStorage.clear()
        sessionStorage.clear()
        
        message.success(t('header.user.clearCacheSuccess'))
      } catch (error) {
        console.error('清除缓存失败:', error)
        message.error(t('header.user.clearCacheFailed'))
      }
    }
  })
}

// 处理退出登录
const handleLogout = async () => {
  try {
    await userStore.logout()
    // 跳转到登录页
    router.push('/login')
  } catch (error) {
    console.error('退出登录失败:', error)
    message.error(t('header.user.logoutFailed'))
  }
}

// 计算头像URL
const avatarUrl = computed(() => userStore.userInfo?.avatar || '')

// 租户切换相关
const tenantOpen = ref(false)
const tenantList = ref<HbtTenantOption[]>([])
const selectedTenantKeys = ref<string[]>([])
const currentTenant = ref<HbtTenantOption | null>(null)

// 获取租户列表
const fetchTenantList = async () => {
  try {
    const { data } = await getTenantOptions()
    if (data.code === 200 && Array.isArray(data.data)) {
      tenantList.value = data.data
      // 设置当前选中的租户
      const currentTenantId = userStore.getCurrentTenantId()
      const foundTenant = tenantList.value.find(t => t.value === currentTenantId)
      currentTenant.value = foundTenant || null
      if (currentTenant.value) {
        selectedTenantKeys.value = [currentTenant.value.value.toString()]
      }
    }
  } catch (error) {
    console.error('获取租户列表失败:', error)
  }
}

// 处理租户点击
const handleTenantClick = () => {
  if (tenantList.value.length === 0) {
    fetchTenantList()
  }
}

// 处理租户选择
const handleTenantSelect = async (info: MenuInfo) => {
  const tenantId = parseInt(info.key as string)
  if (tenantId === currentTenant.value?.value) {
    return
  }

  try {
    await userStore.setCurrentTenantId(tenantId)
    message.success(t('common.tenant.switchSuccess'))
    // 刷新页面以应用新的租户设置
    window.location.reload()
  } catch (error) {
    console.error('切换租户失败:', error)
    message.error(t('common.tenant.switchFailed'))
  }
}

onMounted(() => {
  fetchTenantList()
})
</script>

<style lang="less" scoped>
.header {
  position: fixed;
  top: 0;
  right: 0;
  left: 240px;
  z-index: 9;
  padding: 0;
  height: 64px;
  line-height: 64px;
  transition: all 0.3s;
  background-color: var(--ant-color-bg-container);
  border-bottom: 1px solid var(--ant-color-split);

  &.collapsed {
    left: 48px;
  }

  .header-content {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 100%;

    .header-left {
      display: flex;
      align-items: center;

      .trigger {
        padding: 0 24px;
        font-size: 18px;
        cursor: pointer;
        transition: color 0.3s;
        color: var(--ant-color-text);

        &:hover {
          color: var(--ant-color-primary);
        }

        &.loading {
          animation: loading-rotate 1s linear infinite;
        }
      }
    }

    .header-right {
      display: flex;
      align-items: center;
      padding-right: 24px;

      .user-dropdown {
        margin-left: 8px;
        cursor: pointer;

        .user-info {
          display: flex;
          align-items: center;
          padding: 0 8px;

          .username {
            margin-left: 8px;
            color: var(--ant-color-text);
          }
        }
      }
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

.tenant-switch {
  display: flex;
  align-items: center;
  cursor: pointer;
  padding: 0 12px;
  height: 100%;
  
  &:hover {
    background: rgba(0, 0, 0, 0.025);
  }

  .tenant-name {
    margin-right: 8px;
    max-width: 120px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
}
</style> 