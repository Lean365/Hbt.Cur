<template>
  <a-layout class="mix-layout">
    <!-- 顶部菜单 -->
    <a-layout-header class="mix-header">
      <div class="mix-header-content">
        <hbt-logo />
        <hbt-top-menu />
        <div class="header-tools">
          <a-space :size="4">
            <hbt-font-size />
            <hbt-full-screen />
            <hbt-notification-center />
            <hbt-online-users v-model:open="isOnlineUsersOpen" />
            <hbt-locale />
            <hbt-memorial />
            <hbt-header-user />
            <hbt-theme />
          </a-space>
        </div>
      </div>
    </a-layout-header>

    <a-layout class="mix-content">
      <!-- 侧边栏 -->
      <a-layout-sider
        v-model:collapsed="sidebarCollapsed"
        :trigger="null"
        collapsible
        class="mix-sider"
        :width="200"
        :collapsed-width="80"
      >
        <hbt-mix-menu />
      </a-layout-sider>

      <!-- 右侧内容区 -->
      <a-layout class="mix-main-container">
        <!-- 内容区域 -->
        <div class="content-wrapper">
          <!-- 面包屑导航 - 当显示标签页时隐藏面包屑 -->
          <hbt-breadcrumb v-if="!$route.meta.hideLayout && !configStore.isShowTabs" />

          <!-- 页面标签页 - 包含面包屑功能 -->
          <hbt-page-tabs v-if="!$route.meta.hideLayout && configStore.isShowTabs" />

          <!-- 主内容区 -->
          <a-layout-content class="content">
            <router-view v-slot="{ Component }">
              <transition name="fade" mode="out-in">
                <component :is="Component" />
              </transition>
            </router-view>
          </a-layout-content>

          <!-- 页脚 -->
          <hbt-footer-bar v-if="!$route.meta.hideLayout && configStore.isShowFooter" />
        </div>
      </a-layout>
    </a-layout>
  </a-layout>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { useConfigStore } from '@/stores/config'


// 组合式API和工具函数
const configStore = useConfigStore()

// 响应式状态
const isOnlineUsersOpen = ref(false)
const sidebarCollapsed = ref(false)
</script>

<style lang="less" scoped>
.mix-layout {
  min-height: 100vh;
  background-color: var(--ant-color-bg-layout);

  .mix-header {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    z-index: 9;
    padding: 0;
    height: 64px;
    line-height: 64px;
    background-color: var(--ant-color-bg-container);
    border-bottom: 1px solid var(--ant-color-split);

    .mix-header-content {
      display: flex;
      align-items: center;
      justify-content: space-between;
      height: 100%;
      padding: 0 24px;

      .header-tools {
        display: flex;
        align-items: center;
      }
    }
  }

  .mix-content {
    margin-top: 64px;

    .mix-sider {
      position: fixed;
      left: 0;
      top: 64px;
      bottom: 0;
      z-index: 8;
      background-color: var(--ant-color-bg-container);
      border-right: 1px solid var(--ant-color-split);
    }

    .mix-main-container {
      margin-left: 200px;
      background-color: var(--ant-color-bg-layout);

      .content-wrapper {
        padding: 0;
        min-height: calc(100vh - 64px - 40px);
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
}

// 响应式布局
@media screen and (max-width: 768px) {
  .mix-layout {
    .mix-header {
      .mix-header-content {
        padding: 0 16px;
      }
    }

    .mix-content {
      .mix-main-container {
        .content-wrapper {
          padding: 0;
          
          .content {
            padding: 12px;
          }
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