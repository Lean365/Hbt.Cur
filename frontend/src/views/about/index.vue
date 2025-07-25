<template>
  <div class="about-container">
    <a-card :bordered="false">
      <template #title>
        <h2>关于 Lean.Hbt</h2>
      </template>
      <a-typography>
        <a-typography-title :level="3">系统简介</a-typography-title>
        <a-typography-paragraph>
          Lean.Hbt 是一个现代化的企业级管理系统，采用前后端分离架构，基于 .NET Core 和 Vue.js 开发。系统提供了丰富的功能模块，包括用户管理、权限控制、系统监控、工作流等，帮助企业高效管理各项业务。
        </a-typography-paragraph>

        <a-typography-title :level="3">技术栈</a-typography-title>
        <a-typography-paragraph>
          <ul>
            <li>
              <strong>前端：</strong>
              <ul>
                <li>Vue 3 - 渐进式 JavaScript 框架</li>
                <li>TypeScript - 类型安全的 JavaScript 超集</li>
                <li>Ant Design Vue - 企业级 UI 组件库</li>
                <li>Vite - 下一代前端构建工具</li>
                <li>Pinia - Vue 状态管理库</li>
              </ul>
            </li>
            <li>
              <strong>后端：</strong>
              <ul>
                <li>.NET Core - 跨平台的高性能框架</li>
                <li>Entity Framework Core - ORM 框架</li>
                <li>AutoMapper - 对象映射工具</li>
                <li>NLog - 日志记录框架</li>
              </ul>
            </li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">主要特性</a-typography-title>
        <a-typography-paragraph>
          <ul>
            <li>完善的权限管理：基于 RBAC 的权限控制，支持多租户</li>
            <li>优雅的界面设计：遵循 Ant Design 设计规范，提供舒适的用户体验</li>
            <li>丰富的组件：内置大量实用组件，快速开发业务功能</li>
            <li>国际化支持：内置多语言支持，轻松切换不同语言</li>
            <li>主题定制：支持明暗主题切换，可自定义主题色</li>
            <li>安全性：采用 JWT 认证，支持 OAuth2.0 第三方登录</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">版本信息</a-typography-title>
        <a-descriptions :column="2">
          <a-descriptions-item label="当前版本">v{{ versionInfo.version }}</a-descriptions-item>
          <a-descriptions-item label="应用名称">{{ versionInfo.name }}</a-descriptions-item>
          <a-descriptions-item label="构建时间">{{ versionInfo.buildTime }}</a-descriptions-item>
          <a-descriptions-item label="授权协议">MIT License</a-descriptions-item>
          <a-descriptions-item label="技术支持">support@lean365.com</a-descriptions-item>
        </a-descriptions>

        <a-typography-title :level="3">相关链接</a-typography-title>
        <a-space>
          <a-button type="primary" href="https://github.com/Lean365/Lean.Hbt" target="_blank">
            <template #icon><github-outlined /></template>
            GitHub
          </a-button>
          <a-button href="https://lean365.cn/docs" target="_blank">
            <template #icon><book-outlined /></template>
            在线文档
          </a-button>
          <a-button href="https://lean365.cn" target="_blank">
            <template #icon><global-outlined /></template>
            官方网站
          </a-button>
        </a-space>
      </a-typography>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { getVersionInfo } from '@/utils/version'
import {
  GithubOutlined,
  GlobalOutlined,
  BookOutlined
} from '@ant-design/icons-vue'

const versionInfo = computed(() => getVersionInfo())

defineOptions({
  name: 'About'
})
</script>

<style lang="less" scoped>
.about-container {
  padding: 24px;
  max-width: 1200px;
  margin: 0 auto;

  :deep(.ant-typography) {
    color: var(--ant-text-color);
  }

  :deep(ul) {
    padding-left: 20px;
  }

  .ant-space {
    margin-top: 16px;
  }
}
</style>