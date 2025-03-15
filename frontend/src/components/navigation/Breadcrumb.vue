<template>
  <div class="breadcrumb-wrapper">
    <a-breadcrumb>
      <!-- 首页 -->
      <a-breadcrumb-item>
        <router-link to="/" class="breadcrumb-link">
          <home-outlined />
          <span>{{ t('home.title') }}</span>
        </router-link>
      </a-breadcrumb-item>
      
      <!-- 动态面包屑 -->
      <template v-for="item in breadcrumbItems" :key="item.path">
        <a-breadcrumb-item>
          <router-link 
            v-if="item.path !== route.path" 
            :to="item.path"
            class="breadcrumb-link"
          >
            <component :is="getIcon(item.icon)" v-if="item.icon" />
            <span>{{ t(item.title) }}</span>
          </router-link>
          <template v-else>
            <div class="breadcrumb-link">
              <component :is="getIcon(item.icon)" v-if="item.icon" />
              <span>{{ t(item.title) }}</span>
            </div>
          </template>
        </a-breadcrumb-item>
      </template>
    </a-breadcrumb>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import * as Icons from '@ant-design/icons-vue'

const { t } = useI18n()
const route = useRoute()

// 获取图标组件
const getIcon = (iconName?: string) => {
  if (!iconName) return null
  return (Icons as any)[iconName]
}

// 面包屑项接口定义
interface BreadcrumbItem {
  title: string
  icon?: string
  path: string
}

// 计算面包屑项
const breadcrumbItems = computed<BreadcrumbItem[]>(() => {
  const items: BreadcrumbItem[] = []
  // 过滤掉 Layout 和 Home 路由，只保留实际的导航路径
  const matched = route.matched.filter(item => 
    item.name !== 'Layout' && 
    item.name !== 'Home' && 
    item.meta?.title
  )
  
  // 构建面包屑路径
  matched.forEach((item, index) => {
    // 使用当前路由的完整路径，而不是累积构建
    items.push({
      title: item.meta?.title as string || '',
      icon: item.meta?.icon as string | undefined,
      path: item.path || ''
    })
  })
  
  return items
})
</script>

<style lang="less" scoped>
.breadcrumb-wrapper {
  margin: 16px 24px;
  
  :deep(.ant-breadcrumb) {
    .breadcrumb-link {
      display: inline-flex;
      align-items: center;
      gap: 4px;
      height: 22px;
      line-height: 22px;
      color: var(--ant-text-color-secondary);
      transition: color 0.3s;
      
      &:hover {
        color: var(--ant-primary-color);
      }

      .anticon {
        font-size: 14px;
      }

      span {
        display: inline-block;
      }
    }

    .ant-breadcrumb-separator {
      margin: 0 8px;
      display: inline-flex;
      align-items: center;
      height: 22px;
    }
  }
}
</style>