<template>
  <div class="header">
    <div class="left">
      <menu-unfold-outlined
        v-if="collapsed"
        class="trigger"
        @click="toggleCollapsed"
      />
      <menu-fold-outlined
        v-else
        class="trigger"
        @click="toggleCollapsed"
      />
    </div>
    <div class="right">
      <user-dropdown />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { MenuUnfoldOutlined, MenuFoldOutlined } from '@ant-design/icons-vue'
import UserDropdown from './UserDropdown.vue'

const collapsed = ref(false)

const toggleCollapsed = () => {
  collapsed.value = !collapsed.value
}
</script>

<style lang="less" scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 100%;
  padding: 0 16px;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);

  .left {
    display: flex;
    align-items: center;

    .trigger {
      font-size: 18px;
      cursor: pointer;
      transition: color 0.3s;

      &:hover {
        color: #1890ff;
      }
    }
  }

  .right {
    display: flex;
    align-items: center;
    gap: 16px;
  }
}
</style> 