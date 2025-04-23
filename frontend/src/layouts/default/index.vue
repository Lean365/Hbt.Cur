<template>
  <a-layout class="layout-default">
    <a-layout-header>
      <div class="logo" />
      <a-menu
        v-model:selectedKeys="selectedKeys"
        theme="dark"
        mode="horizontal"
        :items="menuItems"
      />
    </a-layout-header>
    <a-layout-content>
      <router-view />
    </a-layout-content>
    <a-layout-footer style="text-align: center">
      Lean.Hbt ©2024 Created by Your Company
    </a-layout-footer>
  </a-layout>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { MenuProps } from 'ant-design-vue'

const selectedKeys = ref<string[]>(['1'])

const menuItems: MenuProps['items'] = [
  {
    key: '1',
    label: '首页'
  }
]
</script>

<style scoped>
.layout-default {
  min-height: 100vh;
}

.logo {
  float: left;
  width: 120px;
  height: 31px;
  margin: 16px 24px 16px 0;
  background: rgba(255, 255, 255, 0.3);
}
</style> 