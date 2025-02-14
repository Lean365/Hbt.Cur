<template>
  <a-config-provider :locale="antdLocale">
    <a-layout class="layout">
      <a-layout-header>
        <div class="logo">
          <router-link to="/">
            <img src="@/assets/images/logo.svg" alt="Lean.Hbt" />
          </router-link>
        </div>
        <a-menu
          v-model:selectedKeys="selectedKeys"
          theme="dark"
          mode="horizontal"
          :items="menuItems"
        />
        <a-select
          v-model:value="currentLocale"
          class="locale-select"
          :options="localeOptions"
          @change="handleLocaleChange"
        />
      </a-layout-header>
      <a-layout>
        <a-layout-sider>
          <a-menu
            v-model:selectedKeys="selectedKeys"
            v-model:openKeys="openKeys"
            mode="inline"
            :items="sideMenuItems"
          />
        </a-layout-sider>
        <a-layout>
          <a-breadcrumb>
            <a-breadcrumb-item>{{ t('menu.home') }}</a-breadcrumb-item>
            <a-breadcrumb-item>{{ currentRoute }}</a-breadcrumb-item>
          </a-breadcrumb>
          <a-layout-content>
            <router-view></router-view>
          </a-layout-content>
        </a-layout>
      </a-layout>
    </a-layout>
  </a-config-provider>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import type { MenuProps, SelectValue } from 'ant-design-vue'
import type { DefaultOptionType } from 'ant-design-vue/es/select'
import { messages } from '../locales'

const { t, locale } = useI18n()
const route = useRoute()
const selectedKeys = ref<string[]>(['1'])
const openKeys = ref<string[]>(['sub1'])
const currentLocale = ref(locale.value)

// 语言选项
const localeOptions = [
  { value: 'zh-CN', label: '简体中文' },
  { value: 'en-US', label: 'English' }
]

// 语言切换处理函数
const handleLocaleChange = (value: any, option: DefaultOptionType | DefaultOptionType[]) => {
  if (typeof value === 'string') {
    locale.value = value
    localStorage.setItem('locale', value)
  }
}

// 获取当前语言的菜单配置
const currentLocaleMessages = computed(() => {
  const currentLocale = locale.value as keyof typeof messages
  return messages[currentLocale] || messages['zh-CN']
})

// 获取当前语言的Ant Design Vue语言包
const antdLocale = computed(() => {
  const currentMessages = currentLocaleMessages.value
  return (currentMessages as any).antd || {}
})

// 顶部菜单项
const menuItems = computed<MenuProps['items']>(() => [
  {
    key: '1',
    label: t('menu.home')
  },
  {
    key: '2',
    label: t('menu.workspace')
  },
  {
    key: '3',
    label: t('menu.systemManagement')
  }
])

// 侧边栏菜单项
const sideMenuItems = computed<MenuProps['items']>(() => [
  {
    key: 'sub1',
    label: t('menu.dashboard.title'),
    children: [
      { key: '1', label: t('menu.dashboard.analysis') },
      { key: '2', label: t('menu.dashboard.monitor') },
      { key: '3', label: t('menu.dashboard.workplace') }
    ]
  },
  {
    key: 'sub2',
    label: t('menu.form.title'),
    children: [
      { key: '4', label: t('menu.form.basic') },
      { key: '5', label: t('menu.form.step') },
      { key: '6', label: t('menu.form.advanced') }
    ]
  }
])

// 当前路由名称
const currentRoute = computed(() => route.meta.title || t('menu.home'))
</script>

<style lang="less">
.logo {
  float: left;
  height: 32px;
  margin: 16px 24px 16px 0;
  
  img {
    height: 100%;
    width: auto;
  }
}

.locale-select {
  float: right;
  margin: 16px 24px;
  width: 120px;
}
</style> 