<template>
  <a-layout class="basic-layout">
    <!-- 左侧菜单 -->
    <a-layout-sider 
      class="sider"
      :width="240"
      :theme="theme"
      :collapsed-width="48"
      :breakpoint="'lg'"
      v-model:collapsed="collapsed"
      :trigger="null"
    >
      <div class="logo">
        <img src="@/assets/images/logo.svg" alt="Logo" />
        <h1 v-show="!collapsed">Lean.Hbt</h1>
      </div>
      <hbt-sider-menu />
    </a-layout-sider>

    <!-- 右侧内容区 -->
    <a-layout :class="['main-container', { collapsed }]">
      <!-- 头部区域 -->
      <hbt-header-bar v-model:collapsed="collapsed" />

      <!-- 内容区域 -->
      <div class="content-wrapper">
        <!-- 面包屑导航 -->
        <hbt-breadcrumb v-if="!$route.meta.hideLayout" />

        <!-- 主内容区 -->
        <a-layout-content class="content">
          <router-view v-slot="{ Component }">
            <transition name="fade" mode="out-in">
              <component :is="Component" />
            </transition>
          </router-view>
        </a-layout-content>

        <!-- 页脚 -->
        <hbt-footer-bar v-if="!$route.meta.hideLayout" />
      </div>
    </a-layout>
  </a-layout>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useThemeStore } from '@/stores/theme'

// 组合式API和工具函数
const themeStore = useThemeStore()

// 响应式状态
const collapsed = ref(false)
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')
</script>

<style lang="less" scoped>
.basic-layout {
  min-height: 100vh;
  background-color: var(--ant-color-bg-layout);

  .sider {
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    overflow: auto;
    z-index: 10;

    &::-webkit-scrollbar {
      width: 6px;
      height: 6px;
    }

    &::-webkit-scrollbar-thumb {
      border-radius: 3px;
      background: var(--ant-color-text-quaternary);
    }

    .logo {
      height: 64px;
      padding: 16px;
      display: flex;
      align-items: center;
      justify-content: center;
      cursor: pointer;
      transition: all 0.3s;
      background: var(--ant-menu-inline-bg);

      img {
        height: 32px;
        width: auto;
      }

      h1 {
        height: 32px;
        margin: 0 0 0 12px;
        font-weight: 600;
        font-size: 18px;
        line-height: 32px;
        vertical-align: middle;
        animation: fade-in 0.3s;
        color: var(--ant-color-text);
      }
    }
  }

  .main-container {
    margin-left: 240px;
    transition: all 0.3s;
    background-color: var(--ant-color-bg-layout);

    &.collapsed {
      margin-left: 48px;
    }

    .content-wrapper {
      padding: 0;
      min-height: calc(100vh - 64px);
      margin-top: 64px;
      display: flex;
      flex-direction: column;
      background-color: var(--ant-color-bg-layout);

      .breadcrumb {
        padding: 16px 24px;
        margin-bottom: 0;
      }

      .content {
        flex: 1;
        padding: 24px;
        background-color: var(--ant-color-bg-layout);
      }
    }
  }
}

@keyframes fade-in {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .basic-layout {
    .sider {
      position: absolute;
      transform: translateX(-100%);
      
      .collapsed & {
        transform: translateX(0);
      }
    }

    .main-container {
      margin-left: 0;

      .content-wrapper {
        padding: 0;
        
        .content {
          padding: 12px;
        }
      }
    }
  }
}

// 过渡动画
.fade-enter-active,
.fade-leave-active {
  transition: opacity var(--ant-motion-duration-fast) var(--ant-motion-ease-in-out);
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>