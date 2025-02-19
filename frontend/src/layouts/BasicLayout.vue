<template>
  <a-config-provider :locale="antdLocale">
    <a-layout class="basic-layout">
      <!-- 左侧菜单 -->
      <a-layout-sider 
        class="sider"
        :width="240"
        theme="dark"
        :collapsed-width="48"
        :breakpoint="'lg'"
        v-model:collapsed="collapsed"
        :trigger="null"
      >
        <div class="logo">
          <img src="@/assets/images/logo.svg" alt="Logo" />
          <h1 v-show="!collapsed">Lean.Hbt</h1>
        </div>
        <sider-menu />
      </a-layout-sider>

      <!-- 右侧内容区 -->
      <a-layout :class="['main-container', { collapsed }]">
        <!-- 头部区域 -->
        <header-bar v-model:collapsed="collapsed" />

        <!-- 内容区域 -->
        <div class="content-wrapper">
          <!-- 面包屑导航 -->
          <breadcrumb />

          <!-- 主内容区 -->
          <a-layout-content class="content">
            <router-view v-slot="{ Component }">
              <transition name="fade" mode="out-in">
                <component :is="Component" />
              </transition>
            </router-view>
          </a-layout-content>

          <!-- 页脚 -->
          <footer-bar />
        </div>
      </a-layout>
    </a-layout>
  </a-config-provider>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import i18n from '../locales'
import { useThemeStore } from '@/stores/theme'
import SiderMenu from '@/components/navigation/SiderMenu.vue'
import HeaderBar from '@/components/navigation/HeaderBar.vue'
import Breadcrumb from '@/components/navigation/Breadcrumb.vue'
import FooterBar from '@/components/navigation/FooterBar.vue'

// 组合式API和工具函数
const { locale } = useI18n()
const themeStore = useThemeStore()

// 响应式状态
const collapsed = ref(false)

// 计算属性
const antdLocale = computed(() => {
  const currentLocale = locale.value
  const currentMessages = i18n.global.messages.value[currentLocale] || i18n.global.messages.value['zh-CN']
  return (currentMessages as any).antd || {}
})
</script>

<style lang="less" scoped>
.basic-layout {
  min-height: 100vh;

  .sider {
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    overflow: auto;
    z-index: 10;
    background: #001529;

    &::-webkit-scrollbar {
      width: 6px;
      height: 6px;
    }

    &::-webkit-scrollbar-thumb {
      background: rgba(0, 0, 0, 0.2);
      border-radius: 3px;
    }

    .logo {
      height: 64px;
      padding: 16px;
      display: flex;
      align-items: center;
      justify-content: center;
      background: #002140;
      cursor: pointer;
      transition: all 0.3s;

      img {
        height: 32px;
        width: auto;
      }

      h1 {
        height: 32px;
        margin: 0 0 0 12px;
        color: white;
        font-weight: 600;
        font-size: 18px;
        line-height: 32px;
        vertical-align: middle;
        animation: fade-in 0.3s;
      }
    }
  }

  .main-container {
    margin-left: 240px;
    transition: all 0.3s;

    &.collapsed {
      margin-left: 48px;
    }

    .content-wrapper {
      padding: 0;
      min-height: calc(100vh - 64px);
      margin-top: 64px;
      display: flex;
      flex-direction: column;

      .breadcrumb {
        padding: 16px 24px;
        margin-bottom: 0;
      }

      .content {
        flex: 1;
        padding: 24px;
        background: var(--ant-component-background);
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
</style>