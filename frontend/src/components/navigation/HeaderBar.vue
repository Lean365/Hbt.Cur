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
          <font-size-control />
          <fullscreen-toggle />
          <notification-center />
          <locale-select />
          <memorial-theme />
          <theme-switch />
          <a-dropdown :trigger="['click']" placement="bottom" class="user-dropdown">
            <a-button type="text">
              <template #icon>
                <user-outlined />
              </template>
            </a-button>
            <template #overlay>
              <a-menu>
                <a-menu-item key="profile">
                  <user-outlined />
                  <span>{{ t('header.profile') }}</span>
                </a-menu-item>
                <a-menu-item key="settings">
                  <skin-outlined />
                  <span>{{ t('header.settings') }}</span>
                </a-menu-item>
                <a-menu-divider />
                <a-menu-item key="logout">
                  <logout-outlined />
                  <span>{{ t('header.logout') }}</span>
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
import LocaleSelect from '../Base/LocaleSelect.vue'
import ThemeSwitch from '../Base/ThemeSwitch.vue'
import MemorialTheme from '../Base/MemorialTheme.vue'
import FontSizeControl from '../Base/FontSizeControl.vue'
import FullscreenToggle from '../Base/FullscreenToggle.vue'
import NotificationCenter from '../Base/NotificationCenter.vue'
import SystemSettings from '../Base/SystemSettings.vue'
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UserOutlined,
  SettingFilled,
  LogoutOutlined,
  ReloadOutlined,
  SkinOutlined
} from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'

const { t } = useI18n()
const router = useRouter()
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
    const { fullPath, meta } = router.currentRoute.value
    
    // 如果是 PageTabs 组件的页面，需要保持 tab 状态
    if (meta.keepAlive) {
      // 清除组件缓存
      const el = document.querySelector(`[data-key="${fullPath}"]`)
      if (el) {
        // 使用类型断言来访问Vue内部属性
        const vueEl = el as unknown as { __vueParentComponent: any }
        if (vueEl.__vueParentComponent) {
          const vnode = vueEl.__vueParentComponent
          if (vnode?.ctx?.deactivated) {
            vnode.ctx.deactivated()
          }
          if (vnode?.ctx?.activated) {
            vnode.ctx.activated()
          }
        }
      }
    }
    
    // 通过重定向刷新页面
    await router.replace({
      path: '/redirect' + fullPath
    })
    
    // 显示刷新成功提示
    message.success(t('refresh.success'))
  } catch (error) {
    console.error('Refresh failed:', error)
    message.error(t('refresh.failed'))
  } finally {
    // 延迟关闭加载状态，让动画效果更流畅
    setTimeout(() => {
      isRefreshing.value = false
    }, 1000)
  }
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

  :deep(.anticon) {
    font-size: 14px !important;  // 统一所有图标大小为14px
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
        gap: 8px !important;  // 增加间距确保一致性
      }

      :deep(.ant-space-item) {
        display: flex !important;
        align-items: center !important;
        justify-content: center !important;
      }

      .action-icon {
        font-size: 18px;
        cursor: pointer;
        padding: 0 8px;
        color: var(--ant-text-color);
        transition: all 0.3s;

        &:hover {
          color: var(--ant-primary-color);
        }

        &.active {
          color: var(--ant-primary-color);
        }
      }

      .user-dropdown {
        display: flex;
        align-items: center;
        justify-content: center;
      }

      :deep(.ant-dropdown-trigger) {
        display: flex;
        align-items: center;
        justify-content: center;
      }

      :deep(.ant-btn) {
        display: flex;
        align-items: center;
        justify-content: center;
        height: 32px;
        width: 32px;
        padding: 0;
      }

      :deep(.anticon) {
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 16px;
        line-height: 1;
      }
    }
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